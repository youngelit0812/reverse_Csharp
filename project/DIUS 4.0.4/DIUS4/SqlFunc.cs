using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DIUS4.Shield;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace DIUS4
{
	// Token: 0x0200001C RID: 28
	internal class SqlFunc
	{
		// Token: 0x0600013B RID: 315 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public SqlFunc()
		{
			this.Path = Application.StartupPath;
			this.Server = "";
			this.ServerUser = "";
			this.ServerPass = "";
			RegistryKey registryKey = null;
			Protect protect = new Protect(ref registryKey)
			{
				Mode = 0
			};
			protect.SubKey = (protect.GetCompInfo().GetHashCode().ToString() + "00000000").Substring(0, 8);
			this.LocalPass = protect.OutKey();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		internal DataTable Get_DataTable(string SqlCommand, string BaseName)
		{
			DataTable dataTable = new DataTable();
			SqlCeConnection sqlCeConnection = new SqlCeConnection(string.Format("DATASOURCE={0}{1};Password={2};Persist Security Info=True", this.Path, BaseName, this.LocalPass));
			SqlCeDataAdapter sqlCeDataAdapter = new SqlCeDataAdapter(SqlCommand, sqlCeConnection);
			sqlCeDataAdapter.Fill(dataTable);
			sqlCeDataAdapter.Dispose();
			sqlCeConnection.Close();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["id"]
			};
			dataTable.TableName = Conversions.ToString(dataTable.Rows.Count);
			return dataTable;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000DE58 File Offset: 0x0000C058
		public void StoreDataTable(string BaseName, string TableName, DataTable dt)
		{
			if (dt.Rows.Count != 0)
			{
				string[] array = new string[dt.Columns.Count - 1 + 1];
				object[] array2 = new object[dt.Columns.Count - 1 + 1];
				int num = dt.Rows.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					int num2 = dt.Columns.Count - 1;
					for (int j = 0; j <= num2; j++)
					{
						array[j] = dt.Columns[j].ColumnName;
						array2[j] = RuntimeHelpers.GetObjectValue(dt.Rows[i][j]);
					}
					if (BaseName.Contains("."))
					{
						this.StoreSqlRow(BaseName, TableName, array, array2);
					}
					else
					{
						this.StoreExtSqlRow(BaseName, TableName, array, array2);
					}
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000DF30 File Offset: 0x0000C130
		internal DataTable Get_ExtDataTable(string SqlCommand, string BaseName)
		{
			DataTable dataTable = new DataTable();
			SqlConnection sqlConnection = new SqlConnection(string.Format("Server={0};Database={1};User ID={2};Password={3};Trusted_Connection=False;", new object[]
			{
				this.Server,
				BaseName,
				this.ServerUser,
				this.ServerPass
			}));
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlCommand, sqlConnection);
			sqlDataAdapter.Fill(dataTable);
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["id"]
			};
			dataTable.TableName = Conversions.ToString(dataTable.Rows.Count);
			return dataTable;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		internal int SQLExtExecute(string SqlCommand, string BaseName)
		{
			SqlCeConnection sqlCeConnection = new SqlCeConnection(string.Format("DATASOURCE={0}{1};Password={2};Persist Security Info=True", this.Path, BaseName, this.LocalPass));
			SqlCeCommand sqlCeCommand = new SqlCeCommand();
			sqlCeCommand.Connection = sqlCeConnection;
			if (sqlCeConnection.State == ConnectionState.Closed)
			{
				sqlCeConnection.Open();
			}
			sqlCeCommand.CommandText = SqlCommand;
			return Conversions.ToInteger(sqlCeCommand.ExecuteScalar());
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000E02C File Offset: 0x0000C22C
		internal void StoreSqlRow(string BaseName, string TableName, string[] Columns, object[] Values)
		{
			SqlCeConnection sqlCeConnection = new SqlCeConnection(string.Format("DATASOURCE={0}{1};Password={2};Persist Security Info=True", this.Path, BaseName, this.LocalPass));
			SqlCeCommand sqlCeCommand = new SqlCeCommand
			{
				Connection = sqlCeConnection
			};
			if (sqlCeConnection.State == ConnectionState.Closed)
			{
				sqlCeConnection.Open();
			}
			sqlCeCommand.CommandText = string.Format("SELECT COUNT (*) FROM {0} WHERE {1} = '{2}'", TableName, Columns[0], RuntimeHelpers.GetObjectValue(Values[0]));
			if (Operators.ConditionalCompareObjectEqual(sqlCeCommand.ExecuteScalar(), 0, false))
			{
				string text = string.Format("INSERT INTO {0} (", TableName);
				string text2 = " VALUES(";
				int num = 0;
				if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareString(Columns[0], "id", false) == 0, Operators.CompareObjectEqual(Values[0], "0", false))))
				{
					num = 1;
				}
				int num2 = num;
				int num3 = Columns.Count<string>() - 1;
				for (int i = num2; i <= num3; i++)
				{
					text += string.Format("{0},", Columns[i]);
					text2 += string.Format("@{0},", Columns[i]);
					sqlCeCommand.Parameters.AddWithValue(string.Format("@{0}", Columns[i]), RuntimeHelpers.GetObjectValue(Values[i]));
				}
				text = text.Substring(0, text.Length - 1) + ")" + text2.Substring(0, text2.Length - 1) + ")";
				sqlCeCommand.CommandText = text;
			}
			else
			{
				string text3 = string.Format("UPDATE {0} SET", TableName);
				int num4 = Columns.Count<string>() - 1;
				for (int j = 1; j <= num4; j++)
				{
					text3 += string.Format(" {0} = @{1},", Columns[j], Columns[j]);
					sqlCeCommand.Parameters.AddWithValue(string.Format("@{0}", Columns[j]), RuntimeHelpers.GetObjectValue(Values[j]));
				}
				text3 = text3.Substring(0, text3.Length - 1) + string.Format(" WHERE {0} = '{1}'", Columns[0], RuntimeHelpers.GetObjectValue(Values[0]));
				sqlCeCommand.CommandText = text3;
			}
			sqlCeCommand.ExecuteNonQuery();
			sqlCeConnection.Close();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000E23C File Offset: 0x0000C43C
		internal void StoreExtSqlRow(string BaseName, string TableName, string[] Columns, object[] Values)
		{
			SqlConnection sqlConnection = new SqlConnection(string.Format("Server={0};Database={1};User ID={2};Password={3};Trusted_Connection=False;", new object[]
			{
				this.Server,
				BaseName,
				this.ServerUser,
				this.ServerPass
			}));
			SqlCommand sqlCommand = new SqlCommand
			{
				Connection = sqlConnection
			};
			if (sqlConnection.State == ConnectionState.Closed)
			{
				sqlConnection.Open();
			}
			sqlCommand.CommandText = string.Format("SELECT COUNT (*) FROM {0} WHERE {1} = '{2}'", TableName, Columns[0], RuntimeHelpers.GetObjectValue(Values[0]));
			if (Operators.ConditionalCompareObjectEqual(sqlCommand.ExecuteScalar(), 0, false))
			{
				string text = string.Format("INSERT INTO {0} (", TableName);
				string text2 = " VALUES(";
				int num = 0;
				if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareString(Columns[0], "id", false) == 0, Operators.CompareObjectEqual(Values[0], 0, false))))
				{
					num = 1;
				}
				int num2 = num;
				int num3 = Columns.Count<string>() - 1;
				for (int i = num2; i <= num3; i++)
				{
					text += string.Format("{0},", Columns[i]);
					text2 += string.Format("@{0},", Columns[i]);
					sqlCommand.Parameters.AddWithValue(string.Format("@{0}", Columns[i]), RuntimeHelpers.GetObjectValue(Values[i]));
				}
				text = text.Substring(0, text.Length - 1) + ")" + text2.Substring(0, text2.Length - 1) + ")";
				sqlCommand.CommandText = text;
			}
			else
			{
				string text3 = string.Format("UPDATE {0} SET", TableName);
				int num4 = Columns.Count<string>() - 1;
				for (int j = 1; j <= num4; j++)
				{
					text3 += string.Format(" {0} = @{1},", Columns[j], Columns[j]);
					sqlCommand.Parameters.AddWithValue(string.Format("@{0}", Columns[j]), RuntimeHelpers.GetObjectValue(Values[j]));
				}
				text3 = text3.Substring(0, text3.Length - 1) + string.Format(" WHERE {0} = '{1}'", Columns[0], RuntimeHelpers.GetObjectValue(Values[0]));
				sqlCommand.CommandText = text3;
			}
			try
			{
				sqlCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
			}
			sqlCommand.Parameters.Clear();
			sqlConnection.Close();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000E494 File Offset: 0x0000C694
		public void SetPassword(string BaseName)
		{
			try
			{
				using (SqlCeEngine sqlCeEngine = new SqlCeEngine(string.Format("Data Source={0}{1};Password=C30D3E089FE311F5;Persist Security Info=True", this.Path, BaseName)))
				{
					sqlCeEngine.Compact(string.Format("Data Source={0}{1};Password={2};Persist Security Info=True", this.Path, BaseName, this.LocalPass));
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x040000D2 RID: 210
		private string LocalPass;

		// Token: 0x040000D3 RID: 211
		private string Path;

		// Token: 0x040000D4 RID: 212
		private string Server;

		// Token: 0x040000D5 RID: 213
		private string ServerUser;

		// Token: 0x040000D6 RID: 214
		private string ServerPass;
	}
}

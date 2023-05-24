using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4
{
	// Token: 0x02000015 RID: 21
	public class Analyzer
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000078AC File Offset: 0x00005AAC
		public Analyzer()
		{
			this.Table = new DataTable();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000078BF File Offset: 0x00005ABF
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000078C7 File Offset: 0x00005AC7
		public DataTable Table { get; set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x000078D0 File Offset: 0x00005AD0
		public Analyzer.TypeFormat GetTypeFormat(ref byte[] Binary)
		{
			Analyzer.TypeFormat result;
			if (Binary.Count<byte>() == 0)
			{
				result = Analyzer.TypeFormat.Unknown;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(new StreamReader(new MemoryStream(Binary)).ReadToEnd());
				stringBuilder.Replace("\r", "");
				string[] array = stringBuilder.ToString().Split(new char[]
				{
					'\n'
				});
				if (array[0].Contains("ASCII Trace IXXAT"))
				{
					this.Table = new DataTable();
					int num = 0;
					Hashtable hashtable = new Hashtable();
					this.Table.Columns.Add("Time", typeof(string));
					this.Table.Columns.Add("Ident", typeof(string));
					int num2 = array.Count<string>() - 2;
					for (int i = 7; i <= num2; i++)
					{
						string[] array2 = array[i].Replace("\"", "").Split(new char[]
						{
							';'
						});
						this.Table.Rows.Add(new object[0]);
						this.Table.AsEnumerable().ElementAtOrDefault(num)["Time"] = array2[0];
						this.Table.AsEnumerable().ElementAtOrDefault(num)["Ident"] = array2[1];
						if (!hashtable.ContainsKey(array2[1]))
						{
							hashtable.Add(array2[1], array2[1]);
							this.Table.Columns.Add(string.Format("ID_{0}", array2[1]), typeof(string));
						}
						this.Table.AsEnumerable().ElementAtOrDefault(num)[string.Format("ID_{0}", array2[1])] = array2[4];
						num++;
					}
					this.Table.TableName = Conversions.ToString(num);
					result = Analyzer.TypeFormat.Minimon;
				}
				else if (array[0].Contains("Tlam"))
				{
					this.Table = new DataTable();
					foreach (string columnName in array[0].Split(new char[]
					{
						';'
					}))
					{
						this.Table.Columns.Add(columnName, typeof(string));
					}
					int num3 = 0;
					int num4 = array.Count<string>() - 2;
					for (int k = 2; k <= num4; k++)
					{
						string[] array4 = array[k].Split(new char[]
						{
							';'
						});
						this.Table.Rows.Add(new object[0]);
						int num5 = array4.Count<string>() - 1;
						for (int l = 0; l <= num5; l++)
						{
							if (l == 0)
							{
								this.Table.AsEnumerable().ElementAtOrDefault(num3)[l] = Conversions.ToDouble(array4[l]) / 1000.0;
							}
							else
							{
								this.Table.AsEnumerable().ElementAtOrDefault(num3)[l] = array4[l];
							}
						}
						num3++;
					}
					result = Analyzer.TypeFormat.ULA;
				}
				else
				{
					result = Analyzer.TypeFormat.Unknown;
				}
			}
			return result;
		}

		// Token: 0x0200003D RID: 61
		public enum TypeFormat
		{
			// Token: 0x0400017B RID: 379
			Minimon,
			// Token: 0x0400017C RID: 380
			ULA,
			// Token: 0x0400017D RID: 381
			Unknown = 255
		}
	}
}

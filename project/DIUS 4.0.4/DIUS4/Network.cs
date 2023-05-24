using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using DIUS4.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4
{
	// Token: 0x0200001A RID: 26
	public class Network : IDisposable
	{
		// Token: 0x06000124 RID: 292 RVA: 0x0000C63D File Offset: 0x0000A83D
		public Network(ref Stack _Stack)
		{
			this.Port = 9006;
			this.S = _Stack;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000C658 File Offset: 0x0000A858
		public byte Connect()
		{
			bool flag = false;
			byte result;
			if (!MyProject.Computer.Network.IsAvailable)
			{
				result = 127;
			}
			else
			{
				this.Client = new TcpClient
				{
					NoDelay = true
				};
				try
				{
					if (MyProject.Computer.Network.Ping("brptuning.com"))
					{
						this.Client.Connect("brptuning.com", this.Port);
						flag = this.IsConnected;
					}
				}
				catch (Exception ex)
				{
				}
				if (!flag)
				{
					try
					{
						if (MyProject.Computer.Network.Ping("dius.brptuning.com"))
						{
							this.Client.Connect("dius.brptuning.com", this.Port);
						}
					}
					catch (Exception ex2)
					{
					}
				}
				if (!this.IsConnected)
				{
					result = 127;
				}
				else
				{
					this.Stream = this.Client.GetStream();
					this.r = new BinaryReader(this.Stream);
					this.w = new BinaryWriter(this.Stream);
					this.w.Write("DIUS4.0 Client for DIUS platform");
					this.w.Flush();
					result = this.r.ReadByte();
				}
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000C798 File Offset: 0x0000A998
		public byte Send()
		{
			byte b = 127;
			byte b2 = Conversions.ToByte(this.S.Pop());
			try
			{
				Queue queue = new Queue();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				int num = (int)(b2 - 1);
				for (int i = 0; i <= num; i++)
				{
					queue.Enqueue(RuntimeHelpers.GetObjectValue(this.S.Pop()));
				}
				if (this.IsConnected)
				{
					binaryFormatter.Serialize(this.Stream, queue);
					this.w.Flush();
					if (this.IsConnected)
					{
						b = this.r.ReadByte();
						if (b == 255)
						{
							string left = this.r.ReadString();
							if (Operators.CompareString(left, "String", false) != 0)
							{
								if (Operators.CompareString(left, "DataTable", false) != 0)
								{
									if (Operators.CompareString(left, "ByteArray", false) == 0)
									{
										this.S.Push((byte[])binaryFormatter.Deserialize(this.Stream));
									}
								}
								else
								{
									DataTable dataTable = (DataTable)binaryFormatter.Deserialize(this.Stream);
									dataTable.TableName = Conversions.ToString(dataTable.Rows.Count);
									this.S.Push(dataTable);
								}
							}
							else
							{
								this.S.Push(this.r.ReadString());
							}
							b = 0;
						}
						else if (b == 130)
						{
							this.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(ex.Message, MsgBoxStyle.OkOnly, null);
				b = 127;
			}
			return b;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000C934 File Offset: 0x0000AB34
		public void Close()
		{
			this.Client.Close();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000C944 File Offset: 0x0000AB44
		private bool IsConnected
		{
			get
			{
				bool result;
				try
				{
					if (this.Client != null && this.Client.Client != null && this.Client.Client.Connected)
					{
						if (this.Client.Client.Poll(0, SelectMode.SelectRead))
						{
							if (this.Client.Client.Receive(new byte[1], SocketFlags.Peek) == 0)
							{
								result = false;
							}
							else
							{
								result = true;
							}
						}
						else
						{
							result = true;
						}
					}
					else
					{
						result = false;
					}
				}
				catch (Exception ex)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue && disposing)
			{
				try
				{
					this.w.Close();
					this.r.Close();
					this.Stream.Close();
					this.Client.Close();
					this.w.Dispose();
					this.w = null;
					this.r.Dispose();
					this.r = null;
					this.Stream.Dispose();
					this.Stream = null;
					this.Client = null;
				}
				catch (Exception ex)
				{
				}
			}
			this.disposedValue = true;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x040000C6 RID: 198
		private NetworkStream Stream;

		// Token: 0x040000C7 RID: 199
		private TcpClient Client;

		// Token: 0x040000C8 RID: 200
		private BinaryWriter w;

		// Token: 0x040000C9 RID: 201
		private BinaryReader r;

		// Token: 0x040000CA RID: 202
		private Stack S;

		// Token: 0x040000CB RID: 203
		private int Port;

		// Token: 0x040000CC RID: 204
		private const string Message = "DIUS4.0 Client for DIUS platform";

		// Token: 0x040000CD RID: 205
		private bool disposedValue;

		// Token: 0x02000041 RID: 65
		private enum Err : byte
		{
			// Token: 0x04000194 RID: 404
			OK,
			// Token: 0x04000195 RID: 405
			NoInternet = 127,
			// Token: 0x04000196 RID: 406
			NewUser,
			// Token: 0x04000197 RID: 407
			RestoreUser,
			// Token: 0x04000198 RID: 408
			NoAccess,
			// Token: 0x04000199 RID: 409
			Incorrect,
			// Token: 0x0400019A RID: 410
			DataReturn = 255
		}
	}
}

using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DIUS4
{
	// Token: 0x02000016 RID: 22
	public class Decode
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00007C04 File Offset: 0x00005E04
		internal string ToASC(byte[] input, uint start, uint len)
		{
			string text = "";
			int num = (int)((ulong)(start + len) - 1UL);
			for (int i = (int)start; i <= num; i++)
			{
				if (input[i] > 31)
				{
					text += Conversions.ToString(Strings.Chr((int)input[i]));
				}
			}
			return text;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007C48 File Offset: 0x00005E48
		internal byte[] FromASC(string str)
		{
			byte[] array = new byte[str.Length - 1 + 1];
			uint num = (uint)(array.Length - 1);
			for (uint num2 = 0U; num2 <= num; num2 += 1U)
			{
				array[(int)num2] = (byte)Strings.Asc(str.Substring((int)num2, 1));
			}
			return array;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007C8C File Offset: 0x00005E8C
		internal byte[] FromASC(string str, uint Len)
		{
			byte[] array = new byte[(int)((ulong)Len - 1UL) + 1];
			uint num = (uint)((ulong)Len - 1UL);
			for (uint num2 = 0U; num2 <= num; num2 += 1U)
			{
				array[(int)num2] = (byte)Strings.Asc(str.Substring((int)num2, 1));
			}
			return array;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007CCC File Offset: 0x00005ECC
		internal string ToDate(byte[] input, uint start, uint Len)
		{
			string text = "";
			try
			{
				int num = (int)((ulong)(start + Len) - 1UL);
				for (int i = (int)start; i <= num; i++)
				{
					text += Conversions.ToString(Strings.Chr((int)((byte)((uint)input[i] >> 4) | 48)));
					text += Conversions.ToString(Strings.Chr((int)((input[i] & 15) | 48)));
					if ((long)i != (long)((ulong)(start + Len) - 1UL))
					{
						text += "/";
					}
				}
			}
			catch (Exception ex)
			{
			}
			return text;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007D60 File Offset: 0x00005F60
		internal string ByteToHEX8(byte[] input, uint Start, uint Len)
		{
			string result;
			if (input == null)
			{
				result = "";
			}
			else
			{
				string text = "";
				try
				{
					uint num = (uint)(input.Length - 1);
					for (uint num2 = 0U; num2 <= num; num2 += 1U)
					{
						if (((byte)((uint)input[(int)num2] >> 4) | 48) < 58)
						{
							text += Conversions.ToString(Strings.Chr((int)((byte)((uint)input[(int)num2] >> 4) | 48)));
						}
						else
						{
							text += Conversions.ToString(Strings.Chr((int)((byte)((uint)input[(int)num2] >> 4) + 55)));
						}
						if (((input[(int)num2] & 15) | 48) < 58)
						{
							text += Conversions.ToString(Strings.Chr((int)((input[(int)num2] & 15) | 48)));
						}
						else
						{
							text += Conversions.ToString(Strings.Chr((int)((input[(int)num2] & 15) + 55)));
						}
					}
					result = text.Substring((int)Start, (int)Len);
				}
				catch (Exception ex)
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007E50 File Offset: 0x00006050
		internal byte[] HEX8ToByte(string aHex)
		{
			byte[] result;
			if (string.IsNullOrEmpty(aHex))
			{
				result = new byte[0];
			}
			else
			{
				int length = aHex.Length;
				char[] array = aHex.ToCharArray();
				byte[] array2 = new byte[(length >> 1) - 1 + 1];
				int num = array2.Length - 1;
				for (int i = 0; i <= num; i++)
				{
					int num2 = i << 1;
					int num3 = (int)array[num2];
					int num4 = (int)array[num2 + 1];
					num3 -= ((num3 < 58) ? 48 : ((num3 < 97) ? 55 : 87));
					num4 -= ((num4 < 58) ? 48 : ((num4 < 97) ? 55 : 87));
					num3 <<= 4;
					array2[i] = (byte)(num3 + num4);
				}
				result = array2;
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007F00 File Offset: 0x00006100
		internal byte[] TextToByte2(string aHex)
		{
			byte[] result;
			if (string.IsNullOrEmpty(aHex))
			{
				result = new byte[0];
			}
			else
			{
				uint num = (uint)Math.Round((double)aHex.Length / 2.0 - 1.0);
				byte[] array = new byte[num + 1U];
				byte b = (byte)num;
				for (byte b2 = 0; b2 <= b; b2 += 1)
				{
					array[(int)b2] = Convert.ToByte(aHex.Substring((int)((byte)(b2 << 1)), 2), 16);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007F70 File Offset: 0x00006170
		internal byte[] TextToByte16s(string aHex)
		{
			byte[] result;
			if (string.IsNullOrEmpty(aHex))
			{
				result = new byte[0];
			}
			else
			{
				byte[] array = new byte[8];
				byte b = 0;
				do
				{
					array[(int)b] = Convert.ToByte(aHex.Substring((int)((byte)(b << 1)), 2), 16);
					b += 1;
				}
				while (b <= 7);
				result = array;
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007FB8 File Offset: 0x000061B8
		internal string IntToTime(byte[] input)
		{
			string result;
			try
			{
				uint num = (uint)input[0] * 16777216U + (uint)input[1] * 65536U + (uint)input[2] * 256U + (uint)input[3];
				uint num2 = (uint)(num / 60.0);
				result = string.Format("{0}:{1:00}", num2, (long)((ulong)num - (ulong)num2 * 60UL));
			}
			catch (Exception ex)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000803C File Offset: 0x0000623C
		internal string WordToTime(byte[] input)
		{
			string result;
			try
			{
				int num = (int)input[0] * 256 + (int)input[1];
				uint num2 = (uint)(num / 60.0);
				uint num3 = (uint)((ulong)num - (ulong)(Conversion.Int((long)((ulong)num2)) * 60L));
				result = string.Format("{0}:{1:00}", num2, num3);
			}
			catch (Exception ex)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000080B4 File Offset: 0x000062B4
		internal byte[] TimeToInt(string Time)
		{
			string[] array = Time.Split(new char[]
			{
				':'
			});
			uint num = (uint)Math.Round(Conversions.ToDouble(array[0]) * 60.0 + Conversions.ToDouble(array[1]));
			return new byte[]
			{
				(byte)((ulong)num & 255UL),
				(byte)((ulong)(num >> 8) & 255UL),
				(byte)((ulong)(num >> 16) & 255UL),
				(byte)((ulong)(num >> 24) & 255UL)
			};
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008138 File Offset: 0x00006338
		internal byte[] TimeToWord(string Hour, string Minute)
		{
			ushort num = (ushort)Math.Round(Conversions.ToDouble(Hour) * 60.0 + Conversions.ToDouble(Minute));
			return new byte[]
			{
				(byte)((ushort)((uint)num >> 8) & 255),
				(byte)(num & 255)
			};
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008184 File Offset: 0x00006384
		internal byte[] TimeToWord(string Time)
		{
			string[] array = Time.Split(new char[]
			{
				':'
			});
			ushort num = (ushort)Math.Round(Conversions.ToDouble(array[0]) * 60.0 + Conversions.ToDouble(array[1]));
			return new byte[]
			{
				(byte)((ushort)((uint)num >> 8) & 255),
				(byte)(num & 255)
			};
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000081E4 File Offset: 0x000063E4
		internal byte[] FromDate(string Dates)
		{
			return new byte[]
			{
				(byte)Math.Round(Conversion.Val(string.Format("&h{0}", Dates.Substring(0, 2)))),
				(byte)Math.Round(Conversion.Val(string.Format("&h{0}", Dates.Substring(3, 2)))),
				(byte)Math.Round(Conversion.Val(string.Format("&h{0}", Dates.Substring(6, 2))))
			};
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00008258 File Offset: 0x00006458
		internal bool DiusHex(string str)
		{
			uint num = (uint)(str.Length - 1);
			for (uint num2 = 0U; num2 <= num; num2 += 1U)
			{
				int num3 = Strings.Asc(str[(int)num2]);
				if (num3 < 48 | num3 > 70 | num3 == 64)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000082A4 File Offset: 0x000064A4
		internal byte[] IntToBytes(uint Int)
		{
			return new byte[]
			{
				(byte)(((ulong)Int & 18446744073692774400UL) >> 24),
				(byte)(((ulong)Int & 16711680UL) >> 16),
				(byte)(((ulong)Int & 65280UL) >> 8),
				(byte)((ulong)Int & 255UL)
			};
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000082F4 File Offset: 0x000064F4
		internal uint ByDiusoInt(byte[] Bytes)
		{
			int num = 0;
			uint num2;
			do
			{
				num2 = (num2 | (uint)Bytes[num]) << 8;
				num++;
			}
			while (num <= 2);
			num2 |= (uint)Bytes[3];
			return num2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000831A File Offset: 0x0000651A
		internal byte[] IntTo3Bytes(int Int)
		{
			return new byte[]
			{
				(byte)((Int & 16711680) >> 16),
				(byte)((Int & 65280) >> 8),
				(byte)(Int & 255)
			};
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008348 File Offset: 0x00006548
		internal byte[] IntTo2Bytes(int Int)
		{
			return new byte[]
			{
				(byte)((Int & 65280) >> 8),
				(byte)(Int & 255)
			};
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008368 File Offset: 0x00006568
		internal uint BRP_SeedKey(uint Ident, string Table, uint Key)
		{
			uint result;
			if ((ulong)Key == 0UL)
			{
				result = 0U;
			}
			else
			{
				string[] array = Table.Split(new char[]
				{
					','
				});
				int num = 16;
				byte b;
				do
				{
					if ((ushort)(1L << (num & 31) & (long)((ulong)Ident)) != 0)
					{
						b = (byte)(b << 1);
						if ((ushort)(1L << (num & 31) & (long)((ulong)Key)) != 0)
						{
							b |= 1;
						}
					}
					num += -1;
				}
				while (num >= 0);
				b &= 7;
				result = (uint)((ulong)((uint)((ulong)(~(ulong)Key) & 65535UL) * (uint)Conversions.ToByte(array[(int)b]) >> 6) & 65535UL);
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000083E4 File Offset: 0x000065E4
		public bool GetBit(long lValue, long lBitPos)
		{
			return (lValue & (long)Math.Round(Math.Pow(2.0, (double)lBitPos))) != 0L;
		}
	}
}

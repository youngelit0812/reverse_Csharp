using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DIUS4.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace DIUS4.Shield
{
	// Token: 0x0200001B RID: 27
	public class Protect
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000CA85 File Offset: 0x0000AC85
		public Protect(ref RegistryKey Reestre)
		{
			this.RegKey = Reestre;
			this.Key = "DIUS 4.0";
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public byte Mode { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000CAB1 File Offset: 0x0000ACB1
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000CAB9 File Offset: 0x0000ACB9
		public string Key { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000CACA File Offset: 0x0000ACCA
		public string SubKey { get; set; }

		// Token: 0x06000132 RID: 306 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public string OutKey()
		{
			string result;
			if (this.Key.Length == 0 | this.SubKey.Length == 0)
			{
				result = "";
			}
			else
			{
				Decode decode = new Decode();
				byte mode = this.Mode;
				if (mode != 0)
				{
					if (mode != 1)
					{
						result = "";
					}
					else
					{
						byte[] array = this.GenerateKey(this.Key, decode.HEX8ToByte(this.SubKey));
						result = Encoding.ASCII.GetString(array, 0, array.Length);
					}
				}
				else
				{
					byte[] array2 = this.GenerateKey(this.Key, Encoding.ASCII.GetBytes(this.SubKey));
					result = decode.ByteToHEX8(array2, 0U, (uint)(array2.Length * 2));
				}
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000CB80 File Offset: 0x0000AD80
		private byte[] GenerateKey(string MyText, byte[] MyKey)
		{
			uint[] array = new uint[]
			{
				2097152U,
				69206018U,
				67110914U,
				0U,
				2048U,
				67110914U,
				2099202U,
				69208064U,
				69208066U,
				2097152U,
				0U,
				67108866U,
				2U,
				67108864U,
				69206018U,
				2050U,
				67110912U,
				2099202U,
				2097154U,
				67110912U,
				67108866U,
				69206016U,
				69208064U,
				2097154U,
				69206016U,
				2048U,
				2050U,
				69208066U,
				2099200U,
				2U,
				67108864U,
				2099200U,
				67108864U,
				2099200U,
				2097152U,
				67110914U,
				67110914U,
				69206018U,
				69206018U,
				2U,
				2097154U,
				67108864U,
				67110912U,
				2097152U,
				69208064U,
				2050U,
				2099202U,
				69208064U,
				2050U,
				67108866U,
				69208066U,
				69206016U,
				2099200U,
				0U,
				2U,
				69208066U,
				0U,
				2099202U,
				69206016U,
				2048U,
				67108866U,
				67110912U,
				2048U,
				2097154U
			};
			uint[] array2 = new uint[]
			{
				256U,
				34078976U,
				34078720U,
				1107296512U,
				524288U,
				256U,
				1073741824U,
				34078720U,
				1074266368U,
				524288U,
				33554688U,
				1074266368U,
				1107296512U,
				1107820544U,
				524544U,
				1073741824U,
				33554432U,
				1074266112U,
				1074266112U,
				0U,
				1073742080U,
				1107820800U,
				1107820800U,
				33554688U,
				1107820544U,
				1073742080U,
				0U,
				1107296256U,
				34078976U,
				33554432U,
				1107296256U,
				524544U,
				524288U,
				1107296512U,
				256U,
				33554432U,
				1073741824U,
				34078720U,
				1107296512U,
				1074266368U,
				33554688U,
				1073741824U,
				1107820544U,
				34078976U,
				1074266368U,
				256U,
				33554432U,
				1107820544U,
				1107820800U,
				524544U,
				1107296256U,
				1107820800U,
				34078720U,
				0U,
				1074266112U,
				1107296256U,
				524544U,
				33554688U,
				1073742080U,
				524288U,
				0U,
				1074266112U,
				34078976U,
				1073742080U
			};
			uint[] array3 = new uint[]
			{
				520U,
				134349312U,
				0U,
				134348808U,
				134218240U,
				0U,
				131592U,
				134218240U,
				131080U,
				134217736U,
				134217736U,
				131072U,
				134349320U,
				131080U,
				134348800U,
				520U,
				134217728U,
				8U,
				134349312U,
				512U,
				131584U,
				134348800U,
				134348808U,
				131592U,
				134218248U,
				131584U,
				131072U,
				134218248U,
				8U,
				134349320U,
				512U,
				134217728U,
				134349312U,
				134217728U,
				131080U,
				520U,
				131072U,
				134349312U,
				134218240U,
				0U,
				512U,
				131080U,
				134349320U,
				134218240U,
				134217736U,
				512U,
				0U,
				134348808U,
				134218248U,
				131072U,
				134217728U,
				134349320U,
				8U,
				131592U,
				131584U,
				134217736U,
				134348800U,
				134218248U,
				520U,
				134348800U,
				131592U,
				8U,
				134348808U,
				131584U
			};
			uint[] array4 = new uint[]
			{
				16843776U,
				0U,
				65536U,
				16843780U,
				16842756U,
				66564U,
				4U,
				65536U,
				1024U,
				16843776U,
				16843780U,
				1024U,
				16778244U,
				16842756U,
				16777216U,
				4U,
				1028U,
				16778240U,
				16778240U,
				66560U,
				66560U,
				16842752U,
				16842752U,
				16778244U,
				65540U,
				16777220U,
				16777220U,
				65540U,
				0U,
				1028U,
				66564U,
				16777216U,
				65536U,
				16843780U,
				4U,
				16842752U,
				16843776U,
				16777216U,
				16777216U,
				1024U,
				16842756U,
				65536U,
				66560U,
				16777220U,
				1024U,
				4U,
				16778244U,
				66564U,
				16843780U,
				65540U,
				16842752U,
				16778244U,
				16777220U,
				1028U,
				66564U,
				16843776U,
				1028U,
				16778240U,
				16778240U,
				0U,
				65540U,
				66560U,
				0U,
				16842756U
			};
			uint[] array5 = new uint[]
			{
				268439616U,
				4096U,
				262144U,
				268701760U,
				268435456U,
				268439616U,
				64U,
				268435456U,
				262208U,
				268697600U,
				268701760U,
				266240U,
				268701696U,
				266304U,
				4096U,
				64U,
				268697600U,
				268435520U,
				268439552U,
				4160U,
				266240U,
				262208U,
				268697664U,
				268701696U,
				4160U,
				0U,
				0U,
				268697664U,
				268435520U,
				268439552U,
				266304U,
				262144U,
				266304U,
				262144U,
				268701696U,
				4096U,
				64U,
				268697664U,
				4096U,
				266304U,
				268439552U,
				64U,
				268435520U,
				268697600U,
				268697664U,
				268435456U,
				262144U,
				268439616U,
				0U,
				268701760U,
				262208U,
				268435520U,
				268697600U,
				268439552U,
				268439616U,
				0U,
				268701760U,
				266240U,
				266240U,
				4160U,
				4160U,
				262208U,
				268435456U,
				268701696U
			};
			uint[] array6 = new uint[]
			{
				536870928U,
				541065216U,
				16384U,
				541081616U,
				541065216U,
				16U,
				541081616U,
				4194304U,
				536887296U,
				4210704U,
				4194304U,
				536870928U,
				4194320U,
				536887296U,
				536870912U,
				16400U,
				0U,
				4194320U,
				536887312U,
				16384U,
				4210688U,
				536887312U,
				16U,
				541065232U,
				541065232U,
				0U,
				4210704U,
				541081600U,
				16400U,
				4210688U,
				541081600U,
				536870912U,
				536887296U,
				16U,
				541065232U,
				4210688U,
				541081616U,
				4194304U,
				16400U,
				536870928U,
				4194304U,
				536887296U,
				536870912U,
				16400U,
				536870928U,
				541081616U,
				4210688U,
				541065216U,
				4210704U,
				541081600U,
				0U,
				541065232U,
				16U,
				16384U,
				541065216U,
				4210704U,
				16384U,
				4194320U,
				536887312U,
				0U,
				541081600U,
				536870912U,
				4194320U,
				536887312U
			};
			uint[] array7 = new uint[]
			{
				8396801U,
				8321U,
				8321U,
				128U,
				8396928U,
				8388737U,
				8388609U,
				8193U,
				0U,
				8396800U,
				8396800U,
				8396929U,
				129U,
				0U,
				8388736U,
				8388609U,
				1U,
				8192U,
				8388608U,
				8396801U,
				128U,
				8388608U,
				8193U,
				8320U,
				8388737U,
				1U,
				8320U,
				8388736U,
				8192U,
				8396928U,
				8396929U,
				129U,
				8388736U,
				8388609U,
				8396800U,
				8396929U,
				129U,
				0U,
				0U,
				8396800U,
				8320U,
				8388736U,
				8388737U,
				1U,
				8396801U,
				8321U,
				8321U,
				128U,
				8396929U,
				129U,
				1U,
				8192U,
				8388609U,
				8193U,
				8396928U,
				8388737U,
				8193U,
				8320U,
				8388608U,
				8396801U,
				128U,
				8388608U,
				8192U,
				8396928U
			};
			uint[] array8 = new uint[]
			{
				2148565024U,
				2147516416U,
				32768U,
				1081376U,
				1048576U,
				32U,
				2148532256U,
				2147516448U,
				2147483680U,
				2148565024U,
				2148564992U,
				2147483648U,
				2147516416U,
				1048576U,
				32U,
				2148532256U,
				1081344U,
				1048608U,
				2147516448U,
				0U,
				2147483648U,
				32768U,
				1081376U,
				2148532224U,
				1048608U,
				2147483680U,
				0U,
				1081344U,
				32800U,
				2148564992U,
				2148532224U,
				32800U,
				0U,
				1081376U,
				2148532256U,
				1048576U,
				2147516448U,
				2148532224U,
				2148564992U,
				32768U,
				2148532224U,
				2147516416U,
				32U,
				2148565024U,
				1081376U,
				32U,
				32768U,
				2147483648U,
				32800U,
				2148564992U,
				1048576U,
				2147483680U,
				1048608U,
				2147516448U,
				2147483680U,
				1048608U,
				1081344U,
				0U,
				2147516416U,
				32800U,
				2147483648U,
				2148532256U,
				2148565024U,
				1081344U
			};
			int[] array9 = new int[32];
			array9 = this.GeneratePassKey(MyText, this.Mode);
			uint num = (uint)((int)MyKey[0] << 24 | (int)MyKey[1] << 16 | (int)MyKey[2] << 8 | (int)MyKey[3]);
			uint num2 = (uint)((int)MyKey[4] << 24 | (int)MyKey[5] << 16 | (int)MyKey[6] << 8 | (int)MyKey[7]);
			uint num3 = (uint)((ulong)(num >> 4 ^ num2) & 252645135UL);
			num2 ^= num3;
			num ^= num3 << 4;
			num3 = (uint)((ulong)(num >> 16 ^ num2) & 65535UL);
			num2 ^= num3;
			num ^= num3 << 16;
			num3 = (uint)((ulong)(num2 >> 2 ^ num) & 858993459UL);
			num ^= num3;
			num2 ^= num3 << 2;
			num3 = (uint)((ulong)(num2 >> 8 ^ num) & 16711935UL);
			num ^= num3;
			num2 ^= num3 << 8;
			num2 = (uint)(((ulong)((uint)((ulong)num2 * 2UL << 32 >> 32)) & ulong.MaxValue) | (ulong)(num2 >> 31));
			num3 = (uint)((ulong)(num ^ num2) & 18446744072277895850UL);
			num ^= num3;
			num2 ^= num3;
			ulong num4 = (ulong)((uint)((ulong)num * 2UL << 32 >> 32));
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[0]));
			num = (uint)((num4 & ulong.MaxValue) | (ulong)(num >> 31));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[1] ^ (long)((ulong)num2));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[2]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num ^ (ulong)((long)array9[3]));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[4]));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num2 ^ (ulong)((long)array9[5]));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[6]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[7] ^ (long)((ulong)num));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[8]));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num2 ^ (ulong)((long)array9[9]));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[10]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[11] ^ (long)((ulong)num));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[12]));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num2 ^ (ulong)((long)array9[13]));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[14]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[15] ^ (long)((ulong)num));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[16]));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num2 ^ (ulong)((long)array9[17]));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[18]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[19] ^ (long)((ulong)num));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[20]));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num2 ^ (ulong)((long)array9[21]));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[22]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[23] ^ (long)((ulong)num));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[24]));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num2 ^ (ulong)((long)array9[25]));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[26]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[27] ^ (long)((ulong)num));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num2 >> 4 | num2 << 28) ^ (ulong)((long)array9[28]));
			num = (num ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)num2 ^ (ulong)((long)array9[29]));
			num = (num ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((ulong)(num >> 4 | num << 28) ^ (ulong)((long)array9[30]));
			num2 = (num2 ^ array[(int)((ulong)num3 & 63UL)] ^ array2[(int)((ulong)(num3 >> 8) & 63UL)] ^ array3[(int)((ulong)(num3 >> 16) & 63UL)] ^ array4[(int)((ulong)(num3 >> 24) & 63UL)]);
			num3 = (uint)((long)array9[31] ^ (long)((ulong)num));
			num2 = (num2 ^ array5[(int)((ulong)num3 & 63UL)] ^ array6[(int)((ulong)(num3 >> 8) & 63UL)] ^ array7[(int)((ulong)(num3 >> 16) & 63UL)] ^ array8[(int)((ulong)(num3 >> 24) & 63UL)]);
			num2 = (num2 << 31 | num2 >> 1);
			num3 = (uint)((ulong)(num ^ num2) & 18446744072277895850UL);
			num ^= num3;
			num2 ^= num3;
			num = (num >> 1 | num << 31);
			num3 = (uint)((ulong)(num >> 8 ^ num2) & 16711935UL);
			num2 ^= num3;
			num ^= num3 << 8;
			num3 = (uint)((ulong)(num >> 2 ^ num2) & 858993459UL);
			num2 ^= num3;
			num ^= num3 << 2;
			num3 = (uint)((ulong)(num2 >> 16 ^ num) & 65535UL);
			num ^= num3;
			num2 ^= num3 << 16;
			num3 = (uint)((ulong)(num2 >> 4 ^ num) & 252645135UL);
			num ^= num3;
			num2 ^= num3 << 4;
			return new byte[]
			{
				(byte)(num2 >> 24),
				(byte)((ulong)(num2 >> 16) & 255UL),
				(byte)((ulong)(num2 >> 8) & 255UL),
				(byte)((ulong)num2 & 255UL),
				(byte)(num >> 24),
				(byte)((ulong)(num >> 16) & 255UL),
				(byte)((ulong)(num >> 8) & 255UL),
				(byte)((ulong)num & 255UL)
			};
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		private int[] GeneratePassKey(string Password, byte Flag)
		{
			byte[] array = new byte[]
			{
				57,
				49,
				41,
				33,
				25,
				17,
				9,
				1,
				58,
				50,
				42,
				34,
				26,
				18,
				10,
				2,
				59,
				51,
				43,
				35,
				27,
				19,
				11,
				3,
				60,
				52,
				44,
				36,
				63,
				55,
				47,
				39,
				31,
				23,
				15,
				7,
				62,
				54,
				46,
				38,
				30,
				22,
				14,
				6,
				61,
				53,
				45,
				37,
				29,
				21,
				13,
				5,
				28,
				20,
				12,
				4
			};
			byte[] array2 = new byte[]
			{
				128,
				0,
				64,
				0,
				32,
				0,
				16,
				0,
				8,
				0,
				4,
				0,
				2,
				0,
				1,
				0
			};
			byte[] array3 = new byte[]
			{
				1,
				2,
				4,
				6,
				8,
				10,
				12,
				14,
				15,
				17,
				19,
				21,
				23,
				25,
				27,
				28
			};
			byte[] array4 = new byte[]
			{
				14,
				17,
				11,
				24,
				1,
				5,
				3,
				28,
				15,
				6,
				21,
				10,
				23,
				19,
				12,
				4,
				26,
				8,
				16,
				7,
				27,
				20,
				13,
				2,
				41,
				52,
				31,
				37,
				47,
				55,
				30,
				40,
				51,
				45,
				33,
				48,
				44,
				49,
				39,
				56,
				34,
				53,
				46,
				42,
				50,
				36,
				29,
				32
			};
			byte[] array5 = new byte[56];
			byte[] array6 = new byte[57];
			byte[] array7 = new byte[8];
			int[] array8 = new int[32];
			char[] array9 = Password.ToCharArray();
			byte[] array10 = new byte[Password.Length - 1 + 1];
			byte b = (byte)(Password.Length - 1);
			for (byte b2 = 0; b2 <= b; b2 += 1)
			{
				array10[(int)b2] = (byte)Strings.Asc(array9[(int)b2]);
				array10[(int)b2] = (byte)(array10[(int)b2] << 1);
			}
			byte b3 = 0;
			do
			{
				byte b4 = array[(int)b3];
				b4 -= 1;
				byte b5 = b4;
				b5 &= 7;
				byte b6 = 1;
				b4 = (byte)((uint)b4 >> 3);
				if ((array10[(int)b4] & array2[(int)(b5 * 2)]) == 0)
				{
					b6 -= 1;
				}
				array5[(int)b3] = b6;
				b3 += 1;
			}
			while (b3 <= 55);
			byte b7 = 0;
			do
			{
				byte b8 = 0;
				do
				{
					array7[(int)b8] = 0;
					b8 += 1;
				}
				while (b8 <= 7);
				byte b9 = 0;
				do
				{
					byte b10;
					if (Flag == 0)
					{
						b10 = b7;
					}
					else
					{
						b10 = 15 - b7;
					}
					byte b11 = array3[(int)b10] + b9;
					b10 = b11;
					byte b6 = 28;
					if (b9 >= 28)
					{
						b6 += 28;
					}
					if (b6 <= b10)
					{
						b10 = b11 - 28;
					}
					else
					{
						b10 = b11;
					}
					array6[(int)(b9 + 1)] = array5[(int)b10];
					b9 += 1;
				}
				while (b9 <= 55);
				byte b12 = 0;
				do
				{
					byte b10 = array4[(int)b12];
					if (array6[(int)b10] != 0)
					{
						b10 = b12 % 6;
						byte b6 = array2[(int)(b10 * 2)];
						b6 = (byte)((uint)b6 >> 2);
						array7[(int)Conversion.Int((double)b12 / 6.0)] = (array7[(int)Conversion.Int((double)b12 / 6.0)] | b6);
					}
					b12 += 1;
				}
				while (b12 <= 47);
				int num = (int)array7[0] << 24 | (int)array7[2] << 16 | (int)array7[4] << 8 | (int)array7[6];
				array8[(int)(b7 * 2)] = num;
				num = ((int)array7[1] << 24 | (int)array7[3] << 16 | (int)array7[5] << 8 | (int)array7[7]);
				array8[(int)(b7 * 2 + 1)] = num;
				b7 += 1;
			}
			while (b7 <= 15);
			return array8;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000DA38 File Offset: 0x0000BC38
		public string FileTimeHash(ref string FileName)
		{
			string result;
			try
			{
				FileInfo fileInfo = new FileInfo(string.Format("{0}{1}", MyProject.Application.Info.DirectoryPath, FileName));
				this.Mode = 0;
				this.SubKey = fileInfo.LastWriteTime.ToString("HH:mm:ss");
				result = this.OutKey();
			}
			catch (Exception ex)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public void StoreTime()
		{
			this.Mode = 0;
			this.SubKey = DateTime.Now.ToString("dd.MM.yy");
			this.RegKey.SetValue("Update", this.OutKey());
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public bool VerifyTime()
		{
			this.Mode = 1;
			this.SubKey = Conversions.ToString(this.RegKey.GetValue("Update", "50BBBE448AD7CB7A"));
			DateTime dateTime = DateTime.ParseExact(this.OutKey(), "dd'.'MM'.'yy", null);
			return DateTime.Compare(DateTime.Now, dateTime.AddDays(14.0)) <= 0;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public void StoreFileHash(ref string Filename)
		{
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create();
			FileStream fileStream = new FileStream(string.Format("{0}{1}", MyProject.Application.Info.DirectoryPath, Filename), FileMode.Open);
			byte[] value = hashAlgorithm.ComputeHash(fileStream);
			fileStream.Close();
			this.RegKey.SetValue("Hash", BitConverter.ToString(value).Replace("-", ""));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public string GetCompInfo()
		{
			string text = string.Empty;
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher
			{
				Query = new ObjectQuery("Select * From Win32_processor")
			};
			try
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					try
					{
						text += managementObject["ProcessorID"].ToString();
					}
					catch (Exception ex)
					{
					}
				}
			}
			finally
			{
				ManagementObjectCollection.ManagementObjectEnumerator enumerator;
				if (enumerator != null)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			text += Dns.GetHostName();
			managementObjectSearcher.Query = new ObjectQuery("SELECT * FROM Win32_BaseBoard");
			try
			{
				foreach (ManagementBaseObject managementBaseObject2 in managementObjectSearcher.Get())
				{
					ManagementObject managementObject2 = (ManagementObject)managementBaseObject2;
					try
					{
						text += managementObject2["SerialNumber"].ToString();
					}
					catch (Exception ex2)
					{
					}
				}
			}
			finally
			{
				ManagementObjectCollection.ManagementObjectEnumerator enumerator2;
				if (enumerator2 != null)
				{
					((IDisposable)enumerator2).Dispose();
				}
			}
			return text;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public void Update()
		{
			string executablePath = Application.ExecutablePath;
			new Process
			{
				StartInfo = 
				{
					WindowStyle = ProcessWindowStyle.Hidden,
					FileName = "cmd",
					Arguments = "/C ping -n 2 127.0.0.1&& Move /Y \"DIUS 4.0.4.tmp\" \"DIUS 4.0.4.exe\"&&Start \"DIUS 4.0.4.exe\" \"DIUS 4.0.4.exe\""
				}
			}.Start();
			Application.Exit();
		}

		// Token: 0x040000CE RID: 206
		private RegistryKey RegKey;
	}
}

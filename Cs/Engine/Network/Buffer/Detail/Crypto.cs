using System;
using Cs.Core.Util;

namespace Cs.Engine.Network.Buffer.Detail
{
	// Token: 0x020010B1 RID: 4273
	public static class Crypto
	{
		// Token: 0x06009C6F RID: 40047 RVA: 0x00335D38 File Offset: 0x00333F38
		public static void Encrypt(byte[] buffer, int size)
		{
			if (buffer == null)
			{
				return;
			}
			int num = 0;
			Crypto.Encrypt(buffer, size, ref num, Crypto.MaskList);
		}

		// Token: 0x06009C70 RID: 40048 RVA: 0x00335D5C File Offset: 0x00333F5C
		public static void Encrypt(byte[] buffer, int size, ulong[] maskList)
		{
			if (buffer == null)
			{
				return;
			}
			int num = 0;
			Crypto.Encrypt(buffer, size, ref num, maskList);
		}

		// Token: 0x06009C71 RID: 40049 RVA: 0x00335D79 File Offset: 0x00333F79
		public static void Encrypt(byte[] buffer, int size, ref int maskIndex)
		{
			if (buffer == null)
			{
				return;
			}
			Crypto.Encrypt(buffer, size, ref maskIndex, Crypto.MaskList);
		}

		// Token: 0x06009C72 RID: 40050 RVA: 0x00335D8C File Offset: 0x00333F8C
		public static void Encrypt(byte[] buffer, int size, ref int maskIndex, ulong[] maskList)
		{
			if (buffer == null)
			{
				return;
			}
			int i = 0;
			while (i < size)
			{
				ulong num = maskList[maskIndex];
				int num2 = size - i;
				if (num2 >= 8)
				{
					(buffer.DirectToUint64(i) ^ num).DirectWriteTo(buffer, i);
					i += 8;
				}
				else
				{
					for (int j = i; j < size; j++)
					{
						int num3 = j - i;
						int num4 = j;
						buffer[num4] ^= (byte)(num & 255UL << num3 >> num3);
					}
					i += num2;
				}
				maskIndex = (maskIndex + 1) % maskList.Length;
			}
		}

		// Token: 0x04009065 RID: 36965
		private static readonly ulong[] MaskList = new ulong[]
		{
			14170986657190717782UL,
			15546886188969944187UL,
			15913139373130964729UL,
			3486779174683840252UL
		};
	}
}

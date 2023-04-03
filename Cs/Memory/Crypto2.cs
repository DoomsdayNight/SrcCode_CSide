using System;
using Cs.Core.Util;

namespace Cs.Memory
{
	// Token: 0x020010A3 RID: 4259
	public static class Crypto2
	{
		// Token: 0x06009C03 RID: 39939 RVA: 0x0033411F File Offset: 0x0033231F
		public static void Encrypt(byte[] buffer, int size)
		{
			Crypto2.Encrypt(buffer, 0, size);
		}

		// Token: 0x06009C04 RID: 39940 RVA: 0x00334129 File Offset: 0x00332329
		public static void Decrypt(byte[] buffer, int size)
		{
			Crypto2.Decrypt(buffer, 0, size);
		}

		// Token: 0x06009C05 RID: 39941 RVA: 0x00334134 File Offset: 0x00332334
		public static void Encrypt(byte[] buffer, int offset, int size)
		{
			int num = 0;
			int i = 0;
			while (i < size)
			{
				ulong num2 = Crypto2.MaskList[num];
				int num3 = offset + i;
				int num4 = size - i;
				if (num4 >= 8)
				{
					ulong num5 = buffer.DirectToUint64(num3) ^ num2;
					ulong num6 = num5 & 6148914691236517205UL;
					ulong num7 = (num5 & 12297829382473034410UL) >> 1 | num6 << 1;
					num7 = ((num7 & 18446744069414584320UL) | (num7 & (ulong)-16777216) >> 8 | (num7 & 16711680UL) << 8 | (num7 & 65280UL) >> 8 | (num7 & 255UL) << 8);
					num7.DirectWriteTo(buffer, num3);
					i += 8;
				}
				else
				{
					for (int j = 0; j < num4; j++)
					{
						int num8 = num3 + j;
						buffer[num8] ^= (byte)(num2 & 255UL << j >> j);
					}
					i += num4;
				}
				num = (num + 1) % Crypto2.MaskList.Length;
			}
		}

		// Token: 0x06009C06 RID: 39942 RVA: 0x00334224 File Offset: 0x00332424
		public static void Decrypt(byte[] buffer, int offset, int size)
		{
			int num = 0;
			int i = 0;
			while (i < size)
			{
				ulong num2 = Crypto2.MaskList[num];
				int num3 = offset + i;
				int num4 = size - i;
				if (num4 >= 8)
				{
					ulong num5 = buffer.DirectToUint64(num3);
					num5 = ((num5 & 18446744069414584320UL) | (num5 & (ulong)-16777216) >> 8 | (num5 & 16711680UL) << 8 | (num5 & 65280UL) >> 8 | (num5 & 255UL) << 8);
					ulong num6 = num5 & 6148914691236517205UL;
					num5 = ((num5 & 12297829382473034410UL) >> 1 | num6 << 1);
					(num5 ^ num2).DirectWriteTo(buffer, num3);
					i += 8;
				}
				else
				{
					for (int j = 0; j < num4; j++)
					{
						int num7 = num3 + j;
						buffer[num7] ^= (byte)(num2 & 255UL << j >> j);
					}
					i += num4;
				}
				num = (num + 1) % Crypto2.MaskList.Length;
			}
		}

		// Token: 0x04009038 RID: 36920
		private const ulong OddMask = 6148914691236517205UL;

		// Token: 0x04009039 RID: 36921
		private const ulong EvenMask = 12297829382473034410UL;

		// Token: 0x0400903A RID: 36922
		private static readonly ulong[] MaskList = new ulong[]
		{
			14003937370121879411UL,
			295159725236528685UL,
			14656252856989855980UL,
			3126201044280739051UL,
			6176412274767465921UL,
			8501111619623644353UL,
			1001882303165547266UL,
			889784367385610816UL,
			8403001398375820177UL,
			15646421979254498160UL,
			15540104736269140030UL,
			4473111575030559303UL,
			16641115610173278858UL,
			7005653296469604124UL,
			7641466651897675454UL,
			18242667629599333687UL
		};
	}
}

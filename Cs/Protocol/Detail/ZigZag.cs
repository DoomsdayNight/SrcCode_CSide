using System;

namespace Cs.Protocol.Detail
{
	// Token: 0x020010C8 RID: 4296
	internal static class ZigZag
	{
		// Token: 0x06009DE4 RID: 40420 RVA: 0x00339DBC File Offset: 0x00337FBC
		internal static uint Encode32(int n)
		{
			return (uint)(n << 1 ^ n >> 31);
		}

		// Token: 0x06009DE5 RID: 40421 RVA: 0x00339DC6 File Offset: 0x00337FC6
		internal static ulong Encode64(long n)
		{
			return (ulong)(n << 1 ^ n >> 63);
		}

		// Token: 0x06009DE6 RID: 40422 RVA: 0x00339DD0 File Offset: 0x00337FD0
		internal static int Decode32(uint n)
		{
			return (int)(n >> 1 ^ -(int)(n & 1U));
		}

		// Token: 0x06009DE7 RID: 40423 RVA: 0x00339DDA File Offset: 0x00337FDA
		internal static long Decode64(ulong n)
		{
			return (long)(n >> 1 ^ -(long)(n & 1UL));
		}
	}
}

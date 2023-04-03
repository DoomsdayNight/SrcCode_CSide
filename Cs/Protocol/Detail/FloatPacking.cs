using System;

namespace Cs.Protocol.Detail
{
	// Token: 0x020010C7 RID: 4295
	internal static class FloatPacking
	{
		// Token: 0x06009DE2 RID: 40418 RVA: 0x00339DA8 File Offset: 0x00337FA8
		public static uint FloatToLow(this float data)
		{
			return (uint)(data * 100f);
		}

		// Token: 0x06009DE3 RID: 40419 RVA: 0x00339DB2 File Offset: 0x00337FB2
		public static float LowToFloat(this uint data)
		{
			return (float)data * 0.01f;
		}
	}
}

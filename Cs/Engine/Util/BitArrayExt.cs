using System;
using System.Collections;

namespace Cs.Engine.Util
{
	// Token: 0x020010A5 RID: 4261
	public static class BitArrayExt
	{
		// Token: 0x06009C0C RID: 39948 RVA: 0x003343AC File Offset: 0x003325AC
		public static byte[] ToByteArray(this BitArray data)
		{
			byte[] array = new byte[data.Length / 8];
			data.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06009C0D RID: 39949 RVA: 0x003343D0 File Offset: 0x003325D0
		public static int GetByteCount(this BitArray data)
		{
			return data.Length / 8;
		}
	}
}

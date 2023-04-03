using System;

namespace NKM
{
	// Token: 0x0200045D RID: 1117
	public class PerThreadRandom
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0008FB70 File Offset: 0x0008DD70
		public static Random Instance
		{
			get
			{
				if (PerThreadRandom.random_ == null)
				{
					PerThreadRandom.random_ = new Random();
				}
				return PerThreadRandom.random_;
			}
		}

		// Token: 0x04001EEC RID: 7916
		[ThreadStatic]
		private static Random random_;
	}
}

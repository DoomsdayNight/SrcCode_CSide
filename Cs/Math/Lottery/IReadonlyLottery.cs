using System;

namespace Cs.Math.Lottery
{
	// Token: 0x020010CF RID: 4303
	public interface IReadonlyLottery<T>
	{
		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06009DFB RID: 40443
		int TotalRatio { get; }

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06009DFC RID: 40444
		int Count { get; }

		// Token: 0x1700172B RID: 5931
		T this[int index]
		{
			get;
		}

		// Token: 0x06009DFE RID: 40446
		T Decide();
	}
}

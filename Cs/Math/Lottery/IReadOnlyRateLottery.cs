using System;

namespace Cs.Math.Lottery
{
	// Token: 0x020010CB RID: 4299
	public interface IReadOnlyRateLottery<T>
	{
		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06009DF3 RID: 40435
		int TotalRate { get; }

		// Token: 0x06009DF4 RID: 40436
		T Decide();

		// Token: 0x06009DF5 RID: 40437
		bool Decide(out T result);

		// Token: 0x06009DF6 RID: 40438
		bool HasValue(T value);

		// Token: 0x06009DF7 RID: 40439
		bool TryGetRatePercent(T value, out float ratePercent);
	}
}

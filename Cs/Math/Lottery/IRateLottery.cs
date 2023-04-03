using System;

namespace Cs.Math.Lottery
{
	// Token: 0x020010CC RID: 4300
	public interface IRateLottery<T> : IReadOnlyRateLottery<T>
	{
		// Token: 0x06009DF8 RID: 40440
		void AddCase(int rate, T value);
	}
}

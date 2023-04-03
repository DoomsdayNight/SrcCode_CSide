using System;

namespace Cs.Math.Lottery
{
	// Token: 0x020010CE RID: 4302
	public interface IRatioLottery<T> : IReadonlyLottery<T>
	{
		// Token: 0x06009DFA RID: 40442
		void AddCase(int ratio, T value);
	}
}

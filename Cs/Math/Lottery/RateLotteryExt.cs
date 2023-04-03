using System;
using System.Runtime.CompilerServices;

namespace Cs.Math.Lottery
{
	// Token: 0x020010CD RID: 4301
	public static class RateLotteryExt
	{
		// Token: 0x06009DF9 RID: 40441 RVA: 0x00339EC4 File Offset: 0x003380C4
		public static void AddCase<T>(this IRateLottery<T> self, [TupleElementNames(new string[]
		{
			"rate",
			"value"
		})] params ValueTuple<int, T>[] cases)
		{
			foreach (ValueTuple<int, T> valueTuple in cases)
			{
				int item = valueTuple.Item1;
				T item2 = valueTuple.Item2;
				self.AddCase(item, item2);
			}
		}
	}
}

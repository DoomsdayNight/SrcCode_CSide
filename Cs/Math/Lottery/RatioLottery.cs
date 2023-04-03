using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cs.Math.Lottery
{
	// Token: 0x020010D1 RID: 4305
	public sealed class RatioLottery<T> : IRatioLottery<T>, IReadonlyLottery<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06009E0D RID: 40461 RVA: 0x0033A1F1 File Offset: 0x003383F1
		public int TotalRatio
		{
			get
			{
				return this.totalRatio;
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06009E0E RID: 40462 RVA: 0x0033A1F9 File Offset: 0x003383F9
		public int Count
		{
			get
			{
				return this.cases.Count;
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06009E0F RID: 40463 RVA: 0x0033A206 File Offset: 0x00338406
		public IEnumerable<T> CaseValues
		{
			get
			{
				return from e in this.cases
				select e.Value;
			}
		}

		// Token: 0x17001735 RID: 5941
		public T this[int index]
		{
			get
			{
				return this.cases[index].Value;
			}
		}

		// Token: 0x06009E11 RID: 40465 RVA: 0x0033A258 File Offset: 0x00338458
		public void AddCase(int ratio, T value)
		{
			if (ratio < 0)
			{
				throw new Exception(string.Format("invalid ratio data. ratio:{0}", ratio));
			}
			this.totalRatio += ratio;
			this.cases.Add(new RatioLottery<T>.CaseData(ratio, this.totalRatio, value));
		}

		// Token: 0x06009E12 RID: 40466 RVA: 0x0033A2A8 File Offset: 0x003384A8
		public T Decide()
		{
			int num = RandomGenerator.Next(this.TotalRatio);
			foreach (RatioLottery<T>.CaseData caseData in this.cases)
			{
				if (num < caseData.AccumulatedRatio)
				{
					return caseData.Value;
				}
			}
			throw new Exception(string.Format("[RatioLottery] pick failed. randomValue:{0} totalRatio:{1}", num, this.totalRatio));
		}

		// Token: 0x06009E13 RID: 40467 RVA: 0x0033A338 File Offset: 0x00338538
		public IEnumerator<T> GetEnumerator()
		{
			return this.CaseValues.GetEnumerator();
		}

		// Token: 0x06009E14 RID: 40468 RVA: 0x0033A345 File Offset: 0x00338545
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.CaseValues.GetEnumerator();
		}

		// Token: 0x04009093 RID: 37011
		private readonly List<RatioLottery<T>.CaseData> cases = new List<RatioLottery<T>.CaseData>();

		// Token: 0x04009094 RID: 37012
		private int totalRatio;

		// Token: 0x02001A3D RID: 6717
		private readonly struct CaseData
		{
			// Token: 0x0600BB7D RID: 47997 RVA: 0x0036F765 File Offset: 0x0036D965
			public CaseData(int ratio, int accumulatedRatio, T value)
			{
				this.Ratio = ratio;
				this.AccumulatedRatio = accumulatedRatio;
				this.Value = value;
			}

			// Token: 0x17001A08 RID: 6664
			// (get) Token: 0x0600BB7E RID: 47998 RVA: 0x0036F77C File Offset: 0x0036D97C
			public int Ratio { get; }

			// Token: 0x17001A09 RID: 6665
			// (get) Token: 0x0600BB7F RID: 47999 RVA: 0x0036F784 File Offset: 0x0036D984
			public int AccumulatedRatio { get; }

			// Token: 0x17001A0A RID: 6666
			// (get) Token: 0x0600BB80 RID: 48000 RVA: 0x0036F78C File Offset: 0x0036D98C
			public T Value { get; }
		}
	}
}

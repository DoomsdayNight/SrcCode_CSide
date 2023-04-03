using System;
using System.Collections.Generic;
using System.Linq;

namespace Cs.Math.Lottery
{
	// Token: 0x020010D0 RID: 4304
	public sealed class RateLottery<T> : IRateLottery<T>, IReadOnlyRateLottery<T>
	{
		// Token: 0x06009DFF RID: 40447 RVA: 0x00339EFD File Offset: 0x003380FD
		public RateLottery(T defaultValue)
		{
			this.defaultValue = defaultValue;
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06009E00 RID: 40448 RVA: 0x00339F17 File Offset: 0x00338117
		public int TotalRate
		{
			get
			{
				return this.totalRate;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06009E01 RID: 40449 RVA: 0x00339F1F File Offset: 0x0033811F
		public int CaseCount
		{
			get
			{
				return this.cases.Count;
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06009E02 RID: 40450 RVA: 0x00339F2C File Offset: 0x0033812C
		public bool HasDefaultRate
		{
			get
			{
				return this.totalRate < 10000;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06009E03 RID: 40451 RVA: 0x00339F3B File Offset: 0x0033813B
		public bool HasFullRate
		{
			get
			{
				return this.totalRate == 10000;
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06009E04 RID: 40452 RVA: 0x00339F4C File Offset: 0x0033814C
		public IEnumerable<RateLottery<T>.CaseData> Cases
		{
			get
			{
				if (!this.HasFullRate)
				{
					return this.cases.Union(new RateLottery<T>.CaseData[]
					{
						this.DefaultCase
					});
				}
				return this.cases;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06009E05 RID: 40453 RVA: 0x00339F88 File Offset: 0x00338188
		private RateLottery<T>.CaseData DefaultCase
		{
			get
			{
				return new RateLottery<T>.CaseData(10000 - this.totalRate, 10000, this.defaultValue);
			}
		}

		// Token: 0x06009E06 RID: 40454 RVA: 0x00339FA8 File Offset: 0x003381A8
		public void AddCase(int rate, T value)
		{
			if (rate < 0 || this.totalRate + rate > 10000)
			{
				throw new Exception(string.Format("rate value overflow. current:{0} add:{1}", this.totalRate, rate));
			}
			if (rate == 0)
			{
				return;
			}
			this.totalRate += rate;
			this.cases.Add(new RateLottery<T>.CaseData(rate, this.totalRate, value));
		}

		// Token: 0x06009E07 RID: 40455 RVA: 0x0033A014 File Offset: 0x00338214
		public bool Decide(out T result)
		{
			int num = RandomGenerator.Next(10000);
			foreach (RateLottery<T>.CaseData caseData in this.cases)
			{
				if (num < caseData.AccumulatedRate)
				{
					result = caseData.Value;
					return true;
				}
			}
			result = this.defaultValue;
			return false;
		}

		// Token: 0x06009E08 RID: 40456 RVA: 0x0033A098 File Offset: 0x00338298
		public T Decide()
		{
			T result;
			this.Decide(out result);
			return result;
		}

		// Token: 0x06009E09 RID: 40457 RVA: 0x0033A0B0 File Offset: 0x003382B0
		public bool Decide(out RateLottery<T>.CaseData result)
		{
			int num = RandomGenerator.Next(10000);
			foreach (RateLottery<T>.CaseData caseData in this.cases)
			{
				if (num < caseData.AccumulatedRate)
				{
					result = caseData;
					return true;
				}
			}
			result = this.DefaultCase;
			return false;
		}

		// Token: 0x06009E0A RID: 40458 RVA: 0x0033A12C File Offset: 0x0033832C
		public RateLottery<T>.CaseData DecideDetail()
		{
			RateLottery<T>.CaseData result;
			this.Decide(out result);
			return result;
		}

		// Token: 0x06009E0B RID: 40459 RVA: 0x0033A144 File Offset: 0x00338344
		public bool TryGetRatePercent(T value, out float ratePercent)
		{
			foreach (RateLottery<T>.CaseData caseData in this.Cases)
			{
				T value2 = caseData.Value;
				if (value2.Equals(value))
				{
					ratePercent = caseData.RatePercent;
					return true;
				}
			}
			ratePercent = 0f;
			return false;
		}

		// Token: 0x06009E0C RID: 40460 RVA: 0x0033A1C0 File Offset: 0x003383C0
		public bool HasValue(T value)
		{
			return this.cases.Any(delegate(RateLottery<T>.CaseData e)
			{
				T value2 = e.Value;
				return value2.Equals(value);
			});
		}

		// Token: 0x0400908F RID: 37007
		private const int MaxRate = 10000;

		// Token: 0x04009090 RID: 37008
		private readonly List<RateLottery<T>.CaseData> cases = new List<RateLottery<T>.CaseData>();

		// Token: 0x04009091 RID: 37009
		private readonly T defaultValue;

		// Token: 0x04009092 RID: 37010
		private int totalRate;

		// Token: 0x02001A3B RID: 6715
		public readonly struct CaseData
		{
			// Token: 0x0600BB75 RID: 47989 RVA: 0x0036F6B9 File Offset: 0x0036D8B9
			public CaseData(int rate, int accumulatedRate, T value)
			{
				this.Rate = rate;
				this.AccumulatedRate = accumulatedRate;
				this.Value = value;
			}

			// Token: 0x17001A04 RID: 6660
			// (get) Token: 0x0600BB76 RID: 47990 RVA: 0x0036F6D0 File Offset: 0x0036D8D0
			public int Rate { get; }

			// Token: 0x17001A05 RID: 6661
			// (get) Token: 0x0600BB77 RID: 47991 RVA: 0x0036F6D8 File Offset: 0x0036D8D8
			public int AccumulatedRate { get; }

			// Token: 0x17001A06 RID: 6662
			// (get) Token: 0x0600BB78 RID: 47992 RVA: 0x0036F6E0 File Offset: 0x0036D8E0
			public T Value { get; }

			// Token: 0x17001A07 RID: 6663
			// (get) Token: 0x0600BB79 RID: 47993 RVA: 0x0036F6E8 File Offset: 0x0036D8E8
			public float RatePercent
			{
				get
				{
					return (float)this.Rate * 0.01f;
				}
			}

			// Token: 0x0600BB7A RID: 47994 RVA: 0x0036F6F8 File Offset: 0x0036D8F8
			public override string ToString()
			{
				string format = "{0} ({1:0.00}%)";
				T value = this.Value;
				return string.Format(format, value.ToString(), this.RatePercent);
			}
		}
	}
}

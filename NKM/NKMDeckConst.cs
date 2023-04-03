using System;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003D6 RID: 982
	public sealed class NKMDeckConst
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0006F36A File Offset: 0x0006D56A
		public int DefaultRaidDeckCount
		{
			get
			{
				return this.defaultRaidDeckCount;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0006F372 File Offset: 0x0006D572
		public int MaxRaidDeckCount
		{
			get
			{
				return this.maxRaidDeckCount;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0006F37A File Offset: 0x0006D57A
		public int MaxNormalDeckCount
		{
			get
			{
				return this.maxNormalDeckCount;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x0006F382 File Offset: 0x0006D582
		public int MaxPvpDeckCount
		{
			get
			{
				return this.maxPvpDeckCount;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0006F38A File Offset: 0x0006D58A
		public int MaxDailyDeckCount
		{
			get
			{
				return this.maxDailyDeckCount;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0006F392 File Offset: 0x0006D592
		public int MaxFriendDeckCount
		{
			get
			{
				return this.maxFriendDeckCount;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0006F39A File Offset: 0x0006D59A
		public int MaxPvpDefenceDeckCount
		{
			get
			{
				return this.maxPvpDefenceDeckCount;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x0006F3A2 File Offset: 0x0006D5A2
		public int MaxTrimingDeckCount
		{
			get
			{
				return this.maxTrimingDeckCount;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0006F3AA File Offset: 0x0006D5AA
		public int MaxDiveDeckCount
		{
			get
			{
				return this.maxDiveDeckCount;
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0006F3B2 File Offset: 0x0006D5B2
		public void Validate()
		{
			if (this.defaultRaidDeckCount > this.maxRaidDeckCount)
			{
				NKMTempletError.Add(string.Format("[DeckConst] 레이드 소대의 최대 개수가 기본 소대 개수 보다 적음 minCount:{0} maxCount:{1}", this.defaultRaidDeckCount, this.maxRaidDeckCount), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDeckConst.cs", 32);
			}
		}

		// Token: 0x040012EC RID: 4844
		private readonly int defaultRaidDeckCount = 3;

		// Token: 0x040012ED RID: 4845
		private readonly int maxRaidDeckCount = 6;

		// Token: 0x040012EE RID: 4846
		private readonly int maxNormalDeckCount = 10;

		// Token: 0x040012EF RID: 4847
		private readonly int maxPvpDeckCount = 4;

		// Token: 0x040012F0 RID: 4848
		private readonly int maxDailyDeckCount = 20;

		// Token: 0x040012F1 RID: 4849
		private readonly int maxFriendDeckCount = 1;

		// Token: 0x040012F2 RID: 4850
		private readonly int maxPvpDefenceDeckCount = 1;

		// Token: 0x040012F3 RID: 4851
		private readonly int maxTrimingDeckCount = 3;

		// Token: 0x040012F4 RID: 4852
		private readonly int maxDiveDeckCount = 4;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Shop;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200044D RID: 1101
	public sealed class NKMPvpCommonConst
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0008E75C File Offset: 0x0008C95C
		public static NKMPvpCommonConst Instance { get; } = new NKMPvpCommonConst();

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0008E763 File Offset: 0x0008C963
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x0008E76B File Offset: 0x0008C96B
		public int DEMOTION_SCORE { get; private set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0008E774 File Offset: 0x0008C974
		public int LEAGUE_PVP_RESET_SCORE
		{
			get
			{
				return this.leagueResetScore;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0008E77C File Offset: 0x0008C97C
		public int SCORE_MIN_INTERVAL_UNIT
		{
			get
			{
				return this.scoreMinIntervalUnit;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0008E784 File Offset: 0x0008C984
		public int RANK_PVP_LOSE_PENALTY_SCORE
		{
			get
			{
				return this.rankLosePenaltyScore;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0008E78C File Offset: 0x0008C98C
		public int ALL_RANK_LIST_MAX_COUNT
		{
			get
			{
				return this.defaultRankListMaxCount;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0008E794 File Offset: 0x0008C994
		public int LEAGUE_RANK_LIST_MAX_COUNT
		{
			get
			{
				return this.leagueRankListMaxCount;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0008E79C File Offset: 0x0008C99C
		public int RANK_SIMPLE_COUNT
		{
			get
			{
				return this.rankSimpleCount;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x0008E7A4 File Offset: 0x0008C9A4
		public int PvpUnitBanExceptionScore
		{
			get
			{
				return this.unitBanExceptionScore;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0008E7AC File Offset: 0x0008C9AC
		public int AsyncTicketChargeInterval
		{
			get
			{
				return this.asyncTicketChargeInterval;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0008E7B4 File Offset: 0x0008C9B4
		public int AsyncTicketMaxCount
		{
			get
			{
				return this.asyncTicketMaxCount;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0008E7BC File Offset: 0x0008C9BC
		public int AsyncTicketChargeCount
		{
			get
			{
				return this.asyncTicketChargeCount;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0008E7C4 File Offset: 0x0008C9C4
		public TimeSpan AsyncTicketChargeTimeSpan
		{
			get
			{
				return this.asyncTicketChargeTimeSpan;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0008E7CC File Offset: 0x0008C9CC
		public int MaxHistoryCount
		{
			get
			{
				return this.maxHistoryCount;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0008E7D4 File Offset: 0x0008C9D4
		public long CHARGE_POINT_REFRESH_INTERVAL_TICKS
		{
			get
			{
				return this.chargePointRefreshIntervalTicks;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0008E7DC File Offset: 0x0008C9DC
		public TimeSpan ChargePointRefreshInterval
		{
			get
			{
				return new TimeSpan(this.chargePointRefreshIntervalTicks);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0008E7E9 File Offset: 0x0008C9E9
		public int CHARGE_POINT_MAX_COUNT_FOR_PRACTICE
		{
			get
			{
				return this.chargePointMaxCountForPractice;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0008E7F1 File Offset: 0x0008C9F1
		public int CHARGE_POINT_MAX_COUNT
		{
			get
			{
				return this.chargePointMax;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0008E7F9 File Offset: 0x0008C9F9
		public int CHARGE_POINT_ONE_STEP
		{
			get
			{
				return this.chargePointCount;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x0008E801 File Offset: 0x0008CA01
		public int ASYNC_PVP_WIN_POINT
		{
			get
			{
				return this.asyncPvpWinPoint;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0008E809 File Offset: 0x0008CA09
		public int ASYNC_PVP_LOSE_POINT
		{
			get
			{
				return this.asyncPvpLosePoint;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0008E811 File Offset: 0x0008CA11
		public int RANK_PVP_WIN_POINT
		{
			get
			{
				return this.rankPvpWinPoint;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0008E819 File Offset: 0x0008CA19
		public int RANK_PVP_LOSE_POINT
		{
			get
			{
				return this.rankPvpLosePoint;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0008E821 File Offset: 0x0008CA21
		public int LEAGUE_PVP_WIN_POINT
		{
			get
			{
				return this.leaguePvpWinPoint;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0008E829 File Offset: 0x0008CA29
		public int LEAGUE_PVP_LOSE_POINT
		{
			get
			{
				return this.leaguePvpLosePoint;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x0008E831 File Offset: 0x0008CA31
		public int RANK_PVP_OPEN_POINT
		{
			get
			{
				return this.rankPvpOpenPoint;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0008E839 File Offset: 0x0008CA39
		public int LEAGUE_PVP_OPEN_POINT
		{
			get
			{
				return this.leaguePvpOpenPoint;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0008E841 File Offset: 0x0008CA41
		public int ASYNC_TARTGET_COUNT_STANDARD_ME
		{
			get
			{
				return this.asyncTargetCountStandardTime;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0008E849 File Offset: 0x0008CA49
		public string RankUnlockPopupTitle
		{
			get
			{
				return this.rankUnlockPopupTitle;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0008E851 File Offset: 0x0008CA51
		public string RankUnlockPopupDesc
		{
			get
			{
				return this.rankUnlockPopupDesc;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x0008E859 File Offset: 0x0008CA59
		public string RankUnlockPopupImageName
		{
			get
			{
				return this.rankUnlockPopupImageName;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0008E861 File Offset: 0x0008CA61
		public string LeagueUnlockPopupTitle
		{
			get
			{
				return this.leagueUnlockPopupTitle;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0008E869 File Offset: 0x0008CA69
		public string LeagueUnlockPopupDesc
		{
			get
			{
				return this.leagueUnlockPopupDesc;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0008E871 File Offset: 0x0008CA71
		public string LeagueUnlockPopupImageName
		{
			get
			{
				return this.leagueUnlockPopupImageName;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0008E879 File Offset: 0x0008CA79
		public NKMPvpCommonConst.DraftBanConst DraftBan { get; } = new NKMPvpCommonConst.DraftBanConst();

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0008E881 File Offset: 0x0008CA81
		public NKMPvpCommonConst.LeaguePvpConst LeaguePvp { get; } = new NKMPvpCommonConst.LeaguePvpConst();

		// Token: 0x06001E10 RID: 7696 RVA: 0x0008E88C File Offset: 0x0008CA8C
		public static void LoadFromLua()
		{
			string text = "LUA_PVP_CONST";
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath("AB_SCRIPT", text, true))
				{
					Log.ErrorAndExit("fail loading lua file:" + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 98);
				}
				else
				{
					if (!nkmlua.OpenTable("PVP_CONST"))
					{
						Log.ErrorAndExit("fail open table:PVP_CONST", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 104);
					}
					NKMPvpCommonConst.Instance.LoadFromLua(nkmlua);
				}
			}
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0008E914 File Offset: 0x0008CB14
		public void Join()
		{
			this.LeaguePvp.Join();
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0008E921 File Offset: 0x0008CB21
		public void Validate()
		{
			this.LeaguePvp.Validate();
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0008E930 File Offset: 0x0008CB30
		private void LoadFromLua(NKMLua lua)
		{
			bool flag = true;
			this.DEMOTION_SCORE = lua.GetInt32("DemotionScore");
			flag &= lua.GetData("LeagueResetScore", ref this.leagueResetScore);
			flag &= lua.GetData("ScoreMinIntervalUnit", ref this.scoreMinIntervalUnit);
			flag &= lua.GetData("RankLosePenaltyScore", ref this.rankLosePenaltyScore);
			flag &= lua.GetData("DefaultRankListMaxCount", ref this.defaultRankListMaxCount);
			flag &= lua.GetData("LeagueRankListMaxCount", ref this.leagueRankListMaxCount);
			flag &= lua.GetData("RankSimpleCount", ref this.rankSimpleCount);
			flag &= lua.GetData("UnitBanExceptionScore", ref this.unitBanExceptionScore);
			flag &= lua.GetData("AsyncTicketChargeInterval", ref this.asyncTicketChargeInterval);
			flag &= lua.GetData("AsyncTicketMaxCount", ref this.asyncTicketMaxCount);
			flag &= lua.GetData("AsyncTicketChargeCount", ref this.asyncTicketChargeCount);
			flag &= lua.GetData("MaxHistoryCount", ref this.maxHistoryCount);
			flag &= lua.GetData("ChargePointRefreshIntervalTicks", ref this.chargePointRefreshIntervalTicks);
			flag &= lua.GetData("ChargePointMaxCountForPractice", ref this.chargePointMaxCountForPractice);
			flag &= lua.GetData("ChargePointMax", ref this.chargePointMax);
			flag &= lua.GetData("ChargePointCount", ref this.chargePointCount);
			flag &= lua.GetData("AsyncPvpWinPoint", ref this.asyncPvpWinPoint);
			flag &= lua.GetData("AsyncPvpLosePoint", ref this.asyncPvpLosePoint);
			flag &= lua.GetData("RankPvpWinPoint", ref this.rankPvpWinPoint);
			flag &= lua.GetData("RankPvpLosePoint", ref this.rankPvpLosePoint);
			flag &= lua.GetData("LeaguePvpWinPoint", ref this.leaguePvpWinPoint);
			flag &= lua.GetData("LeaguePvpLosePoint", ref this.leaguePvpLosePoint);
			flag &= lua.GetData("RankPvpOpenPoint", ref this.rankPvpOpenPoint);
			flag &= lua.GetData("LeaguePvpOpenPoint", ref this.leaguePvpOpenPoint);
			flag &= lua.GetData("AsyncTargetCountStandardTime", ref this.asyncTargetCountStandardTime);
			using (lua.OpenTable("RankUnlockInfo", "RankUnlockInfo table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 152))
			{
				this.RankUnlockInfo = UnlockInfo.LoadFromLua(lua, true);
				lua.GetData("m_strPopupTitle", ref this.rankUnlockPopupTitle);
				lua.GetData("m_strPopupDesc", ref this.rankUnlockPopupDesc);
				lua.GetData("m_strPopupImageName", ref this.rankUnlockPopupImageName);
			}
			using (lua.OpenTable("LeagueUnlockInfo", "LeagueUnlockInfo table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 160))
			{
				this.LeagueUnlockInfo = UnlockInfo.LoadFromLua(lua, true);
				lua.GetData("m_strPopupTitle", ref this.leagueUnlockPopupTitle);
				lua.GetData("m_strPopupDesc", ref this.leagueUnlockPopupDesc);
				lua.GetData("m_strPopupImageName", ref this.leagueUnlockPopupImageName);
			}
			this.DraftBan.LoadFromLua(lua);
			this.LeaguePvp.LoadFromLua(lua);
			if (!flag)
			{
				Log.ErrorAndExit("lua loading fail. tableName:Pvp", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 173);
			}
			this.asyncTicketChargeTimeSpan = TimeSpan.FromSeconds((double)this.asyncTicketChargeInterval);
		}

		// Token: 0x04001E61 RID: 7777
		private const string TableName = "Pvp";

		// Token: 0x04001E62 RID: 7778
		private int leagueResetScore;

		// Token: 0x04001E63 RID: 7779
		private int scoreMinIntervalUnit;

		// Token: 0x04001E64 RID: 7780
		private int rankLosePenaltyScore;

		// Token: 0x04001E65 RID: 7781
		private int defaultRankListMaxCount;

		// Token: 0x04001E66 RID: 7782
		private int leagueRankListMaxCount;

		// Token: 0x04001E67 RID: 7783
		private int rankSimpleCount;

		// Token: 0x04001E68 RID: 7784
		private int unitBanExceptionScore;

		// Token: 0x04001E69 RID: 7785
		private int asyncTicketChargeInterval;

		// Token: 0x04001E6A RID: 7786
		private int asyncTicketMaxCount;

		// Token: 0x04001E6B RID: 7787
		private int asyncTicketChargeCount;

		// Token: 0x04001E6C RID: 7788
		private TimeSpan asyncTicketChargeTimeSpan;

		// Token: 0x04001E6D RID: 7789
		private int maxHistoryCount = 30;

		// Token: 0x04001E6E RID: 7790
		private long chargePointRefreshIntervalTicks = 216000000000L;

		// Token: 0x04001E6F RID: 7791
		private int chargePointMaxCountForPractice = 100;

		// Token: 0x04001E70 RID: 7792
		private int chargePointMax = 900;

		// Token: 0x04001E71 RID: 7793
		private int chargePointCount = 225;

		// Token: 0x04001E72 RID: 7794
		private int asyncPvpWinPoint = 75;

		// Token: 0x04001E73 RID: 7795
		private int asyncPvpLosePoint = 50;

		// Token: 0x04001E74 RID: 7796
		private int rankPvpWinPoint = 150;

		// Token: 0x04001E75 RID: 7797
		private int rankPvpLosePoint = 125;

		// Token: 0x04001E76 RID: 7798
		private int leaguePvpWinPoint = 150;

		// Token: 0x04001E77 RID: 7799
		private int leaguePvpLosePoint = 90;

		// Token: 0x04001E78 RID: 7800
		private int rankPvpOpenPoint = 1000;

		// Token: 0x04001E79 RID: 7801
		private int leaguePvpOpenPoint = 3000;

		// Token: 0x04001E7A RID: 7802
		private int asyncTargetCountStandardTime = 30;

		// Token: 0x04001E7B RID: 7803
		private string rankUnlockPopupTitle = "";

		// Token: 0x04001E7C RID: 7804
		private string rankUnlockPopupDesc = "";

		// Token: 0x04001E7D RID: 7805
		private string rankUnlockPopupImageName = "";

		// Token: 0x04001E7E RID: 7806
		private string leagueUnlockPopupTitle = "";

		// Token: 0x04001E7F RID: 7807
		private string leagueUnlockPopupDesc = "";

		// Token: 0x04001E80 RID: 7808
		private string leagueUnlockPopupImageName = "";

		// Token: 0x04001E83 RID: 7811
		public UnlockInfo RankUnlockInfo;

		// Token: 0x04001E84 RID: 7812
		public UnlockInfo LeagueUnlockInfo;

		// Token: 0x020011F8 RID: 4600
		public sealed class DraftBanConst
		{
			// Token: 0x170017AD RID: 6061
			// (get) Token: 0x0600A13C RID: 41276 RVA: 0x0033F6FA File Offset: 0x0033D8FA
			// (set) Token: 0x0600A13D RID: 41277 RVA: 0x0033F702 File Offset: 0x0033D902
			public int MinUnitCount { get; private set; }

			// Token: 0x170017AE RID: 6062
			// (get) Token: 0x0600A13E RID: 41278 RVA: 0x0033F70B File Offset: 0x0033D90B
			// (set) Token: 0x0600A13F RID: 41279 RVA: 0x0033F713 File Offset: 0x0033D913
			public int MinShipCount { get; private set; }

			// Token: 0x0600A140 RID: 41280 RVA: 0x0033F71C File Offset: 0x0033D91C
			public void LoadFromLua(NKMLua lua)
			{
				using (lua.OpenTable("DraftBan", "DraftBan table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 186))
				{
					this.MinUnitCount = lua.GetInt32("MinUnitCount");
					this.MinShipCount = lua.GetInt32("MinShipCount");
				}
			}
		}

		// Token: 0x020011F9 RID: 4601
		public sealed class LeaguePvpConst
		{
			// Token: 0x170017AF RID: 6063
			// (get) Token: 0x0600A142 RID: 41282 RVA: 0x0033F78C File Offset: 0x0033D98C
			public IReadOnlyList<DayOfWeek> OpenDaysOfWeek
			{
				get
				{
					return this.openDaysOfWeek;
				}
			}

			// Token: 0x170017B0 RID: 6064
			// (get) Token: 0x0600A143 RID: 41283 RVA: 0x0033F794 File Offset: 0x0033D994
			// (set) Token: 0x0600A144 RID: 41284 RVA: 0x0033F79C File Offset: 0x0033D99C
			public TimeSpan OpenTimeStart { get; private set; }

			// Token: 0x170017B1 RID: 6065
			// (get) Token: 0x0600A145 RID: 41285 RVA: 0x0033F7A5 File Offset: 0x0033D9A5
			// (set) Token: 0x0600A146 RID: 41286 RVA: 0x0033F7AD File Offset: 0x0033D9AD
			public TimeSpan OpenTimeEnd { get; private set; }

			// Token: 0x170017B2 RID: 6066
			// (get) Token: 0x0600A147 RID: 41287 RVA: 0x0033F7B6 File Offset: 0x0033D9B6
			// (set) Token: 0x0600A148 RID: 41288 RVA: 0x0033F7BE File Offset: 0x0033D9BE
			public float ShipHpMultiply { get; private set; }

			// Token: 0x170017B3 RID: 6067
			// (get) Token: 0x0600A149 RID: 41289 RVA: 0x0033F7C7 File Offset: 0x0033D9C7
			// (set) Token: 0x0600A14A RID: 41290 RVA: 0x0033F7CF File Offset: 0x0033D9CF
			public float ShipAttackPowerMultiply { get; private set; }

			// Token: 0x170017B4 RID: 6068
			// (get) Token: 0x0600A14B RID: 41291 RVA: 0x0033F7D8 File Offset: 0x0033D9D8
			// (set) Token: 0x0600A14C RID: 41292 RVA: 0x0033F7E0 File Offset: 0x0033D9E0
			public int TotalGameTimeSec { get; private set; }

			// Token: 0x170017B5 RID: 6069
			// (get) Token: 0x0600A14D RID: 41293 RVA: 0x0033F7E9 File Offset: 0x0033D9E9
			// (set) Token: 0x0600A14E RID: 41294 RVA: 0x0033F7F1 File Offset: 0x0033D9F1
			public float RageBuffShipHpRate { get; private set; }

			// Token: 0x170017B6 RID: 6070
			// (get) Token: 0x0600A14F RID: 41295 RVA: 0x0033F7FA File Offset: 0x0033D9FA
			// (set) Token: 0x0600A150 RID: 41296 RVA: 0x0033F802 File Offset: 0x0033DA02
			public NKMBuffTemplet RageBuff { get; private set; }

			// Token: 0x170017B7 RID: 6071
			// (get) Token: 0x0600A151 RID: 41297 RVA: 0x0033F80B File Offset: 0x0033DA0B
			// (set) Token: 0x0600A152 RID: 41298 RVA: 0x0033F813 File Offset: 0x0033DA13
			public NKMBuffTemplet UiRageBuff { get; private set; }

			// Token: 0x170017B8 RID: 6072
			// (get) Token: 0x0600A153 RID: 41299 RVA: 0x0033F81C File Offset: 0x0033DA1C
			// (set) Token: 0x0600A154 RID: 41300 RVA: 0x0033F824 File Offset: 0x0033DA24
			public NKMBuffTemplet DeadlineBuff { get; private set; }

			// Token: 0x170017B9 RID: 6073
			// (get) Token: 0x0600A155 RID: 41301 RVA: 0x0033F82D File Offset: 0x0033DA2D
			// (set) Token: 0x0600A156 RID: 41302 RVA: 0x0033F835 File Offset: 0x0033DA35
			public NKMBuffTemplet UiDeadlineBuff { get; private set; }

			// Token: 0x0600A157 RID: 41303 RVA: 0x0033F840 File Offset: 0x0033DA40
			public bool IsValidTime(DateTime currentSvc)
			{
				if (!this.openDaysOfWeek.Any((DayOfWeek e) => e == currentSvc.DayOfWeek))
				{
					return false;
				}
				TimeSpan timeOfDay = currentSvc.TimeOfDay;
				return this.OpenTimeStart <= timeOfDay && timeOfDay < this.OpenTimeEnd;
			}

			// Token: 0x0600A158 RID: 41304 RVA: 0x0033F8A0 File Offset: 0x0033DAA0
			public void LoadFromLua(NKMLua lua)
			{
				if (!lua.OpenTable("LeaguePvp"))
				{
					NKMTempletError.Add("[LeaguePvp] open table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 230);
					return;
				}
				this.openDaysOfWeek.Clear();
				using (lua.OpenTable("OpenDaysOfWeek", "OpenDaysOfWeek table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 236))
				{
					int num = 1;
					SHOP_RESET_TYPE? shop_RESET_TYPE = null;
					while (lua.GetExplicitEnum<SHOP_RESET_TYPE>(num, ref shop_RESET_TYPE))
					{
						DayOfWeek dayOfWeek;
						if (!shop_RESET_TYPE.Value.ToDayOfWeek(out dayOfWeek))
						{
							NKMTempletError.Add(string.Format("[LeaguePvp] invalid value:{0}", shop_RESET_TYPE.Value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 244);
							num++;
						}
						else if (this.openDaysOfWeek.Any((DayOfWeek e) => e == dayOfWeek))
						{
							NKMTempletError.Add(string.Format("[LeaguePvp] duplicated value:{0}", shop_RESET_TYPE.Value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 251);
							num++;
						}
						else
						{
							this.openDaysOfWeek.Add(dayOfWeek);
							num++;
						}
					}
				}
				this.OpenTimeStart = lua.GetTimeSpan("OpenTimeStart");
				this.OpenTimeEnd = lua.GetTimeSpan("OpenTimeEnd");
				this.ShipHpMultiply = lua.GetFloat("ShipHpMultiply");
				this.ShipAttackPowerMultiply = lua.GetFloat("ShipAttackPowerMultiply");
				this.TotalGameTimeSec = lua.GetInt32("TotalGameTimeSec");
				this.RageBuffShipHpRate = lua.GetFloat("RageBuff_ShipHpRate");
				this.rageBuffId = lua.GetString("RageBuff_Id");
				this.deadlineBuffId = lua.GetString("DeadlineBuff_Id");
				this.uiRageBuffId = lua.GetString("UiRageBuff_Id");
				this.uiDeadlineBuffId = lua.GetString("UiDeadlineBuff_Id");
				using (lua.OpenTable("DeadlineBuff_Condition", "DeadlineBuff_Condition table open failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 272))
				{
					int num2 = 1;
					while (lua.OpenTable(num2++))
					{
						NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition item = new NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition(lua.GetInt32("RemainTimeSec"), lua.GetInt32("BuffLevel"));
						this.deadlineBuffConditions.Add(item);
						lua.CloseTable();
					}
				}
				lua.CloseTable();
			}

			// Token: 0x0600A159 RID: 41305 RVA: 0x0033FAF8 File Offset: 0x0033DCF8
			public bool GetDeadlineBuffCondition(float gameRemainTime, out NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition result)
			{
				foreach (NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition deadlineBuffCondition in this.deadlineBuffConditions)
				{
					if (gameRemainTime < (float)deadlineBuffCondition.ReaminTimeSec)
					{
						result = deadlineBuffCondition;
						return true;
					}
				}
				result = default(NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition);
				return false;
			}

			// Token: 0x0600A15A RID: 41306 RVA: 0x0033FB64 File Offset: 0x0033DD64
			public float GetDeadlineBuffConditionTimeMax()
			{
				return (float)this.deadlineBuffConditions.Last<NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition>().ReaminTimeSec;
			}

			// Token: 0x0600A15B RID: 41307 RVA: 0x0033FB85 File Offset: 0x0033DD85
			public NKMBuffTemplet GetRageBuff(bool isUnit)
			{
				if (!isUnit)
				{
					return this.UiRageBuff;
				}
				return this.RageBuff;
			}

			// Token: 0x0600A15C RID: 41308 RVA: 0x0033FB97 File Offset: 0x0033DD97
			public NKMBuffTemplet GetDeadlineBuff(bool isUnit)
			{
				if (!isUnit)
				{
					return this.UiDeadlineBuff;
				}
				return this.DeadlineBuff;
			}

			// Token: 0x0600A15D RID: 41309 RVA: 0x0033FBAC File Offset: 0x0033DDAC
			public void Join()
			{
				if (!string.IsNullOrEmpty(this.rageBuffId))
				{
					this.RageBuff = NKMBuffTemplet.Find(this.rageBuffId);
					if (this.RageBuff == null)
					{
						NKMTempletError.Add("[LeaguePvpConst] invalid RageBuff_Id:" + this.rageBuffId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 325);
					}
				}
				if (!string.IsNullOrEmpty(this.uiRageBuffId))
				{
					this.UiRageBuff = NKMBuffTemplet.Find(this.uiRageBuffId);
					if (this.UiRageBuff == null)
					{
						NKMTempletError.Add("[LeaguePvpConst] invalid UiRageBuff_Id:" + this.uiRageBuffId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 334);
					}
				}
				if (!string.IsNullOrEmpty(this.deadlineBuffId))
				{
					this.DeadlineBuff = NKMBuffTemplet.Find(this.deadlineBuffId);
					if (this.DeadlineBuff == null)
					{
						NKMTempletError.Add("[LeaguePvpConst] invalid DeadlineBuff_Id:" + this.deadlineBuffId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 343);
					}
				}
				if (!string.IsNullOrEmpty(this.uiDeadlineBuffId))
				{
					this.UiDeadlineBuff = NKMBuffTemplet.Find(this.uiDeadlineBuffId);
					if (this.UiDeadlineBuff == null)
					{
						NKMTempletError.Add("[LeaguePvpConst] invalid UiDeadlineBuff_Id:" + this.uiDeadlineBuffId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 352);
					}
				}
				this.deadlineBuffConditions.Sort((NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition a, NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition b) => a.ReaminTimeSec.CompareTo(b.ReaminTimeSec));
			}

			// Token: 0x0600A15E RID: 41310 RVA: 0x0033FCF8 File Offset: 0x0033DEF8
			public void Validate()
			{
				if (this.RageBuffShipHpRate <= 0f || this.RageBuffShipHpRate >= 1f)
				{
					NKMTempletError.Add(string.Format("[LeaguePvpConst] invalid RageBuff_ShipHpRate:{0}", this.RageBuffShipHpRate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 364);
				}
				if (this.OpenTimeStart >= this.OpenTimeEnd)
				{
					NKMTempletError.Add(string.Format("[LeaguePvp] invalid open time. start:{0} end:{1}", this.OpenTimeStart, this.OpenTimeEnd), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 369);
				}
				if (this.DeadlineBuff != null && !this.deadlineBuffConditions.Any<NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition>())
				{
					NKMTempletError.Add("[LeaguePvp] 데드라인 버프가 설정되었지만 조건 설정이 비어있음. buffId:" + this.deadlineBuffId, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 374);
				}
				if (this.deadlineBuffConditions.Any<NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition>() && this.DeadlineBuff == null)
				{
					NKMTempletError.Add(string.Format("[LeaguePvp] 데드라인 버프 조건이 설정되었으나 버프 아이디가 비어있음. #conditions:{0}", this.deadlineBuffConditions.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 379);
				}
				if (this.TotalGameTimeSec <= 0 || this.TotalGameTimeSec >= 3600)
				{
					NKMTempletError.Add(string.Format("[LeaguePvp] 게임시간 설정은 1초 ~ 1시간 이내의 값만 허용. TotalGameTimeSec:{0}", this.TotalGameTimeSec), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 384);
				}
				if (this.ShipHpMultiply <= 0f || this.ShipHpMultiply >= 10f)
				{
					NKMTempletError.Add(string.Format("[LeaguePvp] 함선 체력 배율 허용범위 벗어남. ShipHpMultiply:{0}", this.ShipHpMultiply), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 389);
				}
				if (this.ShipAttackPowerMultiply <= 0f || this.ShipAttackPowerMultiply >= 10f)
				{
					NKMTempletError.Add(string.Format("[LeaguePvp] 함선 공격력 배율 허용범위 벗어남. ShipAttackPowerMultiply:{0}", this.ShipAttackPowerMultiply), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPCommonConst.cs", 394);
				}
			}

			// Token: 0x04009409 RID: 37897
			private string rageBuffId;

			// Token: 0x0400940A RID: 37898
			private string deadlineBuffId;

			// Token: 0x0400940B RID: 37899
			private string uiRageBuffId;

			// Token: 0x0400940C RID: 37900
			private string uiDeadlineBuffId;

			// Token: 0x0400940D RID: 37901
			private readonly List<DayOfWeek> openDaysOfWeek = new List<DayOfWeek>();

			// Token: 0x0400940E RID: 37902
			private readonly List<NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition> deadlineBuffConditions = new List<NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition>();

			// Token: 0x02001A4D RID: 6733
			public readonly struct DeadlineBuffCondition
			{
				// Token: 0x0600BB84 RID: 48004 RVA: 0x0036F7B1 File Offset: 0x0036D9B1
				public DeadlineBuffCondition(int remainTimeSec, int buffLevel)
				{
					this.ReaminTimeSec = remainTimeSec;
					this.BuffLevel = buffLevel;
				}

				// Token: 0x17001A0B RID: 6667
				// (get) Token: 0x0600BB85 RID: 48005 RVA: 0x0036F7C1 File Offset: 0x0036D9C1
				public int ReaminTimeSec { get; }

				// Token: 0x17001A0C RID: 6668
				// (get) Token: 0x0600BB86 RID: 48006 RVA: 0x0036F7C9 File Offset: 0x0036D9C9
				public int BuffLevel { get; }
			}
		}
	}
}

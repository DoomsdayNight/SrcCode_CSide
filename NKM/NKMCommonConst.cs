using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.EventPass;
using NKM.Guild;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003B2 RID: 946
	public static class NKMCommonConst
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00064BD2 File Offset: 0x00062DD2
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x00064BD9 File Offset: 0x00062DD9
		public static int WarfareRecoverItemCost { get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00064BE1 File Offset: 0x00062DE1
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x00064BE8 File Offset: 0x00062DE8
		public static int DiveArtifactReturnItemId { get; private set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00064BF0 File Offset: 0x00062DF0
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x00064BF7 File Offset: 0x00062DF7
		public static NegotiationTemplet Negotiation { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00064BFF File Offset: 0x00062DFF
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x00064C06 File Offset: 0x00062E06
		public static GuildTemplet Guild { get; private set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00064C0E File Offset: 0x00062E0E
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00064C15 File Offset: 0x00062E15
		public static int SubscriptionBuyCriteriaDate { get; private set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00064C1D File Offset: 0x00062E1D
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00064C24 File Offset: 0x00062E24
		public static NKMOperatorConstTemplet OperatorConstTemplet { get; private set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00064C2C File Offset: 0x00062E2C
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00064C33 File Offset: 0x00062E33
		public static GuildDungeonConstTemplet GuildDungeonConstTemplet { get; private set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00064C3B File Offset: 0x00062E3B
		public static NKMOfficeConst Office { get; } = new NKMOfficeConst();

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00064C42 File Offset: 0x00062E42
		public static NKMDeckConst Deck { get; } = new NKMDeckConst();

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00064C49 File Offset: 0x00062E49
		public static NKMBackgroundConst BackgroundInfo { get; } = new NKMBackgroundConst();

		// Token: 0x060018E4 RID: 6372 RVA: 0x00064C50 File Offset: 0x00062E50
		public static void LoadFromLUA(string fileName)
		{
			bool flag = true;
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true))
				{
					Log.ErrorAndExit("fail loading lua file:" + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 150);
					return;
				}
				if (nkmlua.OpenTable("m_SystemData"))
				{
					flag &= nkmlua.GetData("m_fConstEvade", ref NKMUnitStatManager.m_fConstEvade);
					flag &= nkmlua.GetData("m_fConstHit", ref NKMUnitStatManager.m_fConstHit);
					flag &= nkmlua.GetData("m_fConstCritical", ref NKMUnitStatManager.m_fConstCritical);
					flag &= nkmlua.GetData("m_fConstDef", ref NKMUnitStatManager.m_fConstDef);
					flag &= nkmlua.GetData("m_fConstEvadeDamage", ref NKMUnitStatManager.m_fConstEvadeDamage);
					flag &= nkmlua.GetData("m_fLONG_RANGE", ref NKMUnitStatManager.m_fLONG_RANGE);
					flag &= nkmlua.GetData("m_fPercentPerBanLevel", ref NKMUnitStatManager.m_fPercentPerBanLevel);
					flag &= nkmlua.GetData("m_fMaxPercentPerBanLevel", ref NKMUnitStatManager.m_fMaxPercentPerBanLevel);
					nkmlua.GetData("m_fPercentPerUpLevel", ref NKMUnitStatManager.m_fPercentPerUpLevel);
					nkmlua.GetData("m_fMaxPercentPerUpLevel", ref NKMUnitStatManager.m_fMaxPercentPerUpLevel);
					flag &= nkmlua.GetData("m_fDEFENDER_PROTECT_RATE", ref NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE);
					flag &= nkmlua.GetData("m_fDEFENDER_PROTECT_RATE_MAX", ref NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX);
					flag &= nkmlua.GetData("ROLE_TYPE_BONUS_FACTOR", ref NKMUnitStatManager.ROLE_TYPE_BONUS_FACTOR);
					flag &= nkmlua.GetData("ROLE_TYPE_REDUCE_FACTOR", ref NKMUnitStatManager.ROLE_TYPE_REDUCE_FACTOR);
					nkmlua.CloseTable();
				}
				if (nkmlua.OpenTable("UNIT_ENHANCE_DATA"))
				{
					flag &= nkmlua.GetData("ENHANCE_CREDIT_COST_PER_UNIT", ref NKMCommonConst.ENHANCE_CREDIT_COST_PER_UNIT);
					flag &= nkmlua.GetData("ENHANCE_CREDIT_COST_FACTOR", ref NKMCommonConst.ENHANCE_CREDIT_COST_FACTOR);
					flag &= nkmlua.GetData("ENHANCE_EXP_BONUS_FACTOR", ref NKMCommonConst.ENHANCE_EXP_BONUS_FACTOR);
					nkmlua.CloseTable();
				}
				int num = 0;
				using (nkmlua.OpenTable("Warfare", "[CommonConst] loading Warfare table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 187))
				{
					flag &= nkmlua.GetData("RecoverCost", ref num);
					NKMCommonConst.WarfareRecoverItemCost = num;
					nkmlua.CloseTable();
				}
				using (nkmlua.OpenTable("Dive", "[CommonConst] loading Dive table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 194))
				{
					flag &= nkmlua.GetData("ArtifactReturnItemId", ref num);
					NKMCommonConst.DiveArtifactReturnItemId = num;
					flag &= nkmlua.GetData("DiveStormCostMiscId", ref NKMDiveTemplet.DiveStormCostMiscId);
					flag &= nkmlua.GetData("DiveStormCostMultiply", ref NKMDiveTemplet.DiveStormCostMultiply);
					flag &= nkmlua.GetData("DiveStormRewardMiscId", ref NKMDiveTemplet.DiveStormRewardMiscId);
					flag &= nkmlua.GetData("DiveStormRewardMultiply", ref NKMDiveTemplet.DiveStormRewardMultiply);
					flag &= nkmlua.GetData("DiveRepairHP_RATE", ref NKMCommonConst.DiveRepairHpRate);
					flag &= nkmlua.GetData("DiveStormHP_RATE", ref NKMCommonConst.DiveStormHpRate);
				}
				using (nkmlua.OpenTable("Negotiation", "[CommonConst] loading Negotiation table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 208))
				{
					NKMCommonConst.Negotiation = new NegotiationTemplet();
					NKMCommonConst.Negotiation.LoadFromLua(nkmlua);
				}
				using (nkmlua.OpenTable("Guild", "[CommonConst] loading Guild table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 214))
				{
					NKMCommonConst.Guild = new GuildTemplet();
					NKMCommonConst.Guild.LoadFromLua(nkmlua);
				}
				using (nkmlua.OpenTable("REWARD_MULTIPLY", "[CommonConst] loading Reward_Multiply table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 220))
				{
					flag &= NKMRewardMultiplyTemplet.LoadFromLUA(nkmlua, NKMRewardMultiplyTemplet.ScopeType.General);
					flag &= NKMRewardMultiplyTemplet.LoadFromLUA(nkmlua, NKMRewardMultiplyTemplet.ScopeType.ShadowPalace);
				}
				using (nkmlua.OpenTable("SUBSCRIPTION_DATA", "[CommonConst] loading Subscription_Data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 226))
				{
					flag &= nkmlua.GetData("BUY_CRITERIA_DATE", ref num);
					NKMCommonConst.SubscriptionBuyCriteriaDate = num;
				}
				using (nkmlua.OpenTable("MENTORING_DATA", "[CommonConst] loading MentoringData table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 232))
				{
					flag &= nkmlua.GetData("MENTOR_ADD_LIMIT_LEVEL", ref NKMMentoringConst.MentorAddLimitLevel);
					flag &= nkmlua.GetData("MENTEE_INVITE_LIMIT_COUNT", ref NKMMentoringConst.MenteeInviteLimitCount);
					flag &= nkmlua.GetData("MENTEE_DELETION_AFTER_DAYS", ref NKMMentoringConst.MenteeDeletionAfterDays);
					flag &= nkmlua.GetData("MENTEE_LIMIT_BELONG_COUNT", ref NKMMentoringConst.MenteeLimitBelongCount);
					flag &= nkmlua.GetData("MENTORING_SEASON_INIT_MINUTES", ref NKMMentoringConst.MentoringSeasonInitMinutes);
					flag &= nkmlua.GetData("INVITATION_EXPIRE_DAYS", ref NKMMentoringConst.InvitationExpireDays);
					nkmlua.GetData("MENTEE_ADD_LIMIT_LEVEL", ref NKMMentoringConst.MenteeAddLimitLevel);
					nkmlua.GetData("MENTORING_ADD_LIMIT_ACTIVE_DAYS", ref NKMMentoringConst.MentoringAddLimitActiveDays);
				}
				using (nkmlua.OpenTable("FIERCE_DATA", "[FierceConst] loading fierceData table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 244))
				{
					flag &= nkmlua.GetData("MAX_BOSS_RANKING_COUNT", ref NKMFierceConst.MaxBossRankingCount);
					flag &= nkmlua.GetData("MAX_TOTAL_RANKING_COUNT", ref NKMFierceConst.MaxTotalRankingCount);
					flag &= nkmlua.GetData("RANKING_INTERVAL_MINUTES", ref NKMFierceConst.RankingIntervalMinutes);
					flag &= nkmlua.GetData("PROFILE_INTERVAL_MINUTES", ref NKMFierceConst.ProfileIntervalMinutes);
				}
				using (nkmlua.OpenTable("Operater", "[OperaterNegotiation] loading opteration negotiation data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 252))
				{
					NKMCommonConst.OperatorConstTemplet = new NKMOperatorConstTemplet();
					NKMCommonConst.OperatorConstTemplet.LoadFromLua(nkmlua);
					flag &= nkmlua.GetData("OPERATOR_SKILL_STOP_TIME", ref NKMCommonConst.OPERATOR_SKILL_STOP_TIME);
					flag &= nkmlua.GetData("OPERATOR_SKILL_DELAY_START_TIME", ref NKMCommonConst.OPERATOR_SKILL_DELAY_START_TIME);
				}
				using (nkmlua.OpenTable("GUILD_DUNGEON", "[GuildDungeonConst] loading GuildDungeon table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 262))
				{
					NKMCommonConst.GuildDungeonConstTemplet = new GuildDungeonConstTemplet();
					NKMCommonConst.GuildDungeonConstTemplet.LoadFromLua(nkmlua);
				}
				using (nkmlua.OpenTable("EVENT_PASS_DATA", "[EventPassConst] loading GuildDungeon table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 268))
				{
					flag &= nkmlua.GetData("FREE_MISSION_REROLL_COUNT", ref NKMEventPassConst.FreeMissionRerollCount);
					flag &= nkmlua.GetData("PAY_MISSION_REROLL_COUNT", ref NKMEventPassConst.PayMissionRerollCount);
					NKMEventPassConst.TotalMissionRerollCount = NKMEventPassConst.FreeMissionRerollCount + NKMEventPassConst.PayMissionRerollCount;
				}
				using (nkmlua.OpenTable("EQUIP_PRESET", "[EquipPreset] loading EquipPreset table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 276))
				{
					flag &= nkmlua.GetData("EQUIP_PRESET_BASIC_COUNT", ref NKMCommonConst.EQUIP_PRESET_BASIC_COUNT);
					flag &= nkmlua.GetData("EQUIP_PRESET_MAX_COUNT", ref NKMCommonConst.EQUIP_PRESET_MAX_COUNT);
					flag &= nkmlua.GetData("EQUIP_PRESET_EXPAND_COST_ITEM_ID", ref NKMCommonConst.EQUIP_PRESET_EXPAND_COST_ITEM_ID);
					flag &= nkmlua.GetData("EQUIP_PRESET_EXPAND_COST_VALUE", ref NKMCommonConst.EQUIP_PRESET_EXPAND_COST_VALUE);
					flag &= nkmlua.GetData("EQUIP_PRESET_NAME_MAX_LENGTH", ref NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH);
				}
				using (nkmlua.OpenTable("SHOP", "[Shop] loading Shop table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 285))
				{
					flag &= nkmlua.GetData("FeaturedListExhibitCount", ref NKMCommonConst.FeaturedListExhibitCount);
					flag &= nkmlua.GetData("FeaturedListTotalPaymentThreshold", ref NKMCommonConst.FeaturedListTotalPaymentThreshold);
				}
				using (nkmlua.OpenTable("Eternium_Recharge", "[Eternium Recharge Const] loading Eternium Recharge table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 291))
				{
					int num2 = 0;
					flag &= nkmlua.GetData("Recharge_Time", ref num2);
					NKMCommonConst.RECHARGE_TIME = TimeSpan.FromSeconds((double)num2);
				}
				using (nkmlua.OpenTable("EVENT_RACE", "[Event Race Const] loading Event Race table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 298))
				{
					flag &= nkmlua.GetData("RACE_PLAY_COUNT", ref NKMCommonConst.EVENT_RACE_PLAY_COUNT);
				}
				using (nkmlua.OpenTable("Skip", "[Skip Const] loading skip table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 303))
				{
					flag &= nkmlua.GetData("MiscItemId", ref NKMCommonConst.SkipCostMiscItemId);
					flag &= nkmlua.GetData("MiscItemCount", ref NKMCommonConst.SkipCostMiscItemCount);
				}
				using (nkmlua.OpenTable("EXTRACT_BONUS", "[Extract Bonus Const] loading Extract Bonus table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 309))
				{
					flag &= nkmlua.GetData("MaxExtractUnitSelect", ref NKMCommonConst.MaxExtractUnitSelect);
					flag &= nkmlua.GetData("ExtractBonusRatePercent_Awaken", ref NKMCommonConst.ExtractBonusRatePercent_Awaken);
					flag &= nkmlua.GetData("ExtractBonusRatePercent_SSR", ref NKMCommonConst.ExtractBonusRatePercent_SSR);
					flag &= nkmlua.GetData("ExtractBonusRatePercent_SR", ref NKMCommonConst.ExtractBonusRatePercent_SR);
				}
				if (NKMOpenTagManager.IsOpened(NKMCommonConst.ImprintOpenTag))
				{
					using (nkmlua.OpenTable("IMPRINT_STAT", "[Equip Imprint] loading Equip Imprint table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 319))
					{
						NKMCommonConst.ImprintMainOptEffectWeapon = new NKMCommonConst.ImprintMainOptEffect(nkmlua.GetFloat("IEP_WEAPON_MAIN_OPTION_MAIN_STAT"), nkmlua.GetFloat("IEP_WEAPON_MAIN_OPTION_SUB_STAT"));
						NKMCommonConst.ImprintMainOptEffectDefence = new NKMCommonConst.ImprintMainOptEffect(nkmlua.GetFloat("IEP_DEFENCE_MAIN_OPTION_MAIN_STAT"), nkmlua.GetFloat("IEP_DEFENCE_MAIN_OPTION_SUB_STAT"));
						NKMCommonConst.ImprintMainOptEffectAccessary = new NKMCommonConst.ImprintMainOptEffect(nkmlua.GetFloat("IEP_ACC_MAIN_OPTION_MAIN_STAT"), nkmlua.GetFloat("IEP_ACC_MAIN_OPTION_SUB_STAT"));
						nkmlua.GetData("IEP_IMPRINT_COST_ITEM_ID", ref NKMCommonConst.ImprintCostItemId);
						nkmlua.GetData("IEP_IMPRINT_COST_ITEM_COUNT", ref NKMCommonConst.ImprintCostItemCount);
						nkmlua.GetData("IEP_RE_IMPRINT_COST_ITEM_ID", ref NKMCommonConst.ReImprintCostItemId);
						nkmlua.GetData("IEP_RE_IMPRINT_COST_ITEM_COUNT", ref NKMCommonConst.ReImprintCostItemCount);
					}
				}
				NKMCommonConst.Office.Load(nkmlua);
				using (nkmlua.OpenTable("ITEM_CONVERT", "[CommonConst] loading Item Convert table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 339))
				{
					using (nkmlua.OpenTable("NotifyMailText", "[CommonConst] loading Item Convert table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 341))
					{
						flag &= nkmlua.GetData("ConvertTitle", ref NKMCommonConst.ConvertTitle);
						flag &= nkmlua.GetData("ConvertMessage", ref NKMCommonConst.ConvertMessage);
						flag &= nkmlua.GetData("DeleteTitle", ref NKMCommonConst.DeleteTitle);
						flag &= nkmlua.GetData("DeleteMessage", ref NKMCommonConst.DeleteMessage);
					}
				}
				using (nkmlua.OpenTable("ITEM_DROP_INFO_MAX_COUNT", "[CommonConst] loading Item Drop Info Max Count table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 350))
				{
					flag &= nkmlua.GetData("SHOP_CASH_TEMPLET", ref NKMCommonConst.DropInfoShopLimit);
					flag &= nkmlua.GetData("WORLDMAP_MISSION_TEMPLET", ref NKMCommonConst.DropInfoWorldMapMissionLimit);
					flag &= nkmlua.GetData("RAID_TEMPLET", ref NKMCommonConst.DropInfoRaidLimit);
					flag &= nkmlua.GetData("SHADOW_PALACE", ref NKMCommonConst.DropInfoShadowPalace);
					flag &= nkmlua.GetData("DIVE_TEMPLET", ref NKMCommonConst.DropInfoDiveLimit);
					flag &= nkmlua.GetData("FIERCE_POINT_REWARD", ref NKMCommonConst.DropInfoFiercePointReward);
					flag &= nkmlua.GetData("RANDOM_MOLD_BOX", ref NKMCommonConst.DropInfoRandomMoldBox);
					flag &= nkmlua.GetData("UNIT_DISMISS", ref NKMCommonConst.DropInfoUnitDismiss);
					flag &= nkmlua.GetData("UNIT_EXTRACT", ref NKMCommonConst.DropInfoUnitExtract);
					flag &= nkmlua.GetData("TRIM_DUNGEON", ref NKMCommonConst.DropInfoTrimDungeon);
					flag &= nkmlua.GetData("SUBSTREAM_SHOP", ref NKMCommonConst.DropInfoSubStreamShop);
				}
				using (nkmlua.OpenTable("ITEM_DROP_INFO_EPISODE_MAX_COUNT", "[CommonConst] loading Item Drop Info Episode Max Count table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 365))
				{
					flag &= nkmlua.GetData("EC_MAINSTREAM", ref NKMCommonConst.DropInfoMainStreamLimit);
					flag &= nkmlua.GetData("EC_SUPPLY", ref NKMCommonConst.DropInfoSupplyLimit);
					flag &= nkmlua.GetData("EC_DAILY", ref NKMCommonConst.DropInfoDailyLimit);
					flag &= nkmlua.GetData("EC_SIDESTORY", ref NKMCommonConst.DropInfoSideStoryLimit);
					flag &= nkmlua.GetData("EC_CHALLENGE", ref NKMCommonConst.DropInfoChallengeLimit);
					flag &= nkmlua.GetData("EC_COUNTERCASE", ref NKMCommonConst.DropInfoCounterCase);
					flag &= nkmlua.GetData("EC_FIELD", ref NKMCommonConst.DropInfoFieldLimit);
					flag &= nkmlua.GetData("EC_EVENT", ref NKMCommonConst.DropInfoEventLimit);
				}
				using (nkmlua.OpenTable("Dungeon", "[Dungeon] loading payback data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 377))
				{
					flag &= nkmlua.GetData("PaybackRatio", ref NKMCommonConst.DungeonPaybackRatio);
				}
				using (nkmlua.OpenTable("INVENTORY_AD_EXPAND", "[INVENTORY_AD_EXPAND] loading ad reward data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 382))
				{
					flag &= nkmlua.GetData("INVENTORY_UNIT_EXPAND_COUNT", ref NKMCommonConst.INVENTORY_UNIT_EXPAND_COUNT);
					flag &= nkmlua.GetData("INVENTORY_EQUIP_EXPAND_COUNT", ref NKMCommonConst.INVENTORY_EQUIP_EXPAND_COUNT);
					flag &= nkmlua.GetData("INVENTORY_SHIP_EXPAND_COUNT", ref NKMCommonConst.INVENTORY_SHIP_EXPAND_COUNT);
					flag &= nkmlua.GetData("INVENTORY_OPERATOR_EXPAND_COUNT", ref NKMCommonConst.INVENTORY_OPERATOR_EXPAND_COUNT);
				}
				using (nkmlua.OpenTable("GAME", "[GAME] loading game data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 390))
				{
					List<float> list;
					if (nkmlua.GetDataList("VALID_LAND_PVE", out list))
					{
						NKMCommonConst.VALID_LAND_PVE = list.ToArray();
					}
					else
					{
						Log.ErrorAndExit("[GAME/VALID_LAND_PVE] loading game data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 399);
					}
					if (nkmlua.GetDataList("VALID_LAND_PVP", out list))
					{
						NKMCommonConst.VALID_LAND_PVP = list.ToArray();
					}
					else
					{
						Log.ErrorAndExit("[GAME/VALID_LAND_PVP] loading game data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 408);
					}
					flag &= nkmlua.GetData("PVE_SUMMON_MIN_POS", ref NKMCommonConst.PVE_SUMMON_MIN_POS);
					flag &= nkmlua.GetData("PVP_SUMMON_MIN_POS", ref NKMCommonConst.PVP_SUMMON_MIN_POS);
					flag &= nkmlua.GetData("PVP_AFK_WARNING_TIME", ref NKMCommonConst.PVP_AFK_WARNING_TIME);
					flag &= nkmlua.GetData("PVP_AFK_AUTO_TIME", ref NKMCommonConst.PVP_AFK_AUTO_TIME);
					nkmlua.GetDataListEnum<NKM_GAME_TYPE>("PVP_AFK_APPLY_MODE", NKMCommonConst.PVP_AFK_APPLY_MODE, true);
					nkmlua.GetData("USE_ROLLBACK", ref NKMCommonConst.USE_ROLLBACK);
				}
				using (nkmlua.OpenTable("SHIP_LIMITBREAK", "[SHIP_LIMITBREAK] loading ship limitbreak data table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 422))
				{
					flag &= nkmlua.GetData("ShipLimitBreakItemCount", ref NKMCommonConst.ShipLimitBreakItemCount);
					flag &= nkmlua.GetData("ModuleReqItemCount", ref NKMCommonConst.ShipModuleReqItemCount);
				}
				using (nkmlua.OpenTable("RECALL", "[RECALL] loading recall data table failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 428))
				{
					nkmlua.GetData("RECALL_REWARD_UNIT_PIECE_TO_POINT", ref NKMCommonConst.RecallRewardUnitPieceToPoint);
				}
				using (nkmlua.OpenTable("STAGE_FAVORITE", "[STAGE_FAVORITE] loading favorite data table failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 433))
				{
					nkmlua.GetData("MAX_STAGE_FAVORITE_COUNT", ref NKMCommonConst.MaxStageFavoriteCount);
				}
				using (nkmlua.OpenTable("TACTIC_UPDATE", "[TACTIC_UPDATE] loading tactic update table failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 438))
				{
					nkmlua.GetData("TACTIC_UPDATE_RETURN_COUNT", ref NKMCommonConst.TacticReturnMaxCount);
					nkmlua.GetData("TACTIC_UPDATE_RETURN_DATE", ref NKMCommonConst.TacticReturnDateString);
				}
				NKMCommonConst.BackgroundInfo.Load(nkmlua);
			}
			if (!flag)
			{
				Log.ErrorAndExit("fail loading lua file:" + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 449);
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00065D5C File Offset: 0x00063F5C
		public static void Join()
		{
			NKMCommonConst.Negotiation.Join();
			NKMCommonConst.Guild.Join();
			NKMCommonConst.Office.Join();
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00065D7C File Offset: 0x00063F7C
		public static void Validate()
		{
			NKMCommonConst.Negotiation.Validate();
			NKMCommonConst.Guild.Validate();
			NKMCommonConst.Office.Validate();
			NKMCommonConst.Deck.Validate();
			NKMRewardMultiplyTemplet.Validate();
			if (NKMItemManager.GetItemMiscTempletByID(NKMCommonConst.EQUIP_PRESET_EXPAND_COST_ITEM_ID) == null)
			{
				Log.ErrorAndExit(string.Format("[EquipPreset] invalid cost itemId:{0}", NKMCommonConst.EQUIP_PRESET_EXPAND_COST_ITEM_ID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 471);
			}
			if (NKMCommonConst.EQUIP_PRESET_EXPAND_COST_VALUE < 0)
			{
				Log.ErrorAndExit(string.Format("[EquipPreset] invalid cost count:{0}", NKMCommonConst.EQUIP_PRESET_EXPAND_COST_VALUE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 476);
			}
			if (NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH < 0 || NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH > 15)
			{
				Log.ErrorAndExit(string.Format("[EquipPreset] invalid name Max Length. input:{0}. DB coverage:1~15", NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 481);
			}
			if (NKMCommonConst.RECHARGE_TIME.TotalSeconds <= 0.0)
			{
				Log.ErrorAndExit("[LUA_COMMON_CONST] Invalid Eternium Recharge Time.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 486);
			}
			if (NKMCommonConst.ExtractBonusRatePercent_Awaken < 0 || NKMCommonConst.ExtractBonusRatePercent_SSR < 0 || NKMCommonConst.ExtractBonusRatePercent_SR < 0)
			{
				Log.ErrorAndExit(string.Format("[LUA_COMMON_CONST] 유닛 추출 보너스 퍼센트(ExtractBonusRatePercent)가 잘못되었습니다. Awaken:{0} SSR:{1}, SR:{2}", NKMCommonConst.ExtractBonusRatePercent_Awaken, NKMCommonConst.ExtractBonusRatePercent_SSR, NKMCommonConst.ExtractBonusRatePercent_SR), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 493);
			}
			if (NKMOpenTagManager.IsOpened(NKMCommonConst.ImprintOpenTag))
			{
				NKMCommonConst.ImprintMainOptEffectWeapon.Validate();
				NKMCommonConst.ImprintMainOptEffectDefence.Validate();
				NKMCommonConst.ImprintMainOptEffectAccessary.Validate();
				if (NKMItemManager.GetItemMiscTempletByID(NKMCommonConst.ImprintCostItemId) == null)
				{
					NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 장비 각인 아이템 아이디가 잘못 되었습니다. ImprintCostItemId:{0}", NKMCommonConst.ImprintCostItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 504);
				}
				if (NKMCommonConst.ImprintCostItemCount <= 0)
				{
					NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 장비 각인 아이템 개수가 잘못 되었습니다. ImprintCostItemCount:{0}", NKMCommonConst.ImprintCostItemCount), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 509);
				}
				if (NKMItemManager.GetItemMiscTempletByID(NKMCommonConst.ReImprintCostItemId) == null)
				{
					NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 장비 재각인 아이템 아이디가 잘못 되었습니다. ReImprintCostItemId:{0}", NKMCommonConst.ReImprintCostItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 514);
				}
				if (NKMCommonConst.ReImprintCostItemCount <= 0)
				{
					NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 장비 재각인 아이템 개수가 잘못 되었습니다. ReImprintCostItemCount:{0}", NKMCommonConst.ReImprintCostItemCount), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 519);
				}
			}
			if (NKMCommonConst.DungeonPaybackRatio <= 0f || NKMCommonConst.DungeonPaybackRatio > 1f)
			{
				NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 이터니움 환급 비율이 잘못되었습니다. Dungeon/PaybackRatio:{0}", NKMCommonConst.DungeonPaybackRatio), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 525);
			}
			if (NKMItemMiscTemplet.Find(NKMCommonConst.SkipCostMiscItemId) == null)
			{
				Log.ErrorAndExit(string.Format("[LUA_COMMON_CONST] 스킵 비용 misc id가 잘못되었습니다. miscId:{0}", NKMCommonConst.SkipCostMiscItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 530);
			}
			if (NKMCommonConst.SkipCostMiscItemCount != 0)
			{
				Log.ErrorAndExit(string.Format("[LUA_COMMON_CONST] 스킵 비용이 0이 아닙니다. Count:{0}", NKMCommonConst.SkipCostMiscItemId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 535);
			}
			if (NKMCommonConst.DiveRepairHpRate < 0f || NKMCommonConst.DiveRepairHpRate > 100f)
			{
				NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 다이브 함선 수리 비율이 0 이하거나 100 이상입니다. Rate:{0}", NKMCommonConst.DiveRepairHpRate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 540);
			}
			if (NKMCommonConst.DiveStormHpRate < -100f || NKMCommonConst.DiveStormHpRate > 0f)
			{
				NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 다이브 함선 체력 감소 비율이 -100 이하거나 0 이상입니다. Rate:{0}", NKMCommonConst.DiveStormHpRate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 545);
			}
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000660A7 File Offset: 0x000642A7
		public static NKMCommonConst.ImprintMainOptEffect GetEquipIimprintMainOptEffect(ITEM_EQUIP_POSITION equipPosition)
		{
			switch (equipPosition)
			{
			case ITEM_EQUIP_POSITION.IEP_WEAPON:
				return NKMCommonConst.ImprintMainOptEffectWeapon;
			case ITEM_EQUIP_POSITION.IEP_DEFENCE:
				return NKMCommonConst.ImprintMainOptEffectDefence;
			case ITEM_EQUIP_POSITION.IEP_ACC:
			case ITEM_EQUIP_POSITION.IEP_ACC2:
				return NKMCommonConst.ImprintMainOptEffectAccessary;
			default:
				return new NKMCommonConst.ImprintMainOptEffect(1f, 1f);
			}
		}

		// Token: 0x040010F2 RID: 4338
		public const float m_fDELTA_TIME_FACTOR_1 = 1.1f;

		// Token: 0x040010F3 RID: 4339
		public const float m_fDELTA_TIME_FACTOR_2 = 1.5f;

		// Token: 0x040010F4 RID: 4340
		public const float m_fDELTA_TIME_FACTOR_05 = 0.6f;

		// Token: 0x040010F5 RID: 4341
		public static bool USE_ROLLBACK = false;

		// Token: 0x040010F6 RID: 4342
		public static float SUMMON_UNIT_NOEVENT_TIME = 0.5f;

		// Token: 0x040010F7 RID: 4343
		public const string ReplayFormatVersion = "RV004";

		// Token: 0x040010F8 RID: 4344
		public static int ENHANCE_CREDIT_COST_PER_UNIT = 300;

		// Token: 0x040010F9 RID: 4345
		public static float ENHANCE_CREDIT_COST_FACTOR = 0.05f;

		// Token: 0x040010FA RID: 4346
		public static float ENHANCE_EXP_BONUS_FACTOR = 0.05f;

		// Token: 0x040010FD RID: 4349
		public static float DiveRepairHpRate;

		// Token: 0x040010FE RID: 4350
		public static float DiveStormHpRate;

		// Token: 0x04001107 RID: 4359
		public static float OPERATOR_SKILL_STOP_TIME = 1f;

		// Token: 0x04001108 RID: 4360
		public static float OPERATOR_SKILL_DELAY_START_TIME = 1f;

		// Token: 0x04001109 RID: 4361
		public static int EQUIP_PRESET_BASIC_COUNT = 20;

		// Token: 0x0400110A RID: 4362
		public static int EQUIP_PRESET_MAX_COUNT = 100;

		// Token: 0x0400110B RID: 4363
		public static int EQUIP_PRESET_EXPAND_COST_ITEM_ID = 101;

		// Token: 0x0400110C RID: 4364
		public static int EQUIP_PRESET_EXPAND_COST_VALUE = 50;

		// Token: 0x0400110D RID: 4365
		public static int EQUIP_PRESET_NAME_MAX_LENGTH = 15;

		// Token: 0x0400110E RID: 4366
		public static int FeaturedListExhibitCount = 4;

		// Token: 0x0400110F RID: 4367
		public static double FeaturedListTotalPaymentThreshold = 119000.0;

		// Token: 0x04001110 RID: 4368
		public static TimeSpan RECHARGE_TIME;

		// Token: 0x04001111 RID: 4369
		public static int EVENT_RACE_PLAY_COUNT = 3;

		// Token: 0x04001112 RID: 4370
		public static int SkipCostMiscItemId = 3;

		// Token: 0x04001113 RID: 4371
		public static int SkipCostMiscItemCount = 0;

		// Token: 0x04001114 RID: 4372
		public static int MaxExtractUnitSelect = 5;

		// Token: 0x04001115 RID: 4373
		public static int ExtractBonusRatePercent_Awaken = 80;

		// Token: 0x04001116 RID: 4374
		public static int ExtractBonusRatePercent_SSR = 20;

		// Token: 0x04001117 RID: 4375
		public static int ExtractBonusRatePercent_SR = 5;

		// Token: 0x04001118 RID: 4376
		public static int RearmamentCostItemCount = 5;

		// Token: 0x04001119 RID: 4377
		public static int RearmamentMaxGrade = 5;

		// Token: 0x0400111A RID: 4378
		public static NKMCommonConst.ImprintMainOptEffect ImprintMainOptEffectWeapon;

		// Token: 0x0400111B RID: 4379
		public static NKMCommonConst.ImprintMainOptEffect ImprintMainOptEffectDefence;

		// Token: 0x0400111C RID: 4380
		public static NKMCommonConst.ImprintMainOptEffect ImprintMainOptEffectAccessary;

		// Token: 0x0400111D RID: 4381
		public static int ImprintCostItemId = 1;

		// Token: 0x0400111E RID: 4382
		public static int ImprintCostItemCount = 300000;

		// Token: 0x0400111F RID: 4383
		public static int ReImprintCostItemId = 1;

		// Token: 0x04001120 RID: 4384
		public static int ReImprintCostItemCount = 500000;

		// Token: 0x04001121 RID: 4385
		public static string ImprintOpenTag = "EQUIP_IMPRINT";

		// Token: 0x04001122 RID: 4386
		public static string ConvertTitle;

		// Token: 0x04001123 RID: 4387
		public static string ConvertMessage;

		// Token: 0x04001124 RID: 4388
		public static string DeleteTitle;

		// Token: 0x04001125 RID: 4389
		public static string DeleteMessage;

		// Token: 0x04001126 RID: 4390
		public static int DropInfoShopLimit = 10;

		// Token: 0x04001127 RID: 4391
		public static int DropInfoWorldMapMissionLimit = 10;

		// Token: 0x04001128 RID: 4392
		public static int DropInfoRaidLimit = 10;

		// Token: 0x04001129 RID: 4393
		public static int DropInfoShadowPalace = 10;

		// Token: 0x0400112A RID: 4394
		public static int DropInfoDiveLimit = 10;

		// Token: 0x0400112B RID: 4395
		public static int DropInfoFiercePointReward = 10;

		// Token: 0x0400112C RID: 4396
		public static int DropInfoRandomMoldBox = 10;

		// Token: 0x0400112D RID: 4397
		public static int DropInfoUnitDismiss = 10;

		// Token: 0x0400112E RID: 4398
		public static int DropInfoUnitExtract = 10;

		// Token: 0x0400112F RID: 4399
		public static int DropInfoMainStreamLimit = 10;

		// Token: 0x04001130 RID: 4400
		public static int DropInfoSupplyLimit = 10;

		// Token: 0x04001131 RID: 4401
		public static int DropInfoDailyLimit = 10;

		// Token: 0x04001132 RID: 4402
		public static int DropInfoSideStoryLimit = 10;

		// Token: 0x04001133 RID: 4403
		public static int DropInfoChallengeLimit = 10;

		// Token: 0x04001134 RID: 4404
		public static int DropInfoCounterCase = 10;

		// Token: 0x04001135 RID: 4405
		public static int DropInfoFieldLimit = 10;

		// Token: 0x04001136 RID: 4406
		public static int DropInfoEventLimit = 10;

		// Token: 0x04001137 RID: 4407
		public static int DropInfoTrimDungeon = 10;

		// Token: 0x04001138 RID: 4408
		public static int DropInfoSubStreamShop = 50;

		// Token: 0x04001139 RID: 4409
		public static float DungeonPaybackRatio = 0.5f;

		// Token: 0x0400113A RID: 4410
		public static int INVENTORY_UNIT_EXPAND_COUNT = 0;

		// Token: 0x0400113B RID: 4411
		public static int INVENTORY_EQUIP_EXPAND_COUNT = 0;

		// Token: 0x0400113C RID: 4412
		public static int INVENTORY_SHIP_EXPAND_COUNT = 0;

		// Token: 0x0400113D RID: 4413
		public static int INVENTORY_OPERATOR_EXPAND_COUNT = 0;

		// Token: 0x0400113E RID: 4414
		public static float[] VALID_LAND_PVE = new float[]
		{
			0.4f,
			0.6f,
			0.8f
		};

		// Token: 0x0400113F RID: 4415
		public static float[] VALID_LAND_PVP = new float[]
		{
			0.4f,
			0.6f,
			0.8f
		};

		// Token: 0x04001140 RID: 4416
		public static float PVE_SUMMON_MIN_POS = 0f;

		// Token: 0x04001141 RID: 4417
		public static float PVP_SUMMON_MIN_POS = 250f;

		// Token: 0x04001142 RID: 4418
		public static float PVP_AFK_WARNING_TIME = 5f;

		// Token: 0x04001143 RID: 4419
		public static float PVP_AFK_AUTO_TIME = 10f;

		// Token: 0x04001144 RID: 4420
		public static HashSet<NKM_GAME_TYPE> PVP_AFK_APPLY_MODE = new HashSet<NKM_GAME_TYPE>();

		// Token: 0x04001145 RID: 4421
		public static int ShipLimitBreakItemCount = 3;

		// Token: 0x04001146 RID: 4422
		public static int ShipModuleReqItemCount = 4;

		// Token: 0x04001147 RID: 4423
		public static int ShipCmdModuleSlotCount = 2;

		// Token: 0x04001148 RID: 4424
		public static int ShipCmdModuleCount = 3;

		// Token: 0x04001149 RID: 4425
		public static float RecallRewardUnitPieceToPoint = 16.67f;

		// Token: 0x0400114A RID: 4426
		public static int MaxStageFavoriteCount = 30;

		// Token: 0x0400114B RID: 4427
		public static int TacticReturnMaxCount = 50;

		// Token: 0x0400114C RID: 4428
		public static string TacticReturnDateString = "DATE_TACTIC_UPDATE_RETURN";

		// Token: 0x020011AF RID: 4527
		public readonly struct ImprintMainOptEffect
		{
			// Token: 0x0600A063 RID: 41059 RVA: 0x0033DE7F File Offset: 0x0033C07F
			public ImprintMainOptEffect(float mainStatMultiply, float subStatMultiply)
			{
				this.MainStatMultiply = mainStatMultiply;
				this.SubStatMultiply = subStatMultiply;
			}

			// Token: 0x17001791 RID: 6033
			// (get) Token: 0x0600A064 RID: 41060 RVA: 0x0033DE8F File Offset: 0x0033C08F
			public float MainStatMultiply { get; }

			// Token: 0x17001792 RID: 6034
			// (get) Token: 0x0600A065 RID: 41061 RVA: 0x0033DE97 File Offset: 0x0033C097
			public float SubStatMultiply { get; }

			// Token: 0x0600A066 RID: 41062 RVA: 0x0033DE9F File Offset: 0x0033C09F
			public float GetMultiplyValue(bool isMainStat)
			{
				if (!isMainStat)
				{
					return this.SubStatMultiply;
				}
				return this.MainStatMultiply;
			}

			// Token: 0x0600A067 RID: 41063 RVA: 0x0033DEB4 File Offset: 0x0033C0B4
			public void Validate()
			{
				if (this.MainStatMultiply < 1f)
				{
					NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 장비 각인 계수가 잘못 되었습니다. MainStatMultiply:{0}", this.SubStatMultiply), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 585);
				}
				if (this.SubStatMultiply < 1f)
				{
					NKMTempletError.Add(string.Format("[LUA_COMMON_CONST] 장비 각인 계수가 잘못 되었습니다. SubStatMultiply:{0}", this.SubStatMultiply), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonConst.cs", 590);
				}
			}
		}
	}
}

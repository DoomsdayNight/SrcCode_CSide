using System;
using System.Collections.Generic;
using System.Linq;
using AssetBundles;
using Cs.Engine.Util;
using Cs.Logging;
using NKC.Loading;
using NKC.Localization;
using NKC.Templet;
using NKM;
using NKM.Contract2;
using NKM.Event;
using NKM.EventPass;
using NKM.Guild;
using NKM.Item;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using NKM.Templet.Recall;
using NKM.Unit;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000699 RID: 1689
	public static class NKCMain
	{
		// Token: 0x06003799 RID: 14233 RVA: 0x0011E300 File Offset: 0x0011C500
		private static void NKCInitLocalContentsVersion()
		{
			string @string = PlayerPrefs.GetString("LOCAL_SAVE_CONTENTS_VERSION_KEY");
			Log.Info("NKCInitLocalContentsVersion LocalCV[" + @string + "], Key[LOCAL_SAVE_CONTENTS_VERSION_KEY]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCMain.cs", 37);
			if (!string.IsNullOrEmpty(@string))
			{
				NKMContentsVersion nkmcontentsVersion = NKMContentsVersion.Create(@string);
				if (nkmcontentsVersion != null && NKMContentsVersionManager.CurrentVersion < nkmcontentsVersion)
				{
					Log.Info("Created LocalContentsVersion LocalCV[" + @string + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCMain.cs", 48);
					NKMContentsVersionManager.SetCurrent(@string);
				}
			}
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x0011E37C File Offset: 0x0011C57C
		public static void InvalidateSafeMode()
		{
			NKCMain.m_ranAsSafeMode = false;
			NKCContentsVersionManager.s_DefaultTagLoaded = false;
			PlayerPrefs.DeleteKey("NKC_SAFE_MODE_KEY");
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x0011E394 File Offset: 0x0011C594
		public static bool IsSafeMode()
		{
			return NKCDefineManager.DEFINE_CAN_ONLY_LOAD_MIN_TEMPLET() && (NKCContentsVersionManager.s_DefaultTagLoaded || (NKCDefineManager.DEFINE_CHECKVERSION() && !ContentsVersionChecker.VersionAckReceived) || PlayerPrefs.GetInt("NKC_SAFE_MODE_KEY", 0) == 1);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x0011E3CC File Offset: 0x0011C5CC
		public static void NKCInit()
		{
			List<string> list = new List<string>();
			NKMDataVersion.LoadFromLUA();
			NKMContentsVersionManager.LoadDefaultVersion();
			if (NKCDefineManager.DEFINE_UNITY_EDITOR())
			{
				NKCContentsVersionManager.TryRecoverTag();
			}
			NKCMain.NKCInitLocalContentsVersion();
			if (NKCDefineManager.DEFINE_UNITY_EDITOR())
			{
				NKM_NATIONAL_CODE currentNationalCode = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_END);
				NKC_VOICE_CODE nkc_VOICE_CODE = NKCUIVoiceManager.LoadLocalVoiceCode();
				NKCUIVoiceManager.SetVoiceCode(nkc_VOICE_CODE);
				AssetBundleManager.ActiveVariants = NKCLocalization.GetVariants(currentNationalCode, nkc_VOICE_CODE);
			}
			NKMCommonConst.LoadFromLUA("LUA_COMMON_CONST");
			NKCClientConst.LoadFromLUA("LUA_CLIENT_CONST");
			NKMTempletContainer<NKCLoginBackgroundTemplet>.Load("AB_SCRIPT", "LUA_LOGIN_BACKGROUND", "LOGIN_BACKGROUND", new Func<NKMLua, NKCLoginBackgroundTemplet>(NKCLoginBackgroundTemplet.LoadFromLUA));
			if (NKCMain.IsSafeMode())
			{
				NKCMain.m_ranAsSafeMode = true;
				return;
			}
			NKMAnimDataManager.LoadFromLUA("LUA_ANIM_DATA");
			NKMShipSkillManager.LoadFromLUA("LUA_SHIP_SKILL_TEMPLET");
			NKMUnitSkillManager.LoadFromLUA("LUA_UNIT_SKILL_TEMPLET");
			NKMTacticalCommandManager.LoadFromLUA("LUA_TACTICAL_COMMAND_TEMPLET");
			NKMItemManager.LoadFromLUA_ITEM_MISC("LUA_ITEM_MISC_TEMPLET");
			NKMTempletContainer<NKMOfficeInteriorTemplet>.Load("AB_SCRIPT", "LUA_ITEM_INTERIOR_TEMPLET", "ITEM_INTERIOR_PREFAB", new Func<NKMLua, NKMOfficeInteriorTemplet>(NKMOfficeInteriorTemplet.LoadFromLua));
			NKMOfficeInteriorTemplet.MergeContainer();
			NKMOfficeGradeTemplet.LoadFromLua();
			list.Clear();
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET2");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET3");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET4");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET5");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET6");
			NKMDETempletManager.LoadFromLUA(list, true);
			NKMCommonUnitEvent.LoadFromLUA("LUA_COMMON_UNIT_EVENT_HEAL", true);
			string[] fileNames = new string[]
			{
				"LUA_UNIT_TEMPLET_BASE",
				"LUA_UNIT_TEMPLET_BASE2"
			};
			NKMTempletContainer<NKMUnitTempletBase>.Load("AB_SCRIPT_UNIT_DATA", fileNames, "m_dicNKMUnitTempletBaseByStrID", new Func<NKMLua, NKMUnitTempletBase>(NKMUnitTempletBase.LoadFromLUA), (NKMUnitTempletBase e) => e.m_UnitStrID);
			fileNames = new string[]
			{
				"LUA_UNIT_STAT_TEMPLET",
				"LUA_UNIT_STAT_TEMPLET2"
			};
			NKMUnitManager.LoadFromLUA(fileNames, "", false);
			NKMSkinManager.LoadFromLua();
			NKMEquipTuningManager.LoadFromLUA_EquipRandomStat("LUA_ITEM_EQUIP_RANDOM_STAT");
			NKMItemManager.LoadFromLUA_EquipSetOption("LUA_ITEM_EQUIP_SET_OPTION");
			NKMItemManager.LoadFromLUA_Item_Equip("LUA_ITEM_EQUIP_TEMPLET");
			NKMItemManager.LoadFromLUA_EquipEnchantExp("LUA_EQUIP_ENCHANT_EXP_TABLE");
			NKMTempletContainer<NKMItemEquipUpgradeTemplet>.Load("AB_SCRIPT", "LUA_ITEM_EQUIP_UPGRADE", "ITEM_EQUIP_UPGRADE", new Func<NKMLua, NKMItemEquipUpgradeTemplet>(NKMItemEquipUpgradeTemplet.LoadFromLua));
			NKMItemManager.LoadFromLua_Item_Mold("LUA_ITEM_MOLD_TEMPLET");
			NKMItemManager.LoadFromLua_Random_Mold_Box("LUA_RANDOM_MOLD_BOX_CL");
			NKMItemManager.LoadFromLua_Item_Mold_Tab("LUA_ITEM_MOLD_TAB");
			NKMItemManager.LoadFromLua_Item_AutoWeight("LUA_EQUIP_AUTO_WEIGHT_CL");
			NKMRewardManager.LoadFromLUA("LUA_REWARD_TEMPLET_CL");
			NKMRandomGradeManager.LoadFromLUA("LUA_RANDOM_GRADE");
			NKMMapManager.LoadFromLUA("LUA_MAP_TEMPLET", false);
			NKMDungeonManager.LoadFromLUA_EventDeckInfo();
			NKMDungeonManager.LoadFromLUA("LUA_DUNGEON_TEMPLET_BASE", "", false);
			NKMTempletContainer<NKMWarfareTemplet>.Load("AB_SCRIPT_WARFARE", "LUA_WARFARE_TEMPLET", "m_dicNKMWarfareTemplet", new Func<NKMLua, NKMWarfareTemplet>(NKMWarfareTemplet.LoadFromLUA), (NKMWarfareTemplet e) => e.m_WarfareStrID);
			NKMTempletContainer<NKMDiveTemplet>.Load("AB_SCRIPT", "LUA_DIVE_TEMPLET", "DIVE_TEMPLET", new Func<NKMLua, NKMDiveTemplet>(NKMDiveTemplet.LoadFromLUA));
			NKMTempletContainer<NKMDiveArtifactTemplet>.Load("AB_SCRIPT", "LUA_DIVE_ARTIFACT", "DIVE_ARTIFACT", new Func<NKMLua, NKMDiveArtifactTemplet>(NKMDiveArtifactTemplet.LoadFromLUA));
			NKMEpisodeMgr.LoadFromLUA("LUA_STAGE_TEMPLET");
			NKMTempletContainer<NKMEpisodeTempletV2>.Load("AB_SCRIPT", "LUA_EPISODE_TEMPLET_V2", "EPISODE_TEMPLET_V2", new Func<NKMLua, NKMEpisodeTempletV2>(NKMEpisodeTempletV2.LoadFromLUA));
			NKMEpisodeMgr.LoadClientOnlyData();
			NKMTempletContainer<NKMEpisodeGroupTemplet>.Load("AB_SCRIPT", "LUA_EPISODE_GROUP_TEMPLET", "EPISODE_GROUP_TEMPLET", new Func<NKMLua, NKMEpisodeGroupTemplet>(NKMEpisodeGroupTemplet.LoadFromLUA));
			NKMTempletContainer<NKCEpisodeSummaryTemplet>.Load("AB_SCRIPT", "LUA_EPISODE_SUMMARY_TEMPLET", "EPISODE_SUMMARY_TEMPLET", new Func<NKMLua, NKCEpisodeSummaryTemplet>(NKCEpisodeSummaryTemplet.LoadFromLua));
			list.Clear();
			list.Add("LUA_DAMAGE_TEMPLET");
			list.Add("LUA_DAMAGE_TEMPLET2");
			list.Add("LUA_DAMAGE_TEMPLET3");
			list.Add("LUA_DAMAGE_TEMPLET4");
			list.Add("LUA_DAMAGE_TEMPLET5");
			list.Add("LUA_DAMAGE_TEMPLET6");
			fileNames = new string[]
			{
				"LUA_DAMAGE_TEMPLET_BASE",
				"LUA_DAMAGE_TEMPLET_BASE2",
				"LUA_DAMAGE_TEMPLET_BASE3",
				"LUA_DAMAGE_TEMPLET_BASE4",
				"LUA_DAMAGE_TEMPLET_BASE5",
				"LUA_DAMAGE_TEMPLET_BASE6"
			};
			NKMDamageManager.LoadFromLUA(fileNames, list);
			NKMBuffManager.LoadFromLUA();
			NKMBuffTemplet.ParseAllSkinDic();
			NKMTempletContainer<NKMCompanyBuffTemplet>.Load("AB_SCRIPT", "LUA_COMPANY_BUFF_TEMPLET", "COMPANY_BUFF_TEMPLET", new Func<NKMLua, NKMCompanyBuffTemplet>(NKMCompanyBuffTemplet.LoadFromLua));
			NKMTempletContainer<NKMUserExpTemplet>.Load("AB_SCRIPT", "LUA_PLAYER_EXP_TABLE", "m_PlayerExpTable", new Func<NKMLua, NKMUserExpTemplet>(NKMUserExpTemplet.LoadFromLUA));
			NKMUnitExpTableContainer.LoadFromLua(false);
			NKMUnitLimitBreakManager.LoadFromLua("LUA_LIMITBREAK_INFO", "LUA_LIMITBREAK_SUBSTITUTE_ITEM");
			NKMWorldMapManager.LoadFromLUA();
			NKMTempletContainer<NKMShipBuildTemplet>.Load("AB_SCRIPT", "LUA_SHIP_BUILD_TEMPLET", "SHIP_BUILD_TEMPLET", new Func<NKMLua, NKMShipBuildTemplet>(NKMShipBuildTemplet.LoadFromLUA));
			NKMShipLevelUpTemplet.LoadFromLua("LUA_SHIP_LEVELUP_TEMPLET", "SHIP_LEVELUP_TEMPLET");
			ShopTabTempletContainer.Load();
			NKMTempletLoader.Load<ShopItemTemplet>("AB_SCRIPT", "m_ShopTable", new Func<NKMLua, ShopItemTemplet>(ShopItemTemplet.LoadFromLUA), new string[]
			{
				"LUA_SHOP_TEMPLET_01",
				"LUA_SHOP_TEMPLET_02"
			});
			NKCRandomBoxManager.LoadFromLUA();
			NKMTempletContainer<NKCContractCategoryTemplet>.Load("AB_SCRIPT", "LUA_CONTRACT_CATEGORY", "CONTRACT_CATEGORY", new Func<NKMLua, NKCContractCategoryTemplet>(NKCContractCategoryTemplet.LoadFromLUA));
			ContractTempletLoader.Load();
			NKMMissionManager.LoadFromLUA("LUA_MISSION_TEMPLET", "LUA_MISSION_TAB_TEMPLET");
			NKMUnitStatManager.LoadFromLua();
			NKMAttendanceManager.LoadFromLua();
			NKCNewsManager.LoadFromLua();
			NKMBattleConditionManager.LoadFromLua();
			NKMPvpCommonConst.LoadFromLua();
			NKCPVPManager.LoadFromLua();
			NKMEventManager.LoadFromLua();
			NKMTempletContainer<NKMCollectionTeamUpGroupTemplet>.Load(from e in NKMTempletLoader<NKMCollectionTeamUpTemplet>.LoadGroup("AB_SCRIPT", "LUA_COLLECTION_TEAMUP_TEMPLET", "COLLECTION_TEAMUP_TEMPLET", new Func<NKMLua, NKMCollectionTeamUpTemplet>(NKMCollectionTeamUpTemplet.LoadFromLUA))
			select new NKMCollectionTeamUpGroupTemplet(e.Key, e.Value), null);
			NKMTempletContainer<NKMRaidTemplet>.Load("AB_SCRIPT", "LUA_RAID_TEMPLET", "m_RaidTemplet", new Func<NKMLua, NKMRaidTemplet>(NKMRaidTemplet.LoadFromLUA));
			NKMTempletContainer<NKMRaidBuffTemplet>.Load("AB_SCRIPT", "LUA_RAID_BUFF_TEMPLET", "m_RaidBuffTemplet", new Func<NKMLua, NKMRaidBuffTemplet>(NKMRaidBuffTemplet.LoadFromLUA));
			NKMTempletContainer<NKMContentUnlockTemplet>.Load("AB_SCRIPT", "LUA_CONTENTS_UNLOCK_TEMPLET", "CONTENTS_UNLOCK_TEMPLET", new Func<NKMLua, NKMContentUnlockTemplet>(NKMContentUnlockTemplet.LoadFromLUA));
			NKCFilterManager.LoadFromLua();
			NKCTutorialManager.LoadFromLua();
			NKMTempletContainer<NKCCollectionVoiceTemplet>.Load("AB_SCRIPT", "LUA_COLLECTION_VOICE_TEMPLET", "COLLECTION_VOICE_TEMPLET", new Func<NKMLua, NKCCollectionVoiceTemplet>(NKCCollectionVoiceTemplet.LoadLua));
			NKMTempletContainer<NKMEmoticonTemplet>.Load("AB_SCRIPT_ITEM_TEMPLET", "LUA_ITEM_EMOTICON_TEMPLET", "EMOTICON_MANAGER", new Func<NKMLua, NKMEmoticonTemplet>(NKMEmoticonTemplet.LoadFromLUA));
			NKMTempletContainer<NKCCurrencyTemplet>.Load("AB_SCRIPT", "LUA_CURRENCY_TEMPLET", "CURRENCY_TEMPLET", new Func<NKMLua, NKCCurrencyTemplet>(NKCCurrencyTemplet.LoadFromLua), (NKCCurrencyTemplet e) => e.m_Code);
			NKMTempletContainer<NKCLobbyIconTemplet>.Load("AB_SCRIPT", "LUA_LOBBY_ICON_TEMPLET", "LOBBY_ICON_TEMPLET", new Func<NKMLua, NKCLobbyIconTemplet>(NKCLobbyIconTemplet.LoadFromLUA));
			NKMTempletContainer<NKCShopBannerTemplet>.Load("AB_SCRIPT", "LUA_SHOP_BANNER_TEMPLET", "SHOP_BANNER_TEMPLET", new Func<NKMLua, NKCShopBannerTemplet>(NKCShopBannerTemplet.LoadFromLUA));
			NKCShopCategoryTemplet.Load();
			NKMTempletContainer<NKMEventTabTemplet>.Load("AB_SCRIPT", "LUA_EVENT_TAB_TEMPLET", "EVENT_TAB_TEMPLET", new Func<NKMLua, NKMEventTabTemplet>(NKMEventTabTemplet.LoadFromLUA));
			NKMTempletContainer<NKCEventMissionTemplet>.Load("AB_SCRIPT", "LUA_EVENT_MISSION_TEMPLET", "EVENT_MISSION_TEMPLET", new Func<NKMLua, NKCEventMissionTemplet>(NKCEventMissionTemplet.LoadFromLUA));
			NKMTempletContainer<NKMPieceTemplet>.Load("AB_SCRIPT_ITEM_TEMPLET", "LUA_PIECE_TEMPLET", "PIECE_TEMPLET", new Func<NKMLua, NKMPieceTemplet>(NKMPieceTemplet.LoadFromLUA));
			NKMCommonConst.Join();
			NKMTempletContainer<NKCBackgroundTemplet>.Load("AB_SCRIPT_ITEM_TEMPLET", "LUA_ITEM_BACKGROUND_PREFAB", "ITEM_BACKGROUND_PREFAB", new Func<NKMLua, NKCBackgroundTemplet>(NKCBackgroundTemplet.LoadFromLUA));
			NKCLoginCutSceneManager.LoadFromLua();
			NKMShadowPalaceManager.LoadFromLua();
			NKMTempletContainer<NKMLeaderBoardTemplet>.Load("AB_SCRIPT", "LUA_LEADERBOARD_TEMPLET", "LEADERBOARD_TEMPLET", new Func<NKMLua, NKMLeaderBoardTemplet>(NKMLeaderBoardTemplet.LoadFromLUA));
			NKMTempletContainer<NKMGuildBadgeColorTemplet>.Load("AB_SCRIPT", "LUA_GUILD_BADGE_COLOR_TEMPLET", "GUILD_BADGE_COLOR_TEMPLET", new Func<NKMLua, NKMGuildBadgeColorTemplet>(NKMGuildBadgeColorTemplet.LoadFromLUA));
			NKMTempletContainer<NKMGuildBadgeFrameTemplet>.Load("AB_SCRIPT", "LUA_GUILD_BADGE_FRAME_TEMPLET", "GUILD_BADGE_FRAME_TEMPLET", new Func<NKMLua, NKMGuildBadgeFrameTemplet>(NKMGuildBadgeFrameTemplet.LoadFromLUA));
			NKMTempletContainer<NKMGuildBadgeMarkTemplet>.Load("AB_SCRIPT", "LUA_GUILD_BADGE_MARK_TEMPLET", "GUILD_BADGE_MARK_TEMPLET", new Func<NKMLua, NKMGuildBadgeMarkTemplet>(NKMGuildBadgeMarkTemplet.LoadFromLUA));
			NKCFierceBattleSupportDataMgr.LoadFromLua();
			NKMTempletContainer<GuildExpTemplet>.Load("AB_SCRIPT", "LUA_GUILD_EXP_TEMPLET", "GUILD_EXP_TEMPLET", new Func<NKMLua, GuildExpTemplet>(GuildExpTemplet.LoadFromLua));
			GuildAttendanceTemplet.LoadFromLua();
			NKMTempletContainer<GuildDonationTemplet>.Load("AB_SCRIPT", "LUA_GUILD_DONATION_TEMPLET", "GUILD_DONATION_TEMPLET", new Func<NKMLua, GuildDonationTemplet>(GuildDonationTemplet.LoadFromLua));
			NKMTempletContainer<GuildWelfareTemplet>.Load("AB_SCRIPT", "LUA_GUILD_WELFARE_TEMPLET", "GUILD_WELFARE_TEMPLET", new Func<NKMLua, GuildWelfareTemplet>(GuildWelfareTemplet.LoadFromLua));
			NKMTempletContainer<NKMMentoringTemplet>.Load("AB_SCRIPT", "LUA_MENTORING_SEASON_TEMPLET", "MENTORING_SEASON_TEMPLET", new Func<NKMLua, NKMMentoringTemplet>(NKMMentoringTemplet.LoadFromLua));
			NKMTempletContainer<NKMMentoringRewardTemplet>.Load("AB_SCRIPT", "LUA_MENTORING_SEASON_REWARD_TEMPLET", "MENTORING_SEASON_REWARD_TEMPLET", new Func<NKMLua, NKMMentoringRewardTemplet>(NKMMentoringRewardTemplet.LoadFromLua));
			NKCLoadingScreenManager.LoadFromLua();
			NKMTempletContainer<GuildSeasonTemplet>.Load("AB_SCRIPT", "LUA_GUILD_SEASON_TEMPLET", "GUILD_SEASON_TEMPLET", new Func<NKMLua, GuildSeasonTemplet>(GuildSeasonTemplet.LoadFromLua));
			GuildDungeonTempletManager.LoadFromLua();
			NKMTempletContainer<NKMLobbyFaceTemplet>.Load("AB_SCRIPT", "LUA_LOBBY_FACE_TEMPLET", "m_LobbyFaceTemplet", new Func<NKMLua, NKMLobbyFaceTemplet>(NKMLobbyFaceTemplet.LoadFromLUA));
			NKMCustomPackageGroupTemplet.LoadFromLua();
			NKMTempletContainer<NKMEventPassTemplet>.Load("AB_SCRIPT", "LUA_EVENT_PASS_TEMPLET", "EVENT_PASS_TEMPLET", new Func<NKMLua, NKMEventPassTemplet>(NKMEventPassTemplet.LoadFromLUA));
			NKMTempletContainer<NKMEventPassRewardTemplet>.Load("AB_SCRIPT", "LUA_EVENT_PASS_REWARD_TEMPLET", "EVENT_PASS_REWARD_TEMPLET", new Func<NKMLua, NKMEventPassRewardTemplet>(NKMEventPassRewardTemplet.LoadFromLUA));
			NKMTempletContainer<NKMEventPassMissionGroupTemplet>.Load("AB_SCRIPT", "LUA_EVENT_PASS_MISSION_GROUP_TEMPLET", "EVENT_PASS_MISSION_GROUP_TEMPLET", new Func<NKMLua, NKMEventPassMissionGroupTemplet>(NKMEventPassMissionGroupTemplet.LoadFromLUA));
			NKMTempletContainer<NKMOperatorExpTemplet>.Load(from e in NKMTempletLoader<NKMOperatorExpData>.LoadGroup("AB_SCRIPT_UNIT_DATA", "LUA_OPERATOR_EXP_TEMPLET", "m_OperatorExpTable", new Func<NKMLua, NKMOperatorExpData>(NKMOperatorExpData.LoadFromLua))
			select new NKMOperatorExpTemplet((NKM_UNIT_GRADE)e.Key, e.Value), null);
			NKMTempletContainer<NKMOperatorSkillTemplet>.Load("AB_SCRIPT_UNIT_DATA", "LUA_OPERATOR_SKILL_TEMPLET", "m_OperatorSkillTable", new Func<NKMLua, NKMOperatorSkillTemplet>(NKMOperatorSkillTemplet.LoadFromLua), (NKMOperatorSkillTemplet e) => e.m_OperSkillStrID);
			NKMTempletContainer<NKMOperatorRandomPassiveGroupTemplet>.Load(from e in NKMTempletLoader<NKMOperatorRandomPassiveTemplet>.LoadGroup("AB_SCRIPT_UNIT_DATA", "LUA_OPERATOR_RANDOM_PASSIVE_TEMPLET", "m_OperatorRandomPassiveTable", new Func<NKMLua, NKMOperatorRandomPassiveTemplet>(NKMOperatorRandomPassiveTemplet.LoadFromLua))
			select new NKMOperatorRandomPassiveGroupTemplet(e.Key, e.Value), null);
			NKMTempletContainer<NKCStatInfoTemplet>.Load("AB_SCRIPT", "LUA_STAT_INFO_TEMPLET", "STAT_INFO_TEMPLET", new Func<NKMLua, NKCStatInfoTemplet>(NKCStatInfoTemplet.LoadFromLUA));
			NKCStatInfoTemplet.MakeGroups();
			NKMTempletContainer<NKMRecallTemplet>.Load("AB_SCRIPT", "LUA_RECALL_TEMPLET", "RECALL_UNIT_TEMPLET", new Func<NKMLua, NKMRecallTemplet>(NKMRecallTemplet.LoadFromLUA));
			NKMTempletContainer<NKMRecallUnitExchangeTemplet>.Load("AB_SCRIPT", "LUA_RECALL_EXCHANGE_UNIT_LIST", "RECALL_EXCHANGE_UNIT_LIST", new Func<NKMLua, NKMRecallUnitExchangeTemplet>(NKMRecallUnitExchangeTemplet.LoadFromLUA));
			NKMTempletContainer<NKMEventWechatCouponTemplet>.Load("AB_SCRIPT", "LUA_WECHAT_COUPON_TEMPLET", "WECHAT_COUPON_TEMPLET", new Func<NKMLua, NKMEventWechatCouponTemplet>(NKMEventWechatCouponTemplet.LoadFromLua));
			NKMTempletContainer<NKMPhaseTemplet>.Load("AB_SCRIPT", "LUA_PHASE_TEMPLET", "PHASE_TEMPLET", new Func<NKMLua, NKMPhaseTemplet>(NKMPhaseTemplet.LoadFromLua), (NKMPhaseTemplet e) => e.StrId);
			NKMPhaseGroupTemplet.LoadFromLua();
			NKMKillCountTemplet.LoadFromLua();
			NKMUnitMissionTemplet.LoadFromLua();
			NKCVoiceActorNameTemplet.LoadFromLua();
			NKCVoiceActorStringTemplet.LoadFromLua();
			NKMPotentialOptionGroupTemplet.LoadFromLua();
			NKCItemDropInfoTemplet.LoadFromLua();
			NKMTempletContainer<NKMRaidSeasonTemplet>.Load("AB_SCRIPT", "LUA_RAID_SEASON_TEMPLET", "RAID_SEASON_TEMPLET", new Func<NKMLua, NKMRaidSeasonTemplet>(NKMRaidSeasonTemplet.LoadFromLua));
			NKMTempletContainer<NKMRaidSeasonRewardTemplet>.Load("AB_SCRIPT", "LUA_RAID_SEASON_REWARD_TEMPLET", "REWARD_BOARD_TEMPLET", new Func<NKMLua, NKMRaidSeasonRewardTemplet>(NKMRaidSeasonRewardTemplet.LoadFromLua));
			NKMTempletContainer<NKMADTemplet>.Load("AB_SCRIPT", "LUA_AD_TEMPLET", "AD_TEMPLET", new Func<NKMLua, NKMADTemplet>(NKMADTemplet.LoadFromLua));
			NKMTrimIntervalTemplet.Load("AB_SCRIPT", "LUA_TRIM_INTERVAL");
			NKMTempletContainer<NKMTrimTemplet>.Load("AB_SCRIPT", "LUA_TRIM_TEMPLET", "TRIM_TEMPLET", new Func<NKMLua, NKMTrimTemplet>(NKMTrimTemplet.LoadFromLua));
			NKMTrimDungeonTemplet.Load("AB_SCRIPT", "LUA_TRIM_DUNGEON");
			NKMTrimCombatPenaltyTemplet.Load("AB_SCRIPT", "LUA_TRIM_COMBAT_PENALTY");
			NKMTrimPointTemplet.Load("AB_SCRIPT", "LUA_TRIM_POINT");
			NKCTrimRewardTemplet.Load("AB_SCRIPT", "LUA_TRIM_REWARD_CL");
			NKMEventCollectionTemplet.LoadFromLua();
			NKMEventCollectionMergeTemplet.LoadFromLua();
			NKMTempletContainer<NKMEventCollectionIndexTemplet>.Load("AB_SCRIPT", "LUA_EVENT_COLLECTION_INDEX_TEMPLET", "EVENT_COLLECTION_INDEX_TEMPLET", new Func<NKMLua, NKMEventCollectionIndexTemplet>(NKMEventCollectionIndexTemplet.LoadFromLua));
			NKMTempletContainer<NKCEventPaybackTemplet>.Load("AB_SCRIPT", "LUA_EVENT_PAYBACK_TEMPLET", "EVENT_PAYBACK_TEMPLET", new Func<NKMLua, NKCEventPaybackTemplet>(NKCEventPaybackTemplet.LoadFromLUA));
			NKMTempletContainer<NKCLobbyEventIndexTemplet>.Load("AB_SCRIPT", "LUA_EVENT_LOBBY_INDEX_TEMPLET", "EVENT_LOBBY_INDEX_TEMPLET", new Func<NKMLua, NKCLobbyEventIndexTemplet>(NKCLobbyEventIndexTemplet.LoadFromLUA));
			NKMTempletContainer<NKMPointExchangeTemplet>.Load("AB_SCRIPT", "LUA_POINTEXCHANGE_TEMPLET", "POINTEXCHANGE_TEMPLET", new Func<NKMLua, NKMPointExchangeTemplet>(NKMPointExchangeTemplet.LoadFromLua));
			NKCTempletContainerUtil.InvokeJoin();
			ShopTabTempletContainer.Join();
			NKMPvpCommonConst.Instance.Join();
			NKCPVPManager.Join();
			GuildDungeonTempletManager.Join();
			NKCLoginCutSceneManager.Join();
			if (NKMOpenTagManager.TagCount > 0)
			{
				NKCTempletContainerUtil.InvokeValidate();
			}
			NKMMissionManager.CheckValidation();
			NKMTempletContainer<NKMEventWechatCouponTemplet>.Join();
			NKCRearmamentUtil.Init();
			NKMTempletContainer<NKMShipLimitBreakTemplet>.Load("AB_SCRIPT", "LUA_SHIP_LIMITBREAK_TEMPLET", "SHIP_LIMITBREAK_TEMPLET", new Func<NKMLua, NKMShipLimitBreakTemplet>(NKMShipLimitBreakTemplet.LoadFromLUA));
			NKMShipModuleGroupTemplet.LoadFromLua();
			NKMTempletContainer<NKMShipCommandModuleTemplet>.Load("AB_SCRIPT", "LUA_COMMANDMODULE_TEMPLET", "COMMANDMODULE_TEMPLET", new Func<NKMLua, NKMShipCommandModuleTemplet>(NKMShipCommandModuleTemplet.LoadFromLUA));
			NKMTempletContainer<NKCMonsterTagTemplet>.Load("AB_SCRIPT", "LUA_MONSTER_TAG_TEMPLET", "MONSTER_TAG", new Func<NKMLua, NKCMonsterTagTemplet>(NKCMonsterTagTemplet.LoadFromLUA));
			NKMTempletContainer<NKCMonsterTagInfoTemplet>.Load("AB_SCRIPT", "LUA_MONSTER_TAG_INFO_TEMPLET", "MONSTER_TAG_INFO", new Func<NKMLua, NKCMonsterTagInfoTemplet>(NKCMonsterTagInfoTemplet.LoadFromLUA));
			NKMTempletContainer<NKCBGMInfoTemplet>.Load("AB_SCRIPT", "LUA_BGM_INFO_TEMPLETE", "BGM_INFO_TEMPLETE", new Func<NKMLua, NKCBGMInfoTemplet>(NKCBGMInfoTemplet.LoadFromLUA));
			NKMTempletContainer<NKMUnitStatusTemplet>.Load("AB_SCRIPT", "LUA_UNIT_STATUS_TEMPLET", "UNIT_STATUS_TEMPLET", new Func<NKMLua, NKMUnitStatusTemplet>(NKMUnitStatusTemplet.LoadFromLua));
			NKMTempletContainer<NKMTacticUpdateTemplet>.Load("AB_SCRIPT", "LUA_TACTIC_UPDATE_TEMPLET", "TACTIC_UPDATE_TEMPLET", new Func<NKMLua, NKMTacticUpdateTemplet>(NKMTacticUpdateTemplet.LoadFromLua));
			NKMTempletContainer<NKMTacticUpdateTemplet>.Load("AB_SCRIPT", "LUA_TACTIC_UPDATE_TEMPLET", "TACTIC_UPDATE_TEMPLET", new Func<NKMLua, NKMTacticUpdateTemplet>(NKMTacticUpdateTemplet.LoadFromLua));
			NKCDiveManager.Initialize();
			NKMEpisodeMgr.Initialize();
			NKMEpisodeMgr.SortEpisodeTemplets();
			NKCLeaderBoardManager.Initialize();
			NKCGuildManager.Initialize();
			NKCChatManager.Initialize();
			NKMConst.Validate();
			NKCUnitMissionManager.Init();
			NKMTrimDungeonTemplet.Validate();
			NKMTempletContainer<NKMTrimTemplet>.Validate();
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x0011F15A File Offset: 0x0011D35A
		public static void QuitGame()
		{
			Log.Debug("[QuitGame]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCMain.cs", 771);
			Application.Quit();
		}

		// Token: 0x04003440 RID: 13376
		public const string marketID = "Steam";

		// Token: 0x04003441 RID: 13377
		public const string NKC_SAFE_MODE_KEY = "NKC_SAFE_MODE_KEY";

		// Token: 0x04003442 RID: 13378
		public static bool m_ranAsSafeMode;
	}
}

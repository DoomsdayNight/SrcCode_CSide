using System;
using ClientPacket.User;
using NKC.UI.Guild;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA9 RID: 2729
	public class NKCUIUserInfo : NKCUIBase
	{
		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x0600794F RID: 31055 RVA: 0x00285AAC File Offset: 0x00283CAC
		public static NKCUIUserInfo Instance
		{
			get
			{
				if (NKCUIUserInfo.m_Instance == null)
				{
					NKCUIUserInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCUIUserInfo>("ab_ui_nkm_ui_user_info", "NKM_UI_USER_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIUserInfo.CleanupInstance)).GetInstance<NKCUIUserInfo>();
					NKCUIUserInfo.m_Instance.Init();
				}
				return NKCUIUserInfo.m_Instance;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06007950 RID: 31056 RVA: 0x00285AFB File Offset: 0x00283CFB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIUserInfo.m_Instance != null && NKCUIUserInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x00285B16 File Offset: 0x00283D16
		private static void CleanupInstance()
		{
			NKCUIUserInfo.m_Instance = null;
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x00285B1E File Offset: 0x00283D1E
		public static void CheckInstanceAndClose()
		{
			if (NKCUIUserInfo.m_Instance != null && NKCUIUserInfo.m_Instance.IsOpen)
			{
				NKCUIUserInfo.m_Instance.Close();
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06007953 RID: 31059 RVA: 0x00285B43 File Offset: 0x00283D43
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06007954 RID: 31060 RVA: 0x00285B46 File Offset: 0x00283D46
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_PROFILE;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06007955 RID: 31061 RVA: 0x00285B4D File Offset: 0x00283D4D
		public override NKCUIBase.eTransitionEffectType eTransitionEffect
		{
			get
			{
				return NKCUIBase.eTransitionEffectType.FadeInOut;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06007956 RID: 31062 RVA: 0x00285B50 File Offset: 0x00283D50
		private NKMUserData UserData
		{
			get
			{
				return NKCScenManager.CurrentUserData();
			}
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x00285B57 File Offset: 0x00283D57
		public static void SetComment(string comment)
		{
			NKCUIUserInfo.m_sComment = comment;
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x00285B5F File Offset: 0x00283D5F
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			this.m_UINPCKimHana = null;
		}

		// Token: 0x06007959 RID: 31065 RVA: 0x00285B6E File Offset: 0x00283D6E
		public override void Hide()
		{
			NKCSoundManager.StopAllSound(SOUND_TRACK.VOICE);
			base.Hide();
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x00285B7C File Offset: 0x00283D7C
		public override void UnHide()
		{
			base.UnHide();
			this.UpdateMainUnitSlot(this.UserData);
			this.UpdateSubUnitSlot(this.UserData);
			this.UpdateBackgroundSlot(this.UserData);
		}

		// Token: 0x0600795B RID: 31067 RVA: 0x00285BA8 File Offset: 0x00283DA8
		public override void CloseInternal()
		{
			NKCSoundManager.StopAllSound(SOUND_TRACK.VOICE);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x00285BBC File Offset: 0x00283DBC
		public void Init()
		{
			this.m_slotMainUnit.Init();
			this.m_slotSubUnit.Init();
			this.m_slotMainShip.Init();
			this.m_slotBackground.Init();
			if (this.m_UINPCKimHana == null)
			{
				this.m_UINPCKimHana = this.m_objNPC_KimHana_TouchArea.GetComponent<NKCUINPCManagerKimHaNa>();
				this.m_UINPCKimHana.Init(true);
			}
			this.m_slotProfile.Init();
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditProfile, new UnityAction(this.OnBtnEditProfile));
			NKCUIComStateButton btnChangeLobby = this.m_btnChangeLobby;
			if (btnChangeLobby != null)
			{
				btnChangeLobby.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnChangeLobby2 = this.m_btnChangeLobby;
			if (btnChangeLobby2 != null)
			{
				btnChangeLobby2.PointerClick.AddListener(new UnityAction(this.OnTouchChangeLobby));
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x00285C88 File Offset: 0x00283E88
		public void Open(NKMUserData userData)
		{
			NKMArmyData armyData = userData.m_ArmyData;
			this.m_UINPCKimHana.Init(true);
			this.m_UINPCKimHana.PlayAni(NPC_ACTION_TYPE.ENTER_PROFILE, false);
			this.m_slotProfile.SetProfiledata(userData.UserProfileData, null);
			this.m_lbNickName.text = userData.m_UserNickName;
			this.m_lbLevel.text = userData.UserLevel.ToString();
			this.m_lbUID.text = NKCUtilString.GetFriendCode(userData.m_FriendCode);
			this.m_lbJoinDate.text = userData.m_NKMUserDateData.m_RegisterTime.ToLocalTime().ToString(NKCUtilString.GET_STRING_REGISTERTIME_DATE);
			long currentExp = NKCExpManager.GetCurrentExp(userData);
			long num = (long)NKCExpManager.GetRequiredExp(userData);
			this.m_lbExp.text = string.Format("{0} / {1}", currentExp, num);
			if (num == 0L)
			{
				this.m_sliderExp.value = 1f;
			}
			else
			{
				this.m_sliderExp.value = (float)currentExp / (float)num;
			}
			this.m_lbAchievementPoint.text = userData.GetMissionAchievePoint().ToString();
			this.SetPVP(userData);
			this.SetGuildData();
			this.m_lbUnitCount.text = string.Format("{0} / {1}", armyData.GetCurrentUnitCount(), armyData.m_MaxUnitCount);
			this.m_lbShipCount.text = string.Format("{0} / {1}", armyData.GetCurrentShipCount(), armyData.m_MaxShipCount);
			NKMInventoryData inventoryData = userData.m_InventoryData;
			this.m_lbEquipCount.text = string.Format("{0} / {1}", inventoryData.GetCountEquipItemTypes(), inventoryData.m_MaxItemEqipCount);
			this.m_lbOperatorCount.text = string.Format("{0} / {1}", armyData.GetCurrentOperatorCount(), armyData.m_MaxOperatorCount);
			NKCUtil.SetGameobjectActive(this.m_objOperator, !NKCOperatorUtil.IsHide());
			this.UpdateEterniumValue(userData);
			this.UpdateMainUnitSlot(userData);
			this.UpdateSubUnitSlot(userData);
			this.UpdateBackgroundSlot(userData);
			base.UIOpened(true);
			this.m_bOpen = true;
		}

		// Token: 0x0600795E RID: 31070 RVA: 0x00285EA0 File Offset: 0x002840A0
		private void SetGuildData()
		{
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, NKCGuildManager.HasGuild());
				if (this.m_objGuild.activeSelf)
				{
					this.m_BadgeUI.SetData(NKCGuildManager.MyGuildData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, NKCGuildManager.MyGuildData.name);
				}
			}
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x00285F04 File Offset: 0x00284104
		private void SetPVP(NKMUserData userData)
		{
			PvpState pvpState;
			NKM_GAME_TYPE gameType;
			if (userData.m_PvpData.Score >= userData.m_AsyncData.Score)
			{
				pvpState = userData.m_PvpData;
				gameType = NKM_GAME_TYPE.NGT_PVP_RANK;
				this.m_imgTierBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_USER_INFO_SPRITE", "NKM_UI_USER_INFO_GAUNTLETBG", false);
			}
			else
			{
				pvpState = userData.m_AsyncData;
				gameType = NKM_GAME_TYPE.NGT_ASYNC_PVP;
				this.m_imgTierBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_USER_INFO_SPRITE", "NKM_UI_USER_INFO_GAUNTLETBG_ASYNC", false);
			}
			int num = NKCPVPManager.FindPvPSeasonID(gameType, NKCSynchronizedTime.GetServerUTCTime(0.0));
			NKCUtil.SetGameobjectActive(this.m_objRecordOff, pvpState.SeasonID != num);
			NKCUtil.SetGameobjectActive(this.m_objRecordOn, pvpState.SeasonID == num);
			int leagueScore = pvpState.Score;
			if (pvpState.SeasonID != num)
			{
				this.m_lbRankStanding.text = NKCUtilString.GET_STRING_NO_RANK;
				leagueScore = NKCPVPManager.GetResetScore(pvpState.SeasonID, pvpState.Score, gameType);
				NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(gameType, num, leagueScore);
				if (rankTempletByScore != null)
				{
					this.m_NKCUILeagueTier.SetUI(rankTempletByScore);
					this.m_lbRank.text = rankTempletByScore.GetLeagueName();
				}
				this.m_lbPVPBestTier.text = "-";
				this.m_lbPVPBestScore.text = "-";
			}
			else
			{
				this.m_lbRankStanding.text = string.Format(NKCUtilString.GET_STRING_TOTAL_RANK_ONE_PARAM, pvpState.Rank);
				NKMPvpRankTemplet rankTempletByTier = NKCPVPManager.GetRankTempletByTier(gameType, num, pvpState.LeagueTierID);
				if (rankTempletByTier != null)
				{
					this.m_NKCUILeagueTier.SetUI(rankTempletByTier);
					this.m_lbRank.text = rankTempletByTier.GetLeagueName();
				}
				NKMPvpRankTemplet rankTempletByTier2 = NKCPVPManager.GetRankTempletByTier(gameType, num, pvpState.MaxLeagueTierID);
				if (rankTempletByTier2 != null)
				{
					this.m_lbPVPBestTier.text = rankTempletByTier2.GetLeagueName();
				}
				this.m_lbPVPBestScore.text = pvpState.MaxScore.ToString();
			}
			this.m_lbPVPScore.text = leagueScore.ToString();
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x002860D8 File Offset: 0x002842D8
		private void UpdateLobbyUnitSlot(int index, NKCUISlot slot, GameObject objUnitBG, NKMUserData userData)
		{
			NKMBackgroundUnitInfo backgroundUnitInfo = userData.GetBackgroundUnitInfo(index);
			if (backgroundUnitInfo == null)
			{
				if (slot != null)
				{
					slot.SetEmpty(new NKCUISlot.OnClick(this.OnTouchSlot));
				}
				return;
			}
			long num = (backgroundUnitInfo != null) ? backgroundUnitInfo.unitUid : 0L;
			NKM_UNIT_TYPE unitType = backgroundUnitInfo.unitType;
			if (unitType - NKM_UNIT_TYPE.NUT_NORMAL > 1)
			{
				if (unitType != NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					slot.SetEmpty(new NKCUISlot.OnClick(this.OnTouchSlot));
				}
				else
				{
					NKMOperator operatorFromUId = userData.m_ArmyData.GetOperatorFromUId(num);
					if (operatorFromUId != null)
					{
						slot.SetUnitData(operatorFromUId, false, false, false, new NKCUISlot.OnClick(this.OnTouchSlot));
					}
				}
			}
			else
			{
				NKMUnitData unitOrShipFromUID = userData.m_ArmyData.GetUnitOrShipFromUID(num);
				slot.SetUnitData(unitOrShipFromUID, false, false, false, new NKCUISlot.OnClick(this.OnTouchSlot));
				slot.SetSeized(unitOrShipFromUID != null && unitOrShipFromUID.IsSeized);
			}
			NKCUtil.SetGameobjectActive(objUnitBG, backgroundUnitInfo.backImage);
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x002861AA File Offset: 0x002843AA
		public void UpdateMainUnitSlot(NKMUserData userData)
		{
			this.UpdateLobbyUnitSlot(0, this.m_slotMainUnit, this.m_objMainUnitBG, userData);
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x002861C0 File Offset: 0x002843C0
		public void UpdateSubUnitSlot(NKMUserData userData)
		{
			this.UpdateLobbyUnitSlot(1, this.m_slotSubUnit, this.m_objSubUnitBG, userData);
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x002861D6 File Offset: 0x002843D6
		public void UpdateBackgroundSlot(NKMUserData userData)
		{
			this.m_slotBackground.SetMiscItemData(userData.BackgroundID, 1L, false, false, true, new NKCUISlot.OnClick(this.OnTouchSlot));
		}

		// Token: 0x06007964 RID: 31076 RVA: 0x002861FA File Offset: 0x002843FA
		private void OnTouchSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnTouchChangeLobby();
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x00286202 File Offset: 0x00284402
		private void OnBtnEditProfile()
		{
			base.Close();
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_FRIEND_MYPROFILE, "", false);
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x00286217 File Offset: 0x00284417
		private void OnTouchChangeLobby()
		{
			NKCUIChangeLobby.Instance.Open(this.UserData);
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x0028622C File Offset: 0x0028442C
		public void RefreshNickname()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKCUtil.SetLabelText(this.m_lbNickName, myUserData.m_UserNickName);
			}
		}

		// Token: 0x06007968 RID: 31080 RVA: 0x00286258 File Offset: 0x00284458
		private void UpdateEterniumValue(NKMUserData userData)
		{
			this.m_lbEterniumCount.text = string.Format("{0} / {1}", userData.GetEternium(), userData.GetEterniumCap());
		}

		// Token: 0x06007969 RID: 31081 RVA: 0x00286288 File Offset: 0x00284488
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData.ItemID == 2)
			{
				NKMUserData userData = NKCScenManager.CurrentUserData();
				this.UpdateEterniumValue(userData);
			}
		}

		// Token: 0x0600796A RID: 31082 RVA: 0x002862AB File Offset: 0x002844AB
		public override void OnUserLevelChanged(NKMUserData userData)
		{
			this.UpdateEterniumValue(userData);
		}

		// Token: 0x0600796B RID: 31083 RVA: 0x002862B4 File Offset: 0x002844B4
		public override void OnGuildDataChanged()
		{
			this.SetGuildData();
		}

		// Token: 0x040065F3 RID: 26099
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_user_info";

		// Token: 0x040065F4 RID: 26100
		private const string UI_ASSET_NAME = "NKM_UI_USER_INFO";

		// Token: 0x040065F5 RID: 26101
		private static NKCUIUserInfo m_Instance;

		// Token: 0x040065F6 RID: 26102
		[Header("메인 정보")]
		public NKCUISlotProfile m_slotProfile;

		// Token: 0x040065F7 RID: 26103
		public Text m_lbNickName;

		// Token: 0x040065F8 RID: 26104
		public Text m_lbLevel;

		// Token: 0x040065F9 RID: 26105
		public Text m_lbUID;

		// Token: 0x040065FA RID: 26106
		public Text m_lbJoinDate;

		// Token: 0x040065FB RID: 26107
		public Slider m_sliderExp;

		// Token: 0x040065FC RID: 26108
		public Text m_lbExp;

		// Token: 0x040065FD RID: 26109
		public NKCUIComStateButton m_csbtnEditProfile;

		// Token: 0x040065FE RID: 26110
		public GameObject m_objGuild;

		// Token: 0x040065FF RID: 26111
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04006600 RID: 26112
		public Text m_lbGuildName;

		// Token: 0x04006601 RID: 26113
		[Header("PVP Score")]
		public GameObject m_objRecordOn;

		// Token: 0x04006602 RID: 26114
		public Text m_lbRank;

		// Token: 0x04006603 RID: 26115
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x04006604 RID: 26116
		public Image m_imgTierBG;

		// Token: 0x04006605 RID: 26117
		public Text m_lbRankStanding;

		// Token: 0x04006606 RID: 26118
		public Text m_lbPVPScore;

		// Token: 0x04006607 RID: 26119
		public GameObject m_objRecordOff;

		// Token: 0x04006608 RID: 26120
		public Text m_lbPVPBestTier;

		// Token: 0x04006609 RID: 26121
		public Text m_lbPVPBestScore;

		// Token: 0x0400660A RID: 26122
		[Header("기업 정보")]
		public Text m_lbEterniumCount;

		// Token: 0x0400660B RID: 26123
		public Text m_lbUnitCount;

		// Token: 0x0400660C RID: 26124
		public Text m_lbShipCount;

		// Token: 0x0400660D RID: 26125
		public Text m_lbEquipCount;

		// Token: 0x0400660E RID: 26126
		public GameObject m_objOperator;

		// Token: 0x0400660F RID: 26127
		public Text m_lbOperatorCount;

		// Token: 0x04006610 RID: 26128
		public Text m_lbVictoryPoint;

		// Token: 0x04006611 RID: 26129
		public Text m_lbInformationPoint;

		// Token: 0x04006612 RID: 26130
		public Text m_lbAchievementPoint;

		// Token: 0x04006613 RID: 26131
		[Header("대표 유닛 슬롯")]
		public NKCUISlot m_slotMainUnit;

		// Token: 0x04006614 RID: 26132
		public GameObject m_objMainUnitBG;

		// Token: 0x04006615 RID: 26133
		public NKCUISlot m_slotSubUnit;

		// Token: 0x04006616 RID: 26134
		public GameObject m_objSubUnitBG;

		// Token: 0x04006617 RID: 26135
		public NKCUISlot m_slotMainShip;

		// Token: 0x04006618 RID: 26136
		public NKCUISlot m_slotBackground;

		// Token: 0x04006619 RID: 26137
		public NKCUIComStateButton m_btnChangeLobby;

		// Token: 0x0400661A RID: 26138
		[Header("NPC")]
		public GameObject m_objNPC_KimHana_TouchArea;

		// Token: 0x0400661B RID: 26139
		private NKCUINPCManagerKimHaNa m_UINPCKimHana;

		// Token: 0x0400661C RID: 26140
		private bool m_bChangeCommentClicked;

		// Token: 0x0400661D RID: 26141
		private static string m_sComment = "";
	}
}

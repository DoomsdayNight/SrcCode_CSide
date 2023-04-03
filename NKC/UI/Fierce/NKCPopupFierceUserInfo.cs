using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Game;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB1 RID: 2993
	public class NKCPopupFierceUserInfo : NKCUIBase
	{
		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06008A59 RID: 35417 RVA: 0x002F099C File Offset: 0x002EEB9C
		public static NKCPopupFierceUserInfo Instance
		{
			get
			{
				if (NKCPopupFierceUserInfo.m_Instance == null)
				{
					NKCPopupFierceUserInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFierceUserInfo>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_INFO_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFierceUserInfo.CleanupInstance)).GetInstance<NKCPopupFierceUserInfo>();
					NKCPopupFierceUserInfo.m_Instance.InitUI();
				}
				return NKCPopupFierceUserInfo.m_Instance;
			}
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06008A5A RID: 35418 RVA: 0x002F09EB File Offset: 0x002EEBEB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupFierceUserInfo.m_Instance != null && NKCPopupFierceUserInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x002F0A06 File Offset: 0x002EEC06
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFierceUserInfo.m_Instance != null && NKCPopupFierceUserInfo.m_Instance.IsOpen)
			{
				NKCPopupFierceUserInfo.m_Instance.Close();
			}
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x002F0A2B File Offset: 0x002EEC2B
		private static void CleanupInstance()
		{
			NKCPopupFierceUserInfo.m_Instance = null;
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x002F0A33 File Offset: 0x002EEC33
		public static bool IsHasInstance()
		{
			return NKCPopupFierceUserInfo.m_Instance != null;
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06008A5E RID: 35422 RVA: 0x002F0A40 File Offset: 0x002EEC40
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06008A5F RID: 35423 RVA: 0x002F0A43 File Offset: 0x002EEC43
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FRIEND_INFO;
			}
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x002F0A4C File Offset: 0x002EEC4C
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstNKCDeckViewUnitSlot[i];
				if (nkcdeckViewUnitSlot != null)
				{
					nkcdeckViewUnitSlot.Init(i, true);
				}
			}
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(this.CloseInternal));
			NKCUtil.SetEventTriggerDelegate(this.m_evt, new UnityAction(this.CloseInternal));
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x002F0AC9 File Offset: 0x002EECC9
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008A62 RID: 35426 RVA: 0x002F0AD8 File Offset: 0x002EECD8
		public void Open(NKMPacket_FIERCE_PROFILE_ACK fierceProfileData)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (fierceProfileData != null)
			{
				this.ClearBossSlots();
				this.UpdateProfileData(fierceProfileData.commonProfile);
				NKCUtil.SetLabelText(this.m_lbUserDesc, NKCFilterManager.CheckBadChat(fierceProfileData.friendIntro));
				this.UpdateGuildData(fierceProfileData.guildData);
				this.SetFierceData(fierceProfileData.profileData);
				this.SelectSlotWhenOpend(fierceProfileData.profileData);
			}
			if (null != this.m_rtResultSlotParent)
			{
				this.m_rtResultSlotParent.anchoredPosition = Vector2.zero;
			}
			if (!NKCPopupFierceUserInfo.m_Instance.IsOpen)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x06008A63 RID: 35427 RVA: 0x002F0B71 File Offset: 0x002EED71
		private void SelectSlotWhenOpend(NKMFierceProfileData fierceProfile)
		{
			this.OnClickBossSlot(fierceProfile.fierceBossGroupId);
		}

		// Token: 0x06008A64 RID: 35428 RVA: 0x002F0B80 File Offset: 0x002EED80
		private void UpdateProfileData(NKMCommonProfile profile)
		{
			if (profile == null)
			{
				return;
			}
			this.m_lFierceUserUID = profile.userUid;
			this.m_ProfileSlot.SetProfiledata(profile, null);
			NKCUtil.SetLabelText(this.m_lbUserName, profile.nickname);
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_FRIEND_INFO_LEVEL_ONE_PARAM, profile.level));
			NKCUtil.SetLabelText(this.m_lbUserCode, NKCUtilString.GetFriendCode(profile.friendCode));
		}

		// Token: 0x06008A65 RID: 35429 RVA: 0x002F0BF4 File Offset: 0x002EEDF4
		private void UpdateEmblem(List<NKMEmblemData> emblems)
		{
			for (int i = 0; i < this.m_lstEmblem.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstEmblem[i];
				if (i < emblems.Count && emblems[i] != null && emblems[i].id > 0 && NKMItemManager.GetItemMiscTempletByID(emblems[i].id) != null)
				{
					if (i <= 3)
					{
						nkcuislot.SetMiscItemData(emblems[i].id, emblems[i].count, false, true, true, null);
					}
					else
					{
						nkcuislot.SetEmpty(null);
					}
				}
				else
				{
					nkcuislot.SetEmpty(null);
				}
			}
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x002F0C93 File Offset: 0x002EEE93
		public bool IsSameProfile(long userUID)
		{
			return userUID == this.m_lFierceUserUID;
		}

		// Token: 0x06008A67 RID: 35431 RVA: 0x002F0CA0 File Offset: 0x002EEEA0
		public void SetFierceData(NKMFierceProfileData fierceProfile)
		{
			if (fierceProfile == null)
			{
				return;
			}
			NKCUIFierceBattleBossListSlot newInstance = NKCUIFierceBattleBossListSlot.GetNewInstance(this.m_rtBossSlotParents);
			if (null != newInstance)
			{
				newInstance.SetData(fierceProfile.fierceBossGroupId);
				if (fierceProfile.totalPoint <= 0)
				{
					NKCUtil.SetBindFunction(newInstance.m_csbtnBtn, null);
				}
				else
				{
					NKCUtil.SetBindFunction(newInstance.m_csbtnBtn, delegate()
					{
						this.OnClickBossSlot(fierceProfile.fierceBossGroupId);
					});
				}
				newInstance.SetHasRecord(fierceProfile.totalPoint > 0);
				this.m_lstBossSlots.Add(newInstance);
			}
			this.m_FierceProfile = fierceProfile;
		}

		// Token: 0x06008A68 RID: 35432 RVA: 0x002F0D50 File Offset: 0x002EEF50
		public void UpdateFierceData()
		{
			NKMFierceProfileData fierceProfile = this.m_FierceProfile;
			if (fierceProfile == null)
			{
				return;
			}
			this.ClearCurResultSlots();
			this.SetDeck(fierceProfile.profileDeck, false);
			int num = 0;
			int num2 = 0;
			foreach (NKMDummyUnitData nkmdummyUnitData in fierceProfile.profileDeck.List)
			{
				if (nkmdummyUnitData != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmdummyUnitData.UnitId);
					if (unitStatTemplet != null)
					{
						num += unitStatTemplet.GetRespawnCost(num2 == 0, null, null);
						num2++;
					}
				}
			}
			NKCUtil.SetLabelText(this.m_ArmyOperationPower, fierceProfile.operationPower.ToString());
			NKCUtil.SetLabelText(this.m_ArmyAvgCost, string.Format("{0:0.00}", num / num2));
			int num3 = fierceProfile.totalPoint - fierceProfile.penaltyPoint;
			NKCUtil.SetLabelText(this.m_lbBaseScore, num3.ToString());
			NKCUtil.SetLabelText(this.m_lbTotalScore, fierceProfile.totalPoint.ToString());
			NKCUtil.SetGameobjectActive(this.m_objNonePenalty, fierceProfile.penaltyIds.Count <= 0);
			NKCUtil.SetGameobjectActive(this.m_lbPenaltyTitle.gameObject, fierceProfile.penaltyIds.Count > 0);
			float num4 = 0f;
			foreach (int bossId in fierceProfile.penaltyIds)
			{
				NKMFiercePenaltyTemplet nkmfiercePenaltyTemplet = NKMFiercePenaltyTemplet.Find(bossId);
				if (nkmfiercePenaltyTemplet != null)
				{
					if (nkmfiercePenaltyTemplet.FiercePenaltyType == FiercePenalty.BUFF)
					{
						NKCUtil.SetLabelText(this.m_lbPenaltyTitle, NKCUtilString.GET_STRING_FIERCE_PENALTY_TITLE_BUFF);
					}
					else if (nkmfiercePenaltyTemplet.FiercePenaltyType == FiercePenalty.DEBUFF)
					{
						NKCUtil.SetLabelText(this.m_lbPenaltyTitle, NKCUtilString.GET_STRING_FIERCE_PENALTY_TITLE_DEBUFF);
					}
					NKCUIFierceBattleSelfPenaltySumSlot nkcuifierceBattleSelfPenaltySumSlot = UnityEngine.Object.Instantiate<NKCUIFierceBattleSelfPenaltySumSlot>(this.m_pfbResultSlot);
					if (null != nkcuifierceBattleSelfPenaltySumSlot)
					{
						nkcuifierceBattleSelfPenaltySumSlot.SetData(nkmfiercePenaltyTemplet);
						nkcuifierceBattleSelfPenaltySumSlot.transform.SetParent(this.m_rtResultSlotParent);
						this.m_lstSumSlots.Add(nkcuifierceBattleSelfPenaltySumSlot);
					}
					num4 += nkmfiercePenaltyTemplet.FierceScoreRate;
				}
			}
			num4 *= 0.01f;
			if (num4 < 0f)
			{
				num4 *= -1f;
				NKCUtil.SetLabelText(this.m_lbBonus, string.Format(NKCUtilString.GET_STRING_FIERCE_PENALTY_SCORE_MINUS, num4));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbBonus, string.Format(NKCUtilString.GET_STRING_FIERCE_PENALTY_SCORE_PLUS, num4));
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x002F0FA4 File Offset: 0x002EF1A4
		private void SetDeck(NKMDummyDeckData deckData, bool bSetFirstDeckLeader = false)
		{
			if (deckData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(deckData.Ship.UnitId);
			if (unitTempletBase != null)
			{
				this.m_imgShip.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
			}
			else
			{
				this.m_imgShip.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_DECK_VIEW_SPRITE", "NKM_DECK_VIEW_SHIP_UNKNOWN", false);
			}
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				if (i < 8)
				{
					NKMDummyUnitData nkmdummyUnitData = deckData.List[i];
					NKMUnitData cNKMUnitData = null;
					if (nkmdummyUnitData != null && nkmdummyUnitData.UnitId > 0)
					{
						cNKMUnitData = nkmdummyUnitData.ToUnitData(-1L);
					}
					this.m_lstNKCDeckViewUnitSlot[i].SetData(cNKMUnitData, false);
					if (bSetFirstDeckLeader && i == 0)
					{
						this.m_lstNKCDeckViewUnitSlot[i].SetLeader(true, false);
					}
				}
			}
			if (NKCOperatorUtil.IsHide())
			{
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
				return;
			}
			if (deckData.operatorUnit == null)
			{
				this.m_OperatorSlot.SetEmpty();
				return;
			}
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(deckData.operatorUnit.UnitId);
			if (unitTempletBase2 != null)
			{
				this.m_OperatorSlot.SetData(unitTempletBase2, deckData.operatorUnit.UnitLevel);
				return;
			}
			this.m_OperatorSlot.SetEmpty();
		}

		// Token: 0x06008A6A RID: 35434 RVA: 0x002F10D0 File Offset: 0x002EF2D0
		private void UpdateGuildData(NKMGuildSimpleData guildData)
		{
			if (this.m_objGuild == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objGuild, guildData != null && guildData.guildUid > 0L);
			if (this.m_objGuild.activeSelf && guildData != null)
			{
				this.m_BadgeUI.SetData(guildData.badgeId);
				NKCUtil.SetLabelText(this.m_lbGuildName, guildData.guildName);
			}
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x002F113C File Offset: 0x002EF33C
		public void OnClickBossSlot(int iBossGroupID)
		{
			foreach (NKCUIFierceBattleBossListSlot nkcuifierceBattleBossListSlot in this.m_lstBossSlots)
			{
				nkcuifierceBattleBossListSlot.OnClicked(nkcuifierceBattleBossListSlot.m_fierceBossGroupID == iBossGroupID);
			}
			this.UpdateFierceData();
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x002F119C File Offset: 0x002EF39C
		private void ClearCurResultSlots()
		{
			if (this.m_lstSumSlots == null)
			{
				return;
			}
			for (int i = 0; i < this.m_lstSumSlots.Count; i++)
			{
				if (null != this.m_lstSumSlots[i])
				{
					UnityEngine.Object.Destroy(this.m_lstSumSlots[i].gameObject);
					this.m_lstSumSlots[i] = null;
				}
			}
			this.m_lstSumSlots.Clear();
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x002F120C File Offset: 0x002EF40C
		private void ClearBossSlots()
		{
			for (int i = 0; i < this.m_lstBossSlots.Count; i++)
			{
				if (!(null == this.m_lstBossSlots[i]))
				{
					UnityEngine.Object.Destroy(this.m_lstBossSlots[i].gameObject);
					this.m_lstBossSlots[i] = null;
				}
			}
			this.m_lstBossSlots.Clear();
		}

		// Token: 0x04007708 RID: 30472
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_fierce_battle";

		// Token: 0x04007709 RID: 30473
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FIERCE_BATTLE_INFO_POPUP";

		// Token: 0x0400770A RID: 30474
		private static NKCPopupFierceUserInfo m_Instance;

		// Token: 0x0400770B RID: 30475
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400770C RID: 30476
		[Header("프로필")]
		public NKCUISlotProfile m_ProfileSlot;

		// Token: 0x0400770D RID: 30477
		public Text m_lbLevel;

		// Token: 0x0400770E RID: 30478
		public Text m_lbUserName;

		// Token: 0x0400770F RID: 30479
		public Text m_lbUserCode;

		// Token: 0x04007710 RID: 30480
		public GameObject m_objGuild;

		// Token: 0x04007711 RID: 30481
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04007712 RID: 30482
		public Text m_lbGuildName;

		// Token: 0x04007713 RID: 30483
		public Text m_lbUserDesc;

		// Token: 0x04007714 RID: 30484
		[Header("Center")]
		public RectTransform m_rtBossSlotParents;

		// Token: 0x04007715 RID: 30485
		public List<NKCUIFierceBattleBossListSlot> m_lstBossSlots;

		// Token: 0x04007716 RID: 30486
		public GameObject m_objPenaltyNone;

		// Token: 0x04007717 RID: 30487
		[Header("Right")]
		public Image m_imgShip;

		// Token: 0x04007718 RID: 30488
		public NKCUIOperatorDeckSlot m_OperatorSlot;

		// Token: 0x04007719 RID: 30489
		public List<NKCDeckViewUnitSlot> m_lstNKCDeckViewUnitSlot;

		// Token: 0x0400771A RID: 30490
		public Text m_ArmyOperationPower;

		// Token: 0x0400771B RID: 30491
		public Text m_ArmyAvgCost;

		// Token: 0x0400771C RID: 30492
		[Header("패널티")]
		public GameObject m_objNonePenalty;

		// Token: 0x0400771D RID: 30493
		public Text m_lbPenaltyTitle;

		// Token: 0x0400771E RID: 30494
		public Text m_lbBaseScore;

		// Token: 0x0400771F RID: 30495
		public Text m_lbBonus;

		// Token: 0x04007720 RID: 30496
		public Text m_lbTotalScore;

		// Token: 0x04007721 RID: 30497
		public List<NKCUISlot> m_lstEmblem;

		// Token: 0x04007722 RID: 30498
		public RectTransform m_rtResultSlotParent;

		// Token: 0x04007723 RID: 30499
		public NKCUIFierceBattleSelfPenaltySumSlot m_pfbResultSlot;

		// Token: 0x04007724 RID: 30500
		private List<NKCUIFierceBattleSelfPenaltySumSlot> m_lstSumSlots = new List<NKCUIFierceBattleSelfPenaltySumSlot>();

		// Token: 0x04007725 RID: 30501
		public EventTrigger m_evt;

		// Token: 0x04007726 RID: 30502
		private long m_lFierceUserUID;

		// Token: 0x04007727 RID: 30503
		private NKMFierceProfileData m_FierceProfile;
	}
}

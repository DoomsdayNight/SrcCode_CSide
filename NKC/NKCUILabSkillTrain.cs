using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B0 RID: 2480
	public class NKCUILabSkillTrain : MonoBehaviour
	{
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06006817 RID: 26647 RVA: 0x00219E3B File Offset: 0x0021803B
		private int CurrentSkillID
		{
			get
			{
				if (this.m_currentSkill == null)
				{
					return 0;
				}
				return this.m_currentSkill.m_ID;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06006818 RID: 26648 RVA: 0x00219E52 File Offset: 0x00218052
		private NKMUserData UserData
		{
			get
			{
				return NKCScenManager.CurrentUserData();
			}
		}

		// Token: 0x06006819 RID: 26649 RVA: 0x00219E5C File Offset: 0x0021805C
		private void OnDisable()
		{
			for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
			{
				this.m_lstSkillSlot[i].ShowEffect(false);
			}
		}

		// Token: 0x0600681A RID: 26650 RVA: 0x00219E94 File Offset: 0x00218094
		public void Init(NKCUILabSkillTrain.OnTrySkillTrain onTrySkillTrain)
		{
			for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
			{
				this.m_lstSkillSlot[i].Init(new NKCUISkillSlot.OnClickSkillSlot(this.OnSelectSlot));
			}
			this.m_csbtnToSkillMenu.PointerClick.RemoveAllListeners();
			this.m_csbtnToSkillMenu.PointerClick.AddListener(new UnityAction(this.StartTrain));
			this.dOnTrySkillTrain = onTrySkillTrain;
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x00219F08 File Offset: 0x00218108
		public void Cleanup()
		{
			this.m_UnitData = null;
			this.m_currentSkill = null;
			foreach (NKCUILabSkillTrain.UnitSkillSlot unitSkillSlot in this.m_lstSkillSlot)
			{
				unitSkillSlot.Cleanup();
			}
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x00219F68 File Offset: 0x00218168
		public void SetData(NKMUserData userData, NKMUnitData unitData, int selectedSkillID = -1, bool bAnimate = false)
		{
			if (!this.m_NKM_UI_LAB_TRAINING_SKILL_LV_BACK.activeSelf)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_LAB_TRAINING_SKILL_LV_BACK, true);
			}
			this.m_UnitData = unitData;
			this.m_currentSkill = null;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase != null)
			{
				for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
				{
					NKMUnitSkillTemplet unitSkillTemplet = NKMUnitSkillManager.GetUnitSkillTemplet(unitTempletBase.GetSkillStrID(i), unitData);
					bool bIsHyper = unitSkillTemplet != null && unitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER;
					this.m_lstSkillSlot[i].SetData(unitSkillTemplet, bIsHyper);
					if (unitSkillTemplet != null && NKMUnitSkillManager.IsLockedSkill(unitSkillTemplet.m_ID, (int)this.m_UnitData.m_LimitBreakLevel))
					{
						this.m_lstSkillSlot[i].LockSkill(true);
					}
					if (i == 0)
					{
						if (unitSkillTemplet == null)
						{
							Debug.Log("Unit have no skill. UnitID : " + unitData.m_UnitID.ToString());
						}
						else if (selectedSkillID == -1)
						{
							selectedSkillID = unitSkillTemplet.m_ID;
						}
					}
					if (i == 3)
					{
						if (unitSkillTemplet == null || NKMUnitSkillManager.IsLockedSkill(unitSkillTemplet.m_ID, (int)unitData.m_LimitBreakLevel))
						{
							NKCUtil.SetGameobjectActive(this.m_objHyperSkillGlowEffect, false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_objHyperSkillGlowEffect, true);
						}
					}
				}
				this.SelectSkill(userData, unitData, selectedSkillID, bAnimate);
				return;
			}
			for (int j = 0; j < this.m_lstSkillSlot.Count; j++)
			{
				this.m_lstSkillSlot[j].SetData(null, false);
			}
			this.SelectSkill(userData, null, 0, bAnimate);
			NKCUtil.SetGameobjectActive(this.m_objHyperSkillGlowEffect, false);
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x0021A0D8 File Offset: 0x002182D8
		private void SelectSkill(NKMUserData userData, NKMUnitData unitData, int skillID, bool bAnimate = true)
		{
			if (unitData == null)
			{
				this.SetSkillDetail(null);
				this.UpdateSkillTrainButton(userData, unitData, skillID, bAnimate);
				return;
			}
			NKMUnitSkillTemplet unitSkillTemplet = NKMUnitSkillManager.GetUnitSkillTemplet(skillID, unitData);
			if (unitSkillTemplet == null)
			{
				if (unitData.GetUnitSkillCount() == 0)
				{
					Debug.LogError("Unit have no skill. unitID : " + unitData.m_UnitID.ToString());
				}
				this.SetSkillDetail(null);
				this.UpdateSkillTrainButton(userData, unitData, skillID, bAnimate);
				return;
			}
			this.m_currentSkill = unitSkillTemplet;
			this.SetHighlightSlot(skillID);
			this.SetSkillDetail(unitSkillTemplet);
			this.UpdateSkillTrainButton(userData, unitData, skillID, bAnimate);
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x0021A15C File Offset: 0x0021835C
		private void SetSkillDetail(NKMUnitSkillTemplet skillTemplet)
		{
			if (skillTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objRootSkillDetail, false);
				this.UpdateRequiredItemData(0, 0);
				foreach (NKCUIComSkillLevelDetail targetMono in this.m_lstSkillLevelDetail)
				{
					NKCUtil.SetGameobjectActive(targetMono, false);
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRootSkillDetail, true);
			if (this.m_lbSkillName != null)
			{
				this.m_lbSkillName.text = skillTemplet.GetSkillName();
				bool flag = NKMUnitSkillManager.IsLockedSkill(skillTemplet.m_ID, (int)this.m_UnitData.m_LimitBreakLevel);
				if (flag)
				{
					this.m_lbSkillName.color = new Color(0.39607844f, 0.39607844f, 0.39607844f);
				}
				else
				{
					this.m_lbSkillName.color = new Color(1f, 1f, 1f);
				}
				NKCUtil.SetGameobjectActive(this.m_imgSkillLock, flag);
			}
			this.SetSkillType(skillTemplet.m_NKM_SKILL_TYPE);
			this.SetUnlockReqUpgradeCount(skillTemplet);
			if (skillTemplet.m_fCooltimeSecond > 0f)
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillCooldown, true);
				NKCUtil.SetLabelText(this.m_lbSkillCooldown, string.Format(NKCUtilString.GET_STRING_TIME_SECOND_ONE_PARAM, skillTemplet.m_fCooltimeSecond));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillCooldown, false);
			}
			int skillAttackCount = this.GetSkillAttackCount(skillTemplet);
			if (skillAttackCount > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillAttackCount, true);
				NKCUtil.SetLabelText(this.m_lbSkillAttackCount, string.Format(NKCUtilString.GET_STRING_SKILL_ATTACK_COUNT_ONE_PARAM, skillAttackCount));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillAttackCount, false);
			}
			if (skillTemplet.m_Level == 1)
			{
				this.m_lbSkillDescription.text = skillTemplet.GetSkillDesc();
			}
			else
			{
				NKMUnitSkillTemplet skillTemplet2 = NKMUnitSkillManager.GetSkillTemplet(skillTemplet.m_ID, 1);
				if (skillTemplet2 != null)
				{
					this.m_lbSkillDescription.text = skillTemplet2.GetSkillDesc();
				}
			}
			this.m_SkillDescScrollRect.verticalNormalizedPosition = 1f;
			this.UpdateRequiredItemData(skillTemplet.m_ID, skillTemplet.m_Level + 1);
			int maxSkillLevel = NKMUnitSkillManager.GetMaxSkillLevel(skillTemplet.m_ID);
			foreach (NKCUIComSkillLevelDetail nkcuicomSkillLevelDetail in this.m_lstSkillLevelDetail)
			{
				if (nkcuicomSkillLevelDetail.m_iLevel <= maxSkillLevel)
				{
					NKCUtil.SetGameobjectActive(nkcuicomSkillLevelDetail, true);
					nkcuicomSkillLevelDetail.SetData(skillTemplet.m_ID, nkcuicomSkillLevelDetail.m_iLevel <= skillTemplet.m_Level, -1);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuicomSkillLevelDetail, false);
				}
			}
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x0021A3DC File Offset: 0x002185DC
		private int GetSkillAttackCount(NKMUnitSkillTemplet unitTemplet)
		{
			if (unitTemplet != null)
			{
				return unitTemplet.m_AttackCount;
			}
			return 0;
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x0021A3EC File Offset: 0x002185EC
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (this.m_currentSkill == null)
			{
				return;
			}
			using (List<NKCUIItemCostSlot>.Enumerator enumerator = this.m_lstCostItemUI.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ItemID == itemData.ItemID)
					{
						this.UpdateRequiredItemData(this.m_currentSkill.m_ID, this.m_currentSkill.m_Level + 1);
						this.UpdateSkillTrainButton(this.UserData, this.m_UnitData, this.m_currentSkill.m_ID, true);
						break;
					}
				}
			}
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x0021A48C File Offset: 0x0021868C
		private void UpdateRequiredItemData(int skillID, int targetLevel)
		{
			NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillID, targetLevel);
			for (int i = 0; i < this.m_lstCostItemUI.Count; i++)
			{
				NKCUIItemCostSlot nkcuiitemCostSlot = this.m_lstCostItemUI[i];
				if (skillTemplet != null)
				{
					if (i < skillTemplet.m_lstUpgradeReqItem.Count)
					{
						NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, true);
						long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(skillTemplet.m_lstUpgradeReqItem[i].ItemID);
						nkcuiitemCostSlot.SetData(skillTemplet.m_lstUpgradeReqItem[i].ItemID, skillTemplet.m_lstUpgradeReqItem[i].ItemCount, countMiscItem, true, true, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, false);
						nkcuiitemCostSlot.SetData(0, 0, 0L, true, true, false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, false);
					nkcuiitemCostSlot.SetData(0, 0, 0L, true, true, false);
				}
			}
			if (skillTemplet != null && skillTemplet.m_UnlockReqUpgrade > 0)
			{
				int num = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID).m_StarGradeMax - 3;
				int reqCnt = skillTemplet.m_UnlockReqUpgrade + num;
				int num2 = (int)this.m_UnitData.m_LimitBreakLevel + num;
				NKCUtil.SetGameobjectActive(this.m_objLimitBreak, true);
				NKCUtil.SetGameobjectActive(this.m_itemLimitBreak, true);
				this.m_itemLimitBreak.SetData(912, reqCnt, (long)num2, true, true, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objLimitBreak, false);
			NKCUtil.SetGameobjectActive(this.m_itemLimitBreak, false);
			this.m_itemLimitBreak.SetData(0, 0, 0L, true, true, false);
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x0021A5F8 File Offset: 0x002187F8
		private void SetHighlightSlot(int HighlightedSlotSkillID)
		{
			if (HighlightedSlotSkillID == 0)
			{
				for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
				{
					this.m_lstSkillSlot[i].SetHighlight(false);
				}
				return;
			}
			for (int j = 0; j < this.m_lstSkillSlot.Count; j++)
			{
				this.m_lstSkillSlot[j].SetHighlight(this.m_lstSkillSlot[j].CurrentSkillID == HighlightedSlotSkillID);
			}
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x0021A66C File Offset: 0x0021886C
		private void UpdateSkillTrainButton(NKMUserData userData, NKMUnitData unitData, int skillID, bool bAnimate = true)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanTrainSkill(userData, unitData, skillID);
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				this.m_csbtnToSkillMenu.UnLock(false);
				NKCUtil.SetGameobjectActive(this.m_objButtonEffect, true);
				this.m_NKM_UI_LAB_TRAINING_ENTER_TEXT.color = NKCUtil.GetButtonUIColor(true);
				this.m_ImgNKM_UI_LAB_TRAINING_ENTER_ICON.color = NKCUtil.GetButtonUIColor(true);
				return;
			}
			switch (nkm_ERROR_CODE)
			{
			case NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NOT_EXIST:
				Debug.LogError("No SkillLevelData");
				this.m_csbtnToSkillMenu.Lock(false);
				NKCUtil.SetGameobjectActive(this.m_objButtonEffect, false);
				this.m_NKM_UI_LAB_TRAINING_ENTER_TEXT.color = NKCUtil.GetButtonUIColor(false);
				this.m_ImgNKM_UI_LAB_TRAINING_ENTER_ICON.color = NKCUtil.GetButtonUIColor(false);
				return;
			default:
				this.m_csbtnToSkillMenu.Lock(false);
				NKCUtil.SetGameobjectActive(this.m_objButtonEffect, false);
				this.m_NKM_UI_LAB_TRAINING_ENTER_TEXT.color = NKCUtil.GetButtonUIColor(false);
				this.m_ImgNKM_UI_LAB_TRAINING_ENTER_ICON.color = NKCUtil.GetButtonUIColor(false);
				return;
			}
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x0021A764 File Offset: 0x00218964
		private NKM_ERROR_CODE CanTrainSkill(NKMUserData userData, NKMUnitData targetUnit, int skillID)
		{
			if (targetUnit == null || userData == null || userData.m_ArmyData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (targetUnit.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED;
			}
			NKMDeckData deckDataByUnitUID = userData.m_ArmyData.GetDeckDataByUnitUID(targetUnit.m_UnitUID);
			if (deckDataByUnitUID != null)
			{
				NKM_DECK_STATE state = deckDataByUnitUID.GetState();
				if (state == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKMUnitSkillManager.CanTrainSkill(userData, targetUnit, skillID);
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x0021A7CD File Offset: 0x002189CD
		private void SetSkillType(NKM_SKILL_TYPE type)
		{
			this.m_lbSkillType.color = NKCUtil.GetSkillTypeColor(type);
			this.m_lbSkillType.text = NKCUtilString.GetSkillTypeName(type);
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x0021A7F4 File Offset: 0x002189F4
		private void SetUnlockReqUpgradeCount(NKMUnitSkillTemplet skillTemplet)
		{
			if (skillTemplet == null || this.m_UnitData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillLockRoot, false);
				return;
			}
			if (!NKMUnitSkillManager.IsLockedSkill(skillTemplet.m_ID, (int)this.m_UnitData.m_LimitBreakLevel))
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillLockRoot, false);
				return;
			}
			NKMUnitSkillManager.GetUnlockReqUpgradeFromSkillId(skillTemplet.m_ID);
			NKCUtil.SetGameobjectActive(this.m_objSkillLockRoot, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
			NKCUtil.SetSkillUnlockStarRank(this.m_lstObjSkillLockStar, skillTemplet, unitTempletBase.m_StarGradeMax);
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x0021A879 File Offset: 0x00218A79
		private void OnSelectSlot(NKMUnitSkillTemplet skillTemplet)
		{
			if (skillTemplet != null)
			{
				this.SelectSkill(this.UserData, this.m_UnitData, skillTemplet.m_ID, true);
			}
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x0021A898 File Offset: 0x00218A98
		public void StartTrain()
		{
			if (this.m_currentSkill == null)
			{
				return;
			}
			if (this.UserData == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanTrainSkill(this.UserData, this.m_UnitData, this.CurrentSkillID);
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				if (this.dOnTrySkillTrain != null)
				{
					this.dOnTrySkillTrain(this.m_UnitData.m_UnitUID, this.CurrentSkillID);
					return;
				}
			}
			else
			{
				if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NEED_LIMIT_BREAK)
				{
					NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NOT_ENOUGH_ITEM)
				{
					int unitSkillLevel = this.m_UnitData.GetUnitSkillLevel(this.CurrentSkillID);
					NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(this.CurrentSkillID, unitSkillLevel + 1);
					if (skillTemplet != null && this.UserData != null && this.UserData.m_InventoryData != null)
					{
						for (int i = 0; i < skillTemplet.m_lstUpgradeReqItem.Count; i++)
						{
							if (this.UserData.m_InventoryData.GetCountMiscItem(skillTemplet.m_lstUpgradeReqItem[i].ItemID) < (long)skillTemplet.m_lstUpgradeReqItem[i].ItemCount)
							{
								NKCShopManager.OpenItemLackPopup(skillTemplet.m_lstUpgradeReqItem[i].ItemID, skillTemplet.m_lstUpgradeReqItem[i].ItemCount);
								return;
							}
						}
						return;
					}
				}
				else if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING || nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING)
				{
					NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
			}
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x0021A9F3 File Offset: 0x00218BF3
		public void UnitUpdated(long uid, NKMUnitData unitData)
		{
			if (this.m_UnitData != null && this.m_UnitData.m_UnitUID == uid && unitData != null)
			{
				this.SetData(this.UserData, unitData, this.CurrentSkillID, true);
			}
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x0021AA22 File Offset: 0x00218C22
		public void SwitchSkillBack(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_LAB_TRAINING_SKILL_LV_BACK, bActive);
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x0021AA30 File Offset: 0x00218C30
		public void OnSkillLevelUp(int skillID)
		{
			if (skillID == 0)
			{
				for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
				{
					this.m_lstSkillSlot[i].ShowEffect(false);
				}
				return;
			}
			for (int j = 0; j < this.m_lstSkillSlot.Count; j++)
			{
				this.m_lstSkillSlot[j].ShowEffect(this.m_lstSkillSlot[j].CurrentSkillID == skillID);
			}
		}

		// Token: 0x04005421 RID: 21537
		private const int HYPER_SKILL_SLOT_INDEX = 3;

		// Token: 0x04005422 RID: 21538
		public List<NKCUILabSkillTrain.UnitSkillSlot> m_lstSkillSlot;

		// Token: 0x04005423 RID: 21539
		[Header("상세정보")]
		public GameObject m_objRootSkillDetail;

		// Token: 0x04005424 RID: 21540
		public Text m_lbSkillName;

		// Token: 0x04005425 RID: 21541
		public Image m_imgSkillLock;

		// Token: 0x04005426 RID: 21542
		public Text m_lbSkillType;

		// Token: 0x04005427 RID: 21543
		public GameObject m_objSkillCooldown;

		// Token: 0x04005428 RID: 21544
		public Text m_lbSkillCooldown;

		// Token: 0x04005429 RID: 21545
		public GameObject m_objSkillAttackCount;

		// Token: 0x0400542A RID: 21546
		public Text m_lbSkillAttackCount;

		// Token: 0x0400542B RID: 21547
		public GameObject m_objHyperSkillGlowEffect;

		// Token: 0x0400542C RID: 21548
		public GameObject m_objSkillLockRoot;

		// Token: 0x0400542D RID: 21549
		public GameObject m_NKM_UI_LAB_TRAINING_SKILL_LV_BACK;

		// Token: 0x0400542E RID: 21550
		public List<GameObject> m_lstObjSkillLockStar;

		// Token: 0x0400542F RID: 21551
		public ScrollRect m_SkillDescScrollRect;

		// Token: 0x04005430 RID: 21552
		public Text m_lbSkillDescription;

		// Token: 0x04005431 RID: 21553
		[Header("스킬 레벨 정보")]
		public List<NKCUIComSkillLevelDetail> m_lstSkillLevelDetail;

		// Token: 0x04005432 RID: 21554
		[Header("훈련 필요 아이템")]
		public List<NKCUIItemCostSlot> m_lstCostItemUI;

		// Token: 0x04005433 RID: 21555
		public int m_QuantityCheckNum = 1;

		// Token: 0x04005434 RID: 21556
		public GameObject m_objLimitBreak;

		// Token: 0x04005435 RID: 21557
		public NKCUIItemCostSlot m_itemLimitBreak;

		// Token: 0x04005436 RID: 21558
		[Header("버튼")]
		public NKCUIComStateButton m_csbtnToSkillMenu;

		// Token: 0x04005437 RID: 21559
		public GameObject m_objButtonEffect;

		// Token: 0x04005438 RID: 21560
		public Text m_NKM_UI_LAB_TRAINING_ENTER_TEXT;

		// Token: 0x04005439 RID: 21561
		public Image m_ImgNKM_UI_LAB_TRAINING_ENTER_ICON;

		// Token: 0x0400543A RID: 21562
		private NKCUILabSkillTrain.OnTrySkillTrain dOnTrySkillTrain;

		// Token: 0x0400543B RID: 21563
		private NKMUnitSkillTemplet m_currentSkill;

		// Token: 0x0400543C RID: 21564
		private NKMUnitData m_UnitData;

		// Token: 0x020016A6 RID: 5798
		[Serializable]
		public class UnitSkillSlot
		{
			// Token: 0x0600B0D4 RID: 45268 RVA: 0x0035F9C0 File Offset: 0x0035DBC0
			public void Init(NKCUISkillSlot.OnClickSkillSlot onClick)
			{
				if (this.m_slot != null)
				{
					this.m_slot.Init(onClick);
				}
				NKCUtil.SetGameobjectActive(this.m_objEffect, false);
			}

			// Token: 0x0600B0D5 RID: 45269 RVA: 0x0035F9E8 File Offset: 0x0035DBE8
			public void Cleanup()
			{
				if (this.m_slot != null)
				{
					this.m_slot.Cleanup();
				}
				NKCUtil.SetGameobjectActive(this.m_objEffect, false);
			}

			// Token: 0x0600B0D6 RID: 45270 RVA: 0x0035FA0F File Offset: 0x0035DC0F
			public void ShowEffect(bool value)
			{
				if (value)
				{
					NKCUtil.SetGameobjectActive(this.m_objEffect, false);
					NKCUtil.SetGameobjectActive(this.m_objEffect, true);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objEffect, false);
			}

			// Token: 0x0600B0D7 RID: 45271 RVA: 0x0035FA39 File Offset: 0x0035DC39
			public void SetData(NKMUnitSkillTemplet unitSkillTemplet, bool bIsHyper)
			{
				if (this.m_slot != null)
				{
					this.m_slot.SetData(unitSkillTemplet, bIsHyper);
				}
			}

			// Token: 0x0600B0D8 RID: 45272 RVA: 0x0035FA56 File Offset: 0x0035DC56
			public void LockSkill(bool value)
			{
				if (this.m_slot != null)
				{
					this.m_slot.LockSkill(value);
				}
			}

			// Token: 0x0600B0D9 RID: 45273 RVA: 0x0035FA72 File Offset: 0x0035DC72
			public void SetHighlight(bool value)
			{
				if (this.m_slot != null)
				{
					this.m_slot.SetHighlight(value);
				}
			}

			// Token: 0x17001915 RID: 6421
			// (get) Token: 0x0600B0DA RID: 45274 RVA: 0x0035FA8E File Offset: 0x0035DC8E
			public int CurrentSkillID
			{
				get
				{
					if (!(this.m_slot != null))
					{
						return 0;
					}
					return this.m_slot.CurrentSkillID;
				}
			}

			// Token: 0x0400A4F5 RID: 42229
			public NKCUISkillSlot m_slot;

			// Token: 0x0400A4F6 RID: 42230
			public GameObject m_objEffect;
		}

		// Token: 0x020016A7 RID: 5799
		// (Invoke) Token: 0x0600B0DD RID: 45277
		public delegate void OnTrySkillTrain(long targetUnitUID, int skillID);
	}
}

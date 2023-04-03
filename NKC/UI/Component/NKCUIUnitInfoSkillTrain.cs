using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C63 RID: 3171
	public class NKCUIUnitInfoSkillTrain : MonoBehaviour
	{
		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06009396 RID: 37782 RVA: 0x00325F1A File Offset: 0x0032411A
		public int SelectedSkillID
		{
			get
			{
				if (this.m_SkillTemplet == null)
				{
					return -1;
				}
				return this.m_SkillTemplet.m_ID;
			}
		}

		// Token: 0x06009397 RID: 37783 RVA: 0x00325F34 File Offset: 0x00324134
		private void OnDisable()
		{
			for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
			{
				this.m_lstSkillSlot[i].ShowEffect(false);
			}
		}

		// Token: 0x06009398 RID: 37784 RVA: 0x00325F6C File Offset: 0x0032416C
		public void Init()
		{
			for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
			{
				this.m_lstSkillSlot[i].Init(new NKCUISkillSlot.OnClickSkillSlot(this.OnSelectSlot));
			}
			NKCUtil.SetBindFunction(this.m_csbtnToSkillMenu, new UnityAction(this.OnClickSkillTrain));
		}

		// Token: 0x06009399 RID: 37785 RVA: 0x00325FC4 File Offset: 0x003241C4
		public void Clear()
		{
			foreach (NKCUIUnitInfoSkillTrain.UnitSkillSlot unitSkillSlot in this.m_lstSkillSlot)
			{
				unitSkillSlot.Cleanup();
			}
		}

		// Token: 0x0600939A RID: 37786 RVA: 0x00326014 File Offset: 0x00324214
		private void OnSelectSlot(NKMUnitSkillTemplet skillTemplet)
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			if (skillTemplet != null)
			{
				this.SelectSkill(skillTemplet.m_ID, true);
			}
		}

		// Token: 0x0600939B RID: 37787 RVA: 0x0032603A File Offset: 0x0032423A
		public void OnUnitUpdate(long uid, NKMUnitData unitData)
		{
			if (this.m_UnitData != null && this.m_UnitData.m_UnitUID == uid && unitData != null)
			{
				this.SetData(unitData, this.m_SkillTemplet.m_ID, true);
			}
		}

		// Token: 0x0600939C RID: 37788 RVA: 0x00326068 File Offset: 0x00324268
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			using (List<NKCUIItemCostSlot>.Enumerator enumerator = this.m_lstCostItemUI.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ItemID == itemData.ItemID)
					{
						this.UpdateRequiredItemData(this.m_SkillTemplet.m_ID, this.m_SkillTemplet.m_Level + 1);
						this.UpdateSkillTrainButton(this.m_SkillTemplet.m_ID, true);
						break;
					}
				}
			}
		}

		// Token: 0x0600939D RID: 37789 RVA: 0x003260F4 File Offset: 0x003242F4
		public void SetData(NKMUnitData unitData, int selectedSkillID = -1, bool bSkillUpAni = false)
		{
			this.m_UnitData = unitData;
			this.m_SkillTemplet = null;
			if (this.m_UnitData.GetUnitSkillCount() <= 0)
			{
				Debug.Log("Unit have no skill. unitID : " + unitData.m_UnitID.ToString());
				return;
			}
			List<NKMUnitSkillTemplet> unitAllSkillTemplets = NKMUnitSkillManager.GetUnitAllSkillTemplets(unitData);
			if (selectedSkillID != -1 && !unitAllSkillTemplets.Exists((NKMUnitSkillTemplet x) => x.m_ID == selectedSkillID))
			{
				selectedSkillID = -1;
			}
			bool flag = false;
			int num = 1;
			for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
			{
				if (unitAllSkillTemplets.Count > i)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[0].m_slot, flag);
					NKMUnitSkillTemplet nkmunitSkillTemplet = unitAllSkillTemplets[i];
					if (nkmunitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
					{
						flag = true;
						this.m_lstSkillSlot[0].SetData(nkmunitSkillTemplet, false);
						if (selectedSkillID == -1)
						{
							selectedSkillID = nkmunitSkillTemplet.m_ID;
						}
					}
					else
					{
						this.m_lstSkillSlot[num].SetData(nkmunitSkillTemplet, nkmunitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER);
						NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[num].m_slot, true);
						num++;
					}
				}
			}
			for (int j = num; j < this.m_lstSkillSlot.Count; j++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[j].m_slot, false);
			}
			if (flag)
			{
				this.OnSkillLevelUp(0);
			}
			NKCUtil.SetGameobjectActive(this.m_objLeaderSkill, flag);
			NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[0].m_slot, flag);
			if (selectedSkillID == -1 && unitAllSkillTemplets.Count > 0)
			{
				selectedSkillID = unitAllSkillTemplets[0].m_ID;
			}
			this.SelectSkill(selectedSkillID, bSkillUpAni);
		}

		// Token: 0x0600939E RID: 37790 RVA: 0x003262C4 File Offset: 0x003244C4
		private void SelectSkill(int skillID, bool bAnimate = true)
		{
			NKMUnitSkillTemplet unitSkillTemplet = NKMUnitSkillManager.GetUnitSkillTemplet(skillID, this.m_UnitData);
			this.m_SkillTemplet = unitSkillTemplet;
			this.SetHighlightSlot(skillID);
			this.SetSkillDetail(unitSkillTemplet);
			this.UpdateSkillTrainButton(skillID, bAnimate);
		}

		// Token: 0x0600939F RID: 37791 RVA: 0x003262FC File Offset: 0x003244FC
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

		// Token: 0x060093A0 RID: 37792 RVA: 0x00326370 File Offset: 0x00324570
		public void OnClickSkillTrain()
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanTrainSkill(this.m_UnitData, this.m_SkillTemplet.m_ID);
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				NKCPacketSender.Send_Packet_NKMPacket_UNIT_SKILL_UPGRADE_REQ(this.m_UnitData.m_UnitUID, this.m_SkillTemplet.m_ID);
				return;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NEED_LIMIT_BREAK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NOT_ENOUGH_ITEM)
			{
				int unitSkillLevel = this.m_UnitData.GetUnitSkillLevel(this.m_SkillTemplet.m_ID);
				NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(this.m_SkillTemplet.m_ID, unitSkillLevel + 1);
				NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
				if (skillTemplet != null && inventoryData != null)
				{
					for (int i = 0; i < skillTemplet.m_lstUpgradeReqItem.Count; i++)
					{
						if (inventoryData.GetCountMiscItem(skillTemplet.m_lstUpgradeReqItem[i].ItemID) < (long)skillTemplet.m_lstUpgradeReqItem[i].ItemCount)
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

		// Token: 0x060093A1 RID: 37793 RVA: 0x003264C4 File Offset: 0x003246C4
		private NKM_ERROR_CODE CanTrainSkill(NKMUnitData targetUnit, int skillID)
		{
			if (this.m_UnitData == null || targetUnit == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (targetUnit.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMDeckData deckDataByUnitUID = nkmuserData.m_ArmyData.GetDeckDataByUnitUID(targetUnit.m_UnitUID);
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
			return NKMUnitSkillManager.CanTrainSkill(nkmuserData, targetUnit, skillID);
		}

		// Token: 0x060093A2 RID: 37794 RVA: 0x00326530 File Offset: 0x00324730
		private void UpdateSkillTrainButton(int skillID, bool bAnimate = true)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanTrainSkill(this.m_UnitData, skillID);
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

		// Token: 0x060093A3 RID: 37795 RVA: 0x0032662C File Offset: 0x0032482C
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
			this.m_lbSkillType.color = NKCUtil.GetSkillTypeColor(skillTemplet.m_NKM_SKILL_TYPE);
			this.m_lbSkillType.text = NKCUtilString.GetSkillTypeName(skillTemplet.m_NKM_SKILL_TYPE);
			this.SetUnlockReqUpgradeCount(skillTemplet);
			if (skillTemplet.m_fCooltimeSecond > 0f)
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillCooldown, true);
				NKMUnitTemplet unitTemplet = this.m_UnitData.GetUnitTemplet();
				bool flag2;
				if (unitTemplet == null)
				{
					flag2 = false;
				}
				else
				{
					NKMUnitTempletBase unitTempletBase = unitTemplet.m_UnitTempletBase;
					bool? flag3 = (unitTempletBase != null) ? new bool?(unitTempletBase.StopDefaultCoolTime) : null;
					bool flag4 = true;
					flag2 = (flag3.GetValueOrDefault() == flag4 & flag3 != null);
				}
				if (flag2)
				{
					NKCUtil.SetLabelText(this.m_lbSkillCooldown, string.Format(NKCUtilString.GET_STRING_COUNT_ONE_PARAM, skillTemplet.m_fCooltimeSecond));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbSkillCooldown, string.Format(NKCUtilString.GET_STRING_COOLTIME_ONE_PARAM, skillTemplet.m_fCooltimeSecond));
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillCooldown, false);
			}
			int num = (skillTemplet == null) ? 0 : skillTemplet.m_AttackCount;
			if (num > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objSkillAttackCount, true);
				NKCUtil.SetLabelText(this.m_lbSkillAttackCount, string.Format(NKCUtilString.GET_STRING_SKILL_ATTACK_COUNT_ONE_PARAM, num));
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

		// Token: 0x060093A4 RID: 37796 RVA: 0x00326940 File Offset: 0x00324B40
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
			NKCUtil.SetGameobjectActive(this.m_objSkillLockRoot, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
			NKCUtil.SetSkillUnlockStarRank(this.m_lstObjSkillLockStar, skillTemplet, unitTempletBase.m_StarGradeMax);
		}

		// Token: 0x060093A5 RID: 37797 RVA: 0x003269BC File Offset: 0x00324BBC
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

		// Token: 0x060093A6 RID: 37798 RVA: 0x00326B28 File Offset: 0x00324D28
		public void OnSkillLevelUp(int skillID = 0)
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

		// Token: 0x04008083 RID: 32899
		[Header("스킬")]
		public GameObject m_objLeaderSkill;

		// Token: 0x04008084 RID: 32900
		public List<NKCUIUnitInfoSkillTrain.UnitSkillSlot> m_lstSkillSlot;

		// Token: 0x04008085 RID: 32901
		public GameObject m_objRootSkillDetail;

		// Token: 0x04008086 RID: 32902
		public Text m_lbSkillName;

		// Token: 0x04008087 RID: 32903
		public Image m_imgSkillLock;

		// Token: 0x04008088 RID: 32904
		public Text m_lbSkillType;

		// Token: 0x04008089 RID: 32905
		public GameObject m_objSkillCooldown;

		// Token: 0x0400808A RID: 32906
		public Text m_lbSkillCooldown;

		// Token: 0x0400808B RID: 32907
		public GameObject m_objSkillAttackCount;

		// Token: 0x0400808C RID: 32908
		public Text m_lbSkillAttackCount;

		// Token: 0x0400808D RID: 32909
		[Space]
		public GameObject m_objHyperSkillGlowEffect;

		// Token: 0x0400808E RID: 32910
		public GameObject m_objSkillLockRoot;

		// Token: 0x0400808F RID: 32911
		public List<GameObject> m_lstObjSkillLockStar;

		// Token: 0x04008090 RID: 32912
		public ScrollRect m_SkillDescScrollRect;

		// Token: 0x04008091 RID: 32913
		public Text m_lbSkillDescription;

		// Token: 0x04008092 RID: 32914
		[Header("스킬 레벨 정보")]
		public List<NKCUIComSkillLevelDetail> m_lstSkillLevelDetail;

		// Token: 0x04008093 RID: 32915
		[Header("훈련 필요 아이템")]
		public List<NKCUIItemCostSlot> m_lstCostItemUI;

		// Token: 0x04008094 RID: 32916
		public int m_QuantityCheckNum = 1;

		// Token: 0x04008095 RID: 32917
		public GameObject m_objLimitBreak;

		// Token: 0x04008096 RID: 32918
		public NKCUIItemCostSlot m_itemLimitBreak;

		// Token: 0x04008097 RID: 32919
		[Header("훈련 버튼")]
		public NKCUIComStateButton m_csbtnToSkillMenu;

		// Token: 0x04008098 RID: 32920
		public GameObject m_objButtonEffect;

		// Token: 0x04008099 RID: 32921
		public Text m_NKM_UI_LAB_TRAINING_ENTER_TEXT;

		// Token: 0x0400809A RID: 32922
		public Image m_ImgNKM_UI_LAB_TRAINING_ENTER_ICON;

		// Token: 0x0400809B RID: 32923
		private NKMUnitSkillTemplet m_SkillTemplet;

		// Token: 0x0400809C RID: 32924
		private NKMUnitData m_UnitData;

		// Token: 0x02001A1E RID: 6686
		[Serializable]
		public class UnitSkillSlot
		{
			// Token: 0x0600BB1D RID: 47901 RVA: 0x0036E727 File Offset: 0x0036C927
			public void Init(NKCUISkillSlot.OnClickSkillSlot onClick)
			{
				if (this.m_slot != null)
				{
					this.m_slot.Init(onClick);
				}
				NKCUtil.SetGameobjectActive(this.m_objEffect, false);
			}

			// Token: 0x0600BB1E RID: 47902 RVA: 0x0036E74F File Offset: 0x0036C94F
			public void Cleanup()
			{
				if (this.m_slot != null)
				{
					this.m_slot.Cleanup();
				}
				NKCUtil.SetGameobjectActive(this.m_objEffect, false);
			}

			// Token: 0x0600BB1F RID: 47903 RVA: 0x0036E776 File Offset: 0x0036C976
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

			// Token: 0x0600BB20 RID: 47904 RVA: 0x0036E7A0 File Offset: 0x0036C9A0
			public void SetData(NKMUnitSkillTemplet unitSkillTemplet, bool bIsHyper)
			{
				if (this.m_slot != null)
				{
					this.m_slot.SetData(unitSkillTemplet, bIsHyper);
				}
			}

			// Token: 0x0600BB21 RID: 47905 RVA: 0x0036E7BD File Offset: 0x0036C9BD
			public void LockSkill(bool value)
			{
				if (this.m_slot != null)
				{
					this.m_slot.LockSkill(value);
				}
			}

			// Token: 0x0600BB22 RID: 47906 RVA: 0x0036E7D9 File Offset: 0x0036C9D9
			public void SetHighlight(bool value)
			{
				if (this.m_slot != null)
				{
					this.m_slot.SetHighlight(value);
				}
			}

			// Token: 0x170019F5 RID: 6645
			// (get) Token: 0x0600BB23 RID: 47907 RVA: 0x0036E7F5 File Offset: 0x0036C9F5
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

			// Token: 0x0400ADBD RID: 44477
			public NKCUISkillSlot m_slot;

			// Token: 0x0400ADBE RID: 44478
			public GameObject m_objEffect;
		}
	}
}

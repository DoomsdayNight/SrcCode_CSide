using System;
using System.Collections.Generic;
using NKC.PacketHandler;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009AF RID: 2479
	public class NKCUILabLimitBreak : MonoBehaviour
	{
		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06006802 RID: 26626 RVA: 0x002192B4 File Offset: 0x002174B4
		private NKMUserData UserData
		{
			get
			{
				return NKCScenManager.CurrentUserData();
			}
		}

		// Token: 0x06006803 RID: 26627 RVA: 0x002192BC File Offset: 0x002174BC
		public void Init(NKCUILabLimitBreak.OnTryLimitBreak onTryLimitBreak)
		{
			this.dOnTryLimitBreak = onTryLimitBreak;
			foreach (NKCUIUnitSelectListSlot nkcuiunitSelectListSlot in this.m_lstUISelectSlot)
			{
				nkcuiunitSelectListSlot.Init(false);
				nkcuiunitSelectListSlot.SetDenied(false, null);
			}
			this.m_csbtnLimitBreak.PointerClick.RemoveAllListeners();
			this.m_csbtnLimitBreak.PointerClick.AddListener(new UnityAction(this.OnClickLimitBreak));
			this.m_csbtnTranscendence.PointerClick.RemoveAllListeners();
			this.m_csbtnTranscendence.PointerClick.AddListener(new UnityAction(this.OnClickLimitBreak));
			this.m_csbtnInformation.PointerClick.RemoveAllListeners();
			this.m_csbtnInformation.PointerClick.AddListener(new UnityAction(this.OnClickInformation));
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x002193A0 File Offset: 0x002175A0
		public void Cleanup()
		{
			this.m_targetUnitData = null;
			this.m_targetLBTemplet = null;
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x002193B0 File Offset: 0x002175B0
		public void SetData(NKMUnitData targetUnitData, NKMUserData userData)
		{
			NKMArmyData armyData = userData.m_ArmyData;
			this.m_targetUnitData = targetUnitData;
			NKMUnitLimitBreakManager.UnitLimitBreakStatus unitLimitbreakStatus = NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(this.m_targetUnitData);
			NKCUtil.SetGameobjectActive(this.m_objMaxLevelRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax);
			NKCUtil.SetGameobjectActive(this.m_objNormalRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanLimitBreak || unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakLevelNotEnough);
			NKCUtil.SetGameobjectActive(this.m_objTCMaxLevelRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax);
			NKCUtil.SetGameobjectActive(this.m_objTranscendenceRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough || unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence);
			NKCUtil.SetGameobjectActive(this.m_objEmptyRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.Invalid);
			if (unitLimitbreakStatus > NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough)
			{
				if (unitLimitbreakStatus - NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence <= 1)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnLimitBreak, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnTranscendence, true);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnLimitBreak, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnTranscendence, false);
			}
			switch (unitLimitbreakStatus)
			{
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.Invalid:
				NKCUtil.SetStarRank(this.lstObjStarMaxLevel, 0, 0);
				this.m_targetLBTemplet = null;
				this.m_lbRequiredLevel.SetData(0, 0, 0L, true, true, false);
				this.SetSubstituteItemData(targetUnitData, userData.m_InventoryData);
				this.LockLimitBreakButton(true);
				return;
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax:
				NKCUtil.SetStarRank(this.lstObjStarMaxLevel, targetUnitData);
				this.m_targetLBTemplet = null;
				this.m_lbRequiredLevel.SetData(0, 0, 0L, true, true, false);
				this.SetSubstituteItemData(targetUnitData, userData.m_InventoryData);
				this.LockLimitBreakButton(true);
				return;
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax:
				NKCUtil.SetStarRank(this.m_lstObjStarTCMaxLevel, targetUnitData);
				this.m_targetLBTemplet = null;
				this.m_lbRequiredLevel.SetData(0, 0, 0L, true, true, false);
				this.SetSubstituteItemData(targetUnitData, userData.m_InventoryData);
				this.LockLimitBreakButton(true);
				return;
			}
			NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo((int)targetUnitData.m_LimitBreakLevel);
			NKMLimitBreakTemplet lbinfo2 = NKMUnitLimitBreakManager.GetLBInfo((int)(targetUnitData.m_LimitBreakLevel + 1));
			this.m_targetLBTemplet = lbinfo2;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetUnitData.m_UnitID);
			int starGrade = targetUnitData.GetStarGrade(unitTempletBase);
			if (unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough || unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence)
			{
				bool flag = lbinfo2 == null || NKMUnitLimitBreakManager.GetLBInfo((int)(targetUnitData.m_LimitBreakLevel + 2)) == null;
				NKCUIComStarRank comStarRankTC = this.m_comStarRankTC;
				if (comStarRankTC != null)
				{
					comStarRankTC.SetStarRank(starGrade, unitTempletBase.m_StarGradeMax, flag);
				}
				NKCUtil.SetGameobjectActive(this.m_objTranscendenceFxYellow, !flag);
				NKCUtil.SetGameobjectActive(this.m_objTranscendenceFxPurple, flag);
				if (lbinfo != null)
				{
					this.m_lbTCMaxLevelBefore.SetText(string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo.m_iMaxLevel), NKMUnitLimitBreakManager.IsTranscendenceUnit(targetUnitData), Array.Empty<Text>());
				}
				if (lbinfo2 != null)
				{
					int num = lbinfo2.m_iLBRank - 3;
					NKCUtil.SetLabelText(this.m_lbTCLevel, NKCUtilString.GET_STRING_LIMITBREAK_TRANSCENDENCE_LEVEL_ONE_PARAM, new object[]
					{
						num
					});
					NKCUtil.SetLabelText(this.m_lbTCMaxLevelAfter, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo2.m_iMaxLevel));
					NKCUIItemCostSlot lbRequiredLevel = this.m_lbRequiredLevel;
					if (lbRequiredLevel != null)
					{
						lbRequiredLevel.SetData(910, lbinfo2.m_iRequiredLevel, (long)targetUnitData.m_UnitLevel, true, true, false);
					}
				}
				else
				{
					Debug.LogError("Next LBTemplet Not Found!");
					NKCUtil.SetLabelText(this.m_lbTCLevel, NKCUtilString.GET_STRING_LIMITBREAK_TRANSCENDENCE_LEVEL_ONE_PARAM, new object[]
					{
						0
					});
				}
				float num2 = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)(targetUnitData.m_LimitBreakLevel + 1)) - NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)targetUnitData.m_LimitBreakLevel);
				NKCUtil.SetLabelText(this.m_lbTCPowerupRate, string.Format(NKCUtilString.GET_STRING_LIMITBREAK_GROWTH_INFO_ONE_PARAM, num2 * 100f));
			}
			else
			{
				NKCUtil.SetStarRank(this.lstObjStarBefore, starGrade, unitTempletBase.m_StarGradeMax);
				NKCUtil.SetStarRank(this.lstObjStarAfter, starGrade + 1, unitTempletBase.m_StarGradeMax);
				GameObject gameObject = this.lstObjStarAfter[starGrade];
				if (gameObject != null)
				{
					NKCUtil.SetGameobjectActive(this.m_rtStarEffect.gameObject, true);
					this.m_rtStarEffect.SetParent(gameObject.transform);
					this.m_rtStarEffect.anchoredPosition = Vector2.zero;
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_rtStarEffect.gameObject, false);
				}
				if (lbinfo != null)
				{
					this.m_lbMaxLevelBefore.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo.m_iMaxLevel);
				}
				if (lbinfo2 != null)
				{
					this.m_lbMaxLevelAfter.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo2.m_iMaxLevel);
					NKCUIItemCostSlot lbRequiredLevel2 = this.m_lbRequiredLevel;
					if (lbRequiredLevel2 != null)
					{
						lbRequiredLevel2.SetData(910, lbinfo2.m_iRequiredLevel, (long)targetUnitData.m_UnitLevel, true, true, false);
					}
				}
				float num3 = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)(targetUnitData.m_LimitBreakLevel + 1)) - NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)targetUnitData.m_LimitBreakLevel);
				this.m_lbGrowthInfo.text = string.Format(NKCUtilString.GET_STRING_LIMITBREAK_GROWTH_INFO_ONE_PARAM, num3 * 100f);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_LAB_TRANSCENDENCE_INFO_DETAIL_02, targetUnitData.m_LimitBreakLevel + 1 == 3);
			this.SetSubstituteItemData(targetUnitData, userData.m_InventoryData);
			List<NKMItemMiscData> list;
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanLimitBreak(this.UserData, this.m_targetUnitData, out list);
			this.LockLimitBreakButton(nkm_ERROR_CODE > NKM_ERROR_CODE.NEC_OK);
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x00219857 File Offset: 0x00217A57
		public void UpdateSubstituteItemData()
		{
			this.SetSubstituteItemData(this.m_targetUnitData, NKCScenManager.CurrentUserData().m_InventoryData);
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x00219870 File Offset: 0x00217A70
		private void SetSubstituteItemData(NKMUnitData targetUnitData, NKMInventoryData inventory)
		{
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(targetUnitData);
			for (int i = 0; i < this.m_lstSubstituteItemUI.Count; i++)
			{
				NKCUIItemCostSlot nkcuiitemCostSlot = this.m_lstSubstituteItemUI[i];
				if (lbsubstituteItemInfo != null && i < lbsubstituteItemInfo.m_lstRequiredItem.Count)
				{
					int itemID = lbsubstituteItemInfo.m_lstRequiredItem[i].itemID;
					if (this.m_targetLBTemplet != null)
					{
						int count = lbsubstituteItemInfo.m_lstRequiredItem[i].count;
						if (count > 0)
						{
							nkcuiitemCostSlot.SetData(itemID, count, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemID), true, true, false);
							NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, true);
						}
						else
						{
							NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, false);
						}
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, false);
				}
			}
			this.UpdateRequiredCredit(targetUnitData);
		}

		// Token: 0x06006808 RID: 26632 RVA: 0x0021992C File Offset: 0x00217B2C
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData.ItemID == 1)
			{
				this.UpdateRequiredCredit(this.m_targetUnitData);
				this.UpdateLimitBreakButton();
				return;
			}
			using (List<NKCUIItemCostSlot>.Enumerator enumerator = this.m_lstSubstituteItemUI.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ItemID == itemData.ItemID)
					{
						this.UpdateSubstituteItemData();
						this.UpdateLimitBreakButton();
						break;
					}
				}
			}
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x002199B0 File Offset: 0x00217BB0
		private void UpdateRequiredCredit(NKMUnitData targetUnitData)
		{
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(targetUnitData);
			if (lbsubstituteItemInfo != null)
			{
				this.m_lbCreditRequired.SetData(1, lbsubstituteItemInfo.m_CreditReq, NKCScenManager.CurrentUserData().GetCredit(), true, true, false);
				return;
			}
			this.m_lbCreditRequired.SetData(0, 0, 0L, true, true, false);
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x002199FC File Offset: 0x00217BFC
		private bool CheckConsumedUnit(List<long> listUnitUID)
		{
			for (int i = 0; i < listUnitUID.Count; i++)
			{
				if (this.CheckConsumedUnit(listUnitUID[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x00219A2C File Offset: 0x00217C2C
		private bool CheckConsumedUnit(long unitUID)
		{
			if (this.UserData != null)
			{
				NKMUnitData unitFromUID = this.UserData.m_ArmyData.GetUnitFromUID(unitUID);
				if (unitFromUID != null)
				{
					return unitFromUID.m_LimitBreakLevel > 0;
				}
			}
			return true;
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x00219A61 File Offset: 0x00217C61
		private bool CheckSelectedUnitWarning(long selectUID, List<long> selectedList, out string msg)
		{
			msg = string.Empty;
			if (this.CheckConsumedUnit(selectUID))
			{
				msg = NKCUtilString.GET_STRING_LIMITBREAK_WARNING_SELECT_UNIT;
				return true;
			}
			return false;
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x00219A7D File Offset: 0x00217C7D
		private void LockLimitBreakButton(bool value)
		{
			if (value)
			{
				this.m_csbtnLimitBreak.Lock(false);
				this.m_csbtnTranscendence.Lock(false);
				return;
			}
			this.m_csbtnLimitBreak.UnLock(false);
			this.m_csbtnTranscendence.UnLock(false);
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x00219AB4 File Offset: 0x00217CB4
		private void UpdateLimitBreakButton()
		{
			List<NKMItemMiscData> list;
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanLimitBreak(this.UserData, this.m_targetUnitData, out list);
			this.LockLimitBreakButton(nkm_ERROR_CODE > NKM_ERROR_CODE.NEC_OK);
		}

		// Token: 0x0600680F RID: 26639 RVA: 0x00219AE0 File Offset: 0x00217CE0
		private NKM_ERROR_CODE CanLimitBreak(NKMUserData userData, NKMUnitData targetUnit, out List<NKMItemMiscData> lstCost)
		{
			lstCost = new List<NKMItemMiscData>();
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
			return NKMUnitLimitBreakManager.CanLimitBreak(userData, targetUnit, out lstCost);
		}

		// Token: 0x06006810 RID: 26640 RVA: 0x00219B50 File Offset: 0x00217D50
		public void UnitUpdated(long uid, NKMUnitData unitData)
		{
			if (this.m_targetUnitData != null && uid == this.m_targetUnitData.m_UnitUID)
			{
				this.SetData(unitData, this.UserData);
			}
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x00219B78 File Offset: 0x00217D78
		public void OnClickLimitBreak()
		{
			if (!this.m_csbtnLimitBreak.m_bLock || !this.m_csbtnTranscendence.m_bLock)
			{
				if (this.dOnTryLimitBreak != null && this.m_targetUnitData != null)
				{
					this.RunLimitBreak();
					return;
				}
			}
			else
			{
				if (this.UserData == null || this.m_targetUnitData == null)
				{
					return;
				}
				List<NKMItemMiscData> list;
				NKM_ERROR_CODE nkm_ERROR_CODE = this.CanLimitBreak(this.UserData, this.m_targetUnitData, out list);
				NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(this.m_targetUnitData);
				if (lbsubstituteItemInfo == null)
				{
					return;
				}
				if (nkm_ERROR_CODE <= NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM)
				{
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT)
					{
						if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM)
						{
							if (NKMUnitLimitBreakManager.GetLBInfo((int)(this.m_targetUnitData.m_LimitBreakLevel + 1)) == null)
							{
								return;
							}
							if (lbsubstituteItemInfo.m_lstRequiredItem == null)
							{
								return;
							}
							for (int i = 0; i < lbsubstituteItemInfo.m_lstRequiredItem.Count; i++)
							{
								int itemID = lbsubstituteItemInfo.m_lstRequiredItem[i].itemID;
								int count = lbsubstituteItemInfo.m_lstRequiredItem[i].count;
								long num = 0L;
								if (this.UserData != null)
								{
									num = this.UserData.m_InventoryData.GetCountMiscItem(itemID);
								}
								if (num < (long)count)
								{
									NKCShopManager.OpenItemLackPopup(itemID, count);
									return;
								}
							}
							return;
						}
					}
					else
					{
						if (this.UserData != null && this.UserData.GetCredit() < (long)lbsubstituteItemInfo.m_CreditReq)
						{
							NKCShopManager.OpenItemLackPopup(1, lbsubstituteItemInfo.m_CreditReq);
							return;
						}
						return;
					}
				}
				else
				{
					if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING || nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING)
					{
						NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
					if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_LIMIT_BREAK_TEMPLET_NULL)
					{
						return;
					}
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
			}
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x00219D0B File Offset: 0x00217F0B
		public void OnClickInformation()
		{
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_INFORMATION, NKCUtilString.GET_STRING_LIMITBTEAK_INFO, null, "");
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x00219D24 File Offset: 0x00217F24
		private void RunLimitBreak()
		{
			if (this.dOnTryLimitBreak == null || this.m_targetUnitData == null)
			{
				return;
			}
			int unitLimitbreakStatus = (int)NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(this.m_targetUnitData);
			List<NKCUISlot.SlotData> lstSlot = this.MakeSlotData(this.m_targetUnitData);
			string content;
			if (unitLimitbreakStatus == 5)
			{
				content = NKCUtilString.GET_STRING_LIMITBREAK_CONFIRM_AWAKEN;
			}
			else
			{
				content = NKCUtilString.GET_STRING_LIMITBREAK_CONFIRM;
			}
			NKCPopupResourceConfirmBox.Instance.OpenItemSlotList(NKCUtilString.GET_STRING_NOTICE, content, lstSlot, delegate
			{
				this.dOnTryLimitBreak(this.m_targetUnitData.m_UnitUID);
			}, null, true);
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x00219D8C File Offset: 0x00217F8C
		private List<NKCUISlot.SlotData> MakeSlotData(NKMUnitData targetUnitData)
		{
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo((int)(targetUnitData.m_LimitBreakLevel + 1));
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(targetUnitData);
			if (lbsubstituteItemInfo != null && lbinfo != null)
			{
				list.Add(NKCUISlot.SlotData.MakeMiscItemData(1, (long)lbsubstituteItemInfo.m_CreditReq, 0));
				for (int i = 0; i < lbsubstituteItemInfo.m_lstRequiredItem.Count; i++)
				{
					NKMLimitBreakItemTemplet.ItemRequirement itemRequirement = lbsubstituteItemInfo.m_lstRequiredItem[i];
					int count = itemRequirement.count;
					if (count > 0)
					{
						NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeMiscItemData(itemRequirement.itemID, (long)count, 0);
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x04005402 RID: 21506
		[Header("초월 가능할 때")]
		public GameObject m_objNormalRoot;

		// Token: 0x04005403 RID: 21507
		public List<GameObject> lstObjStarBefore;

		// Token: 0x04005404 RID: 21508
		public List<GameObject> lstObjStarAfter;

		// Token: 0x04005405 RID: 21509
		public RectTransform m_rtStarEffect;

		// Token: 0x04005406 RID: 21510
		public Text m_lbMaxLevelBefore;

		// Token: 0x04005407 RID: 21511
		public Text m_lbMaxLevelAfter;

		// Token: 0x04005408 RID: 21512
		public NKCUIItemCostSlot m_lbRequiredLevel;

		// Token: 0x04005409 RID: 21513
		public Text m_lbGrowthInfo;

		// Token: 0x0400540A RID: 21514
		public GameObject m_NKM_UI_LAB_TRANSCENDENCE_INFO_DETAIL_02;

		// Token: 0x0400540B RID: 21515
		[Header("초월 각성 할 때")]
		public GameObject m_objTranscendenceRoot;

		// Token: 0x0400540C RID: 21516
		public NKCUIComStarRank m_comStarRankTC;

		// Token: 0x0400540D RID: 21517
		public GameObject m_objTranscendenceFxPurple;

		// Token: 0x0400540E RID: 21518
		public GameObject m_objTranscendenceFxYellow;

		// Token: 0x0400540F RID: 21519
		public Text m_lbTCLevel;

		// Token: 0x04005410 RID: 21520
		public NKCUIComTextUnitLevel m_lbTCMaxLevelBefore;

		// Token: 0x04005411 RID: 21521
		public Text m_lbTCMaxLevelAfter;

		// Token: 0x04005412 RID: 21522
		public Text m_lbTCPowerupRate;

		// Token: 0x04005413 RID: 21523
		[Header("초월 레벨 최대일 때")]
		public GameObject m_objMaxLevelRoot;

		// Token: 0x04005414 RID: 21524
		public List<GameObject> lstObjStarMaxLevel;

		// Token: 0x04005415 RID: 21525
		[Header("초월각성 레벨 최대일 때")]
		public GameObject m_objTCMaxLevelRoot;

		// Token: 0x04005416 RID: 21526
		public List<GameObject> m_lstObjStarTCMaxLevel;

		// Token: 0x04005417 RID: 21527
		[Header("비었을 때")]
		public GameObject m_objEmptyRoot;

		// Token: 0x04005418 RID: 21528
		[Header("재료 선택 슬롯")]
		public List<NKCUIUnitSelectListSlot> m_lstUISelectSlot;

		// Token: 0x04005419 RID: 21529
		[Header("대체 아이템 정보")]
		public NKCUIItemCostSlot m_lbCreditRequired;

		// Token: 0x0400541A RID: 21530
		public List<NKCUIItemCostSlot> m_lstSubstituteItemUI;

		// Token: 0x0400541B RID: 21531
		[Header("시작 버튼")]
		public NKCUIComStateButton m_csbtnLimitBreak;

		// Token: 0x0400541C RID: 21532
		public NKCUIComStateButton m_csbtnTranscendence;

		// Token: 0x0400541D RID: 21533
		[Header("기타")]
		public NKCUIComStateButton m_csbtnInformation;

		// Token: 0x0400541E RID: 21534
		private NKCUILabLimitBreak.OnTryLimitBreak dOnTryLimitBreak;

		// Token: 0x0400541F RID: 21535
		private NKMUnitData m_targetUnitData;

		// Token: 0x04005420 RID: 21536
		private NKMLimitBreakTemplet m_targetLBTemplet;

		// Token: 0x020016A5 RID: 5797
		// (Invoke) Token: 0x0600B0D1 RID: 45265
		public delegate void OnTryLimitBreak(long targetUnitUID);
	}
}

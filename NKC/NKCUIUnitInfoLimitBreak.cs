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
	// Token: 0x02000AA7 RID: 2727
	public class NKCUIUnitInfoLimitBreak : MonoBehaviour
	{
		// Token: 0x06007904 RID: 30980 RVA: 0x002832C4 File Offset: 0x002814C4
		public void Init()
		{
			this.m_RearmSubUI.Init(new NKCUIRearmamentSubUISelectList.OnSelectedUnitID(this.SelectedRearmUnit));
			NKCUtil.SetBindFunction(this.m_csbtnLimitBreak, new UnityAction(this.OnClickLimitBreak));
			NKCUtil.SetBindFunction(this.m_csbtnInformation, new UnityAction(this.OnClickInformation));
			NKCUtil.SetBindFunction(this.m_csbtnRearmament, new UnityAction(this.OnClickMoveToRearmment));
			NKCUtil.SetBindFunction(this.m_csbtnTacticUpdate, new UnityAction(this.OnClickTacticUpdate));
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x00283344 File Offset: 0x00281544
		public void Clear()
		{
			if (this.m_UIUnitSelectList != null && NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_UNIT_LIST)
			{
				this.UnitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x00283374 File Offset: 0x00281574
		public void SetData(NKMUnitData unitData)
		{
			this.m_targetUnitData = unitData;
			NKMUnitLimitBreakManager.UnitLimitBreakStatus unitLimitbreakStatus = NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(unitData);
			bool flag = NKCRearmamentUtil.IsCanRearmamentUnit(this.m_targetUnitData.m_UnitUID) && unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax && NKCRearmamentUtil.IsCanUseContent();
			NKCUtil.SetGameobjectActive(this.m_objMaxLevelRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax);
			NKCUtil.SetGameobjectActive(this.m_objNormalRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanLimitBreak || unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakLevelNotEnough);
			NKCUtil.SetGameobjectActive(this.m_objTCMaxLevelRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax);
			NKCUtil.SetGameobjectActive(this.m_objTranscendenceRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough || unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence);
			NKCUtil.SetGameobjectActive(this.m_objEmptyRoot, unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.Invalid);
			NKCUtil.SetGameobjectActive(this.m_csbtnRearmament, flag);
			this.m_csbtnRearmament.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.REARM, 0, 0), false);
			NKCUtil.SetGameobjectActive(this.m_csbtnLimitBreak, !flag);
			this.UpdateRearmUI(flag);
			this.UpdateTacticUpdateUI();
			if (unitLimitbreakStatus > NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough)
			{
				if (unitLimitbreakStatus - NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence <= 1)
				{
					NKCUtil.SetGameobjectActive(this.m_btnUILimitbreak, false);
					NKCUtil.SetGameobjectActive(this.m_btnUITranscendence, true);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_btnUILimitbreak, true);
				NKCUtil.SetGameobjectActive(this.m_btnUITranscendence, false);
			}
			NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo((int)this.m_targetUnitData.m_LimitBreakLevel);
			NKMLimitBreakTemplet lbinfo2 = NKMUnitLimitBreakManager.GetLBInfo((int)(this.m_targetUnitData.m_LimitBreakLevel + 1));
			this.m_targetLBTemplet = lbinfo2;
			this.UpdateSubstituteItemData();
			NKCScenManager.CurrentUserData();
			switch (unitLimitbreakStatus)
			{
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.Invalid:
				NKCUtil.SetStarRank(this.lstObjStarMaxLevel, 0, 0);
				this.m_targetLBTemplet = null;
				this.m_lbRequiredLevel.SetData(0, 0, 0L, true, true, false);
				this.LockLimitBreakButton(true);
				return;
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax:
				NKCUtil.SetStarRank(this.lstObjStarMaxLevel, this.m_targetUnitData);
				this.m_targetLBTemplet = null;
				this.m_lbRequiredLevel.SetData(0, 0, 0L, true, true, false);
				this.LockLimitBreakButton(true);
				return;
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax:
				NKCUtil.SetStarRank(this.m_lstObjStarTCMaxLevel, this.m_targetUnitData);
				this.m_targetLBTemplet = null;
				this.m_lbRequiredLevel.SetData(0, 0, 0L, true, true, false);
				this.LockLimitBreakButton(true);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_targetUnitData.m_UnitID);
			int starGrade = this.m_targetUnitData.GetStarGrade(unitTempletBase);
			if (unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough || unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence)
			{
				bool flag2 = lbinfo2 == null || NKMUnitLimitBreakManager.GetLBInfo((int)(this.m_targetUnitData.m_LimitBreakLevel + 2)) == null;
				NKCUIComStarRank comStarRankTC = this.m_comStarRankTC;
				if (comStarRankTC != null)
				{
					comStarRankTC.SetStarRank(starGrade, unitTempletBase.m_StarGradeMax, flag2);
				}
				NKCUtil.SetGameobjectActive(this.m_objTranscendenceFxYellow, !flag2);
				NKCUtil.SetGameobjectActive(this.m_objTranscendenceFxPurple, flag2);
				if (lbinfo != null)
				{
					this.m_lbTCMaxLevelBefore.SetText(string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo.m_iMaxLevel), NKMUnitLimitBreakManager.IsTranscendenceUnit(this.m_targetUnitData), Array.Empty<Text>());
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
						lbRequiredLevel.SetData(910, lbinfo2.m_iRequiredLevel, (long)this.m_targetUnitData.m_UnitLevel, true, true, false);
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
				float num2 = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)(this.m_targetUnitData.m_LimitBreakLevel + 1)) - NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)this.m_targetUnitData.m_LimitBreakLevel);
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
						lbRequiredLevel2.SetData(910, lbinfo2.m_iRequiredLevel, (long)this.m_targetUnitData.m_UnitLevel, true, true, false);
					}
				}
				float num3 = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)(this.m_targetUnitData.m_LimitBreakLevel + 1)) - NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier((int)this.m_targetUnitData.m_LimitBreakLevel);
				this.m_lbGrowthInfo.text = string.Format(NKCUtilString.GET_STRING_LIMITBREAK_GROWTH_INFO_ONE_PARAM, num3 * 100f);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_LAB_TRANSCENDENCE_INFO_DETAIL_02, this.m_targetUnitData.m_LimitBreakLevel + 1 == 3);
			List<NKMItemMiscData> list;
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanLimitBreak(this.m_targetUnitData, out list);
			this.LockLimitBreakButton(nkm_ERROR_CODE > NKM_ERROR_CODE.NEC_OK);
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x00283890 File Offset: 0x00281A90
		public void UpdateSubstituteItemData()
		{
			this.SetSubstituteItemData(this.m_targetUnitData);
		}

		// Token: 0x06007908 RID: 30984 RVA: 0x002838A0 File Offset: 0x00281AA0
		private void SetSubstituteItemData(NKMUnitData targetUnitData)
		{
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(targetUnitData);
			bool flag = this.IsPossibleLimitBreakStatusUnit(targetUnitData);
			for (int i = 0; i < this.m_lstSubstituteItemUI.Count; i++)
			{
				NKCUIItemCostSlot nkcuiitemCostSlot = this.m_lstSubstituteItemUI[i];
				if (flag && lbsubstituteItemInfo != null && i < lbsubstituteItemInfo.m_lstRequiredItem.Count)
				{
					int itemID = lbsubstituteItemInfo.m_lstRequiredItem[i].itemID;
					if (this.m_targetLBTemplet != null)
					{
						int num = lbsubstituteItemInfo.m_lstRequiredItem[i].count * this.m_targetLBTemplet.m_iUnitRequirement;
						if (num > 0)
						{
							nkcuiitemCostSlot.SetData(itemID, num, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemID), true, true, false);
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

		// Token: 0x06007909 RID: 30985 RVA: 0x0028397C File Offset: 0x00281B7C
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData.ItemID == 1)
			{
				this.UpdateRequiredCredit(this.m_targetUnitData);
				this.UpdateLimitBreakButton();
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

		// Token: 0x0600790A RID: 30986 RVA: 0x00283A00 File Offset: 0x00281C00
		private void UpdateRequiredCredit(NKMUnitData targetUnitData)
		{
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(targetUnitData);
			if (lbsubstituteItemInfo != null && this.IsPossibleLimitBreakStatusUnit(targetUnitData))
			{
				this.m_lbCreditRequired.SetData(1, lbsubstituteItemInfo.m_CreditReq, NKCScenManager.CurrentUserData().GetCredit(), true, true, false);
				return;
			}
			this.m_lbCreditRequired.SetData(0, 0, 0L, true, true, false);
		}

		// Token: 0x0600790B RID: 30987 RVA: 0x00283A54 File Offset: 0x00281C54
		private bool IsPossibleLimitBreakStatusUnit(NKMUnitData unitData)
		{
			NKMUnitLimitBreakManager.UnitLimitBreakStatus unitLimitbreakStatus = NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(unitData);
			return unitLimitbreakStatus != NKMUnitLimitBreakManager.UnitLimitBreakStatus.Invalid && unitLimitbreakStatus != NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax && unitLimitbreakStatus != NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax;
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x0600790C RID: 30988 RVA: 0x00283A76 File Offset: 0x00281C76
		private NKCUIUnitSelectList UnitSelectList
		{
			get
			{
				if (this.m_UIUnitSelectList == null)
				{
					this.m_UIUnitSelectList = NKCUIUnitSelectList.OpenNewInstance(true);
				}
				return this.m_UIUnitSelectList;
			}
		}

		// Token: 0x0600790D RID: 30989 RVA: 0x00283A98 File Offset: 0x00281C98
		private void LockLimitBreakButton(bool value)
		{
			if (value)
			{
				this.m_csbtnLimitBreak.Lock(false);
			}
			else
			{
				this.m_csbtnLimitBreak.UnLock(false);
			}
			foreach (Image image in this.m_lstEnterBtnIcon)
			{
				NKCUtil.SetImageColor(image, NKCUtil.GetButtonUIColor(!value));
			}
			foreach (Text label in this.m_lstEnterBtnText)
			{
				NKCUtil.SetLabelTextColor(label, NKCUtil.GetButtonUIColor(!value));
			}
		}

		// Token: 0x0600790E RID: 30990 RVA: 0x00283B58 File Offset: 0x00281D58
		private void UpdateLimitBreakButton()
		{
			List<NKMItemMiscData> list;
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanLimitBreak(this.m_targetUnitData, out list);
			this.LockLimitBreakButton(nkm_ERROR_CODE > NKM_ERROR_CODE.NEC_OK);
		}

		// Token: 0x0600790F RID: 30991 RVA: 0x00283B80 File Offset: 0x00281D80
		private NKM_ERROR_CODE CanLimitBreak(NKMUnitData targetUnit, out List<NKMItemMiscData> lstCost)
		{
			lstCost = new List<NKMItemMiscData>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (targetUnit == null || nkmuserData == null || nkmuserData.m_ArmyData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (targetUnit.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED;
			}
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
			return NKMUnitLimitBreakManager.CanLimitBreak(nkmuserData, targetUnit, out lstCost);
		}

		// Token: 0x06007910 RID: 30992 RVA: 0x00283BF6 File Offset: 0x00281DF6
		public void OnUnitUpdate(long uid, NKMUnitData unitData)
		{
			if (this.m_targetUnitData != null && uid == this.m_targetUnitData.m_UnitUID)
			{
				this.SetData(unitData);
			}
		}

		// Token: 0x06007911 RID: 30993 RVA: 0x00283C15 File Offset: 0x00281E15
		private void UpdateResource()
		{
			this.UpdateSubstituteItemData();
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x00283C20 File Offset: 0x00281E20
		private void OnClickLimitBreak()
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			if (!this.m_csbtnLimitBreak.m_bLock)
			{
				if (this.m_targetUnitData != null)
				{
					this.RunLimitBreak();
					return;
				}
			}
			else
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null || this.m_targetUnitData == null)
				{
					return;
				}
				List<NKMItemMiscData> list;
				NKM_ERROR_CODE nkm_ERROR_CODE = this.CanLimitBreak(this.m_targetUnitData, out list);
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
							NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo((int)(this.m_targetUnitData.m_LimitBreakLevel + 1));
							if (lbinfo == null)
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
								int num = lbsubstituteItemInfo.m_lstRequiredItem[i].count * lbinfo.m_iUnitRequirement;
								long num2 = 0L;
								if (nkmuserData != null)
								{
									num2 = nkmuserData.m_InventoryData.GetCountMiscItem(itemID);
								}
								if (num2 < (long)num)
								{
									NKCShopManager.OpenItemLackPopup(itemID, num);
									return;
								}
							}
							return;
						}
					}
					else
					{
						if (nkmuserData != null && nkmuserData.GetCredit() < (long)lbsubstituteItemInfo.m_CreditReq)
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

		// Token: 0x06007913 RID: 30995 RVA: 0x00283DA8 File Offset: 0x00281FA8
		private void OnClickInformation()
		{
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_INFORMATION, NKCUtilString.GET_STRING_LIMITBTEAK_INFO, null, "");
		}

		// Token: 0x06007914 RID: 30996 RVA: 0x00283DBF File Offset: 0x00281FBF
		private void OnClickMoveToRearmment()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.REARM, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.REARM, 0);
				return;
			}
			NKCUIRearmament.Instance.SetReserveRearmData(this.m_iRearmTargetUnitID, this.m_targetUnitData.m_UnitUID);
			NKCUIRearmament.Instance.Open(NKCUIRearmament.REARM_TYPE.RT_PROCESS);
		}

		// Token: 0x06007915 RID: 30997 RVA: 0x00283DFC File Offset: 0x00281FFC
		private void RunLimitBreak()
		{
			if (this.m_targetUnitData == null)
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
				NKCPacketSender.Send_Packet_NKMPacket_LIMIT_BREAK_UNIT_REQ(this.m_targetUnitData.m_UnitUID);
			}, null, true);
		}

		// Token: 0x06007916 RID: 30998 RVA: 0x00283E5C File Offset: 0x0028205C
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
					int num = itemRequirement.count * lbinfo.m_iUnitRequirement;
					if (num > 0)
					{
						NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeMiscItemData(itemRequirement.itemID, (long)num, 0);
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06007917 RID: 30999 RVA: 0x00283EF4 File Offset: 0x002820F4
		private void UpdateRearmUI(bool bShow)
		{
			if (!bShow)
			{
				NKCUtil.SetGameobjectActive(this.m_objRearmUI, false);
				NKCUtil.SetGameobjectActive(this.m_objLimitBreakCost, true);
				NKCUtil.SetGameobjectActive(this.m_objLimitbreakResult, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRearmUI, true);
			NKCUtil.SetGameobjectActive(this.m_objLimitBreakCost, false);
			NKCUtil.SetGameobjectActive(this.m_objLimitbreakResult, false);
			if (this.m_targetUnitData == null)
			{
				return;
			}
			this.m_RearmSubUI.SetData(this.m_targetUnitData.m_UnitID, 0, 0L);
		}

		// Token: 0x06007918 RID: 31000 RVA: 0x00283F6F File Offset: 0x0028216F
		private void SelectedRearmUnit(int unitID)
		{
			this.m_iRearmTargetUnitID = unitID;
		}

		// Token: 0x06007919 RID: 31001 RVA: 0x00283F78 File Offset: 0x00282178
		private void UpdateTacticUpdateUI()
		{
			this.m_tacticUpdateLevelSlot.SetLevel(this.m_targetUnitData.tacticLevel, false);
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x00283F91 File Offset: 0x00282191
		private void OnClickTacticUpdate()
		{
			NKCUITacticUpdate.Instance.Open(this.m_targetUnitData);
		}

		// Token: 0x04006587 RID: 25991
		[Header("일반")]
		public GameObject m_objNormalRoot;

		// Token: 0x04006588 RID: 25992
		public List<GameObject> lstObjStarBefore;

		// Token: 0x04006589 RID: 25993
		public List<GameObject> lstObjStarAfter;

		// Token: 0x0400658A RID: 25994
		public RectTransform m_rtStarEffect;

		// Token: 0x0400658B RID: 25995
		public Text m_lbMaxLevelBefore;

		// Token: 0x0400658C RID: 25996
		public Text m_lbMaxLevelAfter;

		// Token: 0x0400658D RID: 25997
		public NKCUIItemCostSlot m_lbRequiredLevel;

		// Token: 0x0400658E RID: 25998
		public Text m_lbGrowthInfo;

		// Token: 0x0400658F RID: 25999
		public GameObject m_NKM_UI_LAB_TRANSCENDENCE_INFO_DETAIL_02;

		// Token: 0x04006590 RID: 26000
		[Header("초월")]
		public GameObject m_objTranscendenceRoot;

		// Token: 0x04006591 RID: 26001
		public NKCUIComStarRank m_comStarRankTC;

		// Token: 0x04006592 RID: 26002
		public GameObject m_objTranscendenceFxPurple;

		// Token: 0x04006593 RID: 26003
		public GameObject m_objTranscendenceFxYellow;

		// Token: 0x04006594 RID: 26004
		public Text m_lbTCLevel;

		// Token: 0x04006595 RID: 26005
		public NKCUIComTextUnitLevel m_lbTCMaxLevelBefore;

		// Token: 0x04006596 RID: 26006
		public Text m_lbTCMaxLevelAfter;

		// Token: 0x04006597 RID: 26007
		public Text m_lbTCPowerupRate;

		// Token: 0x04006598 RID: 26008
		[Header("초월 레벨 최대일 때")]
		public GameObject m_objMaxLevelRoot;

		// Token: 0x04006599 RID: 26009
		public List<GameObject> lstObjStarMaxLevel;

		// Token: 0x0400659A RID: 26010
		[Header("초월각성 레벨 최대일 때")]
		public GameObject m_objTCMaxLevelRoot;

		// Token: 0x0400659B RID: 26011
		public List<GameObject> m_lstObjStarTCMaxLevel;

		// Token: 0x0400659C RID: 26012
		[Header("비었을 때")]
		public GameObject m_objEmptyRoot;

		// Token: 0x0400659D RID: 26013
		[Header("대체 아이템 정보")]
		public NKCUIItemCostSlot m_lbCreditRequired;

		// Token: 0x0400659E RID: 26014
		public List<NKCUIItemCostSlot> m_lstSubstituteItemUI;

		// Token: 0x0400659F RID: 26015
		[Header("시작 버튼")]
		public NKCUIComStateButton m_csbtnLimitBreak;

		// Token: 0x040065A0 RID: 26016
		public GameObject m_btnUILimitbreak;

		// Token: 0x040065A1 RID: 26017
		public GameObject m_btnUITranscendence;

		// Token: 0x040065A2 RID: 26018
		public List<Image> m_lstEnterBtnIcon;

		// Token: 0x040065A3 RID: 26019
		public List<Text> m_lstEnterBtnText;

		// Token: 0x040065A4 RID: 26020
		[Header("기타")]
		public GameObject m_objLimitbreakResult;

		// Token: 0x040065A5 RID: 26021
		public NKCUIComStateButton m_csbtnInformation;

		// Token: 0x040065A6 RID: 26022
		public GameObject m_objLimitBreakCost;

		// Token: 0x040065A7 RID: 26023
		[Header("재무장")]
		public GameObject m_objRearmUI;

		// Token: 0x040065A8 RID: 26024
		public NKCUIComStateButton m_csbtnRearmament;

		// Token: 0x040065A9 RID: 26025
		public Text m_lbRearmBtn;

		// Token: 0x040065AA RID: 26026
		public Image m_imgRearmBtn;

		// Token: 0x040065AB RID: 26027
		public NKCUIRearmamentSubUISelectList m_RearmSubUI;

		// Token: 0x040065AC RID: 26028
		private NKMUnitData m_targetUnitData;

		// Token: 0x040065AD RID: 26029
		private NKMLimitBreakTemplet m_targetLBTemplet;

		// Token: 0x040065AE RID: 26030
		[Header("전술 업데이트")]
		public NKCUITacticUpdateLevelSlot m_tacticUpdateLevelSlot;

		// Token: 0x040065AF RID: 26031
		public NKCUIComStateButton m_csbtnTacticUpdate;

		// Token: 0x040065B0 RID: 26032
		private NKCUIUnitSelectList m_UIUnitSelectList;

		// Token: 0x040065B1 RID: 26033
		private int m_iRearmTargetUnitID;
	}
}

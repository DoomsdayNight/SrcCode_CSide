using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Negotiation;
using NKM;
using NKM.Unit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA8 RID: 2728
	public class NKCUIUnitInfoNegotiation : MonoBehaviour
	{
		// Token: 0x0600791D RID: 31005 RVA: 0x00283FC0 File Offset: 0x002821C0
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnStart, new UnityAction(this.OnStartButton));
			for (int i = 0; i < 3; i++)
			{
				if (i < NKMCommonConst.Negotiation.Materials.Count)
				{
					MiscItemData miscItemData = new MiscItemData();
					miscItemData.itemId = NKMCommonConst.Negotiation.Materials[i].ItemTemplet.m_ItemMiscID;
					miscItemData.count = 0;
					this.m_lstMaterials.Add(miscItemData);
				}
			}
			this.m_csbtnMaterial_1_Add.PointerDown.RemoveAllListeners();
			this.m_csbtnMaterial_1_Add.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPlusDown_1));
			this.m_csbtnMaterial_1_Add.PointerUp.RemoveAllListeners();
			this.m_csbtnMaterial_1_Add.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			this.m_csbtnMaterial_2_Add.PointerDown.RemoveAllListeners();
			this.m_csbtnMaterial_2_Add.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPlusDown_2));
			this.m_csbtnMaterial_2_Add.PointerUp.RemoveAllListeners();
			this.m_csbtnMaterial_2_Add.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			this.m_csbtnMaterial_3_Add.PointerDown.RemoveAllListeners();
			this.m_csbtnMaterial_3_Add.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPlusDown_3));
			this.m_csbtnMaterial_3_Add.PointerUp.RemoveAllListeners();
			this.m_csbtnMaterial_3_Add.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			this.m_csbtnMaterial_1_Minus.PointerDown.RemoveAllListeners();
			this.m_csbtnMaterial_1_Minus.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnMinusDown_1));
			this.m_csbtnMaterial_1_Minus.PointerUp.RemoveAllListeners();
			this.m_csbtnMaterial_1_Minus.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			this.m_csbtnMaterial_2_Minus.PointerDown.RemoveAllListeners();
			this.m_csbtnMaterial_2_Minus.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnMinusDown_2));
			this.m_csbtnMaterial_2_Minus.PointerUp.RemoveAllListeners();
			this.m_csbtnMaterial_2_Minus.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			this.m_csbtnMaterial_3_Minus.PointerDown.RemoveAllListeners();
			this.m_csbtnMaterial_3_Minus.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnMinusDown_3));
			this.m_csbtnMaterial_3_Minus.PointerUp.RemoveAllListeners();
			this.m_csbtnMaterial_3_Minus.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_1_Add, delegate()
			{
				this.OnChangeCount(NKMCommonConst.Negotiation.Materials[0].ItemId, true);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_2_Add, delegate()
			{
				this.OnChangeCount(NKMCommonConst.Negotiation.Materials[1].ItemId, true);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_3_Add, delegate()
			{
				this.OnChangeCount(NKMCommonConst.Negotiation.Materials[2].ItemId, true);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_1_Minus, delegate()
			{
				this.OnChangeCount(NKMCommonConst.Negotiation.Materials[0].ItemId, false);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_2_Minus, delegate()
			{
				this.OnChangeCount(NKMCommonConst.Negotiation.Materials[1].ItemId, false);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_3_Minus, delegate()
			{
				this.OnChangeCount(NKMCommonConst.Negotiation.Materials[2].ItemId, false);
			});
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x002842D5 File Offset: 0x002824D5
		public void OnDisable()
		{
			NKCUtil.SetGameobjectActive(this.m_TalkBox, false);
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x002842E4 File Offset: 0x002824E4
		public void SetData(NKMUnitData unitData)
		{
			if (this.m_bReservedLevelUpFx && this.m_TargetUnitData != null && this.m_TargetUnitData.m_UnitUID == unitData.m_UnitUID)
			{
				if (this.m_TalkBox != null)
				{
					string speech = NKCNegotiateManager.GetSpeech(unitData, NKCNegotiateManager.GetSpeechType(this.m_NegotiateResult));
					if (!string.IsNullOrEmpty(speech))
					{
						this.SetReserveTalkBox(speech);
					}
					else
					{
						this.SetReserveTalkBox("");
						NKCUtil.SetGameobjectActive(this.m_TalkBox, false);
					}
				}
				this.OnPlayFX(this.m_bLevelUp);
			}
			else if (this.m_TargetUnitData == null || this.m_TargetUnitData.m_UnitUID != unitData.m_UnitUID)
			{
				NKCUtil.SetGameobjectActive(this.m_TalkBox, false);
			}
			this.m_TargetUnitData = unitData;
			this.m_bMaxLevelBreak = false;
			this.m_bMaxLoyalty = false;
			this.m_bReservedLevelUpFx = false;
			this.m_bLevelUp = false;
			this.SetUIItemReq(unitData);
			this.ResetMaterialCount();
			this.RefreshNegotiateInfo(unitData);
			this.RefreshNegotiateUI(unitData);
			this.ShowRequestItem();
			NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT);
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x002843E7 File Offset: 0x002825E7
		public void ReserveUnitData(NKMUnitData unitData)
		{
			if (unitData != null)
			{
				this.m_TargetUnitData = unitData;
			}
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x002843F4 File Offset: 0x002825F4
		private void SetUIItemReq(NKMUnitData unitData)
		{
			if (this.m_lstUIItemReq == null)
			{
				return;
			}
			if (unitData != null)
			{
				for (int i = 0; i < this.m_lstUIItemReq.Count; i++)
				{
					NKCUtil.SetLabelText(this.m_lstMaterialUseCount[i], this.m_lstMaterials[i].count.ToString());
					this.m_lstUIItemReq[i].SetData(this.m_lstMaterials[i].itemId, 0, 0L, false, false, false);
					NKCUtil.SetLabelText(this.m_lstMaterialHaveCount[i], NKCUtilString.GET_STRING_HAVE_COUNT_ONE_PARAM, new object[]
					{
						NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_lstMaterials[i].itemId)
					});
				}
				return;
			}
			for (int j = 0; j < this.m_lstUIItemReq.Count; j++)
			{
				NKCUtil.SetLabelText(this.m_lstMaterialUseCount[j], "0");
				this.m_lstUIItemReq[j].SetData(this.m_lstMaterials[j].itemId, 0, 0L, false, false, false);
				NKCUtil.SetLabelText(this.m_lstMaterialHaveCount[j], NKCUtilString.GET_STRING_HAVE_COUNT_ONE_PARAM, new object[]
				{
					NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_lstMaterials[j].itemId)
				});
			}
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x00284560 File Offset: 0x00282760
		public void ShowRequestItem()
		{
			for (int i = 0; i < this.m_lstUIItemReq.Count; i++)
			{
				if (i < this.m_lstMaterialUseCount.Count && i < this.m_lstMaterialHaveCount.Count && i < this.m_lstMaterials.Count)
				{
					NKCUtil.SetLabelText(this.m_lstMaterialUseCount[i], this.m_lstMaterials[i].count.ToString());
					NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_lstMaterials[i].itemId);
					this.m_lstUIItemReq[i].SetData(this.m_lstMaterials[i].itemId, 0, 0L, false, false, false);
					NKCUtil.SetLabelText(this.m_lstMaterialHaveCount[i], NKCUtilString.GET_STRING_HAVE_COUNT_ONE_PARAM, new object[]
					{
						NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_lstMaterials[i].itemId)
					});
				}
			}
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x00284670 File Offset: 0x00282870
		private void ResetMaterialCount()
		{
			for (int i = 0; i < this.m_lstMaterials.Count; i++)
			{
				this.m_lstMaterials[i].count = 0;
			}
		}

		// Token: 0x06007924 RID: 31012 RVA: 0x002846A5 File Offset: 0x002828A5
		private void RefreshNegotiateInfo(NKMUnitData unitData)
		{
			this.CalcExpChange(unitData);
			this.CalcLoyaltyChange(unitData);
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x002846B5 File Offset: 0x002828B5
		private void RefreshNegotiateUI(NKMUnitData unitData)
		{
			this.UpdateExpUI(unitData);
			this.UpdateLoyaltyUI(unitData);
			this.SetCredit(unitData);
			this.ShowRequestItem();
			this.UpdateStartButton();
			this.UpdateMaterialInfo();
		}

		// Token: 0x06007926 RID: 31014 RVA: 0x002846E0 File Offset: 0x002828E0
		private void CalcLoyaltyChange(NKMUnitData unitData)
		{
			int num = 0;
			this.m_earnLoyalty = 0;
			if (unitData != null)
			{
				num = unitData.loyalty / 100;
				int num2 = Math.Min(100, (unitData.loyalty + NKCNegotiateManager.GetNegotiateLoyalty(this.m_lstMaterials, NEGOTIATE_BOSS_SELECTION.OK)) / 100);
				this.m_earnLoyalty = num2 - num;
			}
			this.m_loyaltyFillAmount = (float)(num + this.m_earnLoyalty) / 100f;
			if (num + this.m_earnLoyalty >= 100)
			{
				if (this.m_iChangeValue > 0 && !this.m_bMaxLoyalty)
				{
					this.UpdateLoyaltyUI(unitData);
					this.m_bPress = false;
				}
				this.m_bMaxLoyalty = true;
				return;
			}
			this.m_bMaxLoyalty = false;
		}

		// Token: 0x06007927 RID: 31015 RVA: 0x0028477C File Offset: 0x0028297C
		private void UpdateLoyaltyUI(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				NKCUtil.SetLabelText(this.m_lbLoyalityBefore, string.Format("{0} / {1}", 0, 100));
				NKCUtil.SetLabelText(this.m_lbLoyaltyEarn, string.Format("+{0}", 0));
				NKCUtil.SetGameobjectActive(this.m_lbLoyaltyEarn, true);
				NKCUtil.SetGameobjectActive(this.m_lbPermanentBonus, false);
				if (this.m_imgLoyalityBarBefore != null)
				{
					this.m_imgLoyalityBarBefore.fillAmount = 0f;
				}
				return;
			}
			NKCUtil.SetLabelText(this.m_lbLoyalityBefore, string.Format("{0} / {1}", unitData.loyalty / 100, 100));
			if (this.m_imgLoyalityBarBefore != null)
			{
				this.m_imgLoyalityBarBefore.fillAmount = this.m_loyaltyFillAmount;
			}
			NKCUtil.SetLabelText(this.m_lbLoyaltyEarn, string.Format("+{0}", this.m_earnLoyalty));
			NKCUtil.SetGameobjectActive(this.m_lbLoyaltyEarn, !unitData.IsPermanentContract);
			NKCUtil.SetGameobjectActive(this.m_lbPermanentBonus, unitData.IsPermanentContract);
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x00284890 File Offset: 0x00282A90
		private void CalcExpChange(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				this.UpdateExpUI(unitData);
				return;
			}
			NKMUnitExpTemplet unitExpTemplet = NKCExpManager.GetUnitExpTemplet(unitData);
			NKCExpManager.CalculateFutureUnitExpAndLevel(unitData, NKCNegotiateManager.GetNegotiateExp(this.m_lstMaterials, unitData.IsPermanentContract), out this.m_expectedLevel, out this.m_expectedExp);
			NKMUnitExpTemplet nkmunitExpTemplet = NKMUnitExpTemplet.FindByUnitId(unitData.m_UnitID, this.m_expectedLevel);
			this.m_currentExpPercent = (float)unitData.m_iUnitLevelEXP / (float)unitExpTemplet.m_iExpRequired;
			this.m_expectedExpPercent = (float)this.m_expectedExp / (float)nkmunitExpTemplet.m_iExpRequired;
			int num = NKCExpManager.CalculateNeedExpForUnitMaxLevel(unitData);
			if (NKCNegotiateManager.GetNegotiateExp(this.m_lstMaterials, unitData.IsPermanentContract) >= num)
			{
				if (this.m_iChangeValue > 0 && !this.m_bMaxLevelBreak)
				{
					this.UpdateExpUI(unitData);
					this.m_bPress = false;
				}
				this.m_bMaxLevelBreak = true;
				return;
			}
			this.m_bMaxLevelBreak = false;
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x00284958 File Offset: 0x00282B58
		private void UpdateExpUI(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objLevelPrev, true);
				NKCUtil.SetGameobjectActive(this.m_objAlreadyMaxLevel, false);
				NKCUtil.SetGameobjectActive(this.m_objExpParent, true);
				NKCUtil.SetLabelText(this.m_lbLevelBefore, "-");
				NKCUtil.SetLabelText(this.m_lbLevelAfter, "-");
				NKCUtil.SetLabelText(this.m_lbCurExpCount, "-");
				NKCUtil.SetLabelText(this.m_lbCurMaxExpCount, "/ -");
				NKCUtil.SetLabelText(this.m_lbEarnExpCount, "");
				if (this.m_imgExpBarAfter != null)
				{
					this.m_imgExpBarAfter.fillAmount = 0f;
				}
				if (this.m_imgExpBarAfterAdd != null)
				{
					this.m_imgExpBarAfterAdd.fillAmount = 0f;
				}
				return;
			}
			NKMUnitExpTemplet unitExpTemplet = NKCExpManager.GetUnitExpTemplet(unitData);
			int num = NKCExpManager.CalculateNeedExpForUnitMaxLevel(unitData);
			int num2 = NKCNegotiateManager.GetNegotiateExp(this.m_lstMaterials, unitData.IsPermanentContract);
			if (num2 >= num)
			{
				num2 = num;
			}
			NKCUtil.SetLabelText(this.m_lbLevelAfter, this.m_expectedLevel.ToString());
			bool flag = unitData.m_UnitLevel == NKCExpManager.GetUnitMaxLevel(unitData);
			NKCUtil.SetGameobjectActive(this.m_objLevelPrev, !flag);
			NKCUtil.SetGameobjectActive(this.m_objAlreadyMaxLevel, flag);
			NKCUtil.SetGameobjectActive(this.m_objExpParent, !flag);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_lbEarnExpCount, "");
				NKCUtil.SetLabelTextColor(this.m_lbEarnExpCount, this.TEXT_COLOR_YELLOW);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbLevelBefore, unitData.m_UnitLevel.ToString());
				NKCUtil.SetLabelText(this.m_lbCurExpCount, (unitData.m_iUnitLevelEXP + num2).ToString());
				NKCUtil.SetLabelText(this.m_lbCurMaxExpCount, string.Format("/ {0}", unitExpTemplet.m_iExpRequired));
				NKCUtil.SetLabelText(this.m_lbEarnExpCount, string.Format(NKCUtilString.GET_STRING_EXP_PLUS_ONE_PARAM, num2));
				if (this.m_expectedLevel == NKCExpManager.GetUnitMaxLevel(unitData))
				{
					NKCUtil.SetLabelTextColor(this.m_lbEarnExpCount, Color.red);
					this.m_expectedExpPercent = 1f;
				}
				else
				{
					NKCUtil.SetLabelTextColor(this.m_lbEarnExpCount, this.TEXT_COLOR_YELLOW);
				}
			}
			if (this.m_imgExpBarAfterAdd != null)
			{
				this.m_imgExpBarAfterAdd.fillAmount = this.m_expectedExpPercent;
			}
			if (this.m_expectedLevel > unitData.m_UnitLevel)
			{
				if (this.m_imgExpBarAfter != null)
				{
					this.m_imgExpBarAfter.fillAmount = 1f;
				}
				if (this.m_imgExpBarAfterAdd != null)
				{
					this.m_imgExpBarAfterAdd.transform.SetAsLastSibling();
					return;
				}
			}
			else if (this.m_imgExpBarAfter != null)
			{
				this.m_imgExpBarAfter.fillAmount = this.m_currentExpPercent;
				this.m_imgExpBarAfter.transform.SetAsLastSibling();
			}
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x00284BFC File Offset: 0x00282DFC
		private void SetCredit(NKMUnitData unitData)
		{
			if (this.m_lbCreditReq == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objDiscountEvent, NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT));
			if (this.m_objDiscountEvent != null && this.m_objDiscountEvent.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbDiscountEvent, string.Format(NKCStringTable.GetString("SI_DP_EVENT_BUFF_LABEL_NEGOTIATION_CREDIT_DISCOUNT_DESC", false), NKCCompanyBuff.GetTotalRatio(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT)));
			}
			if (unitData != null)
			{
				NKCUtil.SetLabelText(this.m_lbCreditReq, NKCNegotiateManager.GetNegotiateSalary(this.m_lstMaterials, NEGOTIATE_BOSS_SELECTION.OK).ToString("N0"));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbCreditReq, "0");
		}

		// Token: 0x0600792B RID: 31019 RVA: 0x00284CB8 File Offset: 0x00282EB8
		private void OnStartButton()
		{
			if (this.m_TargetUnitData == null)
			{
				return;
			}
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < this.m_lstMaterials.Count; i++)
			{
				if (this.m_lstMaterials[i].count > 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_NOT_ENOUGH_NEGOTIATE_MATERIALS, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.StartNegotiate(this.m_lstMaterials);
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x00284D38 File Offset: 0x00282F38
		private void UpdateStartButton()
		{
			bool flag = false;
			for (int i = 0; i < this.m_lstMaterials.Count; i++)
			{
				if (this.m_lstMaterials[i].count > 0)
				{
					flag = true;
					break;
				}
			}
			if (flag && NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(1) < NKCNegotiateManager.GetNegotiateSalary(this.m_lstMaterials, NEGOTIATE_BOSS_SELECTION.OK))
			{
				flag = false;
			}
			this.m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_TEXT.color = NKCUtil.GetButtonUIColor(flag && this.m_TargetUnitData != null);
			this.m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_ICON.color = NKCUtil.GetButtonUIColor(flag && this.m_TargetUnitData != null);
			if (!flag)
			{
				this.m_csbtnStart.Lock(false);
				return;
			}
			if (NKCNegotiateManager.CanStartNegotiate(NKCScenManager.CurrentUserData(), this.m_TargetUnitData, NEGOTIATE_BOSS_SELECTION.OK, this.m_lstMaterials) == NKM_ERROR_CODE.NEC_OK)
			{
				this.m_csbtnStart.UnLock(false);
				return;
			}
			this.m_csbtnStart.Lock(false);
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x00284E18 File Offset: 0x00283018
		private void UpdateMaterialInfo()
		{
			NKCUtil.SetGameobjectActive(this.m_csbtnMaterial_1_Minus, this.m_lstMaterials[0].count > 0);
			NKCUtil.SetGameobjectActive(this.m_csbtnMaterial_2_Minus, this.m_lstMaterials[1].count > 0);
			NKCUtil.SetGameobjectActive(this.m_csbtnMaterial_3_Minus, this.m_lstMaterials[2].count > 0);
			for (int i = 0; i < 3; i++)
			{
				if (!(this.m_lstMaterialUseCount[i] == null) && this.m_lstMaterials[i] != null)
				{
					NKCUtil.SetGameobjectActive(this.m_lstMaterialUseCount[i], this.m_lstMaterials[i].count > 0);
				}
			}
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x00284ED5 File Offset: 0x002830D5
		public void OnUnitUpdate(long uid, NKMUnitData unitData)
		{
			if (this.m_TargetUnitData != null && uid == this.m_TargetUnitData.m_UnitUID)
			{
				this.SetData(unitData);
			}
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x00284EF4 File Offset: 0x002830F4
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData.ItemID == 1)
			{
				this.SetCredit(this.m_TargetUnitData);
				this.UpdateStartButton();
				return;
			}
			this.ShowRequestItem();
			this.UpdateStartButton();
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x00284F1E File Offset: 0x0028311E
		public void OnCompanyBuffChanged()
		{
			this.SetCredit(this.m_TargetUnitData);
			this.ShowRequestItem();
			this.UpdateStartButton();
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x00284F38 File Offset: 0x00283138
		private void OnMinusDown_1(PointerEventData eventData)
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			this.targetId = NKMCommonConst.Negotiation.Materials[0].ItemId;
			this.m_iChangeValue = -1;
			this.m_bPress = true;
			this.m_fDelay = 0.3f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
			this.m_bMaxLevelBreak = false;
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x00284FA8 File Offset: 0x002831A8
		private void OnPlusDown_1(PointerEventData eventData)
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			this.targetId = NKMCommonConst.Negotiation.Materials[0].ItemId;
			this.m_iChangeValue = 1;
			this.m_bPress = true;
			this.m_fDelay = 0.3f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x00285010 File Offset: 0x00283210
		private void OnMinusDown_2(PointerEventData eventData)
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			this.targetId = NKMCommonConst.Negotiation.Materials[1].ItemId;
			this.m_iChangeValue = -1;
			this.m_bPress = true;
			this.m_fDelay = 0.3f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
			this.m_bMaxLevelBreak = false;
		}

		// Token: 0x06007934 RID: 31028 RVA: 0x00285080 File Offset: 0x00283280
		private void OnPlusDown_2(PointerEventData eventData)
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			this.targetId = NKMCommonConst.Negotiation.Materials[1].ItemId;
			this.m_iChangeValue = 1;
			this.m_bPress = true;
			this.m_fDelay = 0.3f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007935 RID: 31029 RVA: 0x002850E8 File Offset: 0x002832E8
		private void OnMinusDown_3(PointerEventData eventData)
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			this.targetId = NKMCommonConst.Negotiation.Materials[2].ItemId;
			this.m_iChangeValue = -1;
			this.m_bPress = true;
			this.m_fDelay = 0.3f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
			this.m_bMaxLevelBreak = false;
		}

		// Token: 0x06007936 RID: 31030 RVA: 0x00285158 File Offset: 0x00283358
		private void OnPlusDown_3(PointerEventData eventData)
		{
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			this.targetId = NKMCommonConst.Negotiation.Materials[2].ItemId;
			this.m_iChangeValue = 1;
			this.m_bPress = true;
			this.m_fDelay = 0.3f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007937 RID: 31031 RVA: 0x002851BF File Offset: 0x002833BF
		private void OnButtonUp()
		{
			this.m_iChangeValue = 0;
			this.m_fDelay = 0.3f;
			this.m_bPress = false;
		}

		// Token: 0x06007938 RID: 31032 RVA: 0x002851DC File Offset: 0x002833DC
		public void OnUpdateButtonHold()
		{
			if (!this.m_bPress)
			{
				return;
			}
			if (this.m_TargetUnitData == null)
			{
				return;
			}
			if (this.m_bMaxLevelBreak && this.m_bMaxLoyalty && this.m_iChangeValue > 0)
			{
				return;
			}
			this.m_fHoldTime += Time.deltaTime;
			if (this.m_fHoldTime >= this.m_fDelay)
			{
				this.m_fHoldTime = 0f;
				this.m_fDelay *= 0.8f;
				int num = (this.m_fDelay < 0.01f) ? 5 : 1;
				this.m_fDelay = Mathf.Clamp(this.m_fDelay, 0.01f, 0.3f);
				MiscItemData miscItemData = this.m_lstMaterials.Find((MiscItemData e) => e.itemId == this.targetId);
				if (miscItemData == null)
				{
					return;
				}
				this.m_bWasHold = true;
				int num2 = 0;
				while (num2 < num && this.m_bPress && (!this.m_bMaxLevelBreak || !this.m_bMaxLoyalty))
				{
					miscItemData.count += this.m_iChangeValue;
					if (this.m_iChangeValue < 0 && miscItemData.count < 0)
					{
						miscItemData.count = 0;
						this.m_bPress = false;
					}
					if (this.m_iChangeValue > 0)
					{
						int notSelectedTotalCount = 0;
						this.m_lstMaterials.ForEach(delegate(MiscItemData x)
						{
							if (x.itemId != this.targetId)
							{
								notSelectedTotalCount += x.count;
							}
						});
						int num3 = Math.Max(0, NKMCommonConst.Negotiation.MaxMaterialUsageLimit - notSelectedTotalCount);
						num3 = Math.Min(num3, (int)NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(miscItemData.itemId));
						if (miscItemData.count >= num3)
						{
							miscItemData.count = num3;
							this.m_bPress = false;
						}
					}
					this.RefreshNegotiateInfo(this.m_TargetUnitData);
					num2++;
				}
			}
			this.RefreshNegotiateUI(this.m_TargetUnitData);
		}

		// Token: 0x06007939 RID: 31033 RVA: 0x002853A8 File Offset: 0x002835A8
		public void OnChangeCount(int targetItemId, bool bPlus = true)
		{
			this.targetId = targetItemId;
			if (this.m_bWasHold)
			{
				this.m_bWasHold = false;
				return;
			}
			if (this.m_TargetUnitData == null)
			{
				return;
			}
			if (NKCUIUnitInfo.IsInstanceOpen && NKCUIUnitInfo.Instance.IsBlockedUnit())
			{
				return;
			}
			if (this.m_bMaxLevelBreak && this.m_bMaxLoyalty)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_MAX_LEVEL_LOYALTY, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			MiscItemData miscItemData = this.m_lstMaterials.Find((MiscItemData e) => e.itemId == this.targetId);
			if (miscItemData == null)
			{
				return;
			}
			if (bPlus)
			{
				int notSelectedTotalCount = 0;
				this.m_lstMaterials.ForEach(delegate(MiscItemData x)
				{
					if (x.itemId != this.targetId)
					{
						notSelectedTotalCount += x.count;
					}
				});
				int num = Math.Max(0, NKMCommonConst.Negotiation.MaxMaterialUsageLimit - notSelectedTotalCount);
				num = Math.Min(num, (int)NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(miscItemData.itemId));
				if (miscItemData.count >= num)
				{
					if (miscItemData.count + notSelectedTotalCount >= NKMCommonConst.Negotiation.MaxMaterialUsageLimit)
					{
						NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_NEGOTIATION_INVALID_MATERIAL_COUNT, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_NOT_ENOUGH_NEGOTIATE_MATERIALS, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
			}
			else
			{
				int num2 = 0;
				if (miscItemData.count <= num2)
				{
					return;
				}
			}
			miscItemData.count += (bPlus ? 1 : -1);
			this.RefreshNegotiateInfo(this.m_TargetUnitData);
			this.RefreshNegotiateUI(this.m_TargetUnitData);
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x00285516 File Offset: 0x00283716
		public void Clear()
		{
			this.m_TargetUnitData = null;
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x0600793B RID: 31035 RVA: 0x0028551F File Offset: 0x0028371F
		// (set) Token: 0x0600793C RID: 31036 RVA: 0x00285526 File Offset: 0x00283726
		public static NEGOTIATE_BOSS_SELECTION LastUserSelection { get; private set; }

		// Token: 0x0600793D RID: 31037 RVA: 0x00285530 File Offset: 0x00283730
		private void OnPlayFX(bool bLevelUp)
		{
			if (bLevelUp)
			{
				NKCSoundManager.PlaySound("FX_UI_ACCOUNT_LEVEL_UP", 1f, 0f, 0f, false, 0f, false, 0f);
			}
			this.m_LevelUpEffect.SetBool(this.BOOL_KEY_LEVELUP, bLevelUp);
			bool flag = false;
			for (int i = 0; i < this.m_lstMaterials.Count; i++)
			{
				if (this.m_lstMaterials[i] != null)
				{
					if (this.m_lstMaterials[i].count > 0 && i == 0)
					{
						flag = true;
						this.m_LevelUpEffect.SetTrigger(this.FX_SLOT_0);
					}
					if (this.m_lstMaterials[i].count > 0 && i == 1)
					{
						flag = true;
						this.m_LevelUpEffect.SetTrigger(this.FX_SLOT_1);
					}
					if (this.m_lstMaterials[i].count > 0 && i == 2)
					{
						flag = true;
						this.m_LevelUpEffect.SetTrigger(this.FX_SLOT_2);
					}
				}
			}
			if (flag)
			{
				NKCSoundManager.PlaySound("FX_UI_NEGOTIATE_SUCCESS_02", 1f, 0f, 0f, false, 0f, false, 0f);
			}
			if (this.m_NegotiateResult == NEGOTIATE_RESULT.SUCCESS)
			{
				this.m_LevelUpEffect.SetTrigger(this.BOOL_KEY_SUCCESS);
			}
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x00285669 File Offset: 0x00283869
		private void SetReserveTalkBox(string text)
		{
			this.m_TalkBox.ReserveText(text);
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x00285678 File Offset: 0x00283878
		public void ShowReservedTalkBox()
		{
			NKCUtil.SetGameobjectActive(this.m_TalkBox, true);
			Animator aniTalkBox = this.m_aniTalkBox;
			if (aniTalkBox != null)
			{
				aniTalkBox.SetTrigger("Speech");
			}
			this.m_TalkBox.ShowReservedText(0f);
			if (this.m_NegotiateResult == NEGOTIATE_RESULT.SUCCESS)
			{
				NKCUIUnitInfo.Instance.SetUnitAnimation(NKCASUIUnitIllust.eAnimation.UNIT_LAUGH);
			}
			else
			{
				NKCUIUnitInfo.Instance.SetUnitAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE);
			}
			this.m_NegotiateResult = NEGOTIATE_RESULT.COMPLETE;
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x002856E0 File Offset: 0x002838E0
		private void StartNegotiate(List<MiscItemData> lstMaterials)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCNegotiateManager.CanStartNegotiate(NKCScenManager.CurrentUserData(), this.m_TargetUnitData, NEGOTIATE_BOSS_SELECTION.OK, lstMaterials);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM)
				{
					NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
					return;
				}
				if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT)
				{
					long negotiateSalary = NKCNegotiateManager.GetNegotiateSalary(lstMaterials, NEGOTIATE_BOSS_SELECTION.OK);
					NKCShopManager.OpenItemLackPopup(1, (int)negotiateSalary);
				}
				return;
			}
			else
			{
				this.m_lstMaterials = lstMaterials;
				int unitMaxLevel = NKCExpManager.GetUnitMaxLevel(this.m_TargetUnitData);
				if (this.m_TargetUnitData.m_UnitLevel >= unitMaxLevel)
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_NEGOTIATE_LEVEL_MAX, delegate()
					{
						NKCPacketSender.Send_NKMPacket_NEGOTIATE_REQ2(this.m_TargetUnitData, NEGOTIATE_BOSS_SELECTION.OK, this.m_lstMaterials);
					}, null, false);
					return;
				}
				int num;
				int num2;
				NKCExpManager.CalculateFutureUnitExpAndLevel(this.m_TargetUnitData, NKCNegotiateManager.GetNegotiateExp(this.m_lstMaterials, this.m_TargetUnitData.IsPermanentContract), out num, out num2);
				if (num >= unitMaxLevel)
				{
					int num3 = NKCExpManager.CalculateNeedExpForUnitMaxLevel(this.m_TargetUnitData);
					int num4 = 0;
					for (int i = 0; i < this.m_lstMaterials.Count; i++)
					{
						if (this.m_lstMaterials[i].count > 0)
						{
							for (int j = 0; j < NKMCommonConst.Negotiation.Materials.Count; j++)
							{
								if (NKMCommonConst.Negotiation.Materials[j].ItemId == this.m_lstMaterials[i].itemId)
								{
									num4 += NKMCommonConst.Negotiation.Materials[j].Exp * this.m_lstMaterials[i].count;
									break;
								}
							}
						}
					}
					if (this.m_TargetUnitData.IsPermanentContract)
					{
						num4 = num4 * 120 / 100;
					}
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, string.Format(NKCUtilString.GET_STRING_NEGOTIATE_OVER_MAX_LEVEL_ONE_PARAM, num4 - num3), delegate()
					{
						NKCPacketSender.Send_NKMPacket_NEGOTIATE_REQ2(this.m_TargetUnitData, NEGOTIATE_BOSS_SELECTION.OK, this.m_lstMaterials);
					}, null, false);
					return;
				}
				NKCPacketSender.Send_NKMPacket_NEGOTIATE_REQ2(this.m_TargetUnitData, NEGOTIATE_BOSS_SELECTION.OK, this.m_lstMaterials);
				return;
			}
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x002858AA File Offset: 0x00283AAA
		public void OnCompanyBuffUpdate()
		{
			this.OnCompanyBuffChanged();
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x002858B2 File Offset: 0x00283AB2
		public void RefreshUIForReconnect()
		{
			if (this.m_TargetUnitData != null)
			{
				this.m_TargetUnitData = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_TargetUnitData.m_UnitUID);
			}
		}

		// Token: 0x06007943 RID: 31043 RVA: 0x002858DC File Offset: 0x00283ADC
		public void ReserveLevelUpFx(NKCNegotiateManager.NegotiateResultUIData negotiateResult)
		{
			this.m_bReservedLevelUpFx = true;
			this.m_NegotiateResult = negotiateResult.NegotiateResult;
			this.m_bLevelUp = (negotiateResult.UnitLevelBefore < negotiateResult.UnitLevelAfter);
		}

		// Token: 0x040065B2 RID: 26034
		[Header("Outro")]
		public NKCComUITalkBox m_UIOutroTalkBox;

		// Token: 0x040065B3 RID: 26035
		[Header("레벨/경험치")]
		public GameObject m_objLevelPrev;

		// Token: 0x040065B4 RID: 26036
		public Text m_lbLevelBefore;

		// Token: 0x040065B5 RID: 26037
		public Text m_lbLevelAfter;

		// Token: 0x040065B6 RID: 26038
		public GameObject m_objAlreadyMaxLevel;

		// Token: 0x040065B7 RID: 26039
		public GameObject m_objExpParent;

		// Token: 0x040065B8 RID: 26040
		public Image m_imgExpBarAfter;

		// Token: 0x040065B9 RID: 26041
		public Image m_imgExpBarAfterAdd;

		// Token: 0x040065BA RID: 26042
		public Text m_lbCurExpCount;

		// Token: 0x040065BB RID: 26043
		public Text m_lbCurMaxExpCount;

		// Token: 0x040065BC RID: 26044
		public Text m_lbEarnExpCount;

		// Token: 0x040065BD RID: 26045
		[Header("애사심")]
		public Text m_lbLoyalityBefore;

		// Token: 0x040065BE RID: 26046
		public Image m_imgLoyalityBarBefore;

		// Token: 0x040065BF RID: 26047
		public Text m_lbLoyaltyEarn;

		// Token: 0x040065C0 RID: 26048
		public Text m_lbPermanentBonus;

		// Token: 0x040065C1 RID: 26049
		[Header("재료")]
		public List<Text> m_lstMaterialUseCount = new List<Text>(3);

		// Token: 0x040065C2 RID: 26050
		public List<NKCUIItemCostSlot> m_lstUIItemReq = new List<NKCUIItemCostSlot>(3);

		// Token: 0x040065C3 RID: 26051
		public List<Text> m_lstMaterialHaveCount = new List<Text>(3);

		// Token: 0x040065C4 RID: 26052
		public Text m_lbCreditReq;

		// Token: 0x040065C5 RID: 26053
		public GameObject m_objDiscountEvent;

		// Token: 0x040065C6 RID: 26054
		public Text m_lbDiscountEvent;

		// Token: 0x040065C7 RID: 26055
		public NKCUIComStateButton m_csbtnStart;

		// Token: 0x040065C8 RID: 26056
		private NKMUnitData m_TargetUnitData;

		// Token: 0x040065C9 RID: 26057
		public GameObject m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_BG_OFF;

		// Token: 0x040065CA RID: 26058
		public Image m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_ICON;

		// Token: 0x040065CB RID: 26059
		public Text m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_TEXT;

		// Token: 0x040065CC RID: 26060
		[Header("재료")]
		public NKCUIComStateButton m_csbtnMaterial_1_Add;

		// Token: 0x040065CD RID: 26061
		public NKCUIComStateButton m_csbtnMaterial_1_Minus;

		// Token: 0x040065CE RID: 26062
		public NKCUIComStateButton m_csbtnMaterial_2_Add;

		// Token: 0x040065CF RID: 26063
		public NKCUIComStateButton m_csbtnMaterial_2_Minus;

		// Token: 0x040065D0 RID: 26064
		public NKCUIComStateButton m_csbtnMaterial_3_Add;

		// Token: 0x040065D1 RID: 26065
		public NKCUIComStateButton m_csbtnMaterial_3_Minus;

		// Token: 0x040065D2 RID: 26066
		[Header("애니메이션")]
		public Animator m_LevelUpEffect;

		// Token: 0x040065D3 RID: 26067
		[Header("말풍선")]
		public Animator m_aniTalkBox;

		// Token: 0x040065D4 RID: 26068
		public NKCComUITalkBox m_TalkBox;

		// Token: 0x040065D5 RID: 26069
		private Color TEXT_COLOR_YELLOW = new Color(1f, 0.8117647f, 0.23137255f);

		// Token: 0x040065D6 RID: 26070
		private List<MiscItemData> m_lstMaterials = new List<MiscItemData>(3);

		// Token: 0x040065D7 RID: 26071
		private bool m_bMaxLevelBreak;

		// Token: 0x040065D8 RID: 26072
		private int m_expectedLevel;

		// Token: 0x040065D9 RID: 26073
		private int m_expectedExp;

		// Token: 0x040065DA RID: 26074
		private float m_currentExpPercent;

		// Token: 0x040065DB RID: 26075
		private float m_expectedExpPercent;

		// Token: 0x040065DC RID: 26076
		private bool m_bReservedLevelUpFx;

		// Token: 0x040065DD RID: 26077
		private NEGOTIATE_RESULT m_NegotiateResult = NEGOTIATE_RESULT.COMPLETE;

		// Token: 0x040065DE RID: 26078
		private bool m_bMaxLoyalty;

		// Token: 0x040065DF RID: 26079
		private const int LOYALTY_MAX = 100;

		// Token: 0x040065E0 RID: 26080
		private float m_loyaltyFillAmount;

		// Token: 0x040065E1 RID: 26081
		private int m_earnLoyalty;

		// Token: 0x040065E2 RID: 26082
		private bool m_bLevelUp;

		// Token: 0x040065E3 RID: 26083
		private const string TALKBOX_PLAY_TRIGGER = "Speech";

		// Token: 0x040065E4 RID: 26084
		public const float PRESS_GAP_MAX = 0.3f;

		// Token: 0x040065E5 RID: 26085
		public const float PRESS_GAP_MIN = 0.01f;

		// Token: 0x040065E6 RID: 26086
		public const float DAMPING = 0.8f;

		// Token: 0x040065E7 RID: 26087
		private float m_fDelay = 0.5f;

		// Token: 0x040065E8 RID: 26088
		private float m_fHoldTime;

		// Token: 0x040065E9 RID: 26089
		private int m_iChangeValue;

		// Token: 0x040065EA RID: 26090
		private bool m_bPress;

		// Token: 0x040065EB RID: 26091
		private bool m_bWasHold;

		// Token: 0x040065EC RID: 26092
		private int targetId;

		// Token: 0x040065EE RID: 26094
		private string FX_SLOT_0 = "FX_LEVELUP_SLOT_0";

		// Token: 0x040065EF RID: 26095
		private string FX_SLOT_1 = "FX_LEVELUP_SLOT_1";

		// Token: 0x040065F0 RID: 26096
		private string FX_SLOT_2 = "FX_LEVELUP_SLOT_2";

		// Token: 0x040065F1 RID: 26097
		private string BOOL_KEY_LEVELUP = "IsLevelup";

		// Token: 0x040065F2 RID: 26098
		private string BOOL_KEY_SUCCESS = "IsLevelupSuccess";
	}
}

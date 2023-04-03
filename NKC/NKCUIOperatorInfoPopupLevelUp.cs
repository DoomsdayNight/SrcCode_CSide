using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A1D RID: 2589
	public class NKCUIOperatorInfoPopupLevelUp : MonoBehaviour
	{
		// Token: 0x06007127 RID: 28967 RVA: 0x00258D3C File Offset: 0x00256F3C
		public void Init(NKCUIOperatorInfoPopupLevelUp.OnStart onStart)
		{
			NKCUtil.SetBindFunction(this.m_NKM_UI_OPERATOR_INFO_NEGOTIATE_REQUIRE_START, new UnityAction(this.OnConfirm));
			this.dOnStart = onStart;
			for (int i = 0; i < NKMCommonConst.OperatorConstTemplet.list.Length; i++)
			{
				MiscItemData miscItemData = new MiscItemData();
				miscItemData.itemId = NKMCommonConst.OperatorConstTemplet.list[i].itemId;
				miscItemData.count = 0;
				this.m_lstMaterials.Add(miscItemData);
			}
			if (this.m_lstMaterials.Count < 3)
			{
				Debug.LogError("오퍼레이터 연봉 협상의 소모 재화가 부족합니다.");
				return;
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
				this.OnChangeCount(this.m_lstMaterials[0].itemId, true);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_2_Add, delegate()
			{
				this.OnChangeCount(this.m_lstMaterials[1].itemId, true);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_3_Add, delegate()
			{
				this.OnChangeCount(this.m_lstMaterials[2].itemId, true);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_1_Minus, delegate()
			{
				this.OnChangeCount(this.m_lstMaterials[0].itemId, false);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_2_Minus, delegate()
			{
				this.OnChangeCount(this.m_lstMaterials[1].itemId, false);
			});
			NKCUtil.SetBindFunction(this.m_csbtnMaterial_3_Minus, delegate()
			{
				this.OnChangeCount(this.m_lstMaterials[2].itemId, false);
			});
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x00259064 File Offset: 0x00257264
		public void SetData(NKMOperator operatorData, bool bShowLevelUpFX = false)
		{
			if (operatorData == null)
			{
				return;
			}
			this.m_bWaitResultPacket = false;
			if (bShowLevelUpFX && this.m_OperatorData != null && this.m_OperatorData.uid == operatorData.uid)
			{
				this.OnPlayFX(this.m_OperatorData.level < operatorData.level);
			}
			this.m_OperatorData = operatorData;
			this.ResetMaterialCount();
			this.RefreshData();
			this.RefreshUI();
		}

		// Token: 0x06007129 RID: 28969 RVA: 0x002590CC File Offset: 0x002572CC
		public void ResetResourceIcon()
		{
			for (int i = 0; i < this.m_lstUIItemReq.Count; i++)
			{
				if (this.m_lstMaterials.Count < i)
				{
					this.m_lstUIItemReq[i].SetData(this.m_lstMaterials[i].itemId, 0, 0L, false, false, false);
				}
			}
		}

		// Token: 0x0600712A RID: 28970 RVA: 0x00259125 File Offset: 0x00257325
		public void Refresh()
		{
			if (this.m_OperatorData != null)
			{
				this.ResetMaterialCount();
				this.RefreshData();
				this.RefreshUI();
			}
		}

		// Token: 0x0600712B RID: 28971 RVA: 0x00259144 File Offset: 0x00257344
		private void ResetMaterialCount()
		{
			foreach (MiscItemData miscItemData in this.m_lstMaterials)
			{
				miscItemData.count = 0;
			}
		}

		// Token: 0x0600712C RID: 28972 RVA: 0x00259198 File Offset: 0x00257398
		private void RefreshData()
		{
			this.m_bMaxLevelBreak = false;
			this.UpdateExpData();
		}

		// Token: 0x0600712D RID: 28973 RVA: 0x002591A7 File Offset: 0x002573A7
		private void RefreshUI()
		{
			this.UpdateCredit();
			this.ShowRequestItem();
			this.UpdateExpUI();
			this.UpdateStat();
			this.UpdateStartButton();
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x002591C8 File Offset: 0x002573C8
		private void UpdateStat()
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			string statPercentageString = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_OperatorData.level, NKM_STAT_TYPE.NST_HP);
			string statPercentageString2 = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_OperatorData.level, NKM_STAT_TYPE.NST_ATK);
			string statPercentageString3 = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_OperatorData.level, NKM_STAT_TYPE.NST_DEF);
			string statPercentageString4 = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_OperatorData.level, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE);
			if (this.m_iExpectLevel > this.m_OperatorData.level)
			{
				string statPercentageString5 = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_iExpectLevel, NKM_STAT_TYPE.NST_HP);
				string statPercentageString6 = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_iExpectLevel, NKM_STAT_TYPE.NST_ATK);
				string statPercentageString7 = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_iExpectLevel, NKM_STAT_TYPE.NST_DEF);
				string statPercentageString8 = NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData.id, this.m_iExpectLevel, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE);
				NKCUtil.SetLabelText(this.m_STAT_HP_NUMBER, statPercentageString + " > " + statPercentageString5);
				NKCUtil.SetLabelText(this.m_STAT_ATT_NUMBER, statPercentageString2 + " > " + statPercentageString6);
				NKCUtil.SetLabelText(this.m_STAT_DEF_NUMBER, statPercentageString3 + " > " + statPercentageString7);
				NKCUtil.SetLabelText(this.m_STAT_SKILL_COOL_NUMBER, statPercentageString4 + " > " + statPercentageString8);
				return;
			}
			NKCUtil.SetLabelText(this.m_STAT_HP_NUMBER, statPercentageString ?? "");
			NKCUtil.SetLabelText(this.m_STAT_ATT_NUMBER, statPercentageString2 ?? "");
			NKCUtil.SetLabelText(this.m_STAT_DEF_NUMBER, statPercentageString3 ?? "");
			NKCUtil.SetLabelText(this.m_STAT_SKILL_COOL_NUMBER, statPercentageString4 ?? "");
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x00259384 File Offset: 0x00257584
		private void UpdateExpData()
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_OperatorData.id);
			this.m_iExpectLevel = 0;
			int num = NKCOperatorUtil.CalculateNeedExpForUnitMaxLevel(this.m_OperatorData, unitTempletBase.m_NKM_UNIT_GRADE);
			this.m_iEarnExp = NKCOperatorUtil.CalcNegotiationTotalExp(this.m_lstMaterials);
			if (this.m_iEarnExp >= num)
			{
				if (this.m_iChangeValue > 0 && !this.m_bMaxLevelBreak)
				{
					this.m_bPress = false;
				}
				this.m_bMaxLevelBreak = true;
				this.m_iEarnExp = num;
			}
		}

		// Token: 0x06007130 RID: 28976 RVA: 0x00259404 File Offset: 0x00257604
		private void UpdateExpUI()
		{
			if (this.m_OperatorData == null)
			{
				if (this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE != null)
				{
					this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE.fillAmount = 0f;
				}
				if (this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE != null)
				{
					this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE_UP.fillAmount = 0f;
				}
				return;
			}
			int requiredExp = NKCOperatorUtil.GetRequiredExp(this.m_OperatorData);
			int num = 0;
			int num2 = 0;
			NKCOperatorUtil.CalculateFutureOperatorExpAndLevel(this.m_OperatorData, NKCOperatorUtil.CalcNegotiationTotalExp(this.m_lstMaterials), out this.m_iExpectLevel, out num);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_OperatorData.id);
			if (unitTempletBase != null)
			{
				num2 = NKCOperatorUtil.GetRequiredUnitExp(unitTempletBase.m_NKM_UNIT_GRADE, this.m_iExpectLevel);
			}
			float fillAmount = (float)this.m_OperatorData.exp / (float)requiredExp;
			float fillAmount2 = (float)num / (float)num2;
			NKCUtil.SetLabelText(this.NKM_UI_PERSONNEL_NEGOTIATE_UNIT_NEXT_LV_COUNT, this.m_iExpectLevel.ToString());
			bool flag = this.m_OperatorData.level == NKMCommonConst.OperatorConstTemplet.unitMaximumLevel;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT, !flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT_1, !flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT_2, !flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_LEVELMAX_TEXT, flag);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_PERSONNEL_NEGOTIATE_RESULT_EXP_COUNT, "");
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_PERSONNEL_NEGOTIATE_RESULT_EXP_COUNT, this.TEXT_COLOR_YELLOW);
				NKCUtil.SetLabelText(this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_PREV_LV_COUNT, NKMCommonConst.OperatorConstTemplet.unitMaximumLevel.ToString());
				fillAmount = 1f;
			}
			else
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_PREV_LV_COUNT, this.m_OperatorData.level.ToString());
				NKCUtil.SetLabelText(this.m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT_1, (this.m_OperatorData.exp + this.m_iEarnExp).ToString());
				NKCUtil.SetLabelText(this.m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT_2, string.Format("/ {0}", requiredExp));
				NKCUtil.SetLabelText(this.m_NKM_UI_PERSONNEL_NEGOTIATE_RESULT_EXP_COUNT, string.Format(NKCUtilString.GET_STRING_EXP_PLUS_ONE_PARAM, this.m_iEarnExp));
				if (this.m_iExpectLevel == NKMCommonConst.OperatorConstTemplet.unitMaximumLevel)
				{
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_PERSONNEL_NEGOTIATE_RESULT_EXP_COUNT, Color.red);
					fillAmount2 = 1f;
				}
				else
				{
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_PERSONNEL_NEGOTIATE_RESULT_EXP_COUNT, this.TEXT_COLOR_YELLOW);
				}
			}
			if (this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE_UP != null)
			{
				this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE_UP.fillAmount = fillAmount2;
			}
			if (this.m_iExpectLevel > this.m_OperatorData.level)
			{
				if (this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE != null)
				{
					this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE.fillAmount = 1f;
				}
				if (this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE_UP != null)
				{
					this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE_UP.transform.SetAsLastSibling();
					return;
				}
			}
			else if (this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE != null)
			{
				this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE.fillAmount = fillAmount;
				this.m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06007131 RID: 28977 RVA: 0x002596C8 File Offset: 0x002578C8
		private void OnConfirm()
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
			if (!flag)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_NOT_ENOUGH_NEGOTIATE_MATERIALS, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			int num = NKCOperatorUtil.CalcNegotiationCostCredit(this.m_lstMaterials);
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(1) < (long)num)
			{
				NKCPopupItemLack.Instance.OpenItemMiscLackPopup(1, num);
				return;
			}
			if (this.m_bWaitResultPacket)
			{
				return;
			}
			NKCUIOperatorInfoPopupLevelUp.OnStart onStart = this.dOnStart;
			if (onStart != null)
			{
				onStart(this.m_lstMaterials);
			}
			this.m_bWaitResultPacket = true;
		}

		// Token: 0x06007132 RID: 28978 RVA: 0x00259770 File Offset: 0x00257970
		private void UpdateCredit()
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_COUNT, NKCOperatorUtil.CalcNegotiationCostCredit(this.m_lstMaterials).ToString("N0"));
		}

		// Token: 0x06007133 RID: 28979 RVA: 0x002597AC File Offset: 0x002579AC
		private void UpdateStartButton()
		{
			if (this.m_OperatorData == null)
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
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_BG_OFF, !flag);
			this.m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_TEXT.color = NKCUtil.GetButtonUIColor(flag);
			this.m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_ICON.color = NKCUtil.GetButtonUIColor(flag);
			if (!flag)
			{
				this.m_NKM_UI_OPERATOR_INFO_NEGOTIATE_REQUIRE_START.Lock(false);
				return;
			}
			this.m_NKM_UI_OPERATOR_INFO_NEGOTIATE_REQUIRE_START.UnLock(false);
		}

		// Token: 0x06007134 RID: 28980 RVA: 0x00259840 File Offset: 0x00257A40
		private void OnPlayFX(bool bLevelUp)
		{
			if (bLevelUp)
			{
				NKCSoundManager.PlaySound("FX_UI_ACCOUNT_LEVEL_UP", 1f, 0f, 0f, false, 0f, false, 0f);
			}
			this.m_LevelUpEffect.SetBool(this.BOOL_KEY_LEVELUP, bLevelUp);
			for (int i = 0; i < this.m_lstMaterials.Count; i++)
			{
				if (this.m_lstMaterials[i] != null)
				{
					if (this.m_lstMaterials[i].count > 0 && i == 0)
					{
						NKCSoundManager.PlaySound("FX_UI_UNIT_GET_MAIN_SSR", 1f, 0f, 0f, false, 0f, false, 0f);
						this.m_LevelUpEffect.SetTrigger(this.FX_SLOT_0);
					}
					if (this.m_lstMaterials[i].count > 0 && i == 1)
					{
						NKCSoundManager.PlaySound("FX_UI_UNIT_GET_MAIN_SSR", 1f, 0f, 0f, false, 0f, false, 0f);
						this.m_LevelUpEffect.SetTrigger(this.FX_SLOT_1);
					}
					if (this.m_lstMaterials[i].count > 0 && i == 2)
					{
						NKCSoundManager.PlaySound("FX_UI_UNIT_GET_MAIN_SSR", 1f, 0f, 0f, false, 0f, false, 0f);
						this.m_LevelUpEffect.SetTrigger(this.FX_SLOT_2);
					}
				}
			}
		}

		// Token: 0x06007135 RID: 28981 RVA: 0x002599A1 File Offset: 0x00257BA1
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData.ItemID == 1)
			{
				this.UpdateCredit();
				this.UpdateStartButton();
				return;
			}
			this.ShowRequestItem();
			this.UpdateStartButton();
		}

		// Token: 0x06007136 RID: 28982 RVA: 0x002599C8 File Offset: 0x00257BC8
		public void ShowRequestItem()
		{
			for (int i = 0; i < this.m_lstUIItemReq.Count; i++)
			{
				if (i < this.m_lstMaterialUseCount.Count && i < this.m_lstMaterialHaveCount.Count && i < this.m_lstMaterials.Count)
				{
					NKCUtil.SetLabelText(this.m_lstMaterialUseCount[i], this.m_lstMaterials[i].count.ToString());
					this.m_lstUIItemReq[i].SetData(this.m_lstMaterials[i].itemId, 0, 0L, false, false, false);
					NKCUtil.SetLabelText(this.m_lstMaterialHaveCount[i], NKCUtilString.GET_STRING_HAVE_COUNT_ONE_PARAM, new object[]
					{
						NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_lstMaterials[i].itemId)
					});
				}
			}
		}

		// Token: 0x06007137 RID: 28983 RVA: 0x00259AB7 File Offset: 0x00257CB7
		public void Update()
		{
			this.OnUpdateButtonHold();
		}

		// Token: 0x06007138 RID: 28984 RVA: 0x00259AC0 File Offset: 0x00257CC0
		private void OnClickPlus(int slotCnt)
		{
			if (slotCnt < 0 || NKMCommonConst.OperatorConstTemplet.list.Length < slotCnt)
			{
				return;
			}
			this.targetId = NKMCommonConst.OperatorConstTemplet.list[slotCnt].itemId;
			this.m_iChangeValue = 1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007139 RID: 28985 RVA: 0x00259B24 File Offset: 0x00257D24
		private void OnClickMinus(int slotCnt)
		{
			if (slotCnt < 0 || NKMCommonConst.OperatorConstTemplet.list.Length < slotCnt)
			{
				return;
			}
			this.targetId = NKMCommonConst.OperatorConstTemplet.list[slotCnt].itemId;
			this.m_iChangeValue = -1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
			this.m_bMaxLevelBreak = false;
		}

		// Token: 0x0600713A RID: 28986 RVA: 0x00259B8E File Offset: 0x00257D8E
		private void OnMinusDown_1(PointerEventData eventData)
		{
			this.OnClickMinus(0);
		}

		// Token: 0x0600713B RID: 28987 RVA: 0x00259B97 File Offset: 0x00257D97
		private void OnPlusDown_1(PointerEventData eventData)
		{
			this.OnClickPlus(0);
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x00259BA0 File Offset: 0x00257DA0
		private void OnMinusDown_2(PointerEventData eventData)
		{
			this.OnClickMinus(1);
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x00259BA9 File Offset: 0x00257DA9
		private void OnPlusDown_2(PointerEventData eventData)
		{
			this.OnClickPlus(1);
		}

		// Token: 0x0600713E RID: 28990 RVA: 0x00259BB2 File Offset: 0x00257DB2
		private void OnMinusDown_3(PointerEventData eventData)
		{
			this.OnClickMinus(2);
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x00259BBB File Offset: 0x00257DBB
		private void OnPlusDown_3(PointerEventData eventData)
		{
			this.OnClickPlus(2);
		}

		// Token: 0x06007140 RID: 28992 RVA: 0x00259BC4 File Offset: 0x00257DC4
		private void OnButtonUp()
		{
			this.m_iChangeValue = 0;
			this.m_fDelay = 0.35f;
			this.m_bPress = false;
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x00259BE0 File Offset: 0x00257DE0
		private void OnUpdateButtonHold()
		{
			if (!this.m_bPress)
			{
				return;
			}
			if (this.m_OperatorData == null)
			{
				return;
			}
			if (this.m_bMaxLevelBreak && this.m_iChangeValue > 0)
			{
				return;
			}
			if (this.m_bMaxLevelBreak)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_MAX_LEVEL_LOYALTY, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.m_fHoldTime += Time.deltaTime;
			if (this.m_fHoldTime >= this.m_fDelay)
			{
				this.m_fHoldTime = 0f;
				this.m_fDelay *= 0.8f;
				int num = (this.m_fDelay < 0.01f) ? 5 : 1;
				this.m_fDelay = Mathf.Clamp(this.m_fDelay, 0.01f, 0.35f);
				MiscItemData miscItemData = this.m_lstMaterials.Find((MiscItemData e) => e.itemId == this.targetId);
				if (miscItemData == null)
				{
					return;
				}
				this.m_bWasHold = true;
				int num2 = 0;
				while (num2 < num && this.m_bPress && !this.m_bMaxLevelBreak)
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
					this.RefreshData();
					num2++;
				}
			}
			this.RefreshUI();
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x00259DAC File Offset: 0x00257FAC
		public void OnChangeCount(int targetItemId, bool bPlus = true)
		{
			this.targetId = targetItemId;
			if (this.m_bWasHold)
			{
				this.m_bWasHold = false;
				return;
			}
			if (this.m_OperatorData == null)
			{
				return;
			}
			if (this.m_bMaxLevelBreak)
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
			this.RefreshData();
			this.RefreshUI();
		}

		// Token: 0x04005CF3 RID: 23795
		public Text m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_PREV_LV_COUNT;

		// Token: 0x04005CF4 RID: 23796
		public Text NKM_UI_PERSONNEL_NEGOTIATE_UNIT_NEXT_LV_COUNT;

		// Token: 0x04005CF5 RID: 23797
		public Image m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE;

		// Token: 0x04005CF6 RID: 23798
		public Image m_NKM_UI_PERSONNEL_NEGOTIATE_UNIT_LV_GAUGE_UP;

		// Token: 0x04005CF7 RID: 23799
		public Text m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT;

		// Token: 0x04005CF8 RID: 23800
		public Text m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT_1;

		// Token: 0x04005CF9 RID: 23801
		public Text m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_EXPTEXT_2;

		// Token: 0x04005CFA RID: 23802
		public Text m_NKM_UI_PERSONNEL_NEGOTIATE_RESULT_EXP_COUNT;

		// Token: 0x04005CFB RID: 23803
		public GameObject m_NKM_UI_PERSONNEL_NEGOTIATE_READY_EXP_LEVELMAX_TEXT;

		// Token: 0x04005CFC RID: 23804
		public Text m_STAT_HP_NUMBER;

		// Token: 0x04005CFD RID: 23805
		public Text m_STAT_ATT_NUMBER;

		// Token: 0x04005CFE RID: 23806
		public Text m_STAT_DEF_NUMBER;

		// Token: 0x04005CFF RID: 23807
		public Text m_STAT_SKILL_COOL_NUMBER;

		// Token: 0x04005D00 RID: 23808
		public GameObject m_NKM_UI_OPERATOR_INFO_DESC_BUTTON;

		// Token: 0x04005D01 RID: 23809
		public NKCUIComStateButton m_BUTTON_SKILLUP;

		// Token: 0x04005D02 RID: 23810
		public NKCUIComStateButton m_BUTTON_LEVELUP;

		// Token: 0x04005D03 RID: 23811
		[Header("재료")]
		public NKCUIComStateButton m_csbtnMaterial_1_Add;

		// Token: 0x04005D04 RID: 23812
		public NKCUIComStateButton m_csbtnMaterial_1_Minus;

		// Token: 0x04005D05 RID: 23813
		public NKCUIComStateButton m_csbtnMaterial_2_Add;

		// Token: 0x04005D06 RID: 23814
		public NKCUIComStateButton m_csbtnMaterial_2_Minus;

		// Token: 0x04005D07 RID: 23815
		public NKCUIComStateButton m_csbtnMaterial_3_Add;

		// Token: 0x04005D08 RID: 23816
		public NKCUIComStateButton m_csbtnMaterial_3_Minus;

		// Token: 0x04005D09 RID: 23817
		[Space]
		public List<Text> m_lstMaterialUseCount = new List<Text>(3);

		// Token: 0x04005D0A RID: 23818
		public List<NKCUIItemCostSlot> m_lstUIItemReq = new List<NKCUIItemCostSlot>(3);

		// Token: 0x04005D0B RID: 23819
		public List<Text> m_lstMaterialHaveCount = new List<Text>(3);

		// Token: 0x04005D0C RID: 23820
		public Text m_COUNT;

		// Token: 0x04005D0D RID: 23821
		public GameObject m_NKM_UI_OPERATOR_INFO_NEGOTIATE_REQUIRE;

		// Token: 0x04005D0E RID: 23822
		public NKCUIComStateButton m_NKM_UI_OPERATOR_INFO_NEGOTIATE_REQUIRE_START;

		// Token: 0x04005D0F RID: 23823
		public GameObject m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_BG_OFF;

		// Token: 0x04005D10 RID: 23824
		public Image m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_ICON;

		// Token: 0x04005D11 RID: 23825
		public Text m_NKM_UI_PERSONNEL_NEGOTIATE_REQUIRE_START_TEXT;

		// Token: 0x04005D12 RID: 23826
		[Header("애니메이션")]
		public Animator m_LevelUpEffect;

		// Token: 0x04005D13 RID: 23827
		private NKMOperator m_OperatorData;

		// Token: 0x04005D14 RID: 23828
		private NKCUIOperatorInfoPopupLevelUp.OnStart dOnStart;

		// Token: 0x04005D15 RID: 23829
		private Color TEXT_COLOR_YELLOW = new Color(1f, 0.8117647f, 0.23137255f);

		// Token: 0x04005D16 RID: 23830
		private List<MiscItemData> m_lstMaterials = new List<MiscItemData>(3);

		// Token: 0x04005D17 RID: 23831
		private bool m_bMaxLevelBreak;

		// Token: 0x04005D18 RID: 23832
		private int m_iExpectLevel;

		// Token: 0x04005D19 RID: 23833
		private int m_iEarnExp;

		// Token: 0x04005D1A RID: 23834
		private bool m_bWaitResultPacket;

		// Token: 0x04005D1B RID: 23835
		private string FX_SLOT_0 = "FX_LEVELUP_SLOT_0";

		// Token: 0x04005D1C RID: 23836
		private string FX_SLOT_1 = "FX_LEVELUP_SLOT_1";

		// Token: 0x04005D1D RID: 23837
		private string FX_SLOT_2 = "FX_LEVELUP_SLOT_2";

		// Token: 0x04005D1E RID: 23838
		private string BOOL_KEY_LEVELUP = "IsLevelup";

		// Token: 0x04005D1F RID: 23839
		public const float PRESS_GAP_MAX = 0.35f;

		// Token: 0x04005D20 RID: 23840
		public const float PRESS_GAP_MIN = 0.01f;

		// Token: 0x04005D21 RID: 23841
		public const float DAMPING = 0.8f;

		// Token: 0x04005D22 RID: 23842
		private float m_fDelay = 0.5f;

		// Token: 0x04005D23 RID: 23843
		private float m_fHoldTime;

		// Token: 0x04005D24 RID: 23844
		private int m_iChangeValue;

		// Token: 0x04005D25 RID: 23845
		private bool m_bPress;

		// Token: 0x04005D26 RID: 23846
		private bool m_bWasHold;

		// Token: 0x04005D27 RID: 23847
		private int targetId;

		// Token: 0x0200175C RID: 5980
		// (Invoke) Token: 0x0600B302 RID: 45826
		public delegate void OnStart(List<MiscItemData> lstMaterials);
	}
}

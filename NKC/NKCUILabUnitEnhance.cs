using System;
using System.Collections.Generic;
using ClientPacket.Unit;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B1 RID: 2481
	public class NKCUILabUnitEnhance : MonoBehaviour
	{
		// Token: 0x0600682D RID: 26669 RVA: 0x0021AAB4 File Offset: 0x00218CB4
		private void OnDisable()
		{
			for (int i = 0; i < this.m_lstObjUnitSlot.Count; i++)
			{
				this.m_lstObjUnitSlot[i].OffEnhanceEffect();
			}
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x0021AAE8 File Offset: 0x00218CE8
		public void Init(NKCUINPCProfessorOlivia npcProfessorOlivia, NKCUILabUnitEnhance.GetUnitList GetUnitList = null)
		{
			if (this.m_sbtnAutoSelect != null)
			{
				this.m_sbtnAutoSelect.PointerClick.RemoveAllListeners();
				this.m_sbtnAutoSelect.PointerClick.AddListener(new UnityAction(this.OnBtnAutoSelect));
			}
			if (this.m_sbtnClear != null)
			{
				this.m_sbtnClear.PointerClick.RemoveAllListeners();
				this.m_sbtnClear.PointerClick.AddListener(new UnityAction(this.ClearAllFeedUnitSlots));
			}
			if (this.m_sbtnEnhance != null)
			{
				this.m_sbtnEnhance.PointerClick.RemoveAllListeners();
				this.m_sbtnEnhance.PointerClick.AddListener(new UnityAction(this.Enhance));
			}
			if (this.m_csbtnDetail != null)
			{
				this.m_csbtnDetail.PointerClick.RemoveAllListeners();
				this.m_csbtnDetail.PointerClick.AddListener(new UnityAction(this.OnBtnDetail));
			}
			foreach (NKCUILabUnitEnhance.ConsumeUnitSlot consumeUnitSlot in this.m_lstObjUnitSlot)
			{
				consumeUnitSlot.Init();
			}
			this.InitChildObject();
			if (GetUnitList != null)
			{
				this.dGetUnitList = GetUnitList;
			}
			if (npcProfessorOlivia != null)
			{
				this.dOnAutoSelectPlayVoice = new NKCUILabUnitEnhance.OnAutoSelectPlayVoice(npcProfessorOlivia.PlayAni);
			}
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x0021AC4C File Offset: 0x00218E4C
		private void InitChildObject()
		{
			this.m_NKM_UI_LAB_UNIT_INFO_RESET_BG_DISABLE = GameObject.Find("NKM_UI_LAB_UNIT_INFO_RESET_BG_DISABLE");
			this.m_NKM_UI_LAB_UNIT_INFO_ENTER_BG_DISABLE = GameObject.Find("NKM_UI_LAB_UNIT_INFO_ENTER_BG_DISABLE");
			this.m_NKM_UI_LAB_UNIT_INFO_ENTER_LIGHT = GameObject.Find("NKM_UI_LAB_UNIT_INFO_ENTER_LIGHT");
			this.m_NKM_UI_LAB_UNIT_INFO_RESET_TEXT = GameObject.Find("NKM_UI_LAB_UNIT_INFO_RESET_TEXT").GetComponent<Text>();
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x0021AC9E File Offset: 0x00218E9E
		public void Cleanup()
		{
			this.m_targetUnitData = null;
			NKCPopupUnitInfoDetail.CheckInstanceAndClose();
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x0021ACAC File Offset: 0x00218EAC
		public void SetData(NKMUnitData unitData)
		{
			this.m_currentSlotUIDList.Clear();
			NKCPopupUnitInfoDetail.CheckInstanceAndClose();
			this.m_targetUnitData = unitData;
			for (int i = 0; i < this.m_lstObjUnitSlot.Count; i++)
			{
				this.m_lstObjUnitSlot[i].m_Slot.SetEmptyMaterial(new NKCUISlot.OnClick(this.OnClickSlot));
				NKCUtil.SetGameobjectActive(this.m_lstObjUnitSlot[i].m_objExpBonus, false);
			}
			this.UpdateStatInfo();
			this.UpdateRequiredCredit();
			this.SwitchEtcObject(this.CanEnhance(unitData));
			NKCUtil.SetGameobjectActive(this.m_sbtnAutoSelect.gameObject, this.m_targetUnitData != null);
			NKCUtil.SetGameobjectActive(this.m_csbtnDetail.gameObject, this.m_targetUnitData != null);
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x0021AD6C File Offset: 0x00218F6C
		private void SwitchEtcObject(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_LAB_UNIT_INFO_ENTER_LIGHT, bActive);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_LAB_UNIT_INFO_RESET_BG_DISABLE, !bActive);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_LAB_UNIT_INFO_ENTER_BG_DISABLE, !bActive);
			this.m_NKM_UI_LAB_UNIT_INFO_RESET_TEXT.color = (bActive ? Color.white : NKCUtil.GetButtonUIColor(false));
			this.m_NKM_UI_LAB_UNIT_INFO_ENTER_TEXT.color = NKCUtil.GetButtonUIColor(bActive);
			this.m_NKM_UI_LAB_UNIT_INFO_ENTER_ICON.color = NKCUtil.GetButtonUIColor(bActive);
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x0021ADE0 File Offset: 0x00218FE0
		public void SetDataWithoutReset(NKMUnitData unitData)
		{
			this.m_targetUnitData = unitData;
			for (int i = 0; i < this.m_lstObjUnitSlot.Count; i++)
			{
				this.m_lstObjUnitSlot[i].SetEnhanceEffect(!this.m_lstObjUnitSlot[i].m_Slot.IsEmpty());
			}
			this.UpdateRequiredCredit();
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x0021AE3C File Offset: 0x0021903C
		public void Enhance()
		{
			if (this.m_targetUnitData == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENHANCE_NEED_SET_TARGET_UNIT, null, "");
				return;
			}
			if (this.m_targetUnitData.IsSeized)
			{
				NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKMDeckData deckDataByUnitUID = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckDataByUnitUID(this.m_targetUnitData.m_UnitUID);
			if (deckDataByUnitUID != null)
			{
				NKM_DECK_STATE state = deckDataByUnitUID.GetState();
				if (state == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
			}
			if (this.m_currentSlotUIDList != null)
			{
				this.m_currentSlotUIDList.RemoveAll((long x) => x == 0L);
				int num = NKMEnhanceManager.CalculateCreditCost(this.m_currentSlotUIDList);
				if (NKCScenManager.CurrentUserData().GetCredit() < (long)num)
				{
					NKCShopManager.OpenItemLackPopup(1, num);
					return;
				}
				if (this.m_currentSlotUIDList.Count > 0)
				{
					foreach (long unitUID in this.m_currentSlotUIDList)
					{
						if (!this.IsDismissibleUnit(unitUID))
						{
							NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_ENHANCE_INCLUDE_HIGH_RARITY_AND_LEVEL", false), delegate()
							{
								this.Send_ENHANCE_UNIT_REQ();
							}, delegate()
							{
								this.m_currentSlotUIDList.Clear();
								NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_ENHANCE_CANCELED", false), new NKCPopupOKCancel.OnButton(this.UpdateUnitSlots), "");
							}, false);
							return;
						}
					}
					this.Send_ENHANCE_UNIT_REQ();
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENHANCE_NEED_SET_CONSUME_UNIT, null, "");
			}
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x0021AFDC File Offset: 0x002191DC
		private void Send_ENHANCE_UNIT_REQ()
		{
			NKMPacket_ENHANCE_UNIT_REQ nkmpacket_ENHANCE_UNIT_REQ = new NKMPacket_ENHANCE_UNIT_REQ();
			nkmpacket_ENHANCE_UNIT_REQ.unitUID = this.m_targetUnitData.m_UnitUID;
			nkmpacket_ENHANCE_UNIT_REQ.consumeUnitUIDList = this.m_currentSlotUIDList;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_ENHANCE_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06006836 RID: 26678 RVA: 0x0021B020 File Offset: 0x00219220
		private void RemoveUnDismissibleUnit()
		{
			int num = 0;
			while (num < this.m_currentSlotUIDList.Count && num >= 0)
			{
				if (!this.IsDismissibleUnit(this.m_currentSlotUIDList[num]))
				{
					this.m_currentSlotUIDList.RemoveAt(num);
					num--;
				}
				num++;
			}
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x0021B06C File Offset: 0x0021926C
		private void UpdateStatInfo()
		{
			NKMUnitData targetUnitData = this.m_targetUnitData;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetUnitData);
			if (unitTempletBase != null)
			{
				Dictionary<NKM_STAT_TYPE, int> dictionary = NKMEnhanceManager.CalculateExpGain(NKCScenManager.CurrentUserData().m_ArmyData, this.m_currentSlotUIDList, unitTempletBase.m_NKM_UNIT_ROLE_TYPE);
				this.m_UIExpBarHP.SetData(targetUnitData, NKM_STAT_TYPE.NST_HP, dictionary[NKM_STAT_TYPE.NST_HP]);
				this.m_UIExpBarAttack.SetData(targetUnitData, NKM_STAT_TYPE.NST_ATK, dictionary[NKM_STAT_TYPE.NST_ATK]);
				this.m_UIExpBarDefense.SetData(targetUnitData, NKM_STAT_TYPE.NST_DEF, dictionary[NKM_STAT_TYPE.NST_DEF]);
				this.m_UIExpBarCritical.SetData(targetUnitData, NKM_STAT_TYPE.NST_CRITICAL, dictionary[NKM_STAT_TYPE.NST_CRITICAL]);
				this.m_UIExpBarHit.SetData(targetUnitData, NKM_STAT_TYPE.NST_HIT, dictionary[NKM_STAT_TYPE.NST_HIT]);
				this.m_UIExpBarEvade.SetData(targetUnitData, NKM_STAT_TYPE.NST_EVADE, dictionary[NKM_STAT_TYPE.NST_EVADE]);
				this.m_txtPower.text = targetUnitData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null).ToString();
				return;
			}
			this.m_UIExpBarHP.SetData(null, NKM_STAT_TYPE.NST_HP, 0);
			this.m_UIExpBarAttack.SetData(null, NKM_STAT_TYPE.NST_ATK, 0);
			this.m_UIExpBarDefense.SetData(null, NKM_STAT_TYPE.NST_DEF, 0);
			this.m_UIExpBarCritical.SetData(null, NKM_STAT_TYPE.NST_CRITICAL, 0);
			this.m_UIExpBarHit.SetData(null, NKM_STAT_TYPE.NST_HIT, 0);
			this.m_UIExpBarEvade.SetData(null, NKM_STAT_TYPE.NST_EVADE, 0);
			this.m_txtPower.text = "";
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x0021B1AC File Offset: 0x002193AC
		private int GetMaxPlusStatValue(float fPlusEXP, float fEXPPerStatUp, float fRemainStat)
		{
			float num = fPlusEXP / fEXPPerStatUp;
			if (num >= fRemainStat)
			{
				num = fRemainStat;
			}
			return (int)num;
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x0021B1CC File Offset: 0x002193CC
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (this.m_targetUnitData == null)
			{
				return;
			}
			if (slotData != null && slotData.UID != 0L)
			{
				this.m_currentSlotUIDList.Remove(slotData.UID);
				this.UpdateUnitSlots();
				return;
			}
			if (NKMEnhanceManager.CheckUnitFullEnhance(this.m_targetUnitData))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ALREADY_ENHANCE_MAX, null, "");
				return;
			}
			NKCUIUnitSelectList.UnitSelectListOptions options = this.MakeUnitSelectListOption();
			options.dOnSelectedUnitWarning = new NKCUIUnitSelectList.UnitSelectListOptions.OnSelectedUnitWarning(this.CheckSelectedUnit);
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.UpdateUnitSlots), null, null, null, null);
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x0021B260 File Offset: 0x00219460
		private bool CheckSelectedUnit(long selectUnitUID, List<long> selectedUnitList, out string msg)
		{
			msg = string.Empty;
			if (!this.IsDismissibleUnit(selectUnitUID))
			{
				msg = NKCStringTable.GetString("SI_DP_ENHANCE_SELECT_HIGH_RARITY_AND_LEVEL", false);
				return true;
			}
			List<long> list = new List<long>();
			if (selectedUnitList != null && selectedUnitList.Count > 0)
			{
				list.AddRange(selectedUnitList);
			}
			if (this.WillBecomeFullExp(this.m_targetUnitData, new HashSet<long>(list)))
			{
				list.RemoveAt(list.Count - 1);
				if (!this.WillBecomeFullExp(this.m_targetUnitData, new HashSet<long>(list)))
				{
					msg = NKCStringTable.GetString("SI_DP_ENHANCE_ALREADY_MAX_CONSUME_UNIT", false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x0021B2EC File Offset: 0x002194EC
		private bool IsDismissibleUnit(long unitUID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(unitUID);
				if (unitFromUID.m_UnitLevel > 1)
				{
					return false;
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID);
				if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
				{
					return true;
				}
				if (unitFromUID.GetUnitGrade() >= NKM_UNIT_GRADE.NUG_SR)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x0021B33C File Offset: 0x0021953C
		public void ClearFeedUnitSlot(int index)
		{
			if (index < 0 || index >= this.m_lstObjUnitSlot.Count)
			{
				return;
			}
			this.m_lstObjUnitSlot[index].m_Slot.SetEmptyMaterial(new NKCUISlot.OnClick(this.OnClickSlot));
			NKCUtil.SetGameobjectActive(this.m_lstObjUnitSlot[index].m_objExpBonus, false);
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x0021B395 File Offset: 0x00219595
		public void ClearAllFeedUnitSlots()
		{
			if (this.m_currentSlotUIDList.Count > 0)
			{
				this.m_currentSlotUIDList.Clear();
			}
			this.UpdateUnitSlots();
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x0021B3B8 File Offset: 0x002195B8
		private void UpdateUnitSlots(List<long> unitUID)
		{
			NKCUIUnitSelectList.CheckInstanceAndClose();
			bool flag = false;
			if (unitUID.Count == this.m_currentSlotUIDList.Count)
			{
				for (int i = 0; i < unitUID.Count; i++)
				{
					if (!this.m_currentSlotUIDList.Contains(unitUID[i]))
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				this.m_currentSlotUIDList.Clear();
				this.m_currentSlotUIDList.AddRange(unitUID);
				this.UpdateUnitSlots();
			}
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x0021B42C File Offset: 0x0021962C
		private void UpdateUnitSlots()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_targetUnitData);
			NKM_UNIT_ROLE_TYPE nkm_UNIT_ROLE_TYPE = (unitTempletBase != null) ? unitTempletBase.m_NKM_UNIT_ROLE_TYPE : NKM_UNIT_ROLE_TYPE.NURT_INVALID;
			for (int i = 0; i < this.m_lstObjUnitSlot.Count; i++)
			{
				if (i < this.m_currentSlotUIDList.Count)
				{
					NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(this.m_currentSlotUIDList[i]);
					if (unitFromUID != null)
					{
						this.m_lstObjUnitSlot[i].m_Slot.SetUnitData(unitFromUID, false, false, true, new NKCUISlot.OnClick(this.OnClickSlot));
						NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitFromUID);
						NKCUtil.SetGameobjectActive(this.m_lstObjUnitSlot[i].m_objExpBonus, nkm_UNIT_ROLE_TYPE == unitTempletBase2.m_NKM_UNIT_ROLE_TYPE);
					}
					else
					{
						this.m_lstObjUnitSlot[i].m_Slot.SetEmptyMaterial(new NKCUISlot.OnClick(this.OnClickSlot));
						NKCUtil.SetGameobjectActive(this.m_lstObjUnitSlot[i].m_objExpBonus, false);
					}
				}
				else
				{
					this.m_lstObjUnitSlot[i].m_Slot.SetEmptyMaterial(new NKCUISlot.OnClick(this.OnClickSlot));
					NKCUtil.SetGameobjectActive(this.m_lstObjUnitSlot[i].m_objExpBonus, false);
				}
			}
			this.UpdateStatInfo();
			this.UpdateRequiredCredit();
			this.SwitchEtcObject(this.CanEnhance(this.m_targetUnitData));
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x0021B58C File Offset: 0x0021978C
		public void UpdateRequiredCredit()
		{
			this.m_currentSlotUIDList.RemoveAll((long x) => x == 0L);
			int reqCnt = NKMEnhanceManager.CalculateCreditCost(this.m_currentSlotUIDList);
			this.m_costSlot.SetData(1, reqCnt, NKCScenManager.CurrentUserData().GetCredit(), true, true, false);
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x0021B5EA File Offset: 0x002197EA
		private void OnBtnAutoSelect()
		{
			this.AutoSelect();
		}

		// Token: 0x06006842 RID: 26690 RVA: 0x0021B5F4 File Offset: 0x002197F4
		private void AutoSelect()
		{
			if (this.m_targetUnitData == null)
			{
				return;
			}
			if (NKMEnhanceManager.CheckUnitFullEnhance(this.m_targetUnitData))
			{
				return;
			}
			NKCUnitSort nkcunitSort = new NKCUnitSort(NKCScenManager.CurrentUserData(), this.MakeSortOptions(true));
			HashSet<long> hashSet = new HashSet<long>();
			foreach (long item in this.m_currentSlotUIDList)
			{
				hashSet.Add(item);
			}
			if (this.WillBecomeFullExp(this.m_targetUnitData, hashSet))
			{
				return;
			}
			for (int i = this.m_currentSlotUIDList.Count; i < this.m_lstObjUnitSlot.Count; i++)
			{
				NKMUnitData nkmunitData = nkcunitSort.AutoSelect(hashSet, new NKCUnitSortSystem.AutoSelectExtraFilter(this.AutoSelectFilterSameType));
				if (nkmunitData == null)
				{
					nkmunitData = nkcunitSort.AutoSelect(hashSet, new NKCUnitSortSystem.AutoSelectExtraFilter(this.AutoSelectFilterAnyType));
				}
				if (nkmunitData == null)
				{
					break;
				}
				hashSet.Add(nkmunitData.m_UnitUID);
				if (this.WillBecomeFullExp(this.m_targetUnitData, hashSet))
				{
					break;
				}
			}
			List<long> list = new List<long>();
			list.AddRange(hashSet);
			this.UpdateUnitSlots(list);
			if (this.dOnAutoSelectPlayVoice != null)
			{
				this.dOnAutoSelectPlayVoice(NPC_ACTION_TYPE.MATERIAL_AUTO_SELECT, false);
			}
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x0021B728 File Offset: 0x00219928
		private bool WillBecomeFullExp(NKMUnitData targetUnit, HashSet<long> hsTargetUnits)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_targetUnitData);
			if (unitTempletBase == null)
			{
				return true;
			}
			Dictionary<NKM_STAT_TYPE, int> dictionary = NKMEnhanceManager.CalculateExpGain(armyData, new List<long>(hsTargetUnits), unitTempletBase.m_NKM_UNIT_ROLE_TYPE);
			foreach (NKM_STAT_TYPE nkm_STAT_TYPE in NKMEnhanceManager.s_lstEnhancebleStat)
			{
				int num = NKMEnhanceManager.CalculateMaxEXP(targetUnit, nkm_STAT_TYPE);
				if (dictionary[nkm_STAT_TYPE] + targetUnit.m_listStatEXP[(int)nkm_STAT_TYPE] < num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x0021B7D0 File Offset: 0x002199D0
		private bool AutoSelectFilterSameType(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_targetUnitData);
			if (unitTempletBase == null)
			{
				return false;
			}
			NKM_UNIT_ROLE_TYPE nkm_UNIT_ROLE_TYPE = unitTempletBase.m_NKM_UNIT_ROLE_TYPE;
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase2 == null)
			{
				return false;
			}
			if (unitData.m_UnitLevel > 1)
			{
				return false;
			}
			if (nkm_UNIT_ROLE_TYPE != unitTempletBase2.m_NKM_UNIT_ROLE_TYPE)
			{
				return false;
			}
			if (unitTempletBase2.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return true;
			}
			NKM_UNIT_GRADE nkm_UNIT_GRADE = unitTempletBase2.m_NKM_UNIT_GRADE;
			if (nkm_UNIT_GRADE != NKM_UNIT_GRADE.NUG_N)
			{
				if (nkm_UNIT_GRADE - NKM_UNIT_GRADE.NUG_R > 2)
				{
				}
				return false;
			}
			return true;
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x0021B838 File Offset: 0x00219A38
		private bool AutoSelectFilterAnyType(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase == null)
			{
				return false;
			}
			if (unitData.m_UnitLevel > 1)
			{
				return false;
			}
			if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return true;
			}
			NKM_UNIT_GRADE nkm_UNIT_GRADE = unitTempletBase.m_NKM_UNIT_GRADE;
			return nkm_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N || nkm_UNIT_GRADE - NKM_UNIT_GRADE.NUG_R > 2;
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x0021B87E File Offset: 0x00219A7E
		private void OnBtnDetail()
		{
			if (this.m_targetUnitData == null)
			{
				return;
			}
			if (NKCPopupUnitInfoDetail.IsInstanceOpen)
			{
				NKCPopupUnitInfoDetail.CheckInstanceAndClose();
				return;
			}
			NKCPopupUnitInfoDetail.InstanceOpen(this.m_targetUnitData, NKCPopupUnitInfoDetail.UnitInfoDetailType.lab, null);
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x0021B8A4 File Offset: 0x00219AA4
		private NKCUIUnitSelectList.UnitSelectListOptions MakeUnitSelectListOption()
		{
			NKCUIUnitSelectList.UnitSelectListOptions result = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			result.m_SortOptions = this.MakeSortOptions(false);
			result.bShowRemoveSlot = false;
			result.bShowHideDeckedUnitMenu = false;
			result.strUpsideMenuName = NKCUtilString.GET_STRING_ENHANCE_SELECT_CONSUM_UNIT;
			result.iMaxMultipleSelect = this.m_lstObjUnitSlot.Count;
			result.dOnAutoSelectFilter = new NKCUIUnitSelectList.UnitSelectListOptions.OnAutoSelectFilter(this.AutoSelectFilterAnyType);
			result.dOnSlotSetData = new NKCUIUnitSelectList.UnitSelectListOptions.OnSlotSetData(this.OnSlotSetData);
			result.strEmptyMessage = NKCUtilString.GET_STRING_ENHANCE_NO_EXIST_CONSUME_UNIT;
			result.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			result.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			result.setSelectedUnitUID = new HashSet<long>(this.m_currentSlotUIDList);
			if (NKCGameEventManager.IsEventPlaying())
			{
				result.m_SortOptions.PreemptiveSortFunc = new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.TutorialSort);
			}
			return result;
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x0021B978 File Offset: 0x00219B78
		private int TutorialSort(NKMUnitData lhs, NKMUnitData rhs)
		{
			bool flag = lhs.m_UnitID == 1008;
			bool flag2 = rhs.m_UnitID == 1008;
			if (flag != flag2)
			{
				return flag2.CompareTo(flag);
			}
			if (lhs.m_UnitLevel != rhs.m_UnitLevel)
			{
				return lhs.m_UnitLevel.CompareTo(rhs.m_UnitLevel);
			}
			return lhs.m_UnitUID.CompareTo(rhs.m_UnitUID);
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x0021B9E0 File Offset: 0x00219BE0
		private void OnSlotSetData(NKCUIUnitSelectListSlotBase cUnitSlot, NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex)
		{
			if (cNKMUnitData == null)
			{
				return;
			}
			NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = cUnitSlot as NKCUIUnitSelectListSlot;
			if (nkcuiunitSelectListSlot != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_targetUnitData);
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(cNKMUnitData);
				if (unitTempletBase != null && unitTempletBase2 != null && unitTempletBase.m_NKM_UNIT_ROLE_TYPE == unitTempletBase2.m_NKM_UNIT_ROLE_TYPE)
				{
					nkcuiunitSelectListSlot.SetExpBonusMark(true);
					return;
				}
				nkcuiunitSelectListSlot.SetExpBonusMark(false);
			}
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x0021BA38 File Offset: 0x00219C38
		private NKCUnitSortSystem.UnitListOptions MakeSortOptions(bool bIsAutoSelect)
		{
			NKCUnitSortSystem.UnitListOptions result = new NKCUnitSortSystem.UnitListOptions
			{
				bDescending = false,
				setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>(),
				lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Rarity_Low,
					NKCUnitSortSystem.eSortOption.Level_Low
				},
				bExcludeDeckedUnit = false,
				bExcludeLockedUnit = false,
				bHideDeckedUnit = false,
				bUseDeckedState = true,
				bUseLockedState = true,
				bPushBackUnselectable = true,
				eDeckType = NKM_DECK_TYPE.NDT_NORMAL,
				bIncludeUndeckableUnit = true,
				setExcludeUnitID = NKCUnitSortSystem.GetDefaultExcludeUnitIDs(),
				AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.IsUnitHaveEnhanceExp)
			};
			result.setExcludeUnitUID = new HashSet<long>
			{
				this.m_targetUnitData.m_UnitUID
			};
			if (bIsAutoSelect)
			{
				result.PreemptiveSortFunc = new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByRoleForAutoSelect);
			}
			else
			{
				result.PreemptiveSortFunc = new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByRole);
			}
			return result;
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x0021BB28 File Offset: 0x00219D28
		private bool IsUnitHaveEnhanceExp(NKMUnitData unitData)
		{
			if (unitData != null)
			{
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
				if (unitStatTemplet != null)
				{
					for (int i = 0; i < NKMEnhanceManager.s_lstEnhancebleStat.Count; i++)
					{
						int statType = (int)NKMEnhanceManager.s_lstEnhancebleStat[i];
						if (unitStatTemplet.m_StatData.GetStatEnhanceFeedEXP(statType) > 0f)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x0021BB7E File Offset: 0x00219D7E
		public void UnitUpdated(long uid, NKMUnitData unitData)
		{
			if (this.m_targetUnitData != null && this.m_targetUnitData.m_UnitUID == uid)
			{
				this.SetDataWithoutReset(unitData);
			}
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x0021BBA0 File Offset: 0x00219DA0
		private int Compare(NKMUnitData lhs, NKMUnitData rhs, bool TrophyFirst)
		{
			if (this.m_targetUnitData == null)
			{
				return 0;
			}
			int num = lhs.m_bLock.CompareTo(rhs.m_bLock);
			if (num != 0)
			{
				return num;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_targetUnitData.m_UnitID);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(lhs.m_UnitID);
			NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(rhs.m_UnitID);
			if (unitTempletBase == null || unitTempletBase2 == null || unitTempletBase3 == null)
			{
				return 0;
			}
			bool flag = unitTempletBase.m_NKM_UNIT_ROLE_TYPE == unitTempletBase2.m_NKM_UNIT_ROLE_TYPE;
			bool flag2 = unitTempletBase.m_NKM_UNIT_ROLE_TYPE == unitTempletBase3.m_NKM_UNIT_ROLE_TYPE;
			if (flag != flag2)
			{
				return flag2.CompareTo(flag);
			}
			bool flag3 = unitTempletBase2.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER;
			bool flag4 = unitTempletBase3.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER;
			if (flag3 == flag4)
			{
				return 0;
			}
			if (TrophyFirst)
			{
				return flag4.CompareTo(flag3);
			}
			return flag3.CompareTo(flag4);
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x0021BC66 File Offset: 0x00219E66
		private int CompareByRole(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.Compare(lhs, rhs, true);
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x0021BC71 File Offset: 0x00219E71
		private int CompareByRoleForAutoSelect(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.Compare(lhs, rhs, true);
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x0021BC7C File Offset: 0x00219E7C
		private bool CanEnhance(NKMUnitData targetUnit)
		{
			if (targetUnit == null)
			{
				return false;
			}
			NKMDeckData deckDataByUnitUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckDataByUnitUID(targetUnit.m_UnitUID);
			if (deckDataByUnitUID != null)
			{
				NKM_DECK_STATE state = deckDataByUnitUID.GetState();
				if (state - NKM_DECK_STATE.DECK_STATE_WARFARE <= 1)
				{
					return false;
				}
			}
			return this.m_currentSlotUIDList.Count > 0;
		}

		// Token: 0x0400543D RID: 21565
		[Header("오른쪽 스탯 정보")]
		public NKCUIRectMove m_rmLabUnitStat;

		// Token: 0x0400543E RID: 21566
		public NKCUIRectMove m_rmLabUnitSlot;

		// Token: 0x0400543F RID: 21567
		public NKCUIComStatEnhanceBar m_UIExpBarHP;

		// Token: 0x04005440 RID: 21568
		public NKCUIComStatEnhanceBar m_UIExpBarAttack;

		// Token: 0x04005441 RID: 21569
		public NKCUIComStatEnhanceBar m_UIExpBarDefense;

		// Token: 0x04005442 RID: 21570
		public NKCUIComStatEnhanceBar m_UIExpBarCritical;

		// Token: 0x04005443 RID: 21571
		public NKCUIComStatEnhanceBar m_UIExpBarHit;

		// Token: 0x04005444 RID: 21572
		public NKCUIComStatEnhanceBar m_UIExpBarEvade;

		// Token: 0x04005445 RID: 21573
		public Text m_txtPower;

		// Token: 0x04005446 RID: 21574
		public NKCUIComStateButton m_csbtnDetail;

		// Token: 0x04005447 RID: 21575
		[Header("먹잇감 유닛 정보")]
		public List<NKCUILabUnitEnhance.ConsumeUnitSlot> m_lstObjUnitSlot;

		// Token: 0x04005448 RID: 21576
		[Header("가격")]
		public NKCUIItemCostSlot m_costSlot;

		// Token: 0x04005449 RID: 21577
		[Header("버튼들")]
		public NKCUIComStateButton m_sbtnAutoSelect;

		// Token: 0x0400544A RID: 21578
		public NKCUIComStateButton m_sbtnClear;

		// Token: 0x0400544B RID: 21579
		public NKCUIComStateButton m_sbtnEnhance;

		// Token: 0x0400544C RID: 21580
		private NKMUnitData m_targetUnitData;

		// Token: 0x0400544D RID: 21581
		private GameObject m_NKM_UI_LAB_UNIT_INFO_RESET_BG_DISABLE;

		// Token: 0x0400544E RID: 21582
		private GameObject m_NKM_UI_LAB_UNIT_INFO_ENTER_BG_DISABLE;

		// Token: 0x0400544F RID: 21583
		private GameObject m_NKM_UI_LAB_UNIT_INFO_ENTER_LIGHT;

		// Token: 0x04005450 RID: 21584
		private Text m_NKM_UI_LAB_UNIT_INFO_RESET_TEXT;

		// Token: 0x04005451 RID: 21585
		public Text m_NKM_UI_LAB_UNIT_INFO_ENTER_TEXT;

		// Token: 0x04005452 RID: 21586
		public Image m_NKM_UI_LAB_UNIT_INFO_ENTER_ICON;

		// Token: 0x04005453 RID: 21587
		private List<long> m_currentSlotUIDList = new List<long>();

		// Token: 0x04005454 RID: 21588
		private NKCUILabUnitEnhance.GetUnitList dGetUnitList;

		// Token: 0x04005455 RID: 21589
		private NKCUILabUnitEnhance.OnAutoSelectPlayVoice dOnAutoSelectPlayVoice;

		// Token: 0x020016A8 RID: 5800
		[Serializable]
		public class ConsumeUnitSlot
		{
			// Token: 0x0600B0E0 RID: 45280 RVA: 0x0035FAB3 File Offset: 0x0035DCB3
			public void Init()
			{
				this.m_Slot.Init();
				NKCUtil.SetGameobjectActive(this.m_objEffect, false);
				NKCUtil.SetGameobjectActive(this.m_objExpBonus, false);
			}

			// Token: 0x0600B0E1 RID: 45281 RVA: 0x0035FAD8 File Offset: 0x0035DCD8
			public void SetEnhanceEffect(bool value)
			{
				NKCUtil.SetGameobjectActive(this.m_objEffect, value);
			}

			// Token: 0x0600B0E2 RID: 45282 RVA: 0x0035FAE6 File Offset: 0x0035DCE6
			public void OffEnhanceEffect()
			{
				NKCUtil.SetGameobjectActive(this.m_objEffect, false);
			}

			// Token: 0x0600B0E3 RID: 45283 RVA: 0x0035FAF4 File Offset: 0x0035DCF4
			public bool HasUnitdata()
			{
				NKCUISlot.SlotData slotData = this.m_Slot.GetSlotData();
				return slotData != null && slotData.UID != 0L;
			}

			// Token: 0x0400A4F7 RID: 42231
			public NKCUISlot m_Slot;

			// Token: 0x0400A4F8 RID: 42232
			public GameObject m_objEffect;

			// Token: 0x0400A4F9 RID: 42233
			public GameObject m_objExpBonus;
		}

		// Token: 0x020016A9 RID: 5801
		// (Invoke) Token: 0x0600B0E6 RID: 45286
		public delegate List<long> GetUnitList(ref int selectIdx);

		// Token: 0x020016AA RID: 5802
		// (Invoke) Token: 0x0600B0EA RID: 45290
		public delegate void OnAutoSelectPlayVoice(NPC_ACTION_TYPE actionType, bool bMute);
	}
}

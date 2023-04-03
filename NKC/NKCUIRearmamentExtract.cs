using System;
using System.Collections.Generic;
using ClientPacket.Unit;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000804 RID: 2052
	public class NKCUIRearmamentExtract : MonoBehaviour
	{
		// Token: 0x06005147 RID: 20807 RVA: 0x0018A474 File Offset: 0x00188674
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnExtract, new UnityAction(this.OnClickExtract));
			NKCUtil.SetBindFunction(this.m_csbtnSynergyInfo, new UnityAction(this.OnClickSynergyInfo));
			NKCUIComStateButton[] lstSelectBtn = this.m_lstSelectBtn;
			for (int i = 0; i < lstSelectBtn.Length; i++)
			{
				NKCUtil.SetBindFunction(lstSelectBtn[i], new UnityAction(this.OnClickSelectList));
			}
			if (null != this.m_SynergyBonusSlot)
			{
				this.m_SynergyBonusSlot.Init();
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(this.m_MiscItemCode, 1L, 0);
				this.m_SynergyBonusSlot.SetData(data, true, new NKCUISlot.OnClick(this.OnClickExtractReward));
			}
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x0018A519 File Offset: 0x00188719
		public void Clear()
		{
			NKCUIUnitSelectList unitSelectList = this.UnitSelectList;
			if (unitSelectList != null)
			{
				unitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x0018A533 File Offset: 0x00188733
		public void Open()
		{
			this.m_lstSelectedUnitsUID.Clear();
			Animator aniExtract = this.m_AniExtract;
			if (aniExtract != null)
			{
				aniExtract.SetTrigger("ACTIVE");
			}
			this.UpdateUI();
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x0018A55C File Offset: 0x0018875C
		public void UpdateUI()
		{
			this.UpdateUnitSlotUI();
			this.UpdateExtractItemData();
			this.UpdateExtractResultItemUI();
			this.UpdateSynergyUI();
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x0018A576 File Offset: 0x00188776
		public void OnRecv(NKMPacket_EXTRACT_UNIT_ACK sPacket)
		{
			this.m_lstSelectedUnitsUID.Clear();
			this.UpdateUI();
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0018A58C File Offset: 0x0018878C
		private void UpdateUnitSlotUI()
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			for (int i = 0; i < NKMCommonConst.MaxExtractUnitSelect; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstPiece[i], this.m_lstSelectedUnitsUID.Count > i);
				NKMUnitData data = null;
				if (this.m_lstSelectedUnitsUID.Count > i)
				{
					data = armyData.GetUnitFromUID(this.m_lstSelectedUnitsUID[i]);
				}
				this.m_lstExtractUnitSlot[i].SetData(data);
			}
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0018A608 File Offset: 0x00188808
		private void UpdateExtractItemData()
		{
			this.m_lstExtractItemData.Clear();
			if (this.m_lstSelectedUnitsUID.Count <= 0)
			{
				Debug.Log("선택된 유닛이 없슈");
				return;
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (long unitUid in this.m_lstSelectedUnitsUID)
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
					if (unitTempletBase != null)
					{
						foreach (NKMRewardInfo nkmrewardInfo in unitTempletBase.ExtractRewards)
						{
							if (nkmrewardInfo.ID != 0 && nkmrewardInfo.Count != 0)
							{
								int id = nkmrewardInfo.ID;
								int count = nkmrewardInfo.Count;
								if (dictionary.ContainsKey(id))
								{
									Dictionary<int, int> dictionary2 = dictionary;
									int key = id;
									dictionary2[key] += count;
								}
								else
								{
									dictionary.Add(id, count);
								}
							}
						}
						if (unitFromUID.FromContract && unitTempletBase.ExtractRewardFromContract != null)
						{
							NKMRewardInfo extractRewardFromContract = unitTempletBase.ExtractRewardFromContract;
							if (dictionary.ContainsKey(extractRewardFromContract.ID))
							{
								Dictionary<int, int> dictionary2 = dictionary;
								int key = extractRewardFromContract.ID;
								dictionary2[key] += extractRewardFromContract.Count;
							}
							else
							{
								dictionary.Add(extractRewardFromContract.ID, extractRewardFromContract.Count);
							}
						}
					}
				}
			}
			if (dictionary.Count <= 0)
			{
				return;
			}
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_MISC, keyValuePair.Key, keyValuePair.Value, 0);
				if (slotData != null)
				{
					this.m_lstExtractItemData.Add(slotData);
				}
			}
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x0018A834 File Offset: 0x00188A34
		private void UpdateExtractResultItemUI()
		{
			foreach (NKCUISlot nkcuislot in this.m_lstExtractItem)
			{
				nkcuislot.CleanUp();
				UnityEngine.Object.Destroy(nkcuislot.gameObject);
			}
			this.m_lstExtractItem.Clear();
			if (this.m_lstExtractItemData.Count <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objRightReady, true);
				NKCUtil.SetGameobjectActive(this.m_objRightResult, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRightReady, false);
			NKCUtil.SetGameobjectActive(this.m_objRightResult, true);
			foreach (NKCUISlot.SlotData slotData in this.m_lstExtractItemData)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
				if (itemMiscTempletByID != null)
				{
					if (itemMiscTempletByID.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_PIECE)
					{
						Debug.LogError("NKCUIRearmamentExtract::UpdateExtractResultItemUI() - Can not support imt_piece type");
					}
					else if (this.m_rtTacticsInfo != null)
					{
						NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_rtTacticsInfo);
						if (null != newInstance)
						{
							NKCUISlot.SlotData slotData2 = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_MISC, slotData.ID, (int)slotData.Count, 0);
							if (slotData2 != null)
							{
								newInstance.SetData(slotData2, true, null);
								NKCUtil.SetGameobjectActive(newInstance.gameObject, true);
							}
							this.m_lstExtractItem.Add(newInstance);
						}
					}
				}
			}
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x0018A9A4 File Offset: 0x00188BA4
		private void UpdateSynergyUI()
		{
			this.m_bActiveSynergyBounds = false;
			if (this.m_lstSelectedUnitsUID.Count >= NKMCommonConst.MaxExtractUnitSelect)
			{
				NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
				List<NKM_UNIT_ROLE_TYPE> lstSelectedUnitsRole = new List<NKM_UNIT_ROLE_TYPE>();
				foreach (long unitUid in this.m_lstSelectedUnitsUID)
				{
					NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
					if (unitFromUID != null)
					{
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
						lstSelectedUnitsRole.Add(unitTempletBase.m_NKM_UNIT_ROLE_TYPE);
					}
				}
				NKM_UNIT_ROLE_TYPE firstRole = lstSelectedUnitsRole[0];
				List<NKM_UNIT_ROLE_TYPE> list = lstSelectedUnitsRole.FindAll((NKM_UNIT_ROLE_TYPE x) => x.Equals(firstRole));
				if (list != null)
				{
					this.m_bActiveSynergyBounds = (list.Count >= NKMCommonConst.MaxExtractUnitSelect);
				}
				if (!this.m_bActiveSynergyBounds)
				{
					bool bActiveSynergyBounds = true;
					int iCnt2;
					int iCnt;
					for (iCnt = 0; iCnt < lstSelectedUnitsRole.Count; iCnt = iCnt2)
					{
						if (lstSelectedUnitsRole.Count > iCnt)
						{
							List<NKM_UNIT_ROLE_TYPE> list2 = lstSelectedUnitsRole.FindAll((NKM_UNIT_ROLE_TYPE x) => x.Equals(lstSelectedUnitsRole[iCnt]));
							if (list2 != null && list2.Count > 1)
							{
								bActiveSynergyBounds = false;
								break;
							}
						}
						iCnt2 = iCnt + 1;
					}
					this.m_bActiveSynergyBounds = bActiveSynergyBounds;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objLeftSynergy, this.m_bActiveSynergyBounds);
			foreach (GameObject targetObj in this.m_lstSynergyBonusOff)
			{
				NKCUtil.SetGameobjectActive(targetObj, !this.m_bActiveSynergyBounds);
			}
			foreach (GameObject targetObj2 in this.m_lstSynergyBonusOn)
			{
				NKCUtil.SetGameobjectActive(targetObj2, this.m_bActiveSynergyBounds);
			}
			foreach (GameObject targetObj3 in this.m_lstSynergyBonusOnFx)
			{
				NKCUtil.SetGameobjectActive(targetObj3, false);
				if (this.m_bActiveSynergyBounds)
				{
					NKCUtil.SetGameobjectActive(targetObj3, true);
				}
			}
			if (this.m_bActiveSynergyBounds)
			{
				int synergyIncreasePercentage = NKCRearmamentUtil.GetSynergyIncreasePercentage(this.m_lstSelectedUnitsUID);
				NKCUtil.SetLabelText(this.m_lbSynergyBounsDesc, string.Format(NKCUtilString.GET_STRING_REARM_EXTRACT_CONFIRM_POPUP_SYNERGY_BONUS, synergyIncreasePercentage));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbSynergyBounsDesc, NKCUtilString.GET_STRING_REARM_EXTRACT_NOT_ACTIVE_SYNERGY_BOUNS);
			}
			Color uitextColor = NKCUtil.GetUITextColor(this.m_lstSelectedUnitsUID.Count > 0);
			NKCUtil.ButtonColor type = NKCUtil.ButtonColor.BC_GRAY;
			if (this.m_lstSelectedUnitsUID.Count > 0)
			{
				type = NKCUtil.ButtonColor.BC_YELLOW;
			}
			Sprite buttonSprite = NKCUtil.GetButtonSprite(type);
			NKCUtil.SetLabelTextColor(this.m_lbExtractBtn, uitextColor);
			NKCUtil.SetImageSprite(this.m_imgExtractBtn, buttonSprite, false);
			if (null != this.m_SynergyBonusSlot)
			{
				int itemID = this.m_MiscItemCode;
				if (!this.m_bActiveSynergyBounds)
				{
					itemID = this.m_MiscItemCodeDisable;
				}
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(itemID, 1L, 0);
				if (slotData != null)
				{
					this.m_SynergyBonusSlot.SetData(slotData, true, new NKCUISlot.OnClick(this.OnClickExtractReward));
				}
			}
			this.UpdateSynergyAni(this.m_bActiveSynergyBounds);
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x0018AD24 File Offset: 0x00188F24
		private void OnClickSelectList()
		{
			NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			unitSelectListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			unitSelectListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
			{
				NKCUnitSortSystem.eSortOption.Level_Low,
				NKCUnitSortSystem.eSortOption.Rarity_Low,
				NKCUnitSortSystem.eSortOption.ID_First,
				NKCUnitSortSystem.eSortOption.UID_Last
			};
			unitSelectListOptions.bDescending = false;
			unitSelectListOptions.bShowRemoveSlot = false;
			unitSelectListOptions.iMaxMultipleSelect = NKMCommonConst.MaxExtractUnitSelect;
			unitSelectListOptions.bExcludeLockedUnit = false;
			unitSelectListOptions.bExcludeDeckedUnit = false;
			unitSelectListOptions.m_SortOptions.bUseDeckedState = true;
			unitSelectListOptions.m_SortOptions.bUseLockedState = true;
			unitSelectListOptions.m_SortOptions.bUseDormInState = true;
			unitSelectListOptions.bShowHideDeckedUnitMenu = false;
			unitSelectListOptions.bHideDeckedUnit = false;
			unitSelectListOptions.dOnAutoSelectFilter = null;
			unitSelectListOptions.bUseRemoveSmartAutoSelect = false;
			unitSelectListOptions.setSelectedUnitUID = new HashSet<long>();
			foreach (long item in this.m_lstSelectedUnitsUID)
			{
				unitSelectListOptions.setSelectedUnitUID.Add(item);
			}
			unitSelectListOptions.strEmptyMessage = NKCUtilString.GET_STRING_REARM_EXTRACT_NOT_TARGET_UNIT;
			unitSelectListOptions.bCanSelectUnitInMission = false;
			unitSelectListOptions.m_SortOptions.bIncludeSeizure = false;
			unitSelectListOptions.dOnClose = null;
			unitSelectListOptions.bPushBackUnselectable = false;
			unitSelectListOptions.m_SortOptions.bIgnoreWorldMapLeader = false;
			unitSelectListOptions.setUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
			{
				NKCUnitSortSystem.eFilterCategory.UnitType,
				NKCUnitSortSystem.eFilterCategory.UnitRole,
				NKCUnitSortSystem.eFilterCategory.UnitMoveType,
				NKCUnitSortSystem.eFilterCategory.UnitTargetType,
				NKCUnitSortSystem.eFilterCategory.Rarity,
				NKCUnitSortSystem.eFilterCategory.Cost,
				NKCUnitSortSystem.eFilterCategory.Decked,
				NKCUnitSortSystem.eFilterCategory.Locked
			};
			unitSelectListOptions.setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
			{
				NKCUnitSortSystem.eSortCategory.IDX,
				NKCUnitSortSystem.eSortCategory.Rarity,
				NKCUnitSortSystem.eSortCategory.UnitSummonCost
			};
			unitSelectListOptions.setExcludeUnitUID = new HashSet<long>();
			unitSelectListOptions.setExcludeUnitID = NKCUnitSortSystem.GetDefaultExcludeUnitIDs();
			unitSelectListOptions.bOpenedAtRearmExtract = true;
			unitSelectListOptions.m_bHideUnitCount = true;
			unitSelectListOptions.m_bUseFavorite = true;
			new List<NKMUnitData>();
			foreach (NKMUnitData nkmunitData in NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyUnit.Values)
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(nkmunitData.m_UnitID);
				if (nkmunitTempletBase != null)
				{
					if (nkmunitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
					{
						unitSelectListOptions.setExcludeUnitUID.Add(nkmunitData.m_UnitUID);
					}
					if (nkmunitTempletBase.m_NKM_UNIT_GRADE < NKM_UNIT_GRADE.NUG_SR)
					{
						unitSelectListOptions.setExcludeUnitUID.Add(nkmunitData.m_UnitUID);
					}
				}
			}
			this.UnitSelectList.Open(unitSelectListOptions, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnSelectedUnits), null, null, null, null);
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005151 RID: 20817 RVA: 0x0018AFD8 File Offset: 0x001891D8
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

		// Token: 0x06005152 RID: 20818 RVA: 0x0018AFFA File Offset: 0x001891FA
		private void OnSelectedUnits(List<long> lstUnits)
		{
			if (this.UnitSelectList.IsOpen)
			{
				this.UnitSelectList.Close();
			}
			this.m_lstSelectedUnitsUID = lstUnits;
			this.UpdateUI();
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x0018B024 File Offset: 0x00189224
		private void UpdateSynergyAni(bool bActive)
		{
			if (!bActive)
			{
				NKCSoundManager.StopSound(this.m_iSynergyBoundSoundUID);
				this.m_AniSynergyBouns.SetTrigger("DEACTIVE");
				return;
			}
			this.m_iSynergyBoundSoundUID = NKCSoundManager.PlaySound(this.m_strSynergyBounsSoundName, 1f, 0f, 0f, this.m_bSynergyBoundSoundLoop, 0f, false, 0f);
			Animator aniSynergyBouns = this.m_AniSynergyBouns;
			if (aniSynergyBouns == null)
			{
				return;
			}
			aniSynergyBouns.SetTrigger("ACTIVE");
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0018B098 File Offset: 0x00189298
		private void OnClickExtract()
		{
			if (this.m_lstExtractItemData.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REARM_EXTRACT_LACK_TARGET_UNIT_COUNT, null, "");
				return;
			}
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetCurrentUnitCount() - this.m_lstSelectedUnitsUID.Count <= 8)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REARM_EXTRACT_UNIT_LIMIT_UNDER_8, null, "");
				return;
			}
			NKCUIPopupRearmamentExtractConfirm.Instance.Open(this.m_lstExtractItemData, this.m_lstSelectedUnitsUID, this.m_bActiveSynergyBounds);
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x0018B119 File Offset: 0x00189319
		private void OnClickSynergyInfo()
		{
			NKCUIPopupRearmamentExtractSynergyInfo.Instance.Open();
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0018B125 File Offset: 0x00189325
		private void OnClickExtractReward(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCUIPopupRearmamentExtractSynergyInfo.Instance.Open();
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0018B131 File Offset: 0x00189331
		public RectTransform GetExtractSlotRectTransform(int iExtractSlotIdx)
		{
			if (iExtractSlotIdx >= 0 && this.m_lstExtractUnitSlot.Count > iExtractSlotIdx)
			{
				return this.m_lstExtractUnitSlot[iExtractSlotIdx].GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x040041B0 RID: 16816
		public NKCUIComStateButton[] m_lstSelectBtn;

		// Token: 0x040041B1 RID: 16817
		public List<GameObject> m_lstPiece;

		// Token: 0x040041B2 RID: 16818
		public List<NKCUIRearmamentExtractUnitSlot> m_lstExtractUnitSlot;

		// Token: 0x040041B3 RID: 16819
		public GameObject m_objLeftSynergy;

		// Token: 0x040041B4 RID: 16820
		public GameObject m_objRightResult;

		// Token: 0x040041B5 RID: 16821
		public GameObject m_objRightReady;

		// Token: 0x040041B6 RID: 16822
		[Space]
		public RectTransform m_rtTacticsInfo;

		// Token: 0x040041B7 RID: 16823
		private List<NKCUISlot> m_lstExtractItem = new List<NKCUISlot>();

		// Token: 0x040041B8 RID: 16824
		[Header("추출 버튼")]
		public Text m_lbExtractBtn;

		// Token: 0x040041B9 RID: 16825
		public Image m_imgExtractBtn;

		// Token: 0x040041BA RID: 16826
		public NKCUIComStateButton m_csbtnExtract;

		// Token: 0x040041BB RID: 16827
		[Header("시너지 보너스")]
		public List<GameObject> m_lstSynergyBonusOnFx;

		// Token: 0x040041BC RID: 16828
		public List<GameObject> m_lstSynergyBonusOff;

		// Token: 0x040041BD RID: 16829
		public List<GameObject> m_lstSynergyBonusOn;

		// Token: 0x040041BE RID: 16830
		public NKCUISlot m_SynergyBonusSlot;

		// Token: 0x040041BF RID: 16831
		public NKCUIComStateButton m_csbtnSynergyInfo;

		// Token: 0x040041C0 RID: 16832
		public Text m_lbSynergyBounsDesc;

		// Token: 0x040041C1 RID: 16833
		[Header("애니메이션")]
		public Animator m_AniExtract;

		// Token: 0x040041C2 RID: 16834
		public Animator m_AniSynergyBouns;

		// Token: 0x040041C3 RID: 16835
		[Header("시너지 보너스 아이템 코드(아이콘 세팅 용)")]
		public int m_MiscItemCode = 1053;

		// Token: 0x040041C4 RID: 16836
		public int m_MiscItemCodeDisable = 1054;

		// Token: 0x040041C5 RID: 16837
		[Header("시너지 활성화 사운드")]
		public bool m_bSynergyBoundSoundLoop;

		// Token: 0x040041C6 RID: 16838
		public string m_strSynergyBounsSoundName = "FX_UI_DIVE_START_MOVIE_FRONT";

		// Token: 0x040041C7 RID: 16839
		private List<NKCUISlot.SlotData> m_lstExtractItemData = new List<NKCUISlot.SlotData>();

		// Token: 0x040041C8 RID: 16840
		private bool m_bActiveSynergyBounds;

		// Token: 0x040041C9 RID: 16841
		private NKCUIUnitSelectList m_UIUnitSelectList;

		// Token: 0x040041CA RID: 16842
		private const string ANI_ACTIVE = "ACTIVE";

		// Token: 0x040041CB RID: 16843
		private const string ANI_DE_ACTIVE = "DEACTIVE";

		// Token: 0x040041CC RID: 16844
		private int m_iSynergyBoundSoundUID;

		// Token: 0x040041CD RID: 16845
		private List<long> m_lstSelectedUnitsUID = new List<long>();
	}
}

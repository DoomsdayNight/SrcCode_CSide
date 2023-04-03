using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NKC.UI.Result;
using NKM;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B25 RID: 2853
	public class NKCUIModuleSubUIMerge : NKCUIModuleSubUIBase
	{
		// Token: 0x060081EF RID: 33263 RVA: 0x002BCB44 File Offset: 0x002BAD44
		public override void Init()
		{
			foreach (NKCUISlot nkcuislot in this.m_lstSelectedItems)
			{
				nkcuislot.Init();
				nkcuislot.SetEmpty(new NKCUISlot.OnClick(this.OnClickSlot));
			}
			NKCUtil.SetBindFunction(this.m_csbtnAuto, new UnityAction(this.OnClickAuto));
			NKCUtil.SetBindFunction(this.m_csbtnMerge, new UnityAction(this.OnClickMerge));
		}

		// Token: 0x060081F0 RID: 33264 RVA: 0x002BCBD4 File Offset: 0x002BADD4
		public override void OnOpen(NKMEventCollectionIndexTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			this.m_curMergeTemplet = NKMTempletContainer<NKMEventCollectionMergeTemplet>.Find(templet.CollectionMergeId);
			if (this.m_curMergeTemplet == null)
			{
				return;
			}
			this.m_dicRecipeGroup.Clear();
			foreach (NKMEventCollectionTemplet nkmeventCollectionTemplet in NKMTempletContainer<NKMEventCollectionTemplet>.Values)
			{
				if (this.m_curMergeTemplet.Key == nkmeventCollectionTemplet.CollectionMergeId)
				{
					using (List<NKMEventCollectionDetailTemplet>.Enumerator enumerator2 = nkmeventCollectionTemplet.Details.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							NKMEventCollectionDetailTemplet collectionDataTemplet = enumerator2.Current;
							NKMEventCollectionMergeRecipeTemplet nkmeventCollectionMergeRecipeTemplet = this.m_curMergeTemplet.RecipeTemplets.Find((NKMEventCollectionMergeRecipeTemplet e) => e.MergeRecipeGroupId == collectionDataTemplet.CollectionGradeGroupId);
							if (nkmeventCollectionMergeRecipeTemplet != null)
							{
								if (!this.m_dicRecipeGroup.ContainsKey(collectionDataTemplet.CollectionGradeGroupId))
								{
									this.m_dicRecipeGroup.Add(collectionDataTemplet.CollectionGradeGroupId, new List<NKMEventCollectionDetailTemplet>
									{
										collectionDataTemplet
									});
								}
								else
								{
									this.m_dicRecipeGroup[collectionDataTemplet.CollectionGradeGroupId].Add(collectionDataTemplet);
								}
							}
						}
					}
				}
			}
			if (this.m_dicRecipeGroup.Count <= 0)
			{
				return;
			}
			foreach (NKCUIComToggle nkcuicomToggle in this.m_ctgRecipes)
			{
				nkcuicomToggle.OnValueChanged.RemoveAllListeners();
				NKCUtil.SetGameobjectActive(nkcuicomToggle, false);
			}
			int num = 0;
			using (Dictionary<int, List<NKMEventCollectionDetailTemplet>>.Enumerator enumerator4 = this.m_dicRecipeGroup.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					KeyValuePair<int, List<NKMEventCollectionDetailTemplet>> data = enumerator4.Current;
					if (this.m_ctgRecipes.Count > num)
					{
						NKCUtil.SetGameobjectActive(this.m_ctgRecipes[num], true);
						NKCUtil.SetToggleValueChangedDelegate(this.m_ctgRecipes[num], delegate(bool b)
						{
							this.OnClickTab(b, data.Key);
						});
						num++;
					}
				}
			}
			this.m_ctgRecipes[0].Select(true, true, true);
			this.OnClickTab(true, this.m_dicRecipeGroup.First<KeyValuePair<int, List<NKMEventCollectionDetailTemplet>>>().Key);
			this.UpdateRecipeUI();
			this.ClearSlots();
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x002BCE64 File Offset: 0x002BB064
		public override void Refresh()
		{
			this.ClearSlots();
			this.UpdateRecipeUI();
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x002BCE72 File Offset: 0x002BB072
		public override void UnHide()
		{
			this.UpdateBackFX();
		}

		// Token: 0x060081F3 RID: 33267 RVA: 0x002BCE7C File Offset: 0x002BB07C
		public void OnCompleteMerge(int mergeID, List<NKMUnitData> lstUnit)
		{
			using (IEnumerator<NKMEventCollectionIndexTemplet> enumerator = NKMTempletContainer<NKMEventCollectionIndexTemplet>.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMEventCollectionIndexTemplet collectionIdxTemplet = enumerator.Current;
					if (collectionIdxTemplet.IsOpen && collectionIdxTemplet.CollectionMergeId == mergeID)
					{
						NKCUIModuleResult moduleResult = NKCUIModuleResult.MakeInstance(collectionIdxTemplet.EventMergeResultPrefabID, collectionIdxTemplet.EventMergeResultPrefabID);
						if (moduleResult != null)
						{
							moduleResult.Open(delegate
							{
								NKMRewardData nkmrewardData = new NKMRewardData();
								nkmrewardData.SetUnitData(lstUnit);
								NKCUIModuleResult moduleResult = moduleResult;
								if (moduleResult != null)
								{
									moduleResult.Close();
								}
								NKCUIModuleResult.CheckInstanceAndClose();
								moduleResult = null;
								if (string.IsNullOrEmpty(collectionIdxTemplet.EventResultPrefabID))
								{
									NKCUIResult.Instance.OpenBoxGain(NKCScenManager.CurrentUserData().m_ArmyData, nkmrewardData, NKCUtilString.GET_STRING_CONTRACT_SLOT_UNIT, null, true, 1, false);
									return;
								}
								NKCUIPopupModuleResult moduleResultPopup = NKCUIPopupModuleResult.MakeInstance(collectionIdxTemplet.EventResultPrefabID, collectionIdxTemplet.EventResultPrefabID);
								if (null != moduleResultPopup)
								{
									moduleResultPopup.Init();
									moduleResultPopup.Open(nkmrewardData, delegate()
									{
										if (moduleResultPopup.IsOpen)
										{
											moduleResultPopup.Close();
											NKCUIPopupModuleResult.CheckInstanceAndClose();
											moduleResultPopup = null;
										}
									});
									return;
								}
							});
						}
					}
				}
			}
		}

		// Token: 0x060081F4 RID: 33268 RVA: 0x002BCF44 File Offset: 0x002BB144
		public void OnClickTab(bool bSelect, int MergeInputGradeGroupID)
		{
			if (!this.m_dicRecipeGroup.ContainsKey(MergeInputGradeGroupID))
			{
				return;
			}
			if (this.m_curMergeTemplet == null)
			{
				return;
			}
			NKMEventCollectionMergeRecipeTemplet nkmeventCollectionMergeRecipeTemplet = this.m_curMergeTemplet.RecipeTemplets.Find((NKMEventCollectionMergeRecipeTemplet e) => e.MergeRecipeGroupId == MergeInputGradeGroupID);
			if (nkmeventCollectionMergeRecipeTemplet == null)
			{
				return;
			}
			this.m_iCurMergeInputCnt = nkmeventCollectionMergeRecipeTemplet.MergeInputValue;
			this.m_iCurTargetGradeGroupID = MergeInputGradeGroupID;
			this.ClearSlots();
			this.UpdateBackFX();
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x002BCFC0 File Offset: 0x002BB1C0
		private void UpdateRecipeUI()
		{
			if (this.m_curMergeTemplet == null)
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			int num = 0;
			foreach (NKMEventCollectionMergeRecipeTemplet nkmeventCollectionMergeRecipeTemplet in this.m_curMergeTemplet.RecipeTemplets)
			{
				if (this.m_lstMergeInputCnt.Count > num)
				{
					NKCUtil.SetLabelText(this.m_lstMergeInputCnt[num], string.Format(NKCUtilString.GET_STRING_MODULE_MERGE_INPUT_COUNT, nkmeventCollectionMergeRecipeTemplet.MergeInputValue));
				}
				int num2 = 0;
				if (this.m_lstMergeHaveCnt.Count > num)
				{
					if (!this.m_dicRecipeGroup.ContainsKey(nkmeventCollectionMergeRecipeTemplet.MergeInputGradeGroupId))
					{
						continue;
					}
					foreach (NKMEventCollectionDetailTemplet nkmeventCollectionDetailTemplet in this.m_dicRecipeGroup[nkmeventCollectionMergeRecipeTemplet.MergeInputGradeGroupId])
					{
						List<NKMUnitData> trophyListByUnitID = armyData.GetTrophyListByUnitID(nkmeventCollectionDetailTemplet.Key);
						if (trophyListByUnitID.Count > 0)
						{
							List<NKMUnitData> list = trophyListByUnitID.FindAll((NKMUnitData x) => !x.m_bLock);
							if (list != null)
							{
								num2 += list.Count;
							}
						}
					}
					NKCUtil.SetLabelText(this.m_lstMergeHaveCnt[num], string.Format(NKCUtilString.GET_STRING_MODULE_MERGE_INPUT_UNIT_HAVE_COUNT, num2));
				}
				num++;
			}
		}

		// Token: 0x060081F6 RID: 33270 RVA: 0x002BD164 File Offset: 0x002BB364
		private void UpdateBackFX()
		{
			this.m_BackAni.SetTrigger("CLICK_TAB");
			Color color = Color.white;
			Color color2 = Color.white;
			int num = 0;
			NKCUtil.SetGameobjectActive(this.m_objAwakeFX, false);
			foreach (KeyValuePair<int, List<NKMEventCollectionDetailTemplet>> keyValuePair in this.m_dicRecipeGroup)
			{
				if (keyValuePair.Key == this.m_iCurTargetGradeGroupID)
				{
					switch (num)
					{
					case 1:
						color = NKCUtil.GetColor("#09a9ff");
						color2 = NKCUtil.GetColor("#1D89D4");
						goto IL_119;
					case 2:
						color = NKCUtil.GetColor("#b409ff");
						color2 = NKCUtil.GetColor("#B74A9B");
						goto IL_119;
					case 3:
						color = NKCUtil.GetColor("#FFBA44");
						color2 = NKCUtil.GetColor("#FF6631");
						goto IL_119;
					case 4:
						color = NKCUtil.GetColor("#FFBA44");
						color2 = NKCUtil.GetColor("#FF6631");
						NKCUtil.SetGameobjectActive(this.m_objAwakeFX, true);
						goto IL_119;
					}
					color = NKCUtil.GetColor("#AAB5BC");
					color2 = NKCUtil.GetColor("#718787");
					break;
				}
				num++;
			}
			IL_119:
			foreach (Image image in this.m_lstCommonImg)
			{
				Color endValue = color;
				if (null != this.m_imgAlpha037 && image.name == this.m_imgAlpha037.name)
				{
					endValue.a = 0.39f;
				}
				else if (null != this.m_imgAlpha047 && image.name == this.m_imgAlpha047.name)
				{
					endValue.a = 0.47f;
				}
				image.DOBlendableColor(endValue, this.m_fBlendDelayTime);
			}
			foreach (Image image2 in this.m_lstOtherImg)
			{
				Color endValue2 = color2;
				if (null != this.m_imgAlpha037 && image2.name == this.m_imgAlpha037.name)
				{
					endValue2.a = 0.39f;
				}
				else if (null != this.m_imgAlpha047 && image2.name == this.m_imgAlpha047.name)
				{
					endValue2.a = 0.47f;
				}
				image2.DOBlendableColor(endValue2, this.m_fBlendDelayTime);
			}
		}

		// Token: 0x060081F7 RID: 33271 RVA: 0x002BD418 File Offset: 0x002BB618
		private void ClearSlots()
		{
			this.m_lstSelectedUnit.Clear();
			for (int i = 0; i < this.m_lstSelectedItems.Count; i++)
			{
				if (null == this.m_lstSelectedItems[i])
				{
					return;
				}
				this.m_lstSelectedItems[i].SetEmpty(new NKCUISlot.OnClick(this.OnClickSlot));
				if (i >= this.m_iCurMergeInputCnt)
				{
					this.m_lstSelectedItems[i].TurnOffExtraUI();
					this.m_lstSelectedItems[i].SetDisable(true, "");
					this.m_lstSelectedItems[i].SetBGVisible(false);
					this.m_lstSelectedItems[i].SetAlphaColorDisableImg(this.m_fDisableImgAlphaValue);
				}
				else
				{
					this.m_lstSelectedItems[i].SetBGVisible(true);
				}
			}
		}

		// Token: 0x060081F8 RID: 33272 RVA: 0x002BD4EC File Offset: 0x002BB6EC
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (!this.m_dicRecipeGroup.ContainsKey(this.m_iCurTargetGradeGroupID))
			{
				return;
			}
			NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			unitSelectListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			unitSelectListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
			{
				NKCUnitSortSystem.eSortOption.Rarity_High,
				NKCUnitSortSystem.eSortOption.UID_First
			};
			unitSelectListOptions.bDescending = false;
			unitSelectListOptions.bShowRemoveSlot = false;
			unitSelectListOptions.iMaxMultipleSelect = this.m_iCurMergeInputCnt;
			unitSelectListOptions.bExcludeLockedUnit = false;
			unitSelectListOptions.bExcludeDeckedUnit = false;
			unitSelectListOptions.m_SortOptions.bUseDeckedState = true;
			unitSelectListOptions.m_SortOptions.bUseLockedState = true;
			unitSelectListOptions.m_SortOptions.bUseDormInState = true;
			unitSelectListOptions.setOnlyIncludeUnitID = new HashSet<int>();
			foreach (NKMEventCollectionDetailTemplet nkmeventCollectionDetailTemplet in this.m_dicRecipeGroup[this.m_iCurTargetGradeGroupID])
			{
				unitSelectListOptions.setOnlyIncludeUnitID.Add(nkmeventCollectionDetailTemplet.Key);
			}
			unitSelectListOptions.bShowHideDeckedUnitMenu = false;
			unitSelectListOptions.bEnableLockUnitSystem = false;
			unitSelectListOptions.setSelectedUnitUID = new HashSet<long>();
			foreach (long item in this.m_lstSelectedUnit)
			{
				unitSelectListOptions.setSelectedUnitUID.Add(item);
			}
			unitSelectListOptions.strEmptyMessage = NKCUtilString.GET_STRING_MOUDLE_MERGE_TARGET_EMPTY;
			unitSelectListOptions.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			unitSelectListOptions.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			unitSelectListOptions.bHideDeckedUnit = true;
			unitSelectListOptions.m_bUseFavorite = true;
			unitSelectListOptions.eTargetUnitType = NKCUIUnitSelectList.TargetTabType.Trophy;
			this.UnitSelectList.Open(unitSelectListOptions, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnUnitSelected), null, null, null, null);
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060081F9 RID: 33273 RVA: 0x002BD6BC File Offset: 0x002BB8BC
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

		// Token: 0x060081FA RID: 33274 RVA: 0x002BD6E0 File Offset: 0x002BB8E0
		public void OnUnitSelected(List<long> selectedList)
		{
			if (this.m_UIUnitSelectList != null && this.m_UIUnitSelectList.IsOpen)
			{
				this.m_UIUnitSelectList.Close();
			}
			this.m_lstSelectedUnit = selectedList;
			if (this.m_lstSelectedUnit.Count == 0)
			{
				this.ClearSlots();
				return;
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			for (int i = 0; i < this.m_lstSelectedItems.Count; i++)
			{
				if (i < this.m_lstSelectedUnit.Count)
				{
					NKMUnitData trophyFromUID = armyData.GetTrophyFromUID(this.m_lstSelectedUnit[i]);
					if (trophyFromUID != null)
					{
						NKMUnitManager.GetUnitTempletBase(trophyFromUID);
						this.m_lstSelectedItems[i].SetData(NKCUISlot.SlotData.MakeUnitData(trophyFromUID), true, new NKCUISlot.OnClick(this.OnClickSlot));
					}
				}
				else if (i >= this.m_iCurMergeInputCnt)
				{
					this.m_lstSelectedItems[i].TurnOffExtraUI();
					this.m_lstSelectedItems[i].SetDisable(true, "");
					this.m_lstSelectedItems[i].SetBGVisible(false);
					this.m_lstSelectedItems[i].SetAlphaColorDisableImg(this.m_fDisableImgAlphaValue);
				}
				else
				{
					this.m_lstSelectedItems[i].SetBGVisible(true);
					this.m_lstSelectedItems[i].SetEmpty(new NKCUISlot.OnClick(this.OnClickSlot));
				}
			}
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x002BD838 File Offset: 0x002BBA38
		private void OnClickAuto()
		{
			if (this.m_iCurTargetGradeGroupID == 0)
			{
				return;
			}
			if (!this.m_dicRecipeGroup.ContainsKey(this.m_iCurTargetGradeGroupID))
			{
				return;
			}
			NKCScenManager.CurrentUserData();
			NKCUnitSortSystem.UnitListOptions options = default(NKCUnitSortSystem.UnitListOptions);
			options.bExcludeLockedUnit = true;
			options.bExcludeDeckedUnit = true;
			options.setOnlyIncludeUnitID = new HashSet<int>(from x in this.m_dicRecipeGroup[this.m_iCurTargetGradeGroupID]
			select x.Key);
			options.bIncludeUndeckableUnit = true;
			NKCUnitSortSystem nkcunitSortSystem = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), options, NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyTrophy.Values);
			HashSet<long> hashSet = new HashSet<long>(this.m_lstSelectedUnit);
			List<long> collection = (from x in nkcunitSortSystem.AutoSelect(hashSet, this.m_iCurMergeInputCnt - hashSet.Count, null)
			select x.m_UnitUID).ToList<long>();
			this.m_lstSelectedUnit.AddRange(collection);
			this.OnUnitSelected(this.m_lstSelectedUnit);
		}

		// Token: 0x060081FC RID: 33276 RVA: 0x002BD94C File Offset: 0x002BBB4C
		private void OnClickMerge()
		{
			if (this.m_iCurMergeInputCnt > this.m_lstSelectedUnit.Count)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MOUDLE_MERGE_NO_ENOUGH_COUNT, null, "");
				return;
			}
			NKCPopupMergeConfirmBox.Instance.Open(this.m_curMergeTemplet.Key, this.m_iCurTargetGradeGroupID, this.m_lstSelectedUnit, new Action(this.Send_NKMPacket_EVENT_COLLECTION_MERGE_REQ));
		}

		// Token: 0x060081FD RID: 33277 RVA: 0x002BD9AF File Offset: 0x002BBBAF
		private void Send_NKMPacket_EVENT_COLLECTION_MERGE_REQ()
		{
			if (NKCPopupMergeConfirmBox.IsInstanceOpen)
			{
				NKCPopupMergeConfirmBox.Instance.Close();
			}
			NKCPacketSender.Send_NKMPacket_EVENT_COLLECTION_MERGE_REQ(this.m_curMergeTemplet.Key, this.m_iCurTargetGradeGroupID, this.m_lstSelectedUnit);
		}

		// Token: 0x060081FE RID: 33278 RVA: 0x002BD9DE File Offset: 0x002BBBDE
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				this.OnClickAuto();
			}
		}

		// Token: 0x04006E0C RID: 28172
		public List<NKCUISlot> m_lstSelectedItems;

		// Token: 0x04006E0D RID: 28173
		[Header("��� ���� ��� ���Ժ��� ���� �ʼ�")]
		public List<NKCUIComToggle> m_ctgRecipes;

		// Token: 0x04006E0E RID: 28174
		public NKCUIComStateButton m_csbtnMerge;

		// Token: 0x04006E0F RID: 28175
		public NKCUIComStateButton m_csbtnAuto;

		// Token: 0x04006E10 RID: 28176
		public Animator m_BackAni;

		// Token: 0x04006E11 RID: 28177
		public List<Image> m_lstCommonImg = new List<Image>();

		// Token: 0x04006E12 RID: 28178
		public List<Image> m_lstOtherImg = new List<Image>();

		// Token: 0x04006E13 RID: 28179
		public GameObject m_objAwakeFX;

		// Token: 0x04006E14 RID: 28180
		[Space]
		public Image m_imgAlpha037;

		// Token: 0x04006E15 RID: 28181
		public Image m_imgAlpha047;

		// Token: 0x04006E16 RID: 28182
		[Header("������ ����(�ʿ䰹��)/���ϵ�޺��� ����")]
		public List<Text> m_lstMergeInputCnt = new List<Text>();

		// Token: 0x04006E17 RID: 28183
		[Header("������ ����(��������)/���ϵ�޺��� ����")]
		public List<Text> m_lstMergeHaveCnt = new List<Text>();

		// Token: 0x04006E18 RID: 28184
		[Header("�÷� ��ȯ �ӵ�")]
		public float m_fBlendDelayTime = 0.4f;

		// Token: 0x04006E19 RID: 28185
		[Header("��Ȱ�� ���� ���� ��")]
		public float m_fDisableImgAlphaValue = 0.5f;

		// Token: 0x04006E1A RID: 28186
		private Dictionary<int, List<NKMEventCollectionDetailTemplet>> m_dicRecipeGroup = new Dictionary<int, List<NKMEventCollectionDetailTemplet>>();

		// Token: 0x04006E1B RID: 28187
		private NKMEventCollectionMergeTemplet m_curMergeTemplet;

		// Token: 0x04006E1C RID: 28188
		private int m_iCurTargetGradeGroupID;

		// Token: 0x04006E1D RID: 28189
		private int m_iCurMergeInputCnt;

		// Token: 0x04006E1E RID: 28190
		private const string CLICK_TAB = "CLICK_TAB";

		// Token: 0x04006E1F RID: 28191
		private List<long> m_lstSelectedUnit = new List<long>();

		// Token: 0x04006E20 RID: 28192
		private NKCUIUnitSelectList m_UIUnitSelectList;
	}
}

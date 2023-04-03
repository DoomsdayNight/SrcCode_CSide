using System;
using System.Collections;
using System.Collections.Generic;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A1E RID: 2590
	public class NKCUIOperatorInfoPopupSkill : NKCUIBase
	{
		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x0600714C RID: 29004 RVA: 0x0025A044 File Offset: 0x00258244
		public static NKCUIOperatorInfoPopupSkill Instance
		{
			get
			{
				if (NKCUIOperatorInfoPopupSkill.m_Instance == null)
				{
					NKCUIOperatorInfoPopupSkill.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperatorInfoPopupSkill>("ab_ui_nkm_ui_operator_info", "NKM_UI_OPERATOR_INFO_POPUP_SKILL", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperatorInfoPopupSkill.CleanupInstance)).GetInstance<NKCUIOperatorInfoPopupSkill>();
					NKCUIOperatorInfoPopupSkill.m_Instance.Init();
				}
				return NKCUIOperatorInfoPopupSkill.m_Instance;
			}
		}

		// Token: 0x0600714D RID: 29005 RVA: 0x0025A093 File Offset: 0x00258293
		private static void CleanupInstance()
		{
			NKCUIOperatorInfoPopupSkill.m_Instance = null;
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x0600714E RID: 29006 RVA: 0x0025A09B File Offset: 0x0025829B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOperatorInfoPopupSkill.m_Instance != null && NKCUIOperatorInfoPopupSkill.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x0025A0B6 File Offset: 0x002582B6
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperatorInfoPopupSkill.m_Instance != null && NKCUIOperatorInfoPopupSkill.m_Instance.IsOpen)
			{
				NKCUIOperatorInfoPopupSkill.m_Instance.Close();
			}
		}

		// Token: 0x06007150 RID: 29008 RVA: 0x0025A0DB File Offset: 0x002582DB
		private void OnDestroy()
		{
			NKCUIOperatorInfoPopupSkill.m_Instance = null;
		}

		// Token: 0x06007151 RID: 29009 RVA: 0x0025A0E3 File Offset: 0x002582E3
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			this.Clear();
			NKCUIOperatorInfoPopupSkill.m_Instance = null;
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06007152 RID: 29010 RVA: 0x0025A0F7 File Offset: 0x002582F7
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_OPERATOR_SKILL_TRANSFER;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06007153 RID: 29011 RVA: 0x0025A0FE File Offset: 0x002582FE
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06007154 RID: 29012 RVA: 0x0025A101 File Offset: 0x00258301
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_OPERATOR_COMPOSE";
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06007155 RID: 29013 RVA: 0x0025A108 File Offset: 0x00258308
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x0025A110 File Offset: 0x00258310
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.Cleanup();
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x0025A134 File Offset: 0x00258334
		public override void UnHide()
		{
			base.UnHide();
			this.m_SKILL_ANI.SetTrigger(this.m_aniIdle);
			NKCUtil.SetGameobjectActive(this.m_BaseCardMergeEffect, false);
			NKCUtil.SetGameobjectActive(this.m_ResourceCardMergeEffect, false);
			if (this.m_ResourceOperator != null)
			{
				this.m_SubSkillAni.SetTrigger("idle");
			}
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x0025A188 File Offset: 0x00258388
		public void Init()
		{
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
				this.m_LoopScrollRect.ContentConstraintCount = 3;
				this.m_LoopScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			}
			if (this.m_SortUI != null)
			{
				this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), false);
				this.m_SortUI.RegisterCategories(NKCOperatorSortSystem.MakeDefaultFilterCategory(NKCOperatorSortSystem.FILTER_OPEN_TYPE.NORMAL), NKCPopupSort.MakeDefaultOprSortSet(NKM_UNIT_TYPE.NUT_OPERATOR, false), false);
				if (this.m_SortUI.m_NKCPopupSort != null)
				{
					this.m_SortUI.m_NKCPopupSort.m_bUseDefaultSortAdd = false;
				}
			}
			this.m_sortOptions.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
			this.m_sortOptions.SetBuildOption(true, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.DESCENDING,
				BUILD_OPTIONS.EXCLUDE_LOCKED_UNIT,
				BUILD_OPTIONS.EXCLUDE_DECKED_UNIT
			});
			this.m_sortOptions.lstSortOption = NKCOperatorSortSystem.GetDefaultSortOptions(false, false);
			this.m_sortOptions.eDeckType = NKM_DECK_TYPE.NDT_NORMAL;
			NKCUtil.SetBindFunction(this.m_BUTTON_CANCEL, new UnityAction(this.OnClickMatUnitCancel));
			NKCUtil.SetBindFunction(this.m_MERGE_BUTTON, new UnityAction(this.OnClickUnitTransfer));
			if (this.m_SKILL_IMPLANT_TOGGLE_CHECK != null)
			{
				this.m_SKILL_IMPLANT_TOGGLE_CHECK.OnValueChanged.RemoveAllListeners();
				this.m_SKILL_IMPLANT_TOGGLE_CHECK.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickPassiveSkillTransfer));
			}
			if (this.m_BaseMainSkillBtn != null)
			{
				this.m_BaseMainSkillBtn.PointerDown.RemoveAllListeners();
				this.m_BaseMainSkillBtn.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointDownMainBaseSkill));
			}
			if (this.m_BaseSubSkillBtn != null)
			{
				this.m_BaseSubSkillBtn.PointerDown.RemoveAllListeners();
				this.m_BaseSubSkillBtn.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointDownMainSubSkill));
			}
			if (this.m_ResourceMainSkillBtn != null)
			{
				this.m_ResourceMainSkillBtn.PointerDown.RemoveAllListeners();
				this.m_ResourceMainSkillBtn.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointDownResourceBaseSkill));
			}
			if (this.m_ResourceSubSkillBtn != null)
			{
				this.m_ResourceSubSkillBtn.PointerDown.RemoveAllListeners();
				this.m_ResourceSubSkillBtn.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointDownResourceSubSkill));
			}
		}

		// Token: 0x06007159 RID: 29017 RVA: 0x0025A407 File Offset: 0x00258607
		public void Cleanup()
		{
			this.m_OperatorSortSystem = null;
			NKCUIComUnitSortOptions sortUI = this.m_SortUI;
			if (sortUI == null)
			{
				return;
			}
			sortUI.ResetUI(false);
		}

		// Token: 0x0600715A RID: 29018 RVA: 0x0025A424 File Offset: 0x00258624
		public void Open(NKMOperator operatorData)
		{
			if (operatorData == null)
			{
				return;
			}
			this.m_OperatorSortSystem = null;
			this.m_BaseOperator = operatorData;
			this.m_ResourceOperator = null;
			this.UpdateUI();
			this.UpdateCostItem();
			this.m_bWaitPacketResult = false;
			base.UIOpened(true);
			NKCUtil.SetGameobjectActive(this.m_BaseCardMergeEffect, false);
			NKCUtil.SetGameobjectActive(this.m_ResourceCardMergeEffect, false);
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x0025A47C File Offset: 0x0025867C
		private NKMDeckIndex GetDeckIndex(long unitUID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.m_ArmyData != null)
			{
				return nkmuserData.m_ArmyData.GetOperatorDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, unitUID);
			}
			return NKMDeckIndex.None;
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x0025A4B0 File Offset: 0x002586B0
		private void UpdateLeftUI()
		{
			this.m_bCanTrasferPassiveSkill = false;
			this.UpdateBaseUnitUI();
			if (this.m_ResourceOperator != null)
			{
				this.m_ResourceSlot.SetData(this.m_ResourceOperator, this.GetDeckIndex(this.m_ResourceOperator.uid), true, null);
				this.UpdateSkillUI();
				this.UpdateCostItem();
			}
			if (!this.m_bCanTrasferPassiveSkill)
			{
				this.m_SKILL_IMPLANT_TOGGLE_CHECK.Select(false, true, false);
			}
			NKCUtil.SetGameobjectActive(this.m_SkillPanelNone, this.m_ResourceOperator == null);
			NKCUtil.SetGameobjectActive(this.m_BUTTON_CANCEL, this.m_ResourceOperator != null);
			NKCUtil.SetGameobjectActive(this.m_objResourceSlot, this.m_ResourceOperator != null);
		}

		// Token: 0x0600715D RID: 29021 RVA: 0x0025A558 File Offset: 0x00258758
		private void UpdateBaseUnitUI()
		{
			if (this.m_BaseOperator == null)
			{
				return;
			}
			this.m_BaseSlot.SetData(this.m_BaseOperator, this.GetDeckIndex(this.m_BaseOperator.uid), true, null);
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_BaseOperator.uid);
			if (operatorData == null)
			{
				return;
			}
			this.m_BaseMainSkill.SetData(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, false);
			this.m_BaseSubSkill.SetData(operatorData.subSkill.id, (int)operatorData.subSkill.level, false);
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x0025A5EC File Offset: 0x002587EC
		private void UpdateSkillUI()
		{
			if (this.m_BaseOperator == null && this.m_ResourceOperator == null)
			{
				return;
			}
			this.m_bEnhanceMainSkill = NKCOperatorUtil.IsCanEnhanceMainSkill(this.m_BaseOperator, this.m_ResourceOperator);
			this.m_bEnhanceSubSkill = NKCOperatorUtil.IsCanEnhanceSubSkill(this.m_BaseOperator, this.m_ResourceOperator);
			this.m_MatMainSkill.SetData(this.m_ResourceOperator.mainSkill.id, (int)this.m_ResourceOperator.mainSkill.level, false);
			this.m_MatSubSkill.SetData(this.m_ResourceOperator.subSkill.id, (int)this.m_ResourceOperator.subSkill.level, false);
			if (this.m_bEnhanceMainSkill)
			{
				int enhanceSuccessfulRate = NKCOperatorUtil.GetEnhanceSuccessfulRate(this.m_ResourceOperator);
				NKCUtil.SetLabelText(this.m_lbMainSkillSuccessfulRate, string.Format("{0}%", enhanceSuccessfulRate));
				Color enhanceSuccessfulRateColor = NKCUIOperatorInfoPopupSkill.GetEnhanceSuccessfulRateColor(enhanceSuccessfulRate);
				NKCUtil.SetLabelTextColor(this.m_lbMainSkillSuccessfulRate, enhanceSuccessfulRateColor);
				using (List<Image>.Enumerator enumerator = this.m_lstMainSkillGrow.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Image image = enumerator.Current;
						NKCUtil.SetImageColor(image, enhanceSuccessfulRateColor);
					}
					goto IL_157;
				}
			}
			if (NKCOperatorUtil.IsMaximumSkillLevel(this.m_BaseOperator.mainSkill.id, (int)this.m_BaseOperator.mainSkill.level))
			{
				NKCUtil.SetLabelText(this.m_lbMainSkillDesc, NKCUtilString.GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_MAIN_SKILL_MAX_LEVEL);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbMainSkillDesc, NKCUtilString.GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_MAIN_SKILL_NOT_MATCH);
			}
			IL_157:
			if (this.m_bEnhanceSubSkill)
			{
				int enhanceSuccessfulRate2 = NKCOperatorUtil.GetEnhanceSuccessfulRate(this.m_ResourceOperator);
				NKCUtil.SetLabelText(this.m_lbMainSubSuccessfulRate, string.Format("{0}%", enhanceSuccessfulRate2));
				Color enhanceSuccessfulRateColor2 = NKCUIOperatorInfoPopupSkill.GetEnhanceSuccessfulRateColor(enhanceSuccessfulRate2);
				NKCUtil.SetLabelTextColor(this.m_lbMainSubSuccessfulRate, enhanceSuccessfulRateColor2);
				using (List<Image>.Enumerator enumerator = this.m_lstSubSkillGrow.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Image image2 = enumerator.Current;
						NKCUtil.SetImageColor(image2, enhanceSuccessfulRateColor2);
					}
					goto IL_21D;
				}
			}
			if (NKCOperatorUtil.IsMaximumSkillLevel(this.m_BaseOperator.subSkill.id, (int)this.m_BaseOperator.subSkill.level))
			{
				NKCUtil.SetLabelText(this.m_lbSubSkillDesc, NKCUtilString.GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_SUB_SKILL_MAX_LEVEL);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbSubSkillDesc, NKCUtilString.GET_STRING_OPERATOR_POPUP_SKILL_ENHACE_REJECT_DESC_SUB_SKILL_NOT_MATCH);
			}
			IL_21D:
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_BaseOperator.id);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.m_ResourceOperator.id);
			if (unitTempletBase != null && unitTempletBase2 != null)
			{
				this.m_bCanTrasferPassiveSkill = (this.m_BaseOperator.subSkill.id != this.m_ResourceOperator.subSkill.id && unitTempletBase.m_NKM_UNIT_GRADE <= unitTempletBase2.m_NKM_UNIT_GRADE);
			}
		}

		// Token: 0x0600715F RID: 29023 RVA: 0x0025A898 File Offset: 0x00258A98
		private void UpdateSkillTransferInfo()
		{
			if (!this.m_bCanTrasferPassiveSkill)
			{
				return;
			}
			this.m_BaseSubSkill.SetData(this.m_BaseOperator.subSkill.id, (int)this.m_BaseOperator.subSkill.level, false);
			this.m_MatSubSkill.SetData(this.m_ResourceOperator.subSkill.id, (int)this.m_ResourceOperator.subSkill.level, false);
			int transferSuccessfulRate = NKCOperatorUtil.GetTransferSuccessfulRate(this.m_ResourceOperator);
			NKCUtil.SetLabelText(this.m_lbMainSubSuccessfulRate, string.Format("{0}%", transferSuccessfulRate));
			Color enhanceSuccessfulRateColor = NKCUIOperatorInfoPopupSkill.GetEnhanceSuccessfulRateColor(transferSuccessfulRate);
			NKCUtil.SetLabelTextColor(this.m_lbMainSubSuccessfulRate, enhanceSuccessfulRateColor);
			foreach (Image image in this.m_lstSubSkillGrow)
			{
				NKCUtil.SetImageColor(image, enhanceSuccessfulRateColor);
			}
		}

		// Token: 0x06007160 RID: 29024 RVA: 0x0025A984 File Offset: 0x00258B84
		public static Color GetEnhanceSuccessfulRateColor(int successfulRate)
		{
			if (successfulRate >= 100)
			{
				return NKCUtil.GetColor("#2467F0");
			}
			if (successfulRate >= 70)
			{
				return NKCUtil.GetColor("#4ABA24");
			}
			if (successfulRate >= 30)
			{
				return NKCUtil.GetColor("#FF9800");
			}
			return NKCUtil.GetColor("#D40000");
		}

		// Token: 0x06007161 RID: 29025 RVA: 0x0025A9C0 File Offset: 0x00258BC0
		private void UpdateCostItem()
		{
			this.m_bEnoughCostItem = false;
			this.m_iLackItemCnt = 0;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_BaseOperator.id);
			if (unitTempletBase != null)
			{
				NKMOperatorConstTemplet.HostUnit hostUnit = NKMCommonConst.OperatorConstTemplet.hostUnits.Find((NKMOperatorConstTemplet.HostUnit e) => e.m_NKM_UNIT_GRADE == unitTempletBase.m_NKM_UNIT_GRADE);
				if (hostUnit != null)
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(hostUnit.itemId);
					if (itemMiscTempletByID != null)
					{
						NKCUtil.SetImageSprite(this.m_RESOURCE_ICON, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID), false);
					}
					NKMItemMiscData itemMisc = NKCScenManager.CurrentUserData().m_InventoryData.GetItemMisc(hostUnit.itemId);
					if (itemMisc != null)
					{
						this.m_bEnoughCostItem = (itemMisc.TotalCount >= (long)hostUnit.itemCount);
						this.m_iLackItemID = hostUnit.itemId;
						this.m_iLackItemCnt = hostUnit.itemCount - (int)itemMisc.TotalCount;
					}
					else
					{
						this.m_bEnoughCostItem = false;
						this.m_iLackItemID = hostUnit.itemId;
						this.m_iLackItemCnt = hostUnit.itemCount;
					}
					if (this.m_bEnoughCostItem)
					{
						NKCUtil.SetLabelText(this.m_RESOURCE_NUMBER_TEXT, hostUnit.itemCount.ToString());
						return;
					}
					NKCUtil.SetLabelText(this.m_RESOURCE_NUMBER_TEXT, string.Format("<color=#ff0000ff>{0}</color>", hostUnit.itemCount.ToString()));
					return;
				}
			}
			NKCUtil.SetLabelText(this.m_RESOURCE_NUMBER_TEXT, "0");
		}

		// Token: 0x06007162 RID: 29026 RVA: 0x0025AB08 File Offset: 0x00258D08
		private void UpdateMatUnitList()
		{
			if (this.m_OperatorSortSystem == null)
			{
				this.m_sortOptions.setExcludeOperatorUID = this.GetExclueOperatorUID();
				this.m_OperatorSortSystem = new NKCOperatorSort(NKCScenManager.CurrentUserData(), this.m_sortOptions);
				if (this.m_SortUI != null)
				{
					this.m_SortUI.RegisterOperatorSort(this.m_OperatorSortSystem);
				}
				this.m_LoopScrollRect.TotalCount = this.m_OperatorSortSystem.SortedOperatorList.Count;
				this.m_LoopScrollRect.SetIndexPosition(0);
				this.m_LoopScrollRect.RefreshCells(true);
			}
			if (this.m_SortUI != null)
			{
				this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), false);
				this.m_SortUI.RegisterCategories(NKCOperatorSortSystem.MakeDefaultFilterCategory(NKCOperatorSortSystem.FILTER_OPEN_TYPE.NORMAL), NKCPopupSort.MakeDefaultOprSortSet(NKM_UNIT_TYPE.NUT_OPERATOR, false), false);
				if (this.m_SortUI.m_NKCPopupSort != null)
				{
					this.m_SortUI.m_NKCPopupSort.m_bUseDefaultSortAdd = false;
				}
			}
		}

		// Token: 0x06007163 RID: 29027 RVA: 0x0025ABFC File Offset: 0x00258DFC
		private HashSet<long> GetExclueOperatorUID()
		{
			HashSet<long> hashSet = new HashSet<long>
			{
				this.m_BaseOperator.uid
			};
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_BaseOperator.uid);
			if (operatorData != null)
			{
				Dictionary<long, NKMOperator> dicMyOperator = NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyOperator;
				if (NKCOperatorUtil.IsMaximumSkillLevel(operatorData.mainSkill.id, (int)operatorData.mainSkill.level) && NKCOperatorUtil.IsMaximumSkillLevel(operatorData.subSkill.id, (int)operatorData.subSkill.level))
				{
					foreach (KeyValuePair<long, NKMOperator> keyValuePair in dicMyOperator)
					{
						if (keyValuePair.Value.id == operatorData.id && keyValuePair.Value.subSkill.id == operatorData.subSkill.id)
						{
							hashSet.Add(keyValuePair.Key);
						}
					}
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_BaseOperator.id);
				if (unitTempletBase != null)
				{
					if (NKCOperatorUtil.IsMaximumSkillLevel(operatorData.subSkill.id, (int)operatorData.subSkill.level))
					{
						using (Dictionary<long, NKMOperator>.Enumerator enumerator = dicMyOperator.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<long, NKMOperator> keyValuePair2 = enumerator.Current;
								NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(keyValuePair2.Value.id);
								if (unitTempletBase2 != null && unitTempletBase.m_NKM_UNIT_GRADE > unitTempletBase2.m_NKM_UNIT_GRADE)
								{
									hashSet.Add(keyValuePair2.Key);
								}
							}
							return hashSet;
						}
					}
					foreach (KeyValuePair<long, NKMOperator> keyValuePair3 in dicMyOperator)
					{
						NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(keyValuePair3.Value.id);
						if (unitTempletBase3 != null && unitTempletBase.m_NKM_UNIT_GRADE > unitTempletBase3.m_NKM_UNIT_GRADE && operatorData.subSkill.id != keyValuePair3.Value.subSkill.id)
						{
							hashSet.Add(keyValuePair3.Key);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06007164 RID: 29028 RVA: 0x0025AE30 File Offset: 0x00259030
		private int SortBySameOperator(NKMUnitData lhs, NKMUnitData rhs)
		{
			if (this.m_BaseOperator == null || (this.m_BaseOperator.id != lhs.m_UnitID && this.m_BaseOperator.id != rhs.m_UnitID))
			{
				return 0;
			}
			if (this.m_BaseOperator.id == lhs.m_UnitID)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06007165 RID: 29029 RVA: 0x0025AE84 File Offset: 0x00259084
		private int SortBySameSubSkill(NKMOperator lhs, NKMOperator rhs)
		{
			if (this.m_BaseOperator != null)
			{
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_BaseOperator.uid);
				if (lhs != null && rhs != null && operatorData != null && (operatorData.subSkill.id == lhs.subSkill.id || operatorData.subSkill.id == rhs.subSkill.id))
				{
					if (operatorData.subSkill.id == lhs.subSkill.id)
					{
						return -1;
					}
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x0025AF00 File Offset: 0x00259100
		private void UpdateUI()
		{
			this.UpdateSlotAni();
			this.UpdateLeftUI();
			this.UpdateMatUnitList();
		}

		// Token: 0x06007167 RID: 29031 RVA: 0x0025AF14 File Offset: 0x00259114
		private void OnClickMatUnitCancel()
		{
			if (this.m_ResourceOperator == null)
			{
				return;
			}
			this.m_SubSkillAni.SetTrigger("unselect");
			NKCUtil.SetGameobjectActive(this.m_BUTTON_CANCEL, false);
			NKCUtil.SetGameobjectActive(this.m_ResourceSlot, false);
			this.m_ResourceOperator = null;
			this.UpdateSlotAni();
			this.UpdateLeftUI();
			this.UpdateMatUnitList();
			foreach (NKCUIOperatorDeckSelectSlot nkcuioperatorDeckSelectSlot in this.m_lstVisibleSlot)
			{
				nkcuioperatorDeckSelectSlot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
			}
		}

		// Token: 0x06007168 RID: 29032 RVA: 0x0025AFB0 File Offset: 0x002591B0
		private void OnClickPassiveSkillTransfer(bool bSet)
		{
			if (bSet)
			{
				if (!this.m_bCanTrasferPassiveSkill)
				{
					this.m_SKILL_IMPLANT_TOGGLE_CHECK.Select(false, true, false);
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_OPERATOR_PASSIVE_SKILL_TRANSFER_BLOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				this.UpdateSkillTransferInfo();
			}
			this.UpdateSlotAni();
		}

		// Token: 0x06007169 RID: 29033 RVA: 0x0025AFEC File Offset: 0x002591EC
		private void OnClickUnitTransfer()
		{
			if (this.m_ResourceOperator == null)
			{
				return;
			}
			if (!this.m_bEnoughCostItem)
			{
				NKCShopManager.OpenItemLackPopup(this.m_iLackItemID, this.m_iLackItemCnt);
				return;
			}
			if (NKCOperatorUtil.GetOperatorData(this.m_ResourceOperator.uid).bLock)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OPERATOR_IMPLANT_BLOCK_LOCK_UNIT, null, "");
				return;
			}
			if (!this.m_bEnhanceMainSkill && !this.m_bEnhanceSubSkill)
			{
				if (!this.m_bCanTrasferPassiveSkill)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OPERATOR_IMPLANT_BLOCK_NOT_POSSIBLE, null, "");
					return;
				}
				if (this.m_bCanTrasferPassiveSkill && !this.m_SKILL_IMPLANT_TOGGLE_CHECK.m_bSelect)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OPERATOR_IMPLANT_TRY_NOTHING, null, "");
					return;
				}
			}
			if (this.m_bWaitPacketResult)
			{
				return;
			}
			NKCUIOperatorPopupConfirm.Instance.Open(this.m_ResourceOperator.uid, this.m_BaseOperator.id, new UnityAction(this.OnConfirm));
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x0025B0D7 File Offset: 0x002592D7
		private void OnConfirm()
		{
			this.m_bWaitPacketResult = true;
			NKCPacketSender.Send_NKMPacket_OPERATOR_ENHANCE_REQ(this.m_BaseOperator.uid, this.m_ResourceOperator.uid, this.m_SKILL_IMPLANT_TOGGLE_CHECK.m_bSelect);
		}

		// Token: 0x0600716B RID: 29035 RVA: 0x0025B108 File Offset: 0x00259308
		private void OnPointDownMainBaseSkill(PointerEventData eventData)
		{
			if (this.m_BaseOperator != null)
			{
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_BaseOperator.uid);
				if (operatorData != null)
				{
					NKCUITooltip.Instance.Open(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, new Vector2?(eventData.position));
				}
			}
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x0025B15C File Offset: 0x0025935C
		private void OnPointDownMainSubSkill(PointerEventData eventData)
		{
			if (this.m_BaseOperator != null)
			{
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_BaseOperator.uid);
				if (operatorData != null)
				{
					NKCUITooltip.Instance.Open(operatorData.subSkill.id, (int)operatorData.subSkill.level, new Vector2?(eventData.position));
				}
			}
		}

		// Token: 0x0600716D RID: 29037 RVA: 0x0025B1B0 File Offset: 0x002593B0
		private void OnPointDownResourceBaseSkill(PointerEventData eventData)
		{
			if (this.m_ResourceOperator != null)
			{
				NKCUITooltip.Instance.Open(this.m_ResourceOperator.mainSkill.id, (int)this.m_ResourceOperator.mainSkill.level, new Vector2?(eventData.position));
			}
		}

		// Token: 0x0600716E RID: 29038 RVA: 0x0025B1EF File Offset: 0x002593EF
		private void OnPointDownResourceSubSkill(PointerEventData eventData)
		{
			if (this.m_ResourceOperator != null)
			{
				NKCUITooltip.Instance.Open(this.m_ResourceOperator.subSkill.id, (int)this.m_ResourceOperator.subSkill.level, new Vector2?(eventData.position));
			}
		}

		// Token: 0x0600716F RID: 29039 RVA: 0x0025B230 File Offset: 0x00259430
		private RectTransform GetSlot(int index)
		{
			if (this.m_stkOperatorSlotPool.Count > 0)
			{
				NKCUIOperatorDeckSelectSlot nkcuioperatorDeckSelectSlot = this.m_stkOperatorSlotPool.Pop();
				NKCUtil.SetGameobjectActive(nkcuioperatorDeckSelectSlot, true);
				nkcuioperatorDeckSelectSlot.transform.localScale = Vector3.one;
				this.m_lstVisibleSlot.Add(nkcuioperatorDeckSelectSlot);
				return nkcuioperatorDeckSelectSlot.GetComponent<RectTransform>();
			}
			NKCUIOperatorDeckSelectSlot newInstance = NKCUIOperatorDeckSelectSlot.GetNewInstance(this.m_LoopScrollRect.content);
			newInstance.Init(false);
			NKCUtil.SetGameobjectActive(newInstance, true);
			newInstance.transform.localScale = Vector3.one;
			this.m_lstVisibleSlot.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x0025B2C4 File Offset: 0x002594C4
		private void ReturnSlot(Transform go)
		{
			NKCUIOperatorDeckSelectSlot component = go.GetComponent<NKCUIOperatorDeckSelectSlot>();
			this.m_lstVisibleSlot.Remove(component);
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rectSlotPoolRect);
			this.m_stkOperatorSlotPool.Push(component);
		}

		// Token: 0x06007171 RID: 29041 RVA: 0x0025B304 File Offset: 0x00259504
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (this.m_OperatorSortSystem == null)
			{
				return;
			}
			NKCUIOperatorDeckSelectSlot component = tr.GetComponent<NKCUIOperatorDeckSelectSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_OperatorSortSystem.SortedOperatorList.Count <= idx)
			{
				return;
			}
			NKMOperator curOperatorData = this.m_OperatorSortSystem.SortedOperatorList[idx];
			component.SetData(this.m_BaseOperator, curOperatorData, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnSlotSelected));
		}

		// Token: 0x06007172 RID: 29042 RVA: 0x0025B36C File Offset: 0x0025956C
		private void OnSlotSelected(NKMOperator selectedOperator, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (selectedOperator != null)
			{
				this.m_ResourceOperator = selectedOperator;
				NKCUtil.SetGameobjectActive(this.m_SubSkillAni.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_SubSkillAni.gameObject, true);
				this.m_SubSkillAni.SetTrigger("select");
				this.UpdateSlotAni();
				this.UpdateLeftUI();
				if (this.m_bCanTrasferPassiveSkill && this.m_SKILL_IMPLANT_TOGGLE_CHECK.m_bSelect)
				{
					this.UpdateSkillTransferInfo();
				}
				foreach (NKCUIOperatorDeckSelectSlot nkcuioperatorDeckSelectSlot in this.m_lstVisibleSlot)
				{
					if (nkcuioperatorDeckSelectSlot.OperatorUID == this.m_ResourceOperator.uid)
					{
						nkcuioperatorDeckSelectSlot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
					}
					else
					{
						nkcuioperatorDeckSelectSlot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
					}
				}
			}
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x0025B444 File Offset: 0x00259644
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.m_OperatorSortSystem != null)
			{
				this.m_sortOptions = this.m_OperatorSortSystem.Options;
				this.m_LoopScrollRect.TotalCount = this.m_OperatorSortSystem.SortedOperatorList.Count;
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
					return;
				}
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x06007174 RID: 29044 RVA: 0x0025B4A4 File Offset: 0x002596A4
		private void UpdateSlotAni()
		{
			NKCUtil.SetGameobjectActive(this.m_MainSkillSlotAni.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_MainSkillSlotAni.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_SubSkillSlotAni.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_SubSkillSlotAni.gameObject, true);
			if (this.m_ResourceOperator == null)
			{
				if (this.m_bEnhanceMainSkill)
				{
					this.m_MainSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ENHANCE_OUT.ToString());
					Debug.Log("<color=red>1. 재료 유닛 x, 이전에 강화가 가능했었음 ==> ENHANCE_OUT </color>");
				}
				if (this.m_bEnhanceSubSkill)
				{
					this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ENHANCE_OUT.ToString());
					Debug.Log("<color=red>2. 재료 유닛 x, 이전에 보조 강화가 가능했었음 ==> ENHANCE_OUT </color>");
				}
				else if (this.m_bCanTrasferPassiveSkill)
				{
					this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.SKILL_ON_OUT.ToString());
					Debug.Log("<color=red>3. 재료 유닛 x, 이전에 보조 이식이 가능했었음 ==> SKILL_ON_OUT </color>");
				}
				this.m_MainSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.NONE.ToString());
				this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.NONE.ToString());
				this.m_MainSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), false);
				this.m_SubSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), false);
				return;
			}
			if (this.m_BaseOperator == null || this.m_ResourceOperator == null)
			{
				return;
			}
			if (NKCOperatorUtil.IsCanEnhanceMainSkill(this.m_BaseOperator, this.m_ResourceOperator))
			{
				this.m_MainSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), false);
				this.m_MainSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ENHANCE_LOOP.ToString());
				Debug.Log("<color=red>4. 재료 유닛 o, 주 스킬 강화가 가능 ==> ENHANCE_LOOP </color>");
			}
			else
			{
				if (this.m_bEnhanceMainSkill)
				{
					this.m_MainSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ENHANCE_OUT.ToString());
					Debug.Log("<color=red>5. 재료 유닛 o, 주 스킬 강화가 가능했었음 지금은 아님 ==> ENHANCE_OUT </color>");
				}
				this.m_MainSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), true);
			}
			if (NKCOperatorUtil.IsCanEnhanceSubSkill(this.m_BaseOperator, this.m_ResourceOperator))
			{
				this.m_SubSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), false);
				this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ENHANCE_LOOP.ToString());
				Debug.Log("<color=red>6. 재료 유닛 o, 보조 스킬 강화가 가능 ==> ENHANCE_LOOP </color>");
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_BaseOperator.id);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.m_ResourceOperator.id);
			if (unitTempletBase != null && unitTempletBase2 != null)
			{
				if (this.m_BaseOperator.subSkill.id != this.m_ResourceOperator.subSkill.id && unitTempletBase.m_NKM_UNIT_GRADE <= unitTempletBase2.m_NKM_UNIT_GRADE)
				{
					if (!this.m_SKILL_IMPLANT_TOGGLE_CHECK.m_bSelect)
					{
						if (this.m_bCanTrasferPassiveSkill)
						{
							this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.SKILL_ON_OUT.ToString());
						}
						else
						{
							this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.NONE.ToString());
						}
						this.m_SubSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), true);
						Debug.Log("<color=red>7. 재료 유닛 o, 보조 스킬 이식 가능 상태이지만, 이식 버튼 off ==> SKILL_ON_OUT </color>");
						return;
					}
					this.m_SubSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), false);
					this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.SKILL_ON.ToString());
					Debug.Log("<color=red>8. 재료 유닛 o, 보조 스킬 이식 가능 ==> SKILL_ON </color>");
					return;
				}
				else
				{
					if (this.m_bCanTrasferPassiveSkill)
					{
						this.m_SubSkillSlotAni.SetTrigger(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.SKILL_ON_OUT.ToString());
						Debug.Log("<color=red>9. 재료 유닛 o, 보조 스킬 이식 가능 했었는데 지금은 아님 ==> SKILL_ON_OUT </color>");
					}
					this.m_SubSkillSlotAni.SetBool(NKCUIOperatorInfoPopupSkill.SLOT_ANI_TYPE.ISLOCK.ToString(), true);
				}
			}
		}

		// Token: 0x06007175 RID: 29045 RVA: 0x0025B84C File Offset: 0x00259A4C
		public void OnRecv(bool bTryMainSkill, bool bMainSkillLvUp, bool bTrySubskill, bool bSubskillLvUp, bool bTryImplantSubSkill, bool bImplantSubskill, int oldSubSkillID, int oldSubSkillLv)
		{
			this.m_bWaitPacketResult = false;
			Debug.Log(string.Format("메인강화 시도:{0}, 결과 {1}\n서브강화 시도:{2}, 결과:{3}\n이식시도:{4},결과:{5}", new object[]
			{
				bTryMainSkill,
				bMainSkillLvUp,
				bTrySubskill,
				bSubskillLvUp,
				bTryImplantSubSkill,
				bImplantSubskill
			}));
			this.m_BaseOperator = NKCOperatorUtil.GetOperatorData(this.m_BaseOperator.uid);
			this.m_OperatorSortSystem = null;
			this.m_ResourceOperator = null;
			this.UpdateUI();
			float fWaitTime = 1f;
			NKCUtil.SetGameobjectActive(this.m_BaseCardMergeEffect, false);
			NKCUtil.SetGameobjectActive(this.m_BaseCardMergeEffect, true);
			NKCUtil.SetGameobjectActive(this.m_ResourceCardMergeEffect, false);
			NKCUtil.SetGameobjectActive(this.m_ResourceCardMergeEffect, true);
			base.StartCoroutine(this.OnWaitResultUI(fWaitTime, bTryMainSkill, bMainSkillLvUp, bTrySubskill, bSubskillLvUp, bTryImplantSubSkill, bImplantSubskill, oldSubSkillID, oldSubSkillLv));
		}

		// Token: 0x06007176 RID: 29046 RVA: 0x0025B92C File Offset: 0x00259B2C
		private IEnumerator OnWaitResultUI(float fWaitTime, bool bTryMainSkill, bool bMainSkillLvUp, bool bTrySubskill, bool bSubskillLvUp, bool bTryImplantSubSkill, bool bImplantSubskill, int oldSubSkillID, int oldSubSkillLv)
		{
			yield return new WaitForSeconds(fWaitTime);
			NKCOperatorInfoPopupSkillResult.Instance.Open(this.m_BaseOperator.uid, bTryMainSkill, bMainSkillLvUp, bTrySubskill, bSubskillLvUp, bTryImplantSubSkill, bImplantSubskill, oldSubSkillID, oldSubSkillLv);
			yield break;
		}

		// Token: 0x06007177 RID: 29047 RVA: 0x0025B98C File Offset: 0x00259B8C
		private void Clear()
		{
			foreach (NKCUIOperatorDeckSelectSlot nkcuioperatorDeckSelectSlot in this.m_lstVisibleSlot)
			{
				nkcuioperatorDeckSelectSlot.DestoryInstance();
			}
			this.m_lstVisibleSlot.Clear();
			while (this.m_stkOperatorSlotPool.Count > 0)
			{
				NKCUIOperatorDeckSelectSlot nkcuioperatorDeckSelectSlot2 = this.m_stkOperatorSlotPool.Pop();
				if (nkcuioperatorDeckSelectSlot2 != null)
				{
					nkcuioperatorDeckSelectSlot2.DestoryInstance();
				}
			}
		}

		// Token: 0x04005D28 RID: 23848
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operator_info";

		// Token: 0x04005D29 RID: 23849
		private const string UI_ASSET_NAME = "NKM_UI_OPERATOR_INFO_POPUP_SKILL";

		// Token: 0x04005D2A RID: 23850
		private static NKCUIOperatorInfoPopupSkill m_Instance;

		// Token: 0x04005D2B RID: 23851
		private readonly List<int> RESOURCE_LIST = new List<int>
		{
			1,
			3,
			101
		};

		// Token: 0x04005D2C RID: 23852
		public NKCUIOperatorSelectListSlot m_BaseSlot;

		// Token: 0x04005D2D RID: 23853
		public NKCUIOperatorSelectListSlot m_ResourceSlot;

		// Token: 0x04005D2E RID: 23854
		public GameObject m_objResourceSlot;

		// Token: 0x04005D2F RID: 23855
		public NKCUIComStateButton m_BUTTON_CANCEL;

		// Token: 0x04005D30 RID: 23856
		public NKCUIOperatorSkill m_BaseMainSkill;

		// Token: 0x04005D31 RID: 23857
		public NKCUIOperatorSkill m_BaseSubSkill;

		// Token: 0x04005D32 RID: 23858
		public NKCUIOperatorSkill m_MatMainSkill;

		// Token: 0x04005D33 RID: 23859
		public NKCUIOperatorSkill m_MatSubSkill;

		// Token: 0x04005D34 RID: 23860
		public Text m_lbMainSkillSuccessfulRate;

		// Token: 0x04005D35 RID: 23861
		public Text m_lbMainSubSuccessfulRate;

		// Token: 0x04005D36 RID: 23862
		public GameObject m_SkillPanel;

		// Token: 0x04005D37 RID: 23863
		public GameObject m_SkillPanelNone;

		// Token: 0x04005D38 RID: 23864
		public List<Image> m_lstMainSkillGrow;

		// Token: 0x04005D39 RID: 23865
		public List<Image> m_lstSubSkillGrow;

		// Token: 0x04005D3A RID: 23866
		[Header("button")]
		public NKCUIComToggle m_SKILL_IMPLANT_TOGGLE_CHECK;

		// Token: 0x04005D3B RID: 23867
		public NKCUIComStateButton m_MERGE_BUTTON;

		// Token: 0x04005D3C RID: 23868
		[Header("Resource")]
		public Image m_RESOURCE_ICON;

		// Token: 0x04005D3D RID: 23869
		public Text m_RESOURCE_NUMBER_TEXT;

		// Token: 0x04005D3E RID: 23870
		[Header("Sort & filter")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x04005D3F RID: 23871
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04005D40 RID: 23872
		[Header("animaitor")]
		public Animator m_SKILL_ANI;

		// Token: 0x04005D41 RID: 23873
		public Animator m_SubSkillAni;

		// Token: 0x04005D42 RID: 23874
		public GameObject m_BaseCardMergeEffect;

		// Token: 0x04005D43 RID: 23875
		public GameObject m_ResourceCardMergeEffect;

		// Token: 0x04005D44 RID: 23876
		public Animator m_MainSkillSlotAni;

		// Token: 0x04005D45 RID: 23877
		public Animator m_SubSkillSlotAni;

		// Token: 0x04005D46 RID: 23878
		[Header("block text")]
		public Text m_lbMainSkillDesc;

		// Token: 0x04005D47 RID: 23879
		public Text m_lbSubSkillDesc;

		// Token: 0x04005D48 RID: 23880
		private NKMOperator m_BaseOperator;

		// Token: 0x04005D49 RID: 23881
		private NKMOperator m_ResourceOperator;

		// Token: 0x04005D4A RID: 23882
		private bool m_bEnhanceSubSkill;

		// Token: 0x04005D4B RID: 23883
		private bool m_bEnhanceMainSkill;

		// Token: 0x04005D4C RID: 23884
		private bool m_bCanTrasferPassiveSkill;

		// Token: 0x04005D4D RID: 23885
		private bool m_bEnoughCostItem;

		// Token: 0x04005D4E RID: 23886
		private int m_iLackItemID;

		// Token: 0x04005D4F RID: 23887
		private int m_iLackItemCnt;

		// Token: 0x04005D50 RID: 23888
		[Header("오퍼레이터 스킬 버튼")]
		public NKCUIComStateButton m_BaseMainSkillBtn;

		// Token: 0x04005D51 RID: 23889
		public NKCUIComStateButton m_BaseSubSkillBtn;

		// Token: 0x04005D52 RID: 23890
		public NKCUIComStateButton m_ResourceMainSkillBtn;

		// Token: 0x04005D53 RID: 23891
		public NKCUIComStateButton m_ResourceSubSkillBtn;

		// Token: 0x04005D54 RID: 23892
		[Header("애니메이션 딜레이 갭")]
		public float m_fDelayGap;

		// Token: 0x04005D55 RID: 23893
		private string m_aniIdle = "IDLE";

		// Token: 0x04005D56 RID: 23894
		private bool m_bWaitPacketResult;

		// Token: 0x04005D57 RID: 23895
		private NKCOperatorSortSystem m_OperatorSortSystem;

		// Token: 0x04005D58 RID: 23896
		private Stack<NKCUIOperatorDeckSelectSlot> m_stkOperatorSlotPool = new Stack<NKCUIOperatorDeckSelectSlot>();

		// Token: 0x04005D59 RID: 23897
		private List<NKCUIOperatorDeckSelectSlot> m_lstVisibleSlot = new List<NKCUIOperatorDeckSelectSlot>();

		// Token: 0x04005D5A RID: 23898
		private NKCOperatorSortSystem.OperatorListOptions m_sortOptions;

		// Token: 0x04005D5B RID: 23899
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x0200175F RID: 5983
		private enum SLOT_ANI_TYPE
		{
			// Token: 0x0400A69C RID: 42652
			NONE,
			// Token: 0x0400A69D RID: 42653
			ENHANCE_LOOP,
			// Token: 0x0400A69E RID: 42654
			ENHANCE_OUT,
			// Token: 0x0400A69F RID: 42655
			SKILL_ON,
			// Token: 0x0400A6A0 RID: 42656
			SKILL_ON_OUT,
			// Token: 0x0400A6A1 RID: 42657
			ISLOCK
		}
	}
}

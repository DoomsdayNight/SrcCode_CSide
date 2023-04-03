using System;
using System.Collections.Generic;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BF1 RID: 3057
	public class NKCPopupContractConfirmation : NKCUIBase
	{
		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06008DFA RID: 36346 RVA: 0x00305FE0 File Offset: 0x003041E0
		public static NKCPopupContractConfirmation Instance
		{
			get
			{
				if (NKCPopupContractConfirmation.m_Instance == null)
				{
					NKCPopupContractConfirmation.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupContractConfirmation>("ab_ui_nkm_ui_popup_contract", "NKM_UI_POPUP_CONTRACT_CONFIRMATION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupContractConfirmation.CleanupInstance)).GetInstance<NKCPopupContractConfirmation>();
					NKCPopupContractConfirmation.m_Instance.Init();
				}
				return NKCPopupContractConfirmation.m_Instance;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06008DFB RID: 36347 RVA: 0x0030602F File Offset: 0x0030422F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupContractConfirmation.m_Instance != null && NKCPopupContractConfirmation.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008DFC RID: 36348 RVA: 0x0030604A File Offset: 0x0030424A
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupContractConfirmation.m_Instance != null && NKCPopupContractConfirmation.m_Instance.IsOpen)
			{
				NKCPopupContractConfirmation.m_Instance.Close();
			}
		}

		// Token: 0x06008DFD RID: 36349 RVA: 0x0030606F File Offset: 0x0030426F
		private static void CleanupInstance()
		{
			NKCPopupContractConfirmation.m_Instance = null;
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06008DFE RID: 36350 RVA: 0x00306077 File Offset: 0x00304277
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06008DFF RID: 36351 RVA: 0x0030607A File Offset: 0x0030427A
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_SHOP_SKIN_INFO;
			}
		}

		// Token: 0x06008E00 RID: 36352 RVA: 0x00306084 File Offset: 0x00304284
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			foreach (NKCAssetInstanceData cInstantData in this.m_lstAssetInstanceData)
			{
				NKCAssetResourceManager.CloseInstance(cInstantData);
			}
			this.m_lstAssetInstanceData.Clear();
		}

		// Token: 0x06008E01 RID: 36353 RVA: 0x003060EC File Offset: 0x003042EC
		private void Init()
		{
			NKCUtil.SetBindFunction(this.NKM_UI_POPUP_CLOSEBUTTON, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_BG, new UnityAction(base.Close));
		}

		// Token: 0x06008E02 RID: 36354 RVA: 0x0030611C File Offset: 0x0030431C
		public void Open(int contractID)
		{
			this.Open(ContractTempletV2.Find(contractID));
		}

		// Token: 0x06008E03 RID: 36355 RVA: 0x0030612C File Offset: 0x0030432C
		public void Open(ContractTempletV2 templet)
		{
			if (templet == null)
			{
				return;
			}
			if (templet.m_ContractBonusCountGroupID == 0)
			{
				return;
			}
			if (templet.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				NKCUtil.SetLabelText(this.NKM_UI_POPUP_OK_BOX_TOP_TEXT, string.Format(NKCUtilString.GET_STRING_CONTRACT_CONFIRMATION_POPUP_TITLE_01, templet.GetContractName()));
				NKCUIBase.SetLabelText(this.CONFIRMATION_INFO_TEXT, NKCUtilString.GET_STRING_CONTRACT_CONFIRM_BOTTOM_DESC);
				List<NKMUnitData> list = new List<NKMUnitData>();
				foreach (RandomUnitTempletV2 randomUnitTempletV in templet.UnitPoolTemplet.UnitTemplets)
				{
					if (randomUnitTempletV.PickUpTarget && randomUnitTempletV.UnitTemplet.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
					{
						NKMUnitData nkmunitData = NKCUtil.MakeDummyUnit(randomUnitTempletV.UnitTemplet.m_UnitID, false);
						if (nkmunitData != null)
						{
							list.Add(nkmunitData);
						}
					}
				}
				List<int> curSelectableUnitList = NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetCurSelectableUnitList(templet.Key);
				bool bPickUpUnit = true;
				using (List<NKMUnitData>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						NKMUnitData unitData = enumerator2.Current;
						if (curSelectableUnitList.Count > 0)
						{
							bPickUpUnit = (curSelectableUnitList.Find((int e) => e == unitData.m_UnitID) > 0);
						}
						this.AddUnitSelectListSlot(unitData, bPickUpUnit);
					}
					goto IL_263;
				}
			}
			if (templet.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				NKCUtil.SetLabelText(this.NKM_UI_POPUP_OK_BOX_TOP_TEXT, string.Format(NKCUtilString.GET_STRING_CONTRACT_CONFIRMATION_POPUP_TITLE_01_OPERATOR, templet.GetContractName()));
				NKCUIBase.SetLabelText(this.CONFIRMATION_INFO_TEXT, NKCUtilString.GET_STRING_CONTRACT_CONFIRM_BOTTOM_DESC_OPERATOR);
				List<NKMOperator> list2 = new List<NKMOperator>();
				foreach (RandomUnitTempletV2 randomUnitTempletV2 in templet.UnitPoolTemplet.UnitTemplets)
				{
					if (randomUnitTempletV2.PickUpTarget && randomUnitTempletV2.UnitTemplet.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
					{
						NKMOperator dummyOperator = NKCOperatorUtil.GetDummyOperator(randomUnitTempletV2.UnitTemplet, true);
						if (dummyOperator != null)
						{
							list2.Add(dummyOperator);
						}
					}
				}
				List<int> curSelectableUnitList2 = NKCScenManager.GetScenManager().GetNKCContractDataMgr().GetCurSelectableUnitList(templet.Key);
				bool bPickUpUnit2 = true;
				using (List<NKMOperator>.Enumerator enumerator3 = list2.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						NKMOperator operData = enumerator3.Current;
						if (curSelectableUnitList2.Count > 0)
						{
							bPickUpUnit2 = (curSelectableUnitList2.Find((int e) => e == operData.id) > 0);
						}
						this.AddUnitSelectListSlot(operData, bPickUpUnit2);
					}
					goto IL_263;
				}
			}
			Debug.LogError("채용 설정을 확인해주세요 - Invaild Unit Type");
			return;
			IL_263:
			base.gameObject.SetActive(true);
			base.UIOpened(true);
		}

		// Token: 0x06008E04 RID: 36356 RVA: 0x003063E4 File Offset: 0x003045E4
		private void AddUnitSelectListSlot(NKMUnitData unitData, bool bPickUpUnit)
		{
			if (unitData == null)
			{
				return;
			}
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_unit_slot_card", "NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT", false, null);
			if (nkcassetInstanceData.m_Instant != null)
			{
				NKCUIUnitSelectListSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIUnitSelectListSlot>();
				if (component != null)
				{
					component.Init(true);
					component.transform.SetParent(this.m_Content, false);
					component.SetDataForContractSelection(unitData, false);
					if (!bPickUpUnit)
					{
						component.SetSlotState(NKCUnitSortSystem.eUnitState.CHECKED);
					}
					this.m_lstAssetInstanceData.Add(nkcassetInstanceData);
				}
			}
		}

		// Token: 0x06008E05 RID: 36357 RVA: 0x00306464 File Offset: 0x00304664
		private void AddUnitSelectListSlot(NKMOperator operatorData, bool bPickUpUnit)
		{
			if (operatorData == null)
			{
				return;
			}
			NKCUIOperatorSelectListSlot newInstance = NKCUIOperatorSelectListSlot.GetNewInstance(this.m_Content);
			if (null != newInstance)
			{
				NKCUtil.SetGameobjectActive(newInstance.gameObject, true);
				newInstance.SetDataForContractSelection(operatorData);
				if (!bPickUpUnit)
				{
					newInstance.SetSlotState(NKCUnitSortSystem.eUnitState.CHECKED);
				}
				this.m_lstAssetInstanceData.Add(newInstance.m_Instance);
			}
		}

		// Token: 0x04007B03 RID: 31491
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_contract";

		// Token: 0x04007B04 RID: 31492
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONTRACT_CONFIRMATION";

		// Token: 0x04007B05 RID: 31493
		private static NKCPopupContractConfirmation m_Instance;

		// Token: 0x04007B06 RID: 31494
		[Header("타이틀")]
		public Text NKM_UI_POPUP_OK_BOX_TOP_TEXT;

		// Token: 0x04007B07 RID: 31495
		public NKCUIComStateButton NKM_UI_POPUP_CLOSEBUTTON;

		// Token: 0x04007B08 RID: 31496
		public Text CONFIRMATION_INFO_TEXT;

		// Token: 0x04007B09 RID: 31497
		[Header("닫기")]
		public NKCUIComStateButton m_BG;

		// Token: 0x04007B0A RID: 31498
		private List<NKCAssetInstanceData> m_lstAssetInstanceData = new List<NKCAssetInstanceData>();

		// Token: 0x04007B0B RID: 31499
		public RectTransform m_Content;

		// Token: 0x04007B0C RID: 31500
		public ScrollRect m_ScrollRect;

		// Token: 0x04007B0D RID: 31501
		private const string asset_bundle = "ab_ui_unit_slot_card";

		// Token: 0x04007B0E RID: 31502
		private const string asset_name = "NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT";
	}
}

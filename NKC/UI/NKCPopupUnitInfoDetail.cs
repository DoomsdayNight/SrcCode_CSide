using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A8C RID: 2700
	public class NKCPopupUnitInfoDetail : NKCUIBase
	{
		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06007775 RID: 30581 RVA: 0x0027BB68 File Offset: 0x00279D68
		public static NKCPopupUnitInfoDetail Instance
		{
			get
			{
				if (NKCPopupUnitInfoDetail.m_Instance == null)
				{
					NKCPopupUnitInfoDetail.UnitInfoDetailType unitInfoDetailType = NKCPopupUnitInfoDetail.m_UnitInfoDetailType;
					if (unitInfoDetailType != NKCPopupUnitInfoDetail.UnitInfoDetailType.lab)
					{
						if (unitInfoDetailType == NKCPopupUnitInfoDetail.UnitInfoDetailType.gauntlet)
						{
							NKCPopupUnitInfoDetail.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUnitInfoDetail>("AB_UI_NKM_UI_UNIT_INFO", "NKM_UI_UNIT_INFO_POPUP_OTHER", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUnitInfoDetail.CleanupInstance)).GetInstance<NKCPopupUnitInfoDetail>();
						}
						else
						{
							NKCPopupUnitInfoDetail.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUnitInfoDetail>("AB_UI_NKM_UI_UNIT_INFO", "NKM_UI_UNIT_INFO_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUnitInfoDetail.CleanupInstance)).GetInstance<NKCPopupUnitInfoDetail>();
						}
					}
					else
					{
						NKCPopupUnitInfoDetail.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUnitInfoDetail>("AB_UI_NKM_UI_UNIT_INFO", "NKM_UI_UNIT_INFO_POPUP_LAB", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUnitInfoDetail.CleanupInstance)).GetInstance<NKCPopupUnitInfoDetail>();
					}
					NKCPopupUnitInfoDetail.m_Instance.InitUI();
				}
				return NKCPopupUnitInfoDetail.m_Instance;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06007776 RID: 30582 RVA: 0x0027BC18 File Offset: 0x00279E18
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupUnitInfoDetail.m_Instance != null && NKCPopupUnitInfoDetail.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007777 RID: 30583 RVA: 0x0027BC33 File Offset: 0x00279E33
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupUnitInfoDetail.m_Instance != null && NKCPopupUnitInfoDetail.m_Instance.IsOpen)
			{
				NKCPopupUnitInfoDetail.m_Instance.Close();
			}
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x0027BC58 File Offset: 0x00279E58
		public static void InstanceOpen(NKMUnitData unitData, NKCPopupUnitInfoDetail.UnitInfoDetailType unitInfoDetailType = NKCPopupUnitInfoDetail.UnitInfoDetailType.normal, List<NKMEquipItemData> listNKMEquipItemData = null)
		{
			if (NKCPopupUnitInfoDetail.m_Instance != null && NKCPopupUnitInfoDetail.m_UnitInfoDetailType != unitInfoDetailType)
			{
				NKCPopupUnitInfoDetail.CleanupInstance();
			}
			NKCPopupUnitInfoDetail.m_UnitInfoDetailType = unitInfoDetailType;
			NKCPopupUnitInfoDetail.Instance.Open(unitData, listNKMEquipItemData);
		}

		// Token: 0x06007779 RID: 30585 RVA: 0x0027BC86 File Offset: 0x00279E86
		private static void CleanupInstance()
		{
			NKCPopupUnitInfoDetail.m_Instance.CleanUp();
			UnityEngine.Object.Destroy(NKCPopupUnitInfoDetail.m_Instance);
			NKCPopupUnitInfoDetail.m_Instance = null;
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x0600777A RID: 30586 RVA: 0x0027BCA2 File Offset: 0x00279EA2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x0600777B RID: 30587 RVA: 0x0027BCA5 File Offset: 0x00279EA5
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_UNIT_INFO_DETAIL;
			}
		}

		// Token: 0x0600777C RID: 30588 RVA: 0x0027BCAC File Offset: 0x00279EAC
		private void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_lstStatTypeToShow == null)
			{
				this.m_lstStatTypeToShow = new List<NKM_STAT_TYPE>();
				for (int i = 0; i < 82; i++)
				{
					this.m_lstStatTypeToShow.Add((NKM_STAT_TYPE)i);
				}
			}
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x0027BD2F File Offset: 0x00279F2F
		private void Open(NKMUnitData unitData, List<NKMEquipItemData> listNKMEquipItemData = null)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetData(unitData, listNKMEquipItemData);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x0600777E RID: 30590 RVA: 0x0027BD58 File Offset: 0x00279F58
		public void SetData(NKMUnitData unitData, List<NKMEquipItemData> listNKMEquipItemData = null)
		{
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			if (unitStatTemplet == null)
			{
				return;
			}
			bool flag = false;
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			nkmstatData.MakeBaseStat(null, flag, unitData, unitStatTemplet.m_StatData, false, 0, null);
			if (listNKMEquipItemData == null)
			{
				nkmstatData.MakeBaseBonusFactor(unitData, NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.EquipItems, null, null, flag);
			}
			else
			{
				NKMInventoryData nkminventoryData = new NKMInventoryData();
				nkminventoryData.AddItemEquip(listNKMEquipItemData);
				nkmstatData.MakeBaseBonusFactor(unitData, nkminventoryData.EquipItems, null, null, flag);
			}
			if (this.m_lstNKCUIUnitInfoDetailStatSlot == null)
			{
				this.m_lstNKCUIUnitInfoDetailStatSlot = new List<NKCUIUnitInfoDetailStatSlot>();
				for (int i = 0; i < this.m_lstStatTypeToShow.Count; i++)
				{
					this.m_lstNKCUIUnitInfoDetailStatSlot.Add(NKCUIUnitInfoDetailStatSlot.GetNewInstance(this.m_NKM_UI_UNIT_INFO_POPUP_STAT_LIST_Content.transform));
				}
				this.m_objLine.transform.SetSiblingIndex(6);
			}
			for (int j = 0; j < this.m_lstNKCUIUnitInfoDetailStatSlot.Count; j++)
			{
				bool flag2 = nkmstatData.GetStatBase(this.m_lstStatTypeToShow[j]) + nkmstatData.GetBaseBonusStat(this.m_lstStatTypeToShow[j]) != 0f;
				NKCUtil.SetGameobjectActive(this.m_lstNKCUIUnitInfoDetailStatSlot[j], flag2);
				if (flag2)
				{
					this.m_lstNKCUIUnitInfoDetailStatSlot[j].SetData(this.m_lstStatTypeToShow[j], nkmstatData);
				}
			}
		}

		// Token: 0x0600777F RID: 30591 RVA: 0x0027BEB3 File Offset: 0x0027A0B3
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x0027BEC1 File Offset: 0x0027A0C1
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007781 RID: 30593 RVA: 0x0027BED8 File Offset: 0x0027A0D8
		public void CleanUp()
		{
			foreach (NKCUIUnitInfoDetailStatSlot nkcuiunitInfoDetailStatSlot in NKCPopupUnitInfoDetail.m_Instance.m_lstNKCUIUnitInfoDetailStatSlot)
			{
				nkcuiunitInfoDetailStatSlot.Clear();
			}
		}

		// Token: 0x0400640A RID: 25610
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_INFO";

		// Token: 0x0400640B RID: 25611
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_INFO_POPUP";

		// Token: 0x0400640C RID: 25612
		private const string UI_ASSET_NAME_LAB = "NKM_UI_UNIT_INFO_POPUP_LAB";

		// Token: 0x0400640D RID: 25613
		private const string UI_ASSET_NAME_OTHER = "NKM_UI_UNIT_INFO_POPUP_OTHER";

		// Token: 0x0400640E RID: 25614
		private static NKCPopupUnitInfoDetail.UnitInfoDetailType m_UnitInfoDetailType;

		// Token: 0x0400640F RID: 25615
		private static NKCPopupUnitInfoDetail m_Instance;

		// Token: 0x04006410 RID: 25616
		public GameObject m_NKM_UI_UNIT_INFO_POPUP_STAT_LIST_Content;

		// Token: 0x04006411 RID: 25617
		public GameObject m_objLine;

		// Token: 0x04006412 RID: 25618
		public NKCUIComButton m_btnClose;

		// Token: 0x04006413 RID: 25619
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006414 RID: 25620
		private List<NKCUIUnitInfoDetailStatSlot> m_lstNKCUIUnitInfoDetailStatSlot;

		// Token: 0x04006415 RID: 25621
		private List<NKM_STAT_TYPE> m_lstStatTypeToShow;

		// Token: 0x020017E8 RID: 6120
		public enum UnitInfoDetailType
		{
			// Token: 0x0400A7B5 RID: 42933
			normal,
			// Token: 0x0400A7B6 RID: 42934
			lab,
			// Token: 0x0400A7B7 RID: 42935
			gauntlet
		}

		// Token: 0x020017E9 RID: 6121
		// (Invoke) Token: 0x0600B48E RID: 46222
		public delegate void OnClickOK(NKCUISlot slot);
	}
}

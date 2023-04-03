using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B5E RID: 2910
	public class NKCPopupGauntletBanList : NKCUIBase
	{
		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x060084A5 RID: 33957 RVA: 0x002CC124 File Offset: 0x002CA324
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x060084A6 RID: 33958 RVA: 0x002CC127 File Offset: 0x002CA327
		public override string MenuName
		{
			get
			{
				return "PopupGauntletBanList";
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x060084A7 RID: 33959 RVA: 0x002CC130 File Offset: 0x002CA330
		public static NKCPopupGauntletBanList Instance
		{
			get
			{
				if (NKCPopupGauntletBanList.m_Instance == null)
				{
					NKCPopupGauntletBanList.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGauntletBanList>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_BANNED_LIST", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGauntletBanList.CleanupInstance)).GetInstance<NKCPopupGauntletBanList>();
					NKCPopupGauntletBanList.m_Instance.InitUI();
				}
				return NKCPopupGauntletBanList.m_Instance;
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x060084A8 RID: 33960 RVA: 0x002CC17F File Offset: 0x002CA37F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGauntletBanList.m_Instance != null && NKCPopupGauntletBanList.m_Instance.IsOpen;
			}
		}

		// Token: 0x060084A9 RID: 33961 RVA: 0x002CC19A File Offset: 0x002CA39A
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupGauntletBanList.m_Instance != null && NKCPopupGauntletBanList.m_Instance.IsOpen)
			{
				NKCPopupGauntletBanList.m_Instance.Close();
			}
		}

		// Token: 0x060084AA RID: 33962 RVA: 0x002CC1BF File Offset: 0x002CA3BF
		private static void CleanupInstance()
		{
			NKCPopupGauntletBanList.m_Instance = null;
		}

		// Token: 0x060084AB RID: 33963 RVA: 0x002CC1C8 File Offset: 0x002CA3C8
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_etBG.triggers.Clear();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(base.Close));
			this.m_ctglUnit.OnValueChanged.RemoveAllListeners();
			this.m_ctglUnit.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedUnit));
			this.m_ctglShip.OnValueChanged.RemoveAllListeners();
			this.m_ctglShip.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedShip));
			this.m_ctglUnitUp.OnValueChanged.RemoveAllListeners();
			this.m_ctglUnitUp.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedUnitUp));
			this.m_lvsrUnit.dOnGetObject += this.GetUnitSlot;
			this.m_lvsrUnit.dOnReturnObject += this.ReturnUnitSlot;
			this.m_lvsrUnit.dOnProvideData += this.ProvideUnitSlotData;
			NKCUtil.SetScrollHotKey(this.m_lvsrUnit, null);
			this.m_lvsrShip.dOnGetObject += this.GetShipSlot;
			this.m_lvsrShip.dOnReturnObject += this.ReturnShipSlot;
			this.m_lvsrShip.dOnProvideData += this.ProvideShipSlotData;
			NKCUtil.SetScrollHotKey(this.m_lvsrShip, null);
			this.m_lvsrUnitUp.dOnGetObject += this.GetUnitSlot;
			this.m_lvsrUnitUp.dOnReturnObject += this.ReturnUnitSlot;
			this.m_lvsrUnitUp.dOnProvideData += this.ProvideUnitSlotDataUp;
			NKCUtil.SetScrollHotKey(this.m_lvsrUnitUp, null);
			NKCUtil.SetBindFunction(this.m_csbtnGuide, new UnityAction(this.OnClickGuide));
		}

		// Token: 0x060084AC RID: 33964 RVA: 0x002CC3CB File Offset: 0x002CA5CB
		private void OnClickGuide()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_PVP_BANLIST", 0);
		}

		// Token: 0x060084AD RID: 33965 RVA: 0x002CC3DD File Offset: 0x002CA5DD
		private RectTransform GetUnitSlot(int index)
		{
			NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = UnityEngine.Object.Instantiate<NKCUIUnitSelectListSlot>(this.m_pfbUnitSlotForBan);
			nkcuiunitSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlot, true);
			nkcuiunitSelectListSlot.transform.localScale = Vector3.one;
			return nkcuiunitSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060084AE RID: 33966 RVA: 0x002CC40D File Offset: 0x002CA60D
		private void ReturnUnitSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060084AF RID: 33967 RVA: 0x002CC428 File Offset: 0x002CA628
		private void ProvideUnitSlotData(Transform tr, int idx)
		{
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 || idx >= this.m_lstNKMBanData.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_lstNKMBanData[idx].m_UnitID);
			component.SetEnableShowBan(true);
			component.SetDataForBan(unitTempletBase, true, null, false, false);
			component.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
		}

		// Token: 0x060084B0 RID: 33968 RVA: 0x002CC498 File Offset: 0x002CA698
		private void ProvideUnitSlotDataUp(Transform tr, int idx)
		{
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 || idx >= this.m_lstNKMUnitUpData.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_lstNKMUnitUpData[idx].unitId);
			component.SetEnableShowBan(true);
			component.SetDataForBan(unitTempletBase, true, null, true, false);
			component.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
		}

		// Token: 0x060084B1 RID: 33969 RVA: 0x002CC507 File Offset: 0x002CA707
		private RectTransform GetShipSlot(int index)
		{
			NKCUIShipSelectListSlot nkcuishipSelectListSlot = UnityEngine.Object.Instantiate<NKCUIShipSelectListSlot>(this.m_pfbShipSlotForBan);
			nkcuishipSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuishipSelectListSlot, true);
			nkcuishipSelectListSlot.transform.localScale = Vector3.one;
			return nkcuishipSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060084B2 RID: 33970 RVA: 0x002CC537 File Offset: 0x002CA737
		private void ReturnShipSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060084B3 RID: 33971 RVA: 0x002CC550 File Offset: 0x002CA750
		private void ProvideShipSlotData(Transform tr, int idx)
		{
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 || idx >= this.m_lstNKMBanShipData.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			NKMUnitTempletBase nkmunitTempletBase = null;
			int shipGroupID = this.m_lstNKMBanShipData[idx].m_ShipGroupID;
			if (!this.m_dicUTB_ByShipGroupID.TryGetValue(shipGroupID, out nkmunitTempletBase))
			{
				nkmunitTempletBase = NKMUnitManager.GetUnitTempletBaseByShipGroupID(shipGroupID);
				this.m_dicUTB_ByShipGroupID.Add(shipGroupID, nkmunitTempletBase);
			}
			component.SetEnableShowBan(true);
			component.SetDataForBan(nkmunitTempletBase, true, null, false, false);
		}

		// Token: 0x060084B4 RID: 33972 RVA: 0x002CC5DC File Offset: 0x002CA7DC
		private void OnValueChangedUnit(bool bSet)
		{
			if (!bSet)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitList, true);
			NKCUtil.SetGameobjectActive(this.m_objShipList, false);
			NKCUtil.SetGameobjectActive(this.m_objUnitListUp, false);
			this.m_lvsrUnit.RefreshCells(false);
			NKCUtil.SetLabelText(this.m_lbSubTitle, NKCStringTable.GetString("SI_DP_GAUNTLET_BAN_POPUP_DESC_UNIT", false));
		}

		// Token: 0x060084B5 RID: 33973 RVA: 0x002CC634 File Offset: 0x002CA834
		private void OnValueChangedShip(bool bSet)
		{
			if (!bSet)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitList, false);
			NKCUtil.SetGameobjectActive(this.m_objShipList, true);
			NKCUtil.SetGameobjectActive(this.m_objUnitListUp, false);
			this.m_lvsrShip.RefreshCells(false);
			NKCUtil.SetLabelText(this.m_lbSubTitle, NKCStringTable.GetString("SI_DP_GAUNTLET_BAN_POPUP_DESC_SHIP", false));
		}

		// Token: 0x060084B6 RID: 33974 RVA: 0x002CC68C File Offset: 0x002CA88C
		private void OnValueChangedUnitUp(bool bSet)
		{
			if (!bSet)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitList, false);
			NKCUtil.SetGameobjectActive(this.m_objShipList, false);
			NKCUtil.SetGameobjectActive(this.m_objUnitListUp, true);
			this.m_lvsrUnitUp.RefreshCells(false);
			NKCUtil.SetLabelText(this.m_lbSubTitle, NKCStringTable.GetString("SI_DP_GAUNTLET_UP_POPUP_DESC_UNIT", false));
		}

		// Token: 0x060084B7 RID: 33975 RVA: 0x002CC6E3 File Offset: 0x002CA8E3
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060084B8 RID: 33976 RVA: 0x002CC6F1 File Offset: 0x002CA8F1
		public void Open()
		{
			base.UIOpened(true);
			this.SetUIWhenOpen();
		}

		// Token: 0x060084B9 RID: 33977 RVA: 0x002CC700 File Offset: 0x002CA900
		private void ResetData()
		{
			this.m_lstNKMBanData = new List<NKMBanData>(NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.ROTATION).Values);
			this.m_lstNKMBanData.Sort(new NKCPopupGauntletBanList.CompNKMBanData());
			this.m_lvsrUnit.TotalCount = this.m_lstNKMBanData.Count;
			this.m_lstNKMBanShipData = new List<NKMBanShipData>(NKCBanManager.GetBanDataShip(NKCBanManager.BAN_DATA_TYPE.ROTATION).Values);
			this.m_lstNKMBanShipData.Sort(new NKCPopupGauntletBanList.CompNKMBanShipData());
			this.m_lvsrShip.TotalCount = this.m_lstNKMBanShipData.Count;
			this.m_lstNKMUnitUpData = new List<NKMUnitUpData>(NKCBanManager.m_dicNKMUpData.Values);
			this.m_lstNKMUnitUpData.Sort(new NKCPopupGauntletBanList.CompNKMUnitUpData());
			this.m_lvsrUnitUp.TotalCount = this.m_lstNKMUnitUpData.Count;
			this.m_dicUTB_ByShipGroupID.Clear();
		}

		// Token: 0x060084BA RID: 33978 RVA: 0x002CC7CB File Offset: 0x002CA9CB
		public void OnChangedBanList()
		{
			this.ResetData();
			this.m_lvsrUnit.SetIndexPosition(0);
			this.m_lvsrShip.SetIndexPosition(0);
			this.m_lvsrUnitUp.SetIndexPosition(0);
		}

		// Token: 0x060084BB RID: 33979 RVA: 0x002CC7F8 File Offset: 0x002CA9F8
		public void SetUIWhenOpen()
		{
			if (this.m_bFirstOpen)
			{
				NKCUtil.SetGameobjectActive(this.m_objUnitList, true);
				NKCUtil.SetGameobjectActive(this.m_objShipList, true);
				NKCUtil.SetGameobjectActive(this.m_objUnitListUp, true);
				this.m_lvsrUnit.PrepareCells(0);
				this.m_lvsrShip.PrepareCells(0);
				this.m_lvsrUnitUp.PrepareCells(0);
				this.ResetData();
				this.m_bFirstOpen = false;
			}
			this.m_lvsrUnit.SetIndexPosition(0);
			this.m_lvsrShip.SetIndexPosition(0);
			this.m_lvsrUnitUp.SetIndexPosition(0);
			this.m_ctglUnit.Select(false, true, false);
			this.m_ctglUnit.Select(true, false, false);
		}

		// Token: 0x040070EA RID: 28906
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040070EB RID: 28907
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_POPUP_BANNED_LIST";

		// Token: 0x040070EC RID: 28908
		private static NKCPopupGauntletBanList m_Instance;

		// Token: 0x040070ED RID: 28909
		[Header("공통")]
		public EventTrigger m_etBG;

		// Token: 0x040070EE RID: 28910
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040070EF RID: 28911
		[Header("상단")]
		public NKCUIComStateButton m_csbtnGuide;

		// Token: 0x040070F0 RID: 28912
		public Text m_lbSubTitle;

		// Token: 0x040070F1 RID: 28913
		[Header("왼쪽")]
		public NKCUIComToggle m_ctglUnit;

		// Token: 0x040070F2 RID: 28914
		public NKCUIComToggle m_ctglShip;

		// Token: 0x040070F3 RID: 28915
		public NKCUIComToggle m_ctglUnitUp;

		// Token: 0x040070F4 RID: 28916
		[Header("오른쪽")]
		public LoopVerticalScrollRect m_lvsrUnit;

		// Token: 0x040070F5 RID: 28917
		public LoopVerticalScrollRect m_lvsrShip;

		// Token: 0x040070F6 RID: 28918
		public LoopVerticalScrollRect m_lvsrUnitUp;

		// Token: 0x040070F7 RID: 28919
		public GameObject m_objUnitList;

		// Token: 0x040070F8 RID: 28920
		public GameObject m_objShipList;

		// Token: 0x040070F9 RID: 28921
		public GameObject m_objUnitListUp;

		// Token: 0x040070FA RID: 28922
		public NKCUIUnitSelectListSlot m_pfbUnitSlotForBan;

		// Token: 0x040070FB RID: 28923
		public NKCUIShipSelectListSlot m_pfbShipSlotForBan;

		// Token: 0x040070FC RID: 28924
		private List<NKMBanData> m_lstNKMBanData = new List<NKMBanData>();

		// Token: 0x040070FD RID: 28925
		private List<NKMBanShipData> m_lstNKMBanShipData = new List<NKMBanShipData>();

		// Token: 0x040070FE RID: 28926
		private List<NKMUnitUpData> m_lstNKMUnitUpData = new List<NKMUnitUpData>();

		// Token: 0x040070FF RID: 28927
		private Dictionary<int, NKMUnitTempletBase> m_dicUTB_ByShipGroupID = new Dictionary<int, NKMUnitTempletBase>();

		// Token: 0x04007100 RID: 28928
		private bool m_bFirstOpen = true;

		// Token: 0x020018F8 RID: 6392
		public class CompNKMUnitUpData : IComparer<NKMUnitUpData>
		{
			// Token: 0x0600B74D RID: 46925 RVA: 0x003678BA File Offset: 0x00365ABA
			public int Compare(NKMUnitUpData x, NKMUnitUpData y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (y.upLevel > x.upLevel)
				{
					return 1;
				}
				if (y.upLevel < x.upLevel)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x020018F9 RID: 6393
		public class CompNKMBanData : IComparer<NKMBanData>
		{
			// Token: 0x0600B74F RID: 46927 RVA: 0x003678EF File Offset: 0x00365AEF
			public int Compare(NKMBanData x, NKMBanData y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (y.m_BanLevel > x.m_BanLevel)
				{
					return 1;
				}
				if (y.m_BanLevel < x.m_BanLevel)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x020018FA RID: 6394
		public class CompNKMBanShipData : IComparer<NKMBanShipData>
		{
			// Token: 0x0600B751 RID: 46929 RVA: 0x00367924 File Offset: 0x00365B24
			public int Compare(NKMBanShipData x, NKMBanShipData y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (y.m_BanLevel > x.m_BanLevel)
				{
					return 1;
				}
				if (y.m_BanLevel < x.m_BanLevel)
				{
					return -1;
				}
				return 0;
			}
		}
	}
}

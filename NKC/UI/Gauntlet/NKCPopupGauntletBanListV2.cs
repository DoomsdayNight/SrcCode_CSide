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
	// Token: 0x02000B5F RID: 2911
	public class NKCPopupGauntletBanListV2 : NKCUIBase
	{
		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x060084BE RID: 33982 RVA: 0x002CC8E7 File Offset: 0x002CAAE7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x060084BF RID: 33983 RVA: 0x002CC8EA File Offset: 0x002CAAEA
		public override string MenuName
		{
			get
			{
				return "PopupGauntletBanList";
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x060084C0 RID: 33984 RVA: 0x002CC8F4 File Offset: 0x002CAAF4
		public static NKCPopupGauntletBanListV2 Instance
		{
			get
			{
				if (NKCPopupGauntletBanListV2.m_Instance == null)
				{
					NKCPopupGauntletBanListV2.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGauntletBanListV2>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_BANNED_LIST_NEW", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGauntletBanListV2.CleanupInstance)).GetInstance<NKCPopupGauntletBanListV2>();
					NKCPopupGauntletBanListV2.m_Instance.InitUI();
				}
				return NKCPopupGauntletBanListV2.m_Instance;
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x060084C1 RID: 33985 RVA: 0x002CC943 File Offset: 0x002CAB43
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGauntletBanListV2.m_Instance != null && NKCPopupGauntletBanListV2.m_Instance.IsOpen;
			}
		}

		// Token: 0x060084C2 RID: 33986 RVA: 0x002CC95E File Offset: 0x002CAB5E
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupGauntletBanListV2.m_Instance != null && NKCPopupGauntletBanListV2.m_Instance.IsOpen)
			{
				NKCPopupGauntletBanListV2.m_Instance.Close();
			}
		}

		// Token: 0x060084C3 RID: 33987 RVA: 0x002CC983 File Offset: 0x002CAB83
		private static void CleanupInstance()
		{
			NKCPopupGauntletBanListV2.m_Instance = null;
		}

		// Token: 0x060084C4 RID: 33988 RVA: 0x002CC98C File Offset: 0x002CAB8C
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetEventTriggerDelegate(this.m_etBG, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(base.Close));
			this.m_lvsrUnit.dOnGetObject += this.GetUnitSlot;
			this.m_lvsrUnit.dOnReturnObject += this.ReturnUnitSlot;
			this.m_lvsrUnit.dOnProvideData += this.ProvideUnitSlotData;
			NKCUtil.SetScrollHotKey(this.m_lvsrUnit, null);
			this.m_lvsrShip.dOnGetObject += this.GetShipSlot;
			this.m_lvsrShip.dOnReturnObject += this.ReturnShipSlot;
			this.m_lvsrShip.dOnProvideData += this.ProvideShipSlotData;
			NKCUtil.SetScrollHotKey(this.m_lvsrShip, null);
			this.m_lvsrOper.dOnGetObject += this.GetOperSlot;
			this.m_lvsrOper.dOnReturnObject += this.ReturnOperSlot;
			this.m_lvsrOper.dOnProvideData += this.ProvideOperSlotData;
			NKCUtil.SetScrollHotKey(this.m_lvsrOper, null);
			this.m_lvsrUnit.PrepareCells(0);
			this.m_lvsrShip.PrepareCells(0);
			this.m_lvsrOper.PrepareCells(0);
			NKCUtil.SetBindFunction(this.m_csbtnGuide, new UnityAction(this.OnClickGuide));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglFinalBan, delegate(bool bSet)
			{
				this.OnClickTab(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_FINAL, false);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglUpUnit, delegate(bool bSet)
			{
				this.OnClickTab(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_UP, false);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglCastingBan, delegate(bool bSet)
			{
				this.OnClickTab(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING, false);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglRotationBan, delegate(bool bSet)
			{
				this.OnClickTab(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_ROTATION, false);
			});
			this.InitLeftMenu();
		}

		// Token: 0x060084C5 RID: 33989 RVA: 0x002CCB63 File Offset: 0x002CAD63
		private void OnClickGuide()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_PVP_BANLIST", 0);
		}

		// Token: 0x060084C6 RID: 33990 RVA: 0x002CCB75 File Offset: 0x002CAD75
		private RectTransform GetUnitSlot(int index)
		{
			NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = UnityEngine.Object.Instantiate<NKCUIUnitSelectListSlot>(this.m_pfbUnitSlotForBan);
			nkcuiunitSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlot, true);
			nkcuiunitSelectListSlot.transform.localScale = Vector3.one;
			return nkcuiunitSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060084C7 RID: 33991 RVA: 0x002CCBA5 File Offset: 0x002CADA5
		private void ReturnUnitSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060084C8 RID: 33992 RVA: 0x002CCBC0 File Offset: 0x002CADC0
		private void ProvideUnitSlotData(Transform tr, int idx)
		{
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 && ((this.m_curTabType == NKCPopupGauntletBanListV2.TAB_TYPE.TAB_UP && idx >= this.m_lstNKMUnitUpData.Count) || idx >= this.m_lstNKMUnitUpData.Count))
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			int unitID = (this.m_curTabType == NKCPopupGauntletBanListV2.TAB_TYPE.TAB_UP) ? this.m_lstNKMUnitUpData[idx].unitId : this.m_lstNKMBanData[idx].m_UnitID;
			NKCUtil.SetGameobjectActive(component, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			component.SetEnableShowBan(true);
			switch (this.m_curTabType)
			{
			case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_FINAL:
				component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.FINAL);
				goto IL_B9;
			case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING:
				component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.CASTING);
				goto IL_B9;
			}
			component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.ROTATION);
			IL_B9:
			component.SetDataForBan(unitTempletBase, true, null, this.m_curTabType == NKCPopupGauntletBanListV2.TAB_TYPE.TAB_UP, false);
			component.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
		}

		// Token: 0x060084C9 RID: 33993 RVA: 0x002CCCA0 File Offset: 0x002CAEA0
		private RectTransform GetShipSlot(int index)
		{
			NKCUIShipSelectListSlot nkcuishipSelectListSlot = UnityEngine.Object.Instantiate<NKCUIShipSelectListSlot>(this.m_pfbShipSlotForBan);
			nkcuishipSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuishipSelectListSlot, true);
			nkcuishipSelectListSlot.transform.localScale = Vector3.one;
			return nkcuishipSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060084CA RID: 33994 RVA: 0x002CCCD0 File Offset: 0x002CAED0
		private void ReturnShipSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060084CB RID: 33995 RVA: 0x002CCCEC File Offset: 0x002CAEEC
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
			switch (this.m_curTabType)
			{
			case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_FINAL:
				component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.FINAL);
				goto IL_A0;
			case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING:
				component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.CASTING);
				goto IL_A0;
			}
			component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.ROTATION);
			IL_A0:
			component.SetEnableShowBan(true);
			component.SetDataForBan(nkmunitTempletBase, true, null, false, false);
		}

		// Token: 0x060084CC RID: 33996 RVA: 0x002CCDAB File Offset: 0x002CAFAB
		private RectTransform GetOperSlot(int index)
		{
			NKCUIOperatorSelectListSlot nkcuioperatorSelectListSlot = UnityEngine.Object.Instantiate<NKCUIOperatorSelectListSlot>(this.m_pfbOperSlotForBan);
			nkcuioperatorSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuioperatorSelectListSlot, true);
			nkcuioperatorSelectListSlot.transform.localScale = Vector3.one;
			return nkcuioperatorSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060084CD RID: 33997 RVA: 0x002CCDDB File Offset: 0x002CAFDB
		private void ReturnOperSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060084CE RID: 33998 RVA: 0x002CCDF4 File Offset: 0x002CAFF4
		private void ProvideOperSlotData(Transform tr, int idx)
		{
			NKCUIOperatorSelectListSlot component = tr.GetComponent<NKCUIOperatorSelectListSlot>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 || idx >= this.m_lstNKMBanOperatorData.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			int operatorID = this.m_lstNKMBanOperatorData[idx].m_OperatorID;
			NKCUtil.SetGameobjectActive(component, true);
			NKMUnitManager.GetUnitTempletBase(operatorID);
			component.SetEnableShowBan(true);
			switch (this.m_curTabType)
			{
			case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_FINAL:
				component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.FINAL);
				goto IL_86;
			case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING:
				component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.CASTING);
				goto IL_86;
			}
			component.SetBanDataType(NKCBanManager.BAN_DATA_TYPE.ROTATION);
			IL_86:
			component.SetDataForBan(NKCOperatorUtil.GetDummyOperator(this.m_lstNKMBanOperatorData[idx].m_OperatorID, false), true, null);
			component.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x002CCEAD File Offset: 0x002CB0AD
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x002CCEBB File Offset: 0x002CB0BB
		public void Open()
		{
			base.UIOpened(true);
			this.m_ctglFinalBan.Select(true, true, false);
			this.OnClickTab(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_FINAL, true);
		}

		// Token: 0x060084D1 RID: 34001 RVA: 0x002CCEDC File Offset: 0x002CB0DC
		private void InitLeftMenu()
		{
			string @string = NKCStringTable.GetString("SI_PVP_POPUP_BAN_LIST_MENU_BAN_UNIT", false);
			string string2 = NKCStringTable.GetString("SI_PVP_POPUP_BAN_LIST_MENU_BAN_SHIP", false);
			string string3 = NKCStringTable.GetString("SI_PVP_POPUP_BAN_LIST_MENU_BAN_OPER", false);
			bool flag = NKCOperatorUtil.IsActiveCastingBan();
			this.m_GuideSubSlotFinalUnit.Init(@string, "UNIT", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
			this.m_GuideSubSlotFinalShip.Init(string2, "SHIP", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
			this.m_GuideSubSlotCastingUnit.Init(@string, "UNIT", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
			this.m_GuideSubSlotCastingShip.Init(string2, "SHIP", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
			this.m_GuideSubSlotRotationUnit.Init(@string, "UNIT", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
			this.m_GuideSubSlotRotationShip.Init(string2, "SHIP", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
			List<NKCUIPopupGuideSubSlot> list = new List<NKCUIPopupGuideSubSlot>
			{
				this.m_GuideSubSlotFinalUnit,
				this.m_GuideSubSlotFinalShip
			};
			List<NKCUIPopupGuideSubSlot> list2 = new List<NKCUIPopupGuideSubSlot>
			{
				this.m_GuideSubSlotCastingUnit,
				this.m_GuideSubSlotCastingShip
			};
			List<NKCUIPopupGuideSubSlot> list3 = new List<NKCUIPopupGuideSubSlot>
			{
				this.m_GuideSubSlotRotationUnit,
				this.m_GuideSubSlotRotationShip
			};
			if (flag)
			{
				this.m_GuideSubSlotFinalOper.Init(string3, "OPER", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
				this.m_GuideSubSlotCastingOper.Init(string3, "OPER", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
				this.m_GuideSubSlotRotationOper.Init(string3, "OPER", new NKCUIPopupGuideSubSlot.OnClicked(this.OnClickedSubSlot));
				list.Add(this.m_GuideSubSlotFinalOper);
				list2.Add(this.m_GuideSubSlotCastingOper);
				list3.Add(this.m_GuideSubSlotRotationOper);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_GuideSubSlotFinalOper.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_GuideSubSlotCastingOper.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_GuideSubSlotRotationOper.gameObject, false);
			}
			this.m_dicCastingBan.Add(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_FINAL, list);
			this.m_dicCastingBan.Add(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING, list2);
			this.m_dicCastingBan.Add(NKCPopupGauntletBanListV2.TAB_TYPE.TAB_ROTATION, list3);
		}

		// Token: 0x060084D2 RID: 34002 RVA: 0x002CD0F9 File Offset: 0x002CB2F9
		private void OnClickTab(NKCPopupGauntletBanListV2.TAB_TYPE tabType, bool bForce = false)
		{
			if (this.m_curTabType == tabType && !bForce)
			{
				return;
			}
			this.m_curTabType = tabType;
			this.OnClickedSubSlot("UNIT", 0);
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x002CD11C File Offset: 0x002CB31C
		public void OnClickedSubSlot(string ArticleID, int i = 0)
		{
			if (this.m_curTabType == NKCPopupGauntletBanListV2.TAB_TYPE.TAB_UP)
			{
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCUtilString.GET_STRING_GAUNTLET_BAN_POPUP_DESC_UP);
			}
			else if (string.Equals(ArticleID, "UNIT"))
			{
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCUtilString.GET_STRING_GAUNTLET_BAN_POPUP_DESC_UNIT);
			}
			else if (string.Equals(ArticleID, "SHIP"))
			{
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCUtilString.GET_STRING_GAUNTLET_BAN_POPUP_DESC_SHIP);
			}
			else if (string.Equals(ArticleID, "OPER"))
			{
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCUtilString.GET_STRING_GAUNTLET_BAN_POPUP_DESC_OPER);
			}
			foreach (KeyValuePair<NKCPopupGauntletBanListV2.TAB_TYPE, List<NKCUIPopupGuideSubSlot>> keyValuePair in this.m_dicCastingBan)
			{
				foreach (NKCUIPopupGuideSubSlot nkcuipopupGuideSubSlot in keyValuePair.Value)
				{
					NKCUtil.SetGameobjectActive(nkcuipopupGuideSubSlot.gameObject, true);
					nkcuipopupGuideSubSlot.OnActive(keyValuePair.Key == this.m_curTabType);
					nkcuipopupGuideSubSlot.OnSelectedObject(ArticleID);
				}
			}
			this.UpdateRightUI(this.m_curTabType, ArticleID);
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x002CD250 File Offset: 0x002CB450
		private void UpdateRightUI(NKCPopupGauntletBanListV2.TAB_TYPE tabType, string articleID)
		{
			NKCUtil.SetGameobjectActive(this.m_objUnitList, string.Equals(articleID, "UNIT"));
			NKCUtil.SetGameobjectActive(this.m_objShipList, string.Equals(articleID, "SHIP"));
			NKCUtil.SetGameobjectActive(this.m_objOperList, string.Equals(articleID, "OPER"));
			if (tabType == NKCPopupGauntletBanListV2.TAB_TYPE.TAB_UP)
			{
				this.m_lstNKMUnitUpData = new List<NKMUnitUpData>(NKCBanManager.m_dicNKMUpData.Values);
				this.m_lstNKMUnitUpData.Sort(new NKCPopupGauntletBanListV2.CompNKMUnitUpData());
				this.m_lvsrUnit.TotalCount = this.m_lstNKMUnitUpData.Count;
			}
			else if (string.Equals(articleID, "UNIT"))
			{
				this.m_lstNKMBanData = null;
				switch (tabType)
				{
				case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_ROTATION:
					this.m_lstNKMBanData = new List<NKMBanData>(NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.ROTATION).Values);
					goto IL_F9;
				case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING:
					this.m_lstNKMBanData = new List<NKMBanData>(NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.CASTING).Values);
					goto IL_F9;
				}
				this.m_lstNKMBanData = new List<NKMBanData>(NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL).Values);
				IL_F9:
				this.m_lstNKMBanData.Sort(new NKCPopupGauntletBanListV2.CompNKMBanData());
				this.m_lvsrUnit.TotalCount = this.m_lstNKMBanData.Count;
			}
			else if (string.Equals(articleID, "SHIP"))
			{
				this.m_lstNKMBanShipData = null;
				switch (tabType)
				{
				case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_ROTATION:
					this.m_lstNKMBanShipData = new List<NKMBanShipData>(NKCBanManager.GetBanDataShip(NKCBanManager.BAN_DATA_TYPE.ROTATION).Values);
					goto IL_197;
				case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING:
					this.m_lstNKMBanShipData = new List<NKMBanShipData>(NKCBanManager.GetBanDataShip(NKCBanManager.BAN_DATA_TYPE.CASTING).Values);
					goto IL_197;
				}
				this.m_lstNKMBanShipData = new List<NKMBanShipData>(NKCBanManager.GetBanDataShip(NKCBanManager.BAN_DATA_TYPE.FINAL).Values);
				IL_197:
				this.m_lstNKMBanShipData.Sort(new NKCPopupGauntletBanListV2.CompNKMBanShipData());
				this.m_lvsrShip.TotalCount = this.m_lstNKMBanShipData.Count;
			}
			else if (string.Equals(articleID, "OPER"))
			{
				this.m_lstNKMBanOperatorData = null;
				switch (tabType)
				{
				case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_ROTATION:
					this.m_lstNKMBanOperatorData = new List<NKMBanOperatorData>(NKCBanManager.GetBanDataOperator(NKCBanManager.BAN_DATA_TYPE.ROTATION).Values);
					goto IL_235;
				case NKCPopupGauntletBanListV2.TAB_TYPE.TAB_CASTING:
					this.m_lstNKMBanOperatorData = new List<NKMBanOperatorData>(NKCBanManager.GetBanDataOperator(NKCBanManager.BAN_DATA_TYPE.CASTING).Values);
					goto IL_235;
				}
				this.m_lstNKMBanOperatorData = new List<NKMBanOperatorData>(NKCBanManager.GetBanDataOperator(NKCBanManager.BAN_DATA_TYPE.FINAL).Values);
				IL_235:
				this.m_lstNKMBanOperatorData.Sort(new NKCPopupGauntletBanListV2.CompNKMBanOperData());
				this.m_lvsrOper.TotalCount = this.m_lstNKMBanOperatorData.Count;
			}
			this.m_dicUTB_ByShipGroupID.Clear();
			this.m_lvsrUnit.SetIndexPosition(0);
			this.m_lvsrShip.SetIndexPosition(0);
			this.m_lvsrOper.SetIndexPosition(0);
		}

		// Token: 0x04007101 RID: 28929
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x04007102 RID: 28930
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_POPUP_BANNED_LIST_NEW";

		// Token: 0x04007103 RID: 28931
		private static NKCPopupGauntletBanListV2 m_Instance;

		// Token: 0x04007104 RID: 28932
		[Header("공통")]
		public EventTrigger m_etBG;

		// Token: 0x04007105 RID: 28933
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04007106 RID: 28934
		[Header("상단")]
		public NKCUIComStateButton m_csbtnGuide;

		// Token: 0x04007107 RID: 28935
		public Text m_lbSubTitle;

		// Token: 0x04007108 RID: 28936
		[Header("왼쪽")]
		public NKCUIComToggle m_ctglFinalBan;

		// Token: 0x04007109 RID: 28937
		public NKCUIComToggle m_ctglRotationBan;

		// Token: 0x0400710A RID: 28938
		public NKCUIComToggle m_ctglCastingBan;

		// Token: 0x0400710B RID: 28939
		public NKCUIComToggle m_ctglUpUnit;

		// Token: 0x0400710C RID: 28940
		public NKCUIPopupGuideSubSlot m_GuideSubSlotFinalUnit;

		// Token: 0x0400710D RID: 28941
		public NKCUIPopupGuideSubSlot m_GuideSubSlotFinalShip;

		// Token: 0x0400710E RID: 28942
		public NKCUIPopupGuideSubSlot m_GuideSubSlotFinalOper;

		// Token: 0x0400710F RID: 28943
		public NKCUIPopupGuideSubSlot m_GuideSubSlotRotationUnit;

		// Token: 0x04007110 RID: 28944
		public NKCUIPopupGuideSubSlot m_GuideSubSlotRotationShip;

		// Token: 0x04007111 RID: 28945
		public NKCUIPopupGuideSubSlot m_GuideSubSlotRotationOper;

		// Token: 0x04007112 RID: 28946
		public NKCUIPopupGuideSubSlot m_GuideSubSlotCastingUnit;

		// Token: 0x04007113 RID: 28947
		public NKCUIPopupGuideSubSlot m_GuideSubSlotCastingShip;

		// Token: 0x04007114 RID: 28948
		public NKCUIPopupGuideSubSlot m_GuideSubSlotCastingOper;

		// Token: 0x04007115 RID: 28949
		[Header("오른쪽")]
		public LoopVerticalScrollRect m_lvsrUnit;

		// Token: 0x04007116 RID: 28950
		public LoopVerticalScrollRect m_lvsrShip;

		// Token: 0x04007117 RID: 28951
		public LoopVerticalScrollRect m_lvsrOper;

		// Token: 0x04007118 RID: 28952
		public GameObject m_objUnitList;

		// Token: 0x04007119 RID: 28953
		public GameObject m_objShipList;

		// Token: 0x0400711A RID: 28954
		public GameObject m_objOperList;

		// Token: 0x0400711B RID: 28955
		public NKCUIUnitSelectListSlot m_pfbUnitSlotForBan;

		// Token: 0x0400711C RID: 28956
		public NKCUIShipSelectListSlot m_pfbShipSlotForBan;

		// Token: 0x0400711D RID: 28957
		public NKCUIOperatorSelectListSlot m_pfbOperSlotForBan;

		// Token: 0x0400711E RID: 28958
		private List<NKMBanData> m_lstNKMBanData = new List<NKMBanData>();

		// Token: 0x0400711F RID: 28959
		private List<NKMBanShipData> m_lstNKMBanShipData = new List<NKMBanShipData>();

		// Token: 0x04007120 RID: 28960
		private List<NKMBanOperatorData> m_lstNKMBanOperatorData = new List<NKMBanOperatorData>();

		// Token: 0x04007121 RID: 28961
		private List<NKMUnitUpData> m_lstNKMUnitUpData = new List<NKMUnitUpData>();

		// Token: 0x04007122 RID: 28962
		private Dictionary<int, NKMUnitTempletBase> m_dicUTB_ByShipGroupID = new Dictionary<int, NKMUnitTempletBase>();

		// Token: 0x04007123 RID: 28963
		private const string UNIT = "UNIT";

		// Token: 0x04007124 RID: 28964
		private const string SHIP = "SHIP";

		// Token: 0x04007125 RID: 28965
		private const string OPER = "OPER";

		// Token: 0x04007126 RID: 28966
		private Dictionary<NKCPopupGauntletBanListV2.TAB_TYPE, List<NKCUIPopupGuideSubSlot>> m_dicCastingBan = new Dictionary<NKCPopupGauntletBanListV2.TAB_TYPE, List<NKCUIPopupGuideSubSlot>>();

		// Token: 0x04007127 RID: 28967
		private NKCPopupGauntletBanListV2.TAB_TYPE m_curTabType;

		// Token: 0x020018FB RID: 6395
		public class CompNKMUnitUpData : IComparer<NKMUnitUpData>
		{
			// Token: 0x0600B753 RID: 46931 RVA: 0x00367959 File Offset: 0x00365B59
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

		// Token: 0x020018FC RID: 6396
		public class CompNKMBanData : IComparer<NKMBanData>
		{
			// Token: 0x0600B755 RID: 46933 RVA: 0x0036798E File Offset: 0x00365B8E
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

		// Token: 0x020018FD RID: 6397
		public class CompNKMBanShipData : IComparer<NKMBanShipData>
		{
			// Token: 0x0600B757 RID: 46935 RVA: 0x003679C3 File Offset: 0x00365BC3
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

		// Token: 0x020018FE RID: 6398
		public class CompNKMBanOperData : IComparer<NKMBanOperatorData>
		{
			// Token: 0x0600B759 RID: 46937 RVA: 0x003679F8 File Offset: 0x00365BF8
			public int Compare(NKMBanOperatorData x, NKMBanOperatorData y)
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

		// Token: 0x020018FF RID: 6399
		private enum TAB_TYPE
		{
			// Token: 0x0400AA43 RID: 43587
			TAB_NONE,
			// Token: 0x0400AA44 RID: 43588
			TAB_FINAL,
			// Token: 0x0400AA45 RID: 43589
			TAB_ROTATION,
			// Token: 0x0400AA46 RID: 43590
			TAB_CASTING,
			// Token: 0x0400AA47 RID: 43591
			TAB_UP
		}
	}
}

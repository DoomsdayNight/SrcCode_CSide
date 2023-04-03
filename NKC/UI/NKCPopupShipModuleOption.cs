using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A86 RID: 2694
	public class NKCPopupShipModuleOption : NKCUIBase
	{
		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06007728 RID: 30504 RVA: 0x0027A404 File Offset: 0x00278604
		public static NKCPopupShipModuleOption Instance
		{
			get
			{
				if (NKCPopupShipModuleOption.m_Instance == null)
				{
					NKCPopupShipModuleOption.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShipModuleOption>("ab_ui_nkm_ui_ship_info", "NKM_UI_POPUP_SHIP_MODULE_OPTION", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShipModuleOption.CleanupInstance)).GetInstance<NKCPopupShipModuleOption>();
					NKCPopupShipModuleOption.m_Instance.Init();
				}
				return NKCPopupShipModuleOption.m_Instance;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06007729 RID: 30505 RVA: 0x0027A453 File Offset: 0x00278653
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupShipModuleOption.m_Instance != null && NKCPopupShipModuleOption.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600772A RID: 30506 RVA: 0x0027A46E File Offset: 0x0027866E
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShipModuleOption.m_Instance != null && NKCPopupShipModuleOption.m_Instance.IsOpen)
			{
				NKCPopupShipModuleOption.m_Instance.Close();
			}
		}

		// Token: 0x0600772B RID: 30507 RVA: 0x0027A493 File Offset: 0x00278693
		private static void CleanupInstance()
		{
			NKCPopupShipModuleOption.m_Instance = null;
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x0600772C RID: 30508 RVA: 0x0027A49B File Offset: 0x0027869B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x0600772D RID: 30509 RVA: 0x0027A49E File Offset: 0x0027869E
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600772E RID: 30510 RVA: 0x0027A4A8 File Offset: 0x002786A8
		public override void CloseInternal()
		{
			foreach (GameObject obj in this.m_lstSlots)
			{
				UnityEngine.Object.Destroy(obj);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600772F RID: 30511 RVA: 0x0027A504 File Offset: 0x00278704
		private void Init()
		{
			this.m_btnOk.PointerClick.RemoveAllListeners();
			this.m_btnOk.PointerClick.AddListener(new UnityAction(base.Close));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
		}

		// Token: 0x06007730 RID: 30512 RVA: 0x0027A574 File Offset: 0x00278774
		public void Open(NKM_UNIT_STYLE_TYPE styleType, NKM_UNIT_GRADE grade, int moduleIndex)
		{
			NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = NKMShipManager.GetNKMShipCommandModuleTemplet(styleType, grade, moduleIndex + 1);
			IReadOnlyList<NKMCommandModulePassiveTemplet> passiveListsByGroupId = NKMShipModuleGroupTemplet.GetPassiveListsByGroupId(nkmshipCommandModuleTemplet.Slot1Id);
			IReadOnlyList<NKMCommandModulePassiveTemplet> passiveListsByGroupId2 = NKMShipModuleGroupTemplet.GetPassiveListsByGroupId(nkmshipCommandModuleTemplet.Slot2Id);
			if (passiveListsByGroupId == null || passiveListsByGroupId2 == null)
			{
				return;
			}
			List<NKMCommandModuleRandomStatTemplet> list = new List<NKMCommandModuleRandomStatTemplet>();
			List<NKMCommandModuleRandomStatTemplet> list2 = new List<NKMCommandModuleRandomStatTemplet>();
			List<int> list3 = new List<int>();
			for (int i = 0; i < passiveListsByGroupId.Count; i++)
			{
				if (!list3.Contains(passiveListsByGroupId[i].StatGroupId))
				{
					list3.Add(passiveListsByGroupId[i].StatGroupId);
				}
			}
			List<int> list4 = new List<int>();
			for (int m = 0; m < passiveListsByGroupId2.Count; m++)
			{
				if (!list4.Contains(passiveListsByGroupId2[m].StatGroupId))
				{
					list4.Add(passiveListsByGroupId2[m].StatGroupId);
				}
			}
			for (int k = 0; k < list3.Count; k++)
			{
				IReadOnlyList<NKMCommandModuleRandomStatTemplet> lstStatTemplet_01 = NKMShipModuleGroupTemplet.GetStatListsByGroupId(list3[k]);
				int j2;
				int j;
				Predicate<NKMCommandModuleRandomStatTemplet> <>9__0;
				for (j = 0; j < lstStatTemplet_01.Count; j = j2 + 1)
				{
					List<NKMCommandModuleRandomStatTemplet> list5 = list;
					Predicate<NKMCommandModuleRandomStatTemplet> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((NKMCommandModuleRandomStatTemplet x) => x.StatType == lstStatTemplet_01[j].StatType));
					}
					if (list5.Find(match) == null)
					{
						list.Add(lstStatTemplet_01[j]);
					}
					j2 = j;
				}
			}
			for (int l = 0; l < list4.Count; l++)
			{
				IReadOnlyList<NKMCommandModuleRandomStatTemplet> lstStatTemplet_02 = NKMShipModuleGroupTemplet.GetStatListsByGroupId(list4[l]);
				int j2;
				int j;
				Predicate<NKMCommandModuleRandomStatTemplet> <>9__1;
				for (j = 0; j < lstStatTemplet_02.Count; j = j2 + 1)
				{
					List<NKMCommandModuleRandomStatTemplet> list6 = list2;
					Predicate<NKMCommandModuleRandomStatTemplet> match2;
					if ((match2 = <>9__1) == null)
					{
						match2 = (<>9__1 = ((NKMCommandModuleRandomStatTemplet x) => x.StatType == lstStatTemplet_02[j].StatType));
					}
					if (list6.Find(match2) == null)
					{
						list2.Add(lstStatTemplet_02[j]);
					}
					j2 = j;
				}
			}
			this.SetData(0, list);
			this.SetData(1, list2);
			base.UIOpened(true);
		}

		// Token: 0x06007731 RID: 30513 RVA: 0x0027A7B8 File Offset: 0x002789B8
		private void SetData(int Idx, List<NKMCommandModuleRandomStatTemplet> lstStat)
		{
			foreach (NKMCommandModuleRandomStatTemplet nkmcommandModuleRandomStatTemplet in lstStat)
			{
				NKCPopupEquipOptionListSlot nkcpopupEquipOptionListSlot = UnityEngine.Object.Instantiate<NKCPopupEquipOptionListSlot>(this.m_pfbSlot);
				if (nkcpopupEquipOptionListSlot != null)
				{
					if (Idx == 0)
					{
						nkcpopupEquipOptionListSlot.transform.SetParent(this.m_trOption_01, false);
					}
					else
					{
						nkcpopupEquipOptionListSlot.transform.SetParent(this.m_trOption_02, false);
					}
					if (NKCUtilString.IsNameReversedIfNegative(nkmcommandModuleRandomStatTemplet.StatType) && (nkmcommandModuleRandomStatTemplet.MaxStatValue < 0f || nkmcommandModuleRandomStatTemplet.MaxStatFactor < 0f))
					{
						nkcpopupEquipOptionListSlot.SetData(NKCUtilString.GetStatShortName(nkmcommandModuleRandomStatTemplet.StatType, true), string.Format("{0} ~ {1}", NKCUtilString.GetShipModuleStatValue(nkmcommandModuleRandomStatTemplet.StatType, nkmcommandModuleRandomStatTemplet.MaxStatValue, nkmcommandModuleRandomStatTemplet.MaxStatFactor), NKCUtilString.GetShipModuleStatValue(nkmcommandModuleRandomStatTemplet.StatType, nkmcommandModuleRandomStatTemplet.MinStatValue, nkmcommandModuleRandomStatTemplet.MinStatFactor)));
					}
					else
					{
						nkcpopupEquipOptionListSlot.SetData(NKCUtilString.GetStatShortName(nkmcommandModuleRandomStatTemplet.StatType), string.Format("{0} ~ {1}", NKCUtilString.GetShipModuleStatValue(nkmcommandModuleRandomStatTemplet.StatType, nkmcommandModuleRandomStatTemplet.MinStatValue, nkmcommandModuleRandomStatTemplet.MinStatFactor), NKCUtilString.GetShipModuleStatValue(nkmcommandModuleRandomStatTemplet.StatType, nkmcommandModuleRandomStatTemplet.MaxStatValue, nkmcommandModuleRandomStatTemplet.MaxStatFactor)));
					}
					this.m_lstSlots.Add(nkcpopupEquipOptionListSlot.gameObject);
				}
			}
		}

		// Token: 0x040063B6 RID: 25526
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_ship_info";

		// Token: 0x040063B7 RID: 25527
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHIP_MODULE_OPTION";

		// Token: 0x040063B8 RID: 25528
		private static NKCPopupShipModuleOption m_Instance;

		// Token: 0x040063B9 RID: 25529
		public NKCPopupEquipOptionListSlot m_pfbSlot;

		// Token: 0x040063BA RID: 25530
		public ScrollRect m_srOption_01;

		// Token: 0x040063BB RID: 25531
		public Transform m_trOption_01;

		// Token: 0x040063BC RID: 25532
		public ScrollRect m_srOption_02;

		// Token: 0x040063BD RID: 25533
		public Transform m_trOption_02;

		// Token: 0x040063BE RID: 25534
		public NKCUIComStateButton m_btnOk;

		// Token: 0x040063BF RID: 25535
		public EventTrigger m_etBG;

		// Token: 0x040063C0 RID: 25536
		private List<GameObject> m_lstSlots = new List<GameObject>();
	}
}

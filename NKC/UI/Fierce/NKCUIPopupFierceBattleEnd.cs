using System;
using NKC.UI.Shop;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BBA RID: 3002
	public class NKCUIPopupFierceBattleEnd : NKCUIBase
	{
		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06008A9D RID: 35485 RVA: 0x002F2330 File Offset: 0x002F0530
		public static NKCUIPopupFierceBattleEnd Instance
		{
			get
			{
				if (NKCUIPopupFierceBattleEnd.m_Instance == null)
				{
					NKCUIPopupFierceBattleEnd.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupFierceBattleEnd>("ab_ui_nkm_ui_world_map_renewal", "NKM_UI_POPUP_FIERCE_BATTLE_END", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupFierceBattleEnd.CleanupInstance)).GetInstance<NKCUIPopupFierceBattleEnd>();
					NKCUIPopupFierceBattleEnd.m_Instance.Init();
				}
				return NKCUIPopupFierceBattleEnd.m_Instance;
			}
		}

		// Token: 0x06008A9E RID: 35486 RVA: 0x002F237F File Offset: 0x002F057F
		private static void CleanupInstance()
		{
			NKCUIPopupFierceBattleEnd.m_Instance = null;
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06008A9F RID: 35487 RVA: 0x002F2387 File Offset: 0x002F0587
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupFierceBattleEnd.m_Instance != null && NKCUIPopupFierceBattleEnd.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008AA0 RID: 35488 RVA: 0x002F23A2 File Offset: 0x002F05A2
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupFierceBattleEnd.m_Instance != null && NKCUIPopupFierceBattleEnd.m_Instance.IsOpen)
			{
				NKCUIPopupFierceBattleEnd.m_Instance.Close();
			}
		}

		// Token: 0x06008AA1 RID: 35489 RVA: 0x002F23C7 File Offset: 0x002F05C7
		private void OnDestroy()
		{
			NKCUIPopupFierceBattleEnd.m_Instance = null;
		}

		// Token: 0x06008AA2 RID: 35490 RVA: 0x002F23CF File Offset: 0x002F05CF
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06008AA3 RID: 35491 RVA: 0x002F23DD File Offset: 0x002F05DD
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06008AA4 RID: 35492 RVA: 0x002F23E0 File Offset: 0x002F05E0
		public override string MenuName
		{
			get
			{
				return "FIERCE_BATTLE_END_POPUP";
			}
		}

		// Token: 0x06008AA5 RID: 35493 RVA: 0x002F23E8 File Offset: 0x002F05E8
		private void Init()
		{
			if (this.m_SEASON_REWARD_Button != null)
			{
				this.m_SEASON_REWARD_Button.PointerClick.RemoveAllListeners();
				this.m_SEASON_REWARD_Button.PointerClick.AddListener(delegate()
				{
					NKCUIShop.ShopShortcut("TAB_SEASON_FIERCE_POINT", 0, 0);
				});
			}
			if (this.m_POPUP_CONSORTIUM_COOP_END_Bg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCUIPopupFierceBattleEnd.CheckInstanceAndClose();
				});
				this.m_POPUP_CONSORTIUM_COOP_END_Bg.triggers.Add(entry);
			}
		}

		// Token: 0x06008AA6 RID: 35494 RVA: 0x002F2498 File Offset: 0x002F0698
		public void Open()
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null && !nkcfierceBattleSupportDataMgr.IsCanAccessFierce())
			{
				NKCUtil.SetLabelText(this.m_lbEndInfo, nkcfierceBattleSupportDataMgr.GetAccessDeniedMessage());
				base.UIOpened(true);
			}
		}

		// Token: 0x04007766 RID: 30566
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_world_map_renewal";

		// Token: 0x04007767 RID: 30567
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FIERCE_BATTLE_END";

		// Token: 0x04007768 RID: 30568
		private static NKCUIPopupFierceBattleEnd m_Instance;

		// Token: 0x04007769 RID: 30569
		public Text m_lbEndInfo;

		// Token: 0x0400776A RID: 30570
		public NKCUIComStateButton m_SEASON_REWARD_Button;

		// Token: 0x0400776B RID: 30571
		public EventTrigger m_POPUP_CONSORTIUM_COOP_END_Bg;
	}
}

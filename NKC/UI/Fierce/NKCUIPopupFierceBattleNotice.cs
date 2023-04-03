using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BBB RID: 3003
	public class NKCUIPopupFierceBattleNotice : NKCUIBase
	{
		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06008AA8 RID: 35496 RVA: 0x002F24DC File Offset: 0x002F06DC
		public static NKCUIPopupFierceBattleNotice Instance
		{
			get
			{
				if (NKCUIPopupFierceBattleNotice.m_Instance == null)
				{
					NKCUIPopupFierceBattleNotice.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupFierceBattleNotice>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_NOTICE", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupFierceBattleNotice.CleanupInstance)).GetInstance<NKCUIPopupFierceBattleNotice>();
					NKCUIPopupFierceBattleNotice.m_Instance.Init();
				}
				return NKCUIPopupFierceBattleNotice.m_Instance;
			}
		}

		// Token: 0x06008AA9 RID: 35497 RVA: 0x002F252B File Offset: 0x002F072B
		private static void CleanupInstance()
		{
			NKCUIPopupFierceBattleNotice.m_Instance = null;
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06008AAA RID: 35498 RVA: 0x002F2533 File Offset: 0x002F0733
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupFierceBattleNotice.m_Instance != null && NKCUIPopupFierceBattleNotice.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008AAB RID: 35499 RVA: 0x002F254E File Offset: 0x002F074E
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupFierceBattleNotice.m_Instance != null && NKCUIPopupFierceBattleNotice.m_Instance.IsOpen)
			{
				NKCUIPopupFierceBattleNotice.m_Instance.Close();
			}
		}

		// Token: 0x06008AAC RID: 35500 RVA: 0x002F2573 File Offset: 0x002F0773
		private void OnDestroy()
		{
			NKCUIPopupFierceBattleNotice.m_Instance = null;
		}

		// Token: 0x06008AAD RID: 35501 RVA: 0x002F257B File Offset: 0x002F077B
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06008AAE RID: 35502 RVA: 0x002F2589 File Offset: 0x002F0789
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06008AAF RID: 35503 RVA: 0x002F258C File Offset: 0x002F078C
		public override string MenuName
		{
			get
			{
				return "FIERCE_BATTLE_NOTICE";
			}
		}

		// Token: 0x06008AB0 RID: 35504 RVA: 0x002F2594 File Offset: 0x002F0794
		public void Init()
		{
			if (this.m_POPUP_FIERCE_BATTLE_NOTICE_Bg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCUIPopupFierceBattleNotice.CheckInstanceAndClose();
				});
				this.m_POPUP_FIERCE_BATTLE_NOTICE_Bg.triggers.Add(entry);
			}
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_CLOSE_BUTTON, new UnityAction(NKCUIPopupFierceBattleNotice.CheckInstanceAndClose));
			NKCUtil.SetBindFunction(this.m_POPUP_FIERCE_BATTLE_NOTICE_BUTTON, new UnityAction(this.MoveToFierce));
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x002F2628 File Offset: 0x002F0828
		public void Open()
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr == null)
			{
				return;
			}
			if (nkcfierceBattleSupportDataMgr.FierceTemplet == null)
			{
				return;
			}
			List<int> fierceBossGroupIdList = nkcfierceBattleSupportDataMgr.FierceTemplet.FierceBossGroupIdList;
			for (int i = 0; i < this.m_lstFierceBattleNoticeSlot.Count; i++)
			{
				int num = 0;
				if (fierceBossGroupIdList.Count > i)
				{
					num = fierceBossGroupIdList[i];
					string bossFaceCardName = "";
					if (NKMFierceBossGroupTemplet.Groups.ContainsKey(num))
					{
						foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Groups[num])
						{
							if (nkmfierceBossGroupTemplet.Level == 1)
							{
								bossFaceCardName = nkmfierceBossGroupTemplet.UI_BossFaceCard;
								break;
							}
						}
					}
					string targetBossName = nkcfierceBattleSupportDataMgr.GetTargetBossName(num, 0);
					this.m_lstFierceBattleNoticeSlot[i].SetData(bossFaceCardName, targetBossName);
					NKCUtil.SetGameobjectActive(this.m_lstFierceBattleNoticeSlot[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstFierceBattleNoticeSlot[i], false);
				}
			}
			base.UIOpened(true);
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x002F2748 File Offset: 0x002F0948
		private void MoveToFierce()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.WORLDMAP, 0, 0) || !NKCContentManager.IsContentsUnlocked(ContentsType.FIERCE, 0, 0))
			{
				return;
			}
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_FIERCE, "", false);
		}

		// Token: 0x0400776C RID: 30572
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_fierce_battle";

		// Token: 0x0400776D RID: 30573
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FIERCE_BATTLE_NOTICE";

		// Token: 0x0400776E RID: 30574
		private static NKCUIPopupFierceBattleNotice m_Instance;

		// Token: 0x0400776F RID: 30575
		public EventTrigger m_POPUP_FIERCE_BATTLE_NOTICE_Bg;

		// Token: 0x04007770 RID: 30576
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSE_BUTTON;

		// Token: 0x04007771 RID: 30577
		public NKCUIComStateButton m_POPUP_FIERCE_BATTLE_NOTICE_BUTTON;

		// Token: 0x04007772 RID: 30578
		public List<NKCUIFierceBattleNoticeSlot> m_lstFierceBattleNoticeSlot;
	}
}

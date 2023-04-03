using System;
using ClientPacket.Common;
using NKC.UI.Component;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x02000953 RID: 2387
	public class NKCPopupEventKillCountReward : NKCUIBase
	{
		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06005F2A RID: 24362 RVA: 0x001D92D4 File Offset: 0x001D74D4
		public static NKCPopupEventKillCountReward Instance
		{
			get
			{
				if (NKCPopupEventKillCountReward.m_Instance == null)
				{
					NKCPopupEventKillCountReward.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventKillCountReward>("AB_UI_NKM_UI_EVENT_PF_COUNT", "NKM_UI_POPUP_EVENT_PF_COUNT_REWARD_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventKillCountReward.CleanUpInstance)).GetInstance<NKCPopupEventKillCountReward>();
					NKCPopupEventKillCountReward.m_Instance.InitUI();
				}
				return NKCPopupEventKillCountReward.m_Instance;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x001D9323 File Offset: 0x001D7523
		public static bool HasInstance
		{
			get
			{
				return NKCPopupEventKillCountReward.m_Instance != null;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06005F2C RID: 24364 RVA: 0x001D9330 File Offset: 0x001D7530
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventKillCountReward.m_Instance != null && NKCPopupEventKillCountReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x001D934B File Offset: 0x001D754B
		private static void CleanUpInstance()
		{
			NKCPopupEventKillCountReward.m_Instance.Release();
			NKCPopupEventKillCountReward.m_Instance = null;
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x001D935D File Offset: 0x001D755D
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventKillCountReward.m_Instance != null && NKCPopupEventKillCountReward.m_Instance.IsOpen)
			{
				NKCPopupEventKillCountReward.m_Instance.Close();
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x001D9382 File Offset: 0x001D7582
		public override string MenuName
		{
			get
			{
				return "Killcount Reward";
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06005F30 RID: 24368 RVA: 0x001D9389 File Offset: 0x001D7589
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x001D938C File Offset: 0x001D758C
		public void InitUI()
		{
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			int childCount = this.m_slotContents.childCount;
			for (int i = 0; i < childCount; i++)
			{
				NKCUIComKillCountRewardSlot component = this.m_slotContents.GetChild(i).GetComponent<NKCUIComKillCountRewardSlot>();
				if (component != null)
				{
					component.Init();
				}
			}
			if (this.m_eventBG != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData e)
				{
					base.Close();
				});
				this.m_eventBG.triggers.Clear();
				this.m_eventBG.triggers.Add(entry);
			}
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x001D9450 File Offset: 0x001D7650
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x001D945E File Offset: 0x001D765E
		public void Open(int eventId)
		{
			this.m_iEventId = eventId;
			this.SetData();
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x001D9480 File Offset: 0x001D7680
		public void SetData()
		{
			NKMKillCountTemplet nkmkillCountTemplet = NKMKillCountTemplet.Find(this.m_iEventId);
			if (nkmkillCountTemplet == null)
			{
				return;
			}
			if (this.m_slotContents != null)
			{
				long currentServerKillCount = 0L;
				NKMServerKillCountData killCountServerData = NKCKillCountManager.GetKillCountServerData(this.m_iEventId);
				if (killCountServerData != null)
				{
					currentServerKillCount = killCountServerData.killCount;
				}
				NKMKillCountData killCountData = NKCKillCountManager.GetKillCountData(this.m_iEventId);
				int maxServerStep = nkmkillCountTemplet.GetMaxServerStep();
				int childCount = this.m_slotContents.childCount;
				int num = maxServerStep - childCount;
				for (int i = 0; i < num; i++)
				{
					NKCUIComKillCountRewardSlot component = UnityEngine.Object.Instantiate<GameObject>(this.m_slotContents.GetChild(0).gameObject, this.m_slotContents).GetComponent<NKCUIComKillCountRewardSlot>();
					if (component != null)
					{
						component.Init();
					}
				}
				childCount = this.m_slotContents.childCount;
				for (int j = 0; j < childCount; j++)
				{
					if (j >= maxServerStep)
					{
						NKCUtil.SetGameobjectActive(this.m_slotContents.gameObject, false);
					}
					else
					{
						NKCUIComKillCountRewardSlot component2 = this.m_slotContents.GetChild(j).GetComponent<NKCUIComKillCountRewardSlot>();
						NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
						nkmkillCountTemplet.TryGetServerStep(j + 1, out nkmkillCountStepTemplet);
						if (component2 == null || nkmkillCountStepTemplet == null)
						{
							NKCUtil.SetGameobjectActive(component2.gameObject, false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(component2.gameObject, true);
							component2.SetData(this.m_iEventId, nkmkillCountStepTemplet, killCountData, currentServerKillCount);
						}
					}
				}
			}
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x001D95CD File Offset: 0x001D77CD
		public void Release()
		{
			this.m_NKCUIOpenAnimator = null;
			this.m_eventBG = null;
		}

		// Token: 0x04004B43 RID: 19267
		public const string UI_ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PF_COUNT";

		// Token: 0x04004B44 RID: 19268
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_PF_COUNT_REWARD_INFO";

		// Token: 0x04004B45 RID: 19269
		private static NKCPopupEventKillCountReward m_Instance;

		// Token: 0x04004B46 RID: 19270
		public Transform m_slotContents;

		// Token: 0x04004B47 RID: 19271
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04004B48 RID: 19272
		public EventTrigger m_eventBG;

		// Token: 0x04004B49 RID: 19273
		private int m_iEventId;

		// Token: 0x04004B4A RID: 19274
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;
	}
}

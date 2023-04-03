using System;
using System.Collections.Generic;
using ClientPacket.Mode;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A85 RID: 2693
	public class NKCPopupShadowRecord : NKCUIBase
	{
		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x0600771A RID: 30490 RVA: 0x00279F9C File Offset: 0x0027819C
		public static NKCPopupShadowRecord Instance
		{
			get
			{
				if (NKCPopupShadowRecord.m_Instance == null)
				{
					NKCPopupShadowRecord.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShadowRecord>("AB_UI_OPERATION_SHADOW", "AB_UI_OPERATION_SHADOW_PALACE_RECORD_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShadowRecord.CleanupInstance)).GetInstance<NKCPopupShadowRecord>();
					NKCPopupShadowRecord.m_Instance.Init();
				}
				return NKCPopupShadowRecord.m_Instance;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x0600771B RID: 30491 RVA: 0x00279FEB File Offset: 0x002781EB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupShadowRecord.m_Instance != null && NKCPopupShadowRecord.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600771C RID: 30492 RVA: 0x0027A006 File Offset: 0x00278206
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShadowRecord.m_Instance != null && NKCPopupShadowRecord.m_Instance.IsOpen)
			{
				NKCPopupShadowRecord.m_Instance.Close();
			}
		}

		// Token: 0x0600771D RID: 30493 RVA: 0x0027A02B File Offset: 0x0027822B
		private static void CleanupInstance()
		{
			NKCPopupShadowRecord.m_Instance = null;
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x0600771E RID: 30494 RVA: 0x0027A033 File Offset: 0x00278233
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x0600771F RID: 30495 RVA: 0x0027A036 File Offset: 0x00278236
		public override string MenuName
		{
			get
			{
				return "shadow palace record popup";
			}
		}

		// Token: 0x06007720 RID: 30496 RVA: 0x0027A040 File Offset: 0x00278240
		private void Init()
		{
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.dOnGetObject += this.OnGetObject;
				this.m_scrollRect.dOnProvideData += this.OnProvideData;
				this.m_scrollRect.dOnReturnObject += this.OnReturnObject;
				this.m_scrollRect.ContentConstraintCount = 1;
				this.m_scrollRect.PrepareCells(0);
			}
			if (this.m_eventTrigger != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnTouchBG));
				this.m_eventTrigger.triggers.Clear();
				this.m_eventTrigger.triggers.Add(entry);
			}
		}

		// Token: 0x06007721 RID: 30497 RVA: 0x0027A10C File Offset: 0x0027830C
		public void Open(NKMShadowGameResult shadowResult, List<int> bestTime, NKCPopupShadowRecord.OnClose onClose)
		{
			if (shadowResult == null || shadowResult.life == 0)
			{
				return;
			}
			this.m_palaceID = shadowResult.palaceId;
			this.dOnClose = onClose;
			NKMPalaceData nkmpalaceData = NKCScenManager.CurrentUserData().m_ShadowPalace.palaceDataList.Find((NKMPalaceData v) => v.palaceId == this.m_palaceID);
			if (nkmpalaceData == null)
			{
				return;
			}
			NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(this.m_palaceID);
			if (palaceTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_txtTitle, NKCUtilString.GET_SHADOW_RECORD_POPUP_TITLE, new object[]
			{
				palaceTemplet.PALACE_NUM_UI,
				palaceTemplet.PalaceName
			});
			List<NKMShadowBattleTemplet> battleTemplets = NKMShadowPalaceManager.GetBattleTemplets(this.m_palaceID);
			this.m_lstDungeonData = nkmpalaceData.dungeonDataList;
			this.m_lstDungeonData.Sort(delegate(NKMPalaceDungeonData a, NKMPalaceDungeonData b)
			{
				NKMShadowBattleTemplet nkmshadowBattleTemplet = battleTemplets.Find((NKMShadowBattleTemplet v) => v.DUNGEON_ID == a.dungeonId);
				NKMShadowBattleTemplet nkmshadowBattleTemplet2 = battleTemplets.Find((NKMShadowBattleTemplet v) => v.DUNGEON_ID == b.dungeonId);
				if (nkmshadowBattleTemplet == null)
				{
					return 1;
				}
				if (nkmshadowBattleTemplet2 == null)
				{
					return -1;
				}
				return nkmshadowBattleTemplet.BATTLE_ORDER.CompareTo(nkmshadowBattleTemplet2.BATTLE_ORDER);
			});
			this.m_scrollRect.TotalCount = this.m_lstDungeonData.Count;
			this.m_scrollRect.RefreshCells(false);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_lstDungeonData.Count; i++)
			{
				num += this.m_lstDungeonData[i].recentTime;
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)num);
			NKCUtil.SetLabelText(this.m_txtTotalTime, NKCUtilString.GetTimeSpanString(timeSpan));
			string msg = "-:--:--";
			if (bestTime.Count == this.m_lstDungeonData.Count)
			{
				for (int j = 0; j < bestTime.Count; j++)
				{
					num2 += bestTime[j];
				}
				if (num2 > 0)
				{
					msg = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds((double)num2));
				}
			}
			NKCUtil.SetLabelText(this.m_txtBestTIme, msg);
			NKCUtil.SetGameobjectActive(this.m_objNewRecord, shadowResult.newRecord);
			base.UIOpened(true);
			this.m_animator.Play("NKM_UI_SHADOW_PALACE_RECORD_POPUP_INTRO");
		}

		// Token: 0x06007722 RID: 30498 RVA: 0x0027A2D1 File Offset: 0x002784D1
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.dOnClose != null)
			{
				this.dOnClose();
			}
		}

		// Token: 0x06007723 RID: 30499 RVA: 0x0027A2F4 File Offset: 0x002784F4
		private RectTransform OnGetObject(int index)
		{
			if (this.m_stkSlotPool.Count > 0)
			{
				return this.m_stkSlotPool.Pop().GetComponent<RectTransform>();
			}
			NKCPopupShadowRecordSlot nkcpopupShadowRecordSlot = UnityEngine.Object.Instantiate<NKCPopupShadowRecordSlot>(this.m_slotPrefab);
			nkcpopupShadowRecordSlot.transform.SetParent(this.m_scrollRect.content);
			return nkcpopupShadowRecordSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007724 RID: 30500 RVA: 0x0027A348 File Offset: 0x00278548
		private void OnProvideData(Transform tr, int idx)
		{
			NKCPopupShadowRecordSlot component = tr.GetComponent<NKCPopupShadowRecordSlot>();
			if (component == null)
			{
				return;
			}
			NKMPalaceDungeonData dungeonData = this.m_lstDungeonData[idx];
			NKMShadowBattleTemplet templet = NKMShadowPalaceManager.GetBattleTemplets(this.m_palaceID).Find((NKMShadowBattleTemplet v) => v.DUNGEON_ID == dungeonData.dungeonId);
			component.SetData(templet, dungeonData);
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x0027A3A8 File Offset: 0x002785A8
		private void OnReturnObject(Transform go)
		{
			if (base.GetComponent<NKCPopupShadowRecordSlot>() != null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(base.transform);
			this.m_stkSlotPool.Push(go.GetComponent<NKCPopupShadowRecordSlot>());
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x0027A3DD File Offset: 0x002785DD
		private void OnTouchBG(BaseEventData eventData)
		{
			base.Close();
		}

		// Token: 0x040063A7 RID: 25511
		private const string BUNDLE_NAME = "AB_UI_OPERATION_SHADOW";

		// Token: 0x040063A8 RID: 25512
		private const string ASSET_NAME = "AB_UI_OPERATION_SHADOW_PALACE_RECORD_POPUP";

		// Token: 0x040063A9 RID: 25513
		private static NKCPopupShadowRecord m_Instance;

		// Token: 0x040063AA RID: 25514
		public Text m_txtTitle;

		// Token: 0x040063AB RID: 25515
		public Text m_txtTotalTime;

		// Token: 0x040063AC RID: 25516
		public Text m_txtBestTIme;

		// Token: 0x040063AD RID: 25517
		public GameObject m_objNewRecord;

		// Token: 0x040063AE RID: 25518
		public LoopScrollRect m_scrollRect;

		// Token: 0x040063AF RID: 25519
		public NKCPopupShadowRecordSlot m_slotPrefab;

		// Token: 0x040063B0 RID: 25520
		public Animator m_animator;

		// Token: 0x040063B1 RID: 25521
		public EventTrigger m_eventTrigger;

		// Token: 0x040063B2 RID: 25522
		private Stack<NKCPopupShadowRecordSlot> m_stkSlotPool = new Stack<NKCPopupShadowRecordSlot>();

		// Token: 0x040063B3 RID: 25523
		private List<NKMPalaceDungeonData> m_lstDungeonData = new List<NKMPalaceDungeonData>();

		// Token: 0x040063B4 RID: 25524
		private int m_palaceID;

		// Token: 0x040063B5 RID: 25525
		private NKCPopupShadowRecord.OnClose dOnClose;

		// Token: 0x020017DF RID: 6111
		// (Invoke) Token: 0x0600B470 RID: 46192
		public delegate void OnClose();
	}
}

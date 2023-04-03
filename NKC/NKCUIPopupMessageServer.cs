using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A2D RID: 2605
	public class NKCUIPopupMessageServer : NKCUIBase
	{
		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06007216 RID: 29206 RVA: 0x0025EF4E File Offset: 0x0025D14E
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06007217 RID: 29207 RVA: 0x0025EF54 File Offset: 0x0025D154
		public static NKCUIPopupMessageServer Instance
		{
			get
			{
				if (NKCUIPopupMessageServer.m_Instance == null)
				{
					NKCUIPopupMessageServer.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupMessageServer>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_MESSAGE_SERVER", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupMessageServer.CleanupInstance)).GetInstance<NKCUIPopupMessageServer>();
					NKCUIPopupMessageServer.m_Instance.Init();
					NKCUIPopupMessageServer.m_Instance.transform.SetParent(NKCUIManager.rectFrontCanvas, false);
				}
				return NKCUIPopupMessageServer.m_Instance;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06007218 RID: 29208 RVA: 0x0025EFB8 File Offset: 0x0025D1B8
		public static bool HasInstance
		{
			get
			{
				return NKCUIPopupMessageServer.m_Instance != null;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06007219 RID: 29209 RVA: 0x0025EFC5 File Offset: 0x0025D1C5
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupMessageServer.m_Instance != null && NKCUIPopupMessageServer.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x0025EFE0 File Offset: 0x0025D1E0
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupMessageServer.m_Instance != null && NKCUIPopupMessageServer.m_Instance.IsOpen)
			{
				NKCUIPopupMessageServer.m_Instance.Close();
			}
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x0025F005 File Offset: 0x0025D205
		private static void CleanupInstance()
		{
			NKCUIPopupMessageServer.m_Instance = null;
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x0600721C RID: 29212 RVA: 0x0025F00D File Offset: 0x0025D20D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x0600721D RID: 29213 RVA: 0x0025F010 File Offset: 0x0025D210
		public override string MenuName
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x0600721E RID: 29214 RVA: 0x0025F018 File Offset: 0x0025D218
		public void Init()
		{
			if (this.m_NKM_UI_POPUP_MESSAGE_BUTTON_CLOSE != null)
			{
				this.m_NKM_UI_POPUP_MESSAGE_BUTTON_CLOSE.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_POPUP_MESSAGE_BUTTON_CLOSE.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_NKM_UI_POPUP_MESSAGE_TEXT != null)
			{
				this.m_textOriginPos = this.m_NKM_UI_POPUP_MESSAGE_TEXT.rectTransform.localPosition;
			}
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x0025F083 File Offset: 0x0025D283
		public void Open(NKCUIPopupMessageServer.eMessageStyle style, string message, int loopCnt = 1)
		{
			if (this.m_bPlaying)
			{
				this.m_MessageQueue.Enqueue(new NKCUIPopupMessageServer.MsgData(style, message, loopCnt));
				return;
			}
			this.m_bPlaying = true;
			this.SetData(style, message, loopCnt);
			if (!base.IsOpen)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x0025F0C0 File Offset: 0x0025D2C0
		private void SetData(NKCUIPopupMessageServer.eMessageStyle style, string message, int loopCnt)
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_MESSAGE_TEXT, message);
			base.StopAllCoroutines();
			this.m_fMaskWidth = this.m_NKM_UI_POPUP_MESSAGE_TEXT.preferredWidth;
			this.m_iLoopCnt = loopCnt;
			this.SetStartPosition(style);
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x0025F0F4 File Offset: 0x0025D2F4
		private void SetStartPosition(NKCUIPopupMessageServer.eMessageStyle style)
		{
			this.m_textPos = this.m_textOriginPos;
			if (style == NKCUIPopupMessageServer.eMessageStyle.Slide)
			{
				this.m_iWidthOffset = (float)Screen.width * 0.6f;
				this.m_textPos.x = this.m_textPos.x + this.m_iWidthOffset;
			}
			this.m_NKM_UI_POPUP_MESSAGE_TEXT.rectTransform.localPosition = this.m_textPos;
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x0025F150 File Offset: 0x0025D350
		private void Update()
		{
			if (this.m_iLoopCnt > 0)
			{
				this.m_textPos.x = this.m_textPos.x - this.m_fMoveSpeed;
				this.m_NKM_UI_POPUP_MESSAGE_TEXT.rectTransform.localPosition = this.m_textPos;
				if (this.m_textPos.x < this.m_iWidthOffset && Mathf.Abs(this.m_textPos.x) >= this.m_iWidthOffset)
				{
					this.m_textPos.x = this.m_textOriginPos.x + this.m_iWidthOffset;
					this.m_iLoopCnt--;
					return;
				}
			}
			else if (this.m_iLoopCnt == 0 && !this.StartNextMessage())
			{
				base.Close();
			}
		}

		// Token: 0x06007223 RID: 29219 RVA: 0x0025F204 File Offset: 0x0025D404
		private bool StartNextMessage()
		{
			if (this.m_MessageQueue.Count == 0)
			{
				return false;
			}
			NKCUIPopupMessageServer.MsgData msgData = this.m_MessageQueue.Dequeue();
			this.SetData(msgData.style, msgData.msg, msgData.loopCnt);
			return true;
		}

		// Token: 0x06007224 RID: 29220 RVA: 0x0025F248 File Offset: 0x0025D448
		public override void CloseInternal()
		{
			if (!this.StartNextMessage())
			{
				this.m_bPlaying = false;
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x04005E02 RID: 24066
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04005E03 RID: 24067
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_MESSAGE_SERVER";

		// Token: 0x04005E04 RID: 24068
		private static NKCUIPopupMessageServer m_Instance;

		// Token: 0x04005E05 RID: 24069
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04005E06 RID: 24070
		public NKCUIComStateButton m_NKM_UI_POPUP_MESSAGE_BUTTON_CLOSE;

		// Token: 0x04005E07 RID: 24071
		public Image m_NKM_UI_POPUP_MESSAGE_BG2;

		// Token: 0x04005E08 RID: 24072
		public Text m_NKM_UI_POPUP_MESSAGE_TEXT;

		// Token: 0x04005E09 RID: 24073
		public float m_fMoveSpeed = 1f;

		// Token: 0x04005E0A RID: 24074
		private Vector3 m_textOriginPos = Vector3.zero;

		// Token: 0x04005E0B RID: 24075
		private Vector3 m_textPos = Vector3.zero;

		// Token: 0x04005E0C RID: 24076
		private float m_fMaskWidth;

		// Token: 0x04005E0D RID: 24077
		private int m_iLoopCnt;

		// Token: 0x04005E0E RID: 24078
		private bool m_bPlaying;

		// Token: 0x04005E0F RID: 24079
		private Queue<NKCUIPopupMessageServer.MsgData> m_MessageQueue = new Queue<NKCUIPopupMessageServer.MsgData>();

		// Token: 0x04005E10 RID: 24080
		private float m_iWidthOffset;

		// Token: 0x02001772 RID: 6002
		public enum eMessageStyle
		{
			// Token: 0x0400A6E7 RID: 42727
			Slide,
			// Token: 0x0400A6E8 RID: 42728
			FadeInOut,
			// Token: 0x0400A6E9 RID: 42729
			Typing
		}

		// Token: 0x02001773 RID: 6003
		private struct MsgData
		{
			// Token: 0x0600B34F RID: 45903 RVA: 0x003637B5 File Offset: 0x003619B5
			public MsgData(NKCUIPopupMessageServer.eMessageStyle newStyle, string newMsg, int cnt)
			{
				this.style = newStyle;
				this.msg = newMsg;
				this.loopCnt = cnt;
			}

			// Token: 0x1700194D RID: 6477
			// (get) Token: 0x0600B350 RID: 45904 RVA: 0x003637CC File Offset: 0x003619CC
			// (set) Token: 0x0600B351 RID: 45905 RVA: 0x003637D4 File Offset: 0x003619D4
			public NKCUIPopupMessageServer.eMessageStyle style { readonly get; private set; }

			// Token: 0x1700194E RID: 6478
			// (get) Token: 0x0600B352 RID: 45906 RVA: 0x003637DD File Offset: 0x003619DD
			// (set) Token: 0x0600B353 RID: 45907 RVA: 0x003637E5 File Offset: 0x003619E5
			public string msg { readonly get; private set; }

			// Token: 0x1700194F RID: 6479
			// (get) Token: 0x0600B354 RID: 45908 RVA: 0x003637EE File Offset: 0x003619EE
			// (set) Token: 0x0600B355 RID: 45909 RVA: 0x003637F6 File Offset: 0x003619F6
			public int loopCnt { readonly get; private set; }
		}
	}
}

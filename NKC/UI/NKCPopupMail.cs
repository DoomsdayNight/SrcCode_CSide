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
	// Token: 0x02000A6B RID: 2667
	public class NKCPopupMail : NKCUIBase
	{
		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x0600759F RID: 30111 RVA: 0x00272200 File Offset: 0x00270400
		public static NKCPopupMail Instance
		{
			get
			{
				if (NKCPopupMail.m_Instance == null)
				{
					NKCPopupMail.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupMail>("ab_ui_nkm_ui_mail", "NKM_UI_POPUP_MAIL", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupMail.CleanupInstance)).GetInstance<NKCPopupMail>();
					NKCPopupMail.m_Instance.InitUI();
				}
				return NKCPopupMail.m_Instance;
			}
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x0027224F File Offset: 0x0027044F
		private static void CleanupInstance()
		{
			NKCPopupMail.m_Instance = null;
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x060075A1 RID: 30113 RVA: 0x00272257 File Offset: 0x00270457
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x060075A2 RID: 30114 RVA: 0x0027225A File Offset: 0x0027045A
		public override string MenuName
		{
			get
			{
				return "MAIL CONTENT";
			}
		}

		// Token: 0x060075A3 RID: 30115 RVA: 0x00272264 File Offset: 0x00270464
		public void InitUI()
		{
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			foreach (NKCUISlot nkcuislot in this.m_lstSlot)
			{
				nkcuislot.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_cbtnReceive, new UnityAction(this.OnBtnReceive));
			NKCUtil.SetHotkey(this.m_cbtnReceive, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_cbtnClose, new UnityAction(this.OnBtnClose));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_etTextContent != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnPointerClick));
				this.m_etTextContent.triggers.Clear();
				this.m_etTextContent.triggers.Add(entry);
			}
		}

		// Token: 0x060075A4 RID: 30116 RVA: 0x00272368 File Offset: 0x00270568
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_PostData = null;
		}

		// Token: 0x060075A5 RID: 30117 RVA: 0x00272380 File Offset: 0x00270580
		public void Open(NKMPostData postData, NKCPopupMail.OnReceive onReceive)
		{
			this.m_PostData = postData;
			this.dOnReceive = onReceive;
			NKCUtil.SetLabelText(this.m_lbContent, NKCUtilString.GetFinalMailContents(postData.contents));
			NKCUtil.SetLabelText(this.m_lbDate, NKMTime.UTCtoLocal(postData.sendDate, 0).ToString("yyyy-MM-dd"));
			NKCUtil.SetLabelText(this.m_lbTimeLeft, NKCUtilString.GetRemainTimeString(postData.expirationDate, 2));
			this.SetSlot(postData.items);
			if (postData.expirationDate >= NKMConst.Post.UnlimitedExpirationUtcDate)
			{
				NKCUtil.SetLabelText(this.m_lbTimeLeft, NKCUtilString.GET_STRING_TIME_NO_LIMIT);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTimeLeft, NKCUtilString.GetRemainTimeString(postData.expirationDate, 2));
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060075A6 RID: 30118 RVA: 0x00272454 File Offset: 0x00270654
		private void OnPointerClick(BaseEventData beventData)
		{
			PointerEventData pointerEventData = (PointerEventData)beventData;
			string text = UITextUtilities.FindIntersectingWord(this.m_lbContent, pointerEventData.position, NKCCamera.GetSubUICamera());
			if (!string.IsNullOrEmpty(text) && UITextUtilities.hasLinkText(text))
			{
				string text2 = UITextUtilities.RemoveHtmlLikeTags(text);
				Debug.Log("Opening link: " + text2);
				Application.OpenURL(text2);
			}
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x002724B1 File Offset: 0x002706B1
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupMail.Instance == null)
			{
				return;
			}
			NKCPopupMail.Instance.Close();
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x002724CC File Offset: 0x002706CC
		private void SetSlot(List<NKMRewardInfo> lstPostItem)
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlot[i];
				if (i < lstPostItem.Count)
				{
					NKMRewardInfo nkmrewardInfo = lstPostItem[i];
					bool flag = NKCUIMailSlot.IsSlotVisible(nkmrewardInfo.rewardType, nkmrewardInfo.ID, nkmrewardInfo.Count);
					NKCUtil.SetGameobjectActive(nkcuislot, flag);
					if (flag)
					{
						nkcuislot.SetData(NKCUISlot.SlotData.MakePostItemData(nkmrewardInfo), false, null);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
		}

		// Token: 0x060075A9 RID: 30121 RVA: 0x00272546 File Offset: 0x00270746
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x060075AA RID: 30122 RVA: 0x0027255B File Offset: 0x0027075B
		private void OnBtnReceive()
		{
			if (this.dOnReceive != null)
			{
				this.dOnReceive((this.m_PostData != null) ? this.m_PostData.postIndex : -1L);
			}
		}

		// Token: 0x060075AB RID: 30123 RVA: 0x00272587 File Offset: 0x00270787
		private void OnBtnClose()
		{
			base.Close();
		}

		// Token: 0x04006207 RID: 25095
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_mail";

		// Token: 0x04006208 RID: 25096
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_MAIL";

		// Token: 0x04006209 RID: 25097
		private static NKCPopupMail m_Instance;

		// Token: 0x0400620A RID: 25098
		public Text m_lbContent;

		// Token: 0x0400620B RID: 25099
		public Text m_lbDate;

		// Token: 0x0400620C RID: 25100
		public GameObject m_objTimeLeft;

		// Token: 0x0400620D RID: 25101
		public Text m_lbTimeLeft;

		// Token: 0x0400620E RID: 25102
		public EventTrigger m_etTextContent;

		// Token: 0x0400620F RID: 25103
		[Header("아이템 슬롯")]
		public List<NKCUISlot> m_lstSlot;

		// Token: 0x04006210 RID: 25104
		[Header("하단 버튼")]
		public NKCUIComStateButton m_cbtnReceive;

		// Token: 0x04006211 RID: 25105
		public NKCUIComStateButton m_cbtnClose;

		// Token: 0x04006212 RID: 25106
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006213 RID: 25107
		private NKMPostData m_PostData;

		// Token: 0x04006214 RID: 25108
		private NKCPopupMail.OnReceive dOnReceive;

		// Token: 0x020017CD RID: 6093
		// (Invoke) Token: 0x0600B440 RID: 46144
		public delegate void OnReceive(long index);
	}
}

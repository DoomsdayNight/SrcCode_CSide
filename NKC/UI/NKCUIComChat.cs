using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Core.Util;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000923 RID: 2339
	public class NKCUIComChat : MonoBehaviour
	{
		// Token: 0x06005DAD RID: 23981 RVA: 0x001CE8EC File Offset: 0x001CCAEC
		public void InitUI(NKCUIComChat.OnSendMessage dOnSendMessage, NKCUIComChat.OnClose dOnClose, bool bDisableTranslate)
		{
			this.m_bEmotionInitComplete = false;
			this.m_dOnSendMessage = dOnSendMessage;
			this.m_dOnClose = dOnClose;
			this.m_bDisableTranslate = bDisableTranslate;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loopScroll.dOnGetObject += this.GetObject;
			this.m_loopScroll.dOnReturnObject += this.ReturnObject;
			this.m_loopScroll.dOnProvideData += this.ProvideData;
			if (this.m_loopEmoticon != null)
			{
				this.m_loopEmoticon.dOnGetObject += this.GetObjectEmoticon;
				this.m_loopEmoticon.dOnReturnObject += this.ReturnObjectEmoticon;
				this.m_loopEmoticon.dOnProvideData += this.ProvideDataEmoticon;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_btnEmotion != null)
			{
				this.m_btnEmotion.PointerClick.RemoveAllListeners();
				this.m_btnEmotion.PointerClick.AddListener(new UnityAction(this.OnClickEmotion));
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Select;
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnSelectIF));
			this.m_IFMessage.GetComponent<EventTrigger>().triggers.Add(entry);
			this.m_IFMessage.onEndEdit.RemoveAllListeners();
			this.m_IFMessage.onEndEdit.AddListener(new UnityAction<string>(this.OnMessageChanged));
			this.m_btnSend.PointerClick.RemoveAllListeners();
			this.m_btnSend.PointerClick.AddListener(new UnityAction(this.OnClickSend));
			if (this.m_btnInfo != null)
			{
				this.m_btnInfo.PointerClick.RemoveAllListeners();
				this.m_btnInfo.PointerClick.AddListener(new UnityAction(this.OnInfo));
			}
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(this.Close));
			}
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x001CEB08 File Offset: 0x001CCD08
		public void Close()
		{
			if (this.m_lstChatMessages.Count > 0)
			{
				NKCChatManager.SetLastCheckedMeesageUid(this.m_CurChannelUid, this.m_lstChatMessages[this.m_lstChatMessages.Count - 1].messageUid);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUIComChat.OnClose dOnClose = this.m_dOnClose;
			if (dOnClose == null)
			{
				return;
			}
			dOnClose();
		}

		// Token: 0x06005DAF RID: 23983 RVA: 0x001CEB68 File Offset: 0x001CCD68
		public void SetData(long defaultChannel = 0L, bool bEnableMute = false, string title = "")
		{
			this.m_IFMessage.text = "";
			this.m_Message = "";
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_CurChannelUid = defaultChannel;
			this.m_bEnableMute = bEnableMute;
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			if (this.m_aniBottom != null)
			{
				this.m_aniBottom.Play("CONSORTIUM_CHAT_Bottom_CLOSE_IDLE");
			}
			NKCUtil.SetGameobjectActive(this.m_objEmoticonSet, false);
			this.m_lstChatMessages = NKCChatManager.GetChatList(defaultChannel, out this.m_bWaitForData);
			if (this.m_bEnableMute)
			{
				this.CheckMute();
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objMute, false);
			}
			NKCUIComStateButton btnEmotion = this.m_btnEmotion;
			if (btnEmotion != null)
			{
				btnEmotion.UnLock(false);
			}
			base.StartCoroutine(this.WaitForData());
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x001CEC30 File Offset: 0x001CCE30
		private RectTransform GetObject(int index)
		{
			NKCPopupChatSlot nkcpopupChatSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupChatSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupChatSlot = UnityEngine.Object.Instantiate<NKCPopupChatSlot>(this.m_ChatSlot, this.m_trContentParent);
			}
			this.m_lstVisibleSlot.Add(nkcpopupChatSlot);
			return nkcpopupChatSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x001CEC80 File Offset: 0x001CCE80
		private void ReturnObject(Transform tr)
		{
			NKCPopupChatSlot component = tr.GetComponent<NKCPopupChatSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			this.m_lstVisibleSlot.Remove(component);
			this.m_stkSlot.Push(component);
			component.transform.SetParent(this.m_trObjPool);
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x001CECC8 File Offset: 0x001CCEC8
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupChatSlot component = tr.GetComponent<NKCPopupChatSlot>();
			if (idx < 0 || this.m_lstChatMessages.Count <= idx || this.m_lstChatMessages[idx].message == null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_CurChannelUid, this.m_lstChatMessages[idx], this.m_bDisableTranslate);
			LayoutRebuilder.ForceRebuildLayoutImmediate(component.GetComponent<RectTransform>());
			if (NKCChatManager.GetLastCheckedMessageUid(this.m_CurChannelUid) <= 0L || this.m_lstChatMessages.FindIndex((NKMChatMessageData x) => x.messageUid == NKCChatManager.GetLastCheckedMessageUid(this.m_CurChannelUid)) < idx)
			{
				component.PlaySDAni();
			}
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x001CED68 File Offset: 0x001CCF68
		private RectTransform GetObjectEmoticon(int idx)
		{
			NKCPopupEmoticonSlotSD nkcpopupEmoticonSlotSD;
			if (this.m_stkEmoticonSlot.Count > 0)
			{
				nkcpopupEmoticonSlotSD = this.m_stkEmoticonSlot.Pop();
			}
			else
			{
				nkcpopupEmoticonSlotSD = UnityEngine.Object.Instantiate<NKCPopupEmoticonSlotSD>(this.m_pfbEmoticon);
			}
			nkcpopupEmoticonSlotSD.transform.SetParent(this.m_trEmoticonParent);
			return nkcpopupEmoticonSlotSD.GetComponent<RectTransform>();
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x001CEDB8 File Offset: 0x001CCFB8
		private void ReturnObjectEmoticon(Transform tr)
		{
			NKCPopupEmoticonSlotSD component = tr.GetComponent<NKCPopupEmoticonSlotSD>();
			NKCUtil.SetGameobjectActive(tr, false);
			component.transform.SetParent(base.transform);
			this.m_stkEmoticonSlot.Push(component);
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x001CEDF0 File Offset: 0x001CCFF0
		private void ProvideDataEmoticon(Transform tr, int idx)
		{
			NKCPopupEmoticonSlotSD component = tr.GetComponent<NKCPopupEmoticonSlotSD>();
			component.StopSDAni();
			component.SetClickEvent(new NKCUISlot.OnClick(this.OnClickEmoticonSlot));
			component.SetClickEventForChange(null);
			component.SetUI(this.m_lstEmoticon[idx]);
			component.SetSelectedWithChangeButton(false);
			component.RemoveCanvas();
			component.transform.localScale = new Vector3(1f, 1f);
		}

		// Token: 0x06005DB6 RID: 23990 RVA: 0x001CEE5C File Offset: 0x001CD05C
		private void SetMuteRemainTime()
		{
			if (NKCChatManager.m_MuteEndDate > ServiceTime.Recent)
			{
				NKCUtil.SetLabelText(this.m_lbMuteRemainTime, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_CHAT_INFORMATION_SANCTION_DESC, NKCUtilString.GetRemainTimeString(NKCChatManager.m_MuteEndDate - ServiceTime.Recent, 2, true)));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objMute, false);
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x001CEEB2 File Offset: 0x001CD0B2
		private IEnumerator WaitForData()
		{
			while (this.m_bWaitForData)
			{
				yield return null;
			}
			this.m_loopScroll.TotalCount = this.m_lstChatMessages.Count;
			this.m_loopScroll.RefillCellsFromEnd(0);
			if (!NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				NKCPacketSender.Send_NKMPacket_EMOTICON_DATA_REQ(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID);
			}
			while (!NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				yield return null;
			}
			if (this.m_lstChatMessages.Count > 0)
			{
				NKCChatManager.SetLastCheckedMeesageUid(this.m_CurChannelUid, this.m_lstChatMessages[this.m_lstChatMessages.Count - 1].messageUid);
			}
			yield break;
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x001CEEC4 File Offset: 0x001CD0C4
		public void AddMessage(NKMChatMessageData data, bool bIsMyMessage, bool bForceResetScroll = false)
		{
			if (bIsMyMessage || data.commonProfile.userUid == this.m_CurChannelUid)
			{
				this.RefreshList(bForceResetScroll || (data.commonProfile != null && data.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID));
			}
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x001CEF18 File Offset: 0x001CD118
		public void RefreshList(bool bResetPosition = false)
		{
			bool flag;
			this.m_lstChatMessages = NKCChatManager.GetChatList(this.m_CurChannelUid, out flag);
			if (this.m_lstChatMessages == null)
			{
				this.m_lstChatMessages = new List<NKMChatMessageData>();
			}
			this.m_loopScroll.TotalCount = this.m_lstChatMessages.Count;
			if (bResetPosition)
			{
				this.m_loopScroll.RefillCellsFromEnd(0);
				this.m_loopScroll.StopMovement();
				if (this.m_lstChatMessages.Count > 0)
				{
					NKCChatManager.SetLastCheckedMeesageUid(this.m_CurChannelUid, this.m_lstChatMessages[this.m_lstChatMessages.Count - 1].messageUid);
					return;
				}
			}
			else
			{
				this.m_loopScroll.RefreshCells(false);
			}
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x001CEFBE File Offset: 0x001CD1BE
		public void OnChatDataReceived(long channelUid, List<NKMChatMessageData> lstData, bool bRefresh = false)
		{
			if (this.m_CurChannelUid != channelUid)
			{
				this.m_bWaitForData = false;
				return;
			}
			this.m_lstChatMessages = lstData;
			this.m_bWaitForData = false;
			if (bRefresh)
			{
				this.RefreshList(false);
			}
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x001CEFE9 File Offset: 0x001CD1E9
		private void OnSelectIF(BaseEventData cBaseEventData)
		{
			if (this.m_objEmoticonSet.activeSelf)
			{
				base.StartCoroutine(this.OnEmoticonEnable(false));
				this.m_SelectedEmoticonID = 0;
			}
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x001CF010 File Offset: 0x001CD210
		private void OnMessageChanged(string str)
		{
			if (str.Length > 70)
			{
				str = str.Substring(0, 70);
			}
			this.m_Message = NKCFilterManager.CheckBadChat(str);
			this.m_IFMessage.text = this.m_Message;
			if (!string.IsNullOrWhiteSpace(this.m_Message) && NKCInputManager.IsChatSubmitEnter())
			{
				this.OnClickSend();
				this.m_IFMessage.ActivateInputField();
			}
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x001CF074 File Offset: 0x001CD274
		private void OnClickEmotion()
		{
			base.StartCoroutine(this.OnEmoticonEnable(!this.m_objEmoticonSet.activeSelf));
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x001CF094 File Offset: 0x001CD294
		private void OnClickEmoticonSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.m_SelectedEmoticonID = slotData.ID;
			NKCPopupEmoticonSlotSD[] componentsInChildren = this.m_trEmoticonParent.GetComponentsInChildren<NKCPopupEmoticonSlotSD>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].GetEmoticonID() == this.m_SelectedEmoticonID)
				{
					componentsInChildren[i].PlaySDAni();
					componentsInChildren[i].transform.localScale = new Vector3(1.1f, 1.1f);
				}
				else
				{
					componentsInChildren[i].StopSDAni();
					componentsInChildren[i].transform.localScale = new Vector3(1f, 1f);
				}
			}
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x001CF121 File Offset: 0x001CD321
		private IEnumerator OnEmoticonEnable(bool bEnabled)
		{
			NKCUIComStateButton btnEmotion = this.m_btnEmotion;
			if (btnEmotion != null)
			{
				btnEmotion.Lock(false);
			}
			this.m_SelectedEmoticonID = 0;
			if (this.m_objEmoticonSet.activeSelf)
			{
				this.m_aniBottom.Play("CONSORTIUM_CHAT_Bottom_CLOSE");
				yield return new WaitForSeconds(0.4f);
				NKCUtil.SetGameobjectActive(this.m_objEmoticonSet, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objEmoticonSet, true);
				this.m_lstEmoticon = NKCChatManager.GetEmoticons();
				this.m_loopEmoticon.TotalCount = this.m_lstEmoticon.Count;
				this.m_loopEmoticon.SetAutoResize(6, false);
				if (!this.m_bEmotionInitComplete)
				{
					this.m_loopEmoticon.PrepareCells(0);
					this.m_bEmotionInitComplete = true;
				}
				this.m_loopEmoticon.RefreshCells(false);
				yield return new WaitForSeconds(0.4f);
				this.m_aniBottom.Play("CONSORTIUM_CHAT_Bottom_OPEN");
			}
			NKCUIComStateButton btnEmotion2 = this.m_btnEmotion;
			if (btnEmotion2 != null)
			{
				btnEmotion2.UnLock(false);
			}
			yield break;
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x001CF130 File Offset: 0x001CD330
		private void OnClickSend()
		{
			if (string.IsNullOrWhiteSpace(this.m_Message) && this.m_SelectedEmoticonID == 0)
			{
				return;
			}
			if (!string.IsNullOrWhiteSpace(this.m_Message))
			{
				NKCUIComChat.OnSendMessage dOnSendMessage = this.m_dOnSendMessage;
				if (dOnSendMessage != null)
				{
					dOnSendMessage(this.m_CurChannelUid, ChatMessageType.Normal, this.m_Message, 0);
				}
			}
			else
			{
				NKCUIComChat.OnSendMessage dOnSendMessage2 = this.m_dOnSendMessage;
				if (dOnSendMessage2 != null)
				{
					dOnSendMessage2(this.m_CurChannelUid, ChatMessageType.Normal, this.m_Message, this.m_SelectedEmoticonID);
				}
			}
			this.m_IFMessage.text = "";
			this.m_Message = "";
			if (this.m_objEmoticonSet.activeSelf)
			{
				base.StartCoroutine(this.OnEmoticonEnable(false));
			}
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x001CF1DB File Offset: 0x001CD3DB
		private void OnInfo()
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_CurChannelUid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x001CF1EC File Offset: 0x001CD3EC
		private void Update()
		{
			if (this.m_bEnableMute && this.m_objMute.activeSelf)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetMuteRemainTime();
				}
			}
			if (!Input.GetKeyDown(KeyCode.Return))
			{
				Input.GetKeyDown(KeyCode.KeypadEnter);
			}
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x001CF259 File Offset: 0x001CD459
		public void CheckMute()
		{
			this.m_fDeltaTime = 0f;
			NKCUtil.SetGameobjectActive(this.m_objMute, NKCChatManager.m_MuteEndDate > ServiceTime.Recent);
			if (this.m_objMute.activeSelf)
			{
				this.SetMuteRemainTime();
			}
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x001CF293 File Offset: 0x001CD493
		public long GetChannelUid()
		{
			return this.m_CurChannelUid;
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x001CF29B File Offset: 0x001CD49B
		public void OnGuildDataChanged()
		{
			if (NKCGuildManager.HasGuild() && !NKCGuildManager.IsGuildMemberByUID(this.m_CurChannelUid))
			{
				this.Close();
			}
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x001CF2B7 File Offset: 0x001CD4B7
		public void OnScreenResolutionChanged()
		{
			this.m_bEmotionInitComplete = false;
		}

		// Token: 0x040049E3 RID: 18915
		[Header("상단")]
		public Text m_lbTitle;

		// Token: 0x040049E4 RID: 18916
		public NKCUIComStateButton m_btnInfo;

		// Token: 0x040049E5 RID: 18917
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040049E6 RID: 18918
		[Header("프리팹")]
		public NKCPopupChatSlot m_ChatSlot;

		// Token: 0x040049E7 RID: 18919
		public NKCPopupEmoticonSlotSD m_pfbEmoticon;

		// Token: 0x040049E8 RID: 18920
		[Header("채팅 루프")]
		public LoopScrollFlexibleRect m_loopScroll;

		// Token: 0x040049E9 RID: 18921
		public Transform m_trContentParent;

		// Token: 0x040049EA RID: 18922
		public Transform m_trObjPool;

		// Token: 0x040049EB RID: 18923
		[Header("하단 입력부")]
		public NKCUIComStateButton m_btnEmotion;

		// Token: 0x040049EC RID: 18924
		public InputField m_IFMessage;

		// Token: 0x040049ED RID: 18925
		public NKCUIComStateButton m_btnSend;

		// Token: 0x040049EE RID: 18926
		[Header("이모티콘")]
		public Animator m_aniBottom;

		// Token: 0x040049EF RID: 18927
		public GameObject m_objEmoticonSet;

		// Token: 0x040049F0 RID: 18928
		public LoopScrollRect m_loopEmoticon;

		// Token: 0x040049F1 RID: 18929
		public Transform m_trEmoticonParent;

		// Token: 0x040049F2 RID: 18930
		[Header("채팅 차단 표시")]
		public GameObject m_objMute;

		// Token: 0x040049F3 RID: 18931
		public Text m_lbMuteRemainTime;

		// Token: 0x040049F4 RID: 18932
		private NKCUIComChat.OnClose m_dOnClose;

		// Token: 0x040049F5 RID: 18933
		private NKCUIComChat.OnSendMessage m_dOnSendMessage;

		// Token: 0x040049F6 RID: 18934
		private Stack<NKCPopupChatSlot> m_stkSlot = new Stack<NKCPopupChatSlot>();

		// Token: 0x040049F7 RID: 18935
		private List<NKCPopupChatSlot> m_lstVisibleSlot = new List<NKCPopupChatSlot>();

		// Token: 0x040049F8 RID: 18936
		private Stack<NKCPopupEmoticonSlotSD> m_stkEmoticonSlot = new Stack<NKCPopupEmoticonSlotSD>();

		// Token: 0x040049F9 RID: 18937
		private List<NKMChatMessageData> m_lstChatMessages = new List<NKMChatMessageData>();

		// Token: 0x040049FA RID: 18938
		private List<int> m_lstEmoticon = new List<int>();

		// Token: 0x040049FB RID: 18939
		private string m_Message = "";

		// Token: 0x040049FC RID: 18940
		private long m_CurChannelUid;

		// Token: 0x040049FD RID: 18941
		private bool m_bWaitForData = true;

		// Token: 0x040049FE RID: 18942
		private int m_SelectedEmoticonID;

		// Token: 0x040049FF RID: 18943
		private float m_fDeltaTime;

		// Token: 0x04004A00 RID: 18944
		private bool m_bEnableMute;

		// Token: 0x04004A01 RID: 18945
		private bool m_bEmotionInitComplete;

		// Token: 0x04004A02 RID: 18946
		private bool m_bDisableTranslate;

		// Token: 0x020015B2 RID: 5554
		// (Invoke) Token: 0x0600ADEE RID: 44526
		public delegate void OnClose();

		// Token: 0x020015B3 RID: 5555
		// (Invoke) Token: 0x0600ADF2 RID: 44530
		public delegate void OnSendMessage(long channelUid, ChatMessageType messageType, string message, int emotionId);
	}
}

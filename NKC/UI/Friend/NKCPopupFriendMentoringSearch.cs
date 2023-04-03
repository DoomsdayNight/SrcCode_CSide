using System;
using System.Collections.Generic;
using ClientPacket.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B1A RID: 2842
	public class NKCPopupFriendMentoringSearch : NKCUIBase
	{
		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x0600816A RID: 33130 RVA: 0x002BA1E4 File Offset: 0x002B83E4
		public static NKCPopupFriendMentoringSearch Instance
		{
			get
			{
				if (NKCPopupFriendMentoringSearch.m_Instance == null)
				{
					NKCPopupFriendMentoringSearch.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFriendMentoringSearch>("ab_ui_nkm_ui_friend", "NKM_UI_POPUP_FRIEND_MENTORING_SEARCH", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFriendMentoringSearch.CleanupInstance)).GetInstance<NKCPopupFriendMentoringSearch>();
					NKCPopupFriendMentoringSearch.m_Instance.Init();
				}
				return NKCPopupFriendMentoringSearch.m_Instance;
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x0600816B RID: 33131 RVA: 0x002BA233 File Offset: 0x002B8433
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupFriendMentoringSearch.m_Instance != null && NKCPopupFriendMentoringSearch.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x002BA24E File Offset: 0x002B844E
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFriendMentoringSearch.m_Instance != null && NKCPopupFriendMentoringSearch.m_Instance.IsOpen)
			{
				NKCPopupFriendMentoringSearch.m_Instance.Close();
			}
		}

		// Token: 0x0600816D RID: 33133 RVA: 0x002BA273 File Offset: 0x002B8473
		private static void CleanupInstance()
		{
			NKCPopupFriendMentoringSearch.m_Instance = null;
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x0600816E RID: 33134 RVA: 0x002BA27B File Offset: 0x002B847B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x0600816F RID: 33135 RVA: 0x002BA27E File Offset: 0x002B847E
		public override string MenuName
		{
			get
			{
				return "NKCPopupFriendMentoringSearch";
			}
		}

		// Token: 0x06008170 RID: 33136 RVA: 0x002BA288 File Offset: 0x002B8488
		public void Init()
		{
			if (this.m_NKM_UI_POPUP_FRIEND_MENTORING_SEARCH_BG != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCPopupFriendMentoringSearch.CheckInstanceAndClose();
				});
				this.m_NKM_UI_POPUP_FRIEND_MENTORING_SEARCH_BG.triggers.Add(entry);
			}
			if (this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON != null)
			{
				this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickSearch));
			}
			if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH != null)
			{
				this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.AddListener(new UnityAction(this.OnClickMentorListRefresh));
			}
			if (this.m_NKM_UI_POPUP_CLOSE_BUTTON != null)
			{
				this.m_NKM_UI_POPUP_CLOSE_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_POPUP_CLOSE_BUTTON.PointerClick.AddListener(new UnityAction(NKCPopupFriendMentoringSearch.CheckInstanceAndClose));
			}
			if (this.m_MENTORING_INVITE_LIST_ScrollRect != null)
			{
				this.m_MENTORING_INVITE_LIST_ScrollRect.dOnGetObject += this.GetMentorSlot;
				this.m_MENTORING_INVITE_LIST_ScrollRect.dOnReturnObject += this.ReturnMentorSlot;
				this.m_MENTORING_INVITE_LIST_ScrollRect.dOnProvideData += this.ProvideData;
				this.m_MENTORING_INVITE_LIST_ScrollRect.PrepareCells(0);
			}
			if (this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT != null)
			{
				this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT.onEndEdit.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSearch));
			}
		}

		// Token: 0x06008171 RID: 33137 RVA: 0x002BA432 File Offset: 0x002B8632
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008172 RID: 33138 RVA: 0x002BA440 File Offset: 0x002B8640
		private void OnClickSearch()
		{
			if (this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT != null)
			{
				NKCPacketSender.Send_NKMPacket_MENTORING_SEARCH_LIST_REQ(MentoringIdentity.Mentee, this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT.text);
			}
		}

		// Token: 0x06008173 RID: 33139 RVA: 0x002BA461 File Offset: 0x002B8661
		private void OnClickMentorListRefresh()
		{
			NKCPacketSender.Send_kNKMPacket_MENTORING_RECEIVE_LIST_REQ(MentoringIdentity.Mentee, true);
		}

		// Token: 0x06008174 RID: 33140 RVA: 0x002BA46A File Offset: 0x002B866A
		public void Open()
		{
			this.UpdateData();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x06008175 RID: 33141 RVA: 0x002BA485 File Offset: 0x002B8685
		public void ResetUI()
		{
			this.UpdateData();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x002BA49C File Offset: 0x002B869C
		private void UpdateData()
		{
			this.m_lstMentor = NKCScenManager.CurrentUserData().MentoringData.lstRecommend;
			this.m_lstInvited = NKCScenManager.CurrentUserData().MentoringData.lstInvited;
			if (this.m_lstInvited != null && this.m_lstMentor != null)
			{
				int i;
				Predicate<FriendListData> <>9__0;
				int j;
				for (i = 0; i < this.m_lstMentor.Count; i = j)
				{
					if (this.m_lstMentor[i] != null)
					{
						List<FriendListData> lstInvited = this.m_lstInvited;
						Predicate<FriendListData> match;
						if ((match = <>9__0) == null)
						{
							match = (<>9__0 = ((FriendListData e) => e.commonProfile.userUid == this.m_lstMentor[i].commonProfile.userUid));
						}
						if (lstInvited.Find(match) != null)
						{
							this.m_lstMentor.Remove(this.m_lstMentor[i]);
							j = i - 1;
							i = j;
						}
					}
					j = i + 1;
				}
			}
			if (this.m_lstMentor != null)
			{
				int num = this.m_lstMentor.Count;
				if (this.m_lstInvited != null)
				{
					num += this.m_lstInvited.Count;
				}
				this.m_MENTORING_INVITE_LIST_ScrollRect.TotalCount = num;
				this.m_MENTORING_INVITE_LIST_ScrollRect.SetIndexPosition(0);
			}
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x002BA5D3 File Offset: 0x002B87D3
		private void OnEndEditSearch(string input)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON.m_bLock)
				{
					this.OnClickSearch();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x06008178 RID: 33144 RVA: 0x002BA5FC File Offset: 0x002B87FC
		private RectTransform GetMentorSlot(int index)
		{
			if (this.m_slotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_slotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUIMentoringSlot newInstance = NKCUIMentoringSlot.GetNewInstance(this.m_parentMentoringSlot);
			if (newInstance == null)
			{
				return null;
			}
			newInstance.transform.localScale = Vector3.one;
			this.m_slotList.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008179 RID: 33145 RVA: 0x002BA663 File Offset: 0x002B8863
		public void ReturnMentorSlot(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			this.m_slotPool.Push(tr.GetComponent<RectTransform>());
		}

		// Token: 0x0600817A RID: 33146 RVA: 0x002BA68C File Offset: 0x002B888C
		public void ProvideData(Transform tr, int index)
		{
			bool invited = false;
			FriendListData friendListData;
			if (this.m_lstInvited != null)
			{
				if (this.m_lstInvited.Count > 0 && this.m_lstInvited.Count > index)
				{
					friendListData = this.m_lstInvited[index];
					invited = true;
				}
				else
				{
					friendListData = this.m_lstMentor[index - this.m_lstInvited.Count];
				}
			}
			else
			{
				friendListData = this.m_lstMentor[index];
			}
			NKCUIMentoringSlot component = tr.GetComponent<NKCUIMentoringSlot>();
			if (component != null)
			{
				component.SetData(friendListData.commonProfile, friendListData.lastLoginDate.Ticks, invited);
			}
		}

		// Token: 0x04006D9C RID: 28060
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_friend";

		// Token: 0x04006D9D RID: 28061
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FRIEND_MENTORING_SEARCH";

		// Token: 0x04006D9E RID: 28062
		private static NKCPopupFriendMentoringSearch m_Instance;

		// Token: 0x04006D9F RID: 28063
		public EventTrigger m_NKM_UI_POPUP_FRIEND_MENTORING_SEARCH_BG;

		// Token: 0x04006DA0 RID: 28064
		public InputField m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT;

		// Token: 0x04006DA1 RID: 28065
		public NKCUIComButton m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON;

		// Token: 0x04006DA2 RID: 28066
		public NKCUIComButton m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH;

		// Token: 0x04006DA3 RID: 28067
		public LoopScrollRect m_MENTORING_INVITE_LIST_ScrollRect;

		// Token: 0x04006DA4 RID: 28068
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSE_BUTTON;

		// Token: 0x04006DA5 RID: 28069
		private List<FriendListData> m_lstInvited;

		// Token: 0x04006DA6 RID: 28070
		private List<FriendListData> m_lstMentor;

		// Token: 0x04006DA7 RID: 28071
		public Transform m_parentMentoringSlot;

		// Token: 0x04006DA8 RID: 28072
		private List<NKCUIMentoringSlot> m_slotList = new List<NKCUIMentoringSlot>();

		// Token: 0x04006DA9 RID: 28073
		private Stack<RectTransform> m_slotPool = new Stack<RectTransform>();
	}
}

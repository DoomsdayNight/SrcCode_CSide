using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B42 RID: 2882
	public class NKCPopupGuildInvite : NKCUIBase
	{
		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06008343 RID: 33603 RVA: 0x002C4348 File Offset: 0x002C2548
		public static NKCPopupGuildInvite Instance
		{
			get
			{
				if (NKCPopupGuildInvite.m_Instance == null)
				{
					NKCPopupGuildInvite.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildInvite>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_INVITE", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildInvite.CleanupInstance)).GetInstance<NKCPopupGuildInvite>();
					if (NKCPopupGuildInvite.m_Instance != null)
					{
						NKCPopupGuildInvite.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildInvite.m_Instance;
			}
		}

		// Token: 0x06008344 RID: 33604 RVA: 0x002C43A9 File Offset: 0x002C25A9
		private static void CleanupInstance()
		{
			NKCPopupGuildInvite.m_Instance = null;
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06008345 RID: 33605 RVA: 0x002C43B1 File Offset: 0x002C25B1
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildInvite.m_Instance != null && NKCPopupGuildInvite.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008346 RID: 33606 RVA: 0x002C43CC File Offset: 0x002C25CC
		private void OnDestroy()
		{
			NKCPopupGuildInvite.m_Instance = null;
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06008347 RID: 33607 RVA: 0x002C43D4 File Offset: 0x002C25D4
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06008348 RID: 33608 RVA: 0x002C43DB File Offset: 0x002C25DB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06008349 RID: 33609 RVA: 0x002C43DE File Offset: 0x002C25DE
		public override void CloseInternal()
		{
			this.m_IFSearch.text = "";
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600834A RID: 33610 RVA: 0x002C43FC File Offset: 0x002C25FC
		private void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnSearch.PointerClick.RemoveAllListeners();
			this.m_btnSearch.PointerClick.AddListener(new UnityAction(this.OnClickSearch));
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			this.m_IFSearch.onEndEdit.RemoveAllListeners();
			this.m_IFSearch.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		}

		// Token: 0x0600834B RID: 33611 RVA: 0x002C44E0 File Offset: 0x002C26E0
		private RectTransform GetObject(int index)
		{
			NKCUIGuildMemberSlot nkcuiguildMemberSlot;
			if (this.m_stk.Count > 0)
			{
				nkcuiguildMemberSlot = this.m_stk.Pop();
			}
			else
			{
				nkcuiguildMemberSlot = NKCUIGuildMemberSlot.GetNewInstance(this.m_trContentParent, new NKCUIGuildMemberSlot.OnSelectedSlot(this.OnSelectedSlot));
			}
			this.m_lstVisible.Add(nkcuiguildMemberSlot);
			if (nkcuiguildMemberSlot == null)
			{
				return null;
			}
			return nkcuiguildMemberSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600834C RID: 33612 RVA: 0x002C453C File Offset: 0x002C273C
		private void ReturnObject(Transform tr)
		{
			NKCUIGuildMemberSlot component = tr.GetComponent<NKCUIGuildMemberSlot>();
			this.m_lstVisible.Remove(component);
			this.m_stk.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x0600834D RID: 33613 RVA: 0x002C457C File Offset: 0x002C277C
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIGuildMemberSlot component = tr.GetComponent<NKCUIGuildMemberSlot>();
			if (this.m_lstUserData.Count < idx)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			component.SetData(this.m_lstUserData[idx], NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Invite);
		}

		// Token: 0x0600834E RID: 33614 RVA: 0x002C45B9 File Offset: 0x002C27B9
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_IFSearch.text = "";
			this.OnClickSearch();
			this.RefreshUI();
			base.UIOpened(true);
		}

		// Token: 0x0600834F RID: 33615 RVA: 0x002C45EC File Offset: 0x002C27EC
		private void SetInvitedCount()
		{
			NKCUtil.SetLabelText(this.m_lbInviteWaitingCount, string.Format("{0}/{1}", NKCGuildManager.MyGuildData.inviteList.Count, NKMCommonConst.Guild.MaxInviteCount));
			if (NKCGuildManager.MyGuildData.inviteList.Count == NKMCommonConst.Guild.MaxInviteCount)
			{
				NKCUtil.SetLabelTextColor(this.m_lbInviteWaitingCount, Color.red);
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_lbInviteWaitingCount, Color.white);
		}

		// Token: 0x06008350 RID: 33616 RVA: 0x002C4670 File Offset: 0x002C2870
		private void RefreshUI()
		{
			this.SetInvitedCount();
			this.m_loop.TotalCount = this.m_lstUserData.Count;
			this.m_loop.RefreshCells(false);
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_loop.TotalCount == 0);
		}

		// Token: 0x06008351 RID: 33617 RVA: 0x002C46C0 File Offset: 0x002C28C0
		private void OnClickSearch()
		{
			if (!string.IsNullOrWhiteSpace(this.m_IFSearch.text))
			{
				NKMPacket_FRIEND_SEARCH_REQ nkmpacket_FRIEND_SEARCH_REQ = new NKMPacket_FRIEND_SEARCH_REQ();
				nkmpacket_FRIEND_SEARCH_REQ.searchKeyword = this.m_IFSearch.text;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_SEARCH_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			NKCPacketSender.Send_NKMPacket_GUILD_RECOMMEND_INVITE_LIST_REQ(NKCGuildManager.MyData.guildUid);
		}

		// Token: 0x06008352 RID: 33618 RVA: 0x002C4719 File Offset: 0x002C2919
		public void OnRecv(List<FriendListData> list)
		{
			this.m_lstUserData = list;
			this.RefreshUI();
			if (this.m_loop.TotalCount > 0)
			{
				this.m_loop.SetIndexPosition(0);
			}
		}

		// Token: 0x06008353 RID: 33619 RVA: 0x002C4742 File Offset: 0x002C2942
		private void OnSelectedSlot(long userUid)
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(userUid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x06008354 RID: 33620 RVA: 0x002C474B File Offset: 0x002C294B
		public override void OnGuildDataChanged()
		{
			this.RefreshUI();
		}

		// Token: 0x06008355 RID: 33621 RVA: 0x002C4753 File Offset: 0x002C2953
		private void OnEndEdit(string input)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_btnSearch.m_bLock)
				{
					this.OnClickSearch();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x04006F73 RID: 28531
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F74 RID: 28532
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_INVITE";

		// Token: 0x04006F75 RID: 28533
		private static NKCPopupGuildInvite m_Instance;

		// Token: 0x04006F76 RID: 28534
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006F77 RID: 28535
		public Text m_lbInviteWaitingCount;

		// Token: 0x04006F78 RID: 28536
		public InputField m_IFSearch;

		// Token: 0x04006F79 RID: 28537
		public NKCUIComStateButton m_btnSearch;

		// Token: 0x04006F7A RID: 28538
		public LoopScrollRect m_loop;

		// Token: 0x04006F7B RID: 28539
		public Transform m_trContentParent;

		// Token: 0x04006F7C RID: 28540
		public GameObject m_objNone;

		// Token: 0x04006F7D RID: 28541
		private Stack<NKCUIGuildMemberSlot> m_stk = new Stack<NKCUIGuildMemberSlot>();

		// Token: 0x04006F7E RID: 28542
		private List<NKCUIGuildMemberSlot> m_lstVisible = new List<NKCUIGuildMemberSlot>();

		// Token: 0x04006F7F RID: 28543
		private List<FriendListData> m_lstUserData = new List<FriendListData>();
	}
}

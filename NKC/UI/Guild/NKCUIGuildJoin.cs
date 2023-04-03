using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B4F RID: 2895
	public class NKCUIGuildJoin : NKCUIBase
	{
		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x060083DA RID: 33754 RVA: 0x002C6A58 File Offset: 0x002C4C58
		public static NKCUIGuildJoin Instance
		{
			get
			{
				if (NKCUIGuildJoin.m_Instance == null)
				{
					NKCUIGuildJoin.m_Instance = NKCUIManager.OpenNewInstance<NKCUIGuildJoin>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_JOIN", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGuildJoin.CleanupInstance)).GetInstance<NKCUIGuildJoin>();
					if (NKCUIGuildJoin.m_Instance != null)
					{
						NKCUIGuildJoin.m_Instance.InitUI();
					}
				}
				return NKCUIGuildJoin.m_Instance;
			}
		}

		// Token: 0x060083DB RID: 33755 RVA: 0x002C6AB9 File Offset: 0x002C4CB9
		private static void CleanupInstance()
		{
			NKCUIGuildJoin.m_Instance = null;
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x060083DC RID: 33756 RVA: 0x002C6AC1 File Offset: 0x002C4CC1
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGuildJoin.m_Instance != null && NKCUIGuildJoin.m_Instance.IsOpen;
			}
		}

		// Token: 0x060083DD RID: 33757 RVA: 0x002C6ADC File Offset: 0x002C4CDC
		private void OnDestroy()
		{
			NKCUIGuildJoin.m_Instance = null;
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x060083DE RID: 33758 RVA: 0x002C6AE4 File Offset: 0x002C4CE4
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_CONSORTIUM_INTRO_JOIN_TEXT;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x060083DF RID: 33759 RVA: 0x002C6AEB File Offset: 0x002C4CEB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x060083E0 RID: 33760 RVA: 0x002C6AEE File Offset: 0x002C4CEE
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060083E1 RID: 33761 RVA: 0x002C6AFC File Offset: 0x002C4CFC
		public void InitUI()
		{
			this.m_tglSearchtab.OnValueChanged.RemoveAllListeners();
			this.m_tglSearchtab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSearchTab));
			this.m_tglRequestedtab.OnValueChanged.RemoveAllListeners();
			this.m_tglRequestedtab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnRequestedTab));
			this.m_tglInvitedtab.OnValueChanged.RemoveAllListeners();
			this.m_tglInvitedtab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnInvitedTab));
			this.m_btnRefresh.PointerClick.RemoveAllListeners();
			this.m_btnRefresh.PointerClick.AddListener(new UnityAction(this.OnRefreshBtn));
			this.m_btnSearch.PointerClick.RemoveAllListeners();
			this.m_btnSearch.PointerClick.AddListener(new UnityAction(this.OnSearchBtn));
			this.m_loopList.dOnGetObject += this.GetObject;
			this.m_loopList.dOnReturnObject += this.ReturnObject;
			this.m_loopList.dOnProvideData += this.ProvideData;
			this.m_loopList.PrepareCells(0);
			this.m_IFSearch.onEndEdit.RemoveAllListeners();
			this.m_IFSearch.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		}

		// Token: 0x060083E2 RID: 33762 RVA: 0x002C6C64 File Offset: 0x002C4E64
		private RectTransform GetObject(int index)
		{
			NKCUIGuildListSlot nkcuiguildListSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiguildListSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuiguildListSlot = NKCUIGuildListSlot.GetNewInstance(this.m_trContentParent, new NKCUIGuildListSlot.OnSelectedSlot(this.OnSelectedSlot));
			}
			NKCUtil.SetGameobjectActive(nkcuiguildListSlot, false);
			if (nkcuiguildListSlot == null)
			{
				return null;
			}
			return nkcuiguildListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060083E3 RID: 33763 RVA: 0x002C6CBC File Offset: 0x002C4EBC
		private void ReturnObject(Transform tr)
		{
			NKCUIGuildListSlot component = tr.GetComponent<NKCUIGuildListSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
			this.m_lstSlot.Remove(component);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x060083E4 RID: 33764 RVA: 0x002C6CFC File Offset: 0x002C4EFC
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIGuildListSlot component = tr.GetComponent<NKCUIGuildListSlot>();
			switch (this.m_CurrentUIType)
			{
			case NKCUIGuildJoin.GuildJoinUIType.Search:
				if (NKCGuildManager.m_lstSearchData.Count < idx)
				{
					return;
				}
				component.SetData(NKCGuildManager.m_lstSearchData[idx], this.m_CurrentUIType);
				return;
			case NKCUIGuildJoin.GuildJoinUIType.Requested:
				if (NKCGuildManager.m_lstRequestedData.Count < idx)
				{
					return;
				}
				component.SetData(NKCGuildManager.m_lstRequestedData[idx], this.m_CurrentUIType);
				return;
			case NKCUIGuildJoin.GuildJoinUIType.Invited:
				if (NKCGuildManager.m_lstInvitedData.Count < idx)
				{
					return;
				}
				component.SetData(NKCGuildManager.m_lstInvitedData[idx], this.m_CurrentUIType);
				return;
			default:
				return;
			}
		}

		// Token: 0x060083E5 RID: 33765 RVA: 0x002C6D9D File Offset: 0x002C4F9D
		public void Open()
		{
			this.m_CurrentUIType = NKCUIGuildJoin.GuildJoinUIType.Search;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.ResetUI();
			NKCPacketSender.Send_NKMPacket_GUILD_SEARCH_REQ("");
			base.UIOpened(true);
		}

		// Token: 0x060083E6 RID: 33766 RVA: 0x002C6DCC File Offset: 0x002C4FCC
		private void ResetUI()
		{
			this.m_IFSearch.text = string.Empty;
			this.m_btnSearch.UnLock(false);
			this.m_btnRefresh.UnLock(false);
			this.m_LastRequestedTime = new Dictionary<NKCUIGuildJoin.GuildJoinUIType, DateTime>();
			this.m_loopList.TotalCount = 0;
			this.m_loopList.RefreshCells(false);
			this.m_loopList.SetIndexPosition(0);
			this.m_tglSearchtab.Select(true, true, true);
		}

		// Token: 0x060083E7 RID: 33767 RVA: 0x002C6E40 File Offset: 0x002C5040
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > this.m_SearchInterval)
			{
				if (this.m_btnSearch.m_bLock)
				{
					this.m_btnSearch.UnLock(false);
				}
				if (this.m_btnRefresh.m_bLock)
				{
					this.m_btnRefresh.UnLock(false);
				}
			}
		}

		// Token: 0x060083E8 RID: 33768 RVA: 0x002C6EA0 File Offset: 0x002C50A0
		private void OnSearchTab(bool bValue)
		{
			if (bValue && this.m_CurrentUIType != NKCUIGuildJoin.GuildJoinUIType.Search)
			{
				this.m_CurrentUIType = NKCUIGuildJoin.GuildJoinUIType.Search;
				NKCUtil.SetGameobjectActive(this.m_objSearch, true);
				if (this.m_LastRequestedTime.ContainsKey(this.m_CurrentUIType) && NKCSynchronizedTime.GetServerUTCTime(0.0) - this.m_LastRequestedTime[this.m_CurrentUIType] < TimeSpan.FromSeconds(3.0))
				{
					this.RefreshUI();
					return;
				}
				this.OnSearchBtn();
			}
		}

		// Token: 0x060083E9 RID: 33769 RVA: 0x002C6F28 File Offset: 0x002C5128
		private void OnRequestedTab(bool bValue)
		{
			if (bValue && this.m_CurrentUIType != NKCUIGuildJoin.GuildJoinUIType.Requested)
			{
				this.m_CurrentUIType = NKCUIGuildJoin.GuildJoinUIType.Requested;
				NKCUtil.SetGameobjectActive(this.m_objSearch, false);
				if (this.m_LastRequestedTime.ContainsKey(this.m_CurrentUIType) && NKCSynchronizedTime.GetServerUTCTime(0.0) - this.m_LastRequestedTime[this.m_CurrentUIType] < TimeSpan.FromSeconds(3.0))
				{
					this.RefreshUI();
					return;
				}
				this.SaveLastRequestedTime(NKCUIGuildJoin.GuildJoinUIType.Requested);
				NKCGuildManager.Send_GUILD_LIST_REQ(GuildListType.SendRequest);
			}
		}

		// Token: 0x060083EA RID: 33770 RVA: 0x002C6FB4 File Offset: 0x002C51B4
		private void OnInvitedTab(bool bValue)
		{
			if (bValue && this.m_CurrentUIType != NKCUIGuildJoin.GuildJoinUIType.Invited)
			{
				this.m_CurrentUIType = NKCUIGuildJoin.GuildJoinUIType.Invited;
				NKCUtil.SetGameobjectActive(this.m_objSearch, false);
				if (this.m_LastRequestedTime.ContainsKey(this.m_CurrentUIType) && NKCSynchronizedTime.GetServerUTCTime(0.0) - this.m_LastRequestedTime[this.m_CurrentUIType] < TimeSpan.FromSeconds(3.0))
				{
					this.RefreshUI();
					return;
				}
				this.SaveLastRequestedTime(NKCUIGuildJoin.GuildJoinUIType.Invited);
				NKCGuildManager.Send_GUILD_LIST_REQ(GuildListType.ReceiveInvite);
			}
		}

		// Token: 0x060083EB RID: 33771 RVA: 0x002C7040 File Offset: 0x002C5240
		public void RefreshUI()
		{
			switch (this.m_CurrentUIType)
			{
			case NKCUIGuildJoin.GuildJoinUIType.Search:
				this.m_loopList.TotalCount = NKCGuildManager.m_lstSearchData.Count;
				break;
			case NKCUIGuildJoin.GuildJoinUIType.Requested:
				this.m_loopList.TotalCount = NKCGuildManager.m_lstRequestedData.Count;
				break;
			case NKCUIGuildJoin.GuildJoinUIType.Invited:
				this.m_loopList.TotalCount = NKCGuildManager.m_lstInvitedData.Count;
				break;
			}
			NKCUtil.SetLabelText(this.m_lbRequestedCount, string.Format("{0} {1}/{2}", NKCUtilString.GET_STRING_CONSORTIUM_JOIN_CONFIRM_SITUATION, NKCGuildManager.m_lstRequestedData.Count, NKMCommonConst.Guild.MaxJoinRequestCount));
			this.m_loopList.RefreshCells(false);
			this.m_loopList.SetIndexPosition(0);
		}

		// Token: 0x060083EC RID: 33772 RVA: 0x002C7100 File Offset: 0x002C5300
		private void SaveLastRequestedTime(NKCUIGuildJoin.GuildJoinUIType joinUIType)
		{
			if (!this.m_LastRequestedTime.ContainsKey(joinUIType))
			{
				this.m_LastRequestedTime.Add(joinUIType, NKCSynchronizedTime.GetServerUTCTime(0.0));
				return;
			}
			this.m_LastRequestedTime[joinUIType] = NKCSynchronizedTime.GetServerUTCTime(0.0);
		}

		// Token: 0x060083ED RID: 33773 RVA: 0x002C7150 File Offset: 0x002C5350
		private void OnSearchBtn()
		{
			this.m_btnSearch.Lock(false);
			this.m_btnRefresh.Lock(false);
			this.m_fDeltaTime = 0f;
			this.SaveLastRequestedTime(NKCUIGuildJoin.GuildJoinUIType.Search);
			NKCPacketSender.Send_NKMPacket_GUILD_SEARCH_REQ(this.m_IFSearch.text);
		}

		// Token: 0x060083EE RID: 33774 RVA: 0x002C718C File Offset: 0x002C538C
		private void OnRefreshBtn()
		{
			this.m_IFSearch.text = string.Empty;
			this.OnSearchBtn();
		}

		// Token: 0x060083EF RID: 33775 RVA: 0x002C71A4 File Offset: 0x002C53A4
		private void OnSelectedSlot(GuildListData guildData)
		{
			NKCPacketSender.Send_NKMPacket_GUILD_DATA_REQ(guildData.guildUid);
		}

		// Token: 0x060083F0 RID: 33776 RVA: 0x002C71B1 File Offset: 0x002C53B1
		public override void OnGuildDataChanged()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
		}

		// Token: 0x060083F1 RID: 33777 RVA: 0x002C71C0 File Offset: 0x002C53C0
		private void OnEndEdit(string input)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_btnSearch.m_bLock)
				{
					this.OnSearchBtn();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x04006FFA RID: 28666
		private const string BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006FFB RID: 28667
		private const string ASSET_NAME = "NKM_UI_CONSORTIUM_JOIN";

		// Token: 0x04006FFC RID: 28668
		private static NKCUIGuildJoin m_Instance;

		// Token: 0x04006FFD RID: 28669
		[Header("탭")]
		public NKCUIComToggle m_tglSearchtab;

		// Token: 0x04006FFE RID: 28670
		public NKCUIComToggle m_tglRequestedtab;

		// Token: 0x04006FFF RID: 28671
		public NKCUIComToggle m_tglInvitedtab;

		// Token: 0x04007000 RID: 28672
		public Text m_lbRequestedCount;

		// Token: 0x04007001 RID: 28673
		[Header("검색")]
		public GameObject m_objSearch;

		// Token: 0x04007002 RID: 28674
		public InputField m_IFSearch;

		// Token: 0x04007003 RID: 28675
		public NKCUIComStateButton m_btnSearch;

		// Token: 0x04007004 RID: 28676
		public NKCUIComStateButton m_btnRefresh;

		// Token: 0x04007005 RID: 28677
		[Header("리스트")]
		public LoopScrollRect m_loopList;

		// Token: 0x04007006 RID: 28678
		public Transform m_trContentParent;

		// Token: 0x04007007 RID: 28679
		public float m_SearchInterval = 3f;

		// Token: 0x04007008 RID: 28680
		private Stack<NKCUIGuildListSlot> m_stkSlot = new Stack<NKCUIGuildListSlot>();

		// Token: 0x04007009 RID: 28681
		private List<NKCUIGuildListSlot> m_lstSlot = new List<NKCUIGuildListSlot>();

		// Token: 0x0400700A RID: 28682
		private NKCUIGuildJoin.GuildJoinUIType m_CurrentUIType;

		// Token: 0x0400700B RID: 28683
		private Dictionary<NKCUIGuildJoin.GuildJoinUIType, DateTime> m_LastRequestedTime = new Dictionary<NKCUIGuildJoin.GuildJoinUIType, DateTime>();

		// Token: 0x0400700C RID: 28684
		private DateTime m_tLastSearchTime = DateTime.MinValue;

		// Token: 0x0400700D RID: 28685
		private float m_fDeltaTime;

		// Token: 0x020018E5 RID: 6373
		public enum GuildJoinUIType
		{
			// Token: 0x0400AA12 RID: 43538
			None,
			// Token: 0x0400AA13 RID: 43539
			Search,
			// Token: 0x0400AA14 RID: 43540
			Requested,
			// Token: 0x0400AA15 RID: 43541
			Invited
		}
	}
}

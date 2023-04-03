using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using ClientPacket.LeaderBoard;
using Cs.Logging;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B56 RID: 2902
	public class NKCUIGuildLobbyRank : MonoBehaviour
	{
		// Token: 0x06008464 RID: 33892 RVA: 0x002CA290 File Offset: 0x002C8490
		public void InitUI()
		{
			this.m_tglLevel.OnValueChanged.RemoveAllListeners();
			this.m_tglLevel.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedLevel));
			this.m_tglCoop.OnValueChanged.RemoveAllListeners();
			this.m_tglCoop.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedCoop));
			this.m_tglCoop.m_bGetCallbackWhileLocked = true;
			this.m_btnSeasonSelect.PointerClick.RemoveAllListeners();
			this.m_btnSeasonSelect.PointerClick.AddListener(new UnityAction(this.OnClickSeasonSelect));
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loop, null);
		}

		// Token: 0x06008465 RID: 33893 RVA: 0x002CA38C File Offset: 0x002C858C
		private RectTransform GetObject(int idx)
		{
			NKCUILeaderBoardSlot nkcuileaderBoardSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuileaderBoardSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuileaderBoardSlot = UnityEngine.Object.Instantiate<NKCUILeaderBoardSlot>(this.m_pfbSlot);
			}
			nkcuileaderBoardSlot.transform.SetParent(this.m_trSlotParent);
			return nkcuileaderBoardSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008466 RID: 33894 RVA: 0x002CA3DC File Offset: 0x002C85DC
		private void ReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.gameObject.transform);
			NKCUILeaderBoardSlot component = tr.GetComponent<NKCUILeaderBoardSlot>();
			if (component != null)
			{
				this.m_stkSlot.Push(component);
			}
		}

		// Token: 0x06008467 RID: 33895 RVA: 0x002CA420 File Offset: 0x002C8620
		private void ProvideData(Transform tr, int idx)
		{
			NKCUILeaderBoardSlot component = tr.GetComponent<NKCUILeaderBoardSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			tr.SetParent(this.m_trSlotParent);
			NKCUtil.SetGameobjectActive(tr, true);
			if (this.m_CurRankType == NKCUIGuildLobbyRank.RANK_TYPE.COOP)
			{
				component.SetData(this.m_lstCoopRank[idx], this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, true);
				return;
			}
			if (this.m_CurRankType == NKCUIGuildLobbyRank.RANK_TYPE.LEVEL)
			{
				component.SetData(this.m_lstLevelRank[idx], this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, true);
			}
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x002CA4AC File Offset: 0x002C86AC
		public void SetData()
		{
			NKCUtil.SetGameobjectActive(this.m_objNone, true);
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_RANKING, 0, 0))
			{
				return;
			}
			if (NKCGuildCoopManager.m_GuildDungeonState == GuildDungeonState.Invalid)
			{
				this.m_tglCoop.Lock(false);
			}
			else
			{
				this.m_tglCoop.UnLock(false);
			}
			if (this.m_CurRankType == NKCUIGuildLobbyRank.RANK_TYPE.NONE)
			{
				this.m_SeasonId = NKCGuildCoopManager.m_SeasonId;
				this.OnValueChangedLevel(true);
				this.m_tglLevel.Select(true, true, true);
				return;
			}
			this.RefreshUI();
		}

		// Token: 0x06008469 RID: 33897 RVA: 0x002CA524 File Offset: 0x002C8724
		private void OnValueChangedLevel(bool bValue)
		{
			if (bValue)
			{
				if (this.m_CurRankType == NKCUIGuildLobbyRank.RANK_TYPE.LEVEL)
				{
					return;
				}
				this.m_CurRankType = NKCUIGuildLobbyRank.RANK_TYPE.LEVEL;
				this.m_cNKMLeaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, 1);
				if (this.m_cNKMLeaderBoardTemplet == null)
				{
					Log.Error(string.Format("NKMLeaderBoardTemplet is null - BoardType : {0}, criteria : {1}", LeaderBoardType.BT_GUILD, 1), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCUIGuildLobbyRank.cs", 147);
					return;
				}
				if (!NKCLeaderBoardManager.HasLeaderBoardData(this.m_cNKMLeaderBoardTemplet))
				{
					this.m_loop.TotalCount = 0;
					this.m_loop.RefreshCells(false);
					NKCLeaderBoardManager.SendReq(this.m_cNKMLeaderBoardTemplet, true);
					return;
				}
				this.RefreshUI();
			}
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x002CA5BC File Offset: 0x002C87BC
		private void OnValueChangedCoop(bool bValue)
		{
			if (this.m_tglCoop.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CONSORTIUM_SEASON_OPEN_BEFORE_TOAST_TEXT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (bValue)
			{
				if (this.m_CurRankType == NKCUIGuildLobbyRank.RANK_TYPE.COOP)
				{
					return;
				}
				this.m_CurRankType = NKCUIGuildLobbyRank.RANK_TYPE.COOP;
				this.m_cNKMLeaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, this.m_SeasonId);
				if (this.m_cNKMLeaderBoardTemplet == null)
				{
					Log.Error(string.Format("NKMLeaderBoardTemplet is null - BoardType : {0}, criteria : {1}", LeaderBoardType.BT_GUILD, this.m_SeasonId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCUIGuildLobbyRank.cs", 183);
					return;
				}
				if (!NKCLeaderBoardManager.HasLeaderBoardData(this.m_cNKMLeaderBoardTemplet))
				{
					NKCLeaderBoardManager.SendReq(this.m_cNKMLeaderBoardTemplet, true);
					return;
				}
				this.RefreshUI();
			}
		}

		// Token: 0x0600846B RID: 33899 RVA: 0x002CA66D File Offset: 0x002C886D
		private void OnClickSeasonSelect()
		{
			NKCPopupGuildRankSeasonSelect.Instance.Open(new NKCPopupGuildRankSeasonSelect.OnSelectSeason(this.OnSeasonSelect), this.m_SeasonId);
		}

		// Token: 0x0600846C RID: 33900 RVA: 0x002CA68B File Offset: 0x002C888B
		private void OnSeasonSelect(int seasonId)
		{
			this.m_SeasonId = seasonId;
			this.m_cNKMLeaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, seasonId);
			if (!NKCLeaderBoardManager.HasLeaderBoardData(this.m_cNKMLeaderBoardTemplet))
			{
				NKCLeaderBoardManager.SendReq(this.m_cNKMLeaderBoardTemplet, true);
			}
		}

		// Token: 0x0600846D RID: 33901 RVA: 0x002CA6BC File Offset: 0x002C88BC
		public void RefreshUI()
		{
			int boardId = 0;
			NKCUIGuildLobbyRank.RANK_TYPE curRankType = this.m_CurRankType;
			if (curRankType != NKCUIGuildLobbyRank.RANK_TYPE.LEVEL)
			{
				if (curRankType == NKCUIGuildLobbyRank.RANK_TYPE.COOP)
				{
					this.m_cNKMLeaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, this.m_SeasonId);
					if (this.m_cNKMLeaderBoardTemplet == null)
					{
						Log.Error(string.Format("NKMLeaderBoardTemplet is null - type : {0}, criteria : {1}", LeaderBoardType.BT_GUILD, this.m_SeasonId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCUIGuildLobbyRank.cs", 244);
						NKCUtil.SetGameobjectActive(base.gameObject, false);
					}
					boardId = this.m_cNKMLeaderBoardTemplet.m_BoardID;
					this.m_lstCoopRank = NKCLeaderBoardManager.GetLeaderBoardData(this.m_cNKMLeaderBoardTemplet.m_BoardID);
					this.m_loop.TotalCount = this.m_lstCoopRank.Count;
					this.m_loop.SetIndexPosition(0);
					NKCUtil.SetLabelText(this.m_lbScoreName, NKCUtilString.GET_STRING_CONSORTIUM_RANKING_TOP_INFO_DAMAGE);
					NKCUtil.SetGameobjectActive(this.m_objNone, this.m_lstCoopRank.Count == 0);
					NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, true);
				}
			}
			else
			{
				this.m_cNKMLeaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, 1);
				if (this.m_cNKMLeaderBoardTemplet == null)
				{
					Log.Error(string.Format("NKMLeaderBoardTemplet is null - type : {0}, criteria : {1}", LeaderBoardType.BT_GUILD, 1), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCUIGuildLobbyRank.cs", 222);
					NKCUtil.SetGameobjectActive(base.gameObject, false);
				}
				boardId = this.m_cNKMLeaderBoardTemplet.m_BoardID;
				this.m_lstLevelRank = NKCLeaderBoardManager.GetLeaderBoardData(this.m_cNKMLeaderBoardTemplet.m_BoardID);
				this.m_loop.TotalCount = this.m_lstLevelRank.Count;
				this.m_loop.SetIndexPosition(0);
				NKCUtil.SetLabelText(this.m_lbScoreName, NKCUtilString.GET_STRING_CONSORTIUM_RANKING_TOP_INFO_EXP);
				NKCUtil.SetGameobjectActive(this.m_objNone, this.m_lstLevelRank.Count == 0);
				NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, false);
			}
			int rank;
			NKMGuildRankData rankData = NKCLeaderBoardManager.MakeMyGuildRankData(boardId, out rank);
			this.m_slotMyGuildRank.SetData(LeaderBoardSlotData.MakeSlotData(rankData, rank), this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, true);
		}

		// Token: 0x0400708D RID: 28813
		public NKCUILeaderBoardSlot m_pfbSlot;

		// Token: 0x0400708E RID: 28814
		public NKCUIComToggle m_tglLevel;

		// Token: 0x0400708F RID: 28815
		public NKCUIComToggle m_tglCoop;

		// Token: 0x04007090 RID: 28816
		public NKCUIComStateButton m_btnSeasonSelect;

		// Token: 0x04007091 RID: 28817
		public Text m_lbScoreName;

		// Token: 0x04007092 RID: 28818
		public LoopScrollRect m_loop;

		// Token: 0x04007093 RID: 28819
		public Transform m_trSlotParent;

		// Token: 0x04007094 RID: 28820
		public NKCUILeaderBoardSlot m_slotMyGuildRank;

		// Token: 0x04007095 RID: 28821
		public GameObject m_objNone;

		// Token: 0x04007096 RID: 28822
		private Stack<NKCUILeaderBoardSlot> m_stkSlot = new Stack<NKCUILeaderBoardSlot>();

		// Token: 0x04007097 RID: 28823
		private List<LeaderBoardSlotData> m_lstLevelRank = new List<LeaderBoardSlotData>();

		// Token: 0x04007098 RID: 28824
		private List<LeaderBoardSlotData> m_lstCoopRank = new List<LeaderBoardSlotData>();

		// Token: 0x04007099 RID: 28825
		private int m_SeasonId;

		// Token: 0x0400709A RID: 28826
		private NKCUIGuildLobbyRank.RANK_TYPE m_CurRankType;

		// Token: 0x0400709B RID: 28827
		private NKMLeaderBoardTemplet m_cNKMLeaderBoardTemplet;

		// Token: 0x020018F0 RID: 6384
		private enum RANK_TYPE
		{
			// Token: 0x0400AA36 RID: 43574
			NONE,
			// Token: 0x0400AA37 RID: 43575
			LEVEL,
			// Token: 0x0400AA38 RID: 43576
			COOP
		}
	}
}

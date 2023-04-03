using System;
using System.Collections.Generic;
using ClientPacket.Pvp;
using NKC.UI.Shop;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B72 RID: 2930
	public class NKCUIGauntletLobby : NKCUIBase
	{
		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x0600861B RID: 34331 RVA: 0x002D6690 File Offset: 0x002D4890
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x0600861C RID: 34332 RVA: 0x002D6697 File Offset: 0x002D4897
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x0600861D RID: 34333 RVA: 0x002D669A File Offset: 0x002D489A
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x0600861E RID: 34334 RVA: 0x002D66A0 File Offset: 0x002D48A0
		public override string GuideTempletID
		{
			get
			{
				switch (this.m_NKC_GAUNTLET_LOBBY_TAB)
				{
				case NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC:
					return "ARTICLE_PVP_ASYNC";
				case NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE:
					return "ARTICLE_PVP_FRIENDLY";
				}
				return "ARTICLE_PVP_RANK";
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x0600861F RID: 34335 RVA: 0x002D66E1 File Offset: 0x002D48E1
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
				{
					return new List<int>
					{
						13,
						5,
						101
					};
				}
				return new List<int>
				{
					5,
					101
				};
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06008620 RID: 34336 RVA: 0x002D671D File Offset: 0x002D491D
		private GameObject UIGauntletLobbyAsync
		{
			get
			{
				if (!this.m_bOpenAsyncNewMode)
				{
					return this.m_NKCUIGauntletLobbyAsync.gameObject;
				}
				return this.m_NKCUIGauntletLobbyAsyncV2.gameObject;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06008621 RID: 34337 RVA: 0x002D673E File Offset: 0x002D493E
		private NKCUIComToggle ctglAsync
		{
			get
			{
				if (!this.m_bOpenAsyncNewMode)
				{
					return this.m_ctglAsync;
				}
				return this.m_ctglAsyncV2;
			}
		}

		// Token: 0x06008622 RID: 34338 RVA: 0x002D6755 File Offset: 0x002D4955
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIBaseSceneMenu>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LOBBY");
		}

		// Token: 0x06008623 RID: 34339 RVA: 0x002D6766 File Offset: 0x002D4966
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIGauntletLobby retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIGauntletLobby>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x06008624 RID: 34340 RVA: 0x002D6775 File Offset: 0x002D4975
		public RANK_TYPE GetCurrRankType()
		{
			if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
			{
				return this.m_NKCUIGauntletLobbyAsync.GetCurrRankType();
			}
			if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE)
			{
				return this.m_NKCUIGauntletLobbyLeague.GetCurrRankType();
			}
			return this.m_NKCUIGauntletLobbyRank.GetCurrRankType();
		}

		// Token: 0x06008625 RID: 34341 RVA: 0x002D67AC File Offset: 0x002D49AC
		public NKC_GAUNTLET_LOBBY_TAB GetCurrentLobbyTab()
		{
			return this.m_NKC_GAUNTLET_LOBBY_TAB;
		}

		// Token: 0x06008626 RID: 34342 RVA: 0x002D67B4 File Offset: 0x002D49B4
		public void CloseInstance()
		{
			NKCUIGauntletLobbyRank nkcuigauntletLobbyRank = this.m_NKCUIGauntletLobbyRank;
			if (nkcuigauntletLobbyRank != null)
			{
				nkcuigauntletLobbyRank.ClearCacheData();
			}
			if (this.m_bOpenAsyncNewMode)
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV != null)
				{
					nkcuigauntletLobbyAsyncV.ClearCacheData();
				}
			}
			else
			{
				NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
				if (nkcuigauntletLobbyAsync != null)
				{
					nkcuigauntletLobbyAsync.ClearCacheData();
				}
			}
			NKCUIGauntletLobbyReplay nkcuigauntletLobbyReplay = this.m_NKCUIGauntletLobbyReplay;
			if (nkcuigauntletLobbyReplay != null)
			{
				nkcuigauntletLobbyReplay.ClearCacheData();
			}
			NKCUIGauntletLobbyCustom nkcuigauntletLobbyCustom = this.m_NKCUIGauntletLobbyCustom;
			if (nkcuigauntletLobbyCustom != null)
			{
				nkcuigauntletLobbyCustom.ClearCacheData();
			}
			NKCUIGauntletLobbyLeague nkcuigauntletLobbyLeague = this.m_NKCUIGauntletLobbyLeague;
			if (nkcuigauntletLobbyLeague != null)
			{
				nkcuigauntletLobbyLeague.ClearCacheData();
			}
			NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LOBBY");
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008627 RID: 34343 RVA: 0x002D684C File Offset: 0x002D4A4C
		public void InitUI()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_bOpenAsyncNewMode = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE);
			this.m_ctglRank.OnValueChanged.RemoveAllListeners();
			this.m_ctglRank.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedRank));
			this.m_ctglRank.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetToggleValueChangedDelegate(this.ctglAsync, new UnityAction<bool>(this.OnValueChangedAsync));
			NKCUtil.SetGameobjectActive(this.m_ctglAsyncV2, this.m_bOpenAsyncNewMode);
			NKCUtil.SetGameobjectActive(this.m_ctglAsync, !this.m_bOpenAsyncNewMode);
			this.m_ctglReplay.OnValueChanged.RemoveAllListeners();
			this.m_ctglReplay.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedReplay));
			NKCUtil.SetGameobjectActive(this.m_ctglReplay, NKCReplayMgr.IsReplayLobbyTabOpened());
			this.m_ctglLeague.OnValueChanged.RemoveAllListeners();
			this.m_ctglLeague.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedLeague));
			NKCUtil.SetGameobjectActive(this.m_ctglLeague, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE));
			this.m_ctglLeague.m_bGetCallbackWhileLocked = true;
			if (this.m_ctglCustom != null)
			{
				this.m_ctglCustom.OnValueChanged.RemoveAllListeners();
				this.m_ctglCustom.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedCustom));
				NKCUtil.SetGameobjectActive(this.m_ctglCustom, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_FRIENDLY_MODE));
			}
			this.m_csbtnPVPShop.PointerClick.RemoveAllListeners();
			this.m_csbtnPVPShop.PointerClick.AddListener(new UnityAction(this.OnClickPVPShop));
			NKCUIGauntletLobbyRank nkcuigauntletLobbyRank = this.m_NKCUIGauntletLobbyRank;
			if (nkcuigauntletLobbyRank != null)
			{
				nkcuigauntletLobbyRank.Init();
			}
			if (!this.m_bOpenAsyncNewMode)
			{
				NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
				if (nkcuigauntletLobbyAsync != null)
				{
					nkcuigauntletLobbyAsync.Init();
				}
			}
			else
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV != null)
				{
					nkcuigauntletLobbyAsyncV.Init();
				}
			}
			NKCUIGauntletLobbyCustom nkcuigauntletLobbyCustom = this.m_NKCUIGauntletLobbyCustom;
			if (nkcuigauntletLobbyCustom != null)
			{
				nkcuigauntletLobbyCustom.Init();
			}
			NKCUIGauntletLobbyLeague nkcuigauntletLobbyLeague = this.m_NKCUIGauntletLobbyLeague;
			if (nkcuigauntletLobbyLeague != null)
			{
				nkcuigauntletLobbyLeague.Init();
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_REPLAY))
			{
				NKCScenManager.GetScenManager().GetNKCReplayMgr().ReadReplayData();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_bInit = true;
		}

		// Token: 0x06008628 RID: 34344 RVA: 0x002D6A69 File Offset: 0x002D4C69
		private void OnLeftMenuTabChanged(NKC_GAUNTLET_LOBBY_TAB eNKC_GAUNTLET_LOBBY_TAB)
		{
			this.m_NKC_GAUNTLET_LOBBY_TAB = eNKC_GAUNTLET_LOBBY_TAB;
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(this.m_NKC_GAUNTLET_LOBBY_TAB);
			this.ResetUIByCurrTab();
			this.TryGetRewardREQ();
		}

		// Token: 0x06008629 RID: 34345 RVA: 0x002D6A94 File Offset: 0x002D4C94
		private void OnValueChangedRank(bool bSet)
		{
			if (this.m_ctglRank.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCUtilString.GET_STRING_GAUNTLET_NOT_OPEN_RANK_MODE, NKMPvpCommonConst.Instance.RANK_PVP_OPEN_POINT), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (bSet)
			{
				this.OnLeftMenuTabChanged(NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK);
			}
		}

		// Token: 0x0600862A RID: 34346 RVA: 0x002D6AEA File Offset: 0x002D4CEA
		private void OnValueChangedAsync(bool bSet)
		{
			if (bSet)
			{
				this.OnLeftMenuTabChanged(NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC);
			}
		}

		// Token: 0x0600862B RID: 34347 RVA: 0x002D6AF6 File Offset: 0x002D4CF6
		private void OnValueChangedReplay(bool bSet)
		{
			if (bSet)
			{
				this.OnLeftMenuTabChanged(NKC_GAUNTLET_LOBBY_TAB.NGLT_REPLAY);
			}
		}

		// Token: 0x0600862C RID: 34348 RVA: 0x002D6B04 File Offset: 0x002D4D04
		private void OnValueChangedLeague(bool bSet)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_COMING_SOON_SYSTEM, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (this.m_ctglLeague.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCUtilString.GET_STRING_GAUNTLET_NOT_OPEN_LEAGUE_MODE, NKMPvpCommonConst.Instance.LEAGUE_PVP_OPEN_POINT), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (bSet)
			{
				this.OnLeftMenuTabChanged(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
			}
		}

		// Token: 0x0600862D RID: 34349 RVA: 0x002D6B77 File Offset: 0x002D4D77
		private void OnValueChangedCustom(bool bSet)
		{
			if (bSet)
			{
				this.OnLeftMenuTabChanged(NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE);
			}
		}

		// Token: 0x0600862E RID: 34350 RVA: 0x002D6B83 File Offset: 0x002D4D83
		private void OnClickPVPShop()
		{
			NKCUIShop.ShopShortcut("TAB_SEASON_GAUNTLET", 0, 0);
		}

		// Token: 0x0600862F RID: 34351 RVA: 0x002D6B94 File Offset: 0x002D4D94
		private void ResetUIByCurrTab()
		{
			NKCUtil.SetGameobjectActive(this.m_NKCUIGauntletLobbyRank, this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK);
			NKCUtil.SetGameobjectActive(this.UIGauntletLobbyAsync, this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC);
			NKCUtil.SetGameobjectActive(this.m_NKCUIGauntletLobbyReplay, this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_REPLAY && NKCReplayMgr.IsReplayLobbyTabOpened());
			NKCUtil.SetGameobjectActive(this.m_NKCUIGauntletLobbyCustom, this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_FRIENDLY_MODE));
			NKCUtil.SetGameobjectActive(this.m_NKCUIGauntletLobbyLeague, this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE));
			switch (this.m_NKC_GAUNTLET_LOBBY_TAB)
			{
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK:
			{
				NKCUIGauntletLobbyRank nkcuigauntletLobbyRank = this.m_NKCUIGauntletLobbyRank;
				if (nkcuigauntletLobbyRank != null)
				{
					nkcuigauntletLobbyRank.SetUI();
				}
				break;
			}
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC:
				if (this.m_bOpenAsyncNewMode)
				{
					NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
					if (nkcuigauntletLobbyAsyncV != null)
					{
						nkcuigauntletLobbyAsyncV.SetUI(this.m_reservedAsyncTab);
					}
				}
				else
				{
					NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
					if (nkcuigauntletLobbyAsync != null)
					{
						nkcuigauntletLobbyAsync.SetUI();
					}
				}
				break;
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_REPLAY:
			{
				NKCUIGauntletLobbyReplay nkcuigauntletLobbyReplay = this.m_NKCUIGauntletLobbyReplay;
				if (nkcuigauntletLobbyReplay != null)
				{
					nkcuigauntletLobbyReplay.SetUI();
				}
				break;
			}
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE:
			{
				NKCUIGauntletLobbyCustom nkcuigauntletLobbyCustom = this.m_NKCUIGauntletLobbyCustom;
				if (nkcuigauntletLobbyCustom != null)
				{
					nkcuigauntletLobbyCustom.SetUI();
				}
				break;
			}
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE:
			{
				NKCUIGauntletLobbyLeague nkcuigauntletLobbyLeague = this.m_NKCUIGauntletLobbyLeague;
				if (nkcuigauntletLobbyLeague != null)
				{
					nkcuigauntletLobbyLeague.SetUI();
				}
				break;
			}
			}
			base.UpdateUpsideMenu();
		}

		// Token: 0x06008630 RID: 34352 RVA: 0x002D6CCC File Offset: 0x002D4ECC
		public void Open(NKC_GAUNTLET_LOBBY_TAB _NKC_GAUNTLET_LOBBY_TAB = NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK, RANK_TYPE eRANK_TYPE = RANK_TYPE.MY_LEAGUE, NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE asyncTab = NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.MAX)
		{
			this.m_NKC_GAUNTLET_LOBBY_TAB = _NKC_GAUNTLET_LOBBY_TAB;
			this.m_reservedAsyncTab = asyncTab;
			this.m_NKCUIGauntletLobbyRank.SetCurrRankType(eRANK_TYPE);
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			bool flag = gameOptionData != null && gameOptionData.UseVideoTexture;
			NKCUtil.SetGameobjectActive(this.m_objBGFallBack, !flag);
			base.UIOpened(true);
			this.m_amtorLeft.Play("NKM_UI_GAUNTLET_LOBBY_CONTENT_INTRO_LEFT");
			if (!NKCPVPManager.IsPvpRankUnlocked())
			{
				this.m_ctglRank.Lock(false);
			}
			else
			{
				this.m_ctglRank.UnLock(false);
			}
			if (!NKCPVPManager.IsPvpLeagueUnlocked() || !NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
			{
				this.m_ctglLeague.Lock(false);
			}
			else
			{
				this.m_ctglLeague.UnLock(false);
			}
			if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				this.m_ctglRank.Select(false, true, false);
				this.m_ctglRank.Select(true, false, false);
			}
			else if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
			{
				this.ctglAsync.Select(false, true, false);
				this.ctglAsync.Select(true, false, false);
			}
			else if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_REPLAY)
			{
				this.m_ctglReplay.Select(false, true, false);
				this.m_ctglReplay.Select(true, false, false);
			}
			else if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE)
			{
				this.m_ctglCustom.Select(false, true, false);
				this.m_ctglCustom.Select(true, false, false);
			}
			else if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE)
			{
				this.m_ctglLeague.Select(false, true, false);
				this.m_ctglLeague.Select(true, false, false);
			}
			this.CheckTutorial();
		}

		// Token: 0x06008631 RID: 34353 RVA: 0x002D6E50 File Offset: 0x002D5050
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUIGauntletLobbyRank nkcuigauntletLobbyRank = this.m_NKCUIGauntletLobbyRank;
			if (nkcuigauntletLobbyRank != null)
			{
				nkcuigauntletLobbyRank.Close();
			}
			NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
			if (nkcuigauntletLobbyAsync != null)
			{
				nkcuigauntletLobbyAsync.Close();
			}
			NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
			if (nkcuigauntletLobbyAsyncV != null)
			{
				nkcuigauntletLobbyAsyncV.Close();
			}
			NKCUIGauntletLobbyReplay nkcuigauntletLobbyReplay = this.m_NKCUIGauntletLobbyReplay;
			if (nkcuigauntletLobbyReplay != null)
			{
				nkcuigauntletLobbyReplay.Close();
			}
			NKCUIGauntletLobbyCustom nkcuigauntletLobbyCustom = this.m_NKCUIGauntletLobbyCustom;
			if (nkcuigauntletLobbyCustom != null)
			{
				nkcuigauntletLobbyCustom.Close();
			}
			NKCUIGauntletLobbyLeague nkcuigauntletLobbyLeague = this.m_NKCUIGauntletLobbyLeague;
			if (nkcuigauntletLobbyLeague == null)
			{
				return;
			}
			nkcuigauntletLobbyLeague.Close();
		}

		// Token: 0x06008632 RID: 34354 RVA: 0x002D6ECE File Offset: 0x002D50CE
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_INTRO, true);
		}

		// Token: 0x06008633 RID: 34355 RVA: 0x002D6EDD File Offset: 0x002D50DD
		public void OnRecv(NKMPacket_PVP_RANK_LIST_ACK cNKMPacket_PVP_RANK_LIST_ACK)
		{
			if (this.m_NKCUIGauntletLobbyRank != null)
			{
				this.m_NKCUIGauntletLobbyRank.OnRecv(cNKMPacket_PVP_RANK_LIST_ACK);
			}
		}

		// Token: 0x06008634 RID: 34356 RVA: 0x002D6EF9 File Offset: 0x002D50F9
		public void OnRecv(NKMPacket_NPC_PVP_TARGET_LIST_ACK sPacket)
		{
			NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
			if (nkcuigauntletLobbyAsyncV == null)
			{
				return;
			}
			nkcuigauntletLobbyAsyncV.OnRecv(sPacket);
		}

		// Token: 0x06008635 RID: 34357 RVA: 0x002D6F0C File Offset: 0x002D510C
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_UNIT_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobbyRank != null)
			{
				this.m_NKCUIGauntletLobbyRank.OnRecv(sPacket);
			}
		}

		// Token: 0x06008636 RID: 34358 RVA: 0x002D6F28 File Offset: 0x002D5128
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_SHIP_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobbyRank != null)
			{
				this.m_NKCUIGauntletLobbyRank.OnRecv(sPacket);
			}
		}

		// Token: 0x06008637 RID: 34359 RVA: 0x002D6F44 File Offset: 0x002D5144
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_OPERATOR_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobbyRank != null)
			{
				this.m_NKCUIGauntletLobbyRank.OnRecv(sPacket);
			}
		}

		// Token: 0x06008638 RID: 34360 RVA: 0x002D6F60 File Offset: 0x002D5160
		public void OnRecv(NKMPacket_LEAGUE_PVP_RANK_LIST_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobbyLeague != null)
			{
				this.m_NKCUIGauntletLobbyLeague.OnRecv(sPacket);
			}
		}

		// Token: 0x06008639 RID: 34361 RVA: 0x002D6F7C File Offset: 0x002D517C
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			NKCUIGauntletLobbyRank nkcuigauntletLobbyRank = this.m_NKCUIGauntletLobbyRank;
			if (nkcuigauntletLobbyRank != null)
			{
				nkcuigauntletLobbyRank.OnRecv(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK);
			}
			NKCUIGauntletLobbyLeague nkcuigauntletLobbyLeague = this.m_NKCUIGauntletLobbyLeague;
			if (nkcuigauntletLobbyLeague != null)
			{
				nkcuigauntletLobbyLeague.OnRecv(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK);
			}
			if (!this.m_bOpenAsyncNewMode)
			{
				NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
				if (nkcuigauntletLobbyAsync == null)
				{
					return;
				}
				nkcuigauntletLobbyAsync.OnRecv(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK);
				return;
			}
			else
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV == null)
				{
					return;
				}
				nkcuigauntletLobbyAsyncV.OnRecv(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK);
				return;
			}
		}

		// Token: 0x0600863A RID: 34362 RVA: 0x002D6FD8 File Offset: 0x002D51D8
		public void OnRecv(NKMPacket_PVP_RANK_WEEK_REWARD_ACK sPacket)
		{
			NKCUIGauntletLobbyRank nkcuigauntletLobbyRank = this.m_NKCUIGauntletLobbyRank;
			if (nkcuigauntletLobbyRank == null)
			{
				return;
			}
			nkcuigauntletLobbyRank.OnRecv(sPacket);
		}

		// Token: 0x0600863B RID: 34363 RVA: 0x002D6FEB File Offset: 0x002D51EB
		public void OnRecv(NKMPacket_PVP_RANK_SEASON_REWARD_ACK cNKMPacket_PVP_RANK_SEASON_REWARD_ACK)
		{
			NKCUIGauntletLobbyRank nkcuigauntletLobbyRank = this.m_NKCUIGauntletLobbyRank;
			if (nkcuigauntletLobbyRank == null)
			{
				return;
			}
			nkcuigauntletLobbyRank.OnRecv(cNKMPacket_PVP_RANK_SEASON_REWARD_ACK);
		}

		// Token: 0x0600863C RID: 34364 RVA: 0x002D6FFE File Offset: 0x002D51FE
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_ACK packet)
		{
			if (!this.m_bOpenAsyncNewMode)
			{
				NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
				if (nkcuigauntletLobbyAsync == null)
				{
					return;
				}
				nkcuigauntletLobbyAsync.OnRecv(packet);
				return;
			}
			else
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV == null)
				{
					return;
				}
				nkcuigauntletLobbyAsyncV.OnRecv(packet);
				return;
			}
		}

		// Token: 0x0600863D RID: 34365 RVA: 0x002D702B File Offset: 0x002D522B
		public void OnRecv(NKMPacket_LEAGUE_PVP_WEEKLY_REWARD_ACK packet)
		{
			NKCUIGauntletLobbyLeague nkcuigauntletLobbyLeague = this.m_NKCUIGauntletLobbyLeague;
			if (nkcuigauntletLobbyLeague == null)
			{
				return;
			}
			nkcuigauntletLobbyLeague.OnRecv(packet);
		}

		// Token: 0x0600863E RID: 34366 RVA: 0x002D703E File Offset: 0x002D523E
		public void OnRecv(NKMPacket_LEAGUE_PVP_SEASON_REWARD_ACK packet)
		{
			NKCUIGauntletLobbyLeague nkcuigauntletLobbyLeague = this.m_NKCUIGauntletLobbyLeague;
			if (nkcuigauntletLobbyLeague == null)
			{
				return;
			}
			nkcuigauntletLobbyLeague.OnRecv(packet);
		}

		// Token: 0x0600863F RID: 34367 RVA: 0x002D7051 File Offset: 0x002D5251
		public void OnRecv(NKMPacket_ASYNC_PVP_TARGET_LIST_ACK packet)
		{
			if (!this.m_bOpenAsyncNewMode)
			{
				NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
				if (nkcuigauntletLobbyAsync == null)
				{
					return;
				}
				nkcuigauntletLobbyAsync.OnRecv(packet);
				return;
			}
			else
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV == null)
				{
					return;
				}
				nkcuigauntletLobbyAsyncV.OnRecv(packet);
				return;
			}
		}

		// Token: 0x06008640 RID: 34368 RVA: 0x002D707E File Offset: 0x002D527E
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_LIST_ACK packet)
		{
			if (!this.m_bOpenAsyncNewMode)
			{
				NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
				if (nkcuigauntletLobbyAsync == null)
				{
					return;
				}
				nkcuigauntletLobbyAsync.OnRecv(packet);
				return;
			}
			else
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV == null)
				{
					return;
				}
				nkcuigauntletLobbyAsyncV.OnRecv(packet);
				return;
			}
		}

		// Token: 0x06008641 RID: 34369 RVA: 0x002D70AB File Offset: 0x002D52AB
		public void OnRecv(NKMPacket_REVENGE_PVP_TARGET_LIST_ACK sPacket)
		{
			NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
			if (nkcuigauntletLobbyAsyncV == null)
			{
				return;
			}
			nkcuigauntletLobbyAsyncV.OnRecv(sPacket);
		}

		// Token: 0x06008642 RID: 34370 RVA: 0x002D70BE File Offset: 0x002D52BE
		public void OnRecv(NKMPacket_UPDATE_DEFENCE_DECK_ACK packet)
		{
			if (!this.m_bOpenAsyncNewMode)
			{
				NKCUIGauntletLobbyAsync nkcuigauntletLobbyAsync = this.m_NKCUIGauntletLobbyAsync;
				if (nkcuigauntletLobbyAsync == null)
				{
					return;
				}
				nkcuigauntletLobbyAsync.OnRecv(packet);
				return;
			}
			else
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV == null)
				{
					return;
				}
				nkcuigauntletLobbyAsyncV.OnRecv(packet);
				return;
			}
		}

		// Token: 0x06008643 RID: 34371 RVA: 0x002D70EC File Offset: 0x002D52EC
		private void TryGetRewardREQ()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			PvpState pvpData;
			int seasonID;
			int weekID;
			if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				pvpData = myUserData.m_PvpData;
				seasonID = NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0));
				weekID = NKCPVPManager.GetWeekIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0), seasonID);
			}
			else if (this.m_NKC_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
			{
				pvpData = myUserData.m_AsyncData;
				seasonID = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
				weekID = NKCPVPManager.GetWeekIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0), seasonID);
			}
			else
			{
				if (this.m_NKC_GAUNTLET_LOBBY_TAB != NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE)
				{
					return;
				}
				pvpData = myUserData.m_LeagueData;
				seasonID = NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0));
				weekID = NKCPVPManager.GetWeekIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0), seasonID);
			}
			if (NKCPVPManager.CanRewardWeek(this.GetGameType(this.m_NKC_GAUNTLET_LOBBY_TAB), pvpData, seasonID, weekID, NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_ERROR_CODE.NEC_OK)
			{
				this.SendWeekRewardREQ(this.m_NKC_GAUNTLET_LOBBY_TAB);
				return;
			}
			if (NKCPVPManager.CanRewardSeason(pvpData, seasonID, NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_ERROR_CODE.NEC_OK)
			{
				this.SendSeasonRewardREQ(this.m_NKC_GAUNTLET_LOBBY_TAB);
			}
		}

		// Token: 0x06008644 RID: 34372 RVA: 0x002D720D File Offset: 0x002D540D
		private NKM_GAME_TYPE GetGameType(NKC_GAUNTLET_LOBBY_TAB gauntletLobbyType)
		{
			switch (gauntletLobbyType)
			{
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK:
				return NKM_GAME_TYPE.NGT_PVP_RANK;
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC:
				return NKM_GAME_TYPE.NGT_ASYNC_PVP;
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE:
				return NKM_GAME_TYPE.NGT_PVP_PRIVATE;
			case NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE:
				return NKM_GAME_TYPE.NGT_PVP_LEAGUE;
			}
			return NKM_GAME_TYPE.NGT_INVALID;
		}

		// Token: 0x06008645 RID: 34373 RVA: 0x002D723C File Offset: 0x002D543C
		private void SendWeekRewardREQ(NKC_GAUNTLET_LOBBY_TAB tab)
		{
			if (tab == NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				NKMPacket_PVP_RANK_WEEK_REWARD_REQ packet = new NKMPacket_PVP_RANK_WEEK_REWARD_REQ();
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			if (tab != NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
			{
				return;
			}
			NKMPacket_ASYNC_PVP_RANK_WEEK_REWARD_REQ packet2 = new NKMPacket_ASYNC_PVP_RANK_WEEK_REWARD_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet2, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008646 RID: 34374 RVA: 0x002D7284 File Offset: 0x002D5484
		private void SendSeasonRewardREQ(NKC_GAUNTLET_LOBBY_TAB tab)
		{
			if (tab == NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				NKMPacket_PVP_RANK_SEASON_REWARD_REQ packet = new NKMPacket_PVP_RANK_SEASON_REWARD_REQ();
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			if (tab == NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
			{
				NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_REQ packet2 = new NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_REQ();
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet2, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			if (tab != NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE)
			{
				return;
			}
			NKMPacket_LEAGUE_PVP_SEASON_REWARD_REQ packet3 = new NKMPacket_LEAGUE_PVP_SEASON_REWARD_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet3, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008647 RID: 34375 RVA: 0x002D72EA File Offset: 0x002D54EA
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.GauntletLobby, true);
		}

		// Token: 0x040072BF RID: 29375
		public NKCUIComToggle m_ctglRank;

		// Token: 0x040072C0 RID: 29376
		public NKCUIComToggle m_ctglAsync;

		// Token: 0x040072C1 RID: 29377
		public NKCUIComToggle m_ctglAsyncV2;

		// Token: 0x040072C2 RID: 29378
		public NKCUIComToggle m_ctglReplay;

		// Token: 0x040072C3 RID: 29379
		public NKCUIComToggle m_ctglCustom;

		// Token: 0x040072C4 RID: 29380
		public NKCUIComToggle m_ctglLeague;

		// Token: 0x040072C5 RID: 29381
		public NKCUIComStateButton m_csbtnPVPShop;

		// Token: 0x040072C6 RID: 29382
		public Animator m_amtorLeft;

		// Token: 0x040072C7 RID: 29383
		public NKCUIGauntletLobbyRank m_NKCUIGauntletLobbyRank;

		// Token: 0x040072C8 RID: 29384
		public NKCUIGauntletLobbyAsync m_NKCUIGauntletLobbyAsync;

		// Token: 0x040072C9 RID: 29385
		public NKCUIGauntletLobbyAsyncV2 m_NKCUIGauntletLobbyAsyncV2;

		// Token: 0x040072CA RID: 29386
		public NKCUIGauntletLobbyReplay m_NKCUIGauntletLobbyReplay;

		// Token: 0x040072CB RID: 29387
		public NKCUIGauntletLobbyCustom m_NKCUIGauntletLobbyCustom;

		// Token: 0x040072CC RID: 29388
		public NKCUIGauntletLobbyLeague m_NKCUIGauntletLobbyLeague;

		// Token: 0x040072CD RID: 29389
		[Header("Fallback BG")]
		public GameObject m_objBGFallBack;

		// Token: 0x040072CE RID: 29390
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040072CF RID: 29391
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_LOBBY";

		// Token: 0x040072D0 RID: 29392
		private bool m_bInit;

		// Token: 0x040072D1 RID: 29393
		private NKC_GAUNTLET_LOBBY_TAB m_NKC_GAUNTLET_LOBBY_TAB;

		// Token: 0x040072D2 RID: 29394
		private NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE m_reservedAsyncTab = NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.MAX;

		// Token: 0x040072D3 RID: 29395
		private bool m_bOpenAsyncNewMode;
	}
}

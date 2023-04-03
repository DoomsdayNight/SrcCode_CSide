using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.LeaderBoard;
using Cs.Logging;
using NKC.UI.Fierce;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000959 RID: 2393
	public class NKCUILeaderBoard : NKCUIBase
	{
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06005F81 RID: 24449 RVA: 0x001DAD48 File Offset: 0x001D8F48
		public static NKCUILeaderBoard Instance
		{
			get
			{
				if (NKCUILeaderBoard.m_Instance == null)
				{
					NKCUILeaderBoard.m_Instance = NKCUIManager.OpenNewInstance<NKCUILeaderBoard>("AB_UI_NKM_UI_LEADER_BOARD", "NKM_UI_LEADER_BOARD", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUILeaderBoard.CleanupInstance)).GetInstance<NKCUILeaderBoard>();
					NKCUILeaderBoard.m_Instance.Init();
				}
				return NKCUILeaderBoard.m_Instance;
			}
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x001DAD97 File Offset: 0x001D8F97
		private static void CleanupInstance()
		{
			NKCUILeaderBoard.m_Instance = null;
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06005F83 RID: 24451 RVA: 0x001DAD9F File Offset: 0x001D8F9F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUILeaderBoard.m_Instance != null && NKCUILeaderBoard.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x001DADBA File Offset: 0x001D8FBA
		private void OnDestroy()
		{
			NKCUILeaderBoard.m_Instance = null;
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06005F85 RID: 24453 RVA: 0x001DADC2 File Offset: 0x001D8FC2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x001DADC5 File Offset: 0x001D8FC5
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_LEADERBOARD", false);
			}
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x001DADD2 File Offset: 0x001D8FD2
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_dicTabTemplet.Clear();
			this.m_CurrentBoardType = LeaderBoardType.BT_NONE;
			this.m_cNKMLeaderBoardTemplet = null;
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x001DADF9 File Offset: 0x001D8FF9
		public override void Hide()
		{
			this.m_CurrentBoardType = LeaderBoardType.BT_NONE;
			base.Hide();
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x001DAE08 File Offset: 0x001D9008
		public override void UnHide()
		{
			base.UnHide();
			this.Open(this.m_cNKMLeaderBoardTemplet, false);
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x001DAE20 File Offset: 0x001D9020
		protected void Init()
		{
			if (this.m_LoopScrollSubTab != null)
			{
				this.m_LoopScrollSubTab.dOnGetObject += this.GetSubTabObject;
				this.m_LoopScrollSubTab.dOnReturnObject += this.ReturnSubTabObject;
				this.m_LoopScrollSubTab.dOnProvideData += this.ProvideSubTabData;
				this.m_LoopScrollSubTab.PrepareCells(0);
			}
			if (this.m_LoopScrollContent != null)
			{
				this.m_LoopScrollContent.dOnGetObject += this.GetContentObject;
				this.m_LoopScrollContent.dOnReturnObject += this.ReturnContentObject;
				this.m_LoopScrollContent.dOnProvideData += this.ProvideContentData;
				this.m_LoopScrollContent.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScrollContent, null);
			}
			if (this.m_pfbTab != null && this.m_trTabParent != null)
			{
				foreach (NKMLeaderBoardTemplet nkmleaderBoardTemplet in NKMTempletContainer<NKMLeaderBoardTemplet>.Values)
				{
					if (!this.m_dicBoardTab.ContainsKey(nkmleaderBoardTemplet.m_BoardTab) && nkmleaderBoardTemplet.m_BoardTabSubIndex == 0 && (nkmleaderBoardTemplet.m_BoardTab != LeaderBoardType.BT_GUILD || NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_RANKING, 0, 0)))
					{
						NKCUILeaderBoardTab nkcuileaderBoardTab = UnityEngine.Object.Instantiate<NKCUILeaderBoardTab>(this.m_pfbTab, this.m_trTabParent);
						nkcuileaderBoardTab.SetData(nkmleaderBoardTemplet.m_BoardTab, this.m_tabTglGroup, new NKCUILeaderBoardTab.OnValueChange(this.OnClickMainTab));
						this.m_dicBoardTab.Add(nkmleaderBoardTemplet.m_BoardTab, nkcuileaderBoardTab);
					}
				}
			}
			if (this.m_btnRewardInfo != null)
			{
				this.m_btnRewardInfo.PointerClick.RemoveAllListeners();
				this.m_btnRewardInfo.PointerClick.AddListener(new UnityAction(this.OnClickRewardInfo));
			}
			if (this.m_btnReward != null)
			{
				this.m_btnReward.PointerClick.RemoveAllListeners();
				this.m_btnReward.PointerClick.AddListener(new UnityAction(this.OnClickReward));
				this.m_btnReward.m_bGetCallbackWhileLocked = true;
			}
			if (this.m_btnSeasonSelect != null)
			{
				this.m_btnSeasonSelect.PointerClick.RemoveAllListeners();
				this.m_btnSeasonSelect.PointerClick.AddListener(new UnityAction(this.OnClickSeasonSelect));
			}
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x001DB07C File Offset: 0x001D927C
		public RectTransform GetSubTabObject(int index)
		{
			if (this.m_stkSubTabPool.Count > 0)
			{
				NKCUILeaderBoardSubTab nkcuileaderBoardSubTab = this.m_stkSubTabPool.Pop();
				nkcuileaderBoardSubTab.transform.SetParent(this.m_trSubTabSlotParent);
				this.m_lstVisibleSubTab.Add(nkcuileaderBoardSubTab);
				return nkcuileaderBoardSubTab.GetComponent<RectTransform>();
			}
			NKCUILeaderBoardSubTab nkcuileaderBoardSubTab2 = UnityEngine.Object.Instantiate<NKCUILeaderBoardSubTab>(this.m_pfbSubTab);
			nkcuileaderBoardSubTab2.transform.SetParent(this.m_trSubTabSlotParent);
			return nkcuileaderBoardSubTab2.GetComponent<RectTransform>();
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x001DB0E8 File Offset: 0x001D92E8
		public void ReturnSubTabObject(Transform tr)
		{
			NKCUILeaderBoardSubTab component = tr.GetComponent<NKCUILeaderBoardSubTab>();
			if (component != null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				tr.SetParent(base.gameObject.transform);
				this.m_lstVisibleSubTab.Remove(component);
				this.m_stkSubTabPool.Push(component);
			}
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x001DB138 File Offset: 0x001D9338
		public void ProvideSubTabData(Transform tr, int idx)
		{
			if (this.m_dicTabTemplet[this.m_CurrentBoardType].Count > idx + 1)
			{
				NKCUILeaderBoardSubTab component = tr.GetComponent<NKCUILeaderBoardSubTab>();
				NKCUtil.SetGameobjectActive(component, true);
				this.m_lstVisibleSubTab.Add(component);
				component.SetData(this.m_subTabTglGroup, this.m_dicTabTemplet[this.m_CurrentBoardType][idx + 1].GetTabName(), this.m_dicTabTemplet[this.m_CurrentBoardType][idx + 1].m_BoardID, new NKCUILeaderBoardSubTab.OnSelectSubTab(this.OnSelectTab));
				return;
			}
			NKCUtil.SetGameobjectActive(tr, false);
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x001DB1D8 File Offset: 0x001D93D8
		public RectTransform GetContentObject(int index)
		{
			if (this.m_stkSlotPool.Count > 0)
			{
				NKCUILeaderBoardSlotAll nkcuileaderBoardSlotAll = this.m_stkSlotPool.Pop();
				nkcuileaderBoardSlotAll.transform.SetParent(this.m_trContentSlotParent);
				this.m_lstVisibleSlot.Add(nkcuileaderBoardSlotAll);
				NKCUtil.SetGameobjectActive(nkcuileaderBoardSlotAll, false);
				return nkcuileaderBoardSlotAll.GetComponent<RectTransform>();
			}
			NKCUILeaderBoardSlotAll nkcuileaderBoardSlotAll2 = UnityEngine.Object.Instantiate<NKCUILeaderBoardSlotAll>(this.m_pfbSlot);
			nkcuileaderBoardSlotAll2.InitUI();
			nkcuileaderBoardSlotAll2.transform.SetParent(this.m_trContentSlotParent);
			this.m_lstVisibleSlot.Add(nkcuileaderBoardSlotAll2);
			NKCUtil.SetGameobjectActive(nkcuileaderBoardSlotAll2, false);
			return nkcuileaderBoardSlotAll2.GetComponent<RectTransform>();
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x001DB268 File Offset: 0x001D9468
		public void ReturnContentObject(Transform tr)
		{
			NKCUILeaderBoardSlotAll component = tr.GetComponent<NKCUILeaderBoardSlotAll>();
			if (component != null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				this.m_lstVisibleSlot.Remove(component);
				this.m_stkSlotPool.Push(component);
				tr.SetParent(base.transform, false);
			}
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x001DB2B4 File Offset: 0x001D94B4
		public void ProvideContentData(Transform tr, int idx)
		{
			NKCUILeaderBoardSlotAll component = tr.GetComponent<NKCUILeaderBoardSlotAll>();
			if (component == null)
			{
				return;
			}
			switch (this.m_cNKMLeaderBoardTemplet.m_LayoutType)
			{
			case LayoutType.NORMAL:
			case LayoutType.TOP_3_ONLY:
				return;
			case LayoutType.TOP_3_AND_RANKING:
			{
				List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
				list.AddRange(NKCLeaderBoardManager.GetLeaderBoardData(this.m_cNKMLeaderBoardTemplet.m_BoardID));
				if (idx == 0)
				{
					for (int i = list.Count; i < 3; i++)
					{
						LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(this.m_cNKMLeaderBoardTemplet.m_BoardTab, this.CheckUseGuildSlot(this.m_cNKMLeaderBoardTemplet.m_BoardTab), i + 1);
						list.Add(item);
					}
					component.transform.SetParent(this.m_trContentSlotParent);
					NKCUtil.SetGameobjectActive(component, true);
					component.GetComponent<NKCUILeaderBoardSlotAll>().SetData(list[idx], list[idx + 1], list[idx + 2], this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, false, new NKCUILeaderBoardSlot.OnDragBegin(this.OnEventPanelBeginDragAll));
					return;
				}
				if (list.Count < idx + 2)
				{
					return;
				}
				component.transform.SetParent(this.m_trContentSlotParent);
				NKCUtil.SetGameobjectActive(component, true);
				component.GetComponent<NKCUILeaderBoardSlotAll>().SetData(NKCLeaderBoardManager.GetLeaderBoardData(this.m_cNKMLeaderBoardTemplet.m_BoardID)[idx + 2], this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, new NKCUILeaderBoardSlot.OnDragBegin(this.OnEventPanelBeginDragAll));
				return;
			}
			}
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x001DB418 File Offset: 0x001D9618
		public void Open(NKMLeaderBoardTemplet reservedTemplet = null, bool bFirstOpen = true)
		{
			if (NKCScenManager.CurrentUserData().UserProfileData == null)
			{
				NKCPacketSender.Send_NKMPacket_MY_USER_PROFILE_INFO_REQ();
			}
			this.SetLeaderBoardSubTabTemplets();
			LeaderBoardType boardType = LeaderBoardType.BT_ACHIEVE;
			if (reservedTemplet != null)
			{
				boardType = reservedTemplet.m_BoardTab;
				this.m_cNKMLeaderBoardTemplet = null;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_LoopScrollContent != null)
			{
				this.m_LoopScrollContent.TotalCount = 0;
				this.m_LoopScrollContent.SetIndexPosition(0);
			}
			if (bFirstOpen)
			{
				base.UIOpened(true);
			}
			this.OnClickMainTab(boardType);
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x001DB494 File Offset: 0x001D9694
		private void SetLeaderBoardSubTabTemplets()
		{
			this.m_dicTabTemplet.Clear();
			using (IEnumerator<NKMLeaderBoardTemplet> enumerator = NKMTempletContainer<NKMLeaderBoardTemplet>.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMLeaderBoardTemplet templet = enumerator.Current;
					if (templet.m_BoardTab != LeaderBoardType.BT_GUILD || NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_RANKING, 0, 0))
					{
						if (this.m_dicTabTemplet.ContainsKey(templet.m_BoardTab))
						{
							if (this.m_dicTabTemplet[templet.m_BoardTab].Find((NKMLeaderBoardTemplet x) => x.m_BoardTabSubIndex == templet.m_BoardTabSubIndex) == null)
							{
								this.m_dicTabTemplet[templet.m_BoardTab].Add(templet);
							}
						}
						else
						{
							List<NKMLeaderBoardTemplet> list = new List<NKMLeaderBoardTemplet>();
							list.Add(templet);
							this.m_dicTabTemplet.Add(templet.m_BoardTab, list);
						}
					}
				}
			}
			foreach (List<NKMLeaderBoardTemplet> list2 in this.m_dicTabTemplet.Values)
			{
				list2.Sort(new Comparison<NKMLeaderBoardTemplet>(this.CompLeaderBoard));
			}
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x001DB5F0 File Offset: 0x001D97F0
		private void SetSubTab(NKMLeaderBoardTemplet targetTemplet)
		{
			bool flag = false;
			if (this.m_dicTabTemplet.ContainsKey(targetTemplet.m_BoardTab))
			{
				flag = (this.m_dicTabTemplet[targetTemplet.m_BoardTab].Count > 2);
			}
			NKCUtil.SetGameobjectActive(this.m_objSubTabParent, flag);
			if (flag)
			{
				this.m_LoopScrollSubTab.TotalCount = this.m_dicTabTemplet[targetTemplet.m_BoardTab].Count - 1;
				this.m_LoopScrollSubTab.RefreshCells(false);
				this.m_rtContent.offsetMin = new Vector2(274f, 0f);
				return;
			}
			this.m_rtContent.offsetMin = new Vector2(0f, 0f);
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x001DB6A0 File Offset: 0x001D98A0
		private int CompLeaderBoard(NKMLeaderBoardTemplet lItem, NKMLeaderBoardTemplet rItem)
		{
			if (lItem.m_OrderList != rItem.m_OrderList)
			{
				return rItem.m_OrderList.CompareTo(lItem.m_OrderList);
			}
			if (lItem.m_BoardTabSubIndex == rItem.m_BoardTabSubIndex)
			{
				return lItem.m_BoardID.CompareTo(rItem.m_BoardID);
			}
			return lItem.m_BoardTabSubIndex.CompareTo(rItem.m_BoardTabSubIndex);
		}

		// Token: 0x06005F95 RID: 24469 RVA: 0x001DB700 File Offset: 0x001D9900
		public void OnClickMainTab(LeaderBoardType boardType)
		{
			if (this.m_CurrentBoardType == boardType)
			{
				return;
			}
			this.m_CurrentBoardType = boardType;
			if (!this.m_dicTabTemplet.ContainsKey(this.m_CurrentBoardType) || this.m_dicTabTemplet[this.m_CurrentBoardType].Count == 0)
			{
				Debug.LogError(string.Format("{0} 데이터 없음", this.m_CurrentBoardType));
				return;
			}
			if (this.m_dicBoardTab.ContainsKey(boardType))
			{
				this.m_dicBoardTab[boardType].GetComponent<NKCUIComToggle>().Select(true, true, false);
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet;
			if (this.m_dicTabTemplet[this.m_CurrentBoardType].Count > 1)
			{
				nkmleaderBoardTemplet = this.m_dicTabTemplet[this.m_CurrentBoardType][1];
			}
			else
			{
				nkmleaderBoardTemplet = this.m_dicTabTemplet[this.m_CurrentBoardType][0];
			}
			this.SetSubTab(nkmleaderBoardTemplet);
			this.OnSelectTab(nkmleaderBoardTemplet.m_BoardID);
		}

		// Token: 0x06005F96 RID: 24470 RVA: 0x001DB7EC File Offset: 0x001D99EC
		private void OnSelectTab(int boardTabID)
		{
			if (this.m_cNKMLeaderBoardTemplet != null && this.m_cNKMLeaderBoardTemplet.m_BoardID == boardTabID)
			{
				return;
			}
			this.m_cNKMLeaderBoardTemplet = NKMTempletContainer<NKMLeaderBoardTemplet>.Find(boardTabID);
			if (this.m_cNKMLeaderBoardTemplet == null)
			{
				Log.Error(string.Format("NKMLeaderBoardTemplet is null - TabID : {0} ", boardTabID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/LeaderBoard/NKCUILeaderBoard.cs", 478);
				return;
			}
			this.m_CurrentBoardType = this.m_cNKMLeaderBoardTemplet.m_BoardTab;
			foreach (NKCUILeaderBoardTab nkcuileaderBoardTab in this.m_dicBoardTab.Values)
			{
				if (nkcuileaderBoardTab != null)
				{
					nkcuileaderBoardTab.SetTitleColor(nkcuileaderBoardTab.m_tgl.m_bChecked);
				}
			}
			for (int i = 0; i < this.m_lstVisibleSubTab.Count; i++)
			{
				if (this.m_lstVisibleSubTab[i].gameObject.activeSelf && this.m_lstVisibleSubTab[i].m_tabID == boardTabID)
				{
					this.m_lstVisibleSubTab[i].m_tgl.Select(true, false, false);
					break;
				}
			}
			if (this.m_imgBanner != null && (this.m_imgBanner.sprite == null || !string.Equals(this.m_imgBanner.sprite.name, this.m_cNKMLeaderBoardTemplet.m_BoardBackgroundImg)))
			{
				NKCUtil.SetImageSprite(this.m_imgBanner, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_BG", this.m_cNKMLeaderBoardTemplet.m_BoardBackgroundImg, false), false);
			}
			if (!NKCLeaderBoardManager.HasLeaderBoardData(this.m_cNKMLeaderBoardTemplet))
			{
				NKCLeaderBoardManager.SendReq(this.m_cNKMLeaderBoardTemplet, false);
				return;
			}
			this.RefreshUI(true);
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x001DB998 File Offset: 0x001D9B98
		public void RefreshUI(bool bResetScroll = false)
		{
			foreach (KeyValuePair<LeaderBoardType, NKCUILeaderBoardTab> keyValuePair in this.m_dicBoardTab)
			{
				keyValuePair.Value.CheckRedDot();
			}
			List<LeaderBoardSlotData> leaderBoardData = NKCLeaderBoardManager.GetLeaderBoardData(this.m_cNKMLeaderBoardTemplet.m_BoardID);
			switch (this.m_cNKMLeaderBoardTemplet.m_LayoutType)
			{
			case LayoutType.NORMAL:
				this.m_LoopScrollContent.TotalCount = leaderBoardData.Count;
				goto IL_FB;
			case LayoutType.TOP_3_ONLY:
				if (leaderBoardData.Count == 0)
				{
					this.m_LoopScrollContent.TotalCount = 0;
					goto IL_FB;
				}
				this.m_LoopScrollContent.TotalCount = Mathf.CeilToInt((float)leaderBoardData.Count / 3f);
				goto IL_FB;
			case LayoutType.TOP_3_AND_RANKING:
				if (leaderBoardData.Count == 0)
				{
					this.m_LoopScrollContent.TotalCount = 0;
					goto IL_FB;
				}
				this.m_LoopScrollContent.TotalCount = ((leaderBoardData.Count > 2) ? (leaderBoardData.Count - 2) : 1);
				goto IL_FB;
			}
			this.m_LoopScrollContent.TotalCount = 0;
			IL_FB:
			NKCUtil.SetGameobjectActive(this.m_lbRemainTime, this.IsShowRemainTime());
			if (this.m_lbRemainTime.gameObject.activeSelf)
			{
				this.m_tNextResetTime = NKCLeaderBoardManager.GetNextResetTime(this.m_cNKMLeaderBoardTemplet);
				this.SetRemainTime(this.m_tNextResetTime);
			}
			if (bResetScroll)
			{
				this.m_LoopScrollContent.SetIndexPosition(0);
			}
			else
			{
				this.m_LoopScrollContent.RefreshCells(false);
			}
			NKCUtil.SetLabelText(this.m_lbTitle, this.m_cNKMLeaderBoardTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_lbDesc, this.m_cNKMLeaderBoardTemplet.GetDesc());
			this.SetMyRank();
			if (NKCLeaderBoardManager.GetLeaderBoardData(this.m_cNKMLeaderBoardTemplet.m_BoardID).Count > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objNone, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNone, true);
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x001DBB70 File Offset: 0x001D9D70
		private bool IsShowRemainTime()
		{
			switch (this.m_cNKMLeaderBoardTemplet.m_BoardTab)
			{
			default:
				return false;
			}
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x001DBBB0 File Offset: 0x001D9DB0
		private void SetRemainTime(DateTime nextResetTime)
		{
			if (nextResetTime > DateTime.MinValue)
			{
				NKCUtil.SetGameobjectActive(this.m_lbRemainTime, true);
				NKCUtil.SetLabelText(this.m_lbRemainTime, "");
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbRemainTime, false);
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x001DBBE8 File Offset: 0x001D9DE8
		private void SetMyRank()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_slotMyRank, false);
				return;
			}
			int num = 0;
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			NKMCommonProfile nkmcommonProfile;
			if (userProfileData != null)
			{
				nkmcommonProfile = userProfileData.commonProfile;
			}
			else
			{
				nkmcommonProfile = new NKMCommonProfile();
				nkmcommonProfile.friendCode = nkmuserData.m_FriendCode;
				nkmcommonProfile.level = nkmuserData.UserLevel;
				nkmcommonProfile.nickname = nkmuserData.m_UserNickName;
				nkmcommonProfile.userUid = nkmuserData.m_UserUID;
			}
			switch (this.m_cNKMLeaderBoardTemplet.m_BoardTab)
			{
			case LeaderBoardType.BT_ACHIEVE:
			{
				NKCUtil.SetGameobjectActive(this.m_slotMyRank, true);
				NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, false);
				LeaderBoardSlotData myRankSlotData = NKCLeaderBoardManager.GetMyRankSlotData(this.m_cNKMLeaderBoardTemplet.m_BoardID);
				myRankSlotData.Profile = nkmcommonProfile;
				if (NKCGuildManager.HasGuild())
				{
					myRankSlotData.GuildData.badgeId = NKCGuildManager.MyGuildData.badgeId;
					myRankSlotData.GuildData.guildName = NKCGuildManager.MyGuildData.name;
					myRankSlotData.GuildData.guildUid = NKCGuildManager.MyGuildData.guildUid;
				}
				this.m_slotMyRank.SetData(myRankSlotData, this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, false);
				NKCUtil.SetGameobjectActive(this.m_btnRewardInfo, false);
				NKCUtil.SetGameobjectActive(this.m_btnReward, false);
				return;
			}
			case LeaderBoardType.BT_SHADOW:
			{
				NKCUtil.SetGameobjectActive(this.m_slotMyRank, true);
				NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, false);
				LeaderBoardSlotData myRankSlotData2 = NKCLeaderBoardManager.GetMyRankSlotData(this.m_cNKMLeaderBoardTemplet.m_BoardID);
				myRankSlotData2.Profile = nkmcommonProfile;
				if (NKCGuildManager.HasGuild())
				{
					myRankSlotData2.GuildData.badgeId = NKCGuildManager.MyGuildData.badgeId;
					myRankSlotData2.GuildData.guildName = NKCGuildManager.MyGuildData.name;
					myRankSlotData2.GuildData.guildUid = NKCGuildManager.MyGuildData.guildUid;
				}
				this.m_slotMyRank.SetData(myRankSlotData2, this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, false);
				NKCUtil.SetGameobjectActive(this.m_btnRewardInfo, false);
				NKCUtil.SetGameobjectActive(this.m_btnReward, false);
				return;
			}
			case LeaderBoardType.BT_FIERCE:
			{
				NKCUtil.SetGameobjectActive(this.m_slotMyRank, true);
				NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, false);
				LeaderBoardSlotData myRankSlotData3 = NKCLeaderBoardManager.GetMyRankSlotData(this.m_cNKMLeaderBoardTemplet.m_BoardID);
				myRankSlotData3.Profile = nkmcommonProfile;
				if (NKCGuildManager.HasGuild())
				{
					myRankSlotData3.GuildData.badgeId = NKCGuildManager.MyGuildData.badgeId;
					myRankSlotData3.GuildData.guildName = NKCGuildManager.MyGuildData.name;
					myRankSlotData3.GuildData.guildUid = NKCGuildManager.MyGuildData.guildUid;
				}
				num = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetRankingTotalNumber();
				if (num != 0 && num <= 100)
				{
					this.m_slotMyRank.SetData(myRankSlotData3, this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, false);
				}
				else
				{
					this.m_slotMyRank.SetData(myRankSlotData3, this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, true, false);
				}
				NKCUtil.SetGameobjectActive(this.m_btnRewardInfo, true);
				return;
			}
			case LeaderBoardType.BT_GUILD:
			{
				NKCUtil.SetGameobjectActive(this.m_slotMyRank, true);
				NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, this.m_cNKMLeaderBoardTemplet.m_BoardCriteria != 1);
				NKCUtil.SetGameobjectActive(this.m_btnRewardInfo, false);
				NKCUtil.SetGameobjectActive(this.m_btnReward, false);
				NKMGuildRankData rankData = NKCLeaderBoardManager.MakeMyGuildRankData(this.m_cNKMLeaderBoardTemplet.m_BoardID, out num);
				this.m_slotMyRank.SetData(LeaderBoardSlotData.MakeSlotData(rankData, num), this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, false);
				return;
			}
			case LeaderBoardType.BT_TIMEATTACK:
			{
				NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, false);
				NKCUtil.SetGameobjectActive(this.m_slotMyRank, true);
				NKCUtil.SetGameobjectActive(this.m_btnRewardInfo, false);
				NKCUtil.SetGameobjectActive(this.m_btnReward, false);
				LeaderBoardSlotData myRankSlotData4 = NKCLeaderBoardManager.GetMyRankSlotData(this.m_cNKMLeaderBoardTemplet.m_BoardID);
				myRankSlotData4.Profile = nkmcommonProfile;
				if (NKCGuildManager.HasGuild())
				{
					myRankSlotData4.GuildData.badgeId = NKCGuildManager.MyGuildData.badgeId;
					myRankSlotData4.GuildData.guildName = NKCGuildManager.MyGuildData.name;
					myRankSlotData4.GuildData.guildUid = NKCGuildManager.MyGuildData.guildUid;
				}
				this.m_slotMyRank.SetData(myRankSlotData4, this.m_cNKMLeaderBoardTemplet.m_BoardCriteria, null, false, true);
				return;
			}
			}
			NKCUtil.SetGameobjectActive(this.m_btnSeasonSelect, false);
			NKCUtil.SetGameobjectActive(this.m_slotMyRank, false);
			NKCUtil.SetGameobjectActive(this.m_btnRewardInfo, false);
			NKCUtil.SetGameobjectActive(this.m_btnReward, false);
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x001DC020 File Offset: 0x001DA220
		public void Update()
		{
			if (this.m_lbRemainTime.gameObject.activeSelf && this.m_tNextResetTime > DateTime.MinValue)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime = 0f;
					if (!NKCSynchronizedTime.IsFinished(this.m_tNextResetTime))
					{
						this.m_tNextResetTime = DateTime.MinValue;
						NKCLeaderBoardManager.SendReq(this.m_cNKMLeaderBoardTemplet, true);
						return;
					}
					this.SetRemainTime(this.m_tNextResetTime);
				}
			}
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x001DC0AC File Offset: 0x001DA2AC
		public void OnClickRewardInfo()
		{
			if (this.m_cNKMLeaderBoardTemplet.m_BoardTab == LeaderBoardType.BT_FIERCE)
			{
				NKCUIPopupFierceBattleRewardInfo.Instance.Open();
			}
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x001DC0C6 File Offset: 0x001DA2C6
		public void OnClickReward()
		{
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x001DC0C8 File Offset: 0x001DA2C8
		public void OnClickSeasonSelect()
		{
			NKCPopupGuildRankSeasonSelect.Instance.Open(new NKCPopupGuildRankSeasonSelect.OnSelectSeason(this.OnSelectSeason), this.m_cNKMLeaderBoardTemplet.m_BoardCriteria);
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x001DC0EB File Offset: 0x001DA2EB
		private void OnSelectSeason(int seasonId)
		{
			this.m_cNKMLeaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, seasonId);
			if (!NKCLeaderBoardManager.HasLeaderBoardData(this.m_cNKMLeaderBoardTemplet))
			{
				NKCLeaderBoardManager.SendReq(this.m_cNKMLeaderBoardTemplet, false);
				return;
			}
			this.RefreshUI(false);
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x001DC11B File Offset: 0x001DA31B
		private bool CheckUseGuildSlot(LeaderBoardType type)
		{
			return type == LeaderBoardType.BT_GUILD;
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x001DC124 File Offset: 0x001DA324
		private void OnEventPanelBeginDragAll()
		{
			if (!NKCLeaderBoardManager.GetReceivedAllData(this.m_cNKMLeaderBoardTemplet.m_BoardID) || NKCLeaderBoardManager.NeedRefreshData(this.m_cNKMLeaderBoardTemplet))
			{
				NKCLeaderBoardManager.SendReq(this.m_cNKMLeaderBoardTemplet, true);
			}
		}

		// Token: 0x04004B95 RID: 19349
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_LEADER_BOARD";

		// Token: 0x04004B96 RID: 19350
		private const string UI_ASSET_NAME = "NKM_UI_LEADER_BOARD";

		// Token: 0x04004B97 RID: 19351
		private static NKCUILeaderBoard m_Instance;

		// Token: 0x04004B98 RID: 19352
		[Header("좌측 탭 - 풀스크린UI전용")]
		public NKCUILeaderBoardTab m_pfbTab;

		// Token: 0x04004B99 RID: 19353
		public NKCUIComToggleGroup m_tabTglGroup;

		// Token: 0x04004B9A RID: 19354
		public ScrollRect m_ScrollTab;

		// Token: 0x04004B9B RID: 19355
		public Transform m_trTabParent;

		// Token: 0x04004B9C RID: 19356
		[Header("서브탭")]
		public NKCUILeaderBoardSubTab m_pfbSubTab;

		// Token: 0x04004B9D RID: 19357
		public GameObject m_objSubTabParent;

		// Token: 0x04004B9E RID: 19358
		public NKCUIComToggleGroup m_subTabTglGroup;

		// Token: 0x04004B9F RID: 19359
		public LoopScrollRect m_LoopScrollSubTab;

		// Token: 0x04004BA0 RID: 19360
		public Transform m_trSubTabSlotParent;

		// Token: 0x04004BA1 RID: 19361
		[Header("컨텐츠")]
		public Text m_lbRemainTime;

		// Token: 0x04004BA2 RID: 19362
		public NKCUILeaderBoardSlotAll m_pfbSlot;

		// Token: 0x04004BA3 RID: 19363
		public RectTransform m_rtContent;

		// Token: 0x04004BA4 RID: 19364
		public Text m_lbTitle;

		// Token: 0x04004BA5 RID: 19365
		public Text m_lbDesc;

		// Token: 0x04004BA6 RID: 19366
		public Image m_imgBanner;

		// Token: 0x04004BA7 RID: 19367
		public NKCUIComStateButton m_btnSeasonSelect;

		// Token: 0x04004BA8 RID: 19368
		public LoopVerticalScrollFlexibleRect m_LoopScrollContent;

		// Token: 0x04004BA9 RID: 19369
		public Transform m_trContentSlotParent;

		// Token: 0x04004BAA RID: 19370
		public GameObject m_objNone;

		// Token: 0x04004BAB RID: 19371
		[Header("보상확인")]
		public NKCUIComStateButton m_btnRewardInfo;

		// Token: 0x04004BAC RID: 19372
		[Header("내 랭킹")]
		public NKCUILeaderBoardSlot m_slotMyRank;

		// Token: 0x04004BAD RID: 19373
		[Header("보상수령")]
		public NKCUIComStateButton m_btnReward;

		// Token: 0x04004BAE RID: 19374
		private Dictionary<LeaderBoardType, List<NKMLeaderBoardTemplet>> m_dicTabTemplet = new Dictionary<LeaderBoardType, List<NKMLeaderBoardTemplet>>();

		// Token: 0x04004BAF RID: 19375
		private Dictionary<LeaderBoardType, NKCUILeaderBoardTab> m_dicBoardTab = new Dictionary<LeaderBoardType, NKCUILeaderBoardTab>();

		// Token: 0x04004BB0 RID: 19376
		private List<NKCUILeaderBoardSubTab> m_lstVisibleSubTab = new List<NKCUILeaderBoardSubTab>();

		// Token: 0x04004BB1 RID: 19377
		private Stack<NKCUILeaderBoardSubTab> m_stkSubTabPool = new Stack<NKCUILeaderBoardSubTab>();

		// Token: 0x04004BB2 RID: 19378
		private List<NKCUILeaderBoardSlotAll> m_lstVisibleSlot = new List<NKCUILeaderBoardSlotAll>();

		// Token: 0x04004BB3 RID: 19379
		private Stack<NKCUILeaderBoardSlotAll> m_stkSlotPool = new Stack<NKCUILeaderBoardSlotAll>();

		// Token: 0x04004BB4 RID: 19380
		private LeaderBoardType m_CurrentBoardType = LeaderBoardType.BT_NONE;

		// Token: 0x04004BB5 RID: 19381
		private NKMLeaderBoardTemplet m_cNKMLeaderBoardTemplet;

		// Token: 0x04004BB6 RID: 19382
		private DateTime m_tNextResetTime = DateTime.MinValue;

		// Token: 0x04004BB7 RID: 19383
		private float m_fDeltaTime;
	}
}

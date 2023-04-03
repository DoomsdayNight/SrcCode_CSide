using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.LeaderBoard;
using ClientPacket.Mode;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009DC RID: 2524
	public class NKCUIShadowPalace : NKCUIBase
	{
		// Token: 0x06006C56 RID: 27734 RVA: 0x002363CE File Offset: 0x002345CE
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIShadowPalace.s_LoadedUIData))
			{
				NKCUIShadowPalace.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIShadowPalace>("AB_UI_OPERATION_SHADOW", "AB_UI_OPERATION_SHADOW", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIShadowPalace.CleanupInstance));
			}
			return NKCUIShadowPalace.s_LoadedUIData;
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06006C57 RID: 27735 RVA: 0x00236402 File Offset: 0x00234602
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIShadowPalace.s_LoadedUIData != null && NKCUIShadowPalace.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06006C58 RID: 27736 RVA: 0x00236417 File Offset: 0x00234617
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIShadowPalace.s_LoadedUIData != null && NKCUIShadowPalace.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06006C59 RID: 27737 RVA: 0x0023642C File Offset: 0x0023462C
		public static NKCUIShadowPalace GetInstance()
		{
			if (NKCUIShadowPalace.s_LoadedUIData != null && NKCUIShadowPalace.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIShadowPalace.s_LoadedUIData.GetInstance<NKCUIShadowPalace>();
			}
			return null;
		}

		// Token: 0x06006C5A RID: 27738 RVA: 0x0023644D File Offset: 0x0023464D
		public static void CleanupInstance()
		{
			NKCUIShadowPalace.s_LoadedUIData = null;
		}

		// Token: 0x06006C5B RID: 27739 RVA: 0x00236455 File Offset: 0x00234655
		public int GetCurrMultiplyRewardCount()
		{
			return this.m_CurrSkipCount;
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06006C5C RID: 27740 RVA: 0x0023645D File Offset: 0x0023465D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06006C5D RID: 27741 RVA: 0x00236460 File Offset: 0x00234660
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_SHADOW_PALACE;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06006C5E RID: 27742 RVA: 0x00236467 File Offset: 0x00234667
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SHADOW_PALACE_INFO";
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06006C5F RID: 27743 RVA: 0x0023646E File Offset: 0x0023466E
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.m_lstUpsideResource;
			}
		}

		// Token: 0x06006C60 RID: 27744 RVA: 0x00236478 File Offset: 0x00234678
		public void Init()
		{
			NKCUIShadowPalaceInfo palaceInfo = this.m_palaceInfo;
			if (palaceInfo != null)
			{
				palaceInfo.Init(new NKCUIShadowPalaceInfo.OnTouchStart(this.OnTouchPalaceStart), new NKCUIShadowPalaceInfo.OnTouchProgress(this.OnTouchPalaceProgress), new NKCUIShadowPalaceInfo.OnTouchRank(this.OnTouchRank));
			}
			NKCUIShadowPalaceRank palaceRank = this.m_palaceRank;
			if (palaceRank != null)
			{
				palaceRank.Init();
			}
			NKCUtil.SetGameobjectActive(this.m_palaceRank, false);
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.dOnGetObject += this.OnGetObject;
				this.m_scrollRect.dOnProvideData += this.OnProvideData;
				this.m_scrollRect.dOnReturnObject += this.OnReturnObject;
				this.m_scrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_scrollRect, null);
			}
			this.m_btnShortcut.PointerClick.RemoveAllListeners();
			this.m_btnShortcut.PointerClick.AddListener(new UnityAction(this.MoveToRank));
			NKCUtil.SetButtonClickDelegate(this.m_btnShop, new UnityAction(this.OnShopShortcut));
			this.m_tglSkip.OnValueChanged.RemoveAllListeners();
			this.m_tglSkip.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickSkip));
			this.m_CurrSkipCount = 1;
			this.m_NKCUIOperationSkip.Init(new NKCUIOperationSkip.OnCountUpdated(this.OnSkipCountUpdate), new UnityAction(this.OnSkipClose));
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x002365DC File Offset: 0x002347DC
		public void Open(int current_palace_id)
		{
			this.m_lstPalaceTemplet = NKMTempletContainer<NKMShadowPalaceTemplet>.Values.ToList<NKMShadowPalaceTemplet>();
			this.m_lstPalaceTemplet.Sort((NKMShadowPalaceTemplet a, NKMShadowPalaceTemplet b) => a.PALACE_NUM_UI.CompareTo(b.PALACE_NUM_UI));
			this.m_selectPalaceID = current_palace_id;
			this.m_bOperationSkip = false;
			if (this.m_selectPalaceID == 0)
			{
				this.m_selectPalaceID = NKMShadowPalaceManager.GetLastClearedPalace();
				NKMShadowPalaceTemplet nkmshadowPalaceTemplet = this.m_lstPalaceTemplet.Find((NKMShadowPalaceTemplet x) => x.PALACE_ID == this.m_selectPalaceID);
				if (nkmshadowPalaceTemplet != null)
				{
					NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
					UnlockInfo unlockInfo = new UnlockInfo(nkmshadowPalaceTemplet.STAGE_UNLOCK_REQ_TYPE, nkmshadowPalaceTemplet.STAGE_UNLOCK_REQ_VALUE);
					if (NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false))
					{
						goto IL_9E;
					}
				}
				this.m_selectPalaceID = 0;
			}
			IL_9E:
			if (this.m_selectPalaceID == 0)
			{
				NKMUserData userData = NKCScenManager.CurrentUserData();
				NKMShadowPalaceTemplet nkmshadowPalaceTemplet2 = this.m_lstPalaceTemplet.FindLast(delegate(NKMShadowPalaceTemplet v)
				{
					UnlockInfo unlockInfo2 = new UnlockInfo(v.STAGE_UNLOCK_REQ_TYPE, v.STAGE_UNLOCK_REQ_VALUE);
					return NKMContentUnlockManager.IsContentUnlocked(userData, unlockInfo2, false);
				});
				if (nkmshadowPalaceTemplet2 != null)
				{
					this.m_selectPalaceID = nkmshadowPalaceTemplet2.PALACE_ID;
				}
				else
				{
					if (this.m_lstPalaceTemplet.Count <= 0)
					{
						Debug.LogError("Can't Find Shadow Palace Templet");
						return;
					}
					this.m_selectPalaceID = this.m_lstPalaceTemplet[0].PALACE_ID;
				}
			}
			this.m_scrollRect.TotalCount = this.m_lstPalaceTemplet.Count;
			this.m_scrollRect.RefreshCells(false);
			this.m_lstUpsideResource.Add(1);
			this.m_lstUpsideResource.Add(19);
			this.m_lstUpsideResource.Add(20);
			this.SetPalaceInfo(this.m_selectPalaceID, true);
			base.UIOpened(true);
			base.StartCoroutine(this.Intro());
		}

		// Token: 0x06006C62 RID: 27746 RVA: 0x00236764 File Offset: 0x00234964
		private IEnumerator Intro()
		{
			yield return null;
			this.MovePalaceSlot(this.m_selectPalaceID);
			this.CheckTutorial();
			yield break;
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x00236773 File Offset: 0x00234973
		public override void CloseInternal()
		{
		}

		// Token: 0x06006C64 RID: 27748 RVA: 0x00236775 File Offset: 0x00234975
		public override void UnHide()
		{
			base.UnHide();
			this.MovePalaceSlot(this.m_selectPalaceID);
		}

		// Token: 0x06006C65 RID: 27749 RVA: 0x00236789 File Offset: 0x00234989
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			this.SetPalaceInfo(this.m_selectPalaceID, false);
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x00236798 File Offset: 0x00234998
		private RectTransform OnGetObject(int index)
		{
			if (this.m_stkPalaceSlotPool.Count > 0)
			{
				return this.m_stkPalaceSlotPool.Pop().GetComponent<RectTransform>();
			}
			NKCUIShadowPalaceSlot nkcuishadowPalaceSlot = UnityEngine.Object.Instantiate<NKCUIShadowPalaceSlot>(this.m_palacePrefab);
			nkcuishadowPalaceSlot.transform.SetParent(this.m_trPalace);
			nkcuishadowPalaceSlot.Init();
			this.m_lstPalaceSlot.Add(nkcuishadowPalaceSlot);
			return nkcuishadowPalaceSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x002367FC File Offset: 0x002349FC
		private void OnProvideData(Transform tr, int idx)
		{
			NKCUIShadowPalaceSlot component = tr.GetComponent<NKCUIShadowPalaceSlot>();
			if (component == null)
			{
				return;
			}
			NKMShadowPalaceTemplet palaceTemplet = this.m_lstPalaceTemplet[idx];
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMShadowPalace shadowPalace = nkmuserData.m_ShadowPalace;
			List<NKMPalaceData> palaceDataList = shadowPalace.palaceDataList;
			UnlockInfo unlockInfo = new UnlockInfo(palaceTemplet.STAGE_UNLOCK_REQ_TYPE, palaceTemplet.STAGE_UNLOCK_REQ_VALUE);
			bool flag = NKMContentUnlockManager.IsContentUnlocked(nkmuserData, unlockInfo, false);
			NKMPalaceData palaceData = palaceDataList.Find((NKMPalaceData v) => v.palaceId == palaceTemplet.PALACE_ID);
			component.SetData(palaceTemplet, palaceData, new NKCUIShadowPalaceSlot.OnTouchSlot(this.OnTouchPalaceSlot));
			component.SetLock(!flag);
			component.SetProgress(shadowPalace.currentPalaceId == palaceTemplet.PALACE_ID);
			component.SetLine(idx == 0, idx == this.m_lstPalaceTemplet.Count - 1);
			component.PlaySelect(palaceTemplet.PALACE_ID == this.m_selectPalaceID, false);
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x002368F2 File Offset: 0x00234AF2
		private void OnReturnObject(Transform go)
		{
			if (base.GetComponent<NKCUIShadowPalaceSlot>() != null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(base.transform);
			this.m_stkPalaceSlotPool.Push(go.GetComponent<NKCUIShadowPalaceSlot>());
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x00236928 File Offset: 0x00234B28
		private void MovePalaceSlot(int palaceID)
		{
			int num = this.m_lstPalaceTemplet.FindIndex((NKMShadowPalaceTemplet v) => v.PALACE_ID == palaceID);
			if (num < 0)
			{
				return;
			}
			this.m_scrollRect.SetIndexPosition(num);
			this.OnTouchPalaceSlot(palaceID);
		}

		// Token: 0x06006C6A RID: 27754 RVA: 0x00236978 File Offset: 0x00234B78
		private void SetPalaceInfo(int palaceID, bool bPlayIntroAni = true)
		{
			NKMShadowPalace shadowPalace = NKCScenManager.CurrentUserData().m_ShadowPalace;
			NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(palaceID);
			UnlockInfo unlockInfo = new UnlockInfo(palaceTemplet.STAGE_UNLOCK_REQ_TYPE, palaceTemplet.STAGE_UNLOCK_REQ_VALUE);
			bool bUnlock = NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), unlockInfo, false);
			bool flag = shadowPalace.currentPalaceId == palaceID;
			int currSkipCount = 1;
			if (flag || shadowPalace.currentPalaceId == 0)
			{
				currSkipCount = this.m_CurrSkipCount;
			}
			this.m_palaceInfo.SetData(palaceTemplet, currSkipCount, flag, bUnlock);
			if (bPlayIntroAni)
			{
				this.m_palaceInfo.PlayIntroAni();
			}
		}

		// Token: 0x06006C6B RID: 27755 RVA: 0x002369FC File Offset: 0x00234BFC
		private void OnTouchPalaceSlot(int palaceID)
		{
			for (int i = 0; i < this.m_lstPalaceSlot.Count; i++)
			{
				NKCUIShadowPalaceSlot nkcuishadowPalaceSlot = this.m_lstPalaceSlot[i];
				if (nkcuishadowPalaceSlot.PalaceID == palaceID)
				{
					nkcuishadowPalaceSlot.PlaySelect(true, true);
				}
				else if (nkcuishadowPalaceSlot.PalaceID == this.m_selectPalaceID)
				{
					nkcuishadowPalaceSlot.PlaySelect(false, true);
				}
				else
				{
					nkcuishadowPalaceSlot.PlaySelect(false, false);
				}
			}
			this.m_selectPalaceID = palaceID;
			this.m_tglSkip.Select(false, false, false);
			this.SetPalaceInfo(palaceID, true);
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x00236A80 File Offset: 0x00234C80
		private void OnTouchPalaceStart()
		{
			if (NKCScenManager.CurrentUserData().m_ShadowPalace.currentPalaceId != 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NED_FAIL_SHADOW_PALACE_DOING), null, "");
				return;
			}
			NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(this.m_selectPalaceID);
			if (palaceTemplet == null)
			{
				return;
			}
			if (this.m_CurrSkipCount > 1)
			{
				int num = palaceTemplet.STAGE_REQ_ITEM_COUNT * (this.m_CurrSkipCount - 1);
				if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(palaceTemplet.STAGE_REQ_ITEM_ID) < (long)num)
				{
					NKCShopManager.OpenItemLackPopup(palaceTemplet.STAGE_REQ_ITEM_ID, num);
					return;
				}
			}
			if (!this.m_bOperationSkip)
			{
				string content = string.Format(NKCUtilString.GET_SHADOW_PALACE_START_CONFIRM, palaceTemplet.PALACE_NUM_UI, palaceTemplet.PalaceName);
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, content, palaceTemplet.STAGE_REQ_ITEM_ID, palaceTemplet.STAGE_REQ_ITEM_COUNT, delegate()
				{
					NKCPacketSender.Send_NKMPacket_SHADOW_PALACE_START_REQ(this.m_selectPalaceID);
				}, null, false);
				return;
			}
			NKCPacketSender.Send_NKMPacket_SHADOW_PALACE_SKIP_REQ(this.m_selectPalaceID, this.m_CurrSkipCount);
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x00236B68 File Offset: 0x00234D68
		private void OnTouchPalaceProgress()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData.m_ShadowPalace.life <= 0)
			{
				Debug.LogError("그림자 전당 - 라이프 부족한데 들어가려고 함 - life " + nkmuserData.m_ShadowPalace.life.ToString());
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_SHADOW_BATTLE().SetShadowPalaceID(nkmuserData.m_ShadowPalace.currentPalaceId);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_BATTLE, true);
		}

		// Token: 0x06006C6E RID: 27758 RVA: 0x00236BD0 File Offset: 0x00234DD0
		private void OnTouchRank()
		{
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_SHADOW, this.m_selectPalaceID);
			if (nkmleaderBoardTemplet == null)
			{
				return;
			}
			if (!this.m_leaderBoardIDs.Contains(nkmleaderBoardTemplet.m_BoardID))
			{
				NKCLeaderBoardManager.SendReq(nkmleaderBoardTemplet, true);
				return;
			}
			this.OpenRank();
		}

		// Token: 0x06006C6F RID: 27759 RVA: 0x00236C10 File Offset: 0x00234E10
		public void OpenRank()
		{
			NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(this.m_selectPalaceID);
			if (palaceTemplet == null)
			{
				return;
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_SHADOW, this.m_selectPalaceID);
			if (nkmleaderBoardTemplet == null)
			{
				return;
			}
			if (!this.m_leaderBoardIDs.Contains(nkmleaderBoardTemplet.m_BoardID))
			{
				this.m_leaderBoardIDs.Add(nkmleaderBoardTemplet.m_BoardID);
			}
			NKCUtil.SetGameobjectActive(this.m_palaceRank, true);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			List<LeaderBoardSlotData> leaderBoardData = NKCLeaderBoardManager.GetLeaderBoardData(nkmleaderBoardTemplet.m_BoardID);
			int rank = NKCLeaderBoardManager.GetMyRankSlotData(nkmleaderBoardTemplet.m_BoardID).rank;
			NKMShadowPalaceData nkmshadowPalaceData = new NKMShadowPalaceData();
			nkmshadowPalaceData.commonProfile.userUid = nkmuserData.m_UserUID;
			nkmshadowPalaceData.commonProfile.friendCode = nkmuserData.m_FriendCode;
			nkmshadowPalaceData.commonProfile.nickname = nkmuserData.m_UserNickName;
			nkmshadowPalaceData.commonProfile.level = nkmuserData.m_UserLevel;
			NKMPalaceData nkmpalaceData = nkmuserData.m_ShadowPalace.palaceDataList.Find((NKMPalaceData v) => v.palaceId == this.m_selectPalaceID);
			List<NKMShadowBattleTemplet> battleTemplets = NKMShadowPalaceManager.GetBattleTemplets(this.m_selectPalaceID);
			int num = 0;
			if (nkmpalaceData != null && battleTemplets != null && nkmpalaceData.dungeonDataList.Count == battleTemplets.Count)
			{
				for (int i = 0; i < nkmpalaceData.dungeonDataList.Count; i++)
				{
					num += nkmpalaceData.dungeonDataList[i].bestTime;
				}
			}
			nkmshadowPalaceData.bestTime = num;
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				nkmshadowPalaceData.commonProfile.mainUnitId = userProfileData.commonProfile.mainUnitId;
				nkmshadowPalaceData.commonProfile.mainUnitSkinId = userProfileData.commonProfile.mainUnitSkinId;
				nkmshadowPalaceData.commonProfile.frameId = userProfileData.commonProfile.frameId;
			}
			LeaderBoardSlotData myRankSlotData = NKCLeaderBoardManager.GetMyRankSlotData(nkmleaderBoardTemplet.m_BoardID);
			this.m_palaceRank.SetData(palaceTemplet, leaderBoardData, myRankSlotData, rank);
			this.m_palaceRank.PlayAni(true);
		}

		// Token: 0x06006C70 RID: 27760 RVA: 0x00236DEA File Offset: 0x00234FEA
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeCategory(EPISODE_CATEGORY.EC_SHADOW);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x06006C71 RID: 27761 RVA: 0x00236E0C File Offset: 0x0023500C
		private void MoveToRank()
		{
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_SHADOW, this.m_selectPalaceID);
			if (nkmleaderBoardTemplet == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_RANKING, nkmleaderBoardTemplet.m_BoardID.ToString(), false);
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x00236E3D File Offset: 0x0023503D
		private void OnShopShortcut()
		{
			NKCUIShop.ShopShortcut("TAB_EXCHANGE_SHADOW_COIN", 0, 0);
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x00236E4B File Offset: 0x0023504B
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.ShadowPalace, true);
		}

		// Token: 0x06006C74 RID: 27764 RVA: 0x00236E58 File Offset: 0x00235058
		public RectTransform GetPalaceSlot(int palaceID)
		{
			int num = this.m_lstPalaceTemplet.FindIndex((NKMShadowPalaceTemplet v) => v.PALACE_ID == palaceID);
			if (num < 0)
			{
				return null;
			}
			this.m_scrollRect.SetIndexPosition(num);
			NKCUIShadowPalaceSlot[] componentsInChildren = this.m_scrollRect.content.GetComponentsInChildren<NKCUIShadowPalaceSlot>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i] != null && componentsInChildren[i].PalaceID == palaceID)
				{
					return componentsInChildren[i].GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x06006C75 RID: 27765 RVA: 0x00236EE0 File Offset: 0x002350E0
		private bool CheckSkip()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			if (NKMShadowPalaceManager.GetPalaceTemplet(this.m_selectPalaceID) == null)
			{
				return false;
			}
			List<NKMShadowBattleTemplet> battleTemplets = NKMShadowPalaceManager.GetBattleTemplets(this.m_selectPalaceID);
			if (battleTemplets == null)
			{
				return false;
			}
			NKMPalaceData nkmpalaceData = nkmuserData.m_ShadowPalace.palaceDataList.Find((NKMPalaceData x) => x.palaceId == this.m_selectPalaceID);
			if (nkmpalaceData == null)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NED_FAIL_SHADOW_PALACE_MULTIPLY_CLEAR_DUNGEON), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return false;
			}
			bool flag = true;
			using (List<NKMShadowBattleTemplet>.Enumerator enumerator = battleTemplets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMShadowBattleTemplet battleTemplet = enumerator.Current;
					if (nkmpalaceData.dungeonDataList.Find((NKMPalaceDungeonData x) => x.dungeonId == battleTemplet.DUNGEON_ID) == null)
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NED_FAIL_SHADOW_PALACE_MULTIPLY_CLEAR_DUNGEON), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return false;
			}
			if (nkmuserData.m_ShadowPalace.currentPalaceId > 0)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_SHADOW_SKIP_ERROR, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return false;
			}
			return true;
		}

		// Token: 0x06006C76 RID: 27766 RVA: 0x0023701C File Offset: 0x0023521C
		private void OnClickSkip(bool bSet)
		{
			if (bSet)
			{
				NKMShadowPalaceTemplet palaceTemplet = NKMShadowPalaceManager.GetPalaceTemplet(this.m_selectPalaceID);
				if (palaceTemplet == null)
				{
					return;
				}
				if (!this.CheckSkip())
				{
					this.m_tglSkip.Select(false, false, false);
					return;
				}
				this.m_bOperationSkip = true;
				NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, true);
				this.m_NKCUIOperationSkip.SetData(0, 0, palaceTemplet.STAGE_REQ_ITEM_ID, palaceTemplet.STAGE_REQ_ITEM_COUNT, this.m_CurrSkipCount, 1, palaceTemplet.MaxRewardMultiply);
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, bSet);
			if (!bSet)
			{
				this.m_bOperationSkip = false;
				this.m_CurrSkipCount = 1;
			}
			this.SetPalaceInfo(this.m_selectPalaceID, false);
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x002370B7 File Offset: 0x002352B7
		private void OnSkipCountUpdate(int count)
		{
			this.m_CurrSkipCount = count;
			this.SetPalaceInfo(this.m_selectPalaceID, false);
		}

		// Token: 0x06006C78 RID: 27768 RVA: 0x002370CD File Offset: 0x002352CD
		private void OnSkipClose()
		{
			this.m_tglSkip.Select(false, false, false);
		}

		// Token: 0x04005819 RID: 22553
		private const string BUNDLE_NAME = "AB_UI_OPERATION_SHADOW";

		// Token: 0x0400581A RID: 22554
		private const string ASSET_NAME = "AB_UI_OPERATION_SHADOW";

		// Token: 0x0400581B RID: 22555
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x0400581C RID: 22556
		[Header("PALACE")]
		public Transform m_trPalace;

		// Token: 0x0400581D RID: 22557
		public NKCUIShadowPalaceSlot m_palacePrefab;

		// Token: 0x0400581E RID: 22558
		public LoopScrollRect m_scrollRect;

		// Token: 0x0400581F RID: 22559
		[Header("PALACE INFO")]
		public NKCUIShadowPalaceInfo m_palaceInfo;

		// Token: 0x04005820 RID: 22560
		public NKCUIShadowPalaceRank m_palaceRank;

		// Token: 0x04005821 RID: 22561
		[Header("BUTTON")]
		public NKCUIComStateButton m_btnShortcut;

		// Token: 0x04005822 RID: 22562
		public NKCUIComStateButton m_btnShop;

		// Token: 0x04005823 RID: 22563
		[Header("스킵")]
		public GameObject m_objSkip;

		// Token: 0x04005824 RID: 22564
		public NKCUIOperationSkip m_NKCUIOperationSkip;

		// Token: 0x04005825 RID: 22565
		public NKCUIComToggle m_tglSkip;

		// Token: 0x04005826 RID: 22566
		private int m_CurrSkipCount = 1;

		// Token: 0x04005827 RID: 22567
		private Stack<NKCUIShadowPalaceSlot> m_stkPalaceSlotPool = new Stack<NKCUIShadowPalaceSlot>();

		// Token: 0x04005828 RID: 22568
		private List<NKCUIShadowPalaceSlot> m_lstPalaceSlot = new List<NKCUIShadowPalaceSlot>();

		// Token: 0x04005829 RID: 22569
		private List<NKMShadowPalaceTemplet> m_lstPalaceTemplet = new List<NKMShadowPalaceTemplet>();

		// Token: 0x0400582A RID: 22570
		private int m_selectPalaceID;

		// Token: 0x0400582B RID: 22571
		private List<int> m_lstUpsideResource = new List<int>();

		// Token: 0x0400582C RID: 22572
		private bool m_bOperationSkip;

		// Token: 0x0400582D RID: 22573
		private List<int> m_leaderBoardIDs = new List<int>();
	}
}

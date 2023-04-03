using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Office;
using NKC.Office;
using NKC.UI.Component.Office;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF8 RID: 2808
	public class NKCUIPopupOfficeInteract : NKCUIBase
	{
		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06007EB1 RID: 32433 RVA: 0x002A7E3C File Offset: 0x002A603C
		public static NKCUIPopupOfficeInteract Instance
		{
			get
			{
				if (NKCUIPopupOfficeInteract.m_Instance == null)
				{
					NKCUIPopupOfficeInteract.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupOfficeInteract>("ab_ui_office", "AB_UI_POPUP_OFFICE_INTERACT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupOfficeInteract.CleanupInstance)).GetInstance<NKCUIPopupOfficeInteract>();
					NKCUIPopupOfficeInteract.m_Instance.InitUI();
				}
				return NKCUIPopupOfficeInteract.m_Instance;
			}
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x002A7E8B File Offset: 0x002A608B
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupOfficeInteract.m_Instance != null && NKCUIPopupOfficeInteract.m_Instance.IsOpen)
			{
				NKCUIPopupOfficeInteract.m_Instance.Close();
			}
		}

		// Token: 0x06007EB3 RID: 32435 RVA: 0x002A7EB0 File Offset: 0x002A60B0
		private static void CleanupInstance()
		{
			NKCUIPopupOfficeInteract.m_Instance = null;
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06007EB4 RID: 32436 RVA: 0x002A7EB8 File Offset: 0x002A60B8
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupOfficeInteract.m_Instance != null && NKCUIPopupOfficeInteract.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007EB5 RID: 32437 RVA: 0x002A7ED3 File Offset: 0x002A60D3
		private bool CheckRefreshInterval()
		{
			return NKCScenManager.CurrentUserData().OfficeData.CanRefreshOfficePost();
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06007EB6 RID: 32438 RVA: 0x002A7EE4 File Offset: 0x002A60E4
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06007EB7 RID: 32439 RVA: 0x002A7EE7 File Offset: 0x002A60E7
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x002A7EEE File Offset: 0x002A60EE
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x002A7EFC File Offset: 0x002A60FC
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRefresh, new UnityAction(this.OnRefresh));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSendBizCardAll, new UnityAction(this.OnSendBizCardAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnGetBizCardAll, new UnityAction(this.OnGetBizCardAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRandomvisit, new UnityAction(this.OnRandomVisit));
			if (this.m_srBizCard != null)
			{
				this.m_srBizCard.dOnGetObject += this.GetObject;
				this.m_srBizCard.dOnReturnObject += this.ReturnObject;
				this.m_srBizCard.dOnProvideData += this.ProvideData;
				this.m_srBizCard.SetAutoResize(2, false);
				this.m_srBizCard.PrepareCells(0);
			}
			if (this.m_trInactiveBizcard == null)
			{
				GameObject gameObject = new GameObject("inactiveBizCard");
				this.m_trInactiveBizcard = gameObject.transform;
				this.m_trInactiveBizcard.SetParent(base.transform);
				gameObject.SetActive(false);
			}
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x002A8025 File Offset: 0x002A6225
		public void Open()
		{
			base.UIOpened(true);
			this.UpdateMyBizCard();
			this.UpdateBizCardList();
			this.UpdateSendBizCardAllState();
			this.TryRefresh();
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x002A8048 File Offset: 0x002A6248
		public void UpdateMyBizCard()
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_trRootMyBizCard, false);
				NKCPacketSender.Send_NKMPacket_MY_USER_PROFILE_INFO_REQ();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_trRootMyBizCard, true);
			if (this.m_MyBizCard == null)
			{
				this.m_MyBizCard = NKCUIComOfficeBizCard.GetInstance(0, this.m_trRootMyBizCard);
			}
			this.m_MyBizCard.SetData(userProfileData, null);
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x002A80B0 File Offset: 0x002A62B0
		public void UpdateBizCardList()
		{
			NKCOfficeData officeData = NKCScenManager.CurrentUserData().OfficeData;
			this.m_srBizCard.TotalCount = officeData.BizcardCount;
			this.m_srBizCard.SetIndexPosition(0);
			bool flag = officeData.BizcardCount != 0;
			bool canReceiveBizcard = officeData.CanReceiveBizcard;
			if (this.m_csbtnGetBizCardAll != null)
			{
				this.m_csbtnGetBizCardAll.SetLock(!flag, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objBizCardNone, !flag);
			NKCUtil.SetGameobjectActive(this.m_objGetBizCardAllReddot, flag && canReceiveBizcard);
			this.UpdateItemReceiveLimitState();
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x002A8138 File Offset: 0x002A6338
		public void UpdateSendBizCardAllState()
		{
			if (this.m_csbtnSendBizCardAll != null)
			{
				bool flag = this.CanSendBizCardAll();
				NKCUtil.SetGameobjectActive(this.m_objSendBizAllReddot, flag && NKCFriendManager.FriendList.Count > 0);
				NKCUtil.SetGameobjectActive(this.m_objSendBizAllNormal, flag);
				NKCUtil.SetGameobjectActive(this.m_objSendBizAllLocked, !flag);
			}
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x002A8194 File Offset: 0x002A6394
		private void UpdateItemReceiveLimitState()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			int recvCountLeft = nkmuserData.OfficeData.RecvCountLeft;
			NKCUtil.SetLabelText(this.m_lbItemReceiveLimit, string.Format("{0}/{1}", recvCountLeft, NKMCommonConst.Office.NameCard.DailyLimit));
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x002A81E6 File Offset: 0x002A63E6
		private void TryRefresh()
		{
			NKCScenManager.CurrentUserData().OfficeData.TryRefreshOfficePost(false);
			if (this.m_csbtnRefresh != null)
			{
				this.m_csbtnRefresh.Lock(false);
			}
		}

		// Token: 0x06007EC0 RID: 32448 RVA: 0x002A8214 File Offset: 0x002A6414
		private bool CanSendBizCardAll()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.OfficeData.CanSendBizcardBroadcast;
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x002A8238 File Offset: 0x002A6438
		private void Update()
		{
			if (this.m_csbtnRefresh != null)
			{
				this.m_csbtnRefresh.SetLock(!this.CheckRefreshInterval(), false);
			}
			if (this.m_objSendBizAllLocked != null && this.m_objSendBizAllLocked.activeSelf)
			{
				this.UpdateSendBizCardAllState();
			}
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x002A8289 File Offset: 0x002A6489
		private void OnRefresh()
		{
			this.TryRefresh();
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x002A8291 File Offset: 0x002A6491
		private void OnSendBizCardAll()
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			if (!this.CanSendBizCardAll())
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("NEC_FAIL_OFFICE_POST_SEND_DAILY_LIMIT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_POST_BROADCAST_REQ();
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x002A82C2 File Offset: 0x002A64C2
		private void OnGetBizCardAll()
		{
			NKCPacketSender.Send_NKMPacket_OFFICE_POST_RECV_REQ();
		}

		// Token: 0x06007EC5 RID: 32453 RVA: 0x002A82C9 File Offset: 0x002A64C9
		private void OnRandomVisit()
		{
			NKCPacketSender.Send_NKMPacket_OFFICE_RANDOM_VISIT_REQ();
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x002A82D0 File Offset: 0x002A64D0
		private RectTransform GetObject(int index)
		{
			if (index >= NKCScenManager.CurrentUserData().OfficeData.BizcardCount)
			{
				return null;
			}
			Stack<NKCUIComOfficeBizCard> stack;
			if (this.m_dicBizCardCache.TryGetValue(0, out stack) && stack.Count > 0)
			{
				NKCUIComOfficeBizCard nkcuicomOfficeBizCard = stack.Pop();
				nkcuicomOfficeBizCard.gameObject.SetActive(true);
				return nkcuicomOfficeBizCard.GetComponent<RectTransform>();
			}
			return NKCUIComOfficeBizCard.GetInstance(0, null).GetComponent<RectTransform>();
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x002A8330 File Offset: 0x002A6530
		private void ReturnObject(Transform go)
		{
			go.SetParent(this.m_trInactiveBizcard);
			NKCUIComOfficeBizCard component = go.GetComponent<NKCUIComOfficeBizCard>();
			if (component == null)
			{
				UnityEngine.Object.Destroy(go);
				return;
			}
			Stack<NKCUIComOfficeBizCard> stack;
			if (this.m_dicBizCardCache.TryGetValue(0, out stack))
			{
				stack.Push(component);
				return;
			}
			Stack<NKCUIComOfficeBizCard> stack2 = new Stack<NKCUIComOfficeBizCard>();
			stack2.Push(component);
			this.m_dicBizCardCache[0] = stack2;
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x002A8394 File Offset: 0x002A6594
		private void ProvideData(Transform tr, int index)
		{
			if (index >= NKCScenManager.CurrentUserData().OfficeData.BizcardCount)
			{
				return;
			}
			NKMOfficePost bizCard = NKCScenManager.CurrentUserData().OfficeData.GetBizCard(index);
			NKCUIComOfficeBizCard component = tr.GetComponent<NKCUIComOfficeBizCard>();
			if (component != null)
			{
				component.SetData(bizCard, new NKCUIComOfficeBizCard.OnClick(this.OnClickCard));
			}
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x002A83E8 File Offset: 0x002A65E8
		private void OnClickCard(long uid)
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(uid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x04006B4F RID: 27471
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006B50 RID: 27472
		private const string UI_ASSET_NAME = "AB_UI_POPUP_OFFICE_INTERACT";

		// Token: 0x04006B51 RID: 27473
		private static NKCUIPopupOfficeInteract m_Instance;

		// Token: 0x04006B52 RID: 27474
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006B53 RID: 27475
		public NKCUIComStateButton m_csbtnRefresh;

		// Token: 0x04006B54 RID: 27476
		public NKCUIComStateButton m_csbtnSendBizCardAll;

		// Token: 0x04006B55 RID: 27477
		public NKCUIComStateButton m_csbtnGetBizCardAll;

		// Token: 0x04006B56 RID: 27478
		public GameObject m_objGetBizCardAllReddot;

		// Token: 0x04006B57 RID: 27479
		public NKCUIComStateButton m_csbtnRandomvisit;

		// Token: 0x04006B58 RID: 27480
		public Text m_lbItemReceiveLimit;

		// Token: 0x04006B59 RID: 27481
		public GameObject m_objSendBizAllReddot;

		// Token: 0x04006B5A RID: 27482
		public GameObject m_objSendBizAllNormal;

		// Token: 0x04006B5B RID: 27483
		public GameObject m_objSendBizAllLocked;

		// Token: 0x04006B5C RID: 27484
		public Transform m_trRootMyBizCard;

		// Token: 0x04006B5D RID: 27485
		private NKCUIComOfficeBizCard m_MyBizCard;

		// Token: 0x04006B5E RID: 27486
		public LoopScrollRect m_srBizCard;

		// Token: 0x04006B5F RID: 27487
		public GameObject m_objBizCardNone;

		// Token: 0x04006B60 RID: 27488
		private Transform m_trInactiveBizcard;

		// Token: 0x04006B61 RID: 27489
		private Dictionary<int, Stack<NKCUIComOfficeBizCard>> m_dicBizCardCache = new Dictionary<int, Stack<NKCUIComOfficeBizCard>>();
	}
}

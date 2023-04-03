using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B6 RID: 2486
	public class NKCUIMail : NKCUIBase
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06006900 RID: 26880 RVA: 0x0021F52C File Offset: 0x0021D72C
		public static NKCUIMail Instance
		{
			get
			{
				if (NKCUIMail.m_Instance == null)
				{
					NKCUIMail.m_Instance = NKCUIManager.OpenNewInstance<NKCUIMail>("ab_ui_nkm_ui_mail", "NKM_UI_MAIL", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIMail.CleanupInstance)).GetInstance<NKCUIMail>();
					NKCUIMail.m_Instance.Init();
				}
				return NKCUIMail.m_Instance;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06006901 RID: 26881 RVA: 0x0021F57B File Offset: 0x0021D77B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIMail.m_Instance != null && NKCUIMail.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x0021F596 File Offset: 0x0021D796
		private static void CleanupInstance()
		{
			NKCUIMail.m_Instance = null;
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06006903 RID: 26883 RVA: 0x0021F59E File Offset: 0x0021D79E
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MAIL;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06006904 RID: 26884 RVA: 0x0021F5A5 File Offset: 0x0021D7A5
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06006905 RID: 26885 RVA: 0x0021F5A8 File Offset: 0x0021D7A8
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME;
			}
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x0021F5AB File Offset: 0x0021D7AB
		public override void CloseInternal()
		{
			NKCMailManager.dOnMailCountChange = (NKCMailManager.OnMailCountChange)Delegate.Remove(NKCMailManager.dOnMailCountChange, new NKCMailManager.OnMailCountChange(this.OnMailCountChanged));
			base.gameObject.SetActive(false);
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06006907 RID: 26887 RVA: 0x0021F5D9 File Offset: 0x0021D7D9
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.lstResources;
			}
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x0021F5E4 File Offset: 0x0021D7E4
		public void Init()
		{
			this.m_LoopScrollRect.dOnGetObject += this.MakeSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
			this.m_LoopScrollRect.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			this.m_cbtnReceiveAll.PointerClick.AddListener(new UnityAction(this.ReceiveAll));
			this.m_cbtnRefresh.PointerClick.AddListener(new UnityAction(this.TryRefreshMail));
		}

		// Token: 0x06006909 RID: 26889 RVA: 0x0021F688 File Offset: 0x0021D888
		private RectTransform MakeSlot(int index)
		{
			if (this.m_stkMailPool.Count > 0)
			{
				RectTransform rectTransform = this.m_stkMailPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUIMailSlot nkcuimailSlot = UnityEngine.Object.Instantiate<NKCUIMailSlot>(this.m_pfbMailSlot);
			nkcuimailSlot.Init();
			nkcuimailSlot.transform.localPosition = Vector3.zero;
			nkcuimailSlot.transform.localScale = Vector3.one;
			return nkcuimailSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600690A RID: 26890 RVA: 0x0021F6EC File Offset: 0x0021D8EC
		private void ReturnSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rtMailSlotPool);
			this.m_stkMailPool.Push(go.GetComponent<RectTransform>());
		}

		// Token: 0x0600690B RID: 26891 RVA: 0x0021F714 File Offset: 0x0021D914
		private void ProvideSlotData(Transform tr, int idx)
		{
			NKMPostData mailByIndex = NKCMailManager.GetMailByIndex(idx);
			if (mailByIndex != null)
			{
				NKCUIMailSlot component = tr.GetComponent<NKCUIMailSlot>();
				if (component != null)
				{
					component.SetData(mailByIndex, new NKCUIMailSlot.OnReceive(this.TryReceiveMail), new NKCUIMailSlot.OnOpen(this.OpenMail));
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(tr, false);
			}
		}

		// Token: 0x0600690C RID: 26892 RVA: 0x0021F764 File Offset: 0x0021D964
		public void Open()
		{
			NKCMailManager.dOnMailCountChange = (NKCMailManager.OnMailCountChange)Delegate.Combine(NKCMailManager.dOnMailCountChange, new NKCMailManager.OnMailCountChange(this.OnMailCountChanged));
			base.gameObject.SetActive(true);
			if (!this.bSlotReady)
			{
				this.bSlotReady = true;
				this.m_LoopScrollRect.PrepareCells(0);
			}
			if (NKCMailManager.CanRefreshMail())
			{
				this.TryRefreshMail();
			}
			else
			{
				this.SetMailCount(NKCMailManager.GetTotalMailCount());
				this.UpdateMailList();
			}
			this.m_LoopScrollRect.velocity = new Vector2(0f, 0f);
			this.m_LoopScrollRect.SetIndexPosition(0);
			base.UIOpened(true);
		}

		// Token: 0x0600690D RID: 26893 RVA: 0x0021F808 File Offset: 0x0021DA08
		private void Update()
		{
			this.SetRefreshMailEnable(NKCMailManager.CanRefreshMail());
			if (this.m_LoopScrollRect.normalizedPosition.y > 1f)
			{
				this.GetNextMail();
			}
			this.m_fUpdateTimer += Time.deltaTime;
			if (this.m_fUpdateTimer > 10f)
			{
				this.CheckAndUpdateMailTimer();
			}
		}

		// Token: 0x0600690E RID: 26894 RVA: 0x0021F864 File Offset: 0x0021DA64
		private void CheckAndUpdateMailTimer()
		{
			NKCMailManager.CheckAndRemoveExpiredMail();
			foreach (NKCUIMailSlot nkcuimailSlot in this.m_lstMailSlot)
			{
				NKMPostData mailByPostID = NKCMailManager.GetMailByPostID(nkcuimailSlot.Index);
				if (mailByPostID != null)
				{
					if (mailByPostID.expirationDate < NKMConst.Post.UnlimitedExpirationUtcDate)
					{
						nkcuimailSlot.UpdateTime();
					}
				}
				else
				{
					Debug.LogError("Logic error");
				}
			}
		}

		// Token: 0x0600690F RID: 26895 RVA: 0x0021F8E8 File Offset: 0x0021DAE8
		public void SetMailCount(int count)
		{
			this.m_lbMailCount.text = string.Format(NKCUtilString.GET_STRING_MAIL_HAVE_COUNT, count);
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x0021F905 File Offset: 0x0021DB05
		public void OnMailCountChanged(int newTotalCount)
		{
			if (base.IsOpen)
			{
				this.UpdateMailList();
				this.SetMailCount(newTotalCount);
				NKCPopupMail.CheckInstanceAndClose();
			}
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x0021F921 File Offset: 0x0021DB21
		private void UpdateMailList()
		{
			NKCUtil.SetGameobjectActive(this.m_objNoMail, NKCMailManager.GetTotalMailCount() == 0);
			this.m_LoopScrollRect.TotalCount = NKCMailManager.GetReceivedMailCount();
			this.m_LoopScrollRect.RefreshCells(false);
			this.SetReceiveAllEnable(NKCMailManager.GetTotalMailCount() > 0);
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x0021F960 File Offset: 0x0021DB60
		private void TryReceiveMail(long index)
		{
			NKCPacketSender.Send_NKMPacket_POST_RECEIVE_REQ(index);
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x0021F968 File Offset: 0x0021DB68
		private void ReceiveAll()
		{
			if (NKCMailManager.GetTotalMailCount() > 0)
			{
				NKCPacketSender.Send_NKMPacket_POST_RECEIVE_REQ(0L);
			}
		}

		// Token: 0x06006914 RID: 26900 RVA: 0x0021F979 File Offset: 0x0021DB79
		public void TryRefreshMail()
		{
			this.m_LoopScrollRect.StopMovement();
			this.m_LoopScrollRect.SetIndexPosition(0);
			NKCMailManager.RefreshMailList();
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x0021F997 File Offset: 0x0021DB97
		public void GetNextMail()
		{
			NKCMailManager.GetNextMail();
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x0021F99E File Offset: 0x0021DB9E
		private void SetRefreshMailEnable(bool value)
		{
			if (value)
			{
				this.m_cbtnRefresh.UnLock(false);
				return;
			}
			this.m_cbtnRefresh.Lock(false);
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x0021F9BC File Offset: 0x0021DBBC
		private void SetReceiveAllEnable(bool value)
		{
			if (value)
			{
				this.m_cbtnReceiveAll.UnLock(false);
				return;
			}
			this.m_cbtnReceiveAll.Lock(false);
		}

		// Token: 0x06006918 RID: 26904 RVA: 0x0021F9DA File Offset: 0x0021DBDA
		private void OpenMail(NKMPostData postData)
		{
			NKCPopupMail.Instance.Open(postData, new NKCPopupMail.OnReceive(this.TryReceiveMail));
		}

		// Token: 0x040054D8 RID: 21720
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_mail";

		// Token: 0x040054D9 RID: 21721
		private const string UI_ASSET_NAME = "NKM_UI_MAIL";

		// Token: 0x040054DA RID: 21722
		private static NKCUIMail m_Instance;

		// Token: 0x040054DB RID: 21723
		private List<NKCUIMailSlot> m_lstMailSlot = new List<NKCUIMailSlot>();

		// Token: 0x040054DC RID: 21724
		public NKCUIMailSlot m_pfbMailSlot;

		// Token: 0x040054DD RID: 21725
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x040054DE RID: 21726
		public RectTransform m_rtMailSlotPool;

		// Token: 0x040054DF RID: 21727
		private Stack<RectTransform> m_stkMailPool = new Stack<RectTransform>();

		// Token: 0x040054E0 RID: 21728
		public NKCUIComStateButton m_cbtnRefresh;

		// Token: 0x040054E1 RID: 21729
		public NKCUIComStateButton m_cbtnReceiveAll;

		// Token: 0x040054E2 RID: 21730
		public Text m_lbMailCount;

		// Token: 0x040054E3 RID: 21731
		public GameObject m_objNoMail;

		// Token: 0x040054E4 RID: 21732
		private const float MAIL_TIMER_UPDATE_INVERVAL = 10f;

		// Token: 0x040054E5 RID: 21733
		private float m_fUpdateTimer;

		// Token: 0x040054E6 RID: 21734
		private bool bSlotReady;

		// Token: 0x040054E7 RID: 21735
		private readonly List<int> lstResources = new List<int>
		{
			1,
			2,
			101,
			102
		};
	}
}

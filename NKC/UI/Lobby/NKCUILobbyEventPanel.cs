using System;
using System.Collections.Generic;
using NKC.UI.Component;
using NKC.UI.Event;
using NKM;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C09 RID: 3081
	public class NKCUILobbyEventPanel : MonoBehaviour
	{
		// Token: 0x06008EAC RID: 36524 RVA: 0x00308878 File Offset: 0x00306A78
		public void Init()
		{
			if (this.m_EventSlidePanel != null)
			{
				this.m_EventSlidePanel.Init(true, true);
				this.m_EventSlidePanel.dOnGetObject += this.GetObject;
				this.m_EventSlidePanel.dOnReturnObject += this.ReturnObject;
				this.m_EventSlidePanel.dOnProvideData += this.ProvideData;
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
		}

		// Token: 0x06008EAD RID: 36525 RVA: 0x003088F4 File Offset: 0x00306AF4
		public void SetData(NKMUserData userData)
		{
			this.SetEventList();
			if (NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_EVENT, 0, 0) && this.m_lstEventTabTemplet.Count > 0 && this.m_EventSlidePanel != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEvent, true);
				this.m_EventSlidePanel.TotalCount = this.m_lstEventTabTemplet.Count;
				this.m_EventSlidePanel.SetIndex(0);
				this.CheckReddot();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEvent, false);
		}

		// Token: 0x06008EAE RID: 36526 RVA: 0x0030896F File Offset: 0x00306B6F
		public void CheckReddot()
		{
			NKCUtil.SetGameobjectActive(this.m_objRedDot, NKMEventManager.CheckRedDot());
		}

		// Token: 0x06008EAF RID: 36527 RVA: 0x00308981 File Offset: 0x00306B81
		private RectTransform GetObject()
		{
			if (this.m_stkEventObjects.Count > 0)
			{
				NKCUILobbyMenuEvent nkcuilobbyMenuEvent = this.m_stkEventObjects.Pop();
				NKCUtil.SetGameobjectActive(nkcuilobbyMenuEvent, false);
				return nkcuilobbyMenuEvent.GetComponent<RectTransform>();
			}
			NKCUILobbyMenuEvent nkcuilobbyMenuEvent2 = UnityEngine.Object.Instantiate<NKCUILobbyMenuEvent>(this.m_pfbEventButton);
			NKCUtil.SetGameobjectActive(nkcuilobbyMenuEvent2, false);
			return nkcuilobbyMenuEvent2.GetComponent<RectTransform>();
		}

		// Token: 0x06008EB0 RID: 36528 RVA: 0x003089C0 File Offset: 0x00306BC0
		private void ReturnObject(RectTransform rect)
		{
			NKCUILobbyMenuEvent component = rect.GetComponent<NKCUILobbyMenuEvent>();
			if (component != null)
			{
				this.m_stkEventObjects.Push(component);
			}
			NKCUtil.SetGameobjectActive(rect, false);
			rect.parent = base.transform;
		}

		// Token: 0x06008EB1 RID: 36529 RVA: 0x003089FC File Offset: 0x00306BFC
		private void ProvideData(RectTransform rect, int idx)
		{
			NKCUILobbyMenuEvent component = rect.GetComponent<NKCUILobbyMenuEvent>();
			if (component != null)
			{
				if (idx >= 0 && idx < this.m_lstEventTabTemplet.Count)
				{
					NKCUtil.SetGameobjectActive(component, true);
					rect.SetParent(this.m_EventSlidePanel.transform);
					component.SetData(this.m_lstEventTabTemplet[idx], new NKCUILobbyMenuEvent.OnButton(this.OnBtnBanner));
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
			}
		}

		// Token: 0x06008EB2 RID: 36530 RVA: 0x00308A69 File Offset: 0x00306C69
		private void OnBtnBanner(NKMEventTabTemplet tabTemplet)
		{
			NKCUIEvent.Instance.Open(tabTemplet);
		}

		// Token: 0x06008EB3 RID: 36531 RVA: 0x00308A78 File Offset: 0x00306C78
		private void SetEventList()
		{
			this.m_lstEventTabTemplet.Clear();
			this.m_tFirstEventEndDateUTC = default(DateTime);
			foreach (NKMEventTabTemplet nkmeventTabTemplet in NKMTempletContainer<NKMEventTabTemplet>.Values)
			{
				if (nkmeventTabTemplet.IsAvailable)
				{
					this.m_lstEventTabTemplet.Add(nkmeventTabTemplet);
					if (this.m_tFirstEventEndDateUTC.Ticks == 0L || this.m_tFirstEventEndDateUTC > nkmeventTabTemplet.EventDateEndUtc)
					{
						this.m_tFirstEventEndDateUTC = nkmeventTabTemplet.EventDateEndUtc;
					}
				}
			}
			this.m_lstEventTabTemplet.Sort(new Comparison<NKMEventTabTemplet>(this.CompEventTabTemplet));
		}

		// Token: 0x06008EB4 RID: 36532 RVA: 0x00308B2C File Offset: 0x00306D2C
		private int CompEventTabTemplet(NKMEventTabTemplet lItem, NKMEventTabTemplet rItem)
		{
			return lItem.m_OrderList.CompareTo(rItem.m_OrderList);
		}

		// Token: 0x06008EB5 RID: 36533 RVA: 0x00308B40 File Offset: 0x00306D40
		private void Update()
		{
			if (this.m_tFirstEventEndDateUTC.Ticks > 0L)
			{
				this.m_fDelatTime += Time.deltaTime;
				if (this.m_fDelatTime > 1f)
				{
					this.m_fDelatTime -= 1f;
					if (this.m_tFirstEventEndDateUTC < NKCSynchronizedTime.GetServerUTCTime(0.0))
					{
						this.SetData(NKCScenManager.CurrentUserData());
					}
				}
			}
		}

		// Token: 0x04007BBF RID: 31679
		private DateTime m_tFirstEventEndDateUTC;

		// Token: 0x04007BC0 RID: 31680
		[Header("이벤트")]
		public GameObject m_objEvent;

		// Token: 0x04007BC1 RID: 31681
		public NKCUILobbyMenuEvent m_pfbEventButton;

		// Token: 0x04007BC2 RID: 31682
		public NKCUIComDragSelectablePanel m_EventSlidePanel;

		// Token: 0x04007BC3 RID: 31683
		public GameObject m_objRedDot;

		// Token: 0x04007BC4 RID: 31684
		private List<NKMEventTabTemplet> m_lstEventTabTemplet = new List<NKMEventTabTemplet>();

		// Token: 0x04007BC5 RID: 31685
		private Stack<NKCUILobbyMenuEvent> m_stkEventObjects = new Stack<NKCUILobbyMenuEvent>();

		// Token: 0x04007BC6 RID: 31686
		private float m_fDelatTime;
	}
}

using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A8A RID: 2698
	public class NKCPopupTopPlayer : NKCUIBase
	{
		// Token: 0x06007765 RID: 30565 RVA: 0x0027B2E9 File Offset: 0x002794E9
		public static NKCPopupTopPlayer OpenInstance(string bundleName, string assetName)
		{
			NKCPopupTopPlayer instance = NKCUIManager.OpenNewInstance<NKCPopupTopPlayer>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIFrontPopup, null).GetInstance<NKCPopupTopPlayer>();
			if (instance == null)
			{
				return instance;
			}
			instance.Init();
			return instance;
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06007766 RID: 30566 RVA: 0x0027B304 File Offset: 0x00279504
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06007767 RID: 30567 RVA: 0x0027B307 File Offset: 0x00279507
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06007768 RID: 30568 RVA: 0x0027B30E File Offset: 0x0027950E
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007769 RID: 30569 RVA: 0x0027B31C File Offset: 0x0027951C
		public void Init()
		{
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_loop != null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.m_loop.dOnGetObject += this.GetObject;
				this.m_loop.dOnReturnObject += this.ReturnObject;
				this.m_loop.dOnProvideData += this.ProvideData;
				this.m_loop.PrepareCells(0);
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			if (this.m_btnReward != null)
			{
				this.m_btnReward.PointerClick.RemoveAllListeners();
				this.m_btnReward.PointerClick.AddListener(new UnityAction(this.OnClickReward));
			}
		}

		// Token: 0x0600776A RID: 30570 RVA: 0x0027B414 File Offset: 0x00279614
		private RectTransform GetObject(int idx)
		{
			NKCPopupTopPlayerSlot nkcpopupTopPlayerSlot;
			if (this.m_stktRankListPool.Count > 0)
			{
				nkcpopupTopPlayerSlot = this.m_stktRankListPool.Pop();
			}
			else
			{
				nkcpopupTopPlayerSlot = UnityEngine.Object.Instantiate<NKCPopupTopPlayerSlot>(this.m_pfbSlot);
			}
			if (nkcpopupTopPlayerSlot == null)
			{
				return null;
			}
			NKCUtil.SetGameobjectActive(nkcpopupTopPlayerSlot, true);
			nkcpopupTopPlayerSlot.transform.SetParent(this.m_trContent);
			return nkcpopupTopPlayerSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600776B RID: 30571 RVA: 0x0027B474 File Offset: 0x00279674
		private void ReturnObject(Transform tr)
		{
			tr.SetParent(this.m_trPoolParent);
			NKCPopupTopPlayerSlot component = tr.GetComponent<NKCPopupTopPlayerSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			this.m_stktRankListPool.Push(component);
		}

		// Token: 0x0600776C RID: 30572 RVA: 0x0027B4A8 File Offset: 0x002796A8
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupTopPlayerSlot component = tr.GetComponent<NKCPopupTopPlayerSlot>();
			if (component == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			idx += this.m_lstTopPlayerSlot.Count;
			if (idx >= this.m_lstSlotData.Count)
			{
				return;
			}
			component.SetData(this.m_lstSlotData[idx].Profile, this.m_lstSlotData[idx].GuildData, this.m_lstSlotData[idx].score, this.m_lstSlotData[idx].raidTryCount, this.m_lstSlotData[idx].raidTryMaxCount, idx + 1);
		}

		// Token: 0x0600776D RID: 30573 RVA: 0x0027B54C File Offset: 0x0027974C
		public void Open(string title_1, string title_2, string subTitle, Sprite sprTitle, List<LeaderBoardSlotData> lstSlotData, List<NKMEmblemData> emblems, long uid = 0L, NKCPopupTopPlayer.OnReward dOnReward = null)
		{
			if (lstSlotData == null || lstSlotData.Count == 0)
			{
				this.CloseInternal();
				return;
			}
			this.m_bWaitingReward = false;
			this.m_lstSlotData = lstSlotData;
			this.m_popupUID = uid;
			this.m_dOnReward = dOnReward;
			NKCUtil.SetLabelText(this.m_lbTopTitleLeft, title_1);
			NKCUtil.SetLabelText(this.m_lbTopTitleRight, title_2);
			NKCUtil.SetLabelText(this.m_lbSubTitle, subTitle);
			if (sprTitle != null)
			{
				NKCUtil.SetImageSprite(this.m_imgTitle, sprTitle, false);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetLabelText(this.m_lbUserLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, this.m_lstSlotData[0].Profile.level));
			NKCUtil.SetLabelText(this.m_lbUserName, this.m_lstSlotData[0].Profile.nickname);
			for (int i = 0; i < this.m_lstEmblem.Count; i++)
			{
				if (i < emblems.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstEmblem[i], true);
					NKMEmblemData nkmemblemData = emblems[i];
					this.m_lstEmblem[i].SetData(NKCUISlot.SlotData.MakeMiscItemData(nkmemblemData.id, nkmemblemData.count, 0), true, null);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstEmblem[i], false);
				}
			}
			if (this.m_CharacterView != null)
			{
				if (this.m_lstSlotData[0].Profile.mainUnitSkinId == 0)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_lstSlotData[0].Profile.mainUnitId);
					this.m_CharacterView.SetCharacterIllust(unitTempletBase, 0, false, true, 0);
				}
				else
				{
					NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_lstSlotData[0].Profile.mainUnitSkinId);
					this.m_CharacterView.SetCharacterIllust(skinTemplet, false, true, 0);
				}
			}
			for (int j = 0; j < this.m_lstTopPlayerSlot.Count; j++)
			{
				if (j < this.m_lstSlotData.Count)
				{
					this.m_lstTopPlayerSlot[j].SetData(this.m_lstSlotData[j].Profile, this.m_lstSlotData[j].GuildData, this.m_lstSlotData[j].score, this.m_lstSlotData[j].raidTryCount, this.m_lstSlotData[j].raidTryMaxCount, j + 1);
				}
				else
				{
					this.m_lstTopPlayerSlot[j].SetEmpty();
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_lstSlotData.Count <= this.m_lstTopPlayerSlot.Count);
			if (this.m_loop != null)
			{
				if (this.m_lstSlotData.Count > this.m_lstTopPlayerSlot.Count)
				{
					this.m_loop.TotalCount = this.m_lstSlotData.Count - this.m_lstTopPlayerSlot.Count;
					this.m_loop.SetIndexPosition(0);
				}
				else
				{
					this.m_loop.TotalCount = 0;
					this.m_loop.RefreshCells(false);
				}
			}
			base.UIOpened(true);
		}

		// Token: 0x0600776E RID: 30574 RVA: 0x0027B866 File Offset: 0x00279A66
		private void OnClickReward()
		{
			if (!this.m_bWaitingReward)
			{
				base.Close();
				this.m_bWaitingReward = true;
				NKCPopupTopPlayer.OnReward dOnReward = this.m_dOnReward;
				if (dOnReward == null)
				{
					return;
				}
				dOnReward(this.m_popupUID);
			}
		}

		// Token: 0x040063E9 RID: 25577
		[Header("팝업 상단")]
		public Text m_lbTopTitleLeft;

		// Token: 0x040063EA RID: 25578
		public Text m_lbTopTitleRight;

		// Token: 0x040063EB RID: 25579
		public Text m_lbSubTitle;

		// Token: 0x040063EC RID: 25580
		public Image m_imgTitle;

		// Token: 0x040063ED RID: 25581
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040063EE RID: 25582
		[Header(" 1등유저 전용 정보")]
		public Text m_lbUserLevel;

		// Token: 0x040063EF RID: 25583
		public Text m_lbUserName;

		// Token: 0x040063F0 RID: 25584
		public List<NKCUISlot> m_lstEmblem = new List<NKCUISlot>();

		// Token: 0x040063F1 RID: 25585
		public NKCUICharacterView m_CharacterView;

		// Token: 0x040063F2 RID: 25586
		[Header(" 탑3 유저 정보")]
		public List<NKCPopupTopPlayerSlot> m_lstTopPlayerSlot = new List<NKCPopupTopPlayerSlot>();

		// Token: 0x040063F3 RID: 25587
		[Header("4등 이상 있을 경우 우측에 나오는 유저 정보")]
		public GameObject m_objNone;

		// Token: 0x040063F4 RID: 25588
		public LoopScrollRect m_loop;

		// Token: 0x040063F5 RID: 25589
		public Transform m_trContent;

		// Token: 0x040063F6 RID: 25590
		public Transform m_trPoolParent;

		// Token: 0x040063F7 RID: 25591
		public NKCPopupTopPlayerSlot m_pfbSlot;

		// Token: 0x040063F8 RID: 25592
		[Header("보상버튼")]
		public NKCUIComStateButton m_btnReward;

		// Token: 0x040063F9 RID: 25593
		private NKCPopupTopPlayer.OnReward m_dOnReward;

		// Token: 0x040063FA RID: 25594
		private long m_popupUID;

		// Token: 0x040063FB RID: 25595
		private bool m_bWaitingReward;

		// Token: 0x040063FC RID: 25596
		private List<LeaderBoardSlotData> m_lstSlotData = new List<LeaderBoardSlotData>();

		// Token: 0x040063FD RID: 25597
		private Stack<NKCPopupTopPlayerSlot> m_stktRankListPool = new Stack<NKCPopupTopPlayerSlot>();

		// Token: 0x020017E7 RID: 6119
		// (Invoke) Token: 0x0600B48A RID: 46218
		public delegate void OnReward(long uid);
	}
}

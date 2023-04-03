using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009DE RID: 2526
	public class NKCUIShadowPalaceRank : MonoBehaviour
	{
		// Token: 0x06006C85 RID: 27781 RVA: 0x0023748C File Offset: 0x0023568C
		public void Init()
		{
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.dOnGetObject += this.OnGetObject;
				this.m_scrollRect.dOnProvideData += this.OnProvideData;
				this.m_scrollRect.dOnReturnObject += this.OnReturnObject;
				this.m_scrollRect.PrepareCells(0);
			}
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(this.OnClose));
			this.m_ani.Play("NKM_UI_SHADOW_INFO_OUTRO_IDLE");
			this.m_imgBG.raycastTarget = false;
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x00237540 File Offset: 0x00235740
		public void SetData(string title, string desc, List<LeaderBoardSlotData> lstRankData, LeaderBoardSlotData myRankData)
		{
			NKCUtil.SetLabelText(this.m_txtPalaceNum, title);
			NKCUtil.SetLabelText(this.m_txtPalaceName, desc);
			this.m_lstRankData = lstRankData;
			this.m_scrollRect.TotalCount = lstRankData.Count;
			this.m_scrollRect.RefreshCells(true);
			this.m_scrollRect.SetIndexPosition(0);
			this.m_myRank.Init();
			this.m_myRank.SetData(myRankData, myRankData.rank, true);
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x002375B5 File Offset: 0x002357B5
		public void SetData(string title, string desc, int boardID)
		{
			this.SetData(title, desc, NKCLeaderBoardManager.GetLeaderBoardData(boardID), NKCLeaderBoardManager.GetMyRankSlotData(boardID));
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x002375CC File Offset: 0x002357CC
		public void SetData(NKMShadowPalaceTemplet palaceTemplet, List<LeaderBoardSlotData> lstRankData, LeaderBoardSlotData myRankData, int myRank)
		{
			NKCUtil.SetLabelText(this.m_txtPalaceNum, NKCUtilString.GET_SHADOW_PALACE_NUMBER, new object[]
			{
				palaceTemplet.PALACE_NUM_UI
			});
			NKCUtil.SetLabelText(this.m_txtPalaceName, palaceTemplet.PalaceName);
			this.m_lstRankData = lstRankData;
			this.m_scrollRect.TotalCount = lstRankData.Count;
			this.m_scrollRect.RefreshCells(true);
			this.m_scrollRect.SetIndexPosition(0);
			this.m_myRank.Init();
			this.m_myRank.SetData(myRankData, myRank, true);
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x00237658 File Offset: 0x00235858
		public void PlayAni(bool bOpen)
		{
			if (bOpen)
			{
				this.m_ani.Play("NKM_UI_SHADOW_INFO_INTRO");
				this.m_imgBG.raycastTarget = true;
				return;
			}
			this.m_ani.Play("NKM_UI_SHADOW_INFO_OUTRO");
			this.m_imgBG.raycastTarget = false;
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x00237698 File Offset: 0x00235898
		private RectTransform OnGetObject(int index)
		{
			if (this.m_stkSlotPool.Count > 0)
			{
				return this.m_stkSlotPool.Pop().GetComponent<RectTransform>();
			}
			NKCUIShadowPalaceRankSlot nkcuishadowPalaceRankSlot = UnityEngine.Object.Instantiate<NKCUIShadowPalaceRankSlot>(this.m_prefabSlot);
			nkcuishadowPalaceRankSlot.transform.SetParent(this.m_scrollRect.content);
			nkcuishadowPalaceRankSlot.Init();
			return nkcuishadowPalaceRankSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x002376F0 File Offset: 0x002358F0
		private void OnProvideData(Transform tr, int idx)
		{
			NKCUIShadowPalaceRankSlot component = tr.GetComponent<NKCUIShadowPalaceRankSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(this.m_lstRankData[idx], idx + 1, false);
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x00237724 File Offset: 0x00235924
		private void OnReturnObject(Transform go)
		{
			if (base.GetComponent<NKCUIShadowPalaceRankSlot>() != null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(base.transform);
			this.m_stkSlotPool.Push(go.GetComponent<NKCUIShadowPalaceRankSlot>());
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x00237759 File Offset: 0x00235959
		private void OnClose()
		{
			this.PlayAni(false);
		}

		// Token: 0x04005840 RID: 22592
		public Animator m_ani;

		// Token: 0x04005841 RID: 22593
		public Image m_imgBG;

		// Token: 0x04005842 RID: 22594
		[Header("Info")]
		public Text m_txtPalaceNum;

		// Token: 0x04005843 RID: 22595
		public Text m_txtPalaceName;

		// Token: 0x04005844 RID: 22596
		[Header("Rank")]
		public LoopScrollRect m_scrollRect;

		// Token: 0x04005845 RID: 22597
		public NKCUIShadowPalaceRankSlot m_prefabSlot;

		// Token: 0x04005846 RID: 22598
		public NKCUIShadowPalaceRankSlot m_myRank;

		// Token: 0x04005847 RID: 22599
		[Header("Button")]
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04005848 RID: 22600
		private Stack<NKCUIShadowPalaceRankSlot> m_stkSlotPool = new Stack<NKCUIShadowPalaceRankSlot>();

		// Token: 0x04005849 RID: 22601
		private List<LeaderBoardSlotData> m_lstRankData = new List<LeaderBoardSlotData>();
	}
}

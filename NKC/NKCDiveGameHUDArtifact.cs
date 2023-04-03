using System;
using System.Collections.Generic;
using NKC.UI;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007A4 RID: 1956
	public class NKCDiveGameHUDArtifact : MonoBehaviour
	{
		// Token: 0x06004CF7 RID: 19703 RVA: 0x001716C0 File Offset: 0x0016F8C0
		private void OnFinishScrollToArtifactDummySlot()
		{
			if (this.m_dOnFinishScrollToArtifactDummySlot != null)
			{
				this.m_dOnFinishScrollToArtifactDummySlot();
			}
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x001716D8 File Offset: 0x0016F8D8
		public void InitUI(NKCDiveGameHUDArtifact.dOnFinishScrollToArtifactDummySlot _dOnFinishScrollToArtifactDummySlot)
		{
			this.m_dOnFinishScrollToArtifactDummySlot = _dOnFinishScrollToArtifactDummySlot;
			this.m_csbtnOpen.PointerClick.RemoveAllListeners();
			this.m_csbtnOpen.PointerClick.AddListener(new UnityAction(this.Open));
			this.m_csbtnClose.PointerClick.RemoveAllListeners();
			this.m_csbtnClose.PointerClick.AddListener(new UnityAction(this.CloseWithAnimate));
			this.m_csbtnTabChange.OnValueChanged.RemoveAllListeners();
			this.m_csbtnTabChange.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedViewTab));
			this.m_csbtnHelp.PointerClick.RemoveAllListeners();
			this.m_csbtnHelp.PointerClick.AddListener(new UnityAction(this.OnClickHelp));
			this.m_LoopScrollRect.dOnGetObject += this.GetArtifactSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnArtifactSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideArtifactSlotData;
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x001717E4 File Offset: 0x0016F9E4
		public void RefreshInvenry()
		{
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			if (diveGameData == null)
			{
				return;
			}
			this.m_LoopScrollRect.TotalCount = diveGameData.Player.PlayerBase.Artifacts.Count;
			this.m_LoopScrollRect.RefreshCells(false);
			this.UpdateTotalViewTextUI();
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x00171834 File Offset: 0x0016FA34
		public RectTransform GetArtifactSlot(int index)
		{
			NKCUIDiveGameArtifactSlot nkcuidiveGameArtifactSlot;
			if (this.m_stkNKCUIDiveGameArtifactSlot.Count > 0)
			{
				nkcuidiveGameArtifactSlot = this.m_stkNKCUIDiveGameArtifactSlot.Pop();
			}
			else
			{
				nkcuidiveGameArtifactSlot = UnityEngine.Object.Instantiate<NKCUIDiveGameArtifactSlot>(this.m_pfbNKCUIDiveGameArtifactSlot);
				nkcuidiveGameArtifactSlot.InitUI();
			}
			return nkcuidiveGameArtifactSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x00171878 File Offset: 0x0016FA78
		public void ReturnArtifactSlot(Transform tr)
		{
			NKCUIDiveGameArtifactSlot component = tr.GetComponent<NKCUIDiveGameArtifactSlot>();
			this.m_stkNKCUIDiveGameArtifactSlot.Push(component);
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x001718AC File Offset: 0x0016FAAC
		public void ProvideArtifactSlotData(Transform tr, int index)
		{
			NKCUIDiveGameArtifactSlot component = tr.GetComponent<NKCUIDiveGameArtifactSlot>();
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			if (diveGameData == null)
			{
				return;
			}
			if (index >= diveGameData.Player.PlayerBase.Artifacts.Count)
			{
				component.SetData(null);
				return;
			}
			if (this.m_bDummySlot && index == diveGameData.Player.PlayerBase.Artifacts.Count - 1)
			{
				component.SetData(null);
				return;
			}
			NKMDiveArtifactTemplet data = NKMDiveArtifactTemplet.Find(diveGameData.Player.PlayerBase.Artifacts[index]);
			component.SetData(data);
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0017193C File Offset: 0x0016FB3C
		public void SetDummySlot()
		{
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			if (diveGameData == null)
			{
				return;
			}
			this.m_bDummySlot = true;
			this.m_LoopScrollRect.TotalCount = diveGameData.Player.PlayerBase.Artifacts.Count;
			this.m_LoopScrollRect.RefreshCells(false);
			this.m_LoopScrollRect.ScrollToCell(this.m_LoopScrollRect.TotalCount - 1, 0.2f, LoopScrollRect.ScrollTarget.Top, new UnityAction(this.OnFinishScrollToArtifactDummySlot));
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x001719B5 File Offset: 0x0016FBB5
		public void InvalidDummySlot()
		{
			this.m_bDummySlot = false;
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x001719C0 File Offset: 0x0016FBC0
		public Vector3 GetLastItemSlotImgPos()
		{
			Transform lastActivatedItem = this.m_LoopScrollRect.GetLastActivatedItem();
			if (lastActivatedItem != null)
			{
				NKCUIDiveGameArtifactSlot component = lastActivatedItem.GetComponent<NKCUIDiveGameArtifactSlot>();
				if (component != null)
				{
					return component.m_NKCUISlot.transform.position;
				}
			}
			return new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x00171A17 File Offset: 0x0016FC17
		private void OnClickHelp()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_DIVE_ARTIFACT", 0);
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x00171A2C File Offset: 0x0016FC2C
		public void ResetUI(bool bHurdle = false)
		{
			this.m_bClosed = true;
			this.m_bEachView = false;
			this.m_csbtnTabChange.Select(true, true, false);
			if (this.m_bFirstOpen)
			{
				this.m_LoopScrollRect.PrepareCells(0);
			}
			this.m_bFirstOpen = false;
			NKCUtil.SetGameobjectActive(this.m_objNormalBG, !bHurdle);
			NKCUtil.SetGameobjectActive(this.m_objHurdleBG, bHurdle);
			int totalCount = 0;
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			if (diveGameData != null)
			{
				totalCount = diveGameData.Player.PlayerBase.Artifacts.Count;
			}
			this.m_LoopScrollRect.velocity = new Vector2(0f, 0f);
			this.m_LoopScrollRect.TotalCount = totalCount;
			this.m_LoopScrollRect.SetIndexPosition(0);
			this.UpdateTotalViewTextUI();
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x00171AEC File Offset: 0x0016FCEC
		public void Open()
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			if (!this.m_bClosed)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnOpen, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnClose, true);
			if (!this.m_bEachView)
			{
				this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SQUAD_LEFT_ARTIFACT_CONTENT_OPEN");
				return;
			}
			this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SQUAD_LEFT_ARTIFACT_CONTENT_OPEN_EACH");
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x00171B54 File Offset: 0x0016FD54
		public void UpdateTotalViewTextUI()
		{
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			if (diveGameData == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTotalViewDesc, NKCUtilString.GetDiveArtifactTotalViewDesc(diveGameData.Player.PlayerBase.Artifacts));
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x00171B90 File Offset: 0x0016FD90
		private void OnChangedViewTab(bool bChecked)
		{
			this.m_bEachView = !bChecked;
			if (this.m_bEachView)
			{
				this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SQUAD_LEFT_ARTIFACT_CONTENT_OPENTOEACH");
				return;
			}
			this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SQUAD_LEFT_ARTIFACT_CONTENT_EACHTOOPEN");
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00171BC5 File Offset: 0x0016FDC5
		private void CloseWithAnimate()
		{
			this.Close(true);
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x00171BD0 File Offset: 0x0016FDD0
		public void Close(bool bAnimate)
		{
			this.m_bClosed = true;
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnOpen, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnClose, false);
			if (!bAnimate)
			{
				this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SQUAD_LEFT_ARTIFACT_CONTENT_CLOSE_IDLE");
				return;
			}
			if (!this.m_bEachView)
			{
				this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SQUAD_LEFT_ARTIFACT_CONTENT_CLOSE");
				return;
			}
			this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SQUAD_LEFT_ARTIFACT_CONTENT_CLOSE_EACH");
		}

		// Token: 0x04003CC2 RID: 15554
		public GameObject m_objNormalBG;

		// Token: 0x04003CC3 RID: 15555
		public GameObject m_objHurdleBG;

		// Token: 0x04003CC4 RID: 15556
		public Animator m_Animator;

		// Token: 0x04003CC5 RID: 15557
		public NKCUIComStateButton m_csbtnOpen;

		// Token: 0x04003CC6 RID: 15558
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04003CC7 RID: 15559
		public NKCUIComToggle m_csbtnTabChange;

		// Token: 0x04003CC8 RID: 15560
		public NKCUIComStateButton m_csbtnHelp;

		// Token: 0x04003CC9 RID: 15561
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04003CCA RID: 15562
		public NKCUIDiveGameArtifactSlot m_pfbNKCUIDiveGameArtifactSlot;

		// Token: 0x04003CCB RID: 15563
		private Stack<NKCUIDiveGameArtifactSlot> m_stkNKCUIDiveGameArtifactSlot = new Stack<NKCUIDiveGameArtifactSlot>();

		// Token: 0x04003CCC RID: 15564
		public Text m_lbTotalViewDesc;

		// Token: 0x04003CCD RID: 15565
		private bool m_bClosed = true;

		// Token: 0x04003CCE RID: 15566
		private bool m_bEachView = true;

		// Token: 0x04003CCF RID: 15567
		private bool m_bFirstOpen = true;

		// Token: 0x04003CD0 RID: 15568
		private bool m_bDummySlot;

		// Token: 0x04003CD1 RID: 15569
		private NKCDiveGameHUDArtifact.dOnFinishScrollToArtifactDummySlot m_dOnFinishScrollToArtifactDummySlot;

		// Token: 0x02001463 RID: 5219
		// (Invoke) Token: 0x0600A894 RID: 43156
		public delegate void dOnFinishScrollToArtifactDummySlot();
	}
}

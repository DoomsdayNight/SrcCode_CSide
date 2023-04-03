using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B7 RID: 1975
	public class NKCUICutScenLogViewer : MonoBehaviour
	{
		// Token: 0x06004E3F RID: 20031 RVA: 0x001795B8 File Offset: 0x001777B8
		private void NullReferenceCheck()
		{
			if (this.m_pfbSlot == null)
			{
				Debug.LogError("m_slot is null");
			}
			if (this.m_loopScroll == null)
			{
				Debug.LogError("m_loopScroll is null");
			}
			if (this.m_btnClose == null)
			{
				Debug.LogError("m_btnClose is null");
			}
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00179610 File Offset: 0x00177810
		private void InitUI()
		{
			this.NullReferenceCheck();
			this.m_loopScroll.SetUseHack(false);
			this.m_loopScroll.SetNoMakeSlotTwo(true);
			this.m_loopScroll.dOnGetObject += this.GetObject;
			this.m_loopScroll.dOnReturnObject += this.ReturnObject;
			this.m_loopScroll.dOnProvideData += this.ProvideData;
			this.m_loopScroll.dOnScrollEvent += this.OnScrollEvent;
			this.m_loopScroll.ContentConstraintCount = 1;
			this.m_loopScroll.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopScroll, null);
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			this.m_bInitComplete = true;
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x001796F0 File Offset: 0x001778F0
		public void OpenUI(List<string> lstDesc, NKCUICutScenLogViewer.OnButton onCloseButton, bool bTitleEnabled, bool bTalkBoxEnabled, bool bAutoEnabled, NKMStageTempletV2 stageTemplet)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.dOnCloseButton = onCloseButton;
			this.m_bTitleEnabled = bTitleEnabled;
			this.m_bTalkBoxEnabled = bTalkBoxEnabled;
			this.m_bAutoEnabled = bAutoEnabled;
			this.m_lstData.Clear();
			this.m_lstData.AddRange(lstDesc);
			this.m_loopScroll.TotalCount = this.m_lstData.Count;
			this.m_loopScroll.SetIndexPosition(this.m_lstData.Count - 1);
			this.m_txtEP.text = "";
			this.m_txtStage.text = "";
			if (stageTemplet != null)
			{
				string text = string.Empty;
				switch (stageTemplet.m_STAGE_TYPE)
				{
				case STAGE_TYPE.ST_WARFARE:
				{
					NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(stageTemplet.m_StageBattleStrID);
					if (nkmwarfareTemplet != null)
					{
						text = nkmwarfareTemplet.GetWarfareName();
						goto IL_110;
					}
					goto IL_110;
				}
				case STAGE_TYPE.ST_PHASE:
					if (stageTemplet.PhaseTemplet != null)
					{
						text = stageTemplet.PhaseTemplet.GetName();
						goto IL_110;
					}
					goto IL_110;
				}
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(stageTemplet.m_StageBattleStrID);
				if (dungeonTempletBase != null)
				{
					text = dungeonTempletBase.GetDungeonName();
				}
				IL_110:
				if (!string.IsNullOrEmpty(text))
				{
					this.m_txtEP.text = stageTemplet.EpisodeTemplet.GetEpisodeTitle();
					this.m_txtStage.text = string.Format("{0}-{1} {2}", stageTemplet.ActId, stageTemplet.m_StageUINum, text);
				}
			}
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0017985A File Offset: 0x00177A5A
		public void OnClickClose()
		{
			this.m_lstData.Clear();
			NKCUICutScenLogViewer.OnButton onButton = this.dOnCloseButton;
			if (onButton != null)
			{
				onButton(this.m_bTitleEnabled, this.m_bTalkBoxEnabled, this.m_bAutoEnabled);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x00179898 File Offset: 0x00177A98
		private RectTransform GetObject(int index)
		{
			if (this.m_stkSlot.Count == 0)
			{
				NKCUICutScenLogViewerSlot nkcuicutScenLogViewerSlot = UnityEngine.Object.Instantiate<NKCUICutScenLogViewerSlot>(this.m_pfbSlot, this.m_trSlotParent);
				this.m_lstSlot.Add(nkcuicutScenLogViewerSlot);
				nkcuicutScenLogViewerSlot.transform.localScale = Vector3.one;
				NKCUtil.SetGameobjectActive(nkcuicutScenLogViewerSlot.gameObject, true);
				return nkcuicutScenLogViewerSlot.GetComponent<RectTransform>();
			}
			NKCUICutScenLogViewerSlot nkcuicutScenLogViewerSlot2 = this.m_stkSlot.Pop();
			this.m_lstSlot.Add(nkcuicutScenLogViewerSlot2);
			NKCUtil.SetGameobjectActive(nkcuicutScenLogViewerSlot2.gameObject, true);
			nkcuicutScenLogViewerSlot2.transform.SetParent(this.m_trSlotParent);
			return nkcuicutScenLogViewerSlot2.GetComponent<RectTransform>();
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x00179930 File Offset: 0x00177B30
		private void ReturnObject(Transform go)
		{
			NKCUICutScenLogViewerSlot component = go.GetComponent<NKCUICutScenLogViewerSlot>();
			if (this.m_lstSlot.Contains(component))
			{
				this.m_lstSlot.Remove(component);
			}
			if (!this.m_stkSlot.Contains(component))
			{
				this.m_stkSlot.Push(component);
			}
			NKCUtil.SetGameobjectActive(component.gameObject, false);
			go.SetParent(base.transform);
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x00179994 File Offset: 0x00177B94
		private void ProvideData(Transform transform, int idx)
		{
			NKCUICutScenLogViewerSlot component = transform.GetComponent<NKCUICutScenLogViewerSlot>();
			NKCUtil.SetGameobjectActive(component.gameObject, true);
			if (idx < 0 || idx >= this.m_lstData.Count)
			{
				component.SetData("");
				return;
			}
			component.SetData(this.m_lstData[idx]);
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x001799E4 File Offset: 0x00177BE4
		private void OnScrollEvent(PointerEventData data, Vector2 normalizedPositionBefore, Vector2 normalizedPositionAfter)
		{
			if (data.scrollDelta.y < 0f && normalizedPositionBefore.y >= 0.999f && normalizedPositionAfter.y >= 1f)
			{
				this.OnClickClose();
			}
		}

		// Token: 0x04003DD8 RID: 15832
		public NKCUICutScenLogViewerSlot m_pfbSlot;

		// Token: 0x04003DD9 RID: 15833
		public LoopScrollRect m_loopScroll;

		// Token: 0x04003DDA RID: 15834
		public Transform m_trSlotParent;

		// Token: 0x04003DDB RID: 15835
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04003DDC RID: 15836
		public Text m_txtEP;

		// Token: 0x04003DDD RID: 15837
		public Text m_txtStage;

		// Token: 0x04003DDE RID: 15838
		private List<NKCUICutScenLogViewerSlot> m_lstSlot = new List<NKCUICutScenLogViewerSlot>();

		// Token: 0x04003DDF RID: 15839
		private Stack<NKCUICutScenLogViewerSlot> m_stkSlot = new Stack<NKCUICutScenLogViewerSlot>();

		// Token: 0x04003DE0 RID: 15840
		private List<string> m_lstData = new List<string>();

		// Token: 0x04003DE1 RID: 15841
		private bool m_bInitComplete;

		// Token: 0x04003DE2 RID: 15842
		private bool m_bTitleEnabled;

		// Token: 0x04003DE3 RID: 15843
		private bool m_bTalkBoxEnabled;

		// Token: 0x04003DE4 RID: 15844
		private bool m_bAutoEnabled;

		// Token: 0x04003DE5 RID: 15845
		private NKCUICutScenLogViewer.OnButton dOnCloseButton;

		// Token: 0x02001476 RID: 5238
		// (Invoke) Token: 0x0600A8DC RID: 43228
		public delegate void OnButton(bool bTitleEnabled, bool bTalkBoxEnabled, bool bAutoEnabled);
	}
}

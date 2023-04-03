using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009F0 RID: 2544
	public class NKCUIStageViewer : MonoBehaviour, INKCUIStageViewer
	{
		// Token: 0x06006DF4 RID: 28148 RVA: 0x00240DD7 File Offset: 0x0023EFD7
		public int GetCurActID()
		{
			return this.m_ActID;
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x00240DDF File Offset: 0x0023EFDF
		public void ResetPosition(Transform parent)
		{
			base.transform.SetParent(parent, false);
			base.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			base.GetComponent<RectTransform>().localScale = Vector3.one;
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x00240E0E File Offset: 0x0023F00E
		public int GetActCount(EPISODE_DIFFICULTY Difficulty)
		{
			if (Difficulty == EPISODE_DIFFICULTY.NORMAL)
			{
				return this.m_lstNormalAct.Count;
			}
			return this.m_lstHardAct.Count;
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x00240E2C File Offset: 0x0023F02C
		public float GetTargetNormalizedPos(int slotIndex)
		{
			for (int i = 0; i < this.m_lstNormalAct.Count; i++)
			{
				if (this.m_lstNormalAct[i].gameObject.activeSelf)
				{
					return this.m_lstNormalAct[i].GetTargetNormalizedPos(slotIndex);
				}
			}
			for (int j = 0; j < this.m_lstHardAct.Count; j++)
			{
				if (this.m_lstHardAct[j].gameObject.activeSelf)
				{
					return this.m_lstHardAct[j].GetTargetNormalizedPos(slotIndex);
				}
			}
			return 1f;
		}

		// Token: 0x06006DF8 RID: 28152 RVA: 0x00240EC0 File Offset: 0x0023F0C0
		public void SetActive(bool bValue)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, bValue);
		}

		// Token: 0x06006DF9 RID: 28153 RVA: 0x00240ED0 File Offset: 0x0023F0D0
		public void SetSelectNode(NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet != null)
			{
				List<NKCUIStageViewerAct> list = new List<NKCUIStageViewerAct>();
				if (stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL)
				{
					list = this.m_lstNormalAct;
				}
				else if (stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.HARD)
				{
					list = this.m_lstHardAct;
				}
				list[stageTemplet.ActId - 1].SelectNode(stageTemplet);
				return;
			}
			for (int i = 0; i < this.m_lstNormalAct.Count; i++)
			{
				if (this.m_lstNormalAct[i].gameObject.activeSelf)
				{
					this.m_lstNormalAct[i].SelectNode(null);
				}
			}
			for (int j = 0; j < this.m_lstHardAct.Count; j++)
			{
				if (this.m_lstHardAct[j].gameObject.activeSelf)
				{
					this.m_lstHardAct[j].SelectNode(null);
				}
			}
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x00240F9C File Offset: 0x0023F19C
		public Vector2 SetData(bool bUseEpSlot, int EpisodeID, int ActID, EPISODE_DIFFICULTY Difficulty, IDungeonSlot.OnSelectedItemSlot onSelectedSlot, EPISODE_SCROLL_TYPE scrollType)
		{
			this.m_bUseEpSlot = bUseEpSlot;
			this.m_EpisodeID = EpisodeID;
			this.m_ActID = ActID;
			this.m_Difficulty = Difficulty;
			this.m_dOnSelectedSlot = onSelectedSlot;
			this.m_ScrollType = scrollType;
			for (int i = 0; i < this.m_lstNormalAct.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstNormalAct[i], false);
			}
			for (int j = 0; j < this.m_lstHardAct.Count; j++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstHardAct[j], false);
			}
			List<NKCUIStageViewerAct> list = new List<NKCUIStageViewerAct>();
			if (Difficulty == EPISODE_DIFFICULTY.NORMAL)
			{
				list = this.m_lstNormalAct;
			}
			else if (Difficulty == EPISODE_DIFFICULTY.HARD)
			{
				list = this.m_lstHardAct;
			}
			if (ActID > list.Count || ActID < 0)
			{
				Log.Error(string.Format("잘못된 ActID 호출 - EpisodeID : {0}. ActID : {1}, Difficulty : {2}", EpisodeID, ActID, Difficulty), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIStageViewer.cs", 128);
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return Vector2.zero;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(list[ActID - 1], true);
			return list[ActID - 1].SetData(EpisodeID, ActID, Difficulty, onSelectedSlot, scrollType);
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x002410C1 File Offset: 0x0023F2C1
		public void RefreshData()
		{
			this.SetData(this.m_bUseEpSlot, this.m_EpisodeID, this.m_ActID, this.m_Difficulty, this.m_dOnSelectedSlot, this.m_ScrollType);
		}

		// Token: 0x0400596D RID: 22893
		[Header("!! 첫 액트부터 차례로 넣어야함 !!")]
		public List<NKCUIStageViewerAct> m_lstNormalAct = new List<NKCUIStageViewerAct>();

		// Token: 0x0400596E RID: 22894
		public List<NKCUIStageViewerAct> m_lstHardAct = new List<NKCUIStageViewerAct>();

		// Token: 0x0400596F RID: 22895
		private bool m_bUseEpSlot;

		// Token: 0x04005970 RID: 22896
		private int m_EpisodeID;

		// Token: 0x04005971 RID: 22897
		private int m_ActID;

		// Token: 0x04005972 RID: 22898
		private EPISODE_DIFFICULTY m_Difficulty;

		// Token: 0x04005973 RID: 22899
		private IDungeonSlot.OnSelectedItemSlot m_dOnSelectedSlot;

		// Token: 0x04005974 RID: 22900
		private EPISODE_SCROLL_TYPE m_ScrollType;
	}
}

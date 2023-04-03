using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A18 RID: 2584
	public class NKCUIStageViewerV2 : MonoBehaviour, INKCUIStageViewer
	{
		// Token: 0x060070DA RID: 28890 RVA: 0x00257310 File Offset: 0x00255510
		public int GetCurActID()
		{
			return this.m_ActID;
		}

		// Token: 0x060070DB RID: 28891 RVA: 0x00257318 File Offset: 0x00255518
		public void ResetPosition(Transform parent)
		{
			base.transform.SetParent(parent, false);
			base.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			base.GetComponent<RectTransform>().localScale = Vector3.one;
		}

		// Token: 0x060070DC RID: 28892 RVA: 0x00257347 File Offset: 0x00255547
		public int GetActCount(EPISODE_DIFFICULTY Difficulty)
		{
			if (Difficulty == EPISODE_DIFFICULTY.NORMAL)
			{
				return this.m_lstNormalAct.Count;
			}
			return this.m_lstHardAct.Count;
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x00257364 File Offset: 0x00255564
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
			return 0f;
		}

		// Token: 0x060070DE RID: 28894 RVA: 0x002573F8 File Offset: 0x002555F8
		public void SetActive(bool bValue)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, bValue);
		}

		// Token: 0x060070DF RID: 28895 RVA: 0x00257408 File Offset: 0x00255608
		public void SetSelectNode(NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
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
				return;
			}
			List<NKCUIStagePrefabAct> list = new List<NKCUIStagePrefabAct>();
			if (stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.NORMAL)
			{
				list = this.m_lstNormalAct;
			}
			else if (stageTemplet.m_Difficulty == EPISODE_DIFFICULTY.HARD)
			{
				list = this.m_lstHardAct;
			}
			if (!stageTemplet.EpisodeTemplet.UseEpSlot())
			{
				list[stageTemplet.ActId - 1].SelectNode(stageTemplet);
				return;
			}
			int index = list.FindIndex((NKCUIStagePrefabAct x) => x.GetEpisodeID() == stageTemplet.EpisodeId);
			list[index].SelectNode(stageTemplet);
		}

		// Token: 0x060070E0 RID: 28896 RVA: 0x0025753C File Offset: 0x0025573C
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
			List<NKCUIStagePrefabAct> list = new List<NKCUIStagePrefabAct>();
			if (Difficulty == EPISODE_DIFFICULTY.NORMAL)
			{
				list = this.m_lstNormalAct;
			}
			else if (Difficulty == EPISODE_DIFFICULTY.HARD)
			{
				list = this.m_lstHardAct;
			}
			if (!bUseEpSlot)
			{
				if (NKMEpisodeTempletV2.Find(EpisodeID, Difficulty) != null)
				{
					if (ActID > list.Count || ActID < 0)
					{
						NKCUtil.SetGameobjectActive(base.gameObject, false);
						return Vector2.zero;
					}
					NKCUtil.SetGameobjectActive(base.gameObject, true);
					NKCUtil.SetGameobjectActive(list[ActID - 1], true);
					return list[ActID - 1].SetData(EpisodeID, ActID, Difficulty, onSelectedSlot, scrollType);
				}
			}
			else
			{
				NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(EpisodeID);
				if (nkmepisodeGroupTemplet != null)
				{
					int index = nkmepisodeGroupTemplet.lstEpisodeTemplet.FindIndex((NKMEpisodeTempletV2 x) => x.m_EpisodeID == ActID);
					NKMEpisodeTempletV2 nkmepisodeTempletV = nkmepisodeGroupTemplet.lstEpisodeTemplet.Find((NKMEpisodeTempletV2 x) => x.m_EpisodeID == ActID);
					if (nkmepisodeTempletV != null)
					{
						NKCUtil.SetGameobjectActive(base.gameObject, true);
						NKCUtil.SetGameobjectActive(list[index], true);
						return list[index].SetData(nkmepisodeTempletV.m_EpisodeID, 1, Difficulty, onSelectedSlot, scrollType);
					}
				}
			}
			return Vector2.zero;
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x002576F0 File Offset: 0x002558F0
		public void RefreshData()
		{
			this.SetData(this.m_bUseEpSlot, this.m_EpisodeID, this.m_ActID, this.m_Difficulty, this.m_dOnSelectedSlot, this.m_ScrollType);
		}

		// Token: 0x04005C9E RID: 23710
		[Header("!! 첫 액트부터 차례로 넣어야함 !!")]
		public List<NKCUIStagePrefabAct> m_lstNormalAct = new List<NKCUIStagePrefabAct>();

		// Token: 0x04005C9F RID: 23711
		public List<NKCUIStagePrefabAct> m_lstHardAct = new List<NKCUIStagePrefabAct>();

		// Token: 0x04005CA0 RID: 23712
		private bool m_bUseEpSlot;

		// Token: 0x04005CA1 RID: 23713
		private int m_EpisodeID;

		// Token: 0x04005CA2 RID: 23714
		private int m_ActID;

		// Token: 0x04005CA3 RID: 23715
		private EPISODE_DIFFICULTY m_Difficulty;

		// Token: 0x04005CA4 RID: 23716
		private IDungeonSlot.OnSelectedItemSlot m_dOnSelectedSlot;

		// Token: 0x04005CA5 RID: 23717
		private EPISODE_SCROLL_TYPE m_ScrollType;
	}
}

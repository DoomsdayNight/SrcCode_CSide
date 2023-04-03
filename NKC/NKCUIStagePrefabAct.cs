using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A16 RID: 2582
	public class NKCUIStagePrefabAct : MonoBehaviour
	{
		// Token: 0x060070C3 RID: 28867 RVA: 0x0025645C File Offset: 0x0025465C
		public int GetEpisodeID()
		{
			return this.m_EpisodeID;
		}

		// Token: 0x060070C4 RID: 28868 RVA: 0x00256464 File Offset: 0x00254664
		public float GetTargetNormalizedPos(int targetIndex)
		{
			if (this.m_lstItemSlot.Count <= 1)
			{
				return 0f;
			}
			if (targetIndex < 0)
			{
				targetIndex = 0;
			}
			else if (targetIndex >= this.m_lstItemSlot.Count)
			{
				targetIndex = this.m_lstItemSlot.Count - 1;
			}
			LayoutElement component = this.m_lstItemSlot[0].GetComponent<LayoutElement>();
			if (component == null)
			{
				float num = this.m_lstItemSlot[0].transform.position.x;
				float num2 = this.m_lstItemSlot[this.m_lstItemSlot.Count - 1].transform.position.x;
				for (int i = 0; i < this.m_lstItemSlot.Count; i++)
				{
					float x = this.m_lstItemSlot[i].transform.position.x;
					if (x < num)
					{
						num = x;
					}
					if (x > num2)
					{
						num2 = x;
					}
				}
				return (this.m_lstItemSlot[targetIndex].transform.position.x - num) / (num2 - num);
			}
			float num3 = this.m_lstItemSlot[0].transform.position.x;
			float num4 = this.m_lstItemSlot[0].transform.position.x + component.preferredWidth * (float)(this.m_lstItemSlot.Count - 1);
			for (int j = 0; j < this.m_lstItemSlot.Count; j++)
			{
				float num5 = this.m_lstItemSlot[0].transform.position.x + component.preferredWidth * (float)j;
				if (num5 < num3)
				{
					num3 = num5;
				}
				if (num5 > num4)
				{
					num4 = num5;
				}
			}
			return (this.m_lstItemSlot[0].transform.position.x + component.preferredWidth * (float)targetIndex - num3) / (num4 - num3);
		}

		// Token: 0x060070C5 RID: 28869 RVA: 0x00256648 File Offset: 0x00254848
		public Vector2 SetData(int EpisodeID, int ActID, EPISODE_DIFFICULTY Difficulty, IDungeonSlot.OnSelectedItemSlot onSelectedSlot, EPISODE_SCROLL_TYPE scrollType)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(EpisodeID, Difficulty);
			if (nkmepisodeTempletV == null)
			{
				return Vector2.zero;
			}
			if (!nkmepisodeTempletV.m_DicStage.ContainsKey(ActID))
			{
				Log.Error(string.Format("액트를 찾을 수 없음 - EpisodeID {0}, ActID {1}, Difficulty {2}", EpisodeID, ActID, Difficulty), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIStagePrefabAct.cs", 86);
				return Vector2.zero;
			}
			int count = nkmepisodeTempletV.m_DicStage[ActID].Count;
			if (count > this.m_lstItemSlot.Count)
			{
				Log.Error(string.Format("Stage 숫자가 프리팹 숫자보다 많음 - EpisodeID {0}, ActID {1}, Difficulty {2}, DungeonCount : {3}, SlotCount : {4}", new object[]
				{
					EpisodeID,
					ActID,
					Difficulty,
					count,
					this.m_lstItemSlot.Count
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIStagePrefabAct.cs", 94);
				return Vector2.zero;
			}
			if (this.m_lstItemSlot.Contains(null))
			{
				Log.Error(string.Format("m_lstItemSlot 에 노드 링크가 잘못됨 - EpisodeID {0}, ActID {1}, Difficulty {2}, index : {3}", new object[]
				{
					EpisodeID,
					ActID,
					Difficulty,
					this.m_lstItemSlot.FindIndex(null)
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIStagePrefabAct.cs", 100);
				return Vector2.zero;
			}
			this.m_EpisodeID = EpisodeID;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.zero;
			switch (scrollType)
			{
			case EPISODE_SCROLL_TYPE.HORIZONTAL:
				vector2.x = ((this.m_lstItemSlot.Count > 0) ? this.m_lstItemSlot[0].transform.localPosition.x : 0f);
				vector3.x = ((this.m_lstItemSlot.Count > 0) ? (this.m_lstItemSlot[this.m_lstItemSlot.Count - 1].transform.localPosition.x - vector2.x) : 0f);
				break;
			case EPISODE_SCROLL_TYPE.VERTICAL:
				vector2.y = ((this.m_lstItemSlot.Count > 0) ? this.m_lstItemSlot[0].transform.localPosition.y : 0f);
				vector3.y = ((this.m_lstItemSlot.Count > 0) ? (this.m_lstItemSlot[this.m_lstItemSlot.Count - 1].transform.localPosition.y - vector2.y) : 0f);
				break;
			case EPISODE_SCROLL_TYPE.FREE:
				vector2 = ((this.m_lstItemSlot.Count > 0) ? this.m_lstItemSlot[0].transform.localPosition : Vector3.zero);
				vector3 = ((this.m_lstItemSlot.Count > 0) ? (this.m_lstItemSlot[this.m_lstItemSlot.Count - 1].transform.localPosition - vector2) : Vector3.zero);
				break;
			}
			for (int i = 0; i < this.m_lstItemSlot.Count; i++)
			{
				NKCUIStagePrefabNode nkcuistagePrefabNode = this.m_lstItemSlot[i];
				this.InitSlot(nkcuistagePrefabNode);
				nkcuistagePrefabNode.SetEnableNewMark(false);
				nkcuistagePrefabNode.SetOnSelectedItemSlot(onSelectedSlot);
				if (i < count)
				{
					NKMStageTempletV2 nkmstageTempletV = nkmepisodeTempletV.m_DicStage[ActID][i];
					bool flag = NKMEpisodeMgr.CheckEpisodeMission(myUserData, nkmstageTempletV);
					if (!nkmstageTempletV.EnableByTag)
					{
						NKCUtil.SetGameobjectActive(nkcuistagePrefabNode, false);
					}
					else if (nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_OPEN)
					{
						if (flag)
						{
							vector = nkcuistagePrefabNode.transform.localPosition;
							nkcuistagePrefabNode.SetData(nkmstageTempletV, onSelectedSlot);
							if (!PlayerPrefs.HasKey(string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, nkmstageTempletV.m_StageBattleStrID)) && !myUserData.CheckStageCleared(nkmstageTempletV))
							{
								nkcuistagePrefabNode.SetEnableNewMark(true);
							}
							else
							{
								nkcuistagePrefabNode.SetEnableNewMark(false);
							}
							if (!nkcuistagePrefabNode.IsActive())
							{
								NKCUtil.SetGameobjectActive(nkcuistagePrefabNode, true);
							}
						}
						else if (nkcuistagePrefabNode.IsActive())
						{
							NKCUtil.SetGameobjectActive(nkcuistagePrefabNode, false);
						}
					}
					else if (nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_LOCK)
					{
						nkcuistagePrefabNode.SetData(nkmstageTempletV, onSelectedSlot);
						if (!nkcuistagePrefabNode.CheckLock())
						{
							vector = nkcuistagePrefabNode.transform.localPosition;
						}
						if (flag && !PlayerPrefs.HasKey(string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, nkmstageTempletV.m_StageBattleStrID)) && !myUserData.CheckStageCleared(nkmstageTempletV))
						{
							nkcuistagePrefabNode.SetEnableNewMark(true);
						}
						else
						{
							nkcuistagePrefabNode.SetEnableNewMark(false);
						}
						if (!nkcuistagePrefabNode.IsActive())
						{
							NKCUtil.SetGameobjectActive(nkcuistagePrefabNode, true);
						}
					}
					else if (nkcuistagePrefabNode.IsActive())
					{
						NKCUtil.SetGameobjectActive(nkcuistagePrefabNode, false);
					}
				}
				else if (nkcuistagePrefabNode.IsActive())
				{
					NKCUtil.SetGameobjectActive(nkcuistagePrefabNode, false);
				}
			}
			if (vector3 == Vector3.zero)
			{
				return Vector2.zero;
			}
			float x = (vector3.x == 0f) ? 0f : ((vector.x - vector2.x) / vector3.x);
			float y = (vector3.y == 0f) ? 0f : ((vector.y - vector2.y) / vector3.y);
			return new Vector2(x, y);
		}

		// Token: 0x060070C6 RID: 28870 RVA: 0x00256B74 File Offset: 0x00254D74
		public void SelectNode(NKMStageTempletV2 stageTemplet)
		{
			for (int i = 0; i < this.m_lstItemSlot.Count; i++)
			{
				if (stageTemplet != null)
				{
					this.m_lstItemSlot[i].SetSelectNode(this.m_lstItemSlot[i].GetStageIndex() == stageTemplet.m_StageIndex);
				}
				else
				{
					this.m_lstItemSlot[i].SetSelectNode(false);
				}
			}
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x00256BD8 File Offset: 0x00254DD8
		private void InitSlot(NKCUIStagePrefabNode cItemSlot)
		{
			cItemSlot != null;
		}

		// Token: 0x04005C86 RID: 23686
		[Header("!! 첫 스테이지부터 차례로 넣어야함 !!")]
		public List<NKCUIStagePrefabNode> m_lstItemSlot = new List<NKCUIStagePrefabNode>();

		// Token: 0x04005C87 RID: 23687
		private int m_EpisodeID;
	}
}

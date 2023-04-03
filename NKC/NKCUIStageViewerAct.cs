using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F1 RID: 2545
	public class NKCUIStageViewerAct : MonoBehaviour
	{
		// Token: 0x06006DFD RID: 28157 RVA: 0x0024110C File Offset: 0x0023F30C
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

		// Token: 0x06006DFE RID: 28158 RVA: 0x002412F0 File Offset: 0x0023F4F0
		public Vector2 SetData(int EpisodeID, int ActID, EPISODE_DIFFICULTY Difficulty, IDungeonSlot.OnSelectedItemSlot onSelectedSlot, EPISODE_SCROLL_TYPE scrollType)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(EpisodeID, Difficulty);
			if (nkmepisodeTempletV == null)
			{
				return Vector2.zero;
			}
			if (!nkmepisodeTempletV.m_DicStage.ContainsKey(ActID))
			{
				Log.Error(string.Format("{0} : List<EPExtraData> 찾을 수 없음 - EpisodeID {1}, ActID {2}, Difficulty {3}", new object[]
				{
					base.transform.parent.name,
					EpisodeID,
					ActID,
					Difficulty
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIStageViewerAct.cs", 87);
				return Vector2.zero;
			}
			int count = nkmepisodeTempletV.m_DicStage[ActID].Count;
			if (count > this.m_lstItemSlot.Count)
			{
				Log.Error(string.Format("{0} : Stage 숫자가 프리팹 숫자보다 많음 - EpisodeID {1}, ActID {2}, Difficulty {3}, StageCount : {4}, SlotCount : {5}", new object[]
				{
					base.transform.parent.name,
					EpisodeID,
					ActID,
					Difficulty,
					count,
					this.m_lstItemSlot.Count
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIStageViewerAct.cs", 95);
				return Vector2.zero;
			}
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
				NKCUIEPActDungeonSlot nkcuiepactDungeonSlot = this.m_lstItemSlot[i];
				this.InitSlot(nkcuiepactDungeonSlot);
				nkcuiepactDungeonSlot.SetEnableNewMark(false);
				nkcuiepactDungeonSlot.SetOnSelectedItemSlot(onSelectedSlot);
				if (i < count)
				{
					NKMStageTempletV2 nkmstageTempletV = nkmepisodeTempletV.m_DicStage[ActID][i];
					bool flag = NKMEpisodeMgr.CheckEpisodeMission(myUserData, nkmstageTempletV);
					if (!nkmstageTempletV.EnableByTag)
					{
						nkcuiepactDungeonSlot.SetActive(false);
					}
					else if (nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_OPEN)
					{
						if (flag)
						{
							vector = nkcuiepactDungeonSlot.transform.localPosition;
							nkcuiepactDungeonSlot.SetData(ActID, nkmstageTempletV.m_StageIndex, nkmstageTempletV.m_StageBattleStrID, false, Difficulty);
							if (!PlayerPrefs.HasKey(string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, nkmstageTempletV.m_StageBattleStrID)) && !myUserData.CheckStageCleared(nkmstageTempletV))
							{
								nkcuiepactDungeonSlot.SetEnableNewMark(true);
							}
							else
							{
								nkcuiepactDungeonSlot.SetEnableNewMark(false);
							}
							if (!nkcuiepactDungeonSlot.IsActive())
							{
								nkcuiepactDungeonSlot.SetActive(true);
							}
						}
						else if (nkcuiepactDungeonSlot.IsActive())
						{
							nkcuiepactDungeonSlot.SetActive(false);
						}
					}
					else if (nkmstageTempletV.m_StageBasicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_LOCK)
					{
						nkcuiepactDungeonSlot.SetData(ActID, nkmstageTempletV.m_StageIndex, nkmstageTempletV.m_StageBattleStrID, !flag, Difficulty);
						if (!nkcuiepactDungeonSlot.CheckLock())
						{
							vector = nkcuiepactDungeonSlot.transform.localPosition;
						}
						if (flag && !PlayerPrefs.HasKey(string.Format("{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, nkmstageTempletV.m_StageBattleStrID)) && !myUserData.CheckStageCleared(nkmstageTempletV))
						{
							nkcuiepactDungeonSlot.SetEnableNewMark(true);
						}
						else
						{
							nkcuiepactDungeonSlot.SetEnableNewMark(false);
						}
						if (!nkcuiepactDungeonSlot.IsActive())
						{
							nkcuiepactDungeonSlot.SetActive(true);
						}
					}
					else if (nkcuiepactDungeonSlot.IsActive())
					{
						nkcuiepactDungeonSlot.SetActive(false);
					}
				}
				else if (nkcuiepactDungeonSlot.IsActive())
				{
					nkcuiepactDungeonSlot.SetActive(false);
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

		// Token: 0x06006DFF RID: 28159 RVA: 0x00241810 File Offset: 0x0023FA10
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

		// Token: 0x06006E00 RID: 28160 RVA: 0x00241874 File Offset: 0x0023FA74
		private void InitSlot(NKCUIEPActDungeonSlot cItemSlot)
		{
			cItemSlot != null;
		}

		// Token: 0x04005975 RID: 22901
		[Header("!! 첫 스테이지부터 차례로 넣어야함 !!")]
		public List<NKCUIEPActDungeonSlot> m_lstItemSlot = new List<NKCUIEPActDungeonSlot>();
	}
}

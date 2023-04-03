using System;
using System.Collections.Generic;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A06 RID: 2566
	public class NKCUIOperationSubGrowth : MonoBehaviour
	{
		// Token: 0x06007008 RID: 28680 RVA: 0x002519FC File Offset: 0x0024FBFC
		public void InitUI()
		{
			if (this.m_tglSupply != null)
			{
				this.m_tglSupply.OnValueChanged.RemoveAllListeners();
				this.m_tglSupply.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSupply));
				this.m_tglSupply.m_bGetCallbackWhileLocked = true;
			}
			if (this.m_tglChallenge != null)
			{
				this.m_tglChallenge.OnValueChanged.RemoveAllListeners();
				this.m_tglChallenge.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChallenge));
				this.m_tglChallenge.m_bGetCallbackWhileLocked = true;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			Canvas.ForceUpdateCanvases();
			this.m_loop.PrepareCells(0);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007009 RID: 28681 RVA: 0x00251B04 File Offset: 0x0024FD04
		private RectTransform GetObject(int idx)
		{
			NKCUIOperationSubGrowthEPSlot nkcuioperationSubGrowthEPSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuioperationSubGrowthEPSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuioperationSubGrowthEPSlot = UnityEngine.Object.Instantiate<NKCUIOperationSubGrowthEPSlot>(this.m_pfbSlot, this.m_loop.content);
			}
			nkcuioperationSubGrowthEPSlot.InitUI(new NKCUIOperationSubGrowthEPSlot.OnClickEpSlot(this.OnClickSlot));
			return nkcuioperationSubGrowthEPSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600700A RID: 28682 RVA: 0x00251B60 File Offset: 0x0024FD60
		private void ReturnObject(Transform tr)
		{
			NKCUIOperationSubGrowthEPSlot component = tr.GetComponent<NKCUIOperationSubGrowthEPSlot>();
			if (component == null)
			{
				return;
			}
			this.m_stkSlot.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x0600700B RID: 28683 RVA: 0x00251B94 File Offset: 0x0024FD94
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIOperationSubGrowthEPSlot component = tr.GetComponent<NKCUIOperationSubGrowthEPSlot>();
			if (component == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_dicData[this.m_CurCategory][idx]);
		}

		// Token: 0x0600700C RID: 28684 RVA: 0x00251BD8 File Offset: 0x0024FDD8
		private int CompByKey(NKMEpisodeGroupTemplet lhs, NKMEpisodeGroupTemplet rhs)
		{
			return lhs.Key.CompareTo(rhs.Key);
		}

		// Token: 0x0600700D RID: 28685 RVA: 0x00251BFC File Offset: 0x0024FDFC
		public void Open()
		{
			this.BuildTemplets();
			NKMEpisodeTempletV2 reservedEpisodeTemplet = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet();
			if (reservedEpisodeTemplet != null)
			{
				this.m_CurCategory = reservedEpisodeTemplet.m_EPCategory;
			}
			else if (NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeCategory() == EPISODE_CATEGORY.EC_SUPPLY || NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeCategory() == EPISODE_CATEGORY.EC_CHALLENGE)
			{
				this.m_CurCategory = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeCategory();
			}
			else
			{
				this.m_CurCategory = EPISODE_CATEGORY.EC_SUPPLY;
			}
			EPISODE_CATEGORY curCategory = this.m_CurCategory;
			if (curCategory != EPISODE_CATEGORY.EC_SUPPLY)
			{
				if (curCategory == EPISODE_CATEGORY.EC_CHALLENGE)
				{
					this.OnChallenge(true);
				}
			}
			else
			{
				this.OnSupply(true);
			}
			this.m_tglSupply.Select(this.m_CurCategory == EPISODE_CATEGORY.EC_SUPPLY, true, true);
			this.m_tglChallenge.Select(this.m_CurCategory == EPISODE_CATEGORY.EC_CHALLENGE, true, true);
			if (reservedEpisodeTemplet != null)
			{
				NKCUIOperationNodeViewer.Instance.Open(reservedEpisodeTemplet);
			}
		}

		// Token: 0x0600700E RID: 28686 RVA: 0x00251CD0 File Offset: 0x0024FED0
		private void SetData()
		{
			this.m_loop.content.GetComponent<RectTransform>().pivot = Vector2.zero;
			this.m_loop.TotalCount = this.m_dicData[this.m_CurCategory].Count;
			this.m_loop.RefreshCells(false);
			this.m_loop.horizontalScrollbarVisibility = LoopScrollRect.ScrollbarVisibility.AutoHide;
			if (NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedStageTemplet() == null)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeCategory(this.m_CurCategory);
			}
			this.TutorialCheck_Tab();
		}

		// Token: 0x0600700F RID: 28687 RVA: 0x00251D5C File Offset: 0x0024FF5C
		private void BuildTemplets()
		{
			List<NKMEpisodeGroupTemplet> list = new List<NKMEpisodeGroupTemplet>();
			foreach (NKMEpisodeGroupTemplet nkmepisodeGroupTemplet in NKMTempletContainer<NKMEpisodeGroupTemplet>.Values)
			{
				if (nkmepisodeGroupTemplet.GroupCategory == EPISODE_GROUP.EG_GROWTH)
				{
					list.Add(nkmepisodeGroupTemplet);
				}
			}
			list.Sort(new Comparison<NKMEpisodeGroupTemplet>(this.CompByKey));
			this.m_dicData.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].lstEpisodeTemplet.Count; j++)
				{
					NKMEpisodeTempletV2 nkmepisodeTempletV = list[i].lstEpisodeTemplet[j];
					if (nkmepisodeTempletV != null && nkmepisodeTempletV.IsOpen && nkmepisodeTempletV.IsOpenedDayOfWeek() && nkmepisodeTempletV.m_Difficulty == EPISODE_DIFFICULTY.NORMAL)
					{
						if (!this.m_dicData.ContainsKey(nkmepisodeTempletV.m_EPCategory))
						{
							this.m_dicData.Add(nkmepisodeTempletV.m_EPCategory, new List<NKMEpisodeTempletV2>());
						}
						this.m_dicData[nkmepisodeTempletV.m_EPCategory].Add(list[i].lstEpisodeTemplet[j]);
					}
				}
			}
		}

		// Token: 0x06007010 RID: 28688 RVA: 0x00251E9C File Offset: 0x0025009C
		public void OnClickSlot(int episodeID)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null)
			{
				NKCUIOperationNodeViewer.Instance.Open(nkmepisodeTempletV);
			}
		}

		// Token: 0x06007011 RID: 28689 RVA: 0x00251EBF File Offset: 0x002500BF
		private void OnSupply(bool bValue)
		{
			if (bValue)
			{
				if (this.m_tglSupply.m_bLock)
				{
					return;
				}
				this.m_CurCategory = EPISODE_CATEGORY.EC_SUPPLY;
				this.SetData();
			}
		}

		// Token: 0x06007012 RID: 28690 RVA: 0x00251EDF File Offset: 0x002500DF
		private void OnChallenge(bool bValue)
		{
			if (bValue)
			{
				if (this.m_tglChallenge.m_bLock)
				{
					return;
				}
				this.m_CurCategory = EPISODE_CATEGORY.EC_CHALLENGE;
				this.SetData();
			}
		}

		// Token: 0x06007013 RID: 28691 RVA: 0x00251EFF File Offset: 0x002500FF
		private void TutorialCheck_Tab()
		{
			if (this.m_CurCategory == EPISODE_CATEGORY.EC_SUPPLY)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_Growth_Supply, true);
			}
			else if (this.m_CurCategory == EPISODE_CATEGORY.EC_CHALLENGE)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_Growth_Challenge, true);
			}
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_Growth, true);
		}

		// Token: 0x04005BA8 RID: 23464
		public NKCUIComToggle m_tglSupply;

		// Token: 0x04005BA9 RID: 23465
		public NKCUIComToggle m_tglChallenge;

		// Token: 0x04005BAA RID: 23466
		[Header("슬롯")]
		public NKCUIOperationSubGrowthEPSlot m_pfbSlot;

		// Token: 0x04005BAB RID: 23467
		public LoopHorizontalScrollRect m_loop;

		// Token: 0x04005BAC RID: 23468
		private Dictionary<EPISODE_CATEGORY, List<NKMEpisodeTempletV2>> m_dicData = new Dictionary<EPISODE_CATEGORY, List<NKMEpisodeTempletV2>>();

		// Token: 0x04005BAD RID: 23469
		private Stack<NKCUIOperationSubGrowthEPSlot> m_stkSlot = new Stack<NKCUIOperationSubGrowthEPSlot>();

		// Token: 0x04005BAE RID: 23470
		private EPISODE_CATEGORY m_CurCategory = EPISODE_CATEGORY.EC_SUPPLY;
	}
}

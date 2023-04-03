using System;
using System.Collections.Generic;
using DG.Tweening;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A0A RID: 2570
	public class NKCUIOperationSubStory : MonoBehaviour
	{
		// Token: 0x06007033 RID: 28723 RVA: 0x00252DC8 File Offset: 0x00250FC8
		public void InitUI()
		{
			this.m_btnCounterCase.PointerClick.RemoveAllListeners();
			this.m_btnCounterCase.PointerClick.AddListener(new UnityAction(this.OnClickCounterCase));
			this.m_btnSupplement.PointerClick.RemoveAllListeners();
			this.m_btnSupplement.PointerClick.AddListener(delegate()
			{
				this.OpenList(true);
			});
			this.m_btnSideStory.PointerClick.RemoveAllListeners();
			this.m_btnSideStory.PointerClick.AddListener(delegate()
			{
				this.OpenList(false);
			});
			this.m_EpViewer.InitUI();
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x00252E64 File Offset: 0x00251064
		public void Open()
		{
			this.SetProgressData();
			this.m_EpViewer.SetData();
			NKMEpisodeTempletV2 reservedEpisodeTemplet = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet();
			if (reservedEpisodeTemplet != null)
			{
				if (reservedEpisodeTemplet.m_EpisodeID == 50)
				{
					this.OnClickCounterCase();
				}
				else
				{
					NKCUIOperationNodeViewer.Instance.Open(reservedEpisodeTemplet);
				}
			}
			int lastPlayedSubStream = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetLastPlayedSubStream();
			if (lastPlayedSubStream != 0 && this.m_objLastEpisode != null && this.m_objFirstEpisode != null)
			{
				this.ScrollToEpisode(lastPlayedSubStream, false);
			}
			this.TutorialCheck();
		}

		// Token: 0x06007035 RID: 28725 RVA: 0x00252EF0 File Offset: 0x002510F0
		private void ScrollToEpisode(int targetEpisodeID, bool bShowFx)
		{
			float num = this.m_objLastEpisode.transform.localPosition.x - this.m_objFirstEpisode.transform.localPosition.x;
			float num2 = 0f;
			List<NKCUIOperationSubStorySlot> epList = this.m_EpViewer.GetEpList();
			if (epList != null)
			{
				int i = 0;
				while (i < epList.Count)
				{
					if (!(epList[i] == null) && epList[i].GetEpisodeID() == targetEpisodeID)
					{
						num2 = epList[i].transform.localPosition.x - this.m_objFirstEpisode.transform.localPosition.x;
						if (bShowFx)
						{
							epList[i].ShowFocusFx();
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				this.m_sr.DOKill(false);
				this.m_sr.DOHorizontalNormalizedPos(num2 / num, 0.1f, false);
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetLastPlayedSubStream(0);
			}
		}

		// Token: 0x06007036 RID: 28726 RVA: 0x00252FE0 File Offset: 0x002511E0
		private void SetProgressData()
		{
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(EPISODE_CATEGORY.EC_SIDESTORY, false, EPISODE_DIFFICULTY.NORMAL);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < listNKMEpisodeTempletByCategory.Count; i++)
			{
				bool flag = NKMEpisodeMgr.IsClearedEpisode(listNKMEpisodeTempletByCategory[i]);
				if (listNKMEpisodeTempletByCategory[i].m_bIsSupplement)
				{
					num4++;
					if (flag)
					{
						num3++;
					}
				}
				else
				{
					num2++;
					if (flag)
					{
						num++;
					}
				}
			}
			NKCUtil.SetLabelText(this.m_lbSideProgress, string.Format("{0}/{1}", num, num2));
			NKCUtil.SetLabelText(this.m_lbSplProgress, string.Format("{0}/{1}", num3, num4));
			NKCUtil.SetLabelText(this.m_lbTotalProgress, string.Format("{0}/{1}", num + num3, num2 + num4));
		}

		// Token: 0x06007037 RID: 28727 RVA: 0x002530B8 File Offset: 0x002512B8
		private void OnClickCounterCase()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.COUNTERCASE, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.COUNTERCASE, 0);
				return;
			}
			NKMEpisodeTempletV2 reservedEpisodeTemplet = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet();
			if (reservedEpisodeTemplet != null && reservedEpisodeTemplet.m_EpisodeID == 50)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OpenCounterCaseViewer();
				return;
			}
			NKCUIFadeInOut.FadeOut(this.m_fFadeTime, delegate
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OpenCounterCaseViewer();
			}, false, -1f);
		}

		// Token: 0x06007038 RID: 28728 RVA: 0x00253137 File Offset: 0x00251337
		private void OpenList(bool bSupplement)
		{
			NKCPopupOperationSubStoryList.Instance.Open(new NKCPopupOperationSubStoryList.OnSelectedSlot(this.OnClickListSlot), bSupplement);
		}

		// Token: 0x06007039 RID: 28729 RVA: 0x00253150 File Offset: 0x00251350
		private void OnClickListSlot(int episodeID)
		{
			this.ScrollToEpisode(episodeID, true);
		}

		// Token: 0x0600703A RID: 28730 RVA: 0x0025315A File Offset: 0x0025135A
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_SubStream, true);
		}

		// Token: 0x04005BE6 RID: 23526
		public NKCUIComStateButton m_btnCounterCase;

		// Token: 0x04005BE7 RID: 23527
		public Text m_lbTotalProgress;

		// Token: 0x04005BE8 RID: 23528
		public Text m_lbSideProgress;

		// Token: 0x04005BE9 RID: 23529
		public Text m_lbSplProgress;

		// Token: 0x04005BEA RID: 23530
		public NKCUIComStateButton m_btnSupplement;

		// Token: 0x04005BEB RID: 23531
		public NKCUIComStateButton m_btnSideStory;

		// Token: 0x04005BEC RID: 23532
		public ScrollRect m_sr;

		// Token: 0x04005BED RID: 23533
		public GameObject m_objFirstEpisode;

		// Token: 0x04005BEE RID: 23534
		public GameObject m_objLastEpisode;

		// Token: 0x04005BEF RID: 23535
		public NKCUIOperationSubStoryEpViewer m_EpViewer;

		// Token: 0x04005BF0 RID: 23536
		public float m_fFadeTime = 0.3f;
	}
}

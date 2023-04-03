using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A04 RID: 2564
	public class NKCUIOperationSubChallenge : MonoBehaviour
	{
		// Token: 0x06006FFD RID: 28669 RVA: 0x00251388 File Offset: 0x0024F588
		public void Open()
		{
			NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMTempletContainer<NKMEpisodeGroupTemplet>.Find((NKMEpisodeGroupTemplet x) => x.EpCategory == EPISODE_CATEGORY.EC_DAILY);
			if (nkmepisodeGroupTemplet != null && nkmepisodeGroupTemplet.lstEpisodeTemplet.Count == this.m_lstDailySlot.Count)
			{
				for (int i = 0; i < nkmepisodeGroupTemplet.lstEpisodeTemplet.Count; i++)
				{
					this.m_lstDailySlot[i].SetData(nkmepisodeGroupTemplet.lstEpisodeTemplet[i], new NKCUIOperationSubChallengeSlot.OnClickSlot(this.OnClickDailySlot));
				}
			}
			NKMEpisodeGroupTemplet groupTemplet = NKMTempletContainer<NKMEpisodeGroupTemplet>.Find((NKMEpisodeGroupTemplet x) => x.EpCategory == EPISODE_CATEGORY.EC_SHADOW);
			this.m_slotShadow.SetData(groupTemplet, EPISODE_CATEGORY.EC_SHADOW, new NKCUIOperationSubChallengeSlot.OnClickSlot(this.OnClickShadow));
			NKMEpisodeGroupTemplet groupTemplet2 = NKMTempletContainer<NKMEpisodeGroupTemplet>.Find((NKMEpisodeGroupTemplet x) => x.EpCategory == EPISODE_CATEGORY.EC_TRIM);
			this.m_slotDimension.SetData(groupTemplet2, EPISODE_CATEGORY.EC_TRIM, new NKCUIOperationSubChallengeSlot.OnClickSlot(this.OnClickDimension));
			NKMEpisodeGroupTemplet groupTemplet3 = NKMTempletContainer<NKMEpisodeGroupTemplet>.Find((NKMEpisodeGroupTemplet x) => x.EpCategory == EPISODE_CATEGORY.EC_TIMEATTACK);
			this.m_slotTimeAttack.SetData(groupTemplet3, EPISODE_CATEGORY.EC_TIMEATTACK, new NKCUIOperationSubChallengeSlot.OnClickSlot(this.OnClickTimeAttack));
			NKMEpisodeTempletV2 reservedEpisodeTemplet = NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet();
			if (reservedEpisodeTemplet != null)
			{
				NKCUIOperationNodeViewer.Instance.Open(reservedEpisodeTemplet);
			}
			this.TutorialCheck();
		}

		// Token: 0x06006FFE RID: 28670 RVA: 0x00251500 File Offset: 0x0024F700
		private void OnClickDailySlot(int key)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(key, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null)
			{
				NKCUIOperationNodeViewer.Instance.Open(nkmepisodeTempletV);
			}
		}

		// Token: 0x06006FFF RID: 28671 RVA: 0x00251523 File Offset: 0x0024F723
		private void OnClickShadow(int key)
		{
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_SHADOW_PALACE, "", false);
		}

		// Token: 0x06007000 RID: 28672 RVA: 0x00251532 File Offset: 0x0024F732
		private void OnClickDimension(int key)
		{
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_TRIM, "", false);
		}

		// Token: 0x06007001 RID: 28673 RVA: 0x00251544 File Offset: 0x0024F744
		private void OnClickTimeAttack(int key)
		{
			NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(key);
			if (nkmepisodeGroupTemplet != null && nkmepisodeGroupTemplet.lstEpisodeTemplet.Count > 0)
			{
				NKCUIOperationNodeViewer.Instance.Open(nkmepisodeGroupTemplet.lstEpisodeTemplet[0]);
			}
		}

		// Token: 0x06007002 RID: 28674 RVA: 0x0025157F File Offset: 0x0024F77F
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_Challenge, true);
		}

		// Token: 0x04005B97 RID: 23447
		[Header("모의작전")]
		public List<NKCUIOperationSubChallengeSlot> m_lstDailySlot = new List<NKCUIOperationSubChallengeSlot>();

		// Token: 0x04005B98 RID: 23448
		[Header("디멘션 트리밍")]
		public NKCUIOperationSubChallengeSlot m_slotShadow;

		// Token: 0x04005B99 RID: 23449
		[Header("디멘션 트리밍")]
		public NKCUIOperationSubChallengeSlot m_slotDimension;

		// Token: 0x04005B9A RID: 23450
		[Header("타임어택")]
		public NKCUIOperationSubChallengeSlot m_slotTimeAttack;
	}
}

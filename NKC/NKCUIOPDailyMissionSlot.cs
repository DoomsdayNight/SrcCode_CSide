using System;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007CF RID: 1999
	public class NKCUIOPDailyMissionSlot : MonoBehaviour
	{
		// Token: 0x06004EED RID: 20205 RVA: 0x0017D1A6 File Offset: 0x0017B3A6
		public void Init()
		{
			NKCUIComButton btn = this.m_btn;
			if (btn != null)
			{
				btn.PointerClick.RemoveAllListeners();
			}
			NKCUIComButton btn2 = this.m_btn;
			if (btn2 == null)
			{
				return;
			}
			btn2.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0017D1E0 File Offset: 0x0017B3E0
		public void SetData(NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE dailyMissionType, NKCUIOPDailyMissionSlot.OnClickDM onClickDM)
		{
			this.m_dOnClickDM = onClickDM;
			this.m_dailyMissionType = dailyMissionType;
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(NKMEpisodeMgr.GetDailyMissionEPId(dailyMissionType), EPISODE_DIFFICULTY.NORMAL);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			ContentsType contentsType = NKCContentManager.GetContentsType(dailyMissionType);
			bool flag = NKMEpisodeMgr.IsPossibleEpisode(myUserData, nkmepisodeTempletV);
			bool flag2 = !NKCContentManager.IsContentsUnlocked(contentsType, 0, 0);
			bool flag3 = nkmepisodeTempletV != null && nkmepisodeTempletV.IsOpenedDayOfWeek();
			NKCUtil.SetGameobjectActive(this.m_objLock, !flag || flag2 || !flag3);
			NKCUtil.SetGameobjectActive(this.m_lbLock, flag2);
			NKCUtil.SetLabelText(this.m_lbLock, NKCContentManager.GetLockedMessage(contentsType, 0));
			int dailyMissionTicketID = NKMEpisodeMgr.GetDailyMissionTicketID(dailyMissionType);
			NKCUtil.SetImageSprite(this.m_imgTicket, NKCResourceUtility.GetOrLoadMiscItemIcon(dailyMissionTicketID), false);
			this.SetTicketCount();
			NKCUtil.SetGameobjectActive(this.m_objEventBadge, nkmepisodeTempletV != null && nkmepisodeTempletV.HaveEventDrop);
			base.gameObject.SetActive(flag3);
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0017D2B8 File Offset: 0x0017B4B8
		public void SetTicketCount()
		{
			int dailyMissionTicketID = NKMEpisodeMgr.GetDailyMissionTicketID(this.m_dailyMissionType);
			long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(dailyMissionTicketID);
			NKCUtil.SetLabelText(this.m_lbRemainTicketCount, countMiscItem.ToString());
			if (countMiscItem <= 0L)
			{
				NKCUtil.SetLabelTextColor(this.m_lbRemainTicketCount, Color.red);
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_lbRemainTicketCount, Color.white);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x0017D31A File Offset: 0x0017B51A
		public void OnClickBtn()
		{
			NKCUIOPDailyMissionSlot.OnClickDM dOnClickDM = this.m_dOnClickDM;
			if (dOnClickDM == null)
			{
				return;
			}
			dOnClickDM(this.m_dailyMissionType);
		}

		// Token: 0x04003ECF RID: 16079
		public GameObject m_objLock;

		// Token: 0x04003ED0 RID: 16080
		public Text m_lbLock;

		// Token: 0x04003ED1 RID: 16081
		public NKCUIComButton m_btn;

		// Token: 0x04003ED2 RID: 16082
		public Image m_imgTicket;

		// Token: 0x04003ED3 RID: 16083
		public Text m_lbRemainTicketCount;

		// Token: 0x04003ED4 RID: 16084
		public GameObject m_objEventBadge;

		// Token: 0x04003ED5 RID: 16085
		private NKCUIOPDailyMissionSlot.OnClickDM m_dOnClickDM;

		// Token: 0x04003ED6 RID: 16086
		private NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE m_dailyMissionType;

		// Token: 0x0200148B RID: 5259
		// (Invoke) Token: 0x0600A92A RID: 43306
		public delegate void OnClickDM(NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE dailyMissionType);
	}
}

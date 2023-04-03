using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using NKM;

namespace NKC
{
	// Token: 0x020006D3 RID: 1747
	public class NKCSurveyMgr
	{
		// Token: 0x06003D03 RID: 15619 RVA: 0x0013A3BE File Offset: 0x001385BE
		public void Clear()
		{
			this.m_lstSurveyInfo.Clear();
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x0013A3CC File Offset: 0x001385CC
		public void UpdaterOrAdd(SurveyInfo si)
		{
			for (int i = 0; i < this.m_lstSurveyInfo.Count; i++)
			{
				SurveyInfo surveyInfo = this.m_lstSurveyInfo[i];
				if (surveyInfo.surveyId == si.surveyId)
				{
					surveyInfo.userLevel = si.userLevel;
					surveyInfo.startDate = si.startDate;
					surveyInfo.endDate = si.endDate;
					return;
				}
			}
			this.m_lstSurveyInfo.Add(si);
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x0013A43C File Offset: 0x0013863C
		public void Delete(long id)
		{
			for (int i = 0; i < this.m_lstSurveyInfo.Count; i++)
			{
				if (this.m_lstSurveyInfo[i].surveyId == id)
				{
					this.m_lstSurveyInfo.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x0013A480 File Offset: 0x00138680
		public bool CheckAvailableSurvey()
		{
			for (int i = 0; i < this.m_lstSurveyInfo.Count; i++)
			{
				if (this.CheckAvailableSurvey(this.m_lstSurveyInfo[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x0013A4BC File Offset: 0x001386BC
		public long GetCurrSurveyID()
		{
			if (this.m_lstSurveyInfo == null || this.m_lstSurveyInfo.Count <= 0)
			{
				return -1L;
			}
			this.m_lstSurveyInfo = (from x in this.m_lstSurveyInfo
			orderby x.surveyId
			select x).ToList<SurveyInfo>();
			return this.m_lstSurveyInfo[0].surveyId;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x0013A528 File Offset: 0x00138728
		private bool CheckAvailableSurvey(SurveyInfo si)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.UserLevel >= si.userLevel && NKCSynchronizedTime.IsEventTime(si.startDate, si.endDate);
		}

		// Token: 0x04003627 RID: 13863
		private List<SurveyInfo> m_lstSurveyInfo = new List<SurveyInfo>();
	}
}

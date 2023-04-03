using System;
using NKC.Publisher;
using NKM;
using UnityEngine.Events;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C1E RID: 3102
	public class NKCUISurvey : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F94 RID: 36756 RVA: 0x0030CE3C File Offset: 0x0030B03C
		private void OnClickBtn()
		{
			long currSurveyID = NKCScenManager.GetScenManager().GetNKCSurveyMgr().GetCurrSurveyID();
			if (currSurveyID <= 0L)
			{
				return;
			}
			NKCPublisherModule.Notice.OpenSurvey(currSurveyID, new NKCPublisherModule.OnComplete(this.OnCompleteSurvey));
		}

		// Token: 0x06008F95 RID: 36757 RVA: 0x0030CE76 File Offset: 0x0030B076
		private void OnCompleteSurvey(NKC_PUBLISHER_RESULT_CODE eNKC_PUBLISHER_RESULT_CODE, string additionalError)
		{
			NKCPublisherModule.CheckError(eNKC_PUBLISHER_RESULT_CODE, additionalError, false, null, false);
		}

		// Token: 0x06008F96 RID: 36758 RVA: 0x0030CE83 File Offset: 0x0030B083
		public void Init()
		{
			if (this.m_csbtnSurvey != null)
			{
				this.m_csbtnSurvey.PointerClick.RemoveAllListeners();
				this.m_csbtnSurvey.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
			}
		}

		// Token: 0x06008F97 RID: 36759 RVA: 0x0030CEBF File Offset: 0x0030B0BF
		protected override void ContentsUpdate(NKMUserData userData)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, NKCScenManager.GetScenManager().GetNKCSurveyMgr().CheckAvailableSurvey());
		}

		// Token: 0x04007C8A RID: 31882
		private NKCUIComStateButton m_csbtnSurvey;
	}
}

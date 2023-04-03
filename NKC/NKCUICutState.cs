using System;
using System.Collections.Generic;

namespace NKC.UI
{
	// Token: 0x0200097C RID: 2428
	public class NKCUICutState
	{
		// Token: 0x060062AC RID: 25260 RVA: 0x001EFBC0 File Offset: 0x001EDDC0
		public void InitPerCut()
		{
			this.m_bTitle = false;
			this.m_bWaitClick = false;
			this.m_bFading = false;
			this.m_fWaitTime = 0f;
			this.m_fAddWaitTimeForAuto = 0f;
			this.m_fElapsedTimeWithoutAutoCalc = 0f;
			this.m_bTalk = false;
			this.m_EndBGMFileName = "";
			this.m_EndFXSoundControl = NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY;
			this.m_EndFXSoundName = "";
			if (this.m_VoiceUID >= 0)
			{
				NKCSoundManager.StopSound(this.m_VoiceUID);
			}
			this.m_VoiceUID = -1;
			this.m_bPlayVideo = false;
			this.m_bWaitSelection = false;
			this.m_bMovieSkipEnable = true;
		}

		// Token: 0x04004E77 RID: 20087
		public bool m_bTitle;

		// Token: 0x04004E78 RID: 20088
		public bool m_bFading;

		// Token: 0x04004E79 RID: 20089
		public bool m_bWaitClick;

		// Token: 0x04004E7A RID: 20090
		public float m_fWaitTime;

		// Token: 0x04004E7B RID: 20091
		public float m_fAddWaitTimeForAuto;

		// Token: 0x04004E7C RID: 20092
		public float m_fElapsedTimeWithoutAutoCalc;

		// Token: 0x04004E7D RID: 20093
		public bool m_bTalk;

		// Token: 0x04004E7E RID: 20094
		public string m_EndBGMFileName = "";

		// Token: 0x04004E7F RID: 20095
		public NKC_CUTSCEN_SOUND_CONTROL m_EndFXSoundControl = NKC_CUTSCEN_SOUND_CONTROL.NCSC_ONE_TIME_PLAY;

		// Token: 0x04004E80 RID: 20096
		public string m_EndFXSoundName = "";

		// Token: 0x04004E81 RID: 20097
		public int m_VoiceUID = -1;

		// Token: 0x04004E82 RID: 20098
		public bool m_bPlayVideo;

		// Token: 0x04004E83 RID: 20099
		public bool m_bWaitSelection;

		// Token: 0x04004E84 RID: 20100
		public List<Tuple<string, string>> m_lstSelectionMark = new List<Tuple<string, string>>();

		// Token: 0x04004E85 RID: 20101
		public bool m_bMovieSkipEnable = true;
	}
}

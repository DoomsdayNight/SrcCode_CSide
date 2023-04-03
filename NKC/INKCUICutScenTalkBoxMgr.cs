using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000807 RID: 2055
	public interface INKCUICutScenTalkBoxMgr
	{
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06005180 RID: 20864
		GameObject MyGameObject { get; }

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06005181 RID: 20865
		bool IsFinished { get; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06005182 RID: 20866
		NKCUICutScenTalkBoxMgr.TalkBoxType MyBoxType { get; }

		// Token: 0x06005183 RID: 20867
		void SetPause(bool bPause);

		// Token: 0x06005184 RID: 20868
		void ResetTalkBox();

		// Token: 0x06005185 RID: 20869
		void Finish();

		// Token: 0x06005186 RID: 20870
		void Open(string _TalkerName, string _Talk, float fCoolTime, bool bWaitClick, bool _bTalkAppend);

		// Token: 0x06005187 RID: 20871
		void StartFadeIn(float fadeTime);

		// Token: 0x06005188 RID: 20872
		void FadeOutBooking(float fadeTime);

		// Token: 0x06005189 RID: 20873
		void ClearTalk();

		// Token: 0x0600518A RID: 20874
		void Close();

		// Token: 0x0600518B RID: 20875
		void OnChange();

		// Token: 0x0600518C RID: 20876
		bool UsingTMPText();
	}
}

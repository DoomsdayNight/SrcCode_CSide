using System;
using UnityEngine;

namespace NKC.UI.HUD
{
	// Token: 0x02000C42 RID: 3138
	public class NKCGameHudObjects : MonoBehaviour
	{
		// Token: 0x04007F5F RID: 32607
		[Header("SCEN_GAME")]
		public GameObject m_NUF_BEFORE_HUD_EFFECT;

		// Token: 0x04007F60 RID: 32608
		public GameObject m_NUF_BEFORE_HUD_CONTROL_EFFECT_ANCHOR;

		// Token: 0x04007F61 RID: 32609
		public GameObject m_NUF_BEFORE_HUD_CONTROL_EFFECT;

		// Token: 0x04007F62 RID: 32610
		public GameObject m_NUF_AFTER_HUD_EFFECT;

		// Token: 0x04007F63 RID: 32611
		[Header("for HUD")]
		public GameObject m_NUF_GAME_HUD_UI_EMOTICON;

		// Token: 0x04007F64 RID: 32612
		public GameObject m_NUF_GAME_HUD_UI_PAUSE;

		// Token: 0x04007F65 RID: 32613
		[Header("GameHUD")]
		public NKCGameHud m_GameHud;
	}
}

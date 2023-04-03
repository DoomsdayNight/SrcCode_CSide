using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.Game
{
	// Token: 0x020008A3 RID: 2211
	public class NKCGameDebugAttackBox : MonoBehaviour
	{
		// Token: 0x040045C3 RID: 17859
		public Transform m_trAttackBox;

		// Token: 0x040045C4 RID: 17860
		public SpriteRenderer m_srAttackBox;

		// Token: 0x040045C5 RID: 17861
		public Text m_lbAttackData;

		// Token: 0x040045C6 RID: 17862
		private bool m_bShowText = true;

		// Token: 0x040045C7 RID: 17863
		public float m_fYOffset = 50f;

		// Token: 0x040045C8 RID: 17864
		private Color m_colUnit = new Color(1f, 0.35f, 0f);

		// Token: 0x040045C9 RID: 17865
		private Color m_colDE = Color.cyan;
	}
}

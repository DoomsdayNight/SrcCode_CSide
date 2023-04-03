using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000797 RID: 1943
	public class NKCUIFierceBattleNoticeSlot : MonoBehaviour
	{
		// Token: 0x06004C52 RID: 19538 RVA: 0x0016D990 File Offset: 0x0016BB90
		public void SetData(string bossFaceCardName, string bossName)
		{
			if (!string.IsNullOrEmpty(bossFaceCardName))
			{
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_unit_face_card", bossFaceCardName, false);
				NKCUtil.SetImageSprite(this.m_BossImage, orLoadAssetResource, false);
			}
			NKCUtil.SetLabelText(this.m_BossName, bossName);
		}

		// Token: 0x04003C0D RID: 15373
		public Image m_BossImage;

		// Token: 0x04003C0E RID: 15374
		public Text m_BossName;
	}
}

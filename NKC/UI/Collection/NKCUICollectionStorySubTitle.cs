using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C2B RID: 3115
	public class NKCUICollectionStorySubTitle : MonoBehaviour
	{
		// Token: 0x06009070 RID: 36976 RVA: 0x00313163 File Offset: 0x00311363
		public void Init()
		{
		}

		// Token: 0x06009071 RID: 36977 RVA: 0x00313165 File Offset: 0x00311365
		public void SetTitle(string str)
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_SLOT_ACT_TEXT, str);
		}

		// Token: 0x04007D95 RID: 32149
		public Text m_NKM_UI_COLLECTION_STORY_SLOT_ACT_TEXT;
	}
}

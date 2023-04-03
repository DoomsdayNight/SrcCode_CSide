using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000628 RID: 1576
	public class NKCGameHudEmoticonComment : MonoBehaviour
	{
		// Token: 0x060030AD RID: 12461 RVA: 0x000F1262 File Offset: 0x000EF462
		public void Start()
		{
			NKCUtil.SetBindFunction(this.m_csbtnComment, delegate()
			{
				this.PlayPreview(this.m_LastEmoticonID);
			});
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000F127B File Offset: 0x000EF47B
		public void SetEnableBtn(bool bSet)
		{
			if (this.m_csbtnComment != null)
			{
				this.m_csbtnComment.enabled = bSet;
			}
			if (this.m_crtComment != null)
			{
				this.m_crtComment.enabled = bSet;
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000F12B4 File Offset: 0x000EF4B4
		public void PlayPreview(int emoticonID)
		{
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(emoticonID);
			if (nkmemoticonTemplet == null)
			{
				return;
			}
			this.m_LastEmoticonID = nkmemoticonTemplet.m_EmoticonID;
			NKCUtil.SetLabelText(this.m_lbComment, nkmemoticonTemplet.GetEmoticonName());
			this.m_amtorComment.Play("FX_UI_HUD_EMOTICON_COMMENT_STOP_" + nkmemoticonTemplet.m_EmoticonaAnimationName, -1, 0f);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000F130C File Offset: 0x000EF50C
		public void Play(int emoticonID)
		{
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(emoticonID);
			if (nkmemoticonTemplet == null)
			{
				return;
			}
			this.m_LastEmoticonID = nkmemoticonTemplet.m_EmoticonID;
			NKCUtil.SetLabelText(this.m_lbComment, nkmemoticonTemplet.GetEmoticonName());
			this.m_amtorComment.Play("FX_UI_HUD_EMOTICON_COMMENT_BASE_" + nkmemoticonTemplet.m_EmoticonaAnimationName, -1, 0f);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000F1362 File Offset: 0x000EF562
		public void Stop()
		{
			this.m_amtorComment.Play("FX_UI_HUD_EMOTICON_COMMENT_IDLE", -1, 0f);
		}

		// Token: 0x04003022 RID: 12322
		public Text m_lbComment;

		// Token: 0x04003023 RID: 12323
		public Animator m_amtorComment;

		// Token: 0x04003024 RID: 12324
		public NKCUIComStateButton m_csbtnComment;

		// Token: 0x04003025 RID: 12325
		public NKCUIComRaycastTarget m_crtComment;

		// Token: 0x04003026 RID: 12326
		private int m_LastEmoticonID = -1;
	}
}

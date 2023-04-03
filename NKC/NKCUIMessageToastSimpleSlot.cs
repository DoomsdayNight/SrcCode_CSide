using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A2A RID: 2602
	public class NKCUIMessageToastSimpleSlot : MonoBehaviour
	{
		// Token: 0x060071F2 RID: 29170 RVA: 0x0025E420 File Offset: 0x0025C620
		public void SetData(NKCPopupMessageToastSimple.MessageData msgData)
		{
			this.m_fIdleTimer = 0f;
			NKCUtil.SetImageSprite(this.m_imgIcon, msgData.sprite, false);
			NKCUtil.SetLabelText(this.m_lbName, msgData.name);
			NKCUtil.SetLabelText(this.m_lbCount, msgData.count.ToString());
			if (string.IsNullOrEmpty(msgData.name))
			{
				return;
			}
			float num = this.m_lbCount.preferredWidth + this.m_rectIcon.GetWidth() + this.m_rectTextX.GetWidth();
			float num2 = this.m_rectLayoutGroup.GetWidth() - num;
			if (this.m_lbName.preferredWidth > num2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int length = msgData.name.Length;
				for (int i = 0; i < length; i++)
				{
					stringBuilder.Append(msgData.name[i]);
					NKCUtil.SetLabelText(this.m_lbName, stringBuilder.ToString());
					if (this.m_lbName.preferredWidth > num2)
					{
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
						stringBuilder.Append("...");
						NKCUtil.SetLabelText(this.m_lbName, stringBuilder.ToString());
						return;
					}
				}
			}
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x0025E546 File Offset: 0x0025C746
		public void ResetSlot()
		{
			NKCUtil.SetImageSprite(this.m_imgIcon, null, false);
			NKCUtil.SetLabelText(this.m_lbName, "");
			NKCUtil.SetLabelText(this.m_lbCount, "");
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x0025E575 File Offset: 0x0025C775
		public void PlayIdleAni()
		{
			this.m_fIdleTimer = this.m_fStayTime;
			this.m_animator.Play("NKM_UI_POPUP_MESSAGE_TOAST_IDLE");
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x0025E594 File Offset: 0x0025C794
		private void Update()
		{
			if (this.m_animator.GetCurrentAnimatorStateInfo(0).IsName("NKM_UI_POPUP_MESSAGE_TOAST_IDLE"))
			{
				this.m_fIdleTimer += Time.deltaTime;
				if (this.m_fIdleTimer >= this.m_fStayTime && base.transform.GetSiblingIndex() == 0)
				{
					this.m_animator.SetTrigger("OUT");
				}
			}
		}

		// Token: 0x04005DE5 RID: 24037
		public float m_fStayTime;

		// Token: 0x04005DE6 RID: 24038
		public Animator m_animator;

		// Token: 0x04005DE7 RID: 24039
		public Image m_imgIcon;

		// Token: 0x04005DE8 RID: 24040
		public Text m_lbName;

		// Token: 0x04005DE9 RID: 24041
		public Text m_lbCount;

		// Token: 0x04005DEA RID: 24042
		public RectTransform m_rectIcon;

		// Token: 0x04005DEB RID: 24043
		public RectTransform m_rectTextX;

		// Token: 0x04005DEC RID: 24044
		public RectTransform m_rectLayoutGroup;

		// Token: 0x04005DED RID: 24045
		private float m_fIdleTimer;
	}
}

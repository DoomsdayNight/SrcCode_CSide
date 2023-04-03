using System;
using System.Text;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D1 RID: 2513
	public class NKCUIPrepareEventDeckConditionSlot : MonoBehaviour
	{
		// Token: 0x06006B89 RID: 27529 RVA: 0x00230C30 File Offset: 0x0022EE30
		public void SetCondition(NKMDeckCondition.SingleCondition condition)
		{
			if (condition.IsProhibited())
			{
				NKCUtil.SetImageSprite(this.m_imgMark, this.m_spForbidden, false);
				NKCUtil.SetLabelTextColor(this.m_lbCondition, this.m_colForbidden);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgMark, this.m_spNormal, false);
				NKCUtil.SetLabelTextColor(this.m_lbCondition, this.m_colNormal);
			}
			NKCUtil.SetLabelText(this.m_lbCondition, NKCUtilString.GetDeckConditionString(condition));
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x00230CA0 File Offset: 0x0022EEA0
		public void SetCondition(NKMDeckCondition.SingleCondition condition, int teamTotalCount)
		{
			bool flag = condition.IsProhibited();
			if (flag)
			{
				NKCUtil.SetImageSprite(this.m_imgMark, this.m_spForbidden, false);
				NKCUtil.SetLabelTextColor(this.m_lbCondition, this.m_colForbidden);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgMark, this.m_spNormal, false);
				NKCUtil.SetLabelTextColor(this.m_lbCondition, this.m_colNormal);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(NKCUtilString.GetDeckConditionString(condition));
			if (!flag)
			{
				stringBuilder.Append(" ");
				if (!condition.IsValueOk(teamTotalCount))
				{
					stringBuilder.AppendFormat("(<color=#ff0000>{0}</color>/{1})", teamTotalCount, condition.Value);
				}
				else
				{
					stringBuilder.AppendFormat("({0}/{1})", teamTotalCount, condition.Value);
				}
			}
			NKCUtil.SetLabelText(this.m_lbCondition, stringBuilder.ToString());
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x00230D78 File Offset: 0x0022EF78
		public void SetCondition(NKMDeckCondition.GameCondition condition)
		{
			NKCUtil.SetImageSprite(this.m_imgMark, this.m_spNormal, false);
			if (condition.IsPenalty())
			{
				NKCUtil.SetLabelTextColor(this.m_lbCondition, this.m_colForbidden);
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_lbCondition, this.m_colNormal);
			}
			NKCUtil.SetLabelText(this.m_lbCondition, NKCUtilString.GetGameConditionString(condition));
		}

		// Token: 0x04005729 RID: 22313
		public Image m_imgMark;

		// Token: 0x0400572A RID: 22314
		public Text m_lbCondition;

		// Token: 0x0400572B RID: 22315
		public Sprite m_spNormal;

		// Token: 0x0400572C RID: 22316
		public Color m_colNormal;

		// Token: 0x0400572D RID: 22317
		public Sprite m_spForbidden;

		// Token: 0x0400572E RID: 22318
		public Color m_colForbidden;
	}
}

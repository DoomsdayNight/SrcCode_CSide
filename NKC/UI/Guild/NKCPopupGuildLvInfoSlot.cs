using System;
using NKM.Guild;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B45 RID: 2885
	public class NKCPopupGuildLvInfoSlot : MonoBehaviour
	{
		// Token: 0x0600836F RID: 33647 RVA: 0x002C4C60 File Offset: 0x002C2E60
		public void SetData(GuildExpTemplet expTemplet)
		{
			NKCUtil.SetLabelText(this.m_lbLevel, expTemplet.GuildLevel.ToString());
			NKCUtil.SetLabelText(this.m_lbMember, expTemplet.MaxMemberCount.ToString());
			NKCUtil.SetLabelText(this.m_lbPoint, expTemplet.WelfarePoint.ToString());
			NKCUtil.SetLabelText(this.m_lbBonus, "");
		}

		// Token: 0x04006F95 RID: 28565
		public Text m_lbLevel;

		// Token: 0x04006F96 RID: 28566
		public Text m_lbMember;

		// Token: 0x04006F97 RID: 28567
		public Text m_lbPoint;

		// Token: 0x04006F98 RID: 28568
		public Text m_lbBonus;
	}
}

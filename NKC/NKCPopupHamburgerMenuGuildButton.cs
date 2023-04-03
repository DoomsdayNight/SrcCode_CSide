using System;
using NKC.UI;
using NKC.UI.Guild;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007F8 RID: 2040
	public class NKCPopupHamburgerMenuGuildButton : NKCPopupHamburgerMenuSimpleButton
	{
		// Token: 0x060050D1 RID: 20689 RVA: 0x00187B30 File Offset: 0x00185D30
		public void SetGuildData()
		{
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, NKCGuildManager.HasGuild());
				if (this.m_objGuild.activeSelf)
				{
					this.m_BadgeUI.SetData(NKCGuildManager.MyGuildData.badgeId, false);
					NKCUtil.SetLabelText(this.m_lbGuildName, NKCUtilString.GetUserGuildName(NKCGuildManager.MyGuildData.name, false));
				}
			}
		}

		// Token: 0x04004118 RID: 16664
		public GameObject m_objGuild;

		// Token: 0x04004119 RID: 16665
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x0400411A RID: 16666
		public Text m_lbGuildName;
	}
}

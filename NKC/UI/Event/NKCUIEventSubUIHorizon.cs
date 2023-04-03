using System;
using NKC.UI.Component;
using NKM.Event;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BDF RID: 3039
	public class NKCUIEventSubUIHorizon : NKCUIEventSubUIBase
	{
		// Token: 0x06008D05 RID: 36101 RVA: 0x002FF610 File Offset: 0x002FD810
		public override void Init()
		{
			base.Init();
			this.m_KillCountTotal.Init();
			this.m_KillCountPrivate.Init();
		}

		// Token: 0x06008D06 RID: 36102 RVA: 0x002FF630 File Offset: 0x002FD830
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			NKCUIEventSubUIHorizon.RewardGet = false;
			if (tabTemplet == null)
			{
				return;
			}
			this.m_tabTemplet = tabTemplet;
			if (NKCStringTable.CheckExistString(tabTemplet.m_EventHelpDesc))
			{
				NKCUtil.SetLabelText(this.m_lbEventDesc, NKCStringTable.GetString(tabTemplet.m_EventHelpDesc, false));
			}
			this.m_KillCountTotal.SetData(tabTemplet.m_EventID);
			this.m_KillCountPrivate.SetData(tabTemplet.m_EventID);
		}

		// Token: 0x06008D07 RID: 36103 RVA: 0x002FF694 File Offset: 0x002FD894
		public override void Refresh()
		{
			if (this.m_tabTemplet != null)
			{
				this.m_KillCountTotal.SetData(this.m_tabTemplet.m_EventID);
				this.m_KillCountPrivate.SetData(this.m_tabTemplet.m_EventID);
			}
		}

		// Token: 0x040079DD RID: 31197
		public Text m_lbEventDesc;

		// Token: 0x040079DE RID: 31198
		public NKCUIComKillCountTotal m_KillCountTotal;

		// Token: 0x040079DF RID: 31199
		public NKCUIComKillCountPrivate m_KillCountPrivate;

		// Token: 0x040079E0 RID: 31200
		public static bool RewardGet;
	}
}

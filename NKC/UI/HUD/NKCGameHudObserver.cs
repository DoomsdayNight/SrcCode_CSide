using System;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.HUD
{
	// Token: 0x02000C43 RID: 3139
	public class NKCGameHudObserver : MonoBehaviour
	{
		// Token: 0x06009269 RID: 37481 RVA: 0x0031FBA2 File Offset: 0x0031DDA2
		public void InitUI(NKCGameHud hud)
		{
			this.m_GameHud = hud;
			NKCUtil.SetButtonClickDelegate(this.m_cbtnChangeTeam, new UnityAction(this.ChangeTeamDeck));
			NKCUtil.SetGameobjectActive(this.m_cbtnChangeTeam, true);
		}

		// Token: 0x0600926A RID: 37482 RVA: 0x0031FBCE File Offset: 0x0031DDCE
		public void ChangeTeamDeck()
		{
			this.m_GameHud.ChangeTeamDeck();
		}

		// Token: 0x04007F66 RID: 32614
		public NKCUIComButton m_cbtnChangeTeam;

		// Token: 0x04007F67 RID: 32615
		private NKCGameHud m_GameHud;
	}
}

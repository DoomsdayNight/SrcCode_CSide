using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C11 RID: 3089
	public class NKCUILobbyMenuEventPassAdapter : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008EF9 RID: 36601 RVA: 0x0030A09C File Offset: 0x0030829C
		public void Init(ContentsType contentsType = ContentsType.None)
		{
			foreach (NKCUILobbyMenuEventPass nkcuilobbyMenuEventPass in this.lstEventPass)
			{
				nkcuilobbyMenuEventPass.Init(contentsType);
			}
		}

		// Token: 0x06008EFA RID: 36602 RVA: 0x0030A0F0 File Offset: 0x003082F0
		protected override void ContentsUpdate(NKMUserData userData)
		{
			foreach (NKCUILobbyMenuEventPass nkcuilobbyMenuEventPass in this.lstEventPass)
			{
				nkcuilobbyMenuEventPass.UpdateContents(userData);
			}
		}

		// Token: 0x06008EFB RID: 36603 RVA: 0x0030A144 File Offset: 0x00308344
		protected override void UpdateLock()
		{
			this.m_bLocked = !NKCContentManager.IsContentsUnlocked(this.m_ContentsType, 0, 0);
			foreach (NKCUILobbyMenuEventPass nkcuilobbyMenuEventPass in this.lstEventPass)
			{
				nkcuilobbyMenuEventPass.UpdateLock(this.m_bLocked);
			}
		}

		// Token: 0x06008EFC RID: 36604 RVA: 0x0030A1B0 File Offset: 0x003083B0
		public void UpdateEventPassEnabled()
		{
			foreach (NKCUILobbyMenuEventPass nkcuilobbyMenuEventPass in this.lstEventPass)
			{
				nkcuilobbyMenuEventPass.UpdateEventPassEnabled();
				nkcuilobbyMenuEventPass.UpdateLeftTime();
			}
		}

		// Token: 0x04007BFC RID: 31740
		public List<NKCUILobbyMenuEventPass> lstEventPass;
	}
}

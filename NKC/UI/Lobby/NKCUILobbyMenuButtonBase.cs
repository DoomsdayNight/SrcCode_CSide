using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C0C RID: 3084
	public abstract class NKCUILobbyMenuButtonBase : MonoBehaviour
	{
		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06008EC7 RID: 36551 RVA: 0x0030915E File Offset: 0x0030735E
		public bool Locked
		{
			get
			{
				return this.m_bLocked;
			}
		}

		// Token: 0x06008EC8 RID: 36552
		protected abstract void ContentsUpdate(NKMUserData userData);

		// Token: 0x06008EC9 RID: 36553 RVA: 0x00309166 File Offset: 0x00307366
		public virtual void PlayAnimation(bool bActive)
		{
		}

		// Token: 0x06008ECA RID: 36554 RVA: 0x00309168 File Offset: 0x00307368
		public virtual void CleanUp()
		{
		}

		// Token: 0x06008ECB RID: 36555 RVA: 0x0030916A File Offset: 0x0030736A
		public void UpdateData(NKMUserData userData)
		{
			this.UpdateLock();
			if (!this.m_bLocked)
			{
				this.ContentsUpdate(userData);
				return;
			}
			this.SetNotify(false);
		}

		// Token: 0x06008ECC RID: 36556 RVA: 0x00309189 File Offset: 0x00307389
		protected virtual void UpdateLock()
		{
			this.m_bLocked = !NKCContentManager.IsContentsUnlocked(this.m_ContentsType, 0, 0);
			NKCUtil.SetLabelText(this.m_lbLock, NKCContentManager.GetLockedMessage(this.m_ContentsType, 0));
			NKCUtil.SetGameobjectActive(this.m_objLock, this.m_bLocked);
		}

		// Token: 0x06008ECD RID: 36557 RVA: 0x003091C9 File Offset: 0x003073C9
		public void SetOnNotify(NKCUILobbyMenuButtonBase.OnNotify onNotify)
		{
			this.dOnNotify = onNotify;
		}

		// Token: 0x06008ECE RID: 36558 RVA: 0x003091D2 File Offset: 0x003073D2
		protected virtual void SetNotify(bool value)
		{
			this.m_bHasNotify = value;
			if (this.dOnNotify != null)
			{
				this.dOnNotify();
			}
		}

		// Token: 0x06008ECF RID: 36559 RVA: 0x003091EE File Offset: 0x003073EE
		public bool HasNotify()
		{
			return this.m_bHasNotify;
		}

		// Token: 0x04007BD4 RID: 31700
		public GameObject m_objLock;

		// Token: 0x04007BD5 RID: 31701
		public Text m_lbLock;

		// Token: 0x04007BD6 RID: 31702
		private NKCUILobbyMenuButtonBase.OnNotify dOnNotify;

		// Token: 0x04007BD7 RID: 31703
		private bool m_bHasNotify;

		// Token: 0x04007BD8 RID: 31704
		protected ContentsType m_ContentsType = ContentsType.None;

		// Token: 0x04007BD9 RID: 31705
		protected bool m_bLocked = true;

		// Token: 0x020019D6 RID: 6614
		// (Invoke) Token: 0x0600BA4D RID: 47693
		public delegate void OnNotify();
	}
}

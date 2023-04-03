using System;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Guild
{
	// Token: 0x02000B2A RID: 2858
	public class NKCPopupGuildCoopArtifactStorage : MonoBehaviour
	{
		// Token: 0x0600822D RID: 33325 RVA: 0x002BEB20 File Offset: 0x002BCD20
		public void InitUI()
		{
			this.m_ArtifactContent.Init();
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(this.Close));
		}

		// Token: 0x0600822E RID: 33326 RVA: 0x002BEB59 File Offset: 0x002BCD59
		public void Close()
		{
			this.m_ArtifactContent.Close();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600822F RID: 33327 RVA: 0x002BEB72 File Offset: 0x002BCD72
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_ArtifactContent.SetData(NKCGuildCoopManager.GetMyArtifactDictionary());
		}

		// Token: 0x04006E59 RID: 28249
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006E5A RID: 28250
		public NKCUIComGuildArtifactContent m_ArtifactContent;
	}
}

using System;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200072A RID: 1834
	public class NKC_SCEN_SHADOW_PALACE : NKC_SCEN_BASIC
	{
		// Token: 0x06004909 RID: 18697 RVA: 0x0016000F File Offset: 0x0015E20F
		public NKC_SCEN_SHADOW_PALACE()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_SHADOW_PALACE;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x0016001F File Offset: 0x0015E21F
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIData))
			{
				this.m_loadUIData = NKCUIShadowPalace.OpenNewInstanceAsync();
			}
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x00160040 File Offset: 0x0015E240
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!(this.m_shadowPalace == null))
			{
				return;
			}
			if (this.m_loadUIData != null && this.m_loadUIData.CheckLoadAndGetInstance<NKCUIShadowPalace>(out this.m_shadowPalace))
			{
				this.m_shadowPalace.Init();
				return;
			}
			Debug.LogError("NKC_SCEN_SHADOW_PALACE.ScenLoadUIComplete - ui load fail");
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x00160093 File Offset: 0x0015E293
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIShadowPalace shadowPalace = this.m_shadowPalace;
			if (shadowPalace != null)
			{
				shadowPalace.Open(NKCScenManager.CurrentUserData().m_ShadowPalace.currentPalaceId);
			}
			if (NKCScenManager.CurrentUserData().UserProfileData == null)
			{
				NKCPacketSender.Send_NKMPacket_MY_USER_PROFILE_INFO_REQ();
			}
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x001600CC File Offset: 0x0015E2CC
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIShadowPalace shadowPalace = this.m_shadowPalace;
			if (shadowPalace != null)
			{
				shadowPalace.Close();
			}
			this.UnloadUI();
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x001600EB File Offset: 0x0015E2EB
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_shadowPalace = null;
			NKCUIManager.LoadedUIData loadUIData = this.m_loadUIData;
			if (loadUIData != null)
			{
				loadUIData.CloseInstance();
			}
			this.m_loadUIData = null;
		}

		// Token: 0x04003879 RID: 14457
		private NKCUIShadowPalace m_shadowPalace;

		// Token: 0x0400387A RID: 14458
		private NKCUIManager.LoadedUIData m_loadUIData;
	}
}

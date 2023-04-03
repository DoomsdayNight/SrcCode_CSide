using System;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000732 RID: 1842
	public class NKC_SCEN_VOICE_LIST : NKC_SCEN_BASIC
	{
		// Token: 0x06004954 RID: 18772 RVA: 0x00160F71 File Offset: 0x0015F171
		public NKC_SCEN_VOICE_LIST()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_VOICE_LIST;
			this.m_NUF_VOICE_LIST = GameObject.Find("NUF_VOICE_LIST");
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x00160F9C File Offset: 0x0015F19C
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NUF_VOICE_LIST.SetActive(true);
			if (!this.m_bLoadedUI && this.m_NKC_SCEN_VOICE_LIST_UI_DATA.m_NUF_VOICE_LIST_PREFAB == null)
			{
				this.m_NKC_SCEN_VOICE_LIST_UI_DATA.m_NUF_VOICE_LIST_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_VOICE_LIST", "NUF_VOICE_LIST_PREFAB", true, null);
			}
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x00160FEC File Offset: 0x0015F1EC
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI)
			{
				this.m_NKC_SCEN_VOICE_LIST_UI_DATA.m_NUF_VOICE_LIST_PREFAB.m_Instant.transform.SetParent(this.m_NUF_VOICE_LIST.transform, false);
				this.m_NKCUIVoiceListDev = NKCUIVoiceListDev.Init(this.m_NKC_SCEN_VOICE_LIST_UI_DATA.m_NUF_VOICE_LIST_PREFAB.m_Instant);
			}
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x00161048 File Offset: 0x0015F248
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCUIVoiceListDev.Open();
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x0016105B File Offset: 0x0015F25B
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.m_NKCUIVoiceListDev.Close();
			this.m_NUF_VOICE_LIST.SetActive(false);
			this.UnloadUI();
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x00161080 File Offset: 0x0015F280
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCUIVoiceListDev = null;
			this.m_NKC_SCEN_VOICE_LIST_UI_DATA.Init();
		}

		// Token: 0x04003898 RID: 14488
		private GameObject m_NUF_VOICE_LIST;

		// Token: 0x04003899 RID: 14489
		private NKC_SCEN_VOICE_LIST_UI_DATA m_NKC_SCEN_VOICE_LIST_UI_DATA = new NKC_SCEN_VOICE_LIST_UI_DATA();

		// Token: 0x0400389A RID: 14490
		private NKCUIVoiceListDev m_NKCUIVoiceListDev;
	}
}

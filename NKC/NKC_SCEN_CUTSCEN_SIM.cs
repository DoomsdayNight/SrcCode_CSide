using System;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000702 RID: 1794
	public class NKC_SCEN_CUTSCEN_SIM : NKC_SCEN_BASIC
	{
		// Token: 0x06004666 RID: 18022 RVA: 0x0015629F File Offset: 0x0015449F
		public NKC_SCEN_CUTSCEN_SIM()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_CUTSCENE_SIM;
			this.m_NUF_CUTSCEN_SIM = GameObject.Find("NUF_CUTSCEN_SIM");
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x001562CC File Offset: 0x001544CC
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NUF_CUTSCEN_SIM.SetActive(true);
			if (!this.m_bLoadedUI && this.m_NKC_SCEN_CUTSCEN_SIM_UI_DATA.m_NUF_CUTSCEN_SIM_PREFAB == null)
			{
				this.m_NKC_SCEN_CUTSCEN_SIM_UI_DATA.m_NUF_CUTSCEN_SIM_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_CUTSCEN", "NUF_CUTSCEN_SIM_PREFAB", true, null);
			}
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x0015631C File Offset: 0x0015451C
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI)
			{
				this.m_NKC_SCEN_CUTSCEN_SIM_UI_DATA.m_NUF_CUTSCEN_SIM_PREFAB.m_Instant.transform.SetParent(this.m_NUF_CUTSCEN_SIM.transform, false);
				NKCUICutScenPlayer.InitiateInstance();
				this.m_NKCUICutsceneSimViewer = NKCUICutsceneSimViewer.InitUI();
			}
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x0015636D File Offset: 0x0015456D
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCUICutsceneSimViewer.Open();
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x00156380 File Offset: 0x00154580
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.m_NKCUICutsceneSimViewer.Close();
			this.m_NUF_CUTSCEN_SIM.SetActive(false);
			this.UnloadUI();
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x001563A5 File Offset: 0x001545A5
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCUICutsceneSimViewer = null;
			this.m_NKC_SCEN_CUTSCEN_SIM_UI_DATA.Init();
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x001563BF File Offset: 0x001545BF
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x001563C7 File Offset: 0x001545C7
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0400377C RID: 14204
		private GameObject m_NUF_CUTSCEN_SIM;

		// Token: 0x0400377D RID: 14205
		private NKC_SCEN_CUTSCEN_SIM_UI_DATA m_NKC_SCEN_CUTSCEN_SIM_UI_DATA = new NKC_SCEN_CUTSCEN_SIM_UI_DATA();

		// Token: 0x0400377E RID: 14206
		private NKCUICutsceneSimViewer m_NKCUICutsceneSimViewer;
	}
}

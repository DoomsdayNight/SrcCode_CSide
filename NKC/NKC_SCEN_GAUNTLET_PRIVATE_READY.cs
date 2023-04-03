using System;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000718 RID: 1816
	public class NKC_SCEN_GAUNTLET_PRIVATE_READY : NKC_SCEN_BASIC
	{
		// Token: 0x060047EB RID: 18411 RVA: 0x0015C0C0 File Offset: 0x0015A2C0
		public NKC_SCEN_GAUNTLET_PRIVATE_READY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY;
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0015C0D0 File Offset: 0x0015A2D0
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIData))
			{
				this.m_loadUIData = NKCUIGauntletPrivateReady.OpenNewInstanceAsync();
			}
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0015C0F0 File Offset: 0x0015A2F0
		private void SetBG()
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
				subUICameraVideoPlayer.m_fMoviePlaySpeed = 1f;
				subUICameraVideoPlayer.SetAlpha(0.6f);
				subUICameraVideoPlayer.Play("Gauntlet_BG.mp4", true, false, null, false);
			}
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x0015C138 File Offset: 0x0015A338
		public override void ScenLoadUpdate()
		{
			if (!NKCAssetResourceManager.IsLoadEnd())
			{
				return;
			}
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null && subUICameraVideoPlayer.IsPreparing())
			{
				return;
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x0015C16C File Offset: 0x0015A36C
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_PrivateReadyUI == null)
			{
				if (this.m_loadUIData == null || !this.m_loadUIData.CheckLoadAndGetInstance<NKCUIGauntletPrivateReady>(out this.m_PrivateReadyUI))
				{
					Debug.LogError("NKC_SCEN_GAUNTLET_PRIVATE_READY.ScenLoadUIComplete - ui load fail");
					return;
				}
				this.m_PrivateReadyUI.Init();
			}
			this.SetBG();
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x0015C1C6 File Offset: 0x0015A3C6
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIGauntletPrivateReady privateReadyUI = this.m_PrivateReadyUI;
			if (privateReadyUI == null)
			{
				return;
			}
			privateReadyUI.Open();
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x0015C1E0 File Offset: 0x0015A3E0
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_PrivateReadyUI != null)
			{
				this.m_PrivateReadyUI.Close();
				this.m_PrivateReadyUI = null;
			}
			NKCUIManager.LoadedUIData loadUIData = this.m_loadUIData;
			if (loadUIData != null)
			{
				loadUIData.CloseInstance();
			}
			this.m_loadUIData = null;
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0015C240 File Offset: 0x0015A440
		public void OnCancelAllProcess()
		{
			NKCUIGauntletPrivateReady privateReadyUI = this.m_PrivateReadyUI;
			if (privateReadyUI == null)
			{
				return;
			}
			privateReadyUI.ProcessBackButton();
		}

		// Token: 0x0400380C RID: 14348
		private NKCUIManager.LoadedUIData m_loadUIData;

		// Token: 0x0400380D RID: 14349
		public NKCUIGauntletPrivateReady m_PrivateReadyUI;
	}
}

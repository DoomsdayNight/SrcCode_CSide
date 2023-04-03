using System;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000713 RID: 1811
	public class NKC_SCEN_GAUNTLET_INTRO : NKC_SCEN_BASIC
	{
		// Token: 0x06004784 RID: 18308 RVA: 0x0015AD91 File Offset: 0x00158F91
		public NKC_SCEN_GAUNTLET_INTRO()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_INTRO;
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x0015ADA1 File Offset: 0x00158FA1
		public void ClearCacheData()
		{
			if (this.m_NKCUIGauntletIntro != null)
			{
				this.m_NKCUIGauntletIntro.CloseInstance();
				this.m_NKCUIGauntletIntro = null;
			}
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x0015ADC3 File Offset: 0x00158FC3
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (this.m_NKCUIGauntletIntro == null)
			{
				this.m_UILoadResourceData = NKCUIGauntletIntro.OpenInstanceAsync();
				return;
			}
			this.m_UILoadResourceData = null;
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x0015ADEC File Offset: 0x00158FEC
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
			if (this.m_NKCUIGauntletIntro == null && this.m_UILoadResourceData != null)
			{
				if (!NKCUIGauntletIntro.CheckInstanceLoaded(this.m_UILoadResourceData, out this.m_NKCUIGauntletIntro))
				{
					return;
				}
				this.m_UILoadResourceData = null;
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x0015AE50 File Offset: 0x00159050
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_NKCUIGauntletIntro != null)
			{
				this.m_NKCUIGauntletIntro.InitUI();
			}
			this.SetBG();
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x0015AE78 File Offset: 0x00159078
		private void SetBG()
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
				subUICameraVideoPlayer.m_fMoviePlaySpeed = 1f;
				subUICameraVideoPlayer.Play("Gauntlet_BG.mp4", true, false, null, false);
			}
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x0015AEB5 File Offset: 0x001590B5
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			if (this.m_NKCUIGauntletIntro != null)
			{
				this.m_NKCUIGauntletIntro.Open();
				NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED, -1);
				NKCContentManager.ShowContentUnlockPopup(null, Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
			}
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x0015AEEF File Offset: 0x001590EF
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIGauntletIntro != null)
			{
				this.m_NKCUIGauntletIntro.Close();
			}
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x0015AF10 File Offset: 0x00159110
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x0015AF18 File Offset: 0x00159118
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x040037EA RID: 14314
		private NKCAssetResourceData m_UILoadResourceData;

		// Token: 0x040037EB RID: 14315
		private NKCUIGauntletIntro m_NKCUIGauntletIntro;
	}
}

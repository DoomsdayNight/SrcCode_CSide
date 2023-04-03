using System;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x0200071A RID: 1818
	public class NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT : NKC_SCEN_BASIC
	{
		// Token: 0x060047FD RID: 18429 RVA: 0x0015C3F7 File Offset: 0x0015A5F7
		public NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x0015C407 File Offset: 0x0015A607
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIData))
			{
				this.m_loadUIData = NKCUIGauntletPrivateRoomDeckSelect.OpenNewInstanceAsync();
			}
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x0015C428 File Offset: 0x0015A628
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

		// Token: 0x06004800 RID: 18432 RVA: 0x0015C470 File Offset: 0x0015A670
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

		// Token: 0x06004801 RID: 18433 RVA: 0x0015C4A4 File Offset: 0x0015A6A4
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_PrivateRoomDeckSelectUI == null)
			{
				if (this.m_loadUIData == null || !this.m_loadUIData.CheckLoadAndGetInstance<NKCUIGauntletPrivateRoomDeckSelect>(out this.m_PrivateRoomDeckSelectUI))
				{
					Debug.LogError("NKC_SCEN_GAUNTLET_PRIVATE_DECK_SELECT.ScenLoadUIComplete - ui load fail");
					return;
				}
				this.m_PrivateRoomDeckSelectUI.Init();
			}
			this.SetBG();
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x0015C4FE File Offset: 0x0015A6FE
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIGauntletPrivateRoomDeckSelect privateRoomDeckSelectUI = this.m_PrivateRoomDeckSelectUI;
			if (privateRoomDeckSelectUI == null)
			{
				return;
			}
			privateRoomDeckSelectUI.Open();
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0015C518 File Offset: 0x0015A718
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_PrivateRoomDeckSelectUI != null)
			{
				this.m_PrivateRoomDeckSelectUI.Close();
				this.m_PrivateRoomDeckSelectUI = null;
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

		// Token: 0x06004804 RID: 18436 RVA: 0x0015C578 File Offset: 0x0015A778
		public void OnCancelAllProcess()
		{
			NKCUIGauntletPrivateRoomDeckSelect privateRoomDeckSelectUI = this.m_PrivateRoomDeckSelectUI;
			if (privateRoomDeckSelectUI == null)
			{
				return;
			}
			privateRoomDeckSelectUI.ProcessBackButton();
		}

		// Token: 0x04003811 RID: 14353
		private NKCUIManager.LoadedUIData m_loadUIData;

		// Token: 0x04003812 RID: 14354
		public NKCUIGauntletPrivateRoomDeckSelect m_PrivateRoomDeckSelectUI;
	}
}

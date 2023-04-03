using System;
using ClientPacket.Pvp;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000719 RID: 1817
	public class NKC_SCEN_GAUNTLET_PRIVATE_ROOM : NKC_SCEN_BASIC
	{
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060047F3 RID: 18419 RVA: 0x0015C252 File Offset: 0x0015A452
		public NKCUIGauntletPrivateRoom GauntletPrivateRoom
		{
			get
			{
				return this.m_gauntletPrivateRoom;
			}
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0015C25A File Offset: 0x0015A45A
		public NKC_SCEN_GAUNTLET_PRIVATE_ROOM()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM;
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x0015C26A File Offset: 0x0015A46A
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIData))
			{
				this.m_loadUIData = NKCUIGauntletPrivateRoom.OpenNewInstanceAsync();
			}
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x0015C28C File Offset: 0x0015A48C
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

		// Token: 0x060047F7 RID: 18423 RVA: 0x0015C2D4 File Offset: 0x0015A4D4
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

		// Token: 0x060047F8 RID: 18424 RVA: 0x0015C308 File Offset: 0x0015A508
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_gauntletPrivateRoom == null)
			{
				if (this.m_loadUIData == null || !this.m_loadUIData.CheckLoadAndGetInstance<NKCUIGauntletPrivateRoom>(out this.m_gauntletPrivateRoom))
				{
					Debug.LogError("NKC_SCEN_GAUNTLET_PRIVATE_ROOM.ScenLoadUIComplete - ui load fail");
					return;
				}
				this.m_gauntletPrivateRoom.Init();
			}
			this.SetBG();
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x0015C362 File Offset: 0x0015A562
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIGauntletPrivateRoom gauntletPrivateRoom = this.m_gauntletPrivateRoom;
			if (gauntletPrivateRoom == null)
			{
				return;
			}
			gauntletPrivateRoom.Open();
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x0015C37C File Offset: 0x0015A57C
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_gauntletPrivateRoom != null)
			{
				this.m_gauntletPrivateRoom.Close();
				this.m_gauntletPrivateRoom = null;
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

		// Token: 0x060047FB RID: 18427 RVA: 0x0015C3DC File Offset: 0x0015A5DC
		public void OnCancelAllProcess()
		{
			NKCUIGauntletPrivateRoom gauntletPrivateRoom = this.m_gauntletPrivateRoom;
			if (gauntletPrivateRoom == null)
			{
				return;
			}
			gauntletPrivateRoom.ProcessBackButton();
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x0015C3EE File Offset: 0x0015A5EE
		public void SetPvpGameLobbyState(NKMPvpGameLobbyState state)
		{
			this.m_pvpGameLobbyState = state;
		}

		// Token: 0x0400380E RID: 14350
		private NKCUIManager.LoadedUIData m_loadUIData;

		// Token: 0x0400380F RID: 14351
		private NKCUIGauntletPrivateRoom m_gauntletPrivateRoom;

		// Token: 0x04003810 RID: 14352
		private NKMPvpGameLobbyState m_pvpGameLobbyState;
	}
}

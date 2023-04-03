using System;
using ClientPacket.Pvp;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000712 RID: 1810
	public class NKC_SCEN_GAUNTLET_ASYNC_READY : NKC_SCEN_BASIC
	{
		// Token: 0x0600477A RID: 18298 RVA: 0x0015AB2C File Offset: 0x00158D2C
		public NKC_SCEN_GAUNTLET_ASYNC_READY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x0015AB51 File Offset: 0x00158D51
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIData))
			{
				this.m_loadUIData = NKCUIGauntletAsyncReady.OpenNewInstanceAsync();
			}
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x0015AB74 File Offset: 0x00158D74
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

		// Token: 0x0600477D RID: 18301 RVA: 0x0015ABBC File Offset: 0x00158DBC
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

		// Token: 0x0600477E RID: 18302 RVA: 0x0015ABF0 File Offset: 0x00158DF0
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_AsyncReadyUI == null)
			{
				if (this.m_loadUIData == null || !this.m_loadUIData.CheckLoadAndGetInstance<NKCUIGauntletAsyncReady>(out this.m_AsyncReadyUI))
				{
					Debug.LogError("NKC_SCEN_GAUNTLET_ASYNC_READY.ScenLoadUIComplete - ui load fail");
					return;
				}
				this.m_AsyncReadyUI.Init();
			}
			this.SetBG();
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x0015AC4C File Offset: 0x00158E4C
		public override void ScenStart()
		{
			base.ScenStart();
			if (this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
			{
				NKCUIGauntletAsyncReady asyncReadyUI = this.m_AsyncReadyUI;
				if (asyncReadyUI == null)
				{
					return;
				}
				asyncReadyUI.Open(this.m_NpcTarget, this.m_lastDeckIndex, this.m_gameType);
				return;
			}
			else
			{
				NKCUIGauntletAsyncReady asyncReadyUI2 = this.m_AsyncReadyUI;
				if (asyncReadyUI2 == null)
				{
					return;
				}
				asyncReadyUI2.Open(this.m_target, this.m_lastDeckIndex, this.m_gameType);
				return;
			}
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x0015ACB0 File Offset: 0x00158EB0
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_AsyncReadyUI != null)
			{
				this.m_lastDeckIndex = this.m_AsyncReadyUI.GetLastDeckIndex();
				this.m_AsyncReadyUI.Close();
				this.m_AsyncReadyUI = null;
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

		// Token: 0x06004781 RID: 18305 RVA: 0x0015AD21 File Offset: 0x00158F21
		public void SetReserveData(NpcPvpTarget target)
		{
			this.m_NpcTarget = target;
			this.m_gameType = NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC;
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x0015AD32 File Offset: 0x00158F32
		public void SetReserveData(AsyncPvpTarget target, NKM_GAME_TYPE gameType = NKM_GAME_TYPE.NGT_ASYNC_PVP)
		{
			this.m_target = target;
			this.m_gameType = gameType;
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x0015AD44 File Offset: 0x00158F44
		public void OnRecv(NKMPacket_ASYNC_PVP_START_GAME_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetTargetData(sPacket.refreshedTargetData);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetAsyncTargetList(sPacket.targetList);
			NKCUIGauntletAsyncReady asyncReadyUI = this.m_AsyncReadyUI;
			if (asyncReadyUI == null)
			{
				return;
			}
			asyncReadyUI.OnRecv(sPacket);
		}

		// Token: 0x040037E4 RID: 14308
		private NKCUIManager.LoadedUIData m_loadUIData;

		// Token: 0x040037E5 RID: 14309
		private NKCUIGauntletAsyncReady m_AsyncReadyUI;

		// Token: 0x040037E6 RID: 14310
		private AsyncPvpTarget m_target;

		// Token: 0x040037E7 RID: 14311
		private NpcPvpTarget m_NpcTarget;

		// Token: 0x040037E8 RID: 14312
		private NKM_GAME_TYPE m_gameType = NKM_GAME_TYPE.NGT_ASYNC_PVP;

		// Token: 0x040037E9 RID: 14313
		private NKMDeckIndex m_lastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);
	}
}

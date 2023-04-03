using System;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000714 RID: 1812
	public class NKC_SCEN_GAUNTLET_LEAGUE_ROOM : NKC_SCEN_BASIC
	{
		// Token: 0x0600478E RID: 18318 RVA: 0x0015AF1B File Offset: 0x0015911B
		public NKC_SCEN_GAUNTLET_LEAGUE_ROOM()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM;
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x0015AF2C File Offset: 0x0015912C
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_loadUIDataLeagueMatch))
			{
				this.m_loadUIDataLeagueMatch = NKCUIGauntletLeagueMatch.OpenNewInstanceAsync();
			}
			if (!NKCUIManager.IsValid(this.m_loadUIDataLeagueGlobalBan))
			{
				this.m_loadUIDataLeagueGlobalBan = NKCUIGauntletLeagueGlobalBan.OpenNewInstanceAsync();
			}
			if (!NKCUIManager.IsValid(this.m_loadUIDataLeagueMain))
			{
				this.m_loadUIDataLeagueMain = NKCUIGauntletLeagueMain.OpenNewInstanceAsync();
			}
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x0015AF88 File Offset: 0x00159188
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
			if (!NKCSoundManager.IsSameMusic("UI_PVP_02"))
			{
				NKCSoundManager.PlayMusic("UI_PVP_02", true, 1f, false, 0f, 0f);
			}
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0015AFF8 File Offset: 0x001591F8
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

		// Token: 0x06004792 RID: 18322 RVA: 0x0015B02C File Offset: 0x0015922C
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_gauntletLeageGlobalBan == null)
			{
				if (this.m_loadUIDataLeagueGlobalBan != null && this.m_loadUIDataLeagueGlobalBan.CheckLoadAndGetInstance<NKCUIGauntletLeagueGlobalBan>(out this.m_gauntletLeageGlobalBan))
				{
					this.m_gauntletLeageGlobalBan.Init();
				}
				else
				{
					Debug.LogError("NKC_SCEN_GAUNTLET_LEAGUE_ROOM.ScenLoadUIComplete - m_gauntletLeageGlobalBan load fail");
				}
			}
			if (this.m_gauntletLeagueMain == null)
			{
				if (this.m_loadUIDataLeagueMain != null && this.m_loadUIDataLeagueMain.CheckLoadAndGetInstance<NKCUIGauntletLeagueMain>(out this.m_gauntletLeagueMain))
				{
					this.m_gauntletLeagueMain.Init();
				}
				else
				{
					Debug.LogError("NKC_SCEN_GAUNTLET_LEAGUE_ROOM.ScenLoadUIComplete - m_gauntletLeagueMain load fail");
				}
			}
			if (this.m_gauntletLeagueMatch == null)
			{
				if (this.m_loadUIDataLeagueMatch != null && this.m_loadUIDataLeagueMatch.CheckLoadAndGetInstance<NKCUIGauntletLeagueMatch>(out this.m_gauntletLeagueMatch))
				{
					this.m_gauntletLeagueMatch.Init();
				}
				else
				{
					Debug.LogError("NKC_SCEN_GAUNTLET_LEAGUE_ROOM.ScenLoadUIComplete - m_gauntletLeagueMatch load fail ");
				}
			}
			this.SetBG();
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x0015B105 File Offset: 0x00159305
		public override void ScenStart()
		{
			base.ScenStart();
			NKCLeaguePVPMgr.m_LeagueRoomStarted = true;
			NKCLeaguePVPMgr.OnRoomStateChanged();
			NKCLeaguePVPMgr.UpdateReservedRoomData();
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x0015B120 File Offset: 0x00159320
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_gauntletLeagueMatch != null)
			{
				this.m_gauntletLeagueMatch.Close();
				this.m_gauntletLeagueMatch = null;
			}
			NKCUIManager.LoadedUIData loadUIDataLeagueMatch = this.m_loadUIDataLeagueMatch;
			if (loadUIDataLeagueMatch != null)
			{
				loadUIDataLeagueMatch.CloseInstance();
			}
			this.m_loadUIDataLeagueMatch = null;
			NKCUIManager.LoadedUIData loadUIDataLeagueGlobalBan = this.m_loadUIDataLeagueGlobalBan;
			if (loadUIDataLeagueGlobalBan != null)
			{
				loadUIDataLeagueGlobalBan.CloseInstance();
			}
			this.m_loadUIDataLeagueGlobalBan = null;
			NKCUIManager.LoadedUIData loadUIDataLeagueMain = this.m_loadUIDataLeagueMain;
			if (loadUIDataLeagueMain != null)
			{
				loadUIDataLeagueMain.CloseInstance();
			}
			this.m_loadUIDataLeagueMain = null;
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x0015B1B0 File Offset: 0x001593B0
		public void OnCancelAllProcess()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x040037EC RID: 14316
		private NKCUIManager.LoadedUIData m_loadUIDataLeagueMatch;

		// Token: 0x040037ED RID: 14317
		private NKCUIManager.LoadedUIData m_loadUIDataLeagueGlobalBan;

		// Token: 0x040037EE RID: 14318
		private NKCUIManager.LoadedUIData m_loadUIDataLeagueMain;

		// Token: 0x040037EF RID: 14319
		public NKCUIGauntletLeagueMatch m_gauntletLeagueMatch;

		// Token: 0x040037F0 RID: 14320
		public NKCUIGauntletLeagueGlobalBan m_gauntletLeageGlobalBan;

		// Token: 0x040037F1 RID: 14321
		public NKCUIGauntletLeagueMain m_gauntletLeagueMain;
	}
}

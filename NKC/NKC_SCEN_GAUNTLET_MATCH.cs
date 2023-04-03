using System;
using ClientPacket.Pvp;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000716 RID: 1814
	public class NKC_SCEN_GAUNTLET_MATCH : NKC_SCEN_BASIC
	{
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x0015BB46 File Offset: 0x00159D46
		public NKCUIGauntletMatch NKCUIGuantletMatch
		{
			get
			{
				return this.m_NKCUIGauntletMatch;
			}
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0015BB4E File Offset: 0x00159D4E
		public NKC_SCEN_GAUNTLET_MATCH()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_MATCH;
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x0015BB65 File Offset: 0x00159D65
		public void SetReservedGameType(NKM_GAME_TYPE eNKM_GAME_TYPE)
		{
			this.m_ReservedGameType = eNKM_GAME_TYPE;
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x0015BB6E File Offset: 0x00159D6E
		public void ClearCacheData()
		{
			if (this.m_NKCUIGauntletMatch != null)
			{
				this.m_NKCUIGauntletMatch.CloseInstance();
				this.m_NKCUIGauntletMatch = null;
			}
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x0015BB90 File Offset: 0x00159D90
		public void ProcessReLogin()
		{
			if (this.m_NKCUIGauntletMatch != null)
			{
				if (this.m_NKCUIGauntletMatch.Get_NKC_GAUNTLET_MATCH_STATE() == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE)
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
					return;
				}
				if (NKCScenManager.CurrentUserData().m_UserState != UserState.PVPReady)
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY, true);
					return;
				}
			}
			else
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
			}
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x0015BBF0 File Offset: 0x00159DF0
		public void ProcessReLogin(NKMGameData gameData)
		{
			if (this.m_NKCUIGauntletMatch != null)
			{
				if (this.m_NKCUIGauntletMatch.Get_NKC_GAUNTLET_MATCH_STATE() == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE && this.m_NKCUIGauntletMatch.GetVSShowTime())
				{
					if (NKCScenManager.GetScenManager().GetGameClient().GetGameDataDummy() == null)
					{
						NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(gameData, false);
					}
					return;
				}
				if (this.m_NKCUIGauntletMatch.Get_NKC_GAUNTLET_MATCH_STATE() == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCHING)
				{
					return;
				}
			}
			if (gameData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(gameData, true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
			}
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x0015BC78 File Offset: 0x00159E78
		public void SetDeckIndex(byte index)
		{
			NKCUIGauntletMatch.SetDeckIndex(index);
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x0015BC80 File Offset: 0x00159E80
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (this.m_NKCUIGauntletMatch == null)
			{
				this.m_UILoadResourceData = NKCUIGauntletMatch.OpenInstanceAsync();
				return;
			}
			this.m_UILoadResourceData = null;
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x0015BCAC File Offset: 0x00159EAC
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
			if (this.m_NKCUIGauntletMatch == null && this.m_UILoadResourceData != null)
			{
				if (!NKCUIGauntletMatch.CheckInstanceLoaded(this.m_UILoadResourceData, out this.m_NKCUIGauntletMatch))
				{
					return;
				}
				this.m_UILoadResourceData = null;
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0015BD10 File Offset: 0x00159F10
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_NKCUIGauntletMatch != null)
			{
				this.m_NKCUIGauntletMatch.InitUI();
			}
			this.SetBG();
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x0015BD38 File Offset: 0x00159F38
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

		// Token: 0x060047D5 RID: 18389 RVA: 0x0015BD78 File Offset: 0x00159F78
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			if (this.m_NKCUIGauntletMatch != null)
			{
				this.m_NKCUIGauntletMatch.Open(this.m_ReservedGameType);
				if (this.m_ReservedGameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC && this.m_reservedAsyncNpcTarget != null)
				{
					this.m_NKCUIGauntletMatch.SetTarget(this.m_reservedAsyncNpcTarget);
					this.m_reservedAsyncNpcTarget = null;
					return;
				}
				if (this.m_reservedAsyncTarget != null)
				{
					this.m_NKCUIGauntletMatch.SetTarget(this.m_reservedAsyncTarget);
					this.m_reservedAsyncTarget = null;
				}
			}
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x0015BDFB File Offset: 0x00159FFB
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIGauntletMatch != null)
			{
				this.m_NKCUIGauntletMatch.Close();
			}
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x0015BE1C File Offset: 0x0015A01C
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x0015BE24 File Offset: 0x0015A024
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0015BE27 File Offset: 0x0015A027
		public void OnRecv(NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)
		{
			this.m_NKCUIGauntletMatch.OnRecv(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0015BE35 File Offset: 0x0015A035
		public void SetReservedAsyncTarget(AsyncPvpTarget target)
		{
			this.m_reservedAsyncTarget = target;
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0015BE3E File Offset: 0x0015A03E
		public void SetReservedAsyncTarget(NpcPvpTarget target)
		{
			this.m_reservedAsyncNpcTarget = target;
		}

		// Token: 0x04003803 RID: 14339
		private NKCAssetResourceData m_UILoadResourceData;

		// Token: 0x04003804 RID: 14340
		private NKCUIGauntletMatch m_NKCUIGauntletMatch;

		// Token: 0x04003805 RID: 14341
		private NKM_GAME_TYPE m_ReservedGameType = NKM_GAME_TYPE.NGT_PVP_RANK;

		// Token: 0x04003806 RID: 14342
		private AsyncPvpTarget m_reservedAsyncTarget;

		// Token: 0x04003807 RID: 14343
		private NpcPvpTarget m_reservedAsyncNpcTarget;
	}
}

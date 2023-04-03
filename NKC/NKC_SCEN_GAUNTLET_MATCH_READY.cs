using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKC.UI;
using NKM;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000717 RID: 1815
	public class NKC_SCEN_GAUNTLET_MATCH_READY : NKC_SCEN_BASIC
	{
		// Token: 0x060047DC RID: 18396 RVA: 0x0015BE47 File Offset: 0x0015A047
		public NKC_SCEN_GAUNTLET_MATCH_READY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY;
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x0015BE6B File Offset: 0x0015A06B
		public NKMUserProfileData GetTargetProfileData()
		{
			return this.m_NKMUserProfileDataTarget;
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x0015BE73 File Offset: 0x0015A073
		public void SetNKMUserProfileDataTarget(NKMUserProfileData cNKMUserProfileData)
		{
			this.m_NKMUserProfileDataTarget = cNKMUserProfileData;
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x0015BE7C File Offset: 0x0015A07C
		public void SetTargetRankType(RANK_TYPE type)
		{
			this.m_TargetRankType = type;
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x0015BE85 File Offset: 0x0015A085
		public RANK_TYPE GetTargetRankType()
		{
			return this.m_TargetRankType;
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x0015BE8D File Offset: 0x0015A08D
		public void SetReservedGameType(NKM_GAME_TYPE eNKM_GAME_TYPE)
		{
			this.m_ReservedGameType = eNKM_GAME_TYPE;
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0015BE98 File Offset: 0x0015A098
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

		// Token: 0x060047E3 RID: 18403 RVA: 0x0015BECB File Offset: 0x0015A0CB
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			this.SetBG();
			NKCUIDeckViewer.Instance.LoadComplete();
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x0015BEE4 File Offset: 0x0015A0E4
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_GAUNTLET;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.PvPBattleFindTarget;
			options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnClickDeckSelect);
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(this.m_LastDeckIndex) == null)
			{
				this.m_LastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);
			}
			options.DeckIndex = this.m_LastDeckIndex;
			options.dOnBackButton = delegate()
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
			};
			options.SelectLeaderUnitOnOpen = true;
			options.bEnableDefaultBackground = false;
			options.bUpsideMenuHomeButton = false;
			options.upsideMenuShowResourceList = new List<int>
			{
				5,
				101
			};
			options.StageBattleStrID = string.Empty;
			NKCUIDeckViewer.Instance.Open(options, true);
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x0015BFD3 File Offset: 0x0015A1D3
		public void OnClickStart()
		{
			NKCUIDeckViewer.Instance.OnSideMenuButtonConfirm();
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x0015BFE0 File Offset: 0x0015A1E0
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

		// Token: 0x060047E7 RID: 18407 RVA: 0x0015C028 File Offset: 0x0015A228
		public void OnClickDeckSelect(NKMDeckIndex selectedDeckIndex)
		{
			if (this.m_ReservedGameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().SetReservedGameType(this.m_ReservedGameType);
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().SetDeckIndex(selectedDeckIndex.m_iIndex);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_MATCH, true);
			}
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x0015C078 File Offset: 0x0015A278
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.m_LastDeckIndex = NKCUIDeckViewer.Instance.GetSelectDeckIndex();
			NKCUIDeckViewer.CheckInstanceAndClose();
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0015C0B5 File Offset: 0x0015A2B5
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x0015C0BD File Offset: 0x0015A2BD
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x04003808 RID: 14344
		private NKMDeckIndex m_LastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);

		// Token: 0x04003809 RID: 14345
		private NKM_GAME_TYPE m_ReservedGameType = NKM_GAME_TYPE.NGT_PVP_RANK;

		// Token: 0x0400380A RID: 14346
		private NKMUserProfileData m_NKMUserProfileDataTarget;

		// Token: 0x0400380B RID: 14347
		private RANK_TYPE m_TargetRankType;
	}
}

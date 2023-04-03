using System;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200072D RID: 1837
	public class NKC_SCEN_TEAM : NKC_SCEN_BASIC
	{
		// Token: 0x06004926 RID: 18726 RVA: 0x001604FF File Offset: 0x0015E6FF
		public NKC_SCEN_TEAM()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_TEAM;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x00160519 File Offset: 0x0015E719
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NKCUIDeckView = NKCUIDeckViewer.Instance;
			this.m_NKCUIDeckView.Load(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00160546 File Offset: 0x0015E746
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			this.m_NKCUIDeckView.LoadComplete();
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x00160559 File Offset: 0x0015E759
		public override void ScenStart()
		{
			base.ScenStart();
			this.Open(true);
			NKCCamera.EnableBloom(false);
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, -1000f);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x00160587 File Offset: 0x0015E787
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.Close();
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x00160598 File Offset: 0x0015E798
		public void Open(bool bDeckInit)
		{
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_ORGANIZATION;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.DeckSetupOnly;
			options.dOnSideMenuButtonConfirm = null;
			options.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DAILY, 0);
			options.dOnBackButton = new NKCUIDeckViewer.DeckViewerOption.OnBackButton(this.ChangeSceneToHome);
			options.SelectLeaderUnitOnOpen = false;
			options.bEnableDefaultBackground = true;
			options.bUpsideMenuHomeButton = false;
			options.StageBattleStrID = string.Empty;
			this.m_NKCUIDeckView.Open(options, bDeckInit);
			this.CheckTutorial();
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x00160621 File Offset: 0x0015E821
		private void ChangeSceneToHome()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x0016062F File Offset: 0x0015E82F
		public void Close()
		{
			this.m_NKCUIDeckView.Close();
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x0016063C File Offset: 0x0015E83C
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			if (!NKCCamera.IsTrackingCameraPos())
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
			}
			this.m_BloomIntensity.Update(Time.deltaTime);
			if (!this.m_BloomIntensity.IsTracking())
			{
				this.m_BloomIntensity.SetTracking(NKMRandom.Range(1f, 2f), 4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			NKCCamera.SetBloomIntensity(this.m_BloomIntensity.GetNowValue());
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x001606DA File Offset: 0x0015E8DA
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x001606DD File Offset: 0x0015E8DD
		public void Close_UnitSelectList()
		{
			NKCUIUnitSelectList.CheckInstanceAndClose();
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x001606E4 File Offset: 0x0015E8E4
		public void Close_UnitInfo()
		{
			NKCUIUnitInfo.CheckInstanceAndClose();
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x001606EB File Offset: 0x0015E8EB
		public void UI_TEAM_SHIP_CLICK()
		{
			this.m_NKCUIDeckView.DeckViewShipClick();
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x001606F8 File Offset: 0x0015E8F8
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Team, true);
		}

		// Token: 0x04003885 RID: 14469
		private NKMTrackingFloat m_BloomIntensity = new NKMTrackingFloat();

		// Token: 0x04003886 RID: 14470
		private NKCUIDeckViewer m_NKCUIDeckView;
	}
}

using System;
using NKC.Publisher;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKC.UI.Result;
using NKM;

namespace NKC
{
	// Token: 0x02000711 RID: 1809
	public class NKC_SCEN_GAME_RESULT : NKC_SCEN_BASIC
	{
		// Token: 0x0600476F RID: 18287 RVA: 0x0015AA04 File Offset: 0x00158C04
		public void SetStageID(int stageID)
		{
			this.m_StageID = stageID;
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x0015AA0D File Offset: 0x00158C0D
		public int GetStageID()
		{
			return this.m_StageID;
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x0015AA15 File Offset: 0x00158C15
		public NKC_SCEN_GAME_RESULT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAME_RESULT;
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x0015AA25 File Offset: 0x00158C25
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (NKCUIManager.NKCUIGauntletResult == null)
			{
				this.m_UILoadResourceData = NKCUIGauntletResult.OpenInstanceAsync();
				return;
			}
			this.m_UILoadResourceData = null;
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x0015AA50 File Offset: 0x00158C50
		public override void ScenLoadUpdate()
		{
			if (NKCAssetResourceManager.IsLoadEnd())
			{
				if (NKCUIManager.NKCUIGauntletResult == null && this.m_UILoadResourceData != null)
				{
					NKCUIGauntletResult nkcuigauntletResult = null;
					if (!NKCUIGauntletResult.CheckInstanceLoaded(this.m_UILoadResourceData, out nkcuigauntletResult))
					{
						return;
					}
					NKCUIManager.NKCUIGauntletResult = nkcuigauntletResult;
					this.m_UILoadResourceData = null;
				}
				this.ScenLoadLastStart();
			}
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x0015AA9E File Offset: 0x00158C9E
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (NKCUIManager.NKCUIGauntletResult != null)
			{
				NKCUIManager.NKCUIGauntletResult.InitUI();
			}
			if (NKCPublisherModule.Auth.LogoutReservedAfterGame)
			{
				NKCPublisherModule.Auth.LogoutReserved();
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x0015AAD3 File Offset: 0x00158CD3
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIResult.CheckInstanceAndClose();
			NKCUIManager.NKCUIGauntletResult.Close();
			this.DoWhenScenStart();
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x0015AAF0 File Offset: 0x00158CF0
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIResult.CheckInstanceAndClose();
			NKCUIGauntletResult nkcuigauntletResult = NKCUIManager.NKCUIGauntletResult;
			if (nkcuigauntletResult == null)
			{
				return;
			}
			nkcuigauntletResult.Close();
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x0015AB0C File Offset: 0x00158D0C
		private void Update()
		{
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x0015AB0E File Offset: 0x00158D0E
		public void SetDoAtScenStart(NKC_SCEN_GAME_RESULT.DoAtScenStart doAtScenStart)
		{
			this.m_DoAtScenStart = doAtScenStart;
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x0015AB17 File Offset: 0x00158D17
		private void DoWhenScenStart()
		{
			if (this.m_DoAtScenStart != null)
			{
				this.m_DoAtScenStart();
			}
		}

		// Token: 0x040037E1 RID: 14305
		private NKC_SCEN_GAME_RESULT.DoAtScenStart m_DoAtScenStart;

		// Token: 0x040037E2 RID: 14306
		private NKCAssetResourceData m_UILoadResourceData;

		// Token: 0x040037E3 RID: 14307
		private int m_StageID;

		// Token: 0x020013F1 RID: 5105
		// (Invoke) Token: 0x0600A72C RID: 42796
		public delegate void DoAtScenStart();
	}
}

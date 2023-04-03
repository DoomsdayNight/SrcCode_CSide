using System;
using System.Collections.Generic;
using NKC.UI;
using NKC.UI.Result;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000707 RID: 1799
	public class NKC_SCEN_DIVE_RESULT : NKC_SCEN_BASIC
	{
		// Token: 0x06004697 RID: 18071 RVA: 0x00156909 File Offset: 0x00154B09
		public NKC_SCEN_DIVE_RESULT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_DIVE_RESULT;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x00156931 File Offset: 0x00154B31
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NKCUIWarfareResult = NKCUIWarfareResult.Instance;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x00156944 File Offset: 0x00154B44
		public bool GetExistNewData()
		{
			return this.m_bNewData;
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x0015694C File Offset: 0x00154B4C
		public void SetData(bool bDiveClear, bool bEventDive, NKMRewardData cNKMRewardData, NKMRewardData cNKMRewardDataArtifact, NKMItemMiscData cStormMiscReward, List<int> lstArtifact, NKMDeckIndex sNKMDeckIndex, NKMDiveTemplet diveTemplet)
		{
			this.m_bNewData = true;
			this.m_bDiveClear = bDiveClear;
			this.m_NKMRewardData = cNKMRewardData;
			this.m_NKMRewardDataArtifact = cNKMRewardDataArtifact;
			if (cStormMiscReward != null)
			{
				this.m_NKMRewardDataStorm = new NKMRewardData();
				this.m_NKMRewardDataStorm.SetMiscItemData(new List<NKMItemMiscData>
				{
					cStormMiscReward
				});
			}
			else
			{
				this.m_NKMRewardDataStorm = null;
			}
			this.m_lstArtifact.Clear();
			if (lstArtifact != null)
			{
				this.m_lstArtifact.AddRange(lstArtifact);
			}
			this.m_currNKMDeckIndex = sNKMDeckIndex;
			this.m_bEventDive = bEventDive;
			this.m_DiveTemplet = diveTemplet;
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x001569D9 File Offset: 0x00154BD9
		private void OnCallBackForResult(NKM_SCEN_ID scenID)
		{
			if (this.m_bEventDive)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedDiveReverseAni(true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(scenID, true);
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x00156A10 File Offset: 0x00154C10
		public override void ScenStart()
		{
			base.ScenStart();
			if (!this.m_bNewData)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE, true);
				return;
			}
			if (this.m_bDiveClear && this.m_DiveTemplet != null && !string.IsNullOrEmpty(this.m_DiveTemplet.CutsceneDiveBossAfter) && !NKCScenManager.CurrentUserData().m_LastDiveHistoryData.Contains(this.m_DiveTemplet.StageID))
			{
				NKCUICutScenPlayer.Instance.LoadAndPlay(this.m_DiveTemplet.CutsceneDiveBossAfter, 0, new NKCUICutScenPlayer.CutScenCallBack(this.ShowResult), true);
				return;
			}
			this.ShowResult();
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x00156AA4 File Offset: 0x00154CA4
		private void ShowResult()
		{
			this.m_NKCUIWarfareResult.OpenForDive(this.m_bDiveClear, this.m_NKMRewardData, this.m_NKMRewardDataArtifact, this.m_NKMRewardDataStorm, this.m_lstArtifact, this.m_currNKMDeckIndex, new NKCUIWarfareResult.CallBackWhenClosed(this.OnCallBackForResult));
			this.m_bNewData = false;
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x00156AF3 File Offset: 0x00154CF3
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.m_NKCUIWarfareResult.Close();
			this.m_NKCUIWarfareResult = null;
		}

		// Token: 0x0400378E RID: 14222
		private NKCUIWarfareResult m_NKCUIWarfareResult;

		// Token: 0x0400378F RID: 14223
		private bool m_bNewData;

		// Token: 0x04003790 RID: 14224
		private bool m_bDiveClear;

		// Token: 0x04003791 RID: 14225
		private NKMRewardData m_NKMRewardData;

		// Token: 0x04003792 RID: 14226
		private NKMRewardData m_NKMRewardDataArtifact;

		// Token: 0x04003793 RID: 14227
		private NKMRewardData m_NKMRewardDataStorm;

		// Token: 0x04003794 RID: 14228
		private NKMDeckIndex m_currNKMDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, 0);

		// Token: 0x04003795 RID: 14229
		private bool m_bEventDive;

		// Token: 0x04003796 RID: 14230
		private List<int> m_lstArtifact = new List<int>();

		// Token: 0x04003797 RID: 14231
		private NKMDiveTemplet m_DiveTemplet;
	}
}

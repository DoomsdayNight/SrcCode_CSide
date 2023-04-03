using System;
using System.Collections;
using UnityEngine;

namespace NKC.UI.Result
{
	// Token: 0x02000BA1 RID: 2977
	public class NKCUIResultSubUIMiddle : NKCUIResultSubUIBase
	{
		// Token: 0x060089AD RID: 35245 RVA: 0x002EAC01 File Offset: 0x002E8E01
		public void Init()
		{
		}

		// Token: 0x060089AE RID: 35246 RVA: 0x002EAC03 File Offset: 0x002E8E03
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.bWaiting = false;
			this.m_bAutoSkip = bAutoSkip;
			bool bWorldMapBranchExpProcessWaiting = false;
			bool bDungeonMissionProcessWaiting = false;
			bool bRaidProcessWaiting = false;
			if (!this.m_uiWorldMapBranchExp.ProcessRequired && !this.m_uiDungeonMission.ProcessRequired && !this.m_NKCUIResultSubUIRaid.ProcessRequired)
			{
				yield break;
			}
			int processRequiredCount = 0;
			if (this.m_uiWorldMapBranchExp.ProcessRequired)
			{
				int num = processRequiredCount;
				processRequiredCount = num + 1;
				NKCUtil.SetGameobjectActive(this.m_uiWorldMapBranchExp, true);
				yield return this.m_uiWorldMapBranchExp.Process(this.m_bAutoSkip);
				bWorldMapBranchExpProcessWaiting = true;
			}
			if (this.m_uiDungeonMission.ProcessRequired)
			{
				int num = processRequiredCount;
				processRequiredCount = num + 1;
				NKCUtil.SetGameobjectActive(this.m_uiDungeonMission, true);
				yield return this.m_uiDungeonMission.Process(this.m_bAutoSkip);
				bDungeonMissionProcessWaiting = true;
			}
			if (this.m_NKCUIResultSubUIRaid.ProcessRequired)
			{
				int num = processRequiredCount;
				processRequiredCount = num + 1;
				NKCUtil.SetGameobjectActive(this.m_NKCUIResultSubUIRaid, true);
				yield return this.m_NKCUIResultSubUIRaid.Process(this.m_bAutoSkip);
				bRaidProcessWaiting = true;
			}
			yield return null;
			while ((processRequiredCount > 0 || this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) && !this.m_bHadUserInput)
			{
				if (bWorldMapBranchExpProcessWaiting && this.m_uiWorldMapBranchExp.IsProcessFinished())
				{
					bWorldMapBranchExpProcessWaiting = false;
					int num = processRequiredCount;
					processRequiredCount = num - 1;
				}
				if (bDungeonMissionProcessWaiting && this.m_uiDungeonMission.IsProcessFinished())
				{
					bDungeonMissionProcessWaiting = false;
					int num = processRequiredCount;
					processRequiredCount = num - 1;
				}
				if (bRaidProcessWaiting && this.m_NKCUIResultSubUIRaid.IsProcessFinished())
				{
					bRaidProcessWaiting = false;
					int num = processRequiredCount;
					processRequiredCount = num - 1;
				}
				yield return null;
			}
			this.FinishProcess();
			yield break;
		}

		// Token: 0x060089AF RID: 35247 RVA: 0x002EAC19 File Offset: 0x002E8E19
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089B0 RID: 35248 RVA: 0x002EAC24 File Offset: 0x002E8E24
		public override void FinishProcess()
		{
			if (this.bWaiting)
			{
				return;
			}
			this.bWaiting = true;
			base.StopAllCoroutines();
			this.m_animator.Play("INTRO", 0, 1f);
			this.m_uiWorldMapBranchExp.FinishProcess();
			this.m_uiDungeonMission.FinishProcess();
			this.m_NKCUIResultSubUIRaid.FinishProcess();
			base.StartCoroutine(this.WaitForCloseAnimation());
		}

		// Token: 0x060089B1 RID: 35249 RVA: 0x002EAC8B File Offset: 0x002E8E8B
		public IEnumerator WaitForCloseAnimation()
		{
			this.m_bHadUserInput = false;
			if (this.m_bAutoSkip)
			{
				float currentTime = 0f;
				while (this.UI_END_DELAY_TIME > currentTime)
				{
					if (this.m_bHadUserInput)
					{
						break;
					}
					currentTime += Time.deltaTime;
					yield return null;
				}
			}
			else
			{
				yield return base.WaitAniOrInput(null);
			}
			while (this.m_bPause)
			{
				yield return null;
			}
			yield return base.PlayCloseAnimation(this.m_animator);
			this.m_bFinished = true;
			this.bWaiting = false;
			yield break;
		}

		// Token: 0x060089B2 RID: 35250 RVA: 0x002EAC9A File Offset: 0x002E8E9A
		public override void Close()
		{
			this.m_uiWorldMapBranchExp.Close();
			this.m_uiDungeonMission.Close();
			this.m_NKCUIResultSubUIRaid.Close();
			base.Close();
		}

		// Token: 0x060089B3 RID: 35251 RVA: 0x002EACC4 File Offset: 0x002E8EC4
		public void SetDataBattleResult(NKCUIResult.BattleResultData data, float endDelayTime, bool bIgnoreAutoClose = false)
		{
			this.UI_END_DELAY_TIME = endDelayTime;
			this.m_uiWorldMapBranchExp.SetData(-1, -1, -1, -1, -1, bIgnoreAutoClose);
			this.m_uiDungeonMission.SetData(data.m_lstMissionData, data.m_bShowMedal, bIgnoreAutoClose, data.m_bShowClearPoint, data.m_fArenaClearPoint);
			this.m_NKCUIResultSubUIRaid.SetData(data, bIgnoreAutoClose);
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
			if (this.m_uiDungeonMission.ProcessRequired || this.m_NKCUIResultSubUIRaid.ProcessRequired)
			{
				base.ProcessRequired = true;
				return;
			}
			base.ProcessRequired = false;
		}

		// Token: 0x060089B4 RID: 35252 RVA: 0x002EAD4C File Offset: 0x002E8F4C
		public void SetDataWorldMapMissionResult(NKCUIResult.CityMissionResultData resultData, float endDelayTime, bool bIgnoreAutoClose = false)
		{
			this.UI_END_DELAY_TIME = endDelayTime;
			this.m_uiWorldMapBranchExp.SetData(resultData.m_CityID, resultData.m_CityLevelOld, resultData.m_CityLevelNew, resultData.m_CityExpOld, resultData.m_CityExpNew, bIgnoreAutoClose);
			this.m_uiDungeonMission.SetData(null, false, false, false, 0f);
			this.m_NKCUIResultSubUIRaid.SetData(null, false);
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
			if (this.m_uiWorldMapBranchExp.ProcessRequired)
			{
				base.ProcessRequired = true;
				return;
			}
			base.ProcessRequired = false;
		}

		// Token: 0x060089B5 RID: 35253 RVA: 0x002EADD0 File Offset: 0x002E8FD0
		public void SetDataNull()
		{
			this.m_uiWorldMapBranchExp.SetData(-1, -1, -1, -1, -1, false);
			this.m_uiDungeonMission.SetData(null, true, false, false, 0f);
			this.m_NKCUIResultSubUIRaid.SetData(null, false);
			this.m_bIgnoreAutoClose = false;
			base.ProcessRequired = false;
		}

		// Token: 0x04007617 RID: 30231
		public Animator m_animator;

		// Token: 0x04007618 RID: 30232
		public NKCUIResultSubUIWorldmapBranchExp m_uiWorldMapBranchExp;

		// Token: 0x04007619 RID: 30233
		public NKCUIResultSubUIDungeon m_uiDungeonMission;

		// Token: 0x0400761A RID: 30234
		public NKCUIResultSubUIRaid m_NKCUIResultSubUIRaid;

		// Token: 0x0400761B RID: 30235
		private bool m_bFinished;

		// Token: 0x0400761C RID: 30236
		private float UI_END_DELAY_TIME = 1f;

		// Token: 0x0400761D RID: 30237
		private bool m_bAutoSkip;

		// Token: 0x0400761E RID: 30238
		private bool bWaiting;
	}
}

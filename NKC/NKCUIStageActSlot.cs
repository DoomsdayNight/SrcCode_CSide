using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A11 RID: 2577
	public class NKCUIStageActSlot : MonoBehaviour
	{
		// Token: 0x06007089 RID: 28809 RVA: 0x00254D17 File Offset: 0x00252F17
		public int GetStageViewerID()
		{
			return this.m_StageViewerID;
		}

		// Token: 0x0600708A RID: 28810 RVA: 0x00254D1F File Offset: 0x00252F1F
		public int GetSortIndex()
		{
			return this.m_SortIndex;
		}

		// Token: 0x0600708B RID: 28811 RVA: 0x00254D27 File Offset: 0x00252F27
		public bool IsLocked()
		{
			return this.m_bIsLocked;
		}

		// Token: 0x0600708C RID: 28812 RVA: 0x00254D30 File Offset: 0x00252F30
		public void SetData(int episodeID, EPISODE_DIFFICULTY difficulty, int actID, string actName, NKCUIComToggleGroup tglGroup, NKCUIStageActSlot.OnSelectActSlot onSelectActSlot)
		{
			this.m_tgl.OnValueChanged.RemoveAllListeners();
			this.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
			this.m_tgl.m_bGetCallbackWhileLocked = true;
			this.m_tgl.SetToggleGroup(tglGroup);
			this.m_EpisodeID = episodeID;
			this.m_Difficulty = difficulty;
			this.m_ActID = actID;
			this.m_dOnSelectActSlot = onSelectActSlot;
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, difficulty);
			if (nkmepisodeTempletV != null)
			{
				this.m_SortIndex = nkmepisodeTempletV.m_SortIndex;
				if (nkmepisodeTempletV.UseEpSlot())
				{
					this.m_StageViewerID = this.m_EpisodeID;
				}
				else
				{
					this.m_StageViewerID = this.m_ActID;
				}
				NKMStageTempletV2 firstStage = nkmepisodeTempletV.GetFirstStage(nkmepisodeTempletV.UseEpSlot() ? 1 : actID);
				if (firstStage != null)
				{
					if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), firstStage.m_UnlockInfo, false))
					{
						this.m_tgl.Lock(false);
						NKCUtil.SetGameobjectActive(this.m_objLocked, true);
						this.m_bIsLocked = true;
						if (!NKMContentUnlockManager.IsStarted(firstStage.m_UnlockInfo))
						{
							this.m_lockEndTimeUtc = NKMContentUnlockManager.GetConditionStartTime(firstStage.m_UnlockInfo);
							NKCUtil.SetGameobjectActive(this.m_objLocked, true);
							NKCUtil.SetGameobjectActive(this.m_objRemainTime, true);
							this.m_bIsLocked = true;
							this.m_bUseLockEndTime = true;
							this.SetLockText(this.m_lockEndTimeUtc);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_objLocked, true);
							NKCUtil.SetGameobjectActive(this.m_objRemainTime, false);
							this.m_bIsLocked = true;
							this.m_bUseLockEndTime = false;
						}
					}
					else
					{
						this.m_tgl.UnLock(false);
						NKCUtil.SetGameobjectActive(this.m_objLocked, false);
						NKCUtil.SetGameobjectActive(this.m_objRemainTime, false);
						this.m_bIsLocked = false;
					}
				}
				if (this.m_fRajdhani != null && this.m_fMain != null)
				{
					EPISODE_CATEGORY epcategory = nkmepisodeTempletV.m_EPCategory;
					if (epcategory == EPISODE_CATEGORY.EC_MAINSTREAM || (epcategory != EPISODE_CATEGORY.EC_DAILY && epcategory != EPISODE_CATEGORY.EC_SUPPLY))
					{
						this.m_lbActNameOn.font = this.m_fRajdhani;
						this.m_lbActNameOn.fontSize = this.m_RajdhaniSize;
						this.m_lbActNameOff.font = this.m_fRajdhani;
						this.m_lbActNameOff.fontSize = this.m_RajdhaniSize;
					}
					else
					{
						this.m_lbActNameOn.font = this.m_fMain;
						this.m_lbActNameOn.fontSize = this.m_MainSize;
						this.m_lbActNameOff.font = this.m_fMain;
						this.m_lbActNameOff.fontSize = this.m_MainSize;
					}
				}
			}
			if (!string.IsNullOrEmpty(actName))
			{
				NKCUtil.SetLabelText(this.m_lbActNameOn, actName);
				NKCUtil.SetLabelText(this.m_lbActNameOff, actName);
			}
			this.RefreshReddot();
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x00254FB8 File Offset: 0x002531B8
		public void SetLockText(DateTime lockEndTimeUtc)
		{
			if (lockEndTimeUtc < NKCSynchronizedTime.GetServerUTCTime(0.0))
			{
				this.SetData(this.m_EpisodeID, this.m_Difficulty, this.m_ActID, "", this.m_tgl.m_ToggleGroup, this.m_dOnSelectActSlot);
				return;
			}
			if (NKCSynchronizedTime.GetTimeLeft(lockEndTimeUtc).TotalSeconds < 1.0)
			{
				NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GET_STRING_QUIT);
				return;
			}
			string remainTimeString = NKCUtilString.GetRemainTimeString(lockEndTimeUtc, 2);
			NKCUtil.SetLabelText(this.m_lbRemainTime, string.Format(NKCUtilString.GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM_CLOSE, remainTimeString));
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x00255052 File Offset: 0x00253252
		public void ResetData()
		{
			this.m_EpisodeID = 0;
			this.m_Difficulty = EPISODE_DIFFICULTY.NORMAL;
			this.m_ActID = 0;
			this.m_StageViewerID = 0;
			this.m_SortIndex = 0;
		}

		// Token: 0x0600708F RID: 28815 RVA: 0x00255077 File Offset: 0x00253277
		public void SetSelected(bool bValue)
		{
			this.m_tgl.Select(bValue, true, true);
		}

		// Token: 0x06007090 RID: 28816 RVA: 0x00255088 File Offset: 0x00253288
		private void OnValueChanged(bool bValue)
		{
			if (this.m_tgl.m_bLock)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(this.m_EpisodeID, this.m_Difficulty);
				if (nkmepisodeTempletV != null)
				{
					if (nkmepisodeTempletV.m_DicStage.ContainsKey(this.m_ActID))
					{
						NKMStageTempletV2 firstStage = nkmepisodeTempletV.GetFirstStage(this.m_ActID);
						if (firstStage != null)
						{
							string unlockConditionRequireDesc = NKCUtilString.GetUnlockConditionRequireDesc(firstStage, false);
							NKCUIManager.NKCPopupMessage.Open(new PopupMessage(unlockConditionRequireDesc, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
						}
					}
					return;
				}
			}
			if (bValue)
			{
				NKCUIStageActSlot.OnSelectActSlot dOnSelectActSlot = this.m_dOnSelectActSlot;
				if (dOnSelectActSlot == null)
				{
					return;
				}
				dOnSelectActSlot(this.m_ActID);
			}
		}

		// Token: 0x06007091 RID: 28817 RVA: 0x00255115 File Offset: 0x00253315
		public void RefreshReddot()
		{
			NKCUtil.SetGameobjectActive(this.m_objNew, NKMEpisodeMgr.HasReddot(this.m_EpisodeID, this.m_Difficulty, this.m_ActID));
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x00255139 File Offset: 0x00253339
		private void Update()
		{
			if (this.m_bUseLockEndTime)
			{
				this.m_deltaTime += Time.deltaTime;
				if (this.m_deltaTime > 1f)
				{
					this.m_deltaTime = 0f;
					this.SetLockText(this.m_lockEndTimeUtc);
				}
			}
		}

		// Token: 0x04005C3E RID: 23614
		[Header("폰트")]
		public Font m_fMain;

		// Token: 0x04005C3F RID: 23615
		public int m_MainSize;

		// Token: 0x04005C40 RID: 23616
		public Font m_fRajdhani;

		// Token: 0x04005C41 RID: 23617
		public int m_RajdhaniSize;

		// Token: 0x04005C42 RID: 23618
		[Space]
		public NKCUIComToggle m_tgl;

		// Token: 0x04005C43 RID: 23619
		public Text m_lbActNameOn;

		// Token: 0x04005C44 RID: 23620
		public Text m_lbActNameOff;

		// Token: 0x04005C45 RID: 23621
		public GameObject m_objNew;

		// Token: 0x04005C46 RID: 23622
		public GameObject m_objLocked;

		// Token: 0x04005C47 RID: 23623
		public GameObject m_objRemainTime;

		// Token: 0x04005C48 RID: 23624
		public Text m_lbRemainTime;

		// Token: 0x04005C49 RID: 23625
		private NKCUIStageActSlot.OnSelectActSlot m_dOnSelectActSlot;

		// Token: 0x04005C4A RID: 23626
		private int m_EpisodeID;

		// Token: 0x04005C4B RID: 23627
		private int m_ActID;

		// Token: 0x04005C4C RID: 23628
		private EPISODE_DIFFICULTY m_Difficulty;

		// Token: 0x04005C4D RID: 23629
		private int m_StageViewerID;

		// Token: 0x04005C4E RID: 23630
		private int m_SortIndex;

		// Token: 0x04005C4F RID: 23631
		private bool m_bIsLocked;

		// Token: 0x04005C50 RID: 23632
		private bool m_bUseLockEndTime;

		// Token: 0x04005C51 RID: 23633
		private DateTime m_lockEndTimeUtc;

		// Token: 0x04005C52 RID: 23634
		private float m_deltaTime;

		// Token: 0x0200174E RID: 5966
		// (Invoke) Token: 0x0600B2E6 RID: 45798
		public delegate void OnSelectActSlot(int actID);
	}
}

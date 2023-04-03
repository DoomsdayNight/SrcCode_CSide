using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200093E RID: 2366
	public class NKCUIComMissionGroup : MonoBehaviour
	{
		// Token: 0x06005E97 RID: 24215 RVA: 0x001D5A08 File Offset: 0x001D3C08
		public void Init()
		{
			if (this.m_RewardSlot != null)
			{
				int num = this.m_RewardSlot.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUISlot nkcuislot = this.m_RewardSlot[i];
					if (nkcuislot != null)
					{
						nkcuislot.Init();
					}
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRewardGet, new UnityAction(this.OnClickGet));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRewardLocked, new UnityAction(this.OnClickRewardLock));
		}

		// Token: 0x06005E98 RID: 24216 RVA: 0x001D5A74 File Offset: 0x001D3C74
		public void SetData(int missionGroup, NKCUIComMissionGroup.OnRewardGet onRewardGet, NKCUIComMissionGroup.OnRewardLocked onRewardLocked = null)
		{
			this.m_dOnRewardGet = onRewardGet;
			this.m_dOnRewardLocked = onRewardLocked;
			List<NKMMissionTemplet> missionTempletListByGroupID = NKMMissionManager.GetMissionTempletListByGroupID(missionGroup);
			if (missionTempletListByGroupID.Count <= 0)
			{
				this.SetEmptyDataUI();
				return;
			}
			NKMMissionData missionData = NKCScenManager.CurrentUserData().m_MissionData.GetMissionData(missionTempletListByGroupID[0]);
			this.m_currentMissionTemplet = null;
			this.m_allMissionCompleted = false;
			long num = 0L;
			bool flag = false;
			long num2 = 0L;
			if (missionData == null)
			{
				this.m_currentMissionTemplet = missionTempletListByGroupID[0];
			}
			else
			{
				int count = missionTempletListByGroupID.Count;
				for (int i = 0; i < count; i++)
				{
					if (missionTempletListByGroupID[i].m_MissionID < missionData.mission_id)
					{
						num2 = missionTempletListByGroupID[i].m_Times;
					}
					else
					{
						if (missionData.mission_id != missionTempletListByGroupID[i].m_MissionID || !missionData.isComplete)
						{
							this.m_currentMissionTemplet = missionTempletListByGroupID[i];
							this.m_allMissionCompleted = false;
							num = missionData.times;
							flag = missionData.isComplete;
							break;
						}
						if (i < count - 1)
						{
							num2 = missionTempletListByGroupID[i].m_Times;
						}
						this.m_allMissionCompleted = true;
						this.m_currentMissionTemplet = missionTempletListByGroupID[i];
						num = missionData.times;
						flag = missionData.isComplete;
					}
				}
			}
			if (this.m_currentMissionTemplet == null)
			{
				this.SetEmptyDataUI();
				return;
			}
			NKCUtil.SetLabelText(this.m_lbDesc, this.m_currentMissionTemplet.GetDesc());
			NKCUtil.SetLabelText(this.m_lbCurrentProgress, num.ToString());
			NKCUtil.SetLabelText(this.m_lbDestProgress, this.m_currentMissionTemplet.m_Times.ToString());
			NKCUtil.SetImageFillAmount(this.m_imgProgressGauge, (float)(num - num2) / (float)(this.m_currentMissionTemplet.m_Times - num2));
			bool rewardGetButtonState = num >= this.m_currentMissionTemplet.m_Times && !flag;
			this.SetRewardGetButtonState(rewardGetButtonState);
			int count2 = this.m_currentMissionTemplet.m_MissionReward.Count;
			if (this.m_RewardSlot != null)
			{
				int num3 = this.m_RewardSlot.Length;
				for (int j = 0; j < num3; j++)
				{
					if (j >= count2)
					{
						NKCUtil.SetGameobjectActive(this.m_RewardSlot[j], false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_RewardSlot[j], true);
						NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(this.m_currentMissionTemplet.m_MissionReward[j].reward_id, (long)this.m_currentMissionTemplet.m_MissionReward[j].reward_value, 0);
						NKCUISlot nkcuislot = this.m_RewardSlot[j];
						if (nkcuislot != null)
						{
							nkcuislot.SetData(data, true, null);
						}
					}
				}
			}
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x001D5CED File Offset: 0x001D3EED
		private void SetRewardGetButtonState(bool rewardGetEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_csbtnRewardGet, rewardGetEnable);
			NKCUtil.SetGameobjectActive(this.m_csbtnRewardLocked, !rewardGetEnable);
			NKCUtil.SetGameobjectActive(this.m_objErrandEnableFx, rewardGetEnable);
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x001D5D18 File Offset: 0x001D3F18
		private void SetEmptyDataUI()
		{
			NKCUtil.SetLabelText(this.m_lbDesc, "-");
			NKCUtil.SetLabelText(this.m_lbCurrentProgress, "-");
			NKCUtil.SetLabelText(this.m_lbDestProgress, "-");
			NKCUtil.SetImageFillAmount(this.m_imgProgressGauge, 0f);
			if (this.m_RewardSlot != null)
			{
				int num = this.m_RewardSlot.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_RewardSlot[i], false);
				}
			}
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x001D5D90 File Offset: 0x001D3F90
		private void OnClickGet()
		{
			if (this.m_dOnRewardGet != null)
			{
				this.m_dOnRewardGet(this.m_currentMissionTemplet);
			}
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x001D5DAB File Offset: 0x001D3FAB
		private void OnClickRewardLock()
		{
			if (this.m_dOnRewardLocked != null)
			{
				this.m_dOnRewardLocked(this.m_allMissionCompleted);
			}
		}

		// Token: 0x04004AB4 RID: 19124
		public Text m_lbDesc;

		// Token: 0x04004AB5 RID: 19125
		public Text m_lbCurrentProgress;

		// Token: 0x04004AB6 RID: 19126
		public Text m_lbDestProgress;

		// Token: 0x04004AB7 RID: 19127
		public Image m_imgProgressGauge;

		// Token: 0x04004AB8 RID: 19128
		public NKCUISlot[] m_RewardSlot;

		// Token: 0x04004AB9 RID: 19129
		public NKCUIComStateButton m_csbtnRewardGet;

		// Token: 0x04004ABA RID: 19130
		public NKCUIComStateButton m_csbtnRewardLocked;

		// Token: 0x04004ABB RID: 19131
		public GameObject m_objErrandEnableFx;

		// Token: 0x04004ABC RID: 19132
		private NKMMissionTemplet m_currentMissionTemplet;

		// Token: 0x04004ABD RID: 19133
		private bool m_allMissionCompleted;

		// Token: 0x04004ABE RID: 19134
		private NKCUIComMissionGroup.OnRewardGet m_dOnRewardGet;

		// Token: 0x04004ABF RID: 19135
		private NKCUIComMissionGroup.OnRewardLocked m_dOnRewardLocked;

		// Token: 0x020015CB RID: 5579
		// (Invoke) Token: 0x0600AE52 RID: 44626
		public delegate void OnRewardGet(NKMMissionTemplet missionTemplet);

		// Token: 0x020015CC RID: 5580
		// (Invoke) Token: 0x0600AE56 RID: 44630
		public delegate void OnRewardLocked(bool allMissionCompleted);
	}
}

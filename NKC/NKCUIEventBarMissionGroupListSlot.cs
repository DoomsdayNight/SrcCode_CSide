using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000955 RID: 2389
	public class NKCUIEventBarMissionGroupListSlot : MonoBehaviour
	{
		// Token: 0x06005F45 RID: 24389 RVA: 0x001D98BC File Offset: 0x001D7ABC
		public static NKCUIEventBarMissionGroupListSlot GetNewInstance(Transform parent, string bundleName, string assetName)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCUIEventBarMissionGroupListSlot nkcuieventBarMissionGroupListSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIEventBarMissionGroupListSlot>() : null;
			if (nkcuieventBarMissionGroupListSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIComMissionGroupListSlot Prefab null!");
				return null;
			}
			nkcuieventBarMissionGroupListSlot.m_InstanceData = nkcassetInstanceData;
			nkcuieventBarMissionGroupListSlot.Init();
			if (parent != null)
			{
				nkcuieventBarMissionGroupListSlot.transform.SetParent(parent);
			}
			nkcuieventBarMissionGroupListSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuieventBarMissionGroupListSlot.gameObject.SetActive(false);
			return nkcuieventBarMissionGroupListSlot;
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x001D994E File Offset: 0x001D7B4E
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x001D9970 File Offset: 0x001D7B70
		private void Update()
		{
			if (this.m_objComplete == null || !this.m_objComplete.activeSelf)
			{
				return;
			}
			if (this.m_lbResetTime == null || !this.m_lbResetTime.gameObject.activeSelf)
			{
				return;
			}
			if (this.m_updateTimer > 1f)
			{
				NKMUserData userData = NKCScenManager.CurrentUserData();
				DateTime nextResetTime = NKMTime.GetNextResetTime(this.GetLoginMissionLastUpdateDateUTC(userData), this.m_resetInterval);
				NKCUtil.SetLabelText(this.m_lbResetTime, NKCUtilString.GetRemainTimeStringOneParam(nextResetTime));
				this.m_updateTimer = 0f;
			}
			this.m_updateTimer += Time.deltaTime;
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x001D9A10 File Offset: 0x001D7C10
		private void Init()
		{
			if (this.m_RewardSlot != null)
			{
				int num = this.m_RewardSlot.Length;
				for (int i = 0; i < num; i++)
				{
					this.m_RewardSlot[i].Init();
				}
			}
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x001D9A48 File Offset: 0x001D7C48
		public void SetData(NKMMissionTemplet missionTemplet)
		{
			if (missionTemplet == null)
			{
				return;
			}
			this.m_resetInterval = missionTemplet.m_ResetInterval;
			NKCUtil.SetLabelText(this.m_lbMissionTitle, missionTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_lbMissionDesc, missionTemplet.GetDesc());
			int count = missionTemplet.m_MissionReward.Count;
			int num = this.m_RewardSlot.Length;
			for (int i = 0; i < num; i++)
			{
				if (i >= count)
				{
					NKCUtil.SetGameobjectActive(this.m_RewardSlot[i], false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_RewardSlot[i], true);
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(missionTemplet.m_MissionReward[i].reward_id, (long)missionTemplet.m_MissionReward[i].reward_value, 0);
					this.m_RewardSlot[i].SetData(data, true, null);
				}
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMMissionData nkmmissionData = null;
			if (nkmuserData != null)
			{
				nkmmissionData = NKCScenManager.CurrentUserData().m_MissionData.GetMissionData(missionTemplet);
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			bool flag = missionTabTemplet != null && missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.POINT_EXCHANGE;
			if (nkmmissionData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objComplete, false);
				NKCUtil.SetLabelText(this.m_lbProgess, string.Format("{0}/{1}", 0, missionTemplet.m_Times));
				return;
			}
			bool flag2 = nkmmissionData.mission_id > missionTemplet.m_MissionID || (nkmmissionData.mission_id == missionTemplet.m_MissionID && nkmmissionData.isComplete);
			NKCUtil.SetGameobjectActive(this.m_lbResetTime, this.m_resetInterval == NKM_MISSION_RESET_INTERVAL.DAILY || this.m_resetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY || this.m_resetInterval == NKM_MISSION_RESET_INTERVAL.MONTHLY);
			if (flag)
			{
				bool flag3 = missionTemplet.m_MissionCond.mission_cond == NKM_MISSION_COND.LOGIN_DAYS && (nkmmissionData.mission_id > missionTemplet.m_MissionID || nkmmissionData.times >= missionTemplet.m_Times);
				flag2 |= (flag && flag3);
			}
			if (flag2 && this.m_lbResetTime != null && this.m_lbResetTime.gameObject.activeSelf)
			{
				DateTime nextResetTime = NKMTime.GetNextResetTime(this.GetLoginMissionLastUpdateDateUTC(nkmuserData), missionTemplet.m_ResetInterval);
				NKCUtil.SetLabelText(this.m_lbResetTime, NKCUtilString.GetRemainTimeStringOneParam(nextResetTime));
			}
			NKCUtil.SetGameobjectActive(this.m_objComplete, flag2);
			if (flag && missionTemplet.m_MissionCond.mission_cond != NKM_MISSION_COND.LOGIN_DAYS)
			{
				NKCUtil.SetLabelText(this.m_lbProgess, string.Format("{0}/{1}", nkmmissionData.times % missionTemplet.m_Times, missionTemplet.m_Times));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbProgess, string.Format("{0}/{1}", nkmmissionData.times, missionTemplet.m_Times));
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x001D9CE8 File Offset: 0x001D7EE8
		private DateTime GetLoginMissionLastUpdateDateUTC(NKMUserData userData)
		{
			if (userData == null)
			{
				return NKCSynchronizedTime.GetServerUTCTime(0.0);
			}
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(10210);
			NKMMissionData missionData = userData.m_MissionData.GetMissionData(missionTemplet);
			if (missionData == null)
			{
				return NKCSynchronizedTime.GetServerUTCTime(0.0);
			}
			return missionData.LastUpdateDate;
		}

		// Token: 0x04004B54 RID: 19284
		public Text m_lbMissionTitle;

		// Token: 0x04004B55 RID: 19285
		public Text m_lbMissionDesc;

		// Token: 0x04004B56 RID: 19286
		public Text m_lbProgess;

		// Token: 0x04004B57 RID: 19287
		public Text m_lbResetTime;

		// Token: 0x04004B58 RID: 19288
		public NKCUISlot[] m_RewardSlot;

		// Token: 0x04004B59 RID: 19289
		public GameObject m_objComplete;

		// Token: 0x04004B5A RID: 19290
		public NKCUIComStateButton m_csbtnSlot;

		// Token: 0x04004B5B RID: 19291
		private NKM_MISSION_RESET_INTERVAL m_resetInterval;

		// Token: 0x04004B5C RID: 19292
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04004B5D RID: 19293
		private float m_updateTimer;
	}
}

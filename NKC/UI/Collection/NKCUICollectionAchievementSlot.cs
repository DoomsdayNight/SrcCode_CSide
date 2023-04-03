using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C20 RID: 3104
	public class NKCUICollectionAchievementSlot : MonoBehaviour
	{
		// Token: 0x06008FBA RID: 36794 RVA: 0x0030DA9D File Offset: 0x0030BC9D
		public void Init()
		{
			this.m_RewardSlot.Init();
		}

		// Token: 0x06008FBB RID: 36795 RVA: 0x0030DAAC File Offset: 0x0030BCAC
		public static NKCUICollectionAchievementSlot GetNewInstance(Transform parent, bool bMentoringSlot = false)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_collection", "NKM_UI_POPUP_COLLECTION_ACHIEVEMENT_SLOT", false, null);
			NKCUICollectionAchievementSlot nkcuicollectionAchievementSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUICollectionAchievementSlot>() : null;
			if (nkcuicollectionAchievementSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKM_UI_POPUP_COLLECTION_ACHIEVEMENT_SLOT Prefab null!");
				return null;
			}
			nkcuicollectionAchievementSlot.m_InstanceData = nkcassetInstanceData;
			nkcuicollectionAchievementSlot.Init();
			if (parent != null)
			{
				nkcuicollectionAchievementSlot.transform.SetParent(parent);
			}
			nkcuicollectionAchievementSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuicollectionAchievementSlot.gameObject.SetActive(false);
			return nkcuicollectionAchievementSlot;
		}

		// Token: 0x06008FBC RID: 36796 RVA: 0x0030DB46 File Offset: 0x0030BD46
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			this.m_UnitMissionStepTemplet = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008FBD RID: 36797 RVA: 0x0030DB6C File Offset: 0x0030BD6C
		public void SetData(int unitId, NKMUnitMissionStepTemplet unitMissionStepTemplet, NKMMissionManager.MissionStateData missionStateData, NKCUICollectionAchievementSlot.OnComplete onComplete = null)
		{
			this.m_UnitId = unitId;
			if (unitMissionStepTemplet == null)
			{
				return;
			}
			this.m_UnitMissionStepTemplet = unitMissionStepTemplet;
			this.m_OnComplete = onComplete;
			NKCUtil.SetLabelText(this.m_lbMissionDesc, NKCStringTable.GetString(unitMissionStepTemplet.MissionDesc, false));
			NKCUtil.SetLabelText(this.m_lbMissionCount, string.Format("{0}/{1}", missionStateData.progressCount, unitMissionStepTemplet.MissionValue));
			NKCUtil.SetGameobjectActive(this.m_objCount, missionStateData.state == NKMMissionManager.MissionState.ONGOING);
			NKCUtil.SetGameobjectActive(this.m_objClear, missionStateData.state != NKMMissionManager.MissionState.ONGOING);
			NKCUtil.SetGameobjectActive(this.m_objComplete, missionStateData.state == NKMMissionManager.MissionState.COMPLETED);
			NKCUtil.SetGameobjectActive(this.m_objClearLine, missionStateData.state == NKMMissionManager.MissionState.CAN_COMPLETE);
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(unitMissionStepTemplet.RewardInfo, 0);
			if (missionStateData.state == NKMMissionManager.MissionState.CAN_COMPLETE)
			{
				this.m_RewardSlot.SetData(data, true, new NKCUISlot.OnClick(this.OnClickSlot));
			}
			else
			{
				this.m_RewardSlot.SetData(data, true, null);
			}
			this.m_RewardSlot.SetRewardFx(missionStateData.state == NKMMissionManager.MissionState.CAN_COMPLETE);
			this.m_RewardSlot.SetCompleteMark(missionStateData.state == NKMMissionManager.MissionState.COMPLETED);
		}

		// Token: 0x06008FBE RID: 36798 RVA: 0x0030DC90 File Offset: 0x0030BE90
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (this.m_OnComplete != null)
			{
				int rewardEnableStepId = NKCUnitMissionManager.GetRewardEnableStepId(this.m_UnitId, this.m_UnitMissionStepTemplet.Owner.MissionId);
				if (rewardEnableStepId > 0)
				{
					this.m_OnComplete(this.m_UnitId, this.m_UnitMissionStepTemplet.Owner.MissionId, rewardEnableStepId);
				}
			}
		}

		// Token: 0x04007CA8 RID: 31912
		public Text m_lbMissionDesc;

		// Token: 0x04007CA9 RID: 31913
		public Text m_lbMissionCount;

		// Token: 0x04007CAA RID: 31914
		public NKCUISlot m_RewardSlot;

		// Token: 0x04007CAB RID: 31915
		public GameObject m_objCount;

		// Token: 0x04007CAC RID: 31916
		public GameObject m_objClear;

		// Token: 0x04007CAD RID: 31917
		public GameObject m_objComplete;

		// Token: 0x04007CAE RID: 31918
		public GameObject m_objClearLine;

		// Token: 0x04007CAF RID: 31919
		private int m_UnitId;

		// Token: 0x04007CB0 RID: 31920
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04007CB1 RID: 31921
		private NKMUnitMissionStepTemplet m_UnitMissionStepTemplet;

		// Token: 0x04007CB2 RID: 31922
		private NKCUICollectionAchievementSlot.OnComplete m_OnComplete;

		// Token: 0x020019E2 RID: 6626
		// (Invoke) Token: 0x0600BA75 RID: 47733
		public delegate void OnComplete(int unitId, int missionId, int stepId);
	}
}

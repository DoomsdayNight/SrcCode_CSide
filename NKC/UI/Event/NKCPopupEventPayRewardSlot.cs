using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BC9 RID: 3017
	public class NKCPopupEventPayRewardSlot : MonoBehaviour
	{
		// Token: 0x06008BAA RID: 35754 RVA: 0x002F7828 File Offset: 0x002F5A28
		public static NKCPopupEventPayRewardSlot GetNewInstance(Transform parent, string bundleName, string assetName)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCPopupEventPayRewardSlot nkcpopupEventPayRewardSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCPopupEventPayRewardSlot>() : null;
			if (nkcpopupEventPayRewardSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCPopupEventPayRewardSlot Prefab null!");
				return null;
			}
			nkcpopupEventPayRewardSlot.m_InstanceData = nkcassetInstanceData;
			nkcpopupEventPayRewardSlot.Init();
			if (parent != null)
			{
				nkcpopupEventPayRewardSlot.transform.SetParent(parent);
			}
			nkcpopupEventPayRewardSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcpopupEventPayRewardSlot.gameObject.SetActive(false);
			return nkcpopupEventPayRewardSlot;
		}

		// Token: 0x06008BAB RID: 35755 RVA: 0x002F78BA File Offset: 0x002F5ABA
		public void DestoryInstance()
		{
			this.m_dOnMissionComplete = null;
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008BAC RID: 35756 RVA: 0x002F78E0 File Offset: 0x002F5AE0
		public void Init()
		{
			NKCUISlot rewardSlot = this.m_rewardSlot;
			if (rewardSlot != null)
			{
				rewardSlot.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_slotButton, new UnityAction(this.OnClickSlot));
		}

		// Token: 0x06008BAD RID: 35757 RVA: 0x002F790C File Offset: 0x002F5B0C
		public void SetData(NKMMissionTemplet missionTemplet, float progress, NKCPopupEventPayRewardSlot.OnSetMissionState onSetMissionState, NKCPopupEventPayRewardSlot.OnMissionComplete onMissionComplete, NKCUISlot.OnClick onRewardIconClick)
		{
			this.m_dOnMissionComplete = onMissionComplete;
			this.m_dOnSetMissionState = onSetMissionState;
			NKMMissionManager.MissionState state = NKMMissionManager.GetMissionStateData(missionTemplet).state;
			bool flag = state == NKMMissionManager.MissionState.CAN_COMPLETE || state == NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE;
			bool flag2 = state == NKMMissionManager.MissionState.REPEAT_COMPLETED || state == NKMMissionManager.MissionState.COMPLETED;
			this.m_slotButton.SetLock(!flag, false);
			NKCUtil.SetGameobjectActive(this.m_objClearRoot, flag && !flag2);
			NKCUtil.SetGameobjectActive(this.m_objCompleteRoot, flag2);
			string text = null;
			if (missionTemplet != null)
			{
				text = missionTemplet.GetDesc();
				if (missionTemplet.m_MissionReward.Count > 0 && missionTemplet.m_MissionReward[0] != null)
				{
					NKCUtil.SetGameobjectActive(this.m_rewardSlot, true);
					MissionReward missionReward = missionTemplet.m_MissionReward[0];
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(missionReward.reward_type, missionReward.reward_id, missionReward.reward_value, 0);
					NKCUISlot rewardSlot = this.m_rewardSlot;
					if (rewardSlot != null)
					{
						rewardSlot.SetData(data, true, onRewardIconClick);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_rewardSlot, false);
				}
				if (missionTemplet.m_MissionCond.value1 != null && missionTemplet.m_MissionCond.value1.Count > 0)
				{
					Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(missionTemplet.m_MissionCond.value1[0]);
					NKCUtil.SetImageSprite(this.m_payIcon, orLoadMiscItemSmallIcon, false);
				}
			}
			NKCUtil.SetLabelText(this.m_missionDesc, string.IsNullOrEmpty(text) ? "" : text);
			NKCUtil.SetImageFillAmount(this.m_progress, progress);
			if (this.m_dOnSetMissionState != null)
			{
				this.m_dOnSetMissionState(flag2);
			}
		}

		// Token: 0x06008BAE RID: 35758 RVA: 0x002F7A83 File Offset: 0x002F5C83
		private void OnClickSlot()
		{
			if (this.m_dOnMissionComplete != null)
			{
				this.m_dOnMissionComplete();
			}
		}

		// Token: 0x04007874 RID: 30836
		public NKCUISlot m_rewardSlot;

		// Token: 0x04007875 RID: 30837
		public Text m_missionDesc;

		// Token: 0x04007876 RID: 30838
		public NKCUIComStateButton m_slotButton;

		// Token: 0x04007877 RID: 30839
		public GameObject m_objClearRoot;

		// Token: 0x04007878 RID: 30840
		public GameObject m_objCompleteRoot;

		// Token: 0x04007879 RID: 30841
		public Image m_progress;

		// Token: 0x0400787A RID: 30842
		public Image m_payIcon;

		// Token: 0x0400787B RID: 30843
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x0400787C RID: 30844
		private NKCPopupEventPayRewardSlot.OnMissionComplete m_dOnMissionComplete;

		// Token: 0x0400787D RID: 30845
		private NKCPopupEventPayRewardSlot.OnSetMissionState m_dOnSetMissionState;

		// Token: 0x0200199A RID: 6554
		// (Invoke) Token: 0x0600B96F RID: 47471
		public delegate void OnMissionComplete();

		// Token: 0x0200199B RID: 6555
		// (Invoke) Token: 0x0600B973 RID: 47475
		public delegate void OnSetMissionState(bool cleared);
	}
}

using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009BB RID: 2491
	public class NKCUIMissionAchieveSlotGrowth : MonoBehaviour
	{
		// Token: 0x0600699B RID: 27035 RVA: 0x00222D38 File Offset: 0x00220F38
		public NKMMissionTemplet GetNKMMissionTemplet()
		{
			return this.m_MissionTemplet;
		}

		// Token: 0x0600699C RID: 27036 RVA: 0x00222D40 File Offset: 0x00220F40
		public static NKCUIMissionAchieveSlotGrowth GetNewInstance(Transform parent, NKCUIMissionAchieveSlotGrowth.OnClickMASlot OnClickMASlotMove = null, NKCUIMissionAchieveSlotGrowth.OnClickMASlot OnClickMASlotComplete = null)
		{
			return NKCUIMissionAchieveSlotGrowth.GetNewInstance(parent, "AB_UI_NKM_UI_MISSION", "NKM_UI_MISSION_GROWTH_LIST_SLOT", OnClickMASlotMove, OnClickMASlotComplete);
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x00222D54 File Offset: 0x00220F54
		public static NKCUIMissionAchieveSlotGrowth GetNewInstance(Transform parent, string BundleName, string AssetName, NKCUIMissionAchieveSlotGrowth.OnClickMASlot OnClickMASlotMove = null, NKCUIMissionAchieveSlotGrowth.OnClickMASlot OnClickMASlotComplete = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(BundleName, AssetName, false, null);
			NKCUIMissionAchieveSlotGrowth component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIMissionAchieveSlotGrowth>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIMissionAchieveSlotGrowth Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			component.m_btnProgress.PointerClick.RemoveAllListeners();
			component.m_btnProgress.PointerClick.AddListener(new UnityAction(component.OnClickMove));
			component.m_btnComplete.PointerClick.RemoveAllListeners();
			component.m_btnComplete.PointerClick.AddListener(new UnityAction(component.OnClickComplete));
			component.m_btnDisable.PointerClick.RemoveAllListeners();
			component.m_btnDisable.PointerClick.AddListener(new UnityAction(component.OnClickMove));
			component.m_OnClickMASlotMove = OnClickMASlotMove;
			component.m_OnClickMASlotComplete = OnClickMASlotComplete;
			for (int i = 0; i < component.m_lstRewardSlot.Count; i++)
			{
				if (component.m_lstRewardSlot[i] != null)
				{
					component.m_lstRewardSlot[i].Init();
				}
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x00222EA8 File Offset: 0x002210A8
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
		}

		// Token: 0x0600699F RID: 27039 RVA: 0x00222EB5 File Offset: 0x002210B5
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x00222ED4 File Offset: 0x002210D4
		public void SetData(NKMMissionTemplet cNKMMissionTemplet)
		{
			if (cNKMMissionTemplet == null)
			{
				return;
			}
			bool flag = false;
			if (this.m_MissionTemplet == cNKMMissionTemplet)
			{
				flag = true;
			}
			this.m_MissionTemplet = cNKMMissionTemplet;
			this.m_MissionData = NKMMissionManager.GetMissionData(cNKMMissionTemplet);
			this.m_MissionUIData = NKMMissionManager.GetMissionStateData(cNKMMissionTemplet);
			NKCUtil.SetImageSprite(this.m_imgMissionThumbnail, NKCUtil.GetMissionThumbnailSprite(this.m_MissionTemplet.m_MissionTabId), false);
			NKCUtil.SetLabelText(this.m_lbMissionTitle, this.m_MissionTemplet.GetTitle());
			NKCUtil.SetLabelText(this.m_lbMissionDesc, this.m_MissionTemplet.GetDesc());
			if (!string.IsNullOrEmpty(this.m_MissionTemplet.m_MissionTip))
			{
				NKCUtil.SetLabelText(this.m_lbMissionTip, this.m_MissionTemplet.GetTip());
			}
			int num = NKMMissionManager.GetMissionTempletListByType(this.m_MissionTemplet.m_MissionTabId).FindIndex((NKMMissionTemplet x) => x == this.m_MissionTemplet);
			NKCUtil.SetLabelText(this.m_lbMissionNum, NKCUtilString.GET_STRING_MISSION_ONE_PARAM, new object[]
			{
				num + 1
			});
			if (!flag)
			{
				for (int i = 0; i < this.m_MissionTemplet.m_MissionReward.Count; i++)
				{
					MissionReward missionReward = this.m_MissionTemplet.m_MissionReward[i];
					this.m_lstRewardSlot[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(missionReward.reward_type, missionReward.reward_id, missionReward.reward_value, 0), true, null);
					this.m_lstRewardSlot[i].SetActive(true);
				}
				for (int j = this.m_MissionTemplet.m_MissionReward.Count; j < this.m_lstRewardSlot.Count; j++)
				{
					this.m_lstRewardSlot[j].SetActive(false);
				}
			}
			NKMMissionManager.CheckCanReset(this.m_MissionTemplet.m_ResetInterval, this.m_MissionData);
			bool isMissionCanClear = this.m_MissionUIData.IsMissionCanClear;
			NKCUtil.SetGameobjectActive(this.m_objMissionTip, this.m_MissionUIData.IsMissionOngoing && !string.IsNullOrEmpty(this.m_MissionTemplet.m_MissionTip));
			NKCUtil.SetGameobjectActive(this.m_objOutline, this.m_MissionUIData.IsMissionOngoing);
			NKCUtil.SetGameobjectActive(this.m_btnProgress, this.m_MissionTemplet.m_ShortCutType != NKM_SHORTCUT_TYPE.SHORTCUT_NONE && this.m_MissionUIData.IsMissionOngoing && !isMissionCanClear);
			NKCUtil.SetGameobjectActive(this.m_objComplete, this.m_MissionUIData.IsMissionCompleted);
			NKCUtil.SetGameobjectActive(this.m_btnComplete, isMissionCanClear);
			NKCUtil.SetGameobjectActive(this.m_objLock, this.m_MissionUIData.IsLocked);
			NKCUtil.SetGameobjectActive(this.m_btnDisable, this.m_MissionUIData.IsLocked);
			long progressCount = this.m_MissionUIData.progressCount;
			NKCUtil.SetLabelText(this.m_lbProgress, string.Format("{0} / {1}", Math.Min(this.m_MissionTemplet.m_Times, progressCount), this.m_MissionTemplet.m_Times));
			this.m_sliderProgress.value = (float)progressCount / (float)this.m_MissionTemplet.m_Times;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x002231C9 File Offset: 0x002213C9
		public void OnClickMove()
		{
			if (this.m_btnDisable.gameObject.activeSelf)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_MISSION_UNAVAILABLE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIMissionAchieveSlotGrowth.OnClickMASlot onClickMASlotMove = this.m_OnClickMASlotMove;
			if (onClickMASlotMove == null)
			{
				return;
			}
			onClickMASlotMove(this);
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x00223202 File Offset: 0x00221402
		public void OnClickComplete()
		{
			NKCUIMissionAchieveSlotGrowth.OnClickMASlot onClickMASlotComplete = this.m_OnClickMASlotComplete;
			if (onClickMASlotComplete == null)
			{
				return;
			}
			onClickMASlotComplete(this);
		}

		// Token: 0x04005555 RID: 21845
		public GameObject m_objComplete;

		// Token: 0x04005556 RID: 21846
		public GameObject m_objLock;

		// Token: 0x04005557 RID: 21847
		public Image m_imgMissionThumbnail;

		// Token: 0x04005558 RID: 21848
		public Text m_lbMissionNum;

		// Token: 0x04005559 RID: 21849
		public Text m_lbMissionTitle;

		// Token: 0x0400555A RID: 21850
		public Text m_lbMissionDesc;

		// Token: 0x0400555B RID: 21851
		public Text m_lbProgress;

		// Token: 0x0400555C RID: 21852
		public Slider m_sliderProgress;

		// Token: 0x0400555D RID: 21853
		public GameObject m_objMissionTip;

		// Token: 0x0400555E RID: 21854
		public Text m_lbMissionTip;

		// Token: 0x0400555F RID: 21855
		public List<NKCUISlot> m_lstRewardSlot;

		// Token: 0x04005560 RID: 21856
		public NKCUIComStateButton m_btnProgress;

		// Token: 0x04005561 RID: 21857
		public NKCUIComStateButton m_btnComplete;

		// Token: 0x04005562 RID: 21858
		public NKCUIComStateButton m_btnDisable;

		// Token: 0x04005563 RID: 21859
		public GameObject m_objOutline;

		// Token: 0x04005564 RID: 21860
		private NKMMissionTemplet m_MissionTemplet;

		// Token: 0x04005565 RID: 21861
		private NKMMissionData m_MissionData;

		// Token: 0x04005566 RID: 21862
		private NKMMissionManager.MissionStateData m_MissionUIData;

		// Token: 0x04005567 RID: 21863
		private NKCUIMissionAchieveSlotGrowth.OnClickMASlot m_OnClickMASlotMove;

		// Token: 0x04005568 RID: 21864
		private NKCUIMissionAchieveSlotGrowth.OnClickMASlot m_OnClickMASlotComplete;

		// Token: 0x04005569 RID: 21865
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x020016B9 RID: 5817
		// (Invoke) Token: 0x0600B120 RID: 45344
		public delegate void OnClickMASlot(NKCUIMissionAchieveSlotGrowth cNKCUIMissionAchieveSlotGrowth);
	}
}

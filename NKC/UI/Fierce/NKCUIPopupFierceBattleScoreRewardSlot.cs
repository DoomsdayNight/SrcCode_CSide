using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BBF RID: 3007
	public class NKCUIPopupFierceBattleScoreRewardSlot : MonoBehaviour
	{
		// Token: 0x06008ADE RID: 35550 RVA: 0x002F341C File Offset: 0x002F161C
		public static NKCUIPopupFierceBattleScoreRewardSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_TOTAL_SCORE_REWARD_SLOT", false, null);
			NKCUIPopupFierceBattleScoreRewardSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIPopupFierceBattleScoreRewardSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIPopupFierceBattleScoreRewardSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
				component.Init();
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06008ADF RID: 35551 RVA: 0x002F349C File Offset: 0x002F169C
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_BUTTON_COMPLETE, new UnityAction(this.OnClickReward));
			foreach (NKCUISlot nkcuislot in this.m_lstRewardSlot)
			{
				if (nkcuislot != null)
				{
					nkcuislot.Init();
				}
			}
		}

		// Token: 0x06008AE0 RID: 35552 RVA: 0x002F350C File Offset: 0x002F170C
		public void SetData(NKMFiercePointRewardTemplet templet, NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE type)
		{
			if (templet == null)
			{
				return;
			}
			this.m_iTargetPointRewardID = templet.FiercePointRewardID;
			string @string = NKCStringTable.GetString(templet.PointDescStrID, false);
			NKCUtil.SetLabelText(this.m_Desc, @string);
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				if (templet.Rewards.Count > i)
				{
					NKMRewardInfo nkmrewardInfo = new NKMRewardInfo();
					nkmrewardInfo.rewardType = templet.Rewards[i].RewardType;
					nkmrewardInfo.ID = templet.Rewards[i].RewardID;
					nkmrewardInfo.Count = templet.Rewards[i].RewardQuantity;
					this.m_lstRewardSlot[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo, 0), true, null);
					NKCUtil.SetGameobjectActive(this.m_lstRewardSlot[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstRewardSlot[i], false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_BUTTON_COMPLETE, type == NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE.CAN_RECEVIE);
			NKCUtil.SetGameobjectActive(this.m_COMPLETE_LINE, type == NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE.CAN_RECEVIE);
			NKCUtil.SetGameobjectActive(this.m_BUTTON_COMPLETE_GET, type == NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE.COMPLETE);
			NKCUtil.SetGameobjectActive(this.m_BUTTON_COMPLETE_DISABLE, type == NKCUIPopupFierceBattleScoreRewardSlot.POINT_REWARD_SLOT_TYPE.DISABLE);
		}

		// Token: 0x06008AE1 RID: 35553 RVA: 0x002F3633 File Offset: 0x002F1833
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008AE2 RID: 35554 RVA: 0x002F3652 File Offset: 0x002F1852
		private void OnClickReward()
		{
			NKCPacketSender.Send_NKMPacket_FIERCE_COMPLETE_POINT_REWARD_REQ(this.m_iTargetPointRewardID);
		}

		// Token: 0x04007798 RID: 30616
		public Text m_Desc;

		// Token: 0x04007799 RID: 30617
		public List<NKCUISlot> m_lstRewardSlot;

		// Token: 0x0400779A RID: 30618
		public NKCUIComStateButton m_BUTTON_COMPLETE;

		// Token: 0x0400779B RID: 30619
		public GameObject m_COMPLETE_LINE;

		// Token: 0x0400779C RID: 30620
		public GameObject m_BUTTON_COMPLETE_GET;

		// Token: 0x0400779D RID: 30621
		public GameObject m_BUTTON_COMPLETE_DISABLE;

		// Token: 0x0400779E RID: 30622
		private int m_iTargetPointRewardID;

		// Token: 0x0400779F RID: 30623
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x02001988 RID: 6536
		public enum POINT_REWARD_SLOT_TYPE
		{
			// Token: 0x0400AC43 RID: 44099
			DISABLE,
			// Token: 0x0400AC44 RID: 44100
			CAN_RECEVIE,
			// Token: 0x0400AC45 RID: 44101
			COMPLETE
		}
	}
}

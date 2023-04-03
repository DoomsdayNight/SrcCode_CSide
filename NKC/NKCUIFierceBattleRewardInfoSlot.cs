using System;
using System.Collections.Generic;
using NKC.UI;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000798 RID: 1944
	public class NKCUIFierceBattleRewardInfoSlot : MonoBehaviour
	{
		// Token: 0x06004C54 RID: 19540 RVA: 0x0016D9D4 File Offset: 0x0016BBD4
		public static NKCUIFierceBattleRewardInfoSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_REWARD_INFO_SLOT", false, null);
			NKCUIFierceBattleRewardInfoSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIFierceBattleRewardInfoSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIFierceBattleRewardInfoSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x0016DA4D File Offset: 0x0016BC4D
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x0016DA6C File Offset: 0x0016BC6C
		public void SetData(NKMFierceRankRewardTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_Desc, NKCStringTable.GetString(templet.RankDescStrID, false));
			for (int i = 0; i < this.m_lstReward.Count; i++)
			{
				if (!(this.m_lstReward[i] == null))
				{
					if (templet.Rewards.Count <= i)
					{
						NKCUtil.SetGameobjectActive(this.m_lstReward[i], false);
					}
					else if (templet.Rewards[i] == null)
					{
						NKCUtil.SetGameobjectActive(this.m_lstReward[i], false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstReward[i], true);
						this.m_lstReward[i].Init();
						NKMRewardInfo nkmrewardInfo = new NKMRewardInfo();
						nkmrewardInfo.rewardType = templet.Rewards[i].RewardType;
						nkmrewardInfo.ID = templet.Rewards[i].RewardID;
						nkmrewardInfo.Count = templet.Rewards[i].RewardQuantity;
						this.m_lstReward[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo, 0), true, null);
					}
				}
			}
		}

		// Token: 0x04003C0F RID: 15375
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04003C10 RID: 15376
		public Text m_Desc;

		// Token: 0x04003C11 RID: 15377
		public List<NKCUISlot> m_lstReward;
	}
}

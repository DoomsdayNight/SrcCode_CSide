using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B62 RID: 2914
	public class NKCPopupGauntletLGSlot : MonoBehaviour
	{
		// Token: 0x06008512 RID: 34066 RVA: 0x002CFA84 File Offset: 0x002CDC84
		public static NKCPopupGauntletLGSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_LEAGUEINFO_SLOT", false, null);
			NKCPopupGauntletLGSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCPopupGauntletLGSlot>();
			if (component == null)
			{
				Debug.LogError("NKCPopupGauntletLGSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.Init();
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06008513 RID: 34067 RVA: 0x002CFB47 File Offset: 0x002CDD47
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008514 RID: 34068 RVA: 0x002CFB68 File Offset: 0x002CDD68
		public void Init()
		{
			for (int i = 0; i < this.m_lstNKCUISlotReward.Count; i++)
			{
				this.m_lstNKCUISlotReward[i].Init();
			}
		}

		// Token: 0x06008515 RID: 34069 RVA: 0x002CFBA0 File Offset: 0x002CDDA0
		public void SetUI(bool bSeason, NKMPvpRankTemplet cNKMPvpRankTemplet, bool bMyLeague)
		{
			if (cNKMPvpRankTemplet == null)
			{
				return;
			}
			this.m_NKCUILeagueTier.SetUI(cNKMPvpRankTemplet);
			this.m_lbTier.text = cNKMPvpRankTemplet.GetLeagueName();
			this.m_lbScore.text = cNKMPvpRankTemplet.LeaguePointReq.ToString() + "+";
			int i = 0;
			if (bSeason)
			{
				if (cNKMPvpRankTemplet.RewardSeason.Count > 0)
				{
					for (int j = 0; j < cNKMPvpRankTemplet.RewardSeason.Count; j++)
					{
						if (i < this.m_lstNKCUISlotReward.Count)
						{
							NKCUtil.SetGameobjectActive(this.m_lstNKCUISlotReward[i], true);
							this.m_lstNKCUISlotReward[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(cNKMPvpRankTemplet.RewardSeason[j], 0), true, null);
						}
						i++;
					}
				}
			}
			else if (cNKMPvpRankTemplet.RewardWeekly.Count > 0)
			{
				for (int j = 0; j < cNKMPvpRankTemplet.RewardWeekly.Count; j++)
				{
					if (i < this.m_lstNKCUISlotReward.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstNKCUISlotReward[i], true);
						this.m_lstNKCUISlotReward[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(cNKMPvpRankTemplet.RewardWeekly[j], 0), true, null);
					}
					i++;
				}
			}
			while (i < this.m_lstNKCUISlotReward.Count)
			{
				NKCUtil.SetGameobjectActive(this.m_lstNKCUISlotReward[i], false);
				i++;
			}
			NKCUtil.SetGameobjectActive(this.m_objMyLeague, bMyLeague);
			NKCUtil.SetGameobjectActive(this.m_objDemoteImpossible, !cNKMPvpRankTemplet.LeagueDemote);
		}

		// Token: 0x06008516 RID: 34070 RVA: 0x002CFD20 File Offset: 0x002CDF20
		public void SetUI(bool bSeason, NKMLeaguePvpRankTemplet cNKMLeaguePvpRankTemplet, bool bMyLeague)
		{
			if (cNKMLeaguePvpRankTemplet == null)
			{
				return;
			}
			this.m_NKCUILeagueTier.SetUI(cNKMLeaguePvpRankTemplet.LeagueTierIcon, cNKMLeaguePvpRankTemplet.LeagueTierIconNumber);
			this.m_lbTier.text = NKCStringTable.GetString(cNKMLeaguePvpRankTemplet.LeagueName, false);
			this.m_lbScore.text = cNKMLeaguePvpRankTemplet.LeaguePointReq.ToString() + "+";
			int i = 0;
			if (bSeason)
			{
				if (cNKMLeaguePvpRankTemplet.RewardSeason.Count > 0)
				{
					for (int j = 0; j < cNKMLeaguePvpRankTemplet.RewardSeason.Count; j++)
					{
						if (i < this.m_lstNKCUISlotReward.Count)
						{
							NKCUtil.SetGameobjectActive(this.m_lstNKCUISlotReward[i], true);
							this.m_lstNKCUISlotReward[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(cNKMLeaguePvpRankTemplet.RewardSeason[j], 0), true, null);
						}
						i++;
					}
				}
			}
			else if (cNKMLeaguePvpRankTemplet.RewardWeekly.Count > 0)
			{
				for (int j = 0; j < cNKMLeaguePvpRankTemplet.RewardWeekly.Count; j++)
				{
					if (i < this.m_lstNKCUISlotReward.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstNKCUISlotReward[i], true);
						this.m_lstNKCUISlotReward[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(cNKMLeaguePvpRankTemplet.RewardWeekly[j], 0), true, null);
					}
					i++;
				}
			}
			while (i < this.m_lstNKCUISlotReward.Count)
			{
				NKCUtil.SetGameobjectActive(this.m_lstNKCUISlotReward[i], false);
				i++;
			}
			NKCUtil.SetGameobjectActive(this.m_objMyLeague, bMyLeague);
			NKCUtil.SetGameobjectActive(this.m_objDemoteImpossible, !cNKMLeaguePvpRankTemplet.LeagueDemote);
		}

		// Token: 0x04007183 RID: 29059
		public GameObject m_objMyLeague;

		// Token: 0x04007184 RID: 29060
		public GameObject m_objDemoteImpossible;

		// Token: 0x04007185 RID: 29061
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x04007186 RID: 29062
		public Text m_lbTier;

		// Token: 0x04007187 RID: 29063
		public Text m_lbScore;

		// Token: 0x04007188 RID: 29064
		public List<NKCUISlot> m_lstNKCUISlotReward;

		// Token: 0x04007189 RID: 29065
		private NKCAssetInstanceData m_InstanceData;
	}
}

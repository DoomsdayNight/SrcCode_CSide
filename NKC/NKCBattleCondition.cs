using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000641 RID: 1601
	public class NKCBattleCondition
	{
		// Token: 0x060031F8 RID: 12792 RVA: 0x000F82EA File Offset: 0x000F64EA
		public void Init()
		{
			this.Close();
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000F82F4 File Offset: 0x000F64F4
		public void Close()
		{
			for (int i = 0; i < this.m_lstNKMBattleConditionTemplet.Count; i++)
			{
				if (this.m_lstNKMBattleConditionTemplet[i] != null)
				{
					this.m_lstNKMBattleConditionTemplet[i] = null;
				}
			}
			for (int j = 0; j < this.m_lstBCObj.Count; j++)
			{
				NKCAssetResourceManager.CloseInstance(this.m_lstBCObj[j]);
				this.m_lstBCObj[j] = null;
			}
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000F8368 File Offset: 0x000F6568
		public void Load(List<int> lstBC)
		{
			this.Init();
			if (lstBC.Count <= 0)
			{
				this.m_lstNKMBattleConditionTemplet.Clear();
				return;
			}
			for (int i = 0; i < lstBC.Count; i++)
			{
				NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(lstBC[i]);
				if (templetByID == null)
				{
					return;
				}
				if (string.IsNullOrEmpty(templetByID.BattleCondMapStrID))
				{
					return;
				}
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_FX_ENV", templetByID.BattleCondMapStrID, true, null);
				if (nkcassetInstanceData != null)
				{
					this.m_lstNKMBattleConditionTemplet.Add(templetByID);
					this.m_lstBCObj.Add(nkcassetInstanceData);
				}
			}
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000F83F0 File Offset: 0x000F65F0
		public void LoadComplete()
		{
			if (this.m_lstBCObj.Count <= 0)
			{
				return;
			}
			foreach (NKCAssetInstanceData nkcassetInstanceData in this.m_lstBCObj)
			{
				if (nkcassetInstanceData != null)
				{
					if (!nkcassetInstanceData.m_Instant.activeSelf)
					{
						nkcassetInstanceData.m_Instant.SetActive(true);
					}
					nkcassetInstanceData.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUM_GAME_BATTLE_EFFECT().transform);
					nkcassetInstanceData.m_Instant.transform.localPosition = Vector3.zero;
					nkcassetInstanceData.m_Instant.transform.localScale = Vector3.one;
				}
			}
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000F84BC File Offset: 0x000F66BC
		public bool IsBC()
		{
			return this.m_lstNKMBattleConditionTemplet.Count > 0;
		}

		// Token: 0x04003103 RID: 12547
		private List<NKCAssetInstanceData> m_lstBCObj = new List<NKCAssetInstanceData>();

		// Token: 0x04003104 RID: 12548
		private List<NKMBattleConditionTemplet> m_lstNKMBattleConditionTemplet = new List<NKMBattleConditionTemplet>();
	}
}

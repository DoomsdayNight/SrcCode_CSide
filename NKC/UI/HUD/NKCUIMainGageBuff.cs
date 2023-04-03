using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC.UI.HUD
{
	// Token: 0x02000C4B RID: 3147
	public class NKCUIMainGageBuff : MonoBehaviour
	{
		// Token: 0x060092B3 RID: 37555 RVA: 0x0032136C File Offset: 0x0031F56C
		public void InitUI()
		{
			for (int i = 0; i < this.m_lstCell.Count; i++)
			{
				this.m_lstCell[i].InitUI();
			}
		}

		// Token: 0x060092B4 RID: 37556 RVA: 0x003213A0 File Offset: 0x0031F5A0
		public void SetUnit(NKCUnitClient cUnit)
		{
			int num = 0;
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in cUnit.GetUnitFrameData().m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (value != null)
				{
					if (value.m_NKMBuffTemplet == null || (value.m_BuffSyncData.m_MasterGameUnitUID == cUnit.GetUnitDataGame().m_GameUnitUID && !value.m_NKMBuffTemplet.m_AffectMe) || !value.m_NKMBuffTemplet.m_bShowBuffIcon)
					{
						continue;
					}
					if (value.m_fLifeTime == -1f || value.m_NKMBuffTemplet.m_bInfinity || value.m_BuffSyncData.m_bRangeSon)
					{
						this.GageSetBuffIconActive(num, true, (int)value.m_BuffSyncData.m_OverlapCount, value.m_NKMBuffTemplet, 1f);
					}
					else
					{
						this.GageSetBuffIconActive(num, true, (int)value.m_BuffSyncData.m_OverlapCount, value.m_NKMBuffTemplet, value.m_fLifeTime / value.GetLifeTimeMax());
					}
				}
				num++;
				if (num >= 6)
				{
					break;
				}
			}
			for (int i = num; i < 6; i++)
			{
				this.GageSetBuffIconActive(i, false, 0, null, 1f);
			}
		}

		// Token: 0x060092B5 RID: 37557 RVA: 0x003214E0 File Offset: 0x0031F6E0
		private void GageSetBuffIconActive(int index, bool bActive, int overlapCount, NKMBuffTemplet cNKMBuffTemplet = null, float fLifeTimeRate = 1f)
		{
			if (cNKMBuffTemplet != null && !cNKMBuffTemplet.m_bShowBuffIcon)
			{
				bActive = false;
			}
			NKCUtil.SetGameobjectActive(this.m_lstCell[index], bActive);
			if (bActive)
			{
				this.m_lstCell[index].SetData(cNKMBuffTemplet, fLifeTimeRate, overlapCount);
			}
		}

		// Token: 0x04007FAF RID: 32687
		public List<NKCUIMainGageBuffCell> m_lstCell;
	}
}

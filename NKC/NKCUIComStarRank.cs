using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000760 RID: 1888
	public class NKCUIComStarRank : MonoBehaviour
	{
		// Token: 0x06004B63 RID: 19299 RVA: 0x001691D8 File Offset: 0x001673D8
		public void SetStarRank(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				this.SetStarRank(0, 0, false);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				bool bTranscendence = NKMShipManager.IsMaxLimitBreak(unitData);
				this.SetStarRank(unitData.GetStarGrade(unitTempletBase), 6, bTranscendence);
				return;
			}
			bool bTranscendence2 = NKMUnitLimitBreakManager.IsMaxLimitBreak(unitData, true);
			this.SetStarRank(unitData.GetStarGrade(unitTempletBase), unitTempletBase.m_StarGradeMax, bTranscendence2);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x0016923C File Offset: 0x0016743C
		public void SetStarRank(int starRank, int maxStarRank, bool bTranscendence = false)
		{
			for (int i = 0; i < this.m_lstStarImages.Count; i++)
			{
				if (!(this.m_lstStarImages[i] == null))
				{
					Image image = this.m_lstStarImages[i];
					if (i < maxStarRank)
					{
						NKCUtil.SetGameobjectActive(image, true);
						if (i < starRank)
						{
							NKCUtil.SetImageSprite(image, bTranscendence ? this.m_spStarPurple : this.m_spStarYellow, false);
						}
						else
						{
							NKCUtil.SetImageSprite(image, this.m_spStarGray, false);
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(image, false);
					}
				}
			}
		}

		// Token: 0x040039FF RID: 14847
		public List<Image> m_lstStarImages;

		// Token: 0x04003A00 RID: 14848
		public Sprite m_spStarGray;

		// Token: 0x04003A01 RID: 14849
		public Sprite m_spStarYellow;

		// Token: 0x04003A02 RID: 14850
		public Sprite m_spStarPurple;
	}
}

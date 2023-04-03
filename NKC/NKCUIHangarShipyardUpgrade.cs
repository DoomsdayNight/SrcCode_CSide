using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009A6 RID: 2470
	public class NKCUIHangarShipyardUpgrade : MonoBehaviour
	{
		// Token: 0x060066F5 RID: 26357 RVA: 0x0021018E File Offset: 0x0020E38E
		public void Init()
		{
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x00210190 File Offset: 0x0020E390
		public void UpdateShipData(NKCUIShipInfoRepair.ShipRepairInfo shipRepairData)
		{
			if (shipRepairData == null || shipRepairData.ShipData == null)
			{
				return;
			}
			this.m_txt_CurrentMaxLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipRepairData.iCurShipMaxLevel);
			this.m_txt_NextMaxLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipRepairData.iNextShipMaxLevel);
			NKCUtil.SetStarRank(this.m_lstCurStar, shipRepairData.iCurStar, 6);
			NKCUtil.SetStarRank(this.m_lstNextStar, shipRepairData.iNextStar, 6);
			if (this.m_rtNKM_UI_SHIPYARD_Upgrade_INFO_STAR_EFFECT != null && shipRepairData.iNextStar > 0)
			{
				this.m_rtNKM_UI_SHIPYARD_Upgrade_INFO_STAR_EFFECT.localPosition = this.m_lstNextStar[shipRepairData.iNextStar - 1].GetComponent<RectTransform>().localPosition;
			}
		}

		// Token: 0x040052FA RID: 21242
		public RectTransform m_rtNKM_UI_SHIPYARD_Upgrade_INFO_STAR_EFFECT;

		// Token: 0x040052FB RID: 21243
		public Text m_txt_CurrentMaxLevel;

		// Token: 0x040052FC RID: 21244
		public Text m_txt_NextMaxLevel;

		// Token: 0x040052FD RID: 21245
		public List<GameObject> m_lstCurStar;

		// Token: 0x040052FE RID: 21246
		public List<GameObject> m_lstNextStar;
	}
}

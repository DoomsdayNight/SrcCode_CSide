using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009A4 RID: 2468
	public class NKCUIHangarShipyardLimitBreak : MonoBehaviour
	{
		// Token: 0x060066DF RID: 26335 RVA: 0x0020F5CE File Offset: 0x0020D7CE
		public void Init()
		{
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x0020F5D0 File Offset: 0x0020D7D0
		public void UpdateShipData(NKCUIShipInfoRepair.ShipRepairInfo shipRepairData)
		{
			if (shipRepairData == null || shipRepairData.ShipData == null)
			{
				return;
			}
			if (this.m_lbCurLimitBreakMaxLevel != null)
			{
				NKCUIComTextUnitLevel nkcuicomTextUnitLevel = this.m_lbCurLimitBreakMaxLevel as NKCUIComTextUnitLevel;
				if (nkcuicomTextUnitLevel != null)
				{
					nkcuicomTextUnitLevel.SetLevel(shipRepairData.ShipData, 0, Array.Empty<Text>());
					NKCUtil.SetLabelText(nkcuicomTextUnitLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, nkcuicomTextUnitLevel.text));
				}
				else
				{
					this.m_lbCurLimitBreakMaxLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipRepairData.ShipData.m_UnitLevel.ToString());
				}
			}
			NKCUtil.SetLabelText(this.m_lbNextLimitBreakMaxLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipRepairData.iNextShipMaxLevel));
			NKCUtil.SetLabelText(this.m_lbModuleUnlockText, string.Format(NKCUtilString.GET_STRING_SHIP_INFO_01_SHIPYARD_MODULE_STEP_INFO, (int)(shipRepairData.ShipData.m_LimitBreakLevel + 1)));
			this.m_StarRank.SetStarRank(shipRepairData.ShipData.GetStarGrade(), shipRepairData.ShipData.GetStarGrade(), NKMShipManager.IsMaxLimitBreak(shipRepairData.ShipData.m_UnitID, (int)(shipRepairData.ShipData.m_LimitBreakLevel + 1)));
		}

		// Token: 0x040052CC RID: 21196
		public Text m_lbCurLimitBreakMaxLevel;

		// Token: 0x040052CD RID: 21197
		public Text m_lbNextLimitBreakMaxLevel;

		// Token: 0x040052CE RID: 21198
		public Text m_lbModuleUnlockText;

		// Token: 0x040052CF RID: 21199
		public NKCUIComStarRank m_StarRank;
	}
}

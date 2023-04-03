using System;
using System.Collections.Generic;
using ClientPacket.Common;

namespace NKM
{
	// Token: 0x0200044A RID: 1098
	public static class NKMOperationPower
	{
		// Token: 0x06001DE2 RID: 7650 RVA: 0x0008E484 File Offset: 0x0008C684
		public static int Calculate(NKMUnitData ship, IEnumerable<NKMUnitData> units, long leaderUnitUID, NKMInventoryData invenData, bool bPVP = false, Dictionary<int, NKMBanData> dicNKMBanData = null, Dictionary<int, NKMUnitUpData> dicNKMUpData = null, int operatorPower = 0)
		{
			int num = 0;
			if (ship != null)
			{
				num += ship.CalculateOperationPower(null, 0, null, null);
			}
			int num2 = 0;
			if (operatorPower != 0)
			{
				num += operatorPower;
			}
			int num3 = 0;
			int num4 = 0;
			foreach (NKMUnitData nkmunitData in units)
			{
				if (nkmunitData != null)
				{
					num2++;
					num3 += nkmunitData.CalculateOperationPower(invenData, 0, null, null);
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmunitData.m_UnitID);
					if (unitStatTemplet != null)
					{
						num4 += unitStatTemplet.GetRespawnCost(bPVP, leaderUnitUID == nkmunitData.m_UnitUID, dicNKMBanData, dicNKMUpData);
					}
				}
			}
			num += num3;
			return NKMOperationPower.Calculate(num, num3, num4, num2);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0008E53C File Offset: 0x0008C73C
		public static int Calculate(int totalOperationPower, int totalUnitOperationPower, int totalSummonCost, int unitCount)
		{
			if (unitCount == 0)
			{
				return totalOperationPower;
			}
			float num = (float)totalSummonCost / (float)unitCount;
			return (int)((float)totalOperationPower + (3f - num) * 0.013f * (float)totalUnitOperationPower);
		}
	}
}

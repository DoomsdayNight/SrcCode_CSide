using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200094A RID: 2378
	public class NKCUIComStatEnhanceBar : MonoBehaviour
	{
		// Token: 0x06005EE0 RID: 24288 RVA: 0x001D741C File Offset: 0x001D561C
		public void SetData(NKMUnitData unitData, NKM_STAT_TYPE stat, int expGain)
		{
			if (this.m_StatInfoToolTip != null)
			{
				this.m_StatInfoToolTip.SetType(stat, false);
			}
			if (unitData == null)
			{
				this.SetUIData(0, 0, 1);
				return;
			}
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			if (unitStatTemplet == null)
			{
				Debug.LogError("Unit statTemplt Not Found! unitID : " + unitData.m_UnitID.ToString());
				this.SetUIData(0, 0, 1);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			int currentStat = (int)NKMUnitStatManager.CalculateStat(stat, unitStatTemplet.m_StatData, unitData.m_UnitLevel, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), null, null, 0, unitTempletBase.m_NKM_UNIT_TYPE);
			int newStat = (int)NKMUnitStatManager.CalculateStat(stat, unitStatTemplet.m_StatData, unitData.m_UnitLevel, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), null, null, 0, unitTempletBase.m_NKM_UNIT_TYPE);
			int currentMaxStat = (int)NKMUnitStatManager.GetMaxStat(stat, unitStatTemplet.m_StatData, unitData.m_UnitLevel, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), null, null, unitTempletBase.m_NKM_UNIT_TYPE);
			this.SetUIData(currentStat, newStat, currentMaxStat);
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x001D7518 File Offset: 0x001D5718
		private void SetUIData(int currentStat, int newStat, int currentMaxStat)
		{
			if (currentMaxStat == 0)
			{
				currentMaxStat = 1;
			}
			if (newStat > currentMaxStat)
			{
				newStat = currentMaxStat;
			}
			NKCUtil.SetLabelText(this.m_lbStatCurrent, currentStat.ToString());
			int num = newStat - currentStat;
			if (currentMaxStat == currentStat)
			{
				if (this.m_slStatEXPNew != null)
				{
					this.m_slStatEXPNew.value = 1f;
				}
				NKCUtil.SetGameobjectActive(this.m_objMax, true);
				NKCUtil.SetGameobjectActive(this.m_slStatEXP, false);
				NKCUtil.SetGameobjectActive(this.m_slStatEXPNew, true);
				NKCUtil.SetGameobjectActive(this.m_lbStatPlus, false);
				return;
			}
			if (num > 0)
			{
				if (this.m_slStatEXP != null)
				{
					this.m_slStatEXP.value = (float)currentStat / (float)currentMaxStat;
				}
				if (this.m_slStatEXPNew != null)
				{
					this.m_slStatEXPNew.value = (float)newStat / (float)currentMaxStat;
				}
				NKCUtil.SetGameobjectActive(this.m_objMax, false);
				NKCUtil.SetGameobjectActive(this.m_slStatEXP, true);
				NKCUtil.SetGameobjectActive(this.m_slStatEXPNew, true);
				NKCUtil.SetGameobjectActive(this.m_lbStatPlus, true);
				NKCUtil.SetLabelText(this.m_lbStatPlus, (newStat - currentStat).ToString("+0"));
				return;
			}
			if (this.m_slStatEXP != null)
			{
				this.m_slStatEXP.value = (float)currentStat / (float)currentMaxStat;
			}
			NKCUtil.SetGameobjectActive(this.m_objMax, false);
			NKCUtil.SetGameobjectActive(this.m_slStatEXP, true);
			NKCUtil.SetGameobjectActive(this.m_slStatEXPNew, false);
			NKCUtil.SetGameobjectActive(this.m_lbStatPlus, false);
		}

		// Token: 0x04004B03 RID: 19203
		public Text m_lbStatCurrent;

		// Token: 0x04004B04 RID: 19204
		public Text m_lbStatPlus;

		// Token: 0x04004B05 RID: 19205
		public Slider m_slStatEXPNew;

		// Token: 0x04004B06 RID: 19206
		public Slider m_slStatEXP;

		// Token: 0x04004B07 RID: 19207
		public GameObject m_objMax;

		// Token: 0x04004B08 RID: 19208
		public NKCComStatInfoToolTip m_StatInfoToolTip;
	}
}

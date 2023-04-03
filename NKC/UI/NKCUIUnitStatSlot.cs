using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009FE RID: 2558
	public class NKCUIUnitStatSlot : MonoBehaviour
	{
		// Token: 0x06006F8C RID: 28556 RVA: 0x0024DEE2 File Offset: 0x0024C0E2
		public void SetStat(NKM_STAT_TYPE eStatType, NKMStatData baseStatData, NKMUnitData unitData)
		{
			this.SetStat(eStatType, baseStatData.GetStatBase(eStatType), baseStatData.GetBaseBonusStat(eStatType), unitData);
		}

		// Token: 0x06006F8D RID: 28557 RVA: 0x0024DEFC File Offset: 0x0024C0FC
		private void SetStat(NKM_STAT_TYPE eStatType, float number, float modifier, NKMUnitData unitData)
		{
			bool flag = NKMUnitStatManager.IsPercentStat(eStatType);
			if (null != this.m_imgEnhanceGauge)
			{
				float enhancePercent = this.GetEnhancePercent(eStatType, unitData, 0f);
				NKCUtil.SetGameobjectActive(this.m_imgEnhanceGauge, enhancePercent < 1f);
				this.m_imgEnhanceGauge.fillAmount = enhancePercent;
				NKCUtil.SetGameobjectActive(this.m_goMaxBG, enhancePercent >= 1f);
				NKCUtil.SetGameobjectActive(this.m_lbMax, enhancePercent >= 1f);
			}
			NKCUtil.SetLabelText(this.m_lbStatName, NKCUtilString.GetStatShortName(eStatType, number + modifier));
			NKCUtil.SetLabelTextColor(this.m_lbStatBonus, NKCUtil.GetBonusColor(modifier));
			if (this.m_StatInfoToolTip != null)
			{
				this.m_StatInfoToolTip.SetType(eStatType, number + modifier < 0f);
			}
			float statPercentage = NKCUtil.GetStatPercentage(eStatType, number + modifier);
			NKCUtil.SetLabelText(this.m_lbStatPlusPercentage, "(" + statPercentage.ToString("N2") + "%)");
			NKCUtil.SetGameobjectActive(this.m_lbStatPlusPercentage.gameObject, statPercentage != 0f);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_lbStatBonus, string.Format("{0: (+#%); (-#%);''}", (int)modifier));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbStatBonus, string.Format("{0: (+#); (-#);''}", (int)modifier));
			}
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_lbStatNumber, string.Format("{0:+#%;-#%;0}", (int)number + (int)modifier));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbStatNumber, string.Format("{0:#;-#;0}", (int)number + (int)modifier));
		}

		// Token: 0x06006F8E RID: 28558 RVA: 0x0024E08C File Offset: 0x0024C28C
		private float GetEnhancePercent(NKM_STAT_TYPE eNKM_STAT_TYPE, NKMUnitData unitData, float fClampMin = 0f)
		{
			if (unitData == null)
			{
				return 0f;
			}
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			if (unitStatTemplet == null)
			{
				return 0f;
			}
			if (unitData.m_listStatEXP.Count <= (int)eNKM_STAT_TYPE)
			{
				return 0f;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			int num = (int)NKMUnitStatManager.CalculateStat(eNKM_STAT_TYPE, unitStatTemplet.m_StatData, unitData.m_UnitLevel, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), null, null, 0, unitTempletBase.m_NKM_UNIT_TYPE);
			int num2 = (int)NKMUnitStatManager.GetMaxStat(eNKM_STAT_TYPE, unitStatTemplet.m_StatData, unitData.m_UnitLevel, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), null, null, unitTempletBase.m_NKM_UNIT_TYPE);
			if (num2 <= num)
			{
				return 1f;
			}
			return Mathf.Clamp((float)num / (float)num2, fClampMin, 1f);
		}

		// Token: 0x04005B24 RID: 23332
		public Text m_lbStatName;

		// Token: 0x04005B25 RID: 23333
		public Text m_lbStatBonus;

		// Token: 0x04005B26 RID: 23334
		public Text m_lbStatNumber;

		// Token: 0x04005B27 RID: 23335
		public Image m_imgEnhanceGauge;

		// Token: 0x04005B28 RID: 23336
		public GameObject m_goMaxBG;

		// Token: 0x04005B29 RID: 23337
		public GameObject m_lbMax;

		// Token: 0x04005B2A RID: 23338
		public NKCComStatInfoToolTip m_StatInfoToolTip;

		// Token: 0x04005B2B RID: 23339
		public Text m_lbStatPlusPercentage;
	}
}

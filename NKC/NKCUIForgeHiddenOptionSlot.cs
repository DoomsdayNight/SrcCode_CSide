using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000997 RID: 2455
	public class NKCUIForgeHiddenOptionSlot : MonoBehaviour
	{
		// Token: 0x060065F0 RID: 26096 RVA: 0x00207788 File Offset: 0x00205988
		public void Init()
		{
			int siblingIndex = base.transform.GetSiblingIndex();
			if (this.m_objSocketSimbol != null)
			{
				int num = this.m_objSocketSimbol.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_objSocketSimbol[i], i <= siblingIndex);
				}
			}
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x002077D4 File Offset: 0x002059D4
		public void Lock(int requiredEnchantLevel, bool openEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objStat, openEnable);
			NKCUtil.SetGameobjectActive(this.m_objLock, !openEnable);
			NKCUtil.SetGameobjectActive(this.m_objOpenEnable, openEnable);
			if (!openEnable)
			{
				NKCUtil.SetLabelText(this.m_lbLockDesc, string.Format(NKCUtilString.GET_STRING_EQUIP_POTENTIAL_REQUIRED_ENCHANT_LV, requiredEnchantLevel));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbStatDesc, NKCUtilString.GET_STRING_EQUIP_POTENTIAL_OPEN_ENABLE);
			NKCUtil.SetLabelTextColor(this.m_lbStatDesc, this.m_openEnableTextColor);
		}

		// Token: 0x060065F2 RID: 26098 RVA: 0x00207848 File Offset: 0x00205A48
		public void Unlocked(int socketIndex, NKM_STAT_TYPE statType, float statValue, float statFactor)
		{
			NKCUtil.SetGameobjectActive(this.m_objStat, true);
			NKCUtil.SetGameobjectActive(this.m_objLock, false);
			NKCUtil.SetGameobjectActive(this.m_objOpenEnable, false);
			bool flag = statValue < 0f || statFactor < 0f;
			if (NKCUtilString.IsNameReversedIfNegative(statType) && flag)
			{
				statValue = Mathf.Abs(statValue);
				statFactor = Mathf.Abs(statFactor);
			}
			string str = (socketIndex == 0) ? NKCUtilString.GetStatShortName(statType, flag) : "";
			NKCUtil.SetLabelText(this.m_lbStatDesc, str + " " + NKCUtil.GetPotentialSocketStatText(NKMUnitStatManager.IsPercentStat(statType), statValue, statFactor));
			NKCUtil.SetLabelTextColor(this.m_lbStatDesc, this.m_openedStatTextColor);
		}

		// Token: 0x0400517C RID: 20860
		public GameObject m_objStat;

		// Token: 0x0400517D RID: 20861
		public Text m_lbStatDesc;

		// Token: 0x0400517E RID: 20862
		public GameObject m_objLock;

		// Token: 0x0400517F RID: 20863
		public Text m_lbLockDesc;

		// Token: 0x04005180 RID: 20864
		public GameObject m_objOpenEnable;

		// Token: 0x04005181 RID: 20865
		public Color m_openEnableTextColor;

		// Token: 0x04005182 RID: 20866
		public Color m_openedStatTextColor;

		// Token: 0x04005183 RID: 20867
		public GameObject[] m_objSocketSimbol;
	}
}

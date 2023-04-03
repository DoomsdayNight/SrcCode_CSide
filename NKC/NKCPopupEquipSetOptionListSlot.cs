using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007F4 RID: 2036
	public class NKCPopupEquipSetOptionListSlot : MonoBehaviour
	{
		// Token: 0x060050AC RID: 20652 RVA: 0x001864D8 File Offset: 0x001846D8
		public void SetData(int setOptionID)
		{
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(setOptionID);
			if (equipSetOptionTemplet == null)
			{
				return;
			}
			NKCUtil.SetImageSprite(this.m_SET_ICON, NKCUtil.GetSpriteEquipSetOptionIcon(equipSetOptionTemplet), false);
			NKCUtil.SetLabelText(this.m_SET_NAME, string.Format("{0} (0/{1})", NKCStringTable.GetString(equipSetOptionTemplet.m_EquipSetName, false), equipSetOptionTemplet.m_EquipSetPart));
			NKCUtil.SetGameobjectActive(this.m_LIST_01, equipSetOptionTemplet.m_StatType_1 != NKM_STAT_TYPE.NST_RANDOM);
			NKCUtil.SetGameobjectActive(this.m_LIST_02, equipSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM);
			string setOptionDescription = NKMItemManager.GetSetOptionDescription(equipSetOptionTemplet.m_StatType_1, equipSetOptionTemplet.m_StatRate_1, equipSetOptionTemplet.m_StatValue_1);
			NKCUtil.SetLabelText(this.m_STAT_TEXT_01, setOptionDescription);
			if (equipSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM)
			{
				string setOptionDescription2 = NKMItemManager.GetSetOptionDescription(equipSetOptionTemplet.m_StatType_2, equipSetOptionTemplet.m_StatRate_2, equipSetOptionTemplet.m_StatValue_2);
				NKCUtil.SetLabelText(this.m_STAT_TEXT_02, setOptionDescription2);
			}
		}

		// Token: 0x040040A4 RID: 16548
		public Image m_SET_ICON;

		// Token: 0x040040A5 RID: 16549
		public Text m_SET_NAME;

		// Token: 0x040040A6 RID: 16550
		public GameObject m_LIST_01;

		// Token: 0x040040A7 RID: 16551
		public Text m_STAT_TEXT_01;

		// Token: 0x040040A8 RID: 16552
		public GameObject m_LIST_02;

		// Token: 0x040040A9 RID: 16553
		public Text m_STAT_TEXT_02;
	}
}

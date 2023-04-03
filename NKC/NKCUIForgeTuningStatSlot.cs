using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200099A RID: 2458
	public class NKCUIForgeTuningStatSlot : MonoBehaviour
	{
		// Token: 0x06006632 RID: 26162 RVA: 0x00209EFA File Offset: 0x002080FA
		public void Clear(int type)
		{
			if (type == 1)
			{
				this.m_NKM_UI_FACTORY_TUNING_STAT_TEXT.text = NKCUtilString.GET_STRING_FORGE_TUNING_STAT_BASE;
			}
			if (type == 2)
			{
				this.m_NKM_UI_FACTORY_TUNING_STAT_TEXT.text = NKCUtilString.GET_STRING_FORGE_TUNING_STAT_RESULT;
			}
			this.m_STAT_TEXT.text = "0";
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x00209F34 File Offset: 0x00208134
		public void SetData(bool bBefore, NKMEquipItemData equipItemData, int idx = 0)
		{
			if (equipItemData == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			int statGroupID = (idx == 0) ? equipTemplet.m_StatGroupID : equipTemplet.m_StatGroupID_2;
			IReadOnlyList<NKMEquipRandomStatTemplet> equipRandomStatGroupList = NKMEquipTuningManager.GetEquipRandomStatGroupList(statGroupID);
			EQUIP_ITEM_STAT equip_ITEM_STAT = null;
			bool bPercentStat = false;
			int num = 0;
			foreach (EQUIP_ITEM_STAT equip_ITEM_STAT2 in equipItemData.m_Stat)
			{
				if (num == idx + 1)
				{
					if (equipRandomStatGroupList != null)
					{
						foreach (NKMEquipRandomStatTemplet nkmequipRandomStatTemplet in equipRandomStatGroupList)
						{
							if (nkmequipRandomStatTemplet.m_StatType == equip_ITEM_STAT2.type)
							{
								bPercentStat = NKCUIForgeTuning.IsPercentStat(nkmequipRandomStatTemplet);
							}
						}
					}
					equip_ITEM_STAT = equip_ITEM_STAT2;
					break;
				}
				num++;
			}
			if (equip_ITEM_STAT == null)
			{
				return;
			}
			if (!bBefore)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_FACTORY_TUNING_STAT_TEXT, NKCUtilString.GET_STRING_FORGE_TUNING_STAT_CHANGE);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_PRECISION_NONE, false);
				bool flag = false;
				NKMEquipTuningCandidate tuiningData = NKCScenManager.GetScenManager().GetMyUserData().GetTuiningData();
				if (tuiningData != null && tuiningData.equipUid == equipItemData.m_ItemUid)
				{
					NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(tuiningData.equipUid);
					NKM_STAT_TYPE nkm_STAT_TYPE = (idx == 0) ? tuiningData.option1 : tuiningData.option2;
					if (itemEquip != null && nkm_STAT_TYPE != NKM_STAT_TYPE.NST_RANDOM && NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID) != null)
					{
						flag = true;
						int precision = (idx == 1) ? equipItemData.m_Precision2 : equipItemData.m_Precision;
						float value = 0f;
						NKMEquipRandomStatTemplet equipRandomStat = NKMEquipTuningManager.GetEquipRandomStat(statGroupID, nkm_STAT_TYPE);
						if (equipRandomStat != null)
						{
							value = equipRandomStat.CalcResultStat(precision);
						}
						NKCUtil.SetGameobjectActive(this.m_STAT, true);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_NONE, false);
						if (NKCUIForgeTuning.IsPercentStat(equipRandomStat))
						{
							decimal num2 = new decimal(value);
							num2 = Math.Round(num2 * 1000m) / 1000m;
							NKCUtil.SetLabelText(this.m_STAT_TEXT, NKCUtilString.GetStatShortString("<color=#ffdb00>{0} {1:P1}</color>", nkm_STAT_TYPE, num2));
						}
						else
						{
							NKCUtil.SetLabelText(this.m_STAT_TEXT, NKCUtilString.GetStatShortString("<color=#ffdb00>{0} {1:+#;-#;''}</color>", nkm_STAT_TYPE, value));
						}
					}
				}
				if (!flag)
				{
					NKCUtil.SetGameobjectActive(this.m_STAT, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_NONE, true);
				}
				return;
			}
			NKCUtil.SetLabelText(this.m_STAT_TEXT, NKCUIForgeTuning.GetTuningOptionStatString(equip_ITEM_STAT, equipItemData, bPercentStat));
			NKCUtil.SetLabelText(this.m_NKM_UI_FACTORY_TUNING_STAT_TEXT, NKCUtilString.GET_STRING_FORGE_TUNING_STAT_CURRENT);
			if (equipItemData.m_Stat.Count <= 1)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_NONE, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_PRECISION_NONE, false);
				NKCUtil.SetGameobjectActive(this.m_STAT, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_NONE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_PRECISION_NONE, false);
			NKCUtil.SetGameobjectActive(this.m_STAT, true);
		}

		// Token: 0x040051E8 RID: 20968
		public Text m_NKM_UI_FACTORY_TUNING_STAT_TEXT;

		// Token: 0x040051E9 RID: 20969
		public GameObject m_STAT;

		// Token: 0x040051EA RID: 20970
		public Text m_STAT_TEXT;

		// Token: 0x040051EB RID: 20971
		public GameObject m_NKM_UI_FACTORY_TUNING_PRECISION_NONE;

		// Token: 0x040051EC RID: 20972
		public GameObject m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_NONE;
	}
}

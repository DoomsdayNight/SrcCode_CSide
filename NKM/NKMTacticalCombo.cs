using System;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000479 RID: 1145
	public class NKMTacticalCombo
	{
		// Token: 0x06001F25 RID: 7973 RVA: 0x00093E84 File Offset: 0x00092084
		public bool CheckCond(NKMUnitTempletBase cUnitTempletBase, int respawnCost)
		{
			switch (this.m_NKM_TACTICAL_COMBO_TYPE)
			{
			case NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_ROLE_TYPE:
				return cUnitTempletBase != null && this.m_NKM_UNIT_ROLE_TYPE == cUnitTempletBase.m_NKM_UNIT_ROLE_TYPE;
			case NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STYLE_TYPE:
				return cUnitTempletBase != null && cUnitTempletBase.HasUnitStyleType(this.m_NKM_UNIT_STYLE_TYPE);
			case NKM_TACTICAL_COMBO_TYPE.NTCBT_RESPAWN_COST:
				return this.m_ValueInt == respawnCost;
			case NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STR_ID:
				return cUnitTempletBase != null && this.m_Value == cUnitTempletBase.m_UnitStrID;
			default:
				return false;
			}
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00093EFC File Offset: 0x000920FC
		public bool Load(string comboValue)
		{
			if (string.IsNullOrWhiteSpace(comboValue))
			{
				return false;
			}
			if (int.TryParse(comboValue, out this.m_ValueInt))
			{
				this.m_NKM_TACTICAL_COMBO_TYPE = NKM_TACTICAL_COMBO_TYPE.NTCBT_RESPAWN_COST;
				return true;
			}
			if (Enum.TryParse<NKM_UNIT_ROLE_TYPE>(comboValue, true, out this.m_NKM_UNIT_ROLE_TYPE))
			{
				this.m_NKM_TACTICAL_COMBO_TYPE = NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_ROLE_TYPE;
				return true;
			}
			if (Enum.TryParse<NKM_UNIT_STYLE_TYPE>(comboValue, true, out this.m_NKM_UNIT_STYLE_TYPE))
			{
				this.m_NKM_TACTICAL_COMBO_TYPE = NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STYLE_TYPE;
				return true;
			}
			if (NKMUnitManager.GetUnitTempletBase(comboValue) != null)
			{
				this.m_NKM_TACTICAL_COMBO_TYPE = NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_STR_ID;
				this.m_Value = comboValue;
				return true;
			}
			Log.ErrorAndExit("[NKMTacticalCommandTemplet] comboValue is bad, comboValue is " + comboValue, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 173);
			return false;
		}

		// Token: 0x04001FA5 RID: 8101
		public NKM_TACTICAL_COMBO_TYPE m_NKM_TACTICAL_COMBO_TYPE = NKM_TACTICAL_COMBO_TYPE.NTCBT_UNIT_ROLE_TYPE;

		// Token: 0x04001FA6 RID: 8102
		public string m_Value = "";

		// Token: 0x04001FA7 RID: 8103
		public NKM_UNIT_ROLE_TYPE m_NKM_UNIT_ROLE_TYPE;

		// Token: 0x04001FA8 RID: 8104
		public NKM_UNIT_STYLE_TYPE m_NKM_UNIT_STYLE_TYPE;

		// Token: 0x04001FA9 RID: 8105
		public int m_ValueInt;
	}
}

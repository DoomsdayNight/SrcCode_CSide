using System;
using NKM;
using NKM.Templet;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200077A RID: 1914
	public class NKCUIDevConsoleCheatUnit : NKCUIDevConsoleContentBase
	{
		// Token: 0x06004C11 RID: 19473 RVA: 0x0016C220 File Offset: 0x0016A420
		private bool CheckUnitListFilter(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return false;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase == null)
			{
				return false;
			}
			if (!unitTempletBase.m_bContractable && unitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return false;
			}
			if (this.m_ifSearch != null && !string.IsNullOrEmpty(this.m_ifSearch.text))
			{
				string text = this.m_ifSearch.text;
				bool flag = false;
				int num;
				if (int.TryParse(text, out num) && unitTempletBase.m_UnitID == num)
				{
					flag = true;
				}
				if (unitTempletBase.m_UnitStrID.ToLower().Contains(text.ToLower()) || unitTempletBase.GetUnitName().Contains(text))
				{
					flag = true;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003AF8 RID: 15096
		public Text m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_NAME_LABEL;

		// Token: 0x04003AF9 RID: 15097
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_SEARCH_BUTTON;

		// Token: 0x04003AFA RID: 15098
		public InputField m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_LEVEL_INPUT_FIELD;

		// Token: 0x04003AFB RID: 15099
		public Dropdown m_m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_LEVEL_DROP_DOWN;

		// Token: 0x04003AFC RID: 15100
		public InputField m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_LOYALTY_INPUT_FIELD;

		// Token: 0x04003AFD RID: 15101
		public InputField m_ifSearch;

		// Token: 0x04003AFE RID: 15102
		public NKCUIComToggle m_tgPermanentContract;

		// Token: 0x04003AFF RID: 15103
		public NKCUIComStateButton m_btnAdd;

		// Token: 0x04003B00 RID: 15104
		public NKCUIComStateButton m_btnAddEnhanced;

		// Token: 0x04003B01 RID: 15105
		public NKCUIComStateButton m_btnAllAdd;

		// Token: 0x04003B02 RID: 15106
		public NKCUIComStateButton m_btnAllAddEnhanced;

		// Token: 0x04003B03 RID: 15107
		public NKCUIComStateButton m_btnAllAddOperator;

		// Token: 0x04003B04 RID: 15108
		public Dropdown m_ddSkillNormal;

		// Token: 0x04003B05 RID: 15109
		public Dropdown m_ddSkillPassive;

		// Token: 0x04003B06 RID: 15110
		public Dropdown m_ddSkillSpecial;

		// Token: 0x04003B07 RID: 15111
		public Dropdown m_ddSkillUltimate;

		// Token: 0x04003B08 RID: 15112
		public Dropdown m_ddSkillLeader;

		// Token: 0x04003B09 RID: 15113
		public Dropdown m_ddTacticUpdate;

		// Token: 0x04003B0A RID: 15114
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_ADD_BUTTON;

		// Token: 0x04003B0B RID: 15115
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_ADD_ENHANCED_BUTTON;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200077D RID: 1917
	public class NKCUIDevConsoleCheatWorldmap : NKCUIDevConsoleContentBase
	{
		// Token: 0x04003B17 RID: 15127
		public List<NKCUIDevConsoleCheatWorldmap.CityMenu> m_cityMenuList;

		// Token: 0x04003B18 RID: 15128
		public GameObject m_objCityInfo;

		// Token: 0x04003B19 RID: 15129
		public Text m_txtName;

		// Token: 0x04003B1A RID: 15130
		public Text m_txtLeader;

		// Token: 0x04003B1B RID: 15131
		public Text m_txtMission;

		// Token: 0x04003B1C RID: 15132
		public NKCUIComStateButton m_btnMission;

		// Token: 0x04003B1D RID: 15133
		public Text m_txtEvent;

		// Token: 0x04003B1E RID: 15134
		public NKCUIComStateButton m_btnEvent;

		// Token: 0x04003B1F RID: 15135
		public Text m_txtBuildingPoint;

		// Token: 0x04003B20 RID: 15136
		public InputField m_ifTargetBuilding;

		// Token: 0x04003B21 RID: 15137
		public InputField m_ifTargetEvent;

		// Token: 0x04003B22 RID: 15138
		public NKCUIComStateButton m_btnTargetBuild;

		// Token: 0x04003B23 RID: 15139
		public NKCUIComStateButton m_btnTargetUpgrade;

		// Token: 0x04003B24 RID: 15140
		public NKCUIComStateButton m_btnTargetRemove;

		// Token: 0x04003B25 RID: 15141
		public Dropdown m_ddDungeonType;

		// Token: 0x04003B26 RID: 15142
		public Dropdown m_ddDungeonRank;

		// Token: 0x04003B27 RID: 15143
		public NKCUIComStateButton m_btnDungeonCreate;

		// Token: 0x04003B28 RID: 15144
		public NKCUIComStateButton m_btnDungeonCreateByID;

		// Token: 0x04003B29 RID: 15145
		public NKCUIComStateButton m_btnDungeonExpire;

		// Token: 0x04003B2A RID: 15146
		public NKCUIComStateButton m_btnLvUp;

		// Token: 0x04003B2B RID: 15147
		public NKCUIComStateButton m_btnLvDown;

		// Token: 0x0200144D RID: 5197
		[Serializable]
		public class CityMenu
		{
			// Token: 0x04009E0A RID: 40458
			public int CityID;

			// Token: 0x04009E0B RID: 40459
			public NKCUIComStateButton CityButton;

			// Token: 0x04009E0C RID: 40460
			public Text CityName;
		}
	}
}

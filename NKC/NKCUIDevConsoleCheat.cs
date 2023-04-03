using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000776 RID: 1910
	public class NKCUIDevConsoleCheat : NKCUIDevConsoleContentBase
	{
		// Token: 0x04003A9D RID: 15005
		private NKCUIDevConsoleContentBase[] m_Contents = new NKCUIDevConsoleContentBase[5];

		// Token: 0x04003A9E RID: 15006
		[Header("button")]
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_WIN_BUTTON;

		// Token: 0x04003A9F RID: 15007
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_LOSE_BUTTON;

		// Token: 0x04003AA0 RID: 15008
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_USER_INFO_BUTTON;

		// Token: 0x04003AA1 RID: 15009
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT_BUTTON;

		// Token: 0x04003AA2 RID: 15010
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_ITEM_BUTTON;

		// Token: 0x04003AA3 RID: 15011
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_WORLDMAP_BUTTON;

		// Token: 0x04003AA4 RID: 15012
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_GUILD_BUTTON;

		// Token: 0x04003AA5 RID: 15013
		[Header("button more")]
		public GameObject m_BUTTON_MORE;

		// Token: 0x04003AA6 RID: 15014
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_MOO_JUCK;

		// Token: 0x04003AA7 RID: 15015
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_ACCOUNT_RESET;

		// Token: 0x04003AA8 RID: 15016
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_WARFARE_EXPIRED;

		// Token: 0x04003AA9 RID: 15017
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_DIVE_EXPIRED;

		// Token: 0x04003AAA RID: 15018
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_PURCHASE_RESET;

		// Token: 0x04003AAB RID: 15019
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_INVENTORY_RESET;

		// Token: 0x04003AAC RID: 15020
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_PLZ_GAUNTLET_POINT;

		// Token: 0x04003AAD RID: 15021
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_PVP_REWARD_WEEK;

		// Token: 0x04003AAE RID: 15022
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_PVP_REWARD_SEASON;

		// Token: 0x04003AAF RID: 15023
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_WARFARE_UNBREAKABLE_MODE;

		// Token: 0x04003AB0 RID: 15024
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_MARQUEE_MESSAGE;

		// Token: 0x04003AB1 RID: 15025
		[Header("PVP")]
		public InputField m_NKM_UI_DEV_CONSLOE_CHEAT_PVP_SCORE_INPUT_FIELD;

		// Token: 0x04003AB2 RID: 15026
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_PVP_SCORE_BUTTON;

		// Token: 0x04003AB3 RID: 15027
		public NKCUIComStateButton m_btnPvpAsyncScoreButton;

		// Token: 0x04003AB4 RID: 15028
		public NKCUIComStateButton m_btnPvpLeagueScoreButton;

		// Token: 0x04003AB5 RID: 15029
		[Header("emoticon")]
		public InputField m_NKM_UI_DEV_CONSLOE_CHEAT_EMOTICON_ID_INPUT_FIELD;

		// Token: 0x04003AB6 RID: 15030
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_EMOTICON_ADD_BUTTON;

		// Token: 0x04003AB7 RID: 15031
		public NKCUIComStateButton m_NKM_UI_DEV_CONSLOE_CHEAT_EMOTICON_LIST_BUTTON;

		// Token: 0x04003AB8 RID: 15032
		[Space]
		public NKCUIComToggle m_tgSkipEffect;

		// Token: 0x04003AB9 RID: 15033
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_DISCONNECT_TEST;

		// Token: 0x04003ABA RID: 15034
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CONTRACT_EFFECT;

		// Token: 0x04003ABB RID: 15035
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_AWAKEN_CONTRACT_EFFECT;

		// Token: 0x04003ABC RID: 15036
		[Header("reset")]
		public NKCUIComStateButton m_btnResetUnit;

		// Token: 0x04003ABD RID: 15037
		public NKCUIComStateButton m_btnResetShip;

		// Token: 0x04003ABE RID: 15038
		public NKCUIComStateButton m_btnResetMisc;

		// Token: 0x04003ABF RID: 15039
		public NKCUIComStateButton m_btnResetEquip;

		// Token: 0x04003AC0 RID: 15040
		[Header("Contents")]
		public NKCUIDevConsoleContentBase m_NKM_UI_DEV_CONSOLE_CHEAT_USER_INFO;

		// Token: 0x04003AC1 RID: 15041
		public NKCUIDevConsoleContentBase m_NKM_UI_DEV_CONSOLE_CHEAT_UNIT;

		// Token: 0x04003AC2 RID: 15042
		public NKCUIDevConsoleContentBase m_NKM_UI_DEV_CONSOLE_CHEAT_ITEM;

		// Token: 0x04003AC3 RID: 15043
		public NKCUIDevConsoleContentBase m_NKM_UI_DEV_CONSOLE_CHEAT_WORLDMAP;

		// Token: 0x04003AC4 RID: 15044
		public NKCUIDevConsoleContentBase m_NKM_UI_DEV_CONSOLE_CHEAT_GUILD;

		// Token: 0x04003AC5 RID: 15045
		public Text m_NKM_UI_DEV_CONSOLE_CHEAT_MOO_JUCK_text;

		// Token: 0x04003AC6 RID: 15046
		public Text m_NKM_UI_DEV_CONSOLE_CHEAT_WARFARE_UNBREAKABLE_MODE_text;

		// Token: 0x04003AC7 RID: 15047
		[Header("KILLCOUNT")]
		public NKCUIComStateButton m_USER_KILLCOUNT_RESET;

		// Token: 0x04003AC8 RID: 15048
		public NKCUIComStateButton m_SERVER_KILLCOUNT_RESET;

		// Token: 0x04003AC9 RID: 15049
		public NKCUIComStateButton m_KILLCOUNT_REWARD_RESET;

		// Token: 0x04003ACA RID: 15050
		public InputField m_USER_KILLCOUNT;

		// Token: 0x04003ACB RID: 15051
		public InputField m_SERVER_KILLCOUNT;

		// Token: 0x04003ACC RID: 15052
		public InputField m_USER_REWARD_STEP;

		// Token: 0x04003ACD RID: 15053
		public InputField m_SERVER_REWARD_STEP;

		// Token: 0x04003ACE RID: 15054
		public InputField m_KILLCOUNT_ID;

		// Token: 0x04003ACF RID: 15055
		public NKCUIComStateButton m_USER_KILLCOUNT_APPLY;

		// Token: 0x04003AD0 RID: 15056
		public NKCUIComStateButton m_SERVER_KILLCOUNT_APPLY;

		// Token: 0x04003AD1 RID: 15057
		public NKCUIComStateButton m_KILLCOUNT_REWARD_STEP_APPLY;

		// Token: 0x04003AD2 RID: 15058
		[Header("Etc")]
		public Text m_NKM_UI_DEV_CONTETNS_TAG;

		// Token: 0x04003AD3 RID: 15059
		public NKCUIComStateButton m_btnShopBuyAll;

		// Token: 0x04003AD4 RID: 15060
		[Header("Raid Point")]
		public NKCUIComStateButton m_RaidPointChange;

		// Token: 0x04003AD5 RID: 15061
		public InputField m_TargetPoint;

		// Token: 0x04003AD6 RID: 15062
		public NKCUIComToggle m_ResetRewardPoint;

		// Token: 0x04003AD7 RID: 15063
		private bool isMooJuckMode;

		// Token: 0x04003AD8 RID: 15064
		private bool isWarfareUnbreakableMode;

		// Token: 0x02001449 RID: 5193
		private enum DevConsoleCheatGroup
		{
			// Token: 0x04009DF9 RID: 40441
			None = -1,
			// Token: 0x04009DFA RID: 40442
			UserInfo,
			// Token: 0x04009DFB RID: 40443
			Unit,
			// Token: 0x04009DFC RID: 40444
			Item,
			// Token: 0x04009DFD RID: 40445
			WorldMap,
			// Token: 0x04009DFE RID: 40446
			Guild,
			// Token: 0x04009DFF RID: 40447
			Max
		}
	}
}

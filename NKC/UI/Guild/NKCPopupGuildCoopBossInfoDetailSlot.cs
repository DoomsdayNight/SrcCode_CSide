using System;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B2E RID: 2862
	public class NKCPopupGuildCoopBossInfoDetailSlot : MonoBehaviour
	{
		// Token: 0x06008251 RID: 33361 RVA: 0x002BF3AC File Offset: 0x002BD5AC
		public void SetData(GuildRaidTemplet raidTemplet, NKMDungeonTempletBase templetBase)
		{
			NKCUtil.SetLabelText(this.m_lbStep, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_RAID_UI_LEVEL_INFO, raidTemplet.GetStageIndex()));
			NKCUtil.SetLabelText(this.m_lbHP, NKMDungeonManager.GetBossHp(raidTemplet.GetStageId(), templetBase.m_DungeonLevel).ToString("N0"));
			NKCUtil.SetLabelText(this.m_lbBattleCondition, (templetBase.BattleCondition != null) ? templetBase.BattleCondition.BattleCondDesc_Translated : NKCUtilString.GET_STRING_NO_EXIST);
			NKCUtil.SetLabelText(this.m_lbKillPoint, raidTemplet.GetRewardPoint().ToString("N0"));
		}

		// Token: 0x04006E7B RID: 28283
		public Text m_lbStep;

		// Token: 0x04006E7C RID: 28284
		public Text m_lbHP;

		// Token: 0x04006E7D RID: 28285
		public Text m_lbBattleCondition;

		// Token: 0x04006E7E RID: 28286
		public Text m_lbKillPoint;
	}
}

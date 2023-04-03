using System;
using NKC.UI.Result;
using NKM;

namespace NKC
{
	// Token: 0x0200070A RID: 1802
	public class NKC_SCEN_DUNGEON_RESULT : NKC_SCEN_BASIC
	{
		// Token: 0x060046C7 RID: 18119 RVA: 0x00157D0B File Offset: 0x00155F0B
		public NKC_SCEN_DUNGEON_RESULT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_DUNGEON_RESULT;
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x00157D1B File Offset: 0x00155F1B
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			NKCUIWarfareResult instance = NKCUIWarfareResult.Instance;
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x00157D29 File Offset: 0x00155F29
		public void SetDummyLeader(int unitID, int skinID)
		{
			this.m_dummyLeaderID = unitID;
			this.m_dummyLeaderSkinID = skinID;
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x00157D39 File Offset: 0x00155F39
		public void SetData(NKCUIResult.BattleResultData battleResultData, long leaderUnitUID, long leaderShipUID)
		{
			this.m_battleResultData = battleResultData;
			this.m_leaderUnitUID = leaderUnitUID;
			this.m_leaderShipUID = leaderShipUID;
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x00157D50 File Offset: 0x00155F50
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIWarfareResult.Instance.OpenForDungeon(this.m_battleResultData, this.m_leaderUnitUID, this.m_leaderShipUID, this.m_dummyLeaderID, this.m_dummyLeaderSkinID, new NKCUIWarfareResult.CallBackWhenClosed(this.OnCloseResultUI));
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x00157D8C File Offset: 0x00155F8C
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIWarfareResult.CheckInstanceAndClose();
			this.m_battleResultData = null;
			this.m_leaderUnitUID = 0L;
			this.m_leaderShipUID = 0L;
			this.m_dummyLeaderID = 0;
			this.m_dummyLeaderSkinID = 0;
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x00157DBE File Offset: 0x00155FBE
		private void OnCloseResultUI(NKM_SCEN_ID scenID)
		{
			NKCScenManager.GetScenManager().ScenChangeFade(scenID, true);
		}

		// Token: 0x040037AC RID: 14252
		private NKCUIResult.BattleResultData m_battleResultData;

		// Token: 0x040037AD RID: 14253
		private long m_leaderUnitUID;

		// Token: 0x040037AE RID: 14254
		private long m_leaderShipUID;

		// Token: 0x040037AF RID: 14255
		private int m_dummyLeaderID;

		// Token: 0x040037B0 RID: 14256
		private int m_dummyLeaderSkinID;
	}
}

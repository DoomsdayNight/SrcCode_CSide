using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000414 RID: 1044
	public sealed class NKMGameResultData : ISerializable
	{
		// Token: 0x06001B56 RID: 6998 RVA: 0x00077DA4 File Offset: 0x00075FA4
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMShipResultData>(ref this.m_dicShipResult);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_WinTeam);
			stream.PutOrGet(ref this.m_GamePlayTime);
			stream.PutOrGet(ref this.m_TotalDamage);
			stream.PutOrGet(ref this.m_BossRemainHp);
			stream.PutOrGet(ref this.m_BossInitHp);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00077DFC File Offset: 0x00075FFC
		public float GetShipResultHpByTeamData(NKMGameTeamData teamData)
		{
			float num = 0f;
			if (teamData.m_MainShip != null)
			{
				for (int i = 0; i < teamData.m_MainShip.m_listGameUnitUID.Count; i++)
				{
					short key = teamData.m_MainShip.m_listGameUnitUID[i];
					NKMShipResultData nkmshipResultData;
					if (this.m_dicShipResult.TryGetValue(key, out nkmshipResultData))
					{
						num += nkmshipResultData.m_fHP;
					}
				}
			}
			return num;
		}

		// Token: 0x04001B1D RID: 6941
		public Dictionary<short, NKMShipResultData> m_dicShipResult = new Dictionary<short, NKMShipResultData>();

		// Token: 0x04001B1E RID: 6942
		public NKM_TEAM_TYPE m_WinTeam;

		// Token: 0x04001B1F RID: 6943
		public float m_GamePlayTime;

		// Token: 0x04001B20 RID: 6944
		public float m_TotalDamage;

		// Token: 0x04001B21 RID: 6945
		public float m_BossRemainHp;

		// Token: 0x04001B22 RID: 6946
		public float m_BossInitHp;
	}
}

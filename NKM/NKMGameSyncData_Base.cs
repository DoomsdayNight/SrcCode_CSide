using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200040D RID: 1037
	public class NKMGameSyncData_Base : ISerializable
	{
		// Token: 0x06001B27 RID: 6951 RVA: 0x00077278 File Offset: 0x00075478
		public void Serialize(IPacketStream stream)
		{
			stream.AsHalf(ref this.m_fGameTime);
			stream.AsHalf(ref this.m_fRemainGameTime);
			stream.AsHalf(ref this.m_fShipDamage);
			stream.AsHalf(ref this.m_fRespawnCostA1);
			stream.AsHalf(ref this.m_fRespawnCostB1);
			stream.AsHalf(ref this.m_fRespawnCostAssistA1);
			stream.AsHalf(ref this.m_fRespawnCostAssistB1);
			stream.AsHalf(ref this.m_fUsedRespawnCostA1);
			stream.AsHalf(ref this.m_fUsedRespawnCostB1);
			stream.PutOrGetEnum<NKM_GAME_SPEED_TYPE>(ref this.m_NKM_GAME_SPEED_TYPE);
			stream.PutOrGetEnum<NKM_GAME_AUTO_SKILL_TYPE>(ref this.m_NKM_GAME_AUTO_SKILL_TYPE_A);
			stream.PutOrGetEnum<NKM_GAME_AUTO_SKILL_TYPE>(ref this.m_NKM_GAME_AUTO_SKILL_TYPE_B);
			stream.PutOrGet<NKMGameSyncData_DieUnit>(ref this.m_NKMGameSyncData_DieUnit);
			stream.PutOrGet<NKMGameSyncData_Unit>(ref this.m_NKMGameSyncData_Unit);
			stream.PutOrGet<NKMGameSyncDataSimple_Unit>(ref this.m_NKMGameSyncDataSimple_Unit);
			stream.PutOrGet<NKMGameSyncData_ShipSkill>(ref this.m_NKMGameSyncData_ShipSkill);
			stream.PutOrGet<NKMGameSyncData_Deck>(ref this.m_NKMGameSyncData_Deck);
			stream.PutOrGet<NKMGameSyncData_DeckAssist>(ref this.m_NKMGameSyncData_DeckAssist);
			stream.PutOrGet<NKMGameSyncData_GameState>(ref this.m_NKMGameSyncData_GameState);
			stream.PutOrGet<NKMGameSyncData_DungeonEvent>(ref this.m_NKMGameSyncData_DungeonEvent);
			stream.PutOrGet<NKMGameSyncData_GameEvent>(ref this.m_NKMGameSyncData_GameEvent);
			stream.PutOrGet<NKMGameSyncData_Fierce>(ref this.m_NKMGameSyncData_Fierce);
			stream.PutOrGet<NKMGameSyncData_Trim>(ref this.m_NKMGameSyncData_Trim);
		}

		// Token: 0x04001ADB RID: 6875
		public bool IsRollbackPacket;

		// Token: 0x04001ADC RID: 6876
		public float m_fGameTime;

		// Token: 0x04001ADD RID: 6877
		public float m_fAbsoluteGameTime;

		// Token: 0x04001ADE RID: 6878
		public float m_fRemainGameTime;

		// Token: 0x04001ADF RID: 6879
		public float m_fShipDamage;

		// Token: 0x04001AE0 RID: 6880
		public float m_fRespawnCostA1;

		// Token: 0x04001AE1 RID: 6881
		public float m_fRespawnCostB1;

		// Token: 0x04001AE2 RID: 6882
		public float m_fRespawnCostAssistA1;

		// Token: 0x04001AE3 RID: 6883
		public float m_fRespawnCostAssistB1;

		// Token: 0x04001AE4 RID: 6884
		public float m_fUsedRespawnCostA1;

		// Token: 0x04001AE5 RID: 6885
		public float m_fUsedRespawnCostB1;

		// Token: 0x04001AE6 RID: 6886
		public NKM_GAME_SPEED_TYPE m_NKM_GAME_SPEED_TYPE;

		// Token: 0x04001AE7 RID: 6887
		public NKM_GAME_AUTO_SKILL_TYPE m_NKM_GAME_AUTO_SKILL_TYPE_A;

		// Token: 0x04001AE8 RID: 6888
		public NKM_GAME_AUTO_SKILL_TYPE m_NKM_GAME_AUTO_SKILL_TYPE_B;

		// Token: 0x04001AE9 RID: 6889
		public List<NKMGameSyncData_DieUnit> m_NKMGameSyncData_DieUnit = new List<NKMGameSyncData_DieUnit>();

		// Token: 0x04001AEA RID: 6890
		public List<NKMGameSyncData_Unit> m_NKMGameSyncData_Unit = new List<NKMGameSyncData_Unit>();

		// Token: 0x04001AEB RID: 6891
		public List<NKMGameSyncDataSimple_Unit> m_NKMGameSyncDataSimple_Unit = new List<NKMGameSyncDataSimple_Unit>();

		// Token: 0x04001AEC RID: 6892
		public List<NKMGameSyncData_ShipSkill> m_NKMGameSyncData_ShipSkill = new List<NKMGameSyncData_ShipSkill>();

		// Token: 0x04001AED RID: 6893
		public List<NKMGameSyncData_Deck> m_NKMGameSyncData_Deck = new List<NKMGameSyncData_Deck>();

		// Token: 0x04001AEE RID: 6894
		public List<NKMGameSyncData_DeckAssist> m_NKMGameSyncData_DeckAssist = new List<NKMGameSyncData_DeckAssist>();

		// Token: 0x04001AEF RID: 6895
		public List<NKMGameSyncData_GameState> m_NKMGameSyncData_GameState = new List<NKMGameSyncData_GameState>();

		// Token: 0x04001AF0 RID: 6896
		public List<NKMGameSyncData_DungeonEvent> m_NKMGameSyncData_DungeonEvent = new List<NKMGameSyncData_DungeonEvent>();

		// Token: 0x04001AF1 RID: 6897
		public List<NKMGameSyncData_GameEvent> m_NKMGameSyncData_GameEvent = new List<NKMGameSyncData_GameEvent>();

		// Token: 0x04001AF2 RID: 6898
		public NKMGameSyncData_Fierce m_NKMGameSyncData_Fierce;

		// Token: 0x04001AF3 RID: 6899
		public NKMGameSyncData_Trim m_NKMGameSyncData_Trim;
	}
}

using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000402 RID: 1026
	public class NKMGameSyncData_Deck : ISerializable
	{
		// Token: 0x06001B11 RID: 6929 RVA: 0x00076F7C File Offset: 0x0007517C
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_NKM_TEAM_TYPE);
			stream.PutOrGet(ref this.m_UnitDeckIndex);
			stream.PutOrGet(ref this.m_UnitDeckUID);
			stream.PutOrGet(ref this.m_DeckUsedAddUnitUID);
			stream.PutOrGet(ref this.m_DeckUsedRemoveIndex);
			stream.PutOrGet(ref this.m_DeckTombAddUnitUID);
			stream.PutOrGet(ref this.m_AutoRespawnIndex);
			stream.PutOrGet(ref this.m_NextDeckUnitUID);
		}

		// Token: 0x04001AB6 RID: 6838
		public NKM_TEAM_TYPE m_NKM_TEAM_TYPE;

		// Token: 0x04001AB7 RID: 6839
		public sbyte m_UnitDeckIndex = -1;

		// Token: 0x04001AB8 RID: 6840
		public long m_UnitDeckUID = -1L;

		// Token: 0x04001AB9 RID: 6841
		public long m_DeckUsedAddUnitUID = -1L;

		// Token: 0x04001ABA RID: 6842
		public sbyte m_DeckUsedRemoveIndex = -1;

		// Token: 0x04001ABB RID: 6843
		public long m_DeckTombAddUnitUID = -1L;

		// Token: 0x04001ABC RID: 6844
		public sbyte m_AutoRespawnIndex = -1;

		// Token: 0x04001ABD RID: 6845
		public long m_NextDeckUnitUID = -1L;
	}
}

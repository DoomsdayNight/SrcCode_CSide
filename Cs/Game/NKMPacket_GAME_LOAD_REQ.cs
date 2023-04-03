using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F2D RID: 3885
	[PacketId(ClientPacketId.kNKMPacket_GAME_LOAD_REQ)]
	public sealed class NKMPacket_GAME_LOAD_REQ : ISerializable
	{
		// Token: 0x0600993A RID: 39226 RVA: 0x0032FA88 File Offset: 0x0032DC88
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isDev);
			stream.PutOrGet(ref this.selectDeckIndex);
			stream.PutOrGet(ref this.stageID);
			stream.PutOrGet(ref this.diveStageID);
			stream.PutOrGet(ref this.dungeonID);
			stream.PutOrGet(ref this.palaceID);
			stream.PutOrGet(ref this.fierceBossId);
			stream.PutOrGet<NKMEventDeckData>(ref this.eventDeckData);
			stream.PutOrGet(ref this.rewardMultiply);
		}

		// Token: 0x04008C19 RID: 35865
		public bool isDev;

		// Token: 0x04008C1A RID: 35866
		public byte selectDeckIndex;

		// Token: 0x04008C1B RID: 35867
		public int stageID;

		// Token: 0x04008C1C RID: 35868
		public int diveStageID;

		// Token: 0x04008C1D RID: 35869
		public int dungeonID;

		// Token: 0x04008C1E RID: 35870
		public int palaceID;

		// Token: 0x04008C1F RID: 35871
		public int fierceBossId;

		// Token: 0x04008C20 RID: 35872
		public NKMEventDeckData eventDeckData;

		// Token: 0x04008C21 RID: 35873
		public int rewardMultiply;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB3 RID: 3251
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_SET_ACK)]
	public sealed class NKMPacket_DECK_UNIT_SET_ACK : ISerializable
	{
		// Token: 0x06009463 RID: 37987 RVA: 0x00328A20 File Offset: 0x00326C20
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.slotIndex);
			stream.PutOrGet(ref this.slotUnitUID);
			stream.PutOrGet<NKMDeckIndex>(ref this.oldDeckIndex);
			stream.PutOrGet(ref this.oldSlotIndex);
			stream.PutOrGet(ref this.leaderSlotIndex);
			stream.PutOrGet(ref this.oldLeaderSlotIndex);
		}

		// Token: 0x040085C5 RID: 34245
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085C6 RID: 34246
		public NKMDeckIndex deckIndex;

		// Token: 0x040085C7 RID: 34247
		public byte slotIndex;

		// Token: 0x040085C8 RID: 34248
		public long slotUnitUID;

		// Token: 0x040085C9 RID: 34249
		public NKMDeckIndex oldDeckIndex;

		// Token: 0x040085CA RID: 34250
		public sbyte oldSlotIndex;

		// Token: 0x040085CB RID: 34251
		public sbyte leaderSlotIndex;

		// Token: 0x040085CC RID: 34252
		public sbyte oldLeaderSlotIndex;
	}
}

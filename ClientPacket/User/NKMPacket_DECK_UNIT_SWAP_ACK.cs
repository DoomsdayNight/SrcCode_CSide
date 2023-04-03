using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CAD RID: 3245
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_SWAP_ACK)]
	public sealed class NKMPacket_DECK_UNIT_SWAP_ACK : ISerializable
	{
		// Token: 0x06009457 RID: 37975 RVA: 0x003288E0 File Offset: 0x00326AE0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.leaderSlotIndex);
			stream.PutOrGet(ref this.slotIndexFrom);
			stream.PutOrGet(ref this.slotIndexTo);
			stream.PutOrGet(ref this.slotUnitUIDFrom);
			stream.PutOrGet(ref this.slotUnitUIDTo);
		}

		// Token: 0x040085B1 RID: 34225
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085B2 RID: 34226
		public NKMDeckIndex deckIndex;

		// Token: 0x040085B3 RID: 34227
		public sbyte leaderSlotIndex = -1;

		// Token: 0x040085B4 RID: 34228
		public byte slotIndexFrom;

		// Token: 0x040085B5 RID: 34229
		public byte slotIndexTo;

		// Token: 0x040085B6 RID: 34230
		public long slotUnitUIDFrom;

		// Token: 0x040085B7 RID: 34231
		public long slotUnitUIDTo;
	}
}

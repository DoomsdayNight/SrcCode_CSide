using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CAC RID: 3244
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_SWAP_REQ)]
	public sealed class NKMPacket_DECK_UNIT_SWAP_REQ : ISerializable
	{
		// Token: 0x06009455 RID: 37973 RVA: 0x003288B2 File Offset: 0x00326AB2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.slotIndexFrom);
			stream.PutOrGet(ref this.slotIndexTo);
		}

		// Token: 0x040085AE RID: 34222
		public NKMDeckIndex deckIndex;

		// Token: 0x040085AF RID: 34223
		public byte slotIndexFrom;

		// Token: 0x040085B0 RID: 34224
		public byte slotIndexTo;
	}
}

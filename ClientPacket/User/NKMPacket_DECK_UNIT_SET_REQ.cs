using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB2 RID: 3250
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_SET_REQ)]
	public sealed class NKMPacket_DECK_UNIT_SET_REQ : ISerializable
	{
		// Token: 0x06009461 RID: 37985 RVA: 0x003289F0 File Offset: 0x00326BF0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.slotIndex);
			stream.PutOrGet(ref this.unitUID);
		}

		// Token: 0x040085C2 RID: 34242
		public NKMDeckIndex deckIndex;

		// Token: 0x040085C3 RID: 34243
		public byte slotIndex;

		// Token: 0x040085C4 RID: 34244
		public long unitUID;
	}
}

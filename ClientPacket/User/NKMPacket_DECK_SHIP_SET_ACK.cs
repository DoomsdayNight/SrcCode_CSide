using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB7 RID: 3255
	[PacketId(ClientPacketId.kNKMPacket_DECK_SHIP_SET_ACK)]
	public sealed class NKMPacket_DECK_SHIP_SET_ACK : ISerializable
	{
		// Token: 0x0600946B RID: 37995 RVA: 0x00328B2A File Offset: 0x00326D2A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet<NKMDeckIndex>(ref this.oldDeckIndex);
			stream.PutOrGet(ref this.shipUID);
		}

		// Token: 0x040085D6 RID: 34262
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085D7 RID: 34263
		public NKMDeckIndex deckIndex;

		// Token: 0x040085D8 RID: 34264
		public NKMDeckIndex oldDeckIndex;

		// Token: 0x040085D9 RID: 34265
		public long shipUID;
	}
}

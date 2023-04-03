using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB8 RID: 3256
	[PacketId(ClientPacketId.kNKMPacket_DECK_OPERATOR_SET_REQ)]
	public sealed class NKMPacket_DECK_OPERATOR_SET_REQ : ISerializable
	{
		// Token: 0x0600946D RID: 37997 RVA: 0x00328B64 File Offset: 0x00326D64
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.operatorUid);
		}

		// Token: 0x040085DA RID: 34266
		public NKMDeckIndex deckIndex;

		// Token: 0x040085DB RID: 34267
		public long operatorUid;
	}
}

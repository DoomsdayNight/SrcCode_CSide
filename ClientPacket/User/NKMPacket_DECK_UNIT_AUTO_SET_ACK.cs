using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB5 RID: 3253
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_AUTO_SET_ACK)]
	public sealed class NKMPacket_DECK_UNIT_AUTO_SET_ACK : ISerializable
	{
		// Token: 0x06009467 RID: 37991 RVA: 0x00328ADA File Offset: 0x00326CDA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckData>(ref this.deckData);
		}

		// Token: 0x040085D1 RID: 34257
		public NKMDeckIndex deckIndex;

		// Token: 0x040085D2 RID: 34258
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085D3 RID: 34259
		public NKMDeckData deckData;
	}
}

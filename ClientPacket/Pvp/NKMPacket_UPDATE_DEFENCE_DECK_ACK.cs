using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D87 RID: 3463
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_DEFENCE_DECK_ACK)]
	public sealed class NKMPacket_UPDATE_DEFENCE_DECK_ACK : ISerializable
	{
		// Token: 0x06009609 RID: 38409 RVA: 0x0032B202 File Offset: 0x00329402
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckData>(ref this.deckData);
		}

		// Token: 0x04008805 RID: 34821
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008806 RID: 34822
		public NKMDeckData deckData;
	}
}

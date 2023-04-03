using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE7 RID: 4071
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK)]
	public sealed class NKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK : ISerializable
	{
		// Token: 0x06009A9E RID: 39582 RVA: 0x00331A6C File Offset: 0x0032FC6C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDummyDeckData>(ref this.deckData);
		}

		// Token: 0x04008DF7 RID: 36343
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DF8 RID: 36344
		public NKMDummyDeckData deckData;
	}
}

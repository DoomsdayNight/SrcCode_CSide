using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE6 RID: 4070
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_PROFILE_MODIFY_DECK_REQ)]
	public sealed class NKMPacket_FRIEND_PROFILE_MODIFY_DECK_REQ : ISerializable
	{
		// Token: 0x06009A9C RID: 39580 RVA: 0x00331A56 File Offset: 0x0032FC56
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
		}

		// Token: 0x04008DF6 RID: 36342
		public NKMDeckIndex deckIndex;
	}
}

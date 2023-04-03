using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CAE RID: 3246
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_SET_LEADER_REQ)]
	public sealed class NKMPacket_DECK_UNIT_SET_LEADER_REQ : ISerializable
	{
		// Token: 0x06009459 RID: 37977 RVA: 0x00328950 File Offset: 0x00326B50
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.leaderSlotIndex);
		}

		// Token: 0x040085B8 RID: 34232
		public NKMDeckIndex deckIndex;

		// Token: 0x040085B9 RID: 34233
		public sbyte leaderSlotIndex;
	}
}

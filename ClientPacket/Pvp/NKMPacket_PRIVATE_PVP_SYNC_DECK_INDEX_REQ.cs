using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC7 RID: 3527
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ : ISerializable
	{
		// Token: 0x06009687 RID: 38535 RVA: 0x0032BA54 File Offset: 0x00329C54
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
		}

		// Token: 0x04008875 RID: 34933
		public NKMDeckIndex deckIndex;
	}
}

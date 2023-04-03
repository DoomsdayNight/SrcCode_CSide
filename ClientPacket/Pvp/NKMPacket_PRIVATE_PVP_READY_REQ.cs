using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D9A RID: 3482
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_READY_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_READY_REQ : ISerializable
	{
		// Token: 0x0600962D RID: 38445 RVA: 0x0032B59F File Offset: 0x0032979F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
		}

		// Token: 0x04008842 RID: 34882
		public NKMDeckIndex deckIndex;
	}
}

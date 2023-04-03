using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE8 RID: 4072
	[PacketId(ClientPacketId.kNKMPacket_SET_EMBLEM_REQ)]
	public sealed class NKMPacket_SET_EMBLEM_REQ : ISerializable
	{
		// Token: 0x06009AA0 RID: 39584 RVA: 0x00331A8E File Offset: 0x0032FC8E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.index);
			stream.PutOrGet(ref this.itemId);
		}

		// Token: 0x04008DF9 RID: 36345
		public sbyte index;

		// Token: 0x04008DFA RID: 36346
		public int itemId;
	}
}

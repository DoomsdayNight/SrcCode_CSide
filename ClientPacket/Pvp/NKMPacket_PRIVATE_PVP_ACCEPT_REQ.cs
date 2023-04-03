using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D95 RID: 3477
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_ACCEPT_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_ACCEPT_REQ : ISerializable
	{
		// Token: 0x06009623 RID: 38435 RVA: 0x0032B46A File Offset: 0x0032966A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
			stream.PutOrGet(ref this.accept);
		}

		// Token: 0x04008831 RID: 34865
		public long targetUserUid;

		// Token: 0x04008832 RID: 34866
		public bool accept;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE0 RID: 4064
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_ACCEPT_ACK)]
	public sealed class NKMPacket_FRIEND_ACCEPT_ACK : ISerializable
	{
		// Token: 0x06009A90 RID: 39568 RVA: 0x00331967 File Offset: 0x0032FB67
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.isAllow);
		}

		// Token: 0x04008DE8 RID: 36328
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DE9 RID: 36329
		public long friendCode;

		// Token: 0x04008DEA RID: 36330
		public bool isAllow;
	}
}

using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200101F RID: 4127
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_DISACCEPT_MENTOR_REQ)]
	public sealed class NKMPacket_MENTORING_DISACCEPT_MENTOR_REQ : ISerializable
	{
		// Token: 0x06009B0E RID: 39694 RVA: 0x0033230A File Offset: 0x0033050A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.mentorUid);
		}

		// Token: 0x04008E69 RID: 36457
		public long mentorUid;
	}
}

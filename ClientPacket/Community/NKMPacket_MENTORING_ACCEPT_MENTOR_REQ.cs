using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200101D RID: 4125
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_ACCEPT_MENTOR_REQ)]
	public sealed class NKMPacket_MENTORING_ACCEPT_MENTOR_REQ : ISerializable
	{
		// Token: 0x06009B0A RID: 39690 RVA: 0x003322C7 File Offset: 0x003304C7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.mentorUid);
		}

		// Token: 0x04008E66 RID: 36454
		public long mentorUid;
	}
}

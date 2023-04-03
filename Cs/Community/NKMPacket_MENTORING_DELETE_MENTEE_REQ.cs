using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001027 RID: 4135
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_DELETE_MENTEE_REQ)]
	public sealed class NKMPacket_MENTORING_DELETE_MENTEE_REQ : ISerializable
	{
		// Token: 0x06009B1E RID: 39710 RVA: 0x00332400 File Offset: 0x00330600
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.menteeUid);
		}

		// Token: 0x04008E75 RID: 36469
		public long menteeUid;
	}
}

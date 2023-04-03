using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001015 RID: 4117
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_MATCH_LIST_REQ)]
	public sealed class NKMPacket_MENTORING_MATCH_LIST_REQ : ISerializable
	{
		// Token: 0x06009AFA RID: 39674 RVA: 0x00332174 File Offset: 0x00330374
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isForce);
		}

		// Token: 0x04008E55 RID: 36437
		public bool isForce;
	}
}

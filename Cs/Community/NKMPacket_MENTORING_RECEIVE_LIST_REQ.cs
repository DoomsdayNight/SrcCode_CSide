using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001017 RID: 4119
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_RECEIVE_LIST_REQ)]
	public sealed class NKMPacket_MENTORING_RECEIVE_LIST_REQ : ISerializable
	{
		// Token: 0x06009AFE RID: 39678 RVA: 0x003321B7 File Offset: 0x003303B7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<MentoringIdentity>(ref this.identity);
			stream.PutOrGet(ref this.isForce);
		}

		// Token: 0x04008E58 RID: 36440
		public MentoringIdentity identity;

		// Token: 0x04008E59 RID: 36441
		public bool isForce;
	}
}

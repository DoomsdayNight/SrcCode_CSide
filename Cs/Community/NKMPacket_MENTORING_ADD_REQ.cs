using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200101B RID: 4123
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_ADD_REQ)]
	public sealed class NKMPacket_MENTORING_ADD_REQ : ISerializable
	{
		// Token: 0x06009B06 RID: 39686 RVA: 0x0033226C File Offset: 0x0033046C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<MentoringIdentity>(ref this.identity);
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008E61 RID: 36449
		public MentoringIdentity identity;

		// Token: 0x04008E62 RID: 36450
		public long userUid;
	}
}

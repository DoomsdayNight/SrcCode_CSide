using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001019 RID: 4121
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_SEARCH_LIST_REQ)]
	public sealed class NKMPacket_MENTORING_SEARCH_LIST_REQ : ISerializable
	{
		// Token: 0x06009B02 RID: 39682 RVA: 0x0033221D File Offset: 0x0033041D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<MentoringIdentity>(ref this.identity);
			stream.PutOrGet(ref this.keyword);
		}

		// Token: 0x04008E5D RID: 36445
		public MentoringIdentity identity;

		// Token: 0x04008E5E RID: 36446
		public string keyword;
	}
}

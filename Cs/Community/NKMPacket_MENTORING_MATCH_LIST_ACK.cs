using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001016 RID: 4118
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_MATCH_LIST_ACK)]
	public sealed class NKMPacket_MENTORING_MATCH_LIST_ACK : ISerializable
	{
		// Token: 0x06009AFC RID: 39676 RVA: 0x0033218A File Offset: 0x0033038A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<MenteeInfo>(ref this.matchList);
		}

		// Token: 0x04008E56 RID: 36438
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E57 RID: 36439
		public List<MenteeInfo> matchList = new List<MenteeInfo>();
	}
}

using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001010 RID: 4112
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_USER_BAN_LIST_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_USER_BAN_LIST_ACK : ISerializable
	{
		// Token: 0x06009AF0 RID: 39664 RVA: 0x003320CC File Offset: 0x003302CC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.banishList);
		}

		// Token: 0x04008E4D RID: 36429
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E4E RID: 36430
		public List<long> banishList = new List<long>();
	}
}

using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E08 RID: 3592
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_LIST_ACK)]
	public sealed class NKMPacket_OFFICE_POST_LIST_ACK : ISerializable
	{
		// Token: 0x06009704 RID: 38660 RVA: 0x0032C694 File Offset: 0x0032A894
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficePost>(ref this.postList);
			stream.PutOrGet(ref this.postCount);
		}

		// Token: 0x0400891A RID: 35098
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400891B RID: 35099
		public List<NKMOfficePost> postList = new List<NKMOfficePost>();

		// Token: 0x0400891C RID: 35100
		public int postCount;
	}
}

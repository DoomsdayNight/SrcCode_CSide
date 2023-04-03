using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CBB RID: 3259
	[PacketId(ClientPacketId.kNKMPacket_POST_LIST_ACK)]
	public sealed class NKMPacket_POST_LIST_ACK : ISerializable
	{
		// Token: 0x06009473 RID: 38003 RVA: 0x00328BD6 File Offset: 0x00326DD6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPostData>(ref this.postDataList);
			stream.PutOrGet(ref this.postCount);
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x040085E1 RID: 34273
		public List<NKMPostData> postDataList = new List<NKMPostData>();

		// Token: 0x040085E2 RID: 34274
		public int postCount;

		// Token: 0x040085E3 RID: 34275
		public NKM_ERROR_CODE errorCode;
	}
}

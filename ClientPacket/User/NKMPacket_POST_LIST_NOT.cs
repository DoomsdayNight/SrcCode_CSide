using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD7 RID: 3287
	[PacketId(ClientPacketId.kNKMPacket_POST_LIST_NOT)]
	public sealed class NKMPacket_POST_LIST_NOT : ISerializable
	{
		// Token: 0x060094AB RID: 38059 RVA: 0x003290EC File Offset: 0x003272EC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPostData>(ref this.postDataList);
			stream.PutOrGet(ref this.postCount);
		}

		// Token: 0x0400862B RID: 34347
		public List<NKMPostData> postDataList = new List<NKMPostData>();

		// Token: 0x0400862C RID: 34348
		public int postCount;
	}
}

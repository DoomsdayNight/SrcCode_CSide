using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E0A RID: 3594
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_RECV_ACK)]
	public sealed class NKMPacket_OFFICE_POST_RECV_ACK : ISerializable
	{
		// Token: 0x06009708 RID: 38664 RVA: 0x0032C6D7 File Offset: 0x0032A8D7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMOfficePost>(ref this.postList);
			stream.PutOrGet(ref this.postCount);
			stream.PutOrGet<NKMOfficePostState>(ref this.postState);
		}

		// Token: 0x0400891D RID: 35101
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400891E RID: 35102
		public NKMRewardData rewardData;

		// Token: 0x0400891F RID: 35103
		public List<NKMOfficePost> postList = new List<NKMOfficePost>();

		// Token: 0x04008920 RID: 35104
		public int postCount;

		// Token: 0x04008921 RID: 35105
		public NKMOfficePostState postState = new NKMOfficePostState();
	}
}

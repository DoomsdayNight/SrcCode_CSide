using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001014 RID: 4116
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_DATA_ACK)]
	public sealed class NKMPacket_MENTORING_DATA_ACK : ISerializable
	{
		// Token: 0x06009AF8 RID: 39672 RVA: 0x0033213B File Offset: 0x0033033B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.mentorData);
			stream.PutOrGet(ref this.isGraduated);
		}

		// Token: 0x04008E52 RID: 36434
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E53 RID: 36435
		public FriendListData mentorData = new FriendListData();

		// Token: 0x04008E54 RID: 36436
		public bool isGraduated;
	}
}

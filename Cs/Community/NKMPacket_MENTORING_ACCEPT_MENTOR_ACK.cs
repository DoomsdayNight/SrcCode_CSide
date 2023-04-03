using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200101E RID: 4126
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_ACCEPT_MENTOR_ACK)]
	public sealed class NKMPacket_MENTORING_ACCEPT_MENTOR_ACK : ISerializable
	{
		// Token: 0x06009B0C RID: 39692 RVA: 0x003322DD File Offset: 0x003304DD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<FriendListData>(ref this.mentorData);
		}

		// Token: 0x04008E67 RID: 36455
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E68 RID: 36456
		public FriendListData mentorData = new FriendListData();
	}
}

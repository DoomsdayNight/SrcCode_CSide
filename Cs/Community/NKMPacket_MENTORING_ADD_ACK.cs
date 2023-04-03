using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200101C RID: 4124
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_ADD_ACK)]
	public sealed class NKMPacket_MENTORING_ADD_ACK : ISerializable
	{
		// Token: 0x06009B08 RID: 39688 RVA: 0x0033228E File Offset: 0x0033048E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<MentoringIdentity>(ref this.identity);
			stream.PutOrGet<FriendListData>(ref this.mentoringData);
		}

		// Token: 0x04008E63 RID: 36451
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E64 RID: 36452
		public MentoringIdentity identity;

		// Token: 0x04008E65 RID: 36453
		public FriendListData mentoringData = new FriendListData();
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001020 RID: 4128
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_DISACCEPT_MENTOR_ACK)]
	public sealed class NKMPacket_MENTORING_DISACCEPT_MENTOR_ACK : ISerializable
	{
		// Token: 0x06009B10 RID: 39696 RVA: 0x00332320 File Offset: 0x00330520
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.mentorUid);
		}

		// Token: 0x04008E6A RID: 36458
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E6B RID: 36459
		public long mentorUid;
	}
}

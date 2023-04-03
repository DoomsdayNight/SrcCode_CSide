using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001028 RID: 4136
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_DELETE_MENTEE_ACK)]
	public sealed class NKMPacket_MENTORING_DELETE_MENTEE_ACK : ISerializable
	{
		// Token: 0x06009B20 RID: 39712 RVA: 0x00332416 File Offset: 0x00330616
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.menteeUid);
		}

		// Token: 0x04008E76 RID: 36470
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E77 RID: 36471
		public long menteeUid;
	}
}

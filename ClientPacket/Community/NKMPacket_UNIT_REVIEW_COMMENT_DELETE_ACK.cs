using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF2 RID: 4082
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_DELETE_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_DELETE_ACK : ISerializable
	{
		// Token: 0x06009AB4 RID: 39604 RVA: 0x00331C27 File Offset: 0x0032FE27
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008E10 RID: 36368
		public NKM_ERROR_CODE errorCode;
	}
}

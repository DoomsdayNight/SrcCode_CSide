using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200100C RID: 4108
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_USER_BAN_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_USER_BAN_ACK : ISerializable
	{
		// Token: 0x06009AE8 RID: 39656 RVA: 0x00332068 File Offset: 0x00330268
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008E48 RID: 36424
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E49 RID: 36425
		public long targetUserUid;
	}
}

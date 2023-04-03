using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200100E RID: 4110
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_ACK : ISerializable
	{
		// Token: 0x06009AEC RID: 39660 RVA: 0x003320A0 File Offset: 0x003302A0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008E4B RID: 36427
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E4C RID: 36428
		public long targetUserUid;
	}
}

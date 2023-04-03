using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E0E RID: 3598
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_SEND_ACK)]
	public sealed class NKMPacket_OFFICE_POST_SEND_ACK : ISerializable
	{
		// Token: 0x06009710 RID: 38672 RVA: 0x0032C780 File Offset: 0x0032A980
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.receiverUserUid);
			stream.PutOrGet<NKMOfficePostState>(ref this.postState);
		}

		// Token: 0x04008925 RID: 35109
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008926 RID: 35110
		public long receiverUserUid;

		// Token: 0x04008927 RID: 35111
		public NKMOfficePostState postState = new NKMOfficePostState();
	}
}

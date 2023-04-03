using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CE1 RID: 3297
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_PVP_INVITATION_OPTION_ACK)]
	public sealed class NKMPacket_UPDATE_PVP_INVITATION_OPTION_ACK : ISerializable
	{
		// Token: 0x060094BF RID: 38079 RVA: 0x00329253 File Offset: 0x00327453
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<PrivatePvpInvitation>(ref this.value);
		}

		// Token: 0x0400863C RID: 34364
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400863D RID: 34365
		public PrivatePvpInvitation value;
	}
}

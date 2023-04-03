using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CE0 RID: 3296
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_PVP_INVITATION_OPTION_REQ)]
	public sealed class NKMPacket_UPDATE_PVP_INVITATION_OPTION_REQ : ISerializable
	{
		// Token: 0x060094BD RID: 38077 RVA: 0x0032923D File Offset: 0x0032743D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<PrivatePvpInvitation>(ref this.value);
		}

		// Token: 0x0400863B RID: 34363
		public PrivatePvpInvitation value;
	}
}

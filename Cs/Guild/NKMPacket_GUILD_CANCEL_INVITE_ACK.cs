using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE7 RID: 3815
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CANCEL_INVITE_ACK)]
	public sealed class NKMPacket_GUILD_CANCEL_INVITE_ACK : ISerializable
	{
		// Token: 0x060098AE RID: 39086 RVA: 0x0032ED7F File Offset: 0x0032CF7F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008B53 RID: 35667
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B54 RID: 35668
		public long userUid;
	}
}

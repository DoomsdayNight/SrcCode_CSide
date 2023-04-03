using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE4 RID: 3812
	[PacketId(ClientPacketId.kNKMPacket_GUILD_INVITE_ACK)]
	public sealed class NKMPacket_GUILD_INVITE_ACK : ISerializable
	{
		// Token: 0x060098A8 RID: 39080 RVA: 0x0032ED25 File Offset: 0x0032CF25
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008B4E RID: 35662
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B4F RID: 35663
		public long userUid;
	}
}

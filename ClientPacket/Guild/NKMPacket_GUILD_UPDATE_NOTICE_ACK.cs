using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EFB RID: 3835
	[PacketId(ClientPacketId.kNKMPacket_GUILD_UPDATE_NOTICE_ACK)]
	public sealed class NKMPacket_GUILD_UPDATE_NOTICE_ACK : ISerializable
	{
		// Token: 0x060098D6 RID: 39126 RVA: 0x0032F117 File Offset: 0x0032D317
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.noticeBefore);
			stream.PutOrGet(ref this.notice);
		}

		// Token: 0x04008B8D RID: 35725
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B8E RID: 35726
		public long guildUid;

		// Token: 0x04008B8F RID: 35727
		public string noticeBefore;

		// Token: 0x04008B90 RID: 35728
		public string notice;
	}
}

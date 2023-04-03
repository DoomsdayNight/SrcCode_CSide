using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EDA RID: 3802
	[PacketId(ClientPacketId.kNKMPacket_GUILD_JOIN_ACK)]
	public sealed class NKMPacket_GUILD_JOIN_ACK : ISerializable
	{
		// Token: 0x06009894 RID: 39060 RVA: 0x0032EB69 File Offset: 0x0032CD69
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.needApproval);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet<PrivateGuildData>(ref this.privateGuildData);
		}

		// Token: 0x04008B35 RID: 35637
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B36 RID: 35638
		public bool needApproval;

		// Token: 0x04008B37 RID: 35639
		public long guildUid;

		// Token: 0x04008B38 RID: 35640
		public PrivateGuildData privateGuildData = new PrivateGuildData();
	}
}

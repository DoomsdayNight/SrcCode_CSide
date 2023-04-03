using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F19 RID: 3865
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_MEMBER_INFO_ACK)]
	public sealed class NKMPacket_GUILD_DUNGEON_MEMBER_INFO_ACK : ISerializable
	{
		// Token: 0x06009912 RID: 39186 RVA: 0x0032F731 File Offset: 0x0032D931
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<GuildDungeonMemberInfo>(ref this.memberInfoList);
		}

		// Token: 0x04008BE7 RID: 35815
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BE8 RID: 35816
		public List<GuildDungeonMemberInfo> memberInfoList = new List<GuildDungeonMemberInfo>();
	}
}

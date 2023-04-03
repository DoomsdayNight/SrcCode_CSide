using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F20 RID: 3872
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_ARENA_PLAY_END_NOT)]
	public sealed class NKMPacket_GUILD_DUNGEON_ARENA_PLAY_END_NOT : ISerializable
	{
		// Token: 0x06009920 RID: 39200 RVA: 0x0032F86F File Offset: 0x0032DA6F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.playedUserUid);
			stream.PutOrGet(ref this.arenaId);
			stream.PutOrGet(ref this.totalGrade);
		}

		// Token: 0x04008BF8 RID: 35832
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BF9 RID: 35833
		public long playedUserUid;

		// Token: 0x04008BFA RID: 35834
		public int arenaId;

		// Token: 0x04008BFB RID: 35835
		public int totalGrade;
	}
}

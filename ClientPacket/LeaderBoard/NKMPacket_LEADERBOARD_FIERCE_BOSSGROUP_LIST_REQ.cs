using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E7B RID: 3707
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ)]
	public sealed class NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ : ISerializable
	{
		// Token: 0x060097E4 RID: 38884 RVA: 0x0032D9F4 File Offset: 0x0032BBF4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.fierceBossGroupId);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A1F RID: 35359
		public int fierceBossGroupId;

		// Token: 0x04008A20 RID: 35360
		public bool isAll;
	}
}

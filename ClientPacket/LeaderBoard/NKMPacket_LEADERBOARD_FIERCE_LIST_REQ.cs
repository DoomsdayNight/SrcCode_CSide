using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E79 RID: 3705
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_FIERCE_LIST_REQ)]
	public sealed class NKMPacket_LEADERBOARD_FIERCE_LIST_REQ : ISerializable
	{
		// Token: 0x060097E0 RID: 38880 RVA: 0x0032D98D File Offset: 0x0032BB8D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A19 RID: 35353
		public bool isAll;
	}
}

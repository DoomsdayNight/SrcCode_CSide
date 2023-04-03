using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E75 RID: 3701
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_ACHIEVE_LIST_REQ)]
	public sealed class NKMPacket_LEADERBOARD_ACHIEVE_LIST_REQ : ISerializable
	{
		// Token: 0x060097D8 RID: 38872 RVA: 0x0032D8BF File Offset: 0x0032BABF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A0D RID: 35341
		public bool isAll;
	}
}

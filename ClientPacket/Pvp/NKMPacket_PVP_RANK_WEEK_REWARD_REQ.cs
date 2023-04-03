using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D7B RID: 3451
	[PacketId(ClientPacketId.kNKMPacket_PVP_RANK_WEEK_REWARD_REQ)]
	public sealed class NKMPacket_PVP_RANK_WEEK_REWARD_REQ : ISerializable
	{
		// Token: 0x060095F1 RID: 38385 RVA: 0x0032B01B File Offset: 0x0032921B
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

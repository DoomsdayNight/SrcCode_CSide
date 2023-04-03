using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D8B RID: 3467
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_RANK_WEEK_REWARD_REQ)]
	public sealed class NKMPacket_ASYNC_PVP_RANK_WEEK_REWARD_REQ : ISerializable
	{
		// Token: 0x06009611 RID: 38417 RVA: 0x0032B317 File Offset: 0x00329517
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

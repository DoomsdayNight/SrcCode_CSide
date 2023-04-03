using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F61 RID: 3937
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_REQ)]
	public sealed class NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_REQ : ISerializable
	{
		// Token: 0x060099A2 RID: 39330 RVA: 0x00330450 File Offset: 0x0032E650
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

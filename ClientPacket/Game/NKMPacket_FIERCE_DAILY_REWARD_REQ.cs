using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F64 RID: 3940
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_DAILY_REWARD_REQ)]
	public sealed class NKMPacket_FIERCE_DAILY_REWARD_REQ : ISerializable
	{
		// Token: 0x060099A8 RID: 39336 RVA: 0x003304A9 File Offset: 0x0032E6A9
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

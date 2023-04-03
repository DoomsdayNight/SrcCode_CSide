using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C9B RID: 3227
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_TURN_FINISH_REQ)]
	public sealed class NKMPacket_WARFARE_GAME_TURN_FINISH_REQ : ISerializable
	{
		// Token: 0x06009433 RID: 37939 RVA: 0x003285C1 File Offset: 0x003267C1
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

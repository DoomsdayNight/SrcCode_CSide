using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C9D RID: 3229
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_NEXT_ORDER_REQ)]
	public sealed class NKMPacket_WARFARE_GAME_NEXT_ORDER_REQ : ISerializable
	{
		// Token: 0x06009437 RID: 37943 RVA: 0x003285F8 File Offset: 0x003267F8
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

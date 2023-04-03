using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DDB RID: 3547
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_START_GAME_SETTING_REQ)]
	public sealed class NKMPacket_PVP_ROOM_START_GAME_SETTING_REQ : ISerializable
	{
		// Token: 0x060096AD RID: 38573 RVA: 0x0032BCF6 File Offset: 0x00329EF6
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

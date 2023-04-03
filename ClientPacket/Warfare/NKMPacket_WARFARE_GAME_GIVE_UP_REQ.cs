using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C9F RID: 3231
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_GIVE_UP_REQ)]
	public sealed class NKMPacket_WARFARE_GAME_GIVE_UP_REQ : ISerializable
	{
		// Token: 0x0600943B RID: 37947 RVA: 0x00328647 File Offset: 0x00326847
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

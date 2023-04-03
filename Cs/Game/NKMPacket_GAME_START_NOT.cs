using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F35 RID: 3893
	[PacketId(ClientPacketId.kNKMPacket_GAME_START_NOT)]
	public sealed class NKMPacket_GAME_START_NOT : ISerializable
	{
		// Token: 0x0600994A RID: 39242 RVA: 0x0032FC25 File Offset: 0x0032DE25
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

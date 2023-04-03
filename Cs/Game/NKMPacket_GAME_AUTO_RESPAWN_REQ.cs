using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F41 RID: 3905
	[PacketId(ClientPacketId.kNKMPacket_GAME_AUTO_RESPAWN_REQ)]
	public sealed class NKMPacket_GAME_AUTO_RESPAWN_REQ : ISerializable
	{
		// Token: 0x06009962 RID: 39266 RVA: 0x0032FFB1 File Offset: 0x0032E1B1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isAutoRespawn);
		}

		// Token: 0x04008C69 RID: 35945
		public bool isAutoRespawn;
	}
}

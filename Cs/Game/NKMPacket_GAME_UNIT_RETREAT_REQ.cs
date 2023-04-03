using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F53 RID: 3923
	[PacketId(ClientPacketId.kNKMPacket_GAME_UNIT_RETREAT_REQ)]
	public sealed class NKMPacket_GAME_UNIT_RETREAT_REQ : ISerializable
	{
		// Token: 0x06009986 RID: 39302 RVA: 0x00330226 File Offset: 0x0032E426
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
		}

		// Token: 0x04008C8C RID: 35980
		public long unitUID;
	}
}

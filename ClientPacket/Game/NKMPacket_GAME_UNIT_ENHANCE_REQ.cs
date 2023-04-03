using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F55 RID: 3925
	[PacketId(ClientPacketId.kNKMPacket_GAME_UNIT_ENHANCE_REQ)]
	public sealed class NKMPacket_GAME_UNIT_ENHANCE_REQ : ISerializable
	{
		// Token: 0x0600998A RID: 39306 RVA: 0x0033025E File Offset: 0x0032E45E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
		}

		// Token: 0x04008C8F RID: 35983
		public long unitUID;
	}
}

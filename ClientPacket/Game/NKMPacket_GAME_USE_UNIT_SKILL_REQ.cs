using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F4A RID: 3914
	[PacketId(ClientPacketId.kNKMPacket_GAME_USE_UNIT_SKILL_REQ)]
	public sealed class NKMPacket_GAME_USE_UNIT_SKILL_REQ : ISerializable
	{
		// Token: 0x06009974 RID: 39284 RVA: 0x003300AE File Offset: 0x0032E2AE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.gameUnitUID);
		}

		// Token: 0x04008C76 RID: 35958
		public short gameUnitUID;
	}
}

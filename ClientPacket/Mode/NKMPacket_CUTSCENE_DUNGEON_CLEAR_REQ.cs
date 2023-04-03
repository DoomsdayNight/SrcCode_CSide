using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E30 RID: 3632
	[PacketId(ClientPacketId.kNKMPacket_CUTSCENE_DUNGEON_CLEAR_REQ)]
	public sealed class NKMPacket_CUTSCENE_DUNGEON_CLEAR_REQ : ISerializable
	{
		// Token: 0x06009750 RID: 38736 RVA: 0x0032CCEF File Offset: 0x0032AEEF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dungeonID);
		}

		// Token: 0x04008972 RID: 35186
		public int dungeonID;
	}
}

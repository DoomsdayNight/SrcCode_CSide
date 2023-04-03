using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E2E RID: 3630
	[PacketId(ClientPacketId.kNKMPacket_CUTSCENE_DUNGEON_START_REQ)]
	public sealed class NKMPacket_CUTSCENE_DUNGEON_START_REQ : ISerializable
	{
		// Token: 0x0600974C RID: 38732 RVA: 0x0032CCAC File Offset: 0x0032AEAC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dungeonID);
		}

		// Token: 0x0400896F RID: 35183
		public int dungeonID;
	}
}

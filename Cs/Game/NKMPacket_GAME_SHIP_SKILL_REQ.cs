using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F3F RID: 3903
	[PacketId(ClientPacketId.kNKMPacket_GAME_SHIP_SKILL_REQ)]
	public sealed class NKMPacket_GAME_SHIP_SKILL_REQ : ISerializable
	{
		// Token: 0x0600995E RID: 39262 RVA: 0x0032FF49 File Offset: 0x0032E149
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.gameUnitUID);
			stream.PutOrGet(ref this.shipSkillID);
			stream.PutOrGet(ref this.skillPosX);
		}

		// Token: 0x04008C62 RID: 35938
		public short gameUnitUID;

		// Token: 0x04008C63 RID: 35939
		public int shipSkillID;

		// Token: 0x04008C64 RID: 35940
		public float skillPosX;
	}
}

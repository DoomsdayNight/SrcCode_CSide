using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F4E RID: 3918
	[PacketId(ClientPacketId.kNKMPacket_GAME_DEV_RESPAWN_REQ)]
	public sealed class NKMPacket_GAME_DEV_RESPAWN_REQ : ISerializable
	{
		// Token: 0x0600997C RID: 39292 RVA: 0x00330142 File Offset: 0x0032E342
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.unitLevel);
			stream.PutOrGet(ref this.respawnPosX);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.teamType);
		}

		// Token: 0x04008C7F RID: 35967
		public int unitID;

		// Token: 0x04008C80 RID: 35968
		public int unitLevel;

		// Token: 0x04008C81 RID: 35969
		public float respawnPosX;

		// Token: 0x04008C82 RID: 35970
		public NKM_TEAM_TYPE teamType;
	}
}

using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F3D RID: 3901
	[PacketId(ClientPacketId.kNKMPacket_GAME_RESPAWN_REQ)]
	public sealed class NKMPacket_GAME_RESPAWN_REQ : ISerializable
	{
		// Token: 0x0600995A RID: 39258 RVA: 0x0032FEE1 File Offset: 0x0032E0E1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.assistUnit);
			stream.PutOrGet(ref this.respawnPosX);
			stream.PutOrGet(ref this.gameTime);
		}

		// Token: 0x04008C5B RID: 35931
		public long unitUID;

		// Token: 0x04008C5C RID: 35932
		public bool assistUnit;

		// Token: 0x04008C5D RID: 35933
		public float respawnPosX;

		// Token: 0x04008C5E RID: 35934
		public float gameTime;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CCE RID: 3278
	[PacketId(ClientPacketId.kNKMPacket_GAME_OPTION_CHANGE_REQ)]
	public sealed class NKMPacket_GAME_OPTION_CHANGE_REQ : ISerializable
	{
		// Token: 0x06009499 RID: 38041 RVA: 0x00328F2C File Offset: 0x0032712C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isActionCamera);
			stream.PutOrGet(ref this.isTrackCamera);
			stream.PutOrGet(ref this.isViewSkillCutIn);
			stream.PutOrGet(ref this.autoSyncFriendDeck);
			stream.PutOrGetEnum<NKM_GAME_AUTO_RESPAWN_TYPE>(ref this.defaultPvpAutoRespawn);
		}

		// Token: 0x04008611 RID: 34321
		public bool isActionCamera;

		// Token: 0x04008612 RID: 34322
		public bool isTrackCamera;

		// Token: 0x04008613 RID: 34323
		public bool isViewSkillCutIn;

		// Token: 0x04008614 RID: 34324
		public bool autoSyncFriendDeck;

		// Token: 0x04008615 RID: 34325
		public NKM_GAME_AUTO_RESPAWN_TYPE defaultPvpAutoRespawn;
	}
}

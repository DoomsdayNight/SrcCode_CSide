using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CCF RID: 3279
	[PacketId(ClientPacketId.kNKMPacket_GAME_OPTION_CHANGE_ACK)]
	public sealed class NKMPacket_GAME_OPTION_CHANGE_ACK : ISerializable
	{
		// Token: 0x0600949B RID: 38043 RVA: 0x00328F74 File Offset: 0x00327174
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isActionCamera);
			stream.PutOrGet(ref this.isTrackCamera);
			stream.PutOrGet(ref this.isViewSkillCutIn);
			stream.PutOrGet(ref this.autoSyncFriendDeck);
			stream.PutOrGetEnum<NKM_GAME_AUTO_RESPAWN_TYPE>(ref this.defaultPvpAutoRespawn);
		}

		// Token: 0x04008616 RID: 34326
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008617 RID: 34327
		public bool isActionCamera;

		// Token: 0x04008618 RID: 34328
		public bool isTrackCamera;

		// Token: 0x04008619 RID: 34329
		public bool isViewSkillCutIn;

		// Token: 0x0400861A RID: 34330
		public bool autoSyncFriendDeck;

		// Token: 0x0400861B RID: 34331
		public NKM_GAME_AUTO_RESPAWN_TYPE defaultPvpAutoRespawn;
	}
}

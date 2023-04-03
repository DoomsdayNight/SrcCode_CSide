using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004F2 RID: 1266
	public class NKMUserOption : ISerializable
	{
		// Token: 0x060023BE RID: 9150 RVA: 0x000BA920 File Offset: 0x000B8B20
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_bAutoRespawn);
			stream.PutOrGet(ref this.m_bActionCamera);
			stream.PutOrGet(ref this.m_bTrackCamera);
			stream.PutOrGet(ref this.m_bViewSkillCutIn);
			stream.PutOrGet(ref this.m_bAutoWarfare);
			stream.PutOrGet(ref this.m_bAutoWarfareRepair);
			stream.PutOrGet(ref this.m_bPlayCutscene);
			stream.PutOrGet(ref this.m_bAutoDive);
			stream.PutOrGetEnum<NKM_GAME_SPEED_TYPE>(ref this.m_eSpeedType);
			stream.PutOrGetEnum<NKM_GAME_AUTO_SKILL_TYPE>(ref this.m_eAutoSkillType);
			stream.PutOrGet(ref this.m_bAutoSyncFriendDeck);
			stream.PutOrGetEnum<NKM_GAME_AUTO_RESPAWN_TYPE>(ref this.m_bDefaultPvpAutoRespawn);
			stream.PutOrGetEnum<PrivatePvpInvitation>(ref this.privatePvpInvitation);
		}

		// Token: 0x0400259C RID: 9628
		public bool m_bAutoRespawn;

		// Token: 0x0400259D RID: 9629
		public bool m_bActionCamera = true;

		// Token: 0x0400259E RID: 9630
		public bool m_bTrackCamera = true;

		// Token: 0x0400259F RID: 9631
		public bool m_bViewSkillCutIn = true;

		// Token: 0x040025A0 RID: 9632
		public bool m_bAutoWarfare;

		// Token: 0x040025A1 RID: 9633
		public bool m_bAutoWarfareRepair = true;

		// Token: 0x040025A2 RID: 9634
		public bool m_bPlayCutscene;

		// Token: 0x040025A3 RID: 9635
		public bool m_bAutoDive;

		// Token: 0x040025A4 RID: 9636
		public NKM_GAME_SPEED_TYPE m_eSpeedType;

		// Token: 0x040025A5 RID: 9637
		public NKM_GAME_AUTO_SKILL_TYPE m_eAutoSkillType = NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER;

		// Token: 0x040025A6 RID: 9638
		public bool m_bAutoSyncFriendDeck = true;

		// Token: 0x040025A7 RID: 9639
		public NKM_GAME_AUTO_RESPAWN_TYPE m_bDefaultPvpAutoRespawn;

		// Token: 0x040025A8 RID: 9640
		public PrivatePvpInvitation privatePvpInvitation;
	}
}

using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D6E RID: 3438
	public sealed class DraftPvpRoomData : ISerializable
	{
		// Token: 0x060095D7 RID: 38359 RVA: 0x0032AD30 File Offset: 0x00328F30
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_GAME_TYPE>(ref this.gameType);
			stream.PutOrGetEnum<DRAFT_PVP_ROOM_STATE>(ref this.roomState);
			stream.PutOrGet(ref this.stateEndTime);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.currentStateTeamType);
			stream.PutOrGet<NKMAsyncUnitData>(ref this.selectedUnit);
			stream.PutOrGet<DraftPvpRoomData.DraftTeamData>(ref this.draftTeamDataA);
			stream.PutOrGet<DraftPvpRoomData.DraftTeamData>(ref this.draftTeamDataB);
		}

		// Token: 0x040087C1 RID: 34753
		public NKM_GAME_TYPE gameType;

		// Token: 0x040087C2 RID: 34754
		public DRAFT_PVP_ROOM_STATE roomState;

		// Token: 0x040087C3 RID: 34755
		public DateTime stateEndTime;

		// Token: 0x040087C4 RID: 34756
		public NKM_TEAM_TYPE currentStateTeamType;

		// Token: 0x040087C5 RID: 34757
		public NKMAsyncUnitData selectedUnit = new NKMAsyncUnitData();

		// Token: 0x040087C6 RID: 34758
		public DraftPvpRoomData.DraftTeamData draftTeamDataA = new DraftPvpRoomData.DraftTeamData();

		// Token: 0x040087C7 RID: 34759
		public DraftPvpRoomData.DraftTeamData draftTeamDataB = new DraftPvpRoomData.DraftTeamData();

		// Token: 0x02001A2A RID: 6698
		public sealed class DraftTeamData : ISerializable
		{
			// Token: 0x0600BB3F RID: 47935 RVA: 0x0036E914 File Offset: 0x0036CB14
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.teamType);
				stream.PutOrGet<NKMUserProfileData>(ref this.userProfileData);
				stream.PutOrGet(ref this.globalBanUnitIdList);
				stream.PutOrGet<NKMAsyncUnitData>(ref this.pickUnitList);
				stream.PutOrGet(ref this.banishedUnitIndex);
				stream.PutOrGet<NKMAsyncUnitData>(ref this.mainShip);
				stream.PutOrGet<NKMOperator>(ref this.operatorUnit);
				stream.PutOrGet(ref this.leaderIndex);
			}

			// Token: 0x0400ADD6 RID: 44502
			public NKM_TEAM_TYPE teamType;

			// Token: 0x0400ADD7 RID: 44503
			public NKMUserProfileData userProfileData = new NKMUserProfileData();

			// Token: 0x0400ADD8 RID: 44504
			public List<int> globalBanUnitIdList = new List<int>();

			// Token: 0x0400ADD9 RID: 44505
			public List<NKMAsyncUnitData> pickUnitList = new List<NKMAsyncUnitData>();

			// Token: 0x0400ADDA RID: 44506
			public int banishedUnitIndex;

			// Token: 0x0400ADDB RID: 44507
			public NKMAsyncUnitData mainShip = new NKMAsyncUnitData();

			// Token: 0x0400ADDC RID: 44508
			public NKMOperator operatorUnit;

			// Token: 0x0400ADDD RID: 44509
			public int leaderIndex;
		}
	}
}

using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Mode;
using ClientPacket.Warfare;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F38 RID: 3896
	[PacketId(ClientPacketId.kNKMPacket_GAME_END_NOT)]
	public sealed class NKMPacket_GAME_END_NOT : ISerializable
	{
		// Token: 0x06009950 RID: 39248 RVA: 0x0032FD10 File Offset: 0x0032DF10
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.win);
			stream.PutOrGet(ref this.giveup);
			stream.PutOrGet<NKMDungeonClearData>(ref this.dungeonClearData);
			stream.PutOrGet<NKMPhaseClearData>(ref this.phaseClearData);
			stream.PutOrGet<NKMEpisodeCompleteData>(ref this.episodeCompleteData);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet<WarfareSyncData>(ref this.warfareSyncData);
			stream.PutOrGet<NKMPVPResultDataForClient>(ref this.pvpResultData);
			stream.PutOrGet<NKMDiveSyncData>(ref this.diveSyncData);
			stream.PutOrGet<NKMRaidBossResultData>(ref this.raidBossResultData);
			stream.PutOrGet<NKMGameRecord>(ref this.gameRecord);
			stream.PutOrGet<UnitLoyaltyUpdateData>(ref this.updatedUnits);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet<NKMStagePlayData>(ref this.stagePlayData);
			stream.PutOrGet<NKMShadowGameResult>(ref this.shadowGameResult);
			stream.PutOrGet<NKMFierceResultData>(ref this.fierceResultData);
			stream.PutOrGet<PhaseModeState>(ref this.phaseModeState);
			stream.PutOrGet(ref this.killCountDelta);
			stream.PutOrGet<NKMKillCountData>(ref this.killCountData);
			stream.PutOrGet<TrimModeState>(ref this.trimModeState);
			stream.PutOrGet(ref this.totalPlayTime);
		}

		// Token: 0x04008C40 RID: 35904
		public bool win;

		// Token: 0x04008C41 RID: 35905
		public bool giveup;

		// Token: 0x04008C42 RID: 35906
		public NKMDungeonClearData dungeonClearData;

		// Token: 0x04008C43 RID: 35907
		public NKMPhaseClearData phaseClearData;

		// Token: 0x04008C44 RID: 35908
		public NKMEpisodeCompleteData episodeCompleteData;

		// Token: 0x04008C45 RID: 35909
		public NKMDeckIndex deckIndex;

		// Token: 0x04008C46 RID: 35910
		public WarfareSyncData warfareSyncData;

		// Token: 0x04008C47 RID: 35911
		public NKMPVPResultDataForClient pvpResultData;

		// Token: 0x04008C48 RID: 35912
		public NKMDiveSyncData diveSyncData;

		// Token: 0x04008C49 RID: 35913
		public NKMRaidBossResultData raidBossResultData = new NKMRaidBossResultData();

		// Token: 0x04008C4A RID: 35914
		public NKMGameRecord gameRecord;

		// Token: 0x04008C4B RID: 35915
		public List<UnitLoyaltyUpdateData> updatedUnits = new List<UnitLoyaltyUpdateData>();

		// Token: 0x04008C4C RID: 35916
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x04008C4D RID: 35917
		public NKMStagePlayData stagePlayData = new NKMStagePlayData();

		// Token: 0x04008C4E RID: 35918
		public NKMShadowGameResult shadowGameResult = new NKMShadowGameResult();

		// Token: 0x04008C4F RID: 35919
		public NKMFierceResultData fierceResultData = new NKMFierceResultData();

		// Token: 0x04008C50 RID: 35920
		public PhaseModeState phaseModeState;

		// Token: 0x04008C51 RID: 35921
		public long killCountDelta;

		// Token: 0x04008C52 RID: 35922
		public NKMKillCountData killCountData;

		// Token: 0x04008C53 RID: 35923
		public TrimModeState trimModeState;

		// Token: 0x04008C54 RID: 35924
		public float totalPlayTime;
	}
}

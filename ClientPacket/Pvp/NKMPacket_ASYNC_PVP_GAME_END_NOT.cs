using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D88 RID: 3464
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_GAME_END_NOT)]
	public sealed class NKMPacket_ASYNC_PVP_GAME_END_NOT : ISerializable
	{
		// Token: 0x0600960B RID: 38411 RVA: 0x0032B224 File Offset: 0x00329424
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<PVP_RESULT>(ref this.result);
			stream.PutOrGet<PvpState>(ref this.pvpState);
			stream.PutOrGet<NKMItemMiscData>(ref this.gainPointItem);
			stream.PutOrGet<NKMGameRecord>(ref this.gameRecord);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItem);
			stream.PutOrGet<PvpSingleHistory>(ref this.history);
			stream.PutOrGet<AsyncPvpTarget>(ref this.targetList);
			stream.PutOrGet(ref this.pointChargeTime);
			stream.PutOrGet(ref this.rankPvpOpen);
			stream.PutOrGet(ref this.leaguePvpOpen);
			stream.PutOrGet(ref this.npcMaxOpenedTier);
		}

		// Token: 0x04008807 RID: 34823
		public PVP_RESULT result;

		// Token: 0x04008808 RID: 34824
		public PvpState pvpState;

		// Token: 0x04008809 RID: 34825
		public NKMItemMiscData gainPointItem;

		// Token: 0x0400880A RID: 34826
		public NKMGameRecord gameRecord;

		// Token: 0x0400880B RID: 34827
		public List<NKMItemMiscData> costItem = new List<NKMItemMiscData>();

		// Token: 0x0400880C RID: 34828
		public PvpSingleHistory history;

		// Token: 0x0400880D RID: 34829
		public List<AsyncPvpTarget> targetList = new List<AsyncPvpTarget>();

		// Token: 0x0400880E RID: 34830
		public DateTime pointChargeTime;

		// Token: 0x0400880F RID: 34831
		public bool rankPvpOpen;

		// Token: 0x04008810 RID: 34832
		public bool leaguePvpOpen;

		// Token: 0x04008811 RID: 34833
		public int npcMaxOpenedTier;
	}
}

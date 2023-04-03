using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C94 RID: 3220
	public sealed class WarfareSyncData : ISerializable
	{
		// Token: 0x06009425 RID: 37925 RVA: 0x003282B8 File Offset: 0x003264B8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<WarfareUnitSyncData>(ref this.updatedUnits);
			stream.PutOrGet<WarfareSyncData.MovedUnit>(ref this.movedUnits);
			stream.PutOrGet<WarfareUnitData>(ref this.newUnits);
			stream.PutOrGet(ref this.retreaters);
			stream.PutOrGet<WarfareTileData>(ref this.tiles);
			stream.PutOrGet<WarfareGameSyncData>(ref this.gameState);
		}

		// Token: 0x0400855C RID: 34140
		public List<WarfareUnitSyncData> updatedUnits = new List<WarfareUnitSyncData>();

		// Token: 0x0400855D RID: 34141
		public List<WarfareSyncData.MovedUnit> movedUnits = new List<WarfareSyncData.MovedUnit>();

		// Token: 0x0400855E RID: 34142
		public List<WarfareUnitData> newUnits = new List<WarfareUnitData>();

		// Token: 0x0400855F RID: 34143
		public List<int> retreaters = new List<int>();

		// Token: 0x04008560 RID: 34144
		public List<WarfareTileData> tiles = new List<WarfareTileData>();

		// Token: 0x04008561 RID: 34145
		public WarfareGameSyncData gameState = new WarfareGameSyncData();

		// Token: 0x02001A27 RID: 6695
		public sealed class MovedUnit : ISerializable
		{
			// Token: 0x0600BB39 RID: 47929 RVA: 0x0036E897 File Offset: 0x0036CA97
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGet(ref this.unitUID);
				stream.PutOrGet(ref this.tileIndex);
			}

			// Token: 0x0400ADCF RID: 44495
			public int unitUID;

			// Token: 0x0400ADD0 RID: 44496
			public short tileIndex;
		}
	}
}

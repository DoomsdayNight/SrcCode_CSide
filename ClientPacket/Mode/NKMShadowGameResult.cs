using System;
using Cs.Protocol;
using NKM.Templet;

namespace ClientPacket.Mode
{
	// Token: 0x02000E46 RID: 3654
	public sealed class NKMShadowGameResult : ISerializable
	{
		// Token: 0x0600977C RID: 38780 RVA: 0x0032D024 File Offset: 0x0032B224
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.palaceId);
			stream.PutOrGet<NKMPalaceDungeonData>(ref this.dungeonData);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.newRecord);
			stream.PutOrGet(ref this.currentDungeonId);
			stream.PutOrGet(ref this.life);
		}

		// Token: 0x0400899E RID: 35230
		public int palaceId;

		// Token: 0x0400899F RID: 35231
		public NKMPalaceDungeonData dungeonData = new NKMPalaceDungeonData();

		// Token: 0x040089A0 RID: 35232
		public NKMRewardData rewardData;

		// Token: 0x040089A1 RID: 35233
		public bool newRecord;

		// Token: 0x040089A2 RID: 35234
		public int currentDungeonId;

		// Token: 0x040089A3 RID: 35235
		public int life;
	}
}

using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200104D RID: 4173
	public sealed class PrivateGuildData : ISerializable
	{
		// Token: 0x06009B5A RID: 39770 RVA: 0x00332D31 File Offset: 0x00330F31
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.donationCount);
			stream.PutOrGet(ref this.lastDailyResetDate);
			stream.PutOrGet(ref this.guildJoinDisableTime);
		}

		// Token: 0x04008F1A RID: 36634
		public long guildUid;

		// Token: 0x04008F1B RID: 36635
		public int donationCount;

		// Token: 0x04008F1C RID: 36636
		public DateTime lastDailyResetDate;

		// Token: 0x04008F1D RID: 36637
		public DateTime guildJoinDisableTime;
	}
}

using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EFF RID: 3839
	[PacketId(ClientPacketId.kNKMPacket_GUILD_ATTENDANCE_ACK)]
	public sealed class NKMPacket_GUILD_ATTENDANCE_ACK : ISerializable
	{
		// Token: 0x060098DE RID: 39134 RVA: 0x0032F1AC File Offset: 0x0032D3AC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.lastAttendanceDate);
			stream.PutOrGet(ref this.memberJoinDate);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMAdditionalReward>(ref this.additionalReward);
			stream.PutOrGet(ref this.yesterdayAttendanceCount);
			stream.PutOrGet(ref this.todayAttendanceCount);
		}

		// Token: 0x04008B96 RID: 35734
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B97 RID: 35735
		public long guildUid;

		// Token: 0x04008B98 RID: 35736
		public DateTime lastAttendanceDate;

		// Token: 0x04008B99 RID: 35737
		public DateTime memberJoinDate;

		// Token: 0x04008B9A RID: 35738
		public NKMRewardData rewardData;

		// Token: 0x04008B9B RID: 35739
		public NKMAdditionalReward additionalReward = new NKMAdditionalReward();

		// Token: 0x04008B9C RID: 35740
		public int yesterdayAttendanceCount;

		// Token: 0x04008B9D RID: 35741
		public int todayAttendanceCount;
	}
}

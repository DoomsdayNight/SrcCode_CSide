using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F12 RID: 3858
	[PacketId(ClientPacketId.kNKMPacket_GUILD_BUY_WELFARE_POINT_ACK)]
	public sealed class NKMPacket_GUILD_BUY_WELFARE_POINT_ACK : ISerializable
	{
		// Token: 0x06009904 RID: 39172 RVA: 0x0032F57A File Offset: 0x0032D77A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008BCC RID: 35788
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BCD RID: 35789
		public long guildUid;

		// Token: 0x04008BCE RID: 35790
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x04008BCF RID: 35791
		public NKMRewardData rewardData;
	}
}

using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F10 RID: 3856
	[PacketId(ClientPacketId.kNKMPacket_GUILD_BUY_BUFF_ACK)]
	public sealed class NKMPacket_GUILD_BUY_BUFF_ACK : ISerializable
	{
		// Token: 0x06009900 RID: 39168 RVA: 0x0032F4F0 File Offset: 0x0032D6F0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.welfareId);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.unionPoint);
		}

		// Token: 0x04008BC4 RID: 35780
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BC5 RID: 35781
		public long guildUid;

		// Token: 0x04008BC6 RID: 35782
		public int welfareId;

		// Token: 0x04008BC7 RID: 35783
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x04008BC8 RID: 35784
		public NKMRewardData rewardData;

		// Token: 0x04008BC9 RID: 35785
		public long unionPoint;
	}
}

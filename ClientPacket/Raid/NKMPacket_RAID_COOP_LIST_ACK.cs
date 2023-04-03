using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D5E RID: 3422
	[PacketId(ClientPacketId.kNKMPacket_RAID_COOP_LIST_ACK)]
	public sealed class NKMPacket_RAID_COOP_LIST_ACK : ISerializable
	{
		// Token: 0x060095B7 RID: 38327 RVA: 0x0032A9F7 File Offset: 0x00328BF7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMCoopRaidData>(ref this.coopRaidDataList);
		}

		// Token: 0x04008795 RID: 34709
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008796 RID: 34710
		public List<NKMCoopRaidData> coopRaidDataList = new List<NKMCoopRaidData>();
	}
}

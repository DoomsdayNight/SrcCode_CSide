using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D5C RID: 3420
	[PacketId(ClientPacketId.kNKMPacket_MY_RAID_LIST_ACK)]
	public sealed class NKMPacket_MY_RAID_LIST_ACK : ISerializable
	{
		// Token: 0x060095B3 RID: 38323 RVA: 0x0032A9C0 File Offset: 0x00328BC0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMMyRaidData>(ref this.myRaidDataList);
		}

		// Token: 0x04008793 RID: 34707
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008794 RID: 34708
		public List<NKMMyRaidData> myRaidDataList = new List<NKMMyRaidData>();
	}
}

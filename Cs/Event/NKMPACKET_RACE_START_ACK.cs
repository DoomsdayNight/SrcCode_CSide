using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F92 RID: 3986
	[PacketId(ClientPacketId.kNKMPACKET_RACE_START_ACK)]
	public sealed class NKMPACKET_RACE_START_ACK : ISerializable
	{
		// Token: 0x060099FE RID: 39422 RVA: 0x00330C64 File Offset: 0x0032EE64
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isWin);
			stream.PutOrGet(ref this.selectLine);
			stream.PutOrGet<NKMRacePrivate>(ref this.racePrivate);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemList);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008D1F RID: 36127
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D20 RID: 36128
		public bool isWin;

		// Token: 0x04008D21 RID: 36129
		public int selectLine;

		// Token: 0x04008D22 RID: 36130
		public NKMRacePrivate racePrivate = new NKMRacePrivate();

		// Token: 0x04008D23 RID: 36131
		public List<NKMItemMiscData> costItemList = new List<NKMItemMiscData>();

		// Token: 0x04008D24 RID: 36132
		public NKMRewardData rewardData;
	}
}

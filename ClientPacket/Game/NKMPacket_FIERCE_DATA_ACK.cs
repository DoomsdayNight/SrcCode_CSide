using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F5A RID: 3930
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_DATA_ACK)]
	public sealed class NKMPacket_FIERCE_DATA_ACK : ISerializable
	{
		// Token: 0x06009994 RID: 39316 RVA: 0x003302E4 File Offset: 0x0032E4E4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.rankNumber);
			stream.PutOrGet(ref this.rankPercent);
			stream.PutOrGet(ref this.pointRewardHistory);
			stream.PutOrGet(ref this.isRankRewardGotten);
			stream.PutOrGet<NKMFierceBoss>(ref this.bossList);
		}

		// Token: 0x04008C96 RID: 35990
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C97 RID: 35991
		public int rankNumber;

		// Token: 0x04008C98 RID: 35992
		public int rankPercent;

		// Token: 0x04008C99 RID: 35993
		public HashSet<int> pointRewardHistory = new HashSet<int>();

		// Token: 0x04008C9A RID: 35994
		public bool isRankRewardGotten;

		// Token: 0x04008C9B RID: 35995
		public List<NKMFierceBoss> bossList = new List<NKMFierceBoss>();
	}
}

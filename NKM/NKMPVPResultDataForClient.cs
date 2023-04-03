using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000454 RID: 1108
	public class NKMPVPResultDataForClient : ISerializable
	{
		// Token: 0x06001E19 RID: 7705 RVA: 0x0008EEC4 File Offset: 0x0008D0C4
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<PVP_RESULT>(ref this.result);
			stream.PutOrGet<PvpState>(ref this.myInfo);
			stream.PutOrGet<PvpSingleHistory>(ref this.history);
			stream.PutOrGet<NKMItemMiscData>(ref this.pvpPoint);
			stream.PutOrGet(ref this.pvpPointChargeTime);
			stream.PutOrGet(ref this.rankPvpOpen);
			stream.PutOrGet(ref this.leaguePvpOpen);
			stream.PutOrGet<NKMItemMiscData>(ref this.pvpChargePoint);
		}

		// Token: 0x04001EC7 RID: 7879
		public PVP_RESULT result;

		// Token: 0x04001EC8 RID: 7880
		public PvpState myInfo;

		// Token: 0x04001EC9 RID: 7881
		public PvpSingleHistory history;

		// Token: 0x04001ECA RID: 7882
		public NKMItemMiscData pvpPoint;

		// Token: 0x04001ECB RID: 7883
		public DateTime pvpPointChargeTime;

		// Token: 0x04001ECC RID: 7884
		public bool rankPvpOpen;

		// Token: 0x04001ECD RID: 7885
		public bool leaguePvpOpen;

		// Token: 0x04001ECE RID: 7886
		public List<NKMItemMiscData> pvpChargePoint = new List<NKMItemMiscData>();
	}
}

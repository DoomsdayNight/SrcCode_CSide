using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E45 RID: 3653
	public sealed class NKMShadowPalace : ISerializable
	{
		// Token: 0x0600977A RID: 38778 RVA: 0x0032CFD8 File Offset: 0x0032B1D8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.currentPalaceId);
			stream.PutOrGet(ref this.life);
			stream.PutOrGet<NKMPalaceData>(ref this.palaceDataList);
			stream.PutOrGet(ref this.rewardMultiply);
		}

		// Token: 0x0400899A RID: 35226
		public int currentPalaceId;

		// Token: 0x0400899B RID: 35227
		public int life;

		// Token: 0x0400899C RID: 35228
		public List<NKMPalaceData> palaceDataList = new List<NKMPalaceData>();

		// Token: 0x0400899D RID: 35229
		public int rewardMultiply = 1;
	}
}

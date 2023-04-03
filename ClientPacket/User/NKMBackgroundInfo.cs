using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CAB RID: 3243
	public sealed class NKMBackgroundInfo : ISerializable
	{
		// Token: 0x06009453 RID: 37971 RVA: 0x00328879 File Offset: 0x00326A79
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.backgroundItemId);
			stream.PutOrGet(ref this.backgroundBgmId);
			stream.PutOrGet<NKMBackgroundUnitInfo>(ref this.unitInfoList);
		}

		// Token: 0x040085AB RID: 34219
		public int backgroundItemId;

		// Token: 0x040085AC RID: 34220
		public int backgroundBgmId;

		// Token: 0x040085AD RID: 34221
		public List<NKMBackgroundUnitInfo> unitInfoList = new List<NKMBackgroundUnitInfo>();
	}
}

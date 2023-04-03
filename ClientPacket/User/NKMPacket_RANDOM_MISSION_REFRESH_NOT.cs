using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC6 RID: 3270
	[PacketId(ClientPacketId.kNKMPacket_RANDOM_MISSION_REFRESH_NOT)]
	public sealed class NKMPacket_RANDOM_MISSION_REFRESH_NOT : ISerializable
	{
		// Token: 0x06009489 RID: 38025 RVA: 0x00328DD7 File Offset: 0x00326FD7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tabId);
			stream.PutOrGet<NKMMissionData>(ref this.missionDataList);
		}

		// Token: 0x040085FE RID: 34302
		public int tabId;

		// Token: 0x040085FF RID: 34303
		public List<NKMMissionData> missionDataList = new List<NKMMissionData>();
	}
}

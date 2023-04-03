using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CBF RID: 3263
	[PacketId(ClientPacketId.kNKMPacket_MISSION_UPDATE_NOT)]
	public sealed class NKMPacket_MISSION_UPDATE_NOT : ISerializable
	{
		// Token: 0x0600947B RID: 38011 RVA: 0x00328C75 File Offset: 0x00326E75
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMMissionData>(ref this.missionDataList);
		}

		// Token: 0x040085EA RID: 34282
		public HashSet<NKMMissionData> missionDataList = new HashSet<NKMMissionData>();
	}
}

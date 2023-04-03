using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C82 RID: 3202
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_MISSION_COMPLETE_ACK)]
	public sealed class NKMPacket_WORLDMAP_MISSION_COMPLETE_ACK : ISerializable
	{
		// Token: 0x06009403 RID: 37891 RVA: 0x00327E68 File Offset: 0x00326068
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.clearedMissionID);
			stream.PutOrGet(ref this.level);
			stream.PutOrGet(ref this.exp);
			stream.PutOrGet(ref this.stMissionIDList);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.isSuccess);
			stream.PutOrGet<NKMWorldMapEventGroup>(ref this.worldMapEventGroup);
		}

		// Token: 0x04008510 RID: 34064
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008511 RID: 34065
		public int cityID;

		// Token: 0x04008512 RID: 34066
		public int clearedMissionID;

		// Token: 0x04008513 RID: 34067
		public int level;

		// Token: 0x04008514 RID: 34068
		public int exp;

		// Token: 0x04008515 RID: 34069
		public List<int> stMissionIDList = new List<int>();

		// Token: 0x04008516 RID: 34070
		public NKMRewardData rewardData;

		// Token: 0x04008517 RID: 34071
		public bool isSuccess;

		// Token: 0x04008518 RID: 34072
		public NKMWorldMapEventGroup worldMapEventGroup = new NKMWorldMapEventGroup();
	}
}

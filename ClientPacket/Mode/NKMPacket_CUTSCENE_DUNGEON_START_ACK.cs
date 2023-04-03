using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E2F RID: 3631
	[PacketId(ClientPacketId.kNKMPacket_CUTSCENE_DUNGEON_START_ACK)]
	public sealed class NKMPacket_CUTSCENE_DUNGEON_START_ACK : ISerializable
	{
		// Token: 0x0600974E RID: 38734 RVA: 0x0032CCC2 File Offset: 0x0032AEC2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMStagePlayData>(ref this.stagePlayData);
		}

		// Token: 0x04008970 RID: 35184
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008971 RID: 35185
		public NKMStagePlayData stagePlayData = new NKMStagePlayData();
	}
}

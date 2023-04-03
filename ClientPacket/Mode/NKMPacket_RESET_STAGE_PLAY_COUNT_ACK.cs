using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E42 RID: 3650
	[PacketId(ClientPacketId.kNKMPacket_RESET_STAGE_PLAY_COUNT_ACK)]
	public sealed class NKMPacket_RESET_STAGE_PLAY_COUNT_ACK : ISerializable
	{
		// Token: 0x06009774 RID: 38772 RVA: 0x0032CF38 File Offset: 0x0032B138
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMStagePlayData>(ref this.stagePlayData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008991 RID: 35217
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008992 RID: 35218
		public NKMStagePlayData stagePlayData = new NKMStagePlayData();

		// Token: 0x04008993 RID: 35219
		public NKMItemMiscData costItemData;
	}
}

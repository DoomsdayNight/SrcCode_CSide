using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E48 RID: 3656
	[PacketId(ClientPacketId.kNKMPacket_SHADOW_PALACE_START_ACK)]
	public sealed class NKMPacket_SHADOW_PALACE_START_ACK : ISerializable
	{
		// Token: 0x06009780 RID: 38784 RVA: 0x0032D0A2 File Offset: 0x0032B2A2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.currentPalaceId);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet(ref this.rewardMultiply);
		}

		// Token: 0x040089A5 RID: 35237
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089A6 RID: 35238
		public int currentPalaceId;

		// Token: 0x040089A7 RID: 35239
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x040089A8 RID: 35240
		public int rewardMultiply = 1;
	}
}

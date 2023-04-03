using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E94 RID: 3732
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_COMPLETE_ACK)]
	public sealed class NKMPacket_CRAFT_COMPLETE_ACK : ISerializable
	{
		// Token: 0x06009814 RID: 38932 RVA: 0x0032DEBC File Offset: 0x0032C0BC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMCraftSlotData>(ref this.craftSlotData);
			stream.PutOrGet<NKMRewardData>(ref this.createdRewardData);
		}

		// Token: 0x04008A67 RID: 35431
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A68 RID: 35432
		public NKMCraftSlotData craftSlotData;

		// Token: 0x04008A69 RID: 35433
		public NKMRewardData createdRewardData;
	}
}

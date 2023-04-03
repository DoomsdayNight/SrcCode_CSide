using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA4 RID: 3748
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_ACK)]
	public sealed class NKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_ACK : ISerializable
	{
		// Token: 0x06009834 RID: 38964 RVA: 0x0032E1E6 File Offset: 0x0032C3E6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.equipUID);
			stream.PutOrGet(ref this.setOptionId);
		}

		// Token: 0x04008A95 RID: 35477
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A96 RID: 35478
		public long equipUID;

		// Token: 0x04008A97 RID: 35479
		public int setOptionId;
	}
}

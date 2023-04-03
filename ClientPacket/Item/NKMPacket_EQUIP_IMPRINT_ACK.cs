using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB9 RID: 3769
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_IMPRINT_ACK)]
	public sealed class NKMPacket_EQUIP_IMPRINT_ACK : ISerializable
	{
		// Token: 0x0600985E RID: 39006 RVA: 0x0032E4FB File Offset: 0x0032C6FB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipItemData>(ref this.equipItemData);
		}

		// Token: 0x04008ABD RID: 35517
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008ABE RID: 35518
		public NKMEquipItemData equipItemData;
	}
}

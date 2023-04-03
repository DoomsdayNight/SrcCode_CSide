using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF7 RID: 3319
	[PacketId(ClientPacketId.kNKMPacket_CONTRACT_PERMANENTLY_ACK)]
	public sealed class NKMPacket_CONTRACT_PERMANENTLY_ACK : ISerializable
	{
		// Token: 0x060094EB RID: 38123 RVA: 0x00329645 File Offset: 0x00327845
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008671 RID: 34417
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008672 RID: 34418
		public long unitUID;

		// Token: 0x04008673 RID: 34419
		public NKMItemMiscData costItemData;
	}
}

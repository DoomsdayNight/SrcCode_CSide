using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D7A RID: 3450
	[PacketId(ClientPacketId.kNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)]
	public sealed class NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK : ISerializable
	{
		// Token: 0x060095EF RID: 38383 RVA: 0x0032AFED File Offset: 0x003291ED
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.itemData);
			stream.PutOrGet(ref this.chrageTime);
		}

		// Token: 0x040087E8 RID: 34792
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087E9 RID: 34793
		public NKMItemMiscData itemData;

		// Token: 0x040087EA RID: 34794
		public DateTime chrageTime;
	}
}

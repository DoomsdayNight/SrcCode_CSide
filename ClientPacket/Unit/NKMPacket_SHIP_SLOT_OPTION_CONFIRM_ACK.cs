using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D17 RID: 3351
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_OPTION_CONFIRM_ACK)]
	public sealed class NKMPacket_SHIP_SLOT_OPTION_CONFIRM_ACK : ISerializable
	{
		// Token: 0x0600952B RID: 38187 RVA: 0x00329C54 File Offset: 0x00327E54
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipData);
		}

		// Token: 0x040086C8 RID: 34504
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086C9 RID: 34505
		public NKMUnitData shipData;
	}
}

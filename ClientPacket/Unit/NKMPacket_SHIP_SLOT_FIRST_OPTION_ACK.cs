using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D19 RID: 3353
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_FIRST_OPTION_ACK)]
	public sealed class NKMPacket_SHIP_SLOT_FIRST_OPTION_ACK : ISerializable
	{
		// Token: 0x0600952F RID: 38191 RVA: 0x00329C98 File Offset: 0x00327E98
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipData);
		}

		// Token: 0x040086CC RID: 34508
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086CD RID: 34509
		public NKMUnitData shipData;
	}
}

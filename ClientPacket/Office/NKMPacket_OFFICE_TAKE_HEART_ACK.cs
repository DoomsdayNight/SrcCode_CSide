using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E03 RID: 3587
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_TAKE_HEART_ACK)]
	public sealed class NKMPacket_OFFICE_TAKE_HEART_ACK : ISerializable
	{
		// Token: 0x060096FA RID: 38650 RVA: 0x0032C5BD File Offset: 0x0032A7BD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.unit);
		}

		// Token: 0x0400890F RID: 35087
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008910 RID: 35088
		public NKMUnitData unit;
	}
}

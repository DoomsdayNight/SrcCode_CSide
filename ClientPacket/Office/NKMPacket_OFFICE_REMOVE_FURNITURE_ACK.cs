using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DFF RID: 3583
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_REMOVE_FURNITURE_ACK)]
	public sealed class NKMPacket_OFFICE_REMOVE_FURNITURE_ACK : ISerializable
	{
		// Token: 0x060096F2 RID: 38642 RVA: 0x0032C4CF File Offset: 0x0032A6CF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.furnitureUid);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMInteriorData>(ref this.changedInterior);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
		}

		// Token: 0x04008904 RID: 35076
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008905 RID: 35077
		public long furnitureUid;

		// Token: 0x04008906 RID: 35078
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x04008907 RID: 35079
		public NKMInteriorData changedInterior = new NKMInteriorData();

		// Token: 0x04008908 RID: 35080
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();
	}
}

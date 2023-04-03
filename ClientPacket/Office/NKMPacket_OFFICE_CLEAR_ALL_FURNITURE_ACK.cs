using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E01 RID: 3585
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_CLEAR_ALL_FURNITURE_ACK)]
	public sealed class NKMPacket_OFFICE_CLEAR_ALL_FURNITURE_ACK : ISerializable
	{
		// Token: 0x060096F6 RID: 38646 RVA: 0x0032C54C File Offset: 0x0032A74C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMInteriorData>(ref this.changedInteriors);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
		}

		// Token: 0x0400890A RID: 35082
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400890B RID: 35083
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x0400890C RID: 35084
		public List<NKMInteriorData> changedInteriors = new List<NKMInteriorData>();

		// Token: 0x0400890D RID: 35085
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();
	}
}

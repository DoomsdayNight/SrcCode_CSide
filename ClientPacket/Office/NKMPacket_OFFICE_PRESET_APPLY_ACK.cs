using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E1D RID: 3613
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_APPLY_ACK)]
	public sealed class NKMPacket_OFFICE_PRESET_APPLY_ACK : ISerializable
	{
		// Token: 0x0600972E RID: 38702 RVA: 0x0032CA04 File Offset: 0x0032AC04
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.presetId);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
			stream.PutOrGet<NKMInteriorData>(ref this.changedInteriors);
		}

		// Token: 0x04008945 RID: 35141
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008946 RID: 35142
		public int presetId;

		// Token: 0x04008947 RID: 35143
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x04008948 RID: 35144
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();

		// Token: 0x04008949 RID: 35145
		public List<NKMInteriorData> changedInteriors = new List<NKMInteriorData>();
	}
}

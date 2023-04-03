using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E25 RID: 3621
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_APPLY_THEMA_ACK)]
	public sealed class NKMPacket_OFFICE_PRESET_APPLY_THEMA_ACK : ISerializable
	{
		// Token: 0x0600973E RID: 38718 RVA: 0x0032CB64 File Offset: 0x0032AD64
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.themaIndex);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
			stream.PutOrGet<NKMInteriorData>(ref this.changedInteriors);
		}

		// Token: 0x04008958 RID: 35160
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008959 RID: 35161
		public int themaIndex;

		// Token: 0x0400895A RID: 35162
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x0400895B RID: 35163
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();

		// Token: 0x0400895C RID: 35164
		public List<NKMInteriorData> changedInteriors = new List<NKMInteriorData>();
	}
}

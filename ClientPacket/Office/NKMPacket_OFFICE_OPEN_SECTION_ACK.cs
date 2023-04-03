using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DED RID: 3565
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_OPEN_SECTION_ACK)]
	public sealed class NKMPacket_OFFICE_OPEN_SECTION_ACK : ISerializable
	{
		// Token: 0x060096CE RID: 38606 RVA: 0x0032C09F File Offset: 0x0032A29F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet(ref this.sectionId);
			stream.PutOrGet<NKMOfficeRoom>(ref this.newRooms);
		}

		// Token: 0x040088CE RID: 35022
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088CF RID: 35023
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x040088D0 RID: 35024
		public int sectionId;

		// Token: 0x040088D1 RID: 35025
		public List<NKMOfficeRoom> newRooms = new List<NKMOfficeRoom>();
	}
}

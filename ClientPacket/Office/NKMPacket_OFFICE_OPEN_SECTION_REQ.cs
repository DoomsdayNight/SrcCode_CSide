using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DEC RID: 3564
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_OPEN_SECTION_REQ)]
	public sealed class NKMPacket_OFFICE_OPEN_SECTION_REQ : ISerializable
	{
		// Token: 0x060096CC RID: 38604 RVA: 0x0032C089 File Offset: 0x0032A289
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.sectionId);
		}

		// Token: 0x040088CD RID: 35021
		public int sectionId;
	}
}

using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF2 RID: 3570
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_UNIT_REQ)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_UNIT_REQ : ISerializable
	{
		// Token: 0x060096D8 RID: 38616 RVA: 0x0032C198 File Offset: 0x0032A398
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.unitUids);
		}

		// Token: 0x040088DA RID: 35034
		public int roomId;

		// Token: 0x040088DB RID: 35035
		public List<long> unitUids = new List<long>();
	}
}

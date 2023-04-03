using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E02 RID: 3586
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_TAKE_HEART_REQ)]
	public sealed class NKMPacket_OFFICE_TAKE_HEART_REQ : ISerializable
	{
		// Token: 0x060096F8 RID: 38648 RVA: 0x0032C5A7 File Offset: 0x0032A7A7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
		}

		// Token: 0x0400890E RID: 35086
		public long unitUid;
	}
}

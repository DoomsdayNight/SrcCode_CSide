using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E04 RID: 3588
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_STATE_REQ)]
	public sealed class NKMPacket_OFFICE_STATE_REQ : ISerializable
	{
		// Token: 0x060096FC RID: 38652 RVA: 0x0032C5DF File Offset: 0x0032A7DF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008911 RID: 35089
		public long userUid;
	}
}

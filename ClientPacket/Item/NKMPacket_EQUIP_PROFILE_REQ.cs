using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA5 RID: 3749
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PROFILE_REQ)]
	public sealed class NKMPacket_EQUIP_PROFILE_REQ : ISerializable
	{
		// Token: 0x06009836 RID: 38966 RVA: 0x0032E214 File Offset: 0x0032C414
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
		}

		// Token: 0x04008A98 RID: 35480
		public long unitUid;
	}
}

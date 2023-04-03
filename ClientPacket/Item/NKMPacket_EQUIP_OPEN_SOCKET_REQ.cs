using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EBC RID: 3772
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_OPEN_SOCKET_REQ)]
	public sealed class NKMPacket_EQUIP_OPEN_SOCKET_REQ : ISerializable
	{
		// Token: 0x06009864 RID: 39012 RVA: 0x0032E59A File Offset: 0x0032C79A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUid);
			stream.PutOrGet(ref this.socketIndex);
		}

		// Token: 0x04008AC5 RID: 35525
		public long equipUid;

		// Token: 0x04008AC6 RID: 35526
		public int socketIndex;
	}
}

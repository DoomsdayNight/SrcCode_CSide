using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DFE RID: 3582
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_REMOVE_FURNITURE_REQ)]
	public sealed class NKMPacket_OFFICE_REMOVE_FURNITURE_REQ : ISerializable
	{
		// Token: 0x060096F0 RID: 38640 RVA: 0x0032C4AD File Offset: 0x0032A6AD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.furnitureUid);
		}

		// Token: 0x04008902 RID: 35074
		public int roomId;

		// Token: 0x04008903 RID: 35075
		public long furnitureUid;
	}
}

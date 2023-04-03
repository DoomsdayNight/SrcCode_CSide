using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E8D RID: 3725
	[PacketId(ClientPacketId.kNKMPacket_RANDOM_ITEM_BOX_OPEN_REQ)]
	public sealed class NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ : ISerializable
	{
		// Token: 0x06009806 RID: 38918 RVA: 0x0032DDAC File Offset: 0x0032BFAC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemID);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008A58 RID: 35416
		public int itemID;

		// Token: 0x04008A59 RID: 35417
		public int count;
	}
}

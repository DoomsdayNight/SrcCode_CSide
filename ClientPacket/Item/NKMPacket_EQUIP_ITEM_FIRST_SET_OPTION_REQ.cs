using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA3 RID: 3747
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ)]
	public sealed class NKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ : ISerializable
	{
		// Token: 0x06009832 RID: 38962 RVA: 0x0032E1D0 File Offset: 0x0032C3D0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUID);
		}

		// Token: 0x04008A94 RID: 35476
		public long equipUID;
	}
}

using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E9F RID: 3743
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ)]
	public sealed class NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ : ISerializable
	{
		// Token: 0x0600982A RID: 38954 RVA: 0x0032E103 File Offset: 0x0032C303
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUID);
		}

		// Token: 0x04008A89 RID: 35465
		public long equipUID;
	}
}

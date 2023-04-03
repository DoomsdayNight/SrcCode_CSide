using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA1 RID: 3745
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ)]
	public sealed class NKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ : ISerializable
	{
		// Token: 0x0600982E RID: 38958 RVA: 0x0032E175 File Offset: 0x0032C375
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUID);
		}

		// Token: 0x04008A8F RID: 35471
		public long equipUID;
	}
}

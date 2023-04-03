using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA7 RID: 3751
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_LIST_REQ)]
	public sealed class NKMPacket_EQUIP_PRESET_LIST_REQ : ISerializable
	{
		// Token: 0x0600983A RID: 38970 RVA: 0x0032E257 File Offset: 0x0032C457
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

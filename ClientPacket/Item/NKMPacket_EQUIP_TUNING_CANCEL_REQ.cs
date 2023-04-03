using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB5 RID: 3765
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_CANCEL_REQ)]
	public sealed class NKMPacket_EQUIP_TUNING_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009856 RID: 38998 RVA: 0x0032E475 File Offset: 0x0032C675
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

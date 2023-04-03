using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C75 RID: 3189
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_INFO_REQ)]
	public sealed class NKMPacket_WORLDMAP_INFO_REQ : ISerializable
	{
		// Token: 0x060093E9 RID: 37865 RVA: 0x00327C73 File Offset: 0x00325E73
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

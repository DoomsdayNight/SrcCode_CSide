using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C85 RID: 3205
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_COLLECT_REQ)]
	public sealed class NKMPacket_WORLDMAP_COLLECT_REQ : ISerializable
	{
		// Token: 0x06009409 RID: 37897 RVA: 0x00327F37 File Offset: 0x00326137
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

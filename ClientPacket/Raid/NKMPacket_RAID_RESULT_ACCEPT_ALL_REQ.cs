using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D67 RID: 3431
	[PacketId(ClientPacketId.kNKMPacket_RAID_RESULT_ACCEPT_ALL_REQ)]
	public sealed class NKMPacket_RAID_RESULT_ACCEPT_ALL_REQ : ISerializable
	{
		// Token: 0x060095C9 RID: 38345 RVA: 0x0032AB3D File Offset: 0x00328D3D
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

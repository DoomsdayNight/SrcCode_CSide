using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D63 RID: 3427
	[PacketId(ClientPacketId.kNKMPacket_RAID_RESULT_LIST_REQ)]
	public sealed class NKMPacket_RAID_RESULT_LIST_REQ : ISerializable
	{
		// Token: 0x060095C1 RID: 38337 RVA: 0x0032AAB6 File Offset: 0x00328CB6
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

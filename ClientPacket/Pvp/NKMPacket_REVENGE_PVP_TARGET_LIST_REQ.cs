using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DDD RID: 3549
	[PacketId(ClientPacketId.kNKMPacket_REVENGE_PVP_TARGET_LIST_REQ)]
	public sealed class NKMPacket_REVENGE_PVP_TARGET_LIST_REQ : ISerializable
	{
		// Token: 0x060096B1 RID: 38577 RVA: 0x0032BD16 File Offset: 0x00329F16
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

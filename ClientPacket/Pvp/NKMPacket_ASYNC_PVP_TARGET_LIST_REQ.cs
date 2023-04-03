using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D80 RID: 3456
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_TARGET_LIST_REQ)]
	public sealed class NKMPacket_ASYNC_PVP_TARGET_LIST_REQ : ISerializable
	{
		// Token: 0x060095FB RID: 38395 RVA: 0x0032B0D0 File Offset: 0x003292D0
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

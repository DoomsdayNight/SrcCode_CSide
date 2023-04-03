using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E58 RID: 3672
	[PacketId(ClientPacketId.kNKMPacket_TRIM_RETRY_REQ)]
	public sealed class NKMPacket_TRIM_RETRY_REQ : ISerializable
	{
		// Token: 0x060097A0 RID: 38816 RVA: 0x0032D3BA File Offset: 0x0032B5BA
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

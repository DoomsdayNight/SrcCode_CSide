using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E5F RID: 3679
	[PacketId(ClientPacketId.kNKMPacket_FAVORITES_STAGE_REQ)]
	public sealed class NKMPacket_FAVORITES_STAGE_REQ : ISerializable
	{
		// Token: 0x060097AE RID: 38830 RVA: 0x0032D4F6 File Offset: 0x0032B6F6
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

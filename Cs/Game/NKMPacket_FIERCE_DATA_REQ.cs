using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F59 RID: 3929
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_DATA_REQ)]
	public sealed class NKMPacket_FIERCE_DATA_REQ : ISerializable
	{
		// Token: 0x06009992 RID: 39314 RVA: 0x003302DA File Offset: 0x0032E4DA
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

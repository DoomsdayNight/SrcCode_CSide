using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA6 RID: 3238
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_FRIEND_LIST_REQ)]
	public sealed class NKMPacket_WARFARE_FRIEND_LIST_REQ : ISerializable
	{
		// Token: 0x06009449 RID: 37961 RVA: 0x0032874F File Offset: 0x0032694F
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

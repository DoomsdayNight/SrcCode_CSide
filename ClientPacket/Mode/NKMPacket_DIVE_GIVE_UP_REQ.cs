using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E38 RID: 3640
	[PacketId(ClientPacketId.kNKMPacket_DIVE_GIVE_UP_REQ)]
	public sealed class NKMPacket_DIVE_GIVE_UP_REQ : ISerializable
	{
		// Token: 0x06009760 RID: 38752 RVA: 0x0032CE44 File Offset: 0x0032B044
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

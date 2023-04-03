using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD5 RID: 3285
	[PacketId(ClientPacketId.kNKMPacket_REFRESH_COMPANY_BUFF_REQ)]
	public sealed class NKMPacket_REFRESH_COMPANY_BUFF_REQ : ISerializable
	{
		// Token: 0x060094A7 RID: 38055 RVA: 0x003290B5 File Offset: 0x003272B5
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E0F RID: 3599
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_RANDOM_VISIT_REQ)]
	public sealed class NKMPacket_OFFICE_RANDOM_VISIT_REQ : ISerializable
	{
		// Token: 0x06009712 RID: 38674 RVA: 0x0032C7B9 File Offset: 0x0032A9B9
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

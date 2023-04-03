using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D49 RID: 3401
	[PacketId(ClientPacketId.kNKMPacket_INQUIRY_RESPONDED_NOT)]
	public sealed class NKMPacket_INQUIRY_RESPONDED_NOT : ISerializable
	{
		// Token: 0x0600958F RID: 38287 RVA: 0x0032A4F4 File Offset: 0x003286F4
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

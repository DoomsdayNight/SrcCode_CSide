using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D50 RID: 3408
	[PacketId(ClientPacketId.kNKMPacket_SURVEY_RESET_NOT)]
	public sealed class NKMPacket_SURVEY_RESET_NOT : ISerializable
	{
		// Token: 0x0600959D RID: 38301 RVA: 0x0032A58D File Offset: 0x0032878D
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}

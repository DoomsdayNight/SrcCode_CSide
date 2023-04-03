using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001081 RID: 4225
	[PacketId(ClientPacketId.kNKMPacket_NEXON_NGS_DATA_NOT)]
	public sealed class NKMPacket_NEXON_NGS_DATA_NOT : ISerializable
	{
		// Token: 0x06009BBF RID: 39871 RVA: 0x00333CCF File Offset: 0x00331ECF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.buffer);
		}

		// Token: 0x04009002 RID: 36866
		public byte[] buffer;
	}
}

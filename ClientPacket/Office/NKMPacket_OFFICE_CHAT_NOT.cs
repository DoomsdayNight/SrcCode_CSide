using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E15 RID: 3605
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_CHAT_NOT)]
	public sealed class NKMPacket_OFFICE_CHAT_NOT : ISerializable
	{
		// Token: 0x0600971E RID: 38686 RVA: 0x0032C8B1 File Offset: 0x0032AAB1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMOfficeChatMessageData>(ref this.message);
		}

		// Token: 0x04008934 RID: 35124
		public NKMOfficeChatMessageData message = new NKMOfficeChatMessageData();
	}
}

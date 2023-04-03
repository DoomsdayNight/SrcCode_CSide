using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F52 RID: 3922
	[PacketId(ClientPacketId.kNKMPacket_GAME_EMOTICON_NOT)]
	public sealed class NKMPacket_GAME_EMOTICON_NOT : ISerializable
	{
		// Token: 0x06009984 RID: 39300 RVA: 0x00330204 File Offset: 0x0032E404
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.senderUserUID);
			stream.PutOrGet(ref this.emoticonID);
		}

		// Token: 0x04008C8A RID: 35978
		public long senderUserUID;

		// Token: 0x04008C8B RID: 35979
		public int emoticonID;
	}
}

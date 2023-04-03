using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F50 RID: 3920
	[PacketId(ClientPacketId.kNKMPacket_GAME_EMOTICON_REQ)]
	public sealed class NKMPacket_GAME_EMOTICON_REQ : ISerializable
	{
		// Token: 0x06009980 RID: 39296 RVA: 0x003301D8 File Offset: 0x0032E3D8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.emoticonID);
		}

		// Token: 0x04008C88 RID: 35976
		public int emoticonID;
	}
}

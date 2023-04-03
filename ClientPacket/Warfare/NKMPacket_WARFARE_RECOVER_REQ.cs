using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA8 RID: 3240
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_RECOVER_REQ)]
	public sealed class NKMPacket_WARFARE_RECOVER_REQ : ISerializable
	{
		// Token: 0x0600944D RID: 37965 RVA: 0x0032879D File Offset: 0x0032699D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.deckIndex);
			stream.PutOrGet(ref this.tileIndex);
		}

		// Token: 0x0400859E RID: 34206
		public byte deckIndex;

		// Token: 0x0400859F RID: 34207
		public short tileIndex;
	}
}

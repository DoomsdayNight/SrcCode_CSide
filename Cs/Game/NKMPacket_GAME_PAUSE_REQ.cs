using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F39 RID: 3897
	[PacketId(ClientPacketId.kNKMPacket_GAME_PAUSE_REQ)]
	public sealed class NKMPacket_GAME_PAUSE_REQ : ISerializable
	{
		// Token: 0x06009952 RID: 39250 RVA: 0x0032FE71 File Offset: 0x0032E071
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isPause);
			stream.PutOrGet(ref this.isPauseEvent);
		}

		// Token: 0x04008C55 RID: 35925
		public bool isPause;

		// Token: 0x04008C56 RID: 35926
		public bool isPauseEvent;
	}
}

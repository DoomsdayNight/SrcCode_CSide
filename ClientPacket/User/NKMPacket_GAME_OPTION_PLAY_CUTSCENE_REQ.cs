using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CCC RID: 3276
	[PacketId(ClientPacketId.kNKMPacket_GAME_OPTION_PLAY_CUTSCENE_REQ)]
	public sealed class NKMPacket_GAME_OPTION_PLAY_CUTSCENE_REQ : ISerializable
	{
		// Token: 0x06009495 RID: 38037 RVA: 0x00328EF4 File Offset: 0x003270F4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isPlayCutscene);
		}

		// Token: 0x0400860E RID: 34318
		public bool isPlayCutscene;
	}
}

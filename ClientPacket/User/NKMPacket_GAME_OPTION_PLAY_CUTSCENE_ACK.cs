using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CCD RID: 3277
	[PacketId(ClientPacketId.kNKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK)]
	public sealed class NKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK : ISerializable
	{
		// Token: 0x06009497 RID: 38039 RVA: 0x00328F0A File Offset: 0x0032710A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isPlayCutscene);
		}

		// Token: 0x0400860F RID: 34319
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008610 RID: 34320
		public bool isPlayCutscene;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F3A RID: 3898
	[PacketId(ClientPacketId.kNKMPacket_GAME_PAUSE_ACK)]
	public sealed class NKMPacket_GAME_PAUSE_ACK : ISerializable
	{
		// Token: 0x06009954 RID: 39252 RVA: 0x0032FE93 File Offset: 0x0032E093
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isPause);
			stream.PutOrGet(ref this.isPauseEvent);
		}

		// Token: 0x04008C57 RID: 35927
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C58 RID: 35928
		public bool isPause;

		// Token: 0x04008C59 RID: 35929
		public bool isPauseEvent;
	}
}

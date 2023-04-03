using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E41 RID: 3649
	[PacketId(ClientPacketId.kNKMPacket_RESET_STAGE_PLAY_COUNT_REQ)]
	public sealed class NKMPacket_RESET_STAGE_PLAY_COUNT_REQ : ISerializable
	{
		// Token: 0x06009772 RID: 38770 RVA: 0x0032CF22 File Offset: 0x0032B122
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
		}

		// Token: 0x04008990 RID: 35216
		public int stageId;
	}
}

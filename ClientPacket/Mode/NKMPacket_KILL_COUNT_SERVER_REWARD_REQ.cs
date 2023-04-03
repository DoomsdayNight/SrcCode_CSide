using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E53 RID: 3667
	[PacketId(ClientPacketId.kNKMPacket_KILL_COUNT_SERVER_REWARD_REQ)]
	public sealed class NKMPacket_KILL_COUNT_SERVER_REWARD_REQ : ISerializable
	{
		// Token: 0x06009796 RID: 38806 RVA: 0x0032D286 File Offset: 0x0032B486
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.templetId);
			stream.PutOrGet(ref this.stepId);
		}

		// Token: 0x040089BF RID: 35263
		public int templetId;

		// Token: 0x040089C0 RID: 35264
		public int stepId;
	}
}

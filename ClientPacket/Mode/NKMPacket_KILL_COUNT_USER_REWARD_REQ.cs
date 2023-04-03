using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E51 RID: 3665
	[PacketId(ClientPacketId.kNKMPacket_KILL_COUNT_USER_REWARD_REQ)]
	public sealed class NKMPacket_KILL_COUNT_USER_REWARD_REQ : ISerializable
	{
		// Token: 0x06009792 RID: 38802 RVA: 0x0032D22B File Offset: 0x0032B42B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.templetId);
			stream.PutOrGet(ref this.stepId);
		}

		// Token: 0x040089BA RID: 35258
		public int templetId;

		// Token: 0x040089BB RID: 35259
		public int stepId;
	}
}

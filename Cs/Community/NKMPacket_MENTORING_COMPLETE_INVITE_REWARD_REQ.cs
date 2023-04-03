using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001023 RID: 4131
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_COMPLETE_INVITE_REWARD_REQ)]
	public sealed class NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_REQ : ISerializable
	{
		// Token: 0x06009B16 RID: 39702 RVA: 0x00332379 File Offset: 0x00330579
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.inviteSuccessRequireCnt);
		}

		// Token: 0x04008E6E RID: 36462
		public int inviteSuccessRequireCnt;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CCA RID: 3274
	[PacketId(ClientPacketId.kNKMPacket_EPISODE_COMPLETE_REWARD_ALL_REQ)]
	public sealed class NKMPacket_EPISODE_COMPLETE_REWARD_ALL_REQ : ISerializable
	{
		// Token: 0x06009491 RID: 38033 RVA: 0x00328E99 File Offset: 0x00327099
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.episodeID);
		}

		// Token: 0x04008609 RID: 34313
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400860A RID: 34314
		public int episodeID;
	}
}

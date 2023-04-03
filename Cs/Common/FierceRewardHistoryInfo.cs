using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001048 RID: 4168
	public sealed class FierceRewardHistoryInfo : ISerializable
	{
		// Token: 0x06009B50 RID: 39760 RVA: 0x00332C0D File Offset: 0x00330E0D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<FierceRewardType>(ref this.fierceRewardType);
			stream.PutOrGet(ref this.fierceRewardId);
		}

		// Token: 0x04008F07 RID: 36615
		public FierceRewardType fierceRewardType;

		// Token: 0x04008F08 RID: 36616
		public int fierceRewardId;
	}
}

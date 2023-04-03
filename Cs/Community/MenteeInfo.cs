using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FCB RID: 4043
	public sealed class MenteeInfo : ISerializable
	{
		// Token: 0x06009A66 RID: 39526 RVA: 0x00331645 File Offset: 0x0032F845
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<MentoringState>(ref this.state);
			stream.PutOrGet<FriendListData>(ref this.data);
			stream.PutOrGet(ref this.missionCount);
		}

		// Token: 0x04008DBF RID: 36287
		public MentoringState state;

		// Token: 0x04008DC0 RID: 36288
		public FriendListData data;

		// Token: 0x04008DC1 RID: 36289
		public long missionCount;
	}
}

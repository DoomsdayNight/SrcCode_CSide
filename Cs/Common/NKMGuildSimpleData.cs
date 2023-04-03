using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200102F RID: 4143
	public sealed class NKMGuildSimpleData : ISerializable
	{
		// Token: 0x06009B2E RID: 39726 RVA: 0x003324C8 File Offset: 0x003306C8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.guildName);
			stream.PutOrGet(ref this.badgeId);
		}

		// Token: 0x04008E7F RID: 36479
		public long guildUid;

		// Token: 0x04008E80 RID: 36480
		public string guildName;

		// Token: 0x04008E81 RID: 36481
		public long badgeId;
	}
}

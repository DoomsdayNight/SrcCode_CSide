using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001044 RID: 4164
	public sealed class NKMReturningUserState : ISerializable
	{
		// Token: 0x06009B48 RID: 39752 RVA: 0x00332AD0 File Offset: 0x00330CD0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<ReturningUserType>(ref this.type);
			stream.PutOrGet(ref this.startDate);
			stream.PutOrGet(ref this.endDate);
		}

		// Token: 0x04008EF2 RID: 36594
		public ReturningUserType type;

		// Token: 0x04008EF3 RID: 36595
		public DateTime startDate;

		// Token: 0x04008EF4 RID: 36596
		public DateTime endDate;
	}
}

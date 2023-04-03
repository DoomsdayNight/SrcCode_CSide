using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001046 RID: 4166
	public sealed class NKMSkillLevelData : ISerializable
	{
		// Token: 0x06009B4C RID: 39756 RVA: 0x00332B69 File Offset: 0x00330D69
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.normalLv);
			stream.PutOrGet(ref this.passiveLv);
			stream.PutOrGet(ref this.specialLv);
			stream.PutOrGet(ref this.ultimateLv);
			stream.PutOrGet(ref this.leaderLv);
		}

		// Token: 0x04008EFC RID: 36604
		public int normalLv;

		// Token: 0x04008EFD RID: 36605
		public int passiveLv;

		// Token: 0x04008EFE RID: 36606
		public int specialLv;

		// Token: 0x04008EFF RID: 36607
		public int ultimateLv;

		// Token: 0x04008F00 RID: 36608
		public int leaderLv;
	}
}

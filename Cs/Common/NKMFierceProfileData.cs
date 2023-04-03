using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001043 RID: 4163
	public sealed class NKMFierceProfileData : ISerializable
	{
		// Token: 0x06009B46 RID: 39750 RVA: 0x00332A68 File Offset: 0x00330C68
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.fierceBossGroupId);
			stream.PutOrGet<NKMDummyDeckData>(ref this.profileDeck);
			stream.PutOrGet(ref this.operationPower);
			stream.PutOrGet(ref this.totalPoint);
			stream.PutOrGet(ref this.penaltyPoint);
			stream.PutOrGet(ref this.penaltyIds);
		}

		// Token: 0x04008EEC RID: 36588
		public int fierceBossGroupId;

		// Token: 0x04008EED RID: 36589
		public NKMDummyDeckData profileDeck;

		// Token: 0x04008EEE RID: 36590
		public int operationPower;

		// Token: 0x04008EEF RID: 36591
		public int totalPoint;

		// Token: 0x04008EF0 RID: 36592
		public int penaltyPoint;

		// Token: 0x04008EF1 RID: 36593
		public List<int> penaltyIds = new List<int>();
	}
}

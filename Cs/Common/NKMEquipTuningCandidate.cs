using System;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001032 RID: 4146
	public sealed class NKMEquipTuningCandidate : ISerializable
	{
		// Token: 0x06009B34 RID: 39732 RVA: 0x003325BE File Offset: 0x003307BE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUid);
			stream.PutOrGetEnum<NKM_STAT_TYPE>(ref this.option1);
			stream.PutOrGetEnum<NKM_STAT_TYPE>(ref this.option2);
			stream.PutOrGet(ref this.setOptionId);
		}

		// Token: 0x04008E8F RID: 36495
		public long equipUid;

		// Token: 0x04008E90 RID: 36496
		public NKM_STAT_TYPE option1;

		// Token: 0x04008E91 RID: 36497
		public NKM_STAT_TYPE option2;

		// Token: 0x04008E92 RID: 36498
		public int setOptionId;
	}
}

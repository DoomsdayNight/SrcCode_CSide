using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Account
{
	// Token: 0x02001069 RID: 4201
	public sealed class NKMAccountLinkUserProfile : ISerializable
	{
		// Token: 0x06009B8F RID: 39823 RVA: 0x003333B8 File Offset: 0x003315B8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGetEnum<NKM_PUBLISHER_TYPE>(ref this.publisherType);
			stream.PutOrGet(ref this.creditCount);
			stream.PutOrGet(ref this.eterniumCount);
			stream.PutOrGet(ref this.cashCount);
			stream.PutOrGet(ref this.medalCount);
		}

		// Token: 0x04008F80 RID: 36736
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008F81 RID: 36737
		public NKM_PUBLISHER_TYPE publisherType;

		// Token: 0x04008F82 RID: 36738
		public long creditCount;

		// Token: 0x04008F83 RID: 36739
		public long eterniumCount;

		// Token: 0x04008F84 RID: 36740
		public long cashCount;

		// Token: 0x04008F85 RID: 36741
		public long medalCount;
	}
}

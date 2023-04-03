using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F7C RID: 3964
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_ACK)]
	public sealed class NKMPacket_EVENT_PASS_ACK : ISerializable
	{
		// Token: 0x060099D4 RID: 39380 RVA: 0x00330917 File Offset: 0x0032EB17
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.totalExp);
			stream.PutOrGet(ref this.rewardNormalLevel);
			stream.PutOrGet(ref this.rewardCoreLevel);
			stream.PutOrGet(ref this.isCorePassPurchased);
		}

		// Token: 0x04008CEF RID: 36079
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CF0 RID: 36080
		public int totalExp;

		// Token: 0x04008CF1 RID: 36081
		public int rewardNormalLevel;

		// Token: 0x04008CF2 RID: 36082
		public int rewardCoreLevel;

		// Token: 0x04008CF3 RID: 36083
		public bool isCorePassPurchased;
	}
}

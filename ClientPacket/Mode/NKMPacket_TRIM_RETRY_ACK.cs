using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E59 RID: 3673
	[PacketId(ClientPacketId.kNKMPacket_TRIM_RETRY_ACK)]
	public sealed class NKMPacket_TRIM_RETRY_ACK : ISerializable
	{
		// Token: 0x060097A2 RID: 38818 RVA: 0x0032D3C4 File Offset: 0x0032B5C4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x040089CF RID: 35279
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089D0 RID: 35280
		public NKMRewardData rewardData;
	}
}

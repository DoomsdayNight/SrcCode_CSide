using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E40 RID: 3648
	[PacketId(ClientPacketId.kNKMPacket_DIVE_SUICIDE_ACK)]
	public sealed class NKMPacket_DIVE_SUICIDE_ACK : ISerializable
	{
		// Token: 0x06009770 RID: 38768 RVA: 0x0032CF00 File Offset: 0x0032B100
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDiveSyncData>(ref this.diveSyncData);
		}

		// Token: 0x0400898E RID: 35214
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400898F RID: 35215
		public NKMDiveSyncData diveSyncData;
	}
}

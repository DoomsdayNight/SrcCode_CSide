using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E37 RID: 3639
	[PacketId(ClientPacketId.kNKMPacket_DIVE_MOVE_FORWARD_ACK)]
	public sealed class NKMPacket_DIVE_MOVE_FORWARD_ACK : ISerializable
	{
		// Token: 0x0600975E RID: 38750 RVA: 0x0032CE22 File Offset: 0x0032B022
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDiveSyncData>(ref this.diveSyncData);
		}

		// Token: 0x04008983 RID: 35203
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008984 RID: 35204
		public NKMDiveSyncData diveSyncData;
	}
}

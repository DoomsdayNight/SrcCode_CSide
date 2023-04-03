using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E3E RID: 3646
	[PacketId(ClientPacketId.kNKMPacket_DIVE_SELECT_ARTIFACT_ACK)]
	public sealed class NKMPacket_DIVE_SELECT_ARTIFACT_ACK : ISerializable
	{
		// Token: 0x0600976C RID: 38764 RVA: 0x0032CEC8 File Offset: 0x0032B0C8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDiveSyncData>(ref this.diveSyncData);
		}

		// Token: 0x0400898B RID: 35211
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400898C RID: 35212
		public NKMDiveSyncData diveSyncData;
	}
}

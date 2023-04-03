using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA9 RID: 3497
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_GLOBAL_BAN_ACK)]
	public sealed class NKMPacket_DRAFT_PVP_GLOBAL_BAN_ACK : ISerializable
	{
		// Token: 0x0600964B RID: 38475 RVA: 0x0032B721 File Offset: 0x00329921
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008852 RID: 34898
		public NKM_ERROR_CODE errorCode;
	}
}

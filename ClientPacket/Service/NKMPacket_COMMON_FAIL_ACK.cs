using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D48 RID: 3400
	[PacketId(ClientPacketId.kNKMPacket_COMMON_FAIL_ACK)]
	public sealed class NKMPacket_COMMON_FAIL_ACK : ISerializable
	{
		// Token: 0x0600958D RID: 38285 RVA: 0x0032A4DE File Offset: 0x003286DE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400873F RID: 34623
		public NKM_ERROR_CODE errorCode;
	}
}

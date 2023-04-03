using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D4C RID: 3404
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_LEAVE_STATE_ACK)]
	public sealed class NKMPacket_ACCOUNT_LEAVE_STATE_ACK : ISerializable
	{
		// Token: 0x06009595 RID: 38293 RVA: 0x0032A51E File Offset: 0x0032871E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.leave);
		}

		// Token: 0x04008741 RID: 34625
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008742 RID: 34626
		public bool leave;
	}
}

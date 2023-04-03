using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D92 RID: 3474
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_CANCEL_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_CANCEL_ACK : ISerializable
	{
		// Token: 0x0600961F RID: 38431 RVA: 0x0032B426 File Offset: 0x00329626
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008824 RID: 34852
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008825 RID: 34853
		public long targetUserUid;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD9 RID: 3289
	[PacketId(ClientPacketId.kNKMPacket_BACKGROUND_CHANGE_ACK)]
	public sealed class NKMPacket_BACKGROUND_CHANGE_ACK : ISerializable
	{
		// Token: 0x060094AF RID: 38063 RVA: 0x0032913A File Offset: 0x0032733A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMBackgroundInfo>(ref this.backgroundInfo);
		}

		// Token: 0x0400862E RID: 34350
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400862F RID: 34351
		public NKMBackgroundInfo backgroundInfo = new NKMBackgroundInfo();
	}
}

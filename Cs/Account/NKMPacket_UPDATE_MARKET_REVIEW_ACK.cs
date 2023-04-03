using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200107E RID: 4222
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_MARKET_REVIEW_ACK)]
	public sealed class NKMPacket_UPDATE_MARKET_REVIEW_ACK : ISerializable
	{
		// Token: 0x06009BB9 RID: 39865 RVA: 0x00333C45 File Offset: 0x00331E45
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008FF9 RID: 36857
		public NKM_ERROR_CODE errorCode;
	}
}

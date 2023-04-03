using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D96 RID: 3478
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_ACCEPT_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_ACCEPT_ACK : ISerializable
	{
		// Token: 0x06009625 RID: 38437 RVA: 0x0032B48C File Offset: 0x0032968C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<PrivatePvpCancelType>(ref this.cancelType);
			stream.PutOrGet(ref this.serverIp);
			stream.PutOrGet(ref this.port);
			stream.PutOrGet(ref this.accessToken);
		}

		// Token: 0x04008833 RID: 34867
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008834 RID: 34868
		public PrivatePvpCancelType cancelType;

		// Token: 0x04008835 RID: 34869
		public string serverIp;

		// Token: 0x04008836 RID: 34870
		public int port;

		// Token: 0x04008837 RID: 34871
		public string accessToken;
	}
}

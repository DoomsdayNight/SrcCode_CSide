using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD9 RID: 3545
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_ACCEPT_INVITE_ACK)]
	public sealed class NKMPacket_PVP_ROOM_ACCEPT_INVITE_ACK : ISerializable
	{
		// Token: 0x060096A9 RID: 38569 RVA: 0x0032BC8F File Offset: 0x00329E8F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<PrivatePvpCancelType>(ref this.cancelType);
			stream.PutOrGet(ref this.serverIp);
			stream.PutOrGet(ref this.port);
			stream.PutOrGet(ref this.accessToken);
		}

		// Token: 0x04008893 RID: 34963
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008894 RID: 34964
		public PrivatePvpCancelType cancelType;

		// Token: 0x04008895 RID: 34965
		public string serverIp;

		// Token: 0x04008896 RID: 34966
		public int port;

		// Token: 0x04008897 RID: 34967
		public string accessToken;
	}
}

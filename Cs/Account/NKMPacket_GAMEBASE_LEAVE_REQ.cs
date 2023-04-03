using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x020010A1 RID: 4257
	[PacketId(ClientPacketId.kNKMPacket_GAMEBASE_LEAVE_REQ)]
	public sealed class NKMPacket_GAMEBASE_LEAVE_REQ : ISerializable
	{
		// Token: 0x06009BFF RID: 39935 RVA: 0x003340DB File Offset: 0x003322DB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userId);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.idpCode);
		}

		// Token: 0x04009034 RID: 36916
		public string userId;

		// Token: 0x04009035 RID: 36917
		public string accessToken;

		// Token: 0x04009036 RID: 36918
		public string idpCode;
	}
}

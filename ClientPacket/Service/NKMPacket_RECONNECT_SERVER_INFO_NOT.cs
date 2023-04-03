using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D47 RID: 3399
	[PacketId(ClientPacketId.kNKMPacket_RECONNECT_SERVER_INFO_NOT)]
	public sealed class NKMPacket_RECONNECT_SERVER_INFO_NOT : ISerializable
	{
		// Token: 0x0600958B RID: 38283 RVA: 0x0032A4B0 File Offset: 0x003286B0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.serverIp);
			stream.PutOrGet(ref this.port);
			stream.PutOrGet(ref this.accessToken);
		}

		// Token: 0x0400873C RID: 34620
		public string serverIp;

		// Token: 0x0400873D RID: 34621
		public int port;

		// Token: 0x0400873E RID: 34622
		public string accessToken;
	}
}

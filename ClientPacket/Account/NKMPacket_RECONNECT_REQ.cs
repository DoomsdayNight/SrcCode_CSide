using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001077 RID: 4215
	[PacketId(ClientPacketId.kNKMPacket_RECONNECT_REQ)]
	public sealed class NKMPacket_RECONNECT_REQ : ISerializable
	{
		// Token: 0x06009BAB RID: 39851 RVA: 0x00333A9D File Offset: 0x00331C9D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.reconnectKey);
		}

		// Token: 0x04008FE2 RID: 36834
		public string reconnectKey;
	}
}

using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001079 RID: 4217
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_RECONNECT_KEY_NOT)]
	public sealed class NKMPacket_UPDATE_RECONNECT_KEY_NOT : ISerializable
	{
		// Token: 0x06009BAF RID: 39855 RVA: 0x00333B33 File Offset: 0x00331D33
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.reconnectKey);
		}

		// Token: 0x04008FEA RID: 36842
		public string reconnectKey;
	}
}

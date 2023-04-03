using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001075 RID: 4213
	[PacketId(ClientPacketId.kNKMPacket_CHANGE_NICKNAME_REQ)]
	public sealed class NKMPacket_CHANGE_NICKNAME_REQ : ISerializable
	{
		// Token: 0x06009BA7 RID: 39847 RVA: 0x00333A59 File Offset: 0x00331C59
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.nickname);
		}

		// Token: 0x04008FDE RID: 36830
		public string nickname;
	}
}

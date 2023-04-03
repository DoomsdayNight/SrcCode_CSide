using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001072 RID: 4210
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_NICKNAME_NOT)]
	public sealed class NKMPacket_UPDATE_NICKNAME_NOT : ISerializable
	{
		// Token: 0x06009BA1 RID: 39841 RVA: 0x00333A23 File Offset: 0x00331C23
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.nickname);
		}

		// Token: 0x04008FDC RID: 36828
		public string nickname;
	}
}

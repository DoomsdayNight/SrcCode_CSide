using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001076 RID: 4214
	[PacketId(ClientPacketId.kNKMPacket_CHANGE_NICKNAME_ACK)]
	public sealed class NKMPacket_CHANGE_NICKNAME_ACK : ISerializable
	{
		// Token: 0x06009BA9 RID: 39849 RVA: 0x00333A6F File Offset: 0x00331C6F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.nickname);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008FDF RID: 36831
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008FE0 RID: 36832
		public string nickname;

		// Token: 0x04008FE1 RID: 36833
		public NKMItemMiscData costItemData;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB1 RID: 3249
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNLOCK_ACK)]
	public sealed class NKMPacket_DECK_UNLOCK_ACK : ISerializable
	{
		// Token: 0x0600945F RID: 37983 RVA: 0x003289B6 File Offset: 0x00326BB6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_DECK_TYPE>(ref this.deckType);
			stream.PutOrGet(ref this.unlockedDeckSize);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x040085BE RID: 34238
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085BF RID: 34239
		public NKM_DECK_TYPE deckType;

		// Token: 0x040085C0 RID: 34240
		public byte unlockedDeckSize;

		// Token: 0x040085C1 RID: 34241
		public NKMItemMiscData costItemData;
	}
}

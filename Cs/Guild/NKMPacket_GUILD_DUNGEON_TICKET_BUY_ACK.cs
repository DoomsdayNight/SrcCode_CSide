using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F23 RID: 3875
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_TICKET_BUY_ACK)]
	public sealed class NKMPacket_GUILD_DUNGEON_TICKET_BUY_ACK : ISerializable
	{
		// Token: 0x06009926 RID: 39206 RVA: 0x0032F8F9 File Offset: 0x0032DAF9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.currentTicketBuyCount);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008C01 RID: 35841
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C02 RID: 35842
		public int currentTicketBuyCount;

		// Token: 0x04008C03 RID: 35843
		public NKMItemMiscData costItemData;
	}
}

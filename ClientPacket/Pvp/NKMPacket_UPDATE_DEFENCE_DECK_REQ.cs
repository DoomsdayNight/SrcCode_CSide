using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D86 RID: 3462
	[PacketId(ClientPacketId.kNKMPacket_UPDATE_DEFENCE_DECK_REQ)]
	public sealed class NKMPacket_UPDATE_DEFENCE_DECK_REQ : ISerializable
	{
		// Token: 0x06009607 RID: 38407 RVA: 0x0032B1EC File Offset: 0x003293EC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckData>(ref this.deckData);
		}

		// Token: 0x04008804 RID: 34820
		public NKMDeckData deckData;
	}
}

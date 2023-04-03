using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D9D RID: 3485
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_ENEMY_DECK_UPDATE_NOT)]
	public sealed class NKMPacket_PRIVATE_PVP_ENEMY_DECK_UPDATE_NOT : ISerializable
	{
		// Token: 0x06009633 RID: 38451 RVA: 0x0032B5E1 File Offset: 0x003297E1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDummyDeckData>(ref this.deckData);
		}

		// Token: 0x04008845 RID: 34885
		public NKMDummyDeckData deckData;
	}
}

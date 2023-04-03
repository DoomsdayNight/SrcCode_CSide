using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D9C RID: 3484
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_ENEMY_READY_NOT)]
	public sealed class NKMPacket_PRIVATE_PVP_ENEMY_READY_NOT : ISerializable
	{
		// Token: 0x06009631 RID: 38449 RVA: 0x0032B5CB File Offset: 0x003297CB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDummyDeckData>(ref this.deckData);
		}

		// Token: 0x04008844 RID: 34884
		public NKMDummyDeckData deckData;
	}
}

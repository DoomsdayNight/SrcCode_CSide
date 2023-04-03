using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CDE RID: 3294
	[PacketId(ClientPacketId.kNKMPacket_DECK_NAME_UPDATE_REQ)]
	public sealed class NKMPacket_DECK_NAME_UPDATE_REQ : ISerializable
	{
		// Token: 0x060094B9 RID: 38073 RVA: 0x003291ED File Offset: 0x003273ED
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.name);
		}

		// Token: 0x04008636 RID: 34358
		public NKMDeckIndex deckIndex;

		// Token: 0x04008637 RID: 34359
		public string name;
	}
}

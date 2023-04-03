using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CDF RID: 3295
	[PacketId(ClientPacketId.kNKMPacket_DECK_NAME_UPDATE_ACK)]
	public sealed class NKMPacket_DECK_NAME_UPDATE_ACK : ISerializable
	{
		// Token: 0x060094BB RID: 38075 RVA: 0x0032920F File Offset: 0x0032740F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.name);
		}

		// Token: 0x04008638 RID: 34360
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008639 RID: 34361
		public NKMDeckIndex deckIndex;

		// Token: 0x0400863A RID: 34362
		public string name;
	}
}

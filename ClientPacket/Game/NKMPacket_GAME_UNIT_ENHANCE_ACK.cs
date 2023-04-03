using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F56 RID: 3926
	[PacketId(ClientPacketId.kNKMPacket_GAME_UNIT_ENHANCE_ACK)]
	public sealed class NKMPacket_GAME_UNIT_ENHANCE_ACK : ISerializable
	{
		// Token: 0x0600998C RID: 39308 RVA: 0x00330274 File Offset: 0x0032E474
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.enhanceCount);
		}

		// Token: 0x04008C90 RID: 35984
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C91 RID: 35985
		public long unitUID;

		// Token: 0x04008C92 RID: 35986
		public sbyte enhanceCount;
	}
}

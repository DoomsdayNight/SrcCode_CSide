using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D0F RID: 3343
	[PacketId(ClientPacketId.kNKMPacket_FAVORITE_UNIT_ACK)]
	public sealed class NKMPacket_FAVORITE_UNIT_ACK : ISerializable
	{
		// Token: 0x0600951B RID: 38171 RVA: 0x00329AB8 File Offset: 0x00327CB8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet(ref this.isFavorite);
		}

		// Token: 0x040086B0 RID: 34480
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086B1 RID: 34481
		public long unitUid;

		// Token: 0x040086B2 RID: 34482
		public bool isFavorite;
	}
}

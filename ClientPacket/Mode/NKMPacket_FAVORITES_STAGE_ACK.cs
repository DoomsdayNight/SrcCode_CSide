using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E60 RID: 3680
	[PacketId(ClientPacketId.kNKMPacket_FAVORITES_STAGE_ACK)]
	public sealed class NKMPacket_FAVORITES_STAGE_ACK : ISerializable
	{
		// Token: 0x060097B0 RID: 38832 RVA: 0x0032D500 File Offset: 0x0032B700
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.favoritesStage);
		}

		// Token: 0x040089DE RID: 35294
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089DF RID: 35295
		public Dictionary<int, int> favoritesStage = new Dictionary<int, int>();
	}
}

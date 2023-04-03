using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E62 RID: 3682
	[PacketId(ClientPacketId.kNKMPacket_FAVORITES_STAGE_ADD_ACK)]
	public sealed class NKMPacket_FAVORITES_STAGE_ADD_ACK : ISerializable
	{
		// Token: 0x060097B4 RID: 38836 RVA: 0x0032D543 File Offset: 0x0032B743
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.favoritesStage);
		}

		// Token: 0x040089E1 RID: 35297
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089E2 RID: 35298
		public Dictionary<int, int> favoritesStage = new Dictionary<int, int>();
	}
}

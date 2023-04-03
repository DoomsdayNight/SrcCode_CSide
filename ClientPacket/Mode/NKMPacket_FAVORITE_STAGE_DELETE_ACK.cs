using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E64 RID: 3684
	[PacketId(ClientPacketId.kNKMPacket_FAVORITE_STAGE_DELETE_ACK)]
	public sealed class NKMPacket_FAVORITE_STAGE_DELETE_ACK : ISerializable
	{
		// Token: 0x060097B8 RID: 38840 RVA: 0x0032D586 File Offset: 0x0032B786
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.favoritesStage);
		}

		// Token: 0x040089E4 RID: 35300
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089E5 RID: 35301
		public Dictionary<int, int> favoritesStage = new Dictionary<int, int>();
	}
}

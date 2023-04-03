using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D81 RID: 3457
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_TARGET_LIST_ACK)]
	public sealed class NKMPacket_ASYNC_PVP_TARGET_LIST_ACK : ISerializable
	{
		// Token: 0x060095FD RID: 38397 RVA: 0x0032B0DA File Offset: 0x003292DA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<AsyncPvpTarget>(ref this.targetList);
		}

		// Token: 0x040087F5 RID: 34805
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087F6 RID: 34806
		public List<AsyncPvpTarget> targetList = new List<AsyncPvpTarget>();
	}
}

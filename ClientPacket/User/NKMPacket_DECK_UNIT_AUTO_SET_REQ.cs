using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB4 RID: 3252
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNIT_AUTO_SET_REQ)]
	public sealed class NKMPacket_DECK_UNIT_AUTO_SET_REQ : ISerializable
	{
		// Token: 0x06009465 RID: 37989 RVA: 0x00328A95 File Offset: 0x00326C95
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.unitUIDList);
			stream.PutOrGet(ref this.shipUID);
			stream.PutOrGet(ref this.operatorUid);
		}

		// Token: 0x040085CD RID: 34253
		public NKMDeckIndex deckIndex;

		// Token: 0x040085CE RID: 34254
		public List<long> unitUIDList = new List<long>();

		// Token: 0x040085CF RID: 34255
		public long shipUID;

		// Token: 0x040085D0 RID: 34256
		public long operatorUid;
	}
}

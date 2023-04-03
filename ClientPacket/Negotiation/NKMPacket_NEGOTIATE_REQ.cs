using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Negotiation
{
	// Token: 0x02000E2C RID: 3628
	[PacketId(ClientPacketId.kNKMPacket_NEGOTIATE_REQ)]
	public sealed class NKMPacket_NEGOTIATE_REQ : ISerializable
	{
		// Token: 0x06009748 RID: 38728 RVA: 0x0032CBF3 File Offset: 0x0032ADF3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet<MiscItemData>(ref this.materials);
			stream.PutOrGetEnum<NEGOTIATE_BOSS_SELECTION>(ref this.negotiateBossSelection);
		}

		// Token: 0x04008964 RID: 35172
		public long unitUid;

		// Token: 0x04008965 RID: 35173
		public List<MiscItemData> materials = new List<MiscItemData>();

		// Token: 0x04008966 RID: 35174
		public NEGOTIATE_BOSS_SELECTION negotiateBossSelection;
	}
}

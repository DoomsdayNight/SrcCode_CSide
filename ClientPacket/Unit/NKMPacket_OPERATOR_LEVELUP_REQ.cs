using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CFA RID: 3322
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_LEVELUP_REQ)]
	public sealed class NKMPacket_OPERATOR_LEVELUP_REQ : ISerializable
	{
		// Token: 0x060094F1 RID: 38129 RVA: 0x003296CE File Offset: 0x003278CE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUnitUid);
			stream.PutOrGet<MiscItemData>(ref this.materials);
		}

		// Token: 0x04008679 RID: 34425
		public long targetUnitUid;

		// Token: 0x0400867A RID: 34426
		public List<MiscItemData> materials = new List<MiscItemData>();
	}
}

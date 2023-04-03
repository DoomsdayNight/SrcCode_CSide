using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CEB RID: 3307
	[PacketId(ClientPacketId.kNKMPacket_UNIT_SKILL_UPGRADE_ACK)]
	public sealed class NKMPacket_UNIT_SKILL_UPGRADE_ACK : ISerializable
	{
		// Token: 0x060094D3 RID: 38099 RVA: 0x00329424 File Offset: 0x00327624
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.skillID);
			stream.PutOrGet(ref this.skillLevel);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008654 RID: 34388
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008655 RID: 34389
		public long unitUID;

		// Token: 0x04008656 RID: 34390
		public int skillID;

		// Token: 0x04008657 RID: 34391
		public int skillLevel;

		// Token: 0x04008658 RID: 34392
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}

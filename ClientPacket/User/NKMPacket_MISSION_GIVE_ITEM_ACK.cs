using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CDD RID: 3293
	[PacketId(ClientPacketId.kNKMPacket_MISSION_GIVE_ITEM_ACK)]
	public sealed class NKMPacket_MISSION_GIVE_ITEM_ACK : ISerializable
	{
		// Token: 0x060094B7 RID: 38071 RVA: 0x003291C0 File Offset: 0x003273C0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
		}

		// Token: 0x04008634 RID: 34356
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008635 RID: 34357
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();
	}
}

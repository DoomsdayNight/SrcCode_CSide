using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB2 RID: 3762
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_APPLY_ACK)]
	public sealed class NKMPacket_EQUIP_PRESET_APPLY_ACK : ISerializable
	{
		// Token: 0x06009850 RID: 38992 RVA: 0x0032E3F9 File Offset: 0x0032C5F9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet<NKMPacket_EQUIP_PRESET_APPLY_ACK.UnitEquipUidSet>(ref this.updateUnitDatas);
		}

		// Token: 0x04008AB1 RID: 35505
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008AB2 RID: 35506
		public int presetIndex;

		// Token: 0x04008AB3 RID: 35507
		public List<NKMPacket_EQUIP_PRESET_APPLY_ACK.UnitEquipUidSet> updateUnitDatas = new List<NKMPacket_EQUIP_PRESET_APPLY_ACK.UnitEquipUidSet>();

		// Token: 0x02001A2B RID: 6699
		public sealed class UnitEquipUidSet : ISerializable
		{
			// Token: 0x0600BB41 RID: 47937 RVA: 0x0036E9B5 File Offset: 0x0036CBB5
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGet(ref this.unitUid);
				stream.PutOrGet(ref this.equipUids);
			}

			// Token: 0x0400ADDE RID: 44510
			public long unitUid;

			// Token: 0x0400ADDF RID: 44511
			public List<long> equipUids = new List<long>();
		}
	}
}

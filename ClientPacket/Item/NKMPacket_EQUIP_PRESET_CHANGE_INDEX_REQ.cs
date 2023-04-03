using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EBE RID: 3774
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ)]
	public sealed class NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ : ISerializable
	{
		// Token: 0x06009868 RID: 39016 RVA: 0x0032E5F5 File Offset: 0x0032C7F5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData>(ref this.changeIndices);
		}

		// Token: 0x04008ACA RID: 35530
		public List<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData> changeIndices = new List<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData>();

		// Token: 0x02001A2C RID: 6700
		public sealed class PresetIndexData : ISerializable
		{
			// Token: 0x0600BB43 RID: 47939 RVA: 0x0036E9E2 File Offset: 0x0036CBE2
			void ISerializable.Serialize(IPacketStream stream)
			{
				stream.PutOrGet(ref this.beforeIndex);
				stream.PutOrGet(ref this.afterIndex);
			}

			// Token: 0x0400ADE0 RID: 44512
			public int beforeIndex;

			// Token: 0x0400ADE1 RID: 44513
			public int afterIndex;
		}
	}
}

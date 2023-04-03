using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E84 RID: 3716
	public sealed class NKMEquipPresetData : ISerializable
	{
		// Token: 0x060097F4 RID: 38900 RVA: 0x0032DB8F File Offset: 0x0032BD8F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGetEnum<NKM_EQUIP_PRESET_TYPE>(ref this.presetType);
			stream.PutOrGet(ref this.presetName);
			stream.PutOrGet(ref this.equipUids);
		}

		// Token: 0x04008A3A RID: 35386
		public int presetIndex;

		// Token: 0x04008A3B RID: 35387
		public NKM_EQUIP_PRESET_TYPE presetType;

		// Token: 0x04008A3C RID: 35388
		public string presetName;

		// Token: 0x04008A3D RID: 35389
		public List<long> equipUids = new List<long>();
	}
}

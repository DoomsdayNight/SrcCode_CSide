using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004F4 RID: 1268
	public class EQUIP_ITEM_STAT : ISerializable
	{
		// Token: 0x060023CC RID: 9164 RVA: 0x000BAACE File Offset: 0x000B8CCE
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_STAT_TYPE>(ref this.type);
			stream.PutOrGet(ref this.stat_value);
			stream.PutOrGet(ref this.stat_level_value);
			stream.PutOrGet(ref this.stat_factor);
		}

		// Token: 0x040025AD RID: 9645
		public NKM_STAT_TYPE type = NKM_STAT_TYPE.NST_RANDOM;

		// Token: 0x040025AE RID: 9646
		public float stat_value;

		// Token: 0x040025AF RID: 9647
		public float stat_level_value;

		// Token: 0x040025B0 RID: 9648
		public float stat_factor;
	}
}

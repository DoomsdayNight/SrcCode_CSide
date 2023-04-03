using System;
using Cs.Protocol;
using NKM.Templet;

namespace ClientPacket.User
{
	// Token: 0x02000CAA RID: 3242
	public sealed class NKMBackgroundUnitInfo : ISerializable
	{
		// Token: 0x06009451 RID: 37969 RVA: 0x00328804 File Offset: 0x00326A04
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGetEnum<NKM_UNIT_TYPE>(ref this.unitType);
			stream.PutOrGet(ref this.unitSize);
			stream.PutOrGet(ref this.unitFace);
			stream.PutOrGet(ref this.unitPosX);
			stream.PutOrGet(ref this.unitPosY);
			stream.PutOrGet(ref this.backImage);
			stream.PutOrGet(ref this.skinOption);
		}

		// Token: 0x040085A3 RID: 34211
		public long unitUid;

		// Token: 0x040085A4 RID: 34212
		public NKM_UNIT_TYPE unitType;

		// Token: 0x040085A5 RID: 34213
		public float unitSize;

		// Token: 0x040085A6 RID: 34214
		public int unitFace;

		// Token: 0x040085A7 RID: 34215
		public float unitPosX;

		// Token: 0x040085A8 RID: 34216
		public float unitPosY;

		// Token: 0x040085A9 RID: 34217
		public bool backImage;

		// Token: 0x040085AA RID: 34218
		public int skinOption;
	}
}

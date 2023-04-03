using System;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DE4 RID: 3556
	public sealed class NKMOfficeFurniture : ISerializable
	{
		// Token: 0x060096BC RID: 38588 RVA: 0x0032BDBC File Offset: 0x00329FBC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.uid);
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGetEnum<OfficePlaneType>(ref this.planeType);
			stream.PutOrGet(ref this.positionX);
			stream.PutOrGet(ref this.positionY);
			stream.PutOrGet(ref this.inverted);
		}

		// Token: 0x040088A6 RID: 34982
		public long uid;

		// Token: 0x040088A7 RID: 34983
		public int itemId;

		// Token: 0x040088A8 RID: 34984
		public OfficePlaneType planeType;

		// Token: 0x040088A9 RID: 34985
		public int positionX;

		// Token: 0x040088AA RID: 34986
		public int positionY;

		// Token: 0x040088AB RID: 34987
		public bool inverted;
	}
}

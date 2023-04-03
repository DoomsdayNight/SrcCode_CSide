using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DFA RID: 3578
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_ADD_FURNITURE_REQ)]
	public sealed class NKMPacket_OFFICE_ADD_FURNITURE_REQ : ISerializable
	{
		// Token: 0x060096E8 RID: 38632 RVA: 0x0032C33C File Offset: 0x0032A53C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGetEnum<OfficePlaneType>(ref this.planeType);
			stream.PutOrGet(ref this.positionX);
			stream.PutOrGet(ref this.positionY);
			stream.PutOrGet(ref this.inverted);
		}

		// Token: 0x040088EE RID: 35054
		public int roomId;

		// Token: 0x040088EF RID: 35055
		public int itemId;

		// Token: 0x040088F0 RID: 35056
		public OfficePlaneType planeType;

		// Token: 0x040088F1 RID: 35057
		public int positionX;

		// Token: 0x040088F2 RID: 35058
		public int positionY;

		// Token: 0x040088F3 RID: 35059
		public bool inverted;
	}
}

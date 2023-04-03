using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DFC RID: 3580
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_UPDATE_FURNITURE_REQ)]
	public sealed class NKMPacket_OFFICE_UPDATE_FURNITURE_REQ : ISerializable
	{
		// Token: 0x060096EC RID: 38636 RVA: 0x0032C40C File Offset: 0x0032A60C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.furnitureUid);
			stream.PutOrGetEnum<OfficePlaneType>(ref this.planeType);
			stream.PutOrGet(ref this.positionX);
			stream.PutOrGet(ref this.positionY);
			stream.PutOrGet(ref this.inverted);
		}

		// Token: 0x040088F9 RID: 35065
		public int roomId;

		// Token: 0x040088FA RID: 35066
		public long furnitureUid;

		// Token: 0x040088FB RID: 35067
		public OfficePlaneType planeType;

		// Token: 0x040088FC RID: 35068
		public int positionX;

		// Token: 0x040088FD RID: 35069
		public int positionY;

		// Token: 0x040088FE RID: 35070
		public bool inverted;
	}
}

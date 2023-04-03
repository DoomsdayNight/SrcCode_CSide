using System;
using Cs.Protocol;
using NKM.Templet.Office;

namespace ClientPacket.Game
{
	// Token: 0x02000F37 RID: 3895
	public sealed class UnitLoyaltyUpdateData : ISerializable
	{
		// Token: 0x0600994E RID: 39246 RVA: 0x0032FCC7 File Offset: 0x0032DEC7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet(ref this.loyalty);
			stream.PutOrGet(ref this.officeRoomId);
			stream.PutOrGetEnum<OfficeGrade>(ref this.officeGrade);
			stream.PutOrGet(ref this.heartGaugeStartTime);
		}

		// Token: 0x04008C3B RID: 35899
		public long unitUid;

		// Token: 0x04008C3C RID: 35900
		public int loyalty;

		// Token: 0x04008C3D RID: 35901
		public int officeRoomId;

		// Token: 0x04008C3E RID: 35902
		public OfficeGrade officeGrade;

		// Token: 0x04008C3F RID: 35903
		public DateTime heartGaugeStartTime;
	}
}

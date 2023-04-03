using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003DD RID: 989
	public sealed class NKMDummyUnitData : ISerializable
	{
		// Token: 0x06001A22 RID: 6690 RVA: 0x00070129 File Offset: 0x0006E329
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.UnitId);
			stream.PutOrGet(ref this.UnitLevel);
			stream.PutOrGet(ref this.SkinId);
			stream.PutOrGet(ref this.LimitBreakLevel);
			stream.PutOrGet(ref this.TacticLevel);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00070167 File Offset: 0x0006E367
		public NKMUnitData ToUnitData(long unitUid)
		{
			return NKMDungeonManager.MakeUnitDataFromID(this.UnitId, unitUid, this.UnitLevel, (int)this.LimitBreakLevel, this.SkinId, this.TacticLevel);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0007018D File Offset: 0x0006E38D
		public GameUnitData ToGameUnitData(long unitUid)
		{
			return new GameUnitData
			{
				unit = this.ToUnitData(unitUid)
			};
		}

		// Token: 0x04001315 RID: 4885
		public int UnitId;

		// Token: 0x04001316 RID: 4886
		public int UnitLevel;

		// Token: 0x04001317 RID: 4887
		public int SkinId;

		// Token: 0x04001318 RID: 4888
		public short LimitBreakLevel;

		// Token: 0x04001319 RID: 4889
		public int TacticLevel;
	}
}

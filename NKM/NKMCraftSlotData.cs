using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003C1 RID: 961
	public class NKMCraftSlotData : ISerializable
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x00067F33 File Offset: 0x00066133
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.Index);
			stream.PutOrGet(ref this.MoldID);
			stream.PutOrGet(ref this.Count);
			stream.PutOrGet(ref this.CompleteDate);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00067F68 File Offset: 0x00066168
		public NKM_CRAFT_SLOT_STATE GetState(DateTime curTimeUTC)
		{
			NKM_CRAFT_SLOT_STATE result = NKM_CRAFT_SLOT_STATE.NECSS_EMPTY;
			if (this.MoldID > 0)
			{
				if (curTimeUTC.Ticks >= this.CompleteDate)
				{
					result = NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED;
				}
				else
				{
					result = NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW;
				}
			}
			return result;
		}

		// Token: 0x040011AE RID: 4526
		public byte Index;

		// Token: 0x040011AF RID: 4527
		public int MoldID;

		// Token: 0x040011B0 RID: 4528
		public int Count;

		// Token: 0x040011B1 RID: 4529
		public long CompleteDate;
	}
}

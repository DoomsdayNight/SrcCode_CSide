using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000377 RID: 887
	public sealed class NKMDiveSlotWithIndexes : ISerializable
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00059596 File Offset: 0x00057796
		public NKMDiveSlot Slot
		{
			get
			{
				return this.slot;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0005959E File Offset: 0x0005779E
		public int SlotSetIndex
		{
			get
			{
				return this.slotSetIndex;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x000595A6 File Offset: 0x000577A6
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000595AE File Offset: 0x000577AE
		public NKMDiveSlotWithIndexes()
		{
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000595C1 File Offset: 0x000577C1
		public NKMDiveSlotWithIndexes(NKMDiveSlot slot, int slotSetIndex, int slotIndex)
		{
			this.slot = slot.DeepCopy<NKMDiveSlot>();
			this.slotSetIndex = slotSetIndex;
			this.slotIndex = slotIndex;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000595EE File Offset: 0x000577EE
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDiveSlot>(ref this.slot);
			stream.PutOrGet(ref this.slotSetIndex);
			stream.PutOrGet(ref this.slotIndex);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00059614 File Offset: 0x00057814
		public void UpdateSlot(NKMDiveSlot slot)
		{
			this.slot = slot.DeepCopy<NKMDiveSlot>();
		}

		// Token: 0x04000EEC RID: 3820
		private NKMDiveSlot slot = new NKMDiveSlot();

		// Token: 0x04000EED RID: 3821
		private int slotSetIndex;

		// Token: 0x04000EEE RID: 3822
		private int slotIndex;
	}
}

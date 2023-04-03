using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000372 RID: 882
	public class NKMDiveFloor : ISerializable
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00058EF4 File Offset: 0x000570F4
		public NKMDiveTemplet Templet
		{
			get
			{
				return this.templet;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x00058EFC File Offset: 0x000570FC
		public List<NKMDiveSlotSet> SlotSets
		{
			get
			{
				return this.slotSets;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00058F04 File Offset: 0x00057104
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00058F0C File Offset: 0x0005710C
		public long ExpireDate
		{
			get
			{
				return this.expireDate;
			}
			set
			{
				this.expireDate = value;
			}
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00058F28 File Offset: 0x00057128
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageID);
			stream.PutOrGet<NKMDiveSlotSet>(ref this.slotSets);
			stream.PutOrGet(ref this.expireDate);
			if (stream is PacketReader)
			{
				this.OnPacketRead();
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00058F5C File Offset: 0x0005715C
		public void OnPacketRead()
		{
			this.templet = NKMDiveTemplet.Find(this.stageID);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00058F70 File Offset: 0x00057170
		public NKMDiveSlot GetSlot(int curSlotSetIndex, int cutSlotIndex)
		{
			if (curSlotSetIndex >= this.SlotSets.Count)
			{
				return null;
			}
			NKMDiveSlotSet nkmdiveSlotSet = this.SlotSets[curSlotSetIndex];
			if (cutSlotIndex >= nkmdiveSlotSet.Slots.Count)
			{
				return null;
			}
			return nkmdiveSlotSet.Slots[cutSlotIndex];
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00058FB6 File Offset: 0x000571B6
		public bool isExpire(DateTime now)
		{
			return now.Ticks >= this.expireDate;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00058FCA File Offset: 0x000571CA
		public int GetDiveStormCostCount()
		{
			return this.templet.RandomSetCount * NKMDiveTemplet.DiveStormCostMultiply;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00058FDD File Offset: 0x000571DD
		public int GetDiveStormRewardCount()
		{
			return this.templet.RandomSetCount * NKMDiveTemplet.DiveStormRewardMultiply;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00058FF0 File Offset: 0x000571F0
		public void Rebuild(int distance, int nextSlotSetIndex, int nextSlotIndex)
		{
			NKMDiveSlotSet nkmdiveSlotSet = this.SlotSets[nextSlotSetIndex];
			NKMDiveSlot item = nkmdiveSlotSet.Slots[nextSlotIndex];
			if (distance > 0)
			{
				this.SlotSets.RemoveAt(0);
			}
			nkmdiveSlotSet.Slots.Clear();
			nkmdiveSlotSet.Slots.Add(item);
		}

		// Token: 0x04000ED3 RID: 3795
		protected int stageID;

		// Token: 0x04000ED4 RID: 3796
		protected NKMDiveTemplet templet;

		// Token: 0x04000ED5 RID: 3797
		protected List<NKMDiveSlotSet> slotSets = new List<NKMDiveSlotSet>();

		// Token: 0x04000ED6 RID: 3798
		protected long expireDate;

		// Token: 0x04000ED7 RID: 3799
		public const int MAX_DISCOVERED_SLOT_SET_COUNT = 2;
	}
}

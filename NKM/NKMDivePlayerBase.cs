using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000374 RID: 884
	public sealed class NKMDivePlayerBase : ISerializable
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00059288 File Offset: 0x00057488
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00059290 File Offset: 0x00057490
		public NKMDivePlayerState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00059299 File Offset: 0x00057499
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x000592A1 File Offset: 0x000574A1
		public int PrevSlotSetIndex
		{
			get
			{
				return this.prevSlotSetIndex;
			}
			set
			{
				this.prevSlotSetIndex = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000592AA File Offset: 0x000574AA
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x000592B2 File Offset: 0x000574B2
		public int PrevSlotIndex
		{
			get
			{
				return this.prevSlotIndex;
			}
			set
			{
				this.prevSlotIndex = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x000592BB File Offset: 0x000574BB
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x000592C3 File Offset: 0x000574C3
		public int SlotSetIndex
		{
			get
			{
				return this.slotSetIndex;
			}
			set
			{
				this.slotSetIndex = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000592CC File Offset: 0x000574CC
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x000592D4 File Offset: 0x000574D4
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
			set
			{
				this.slotIndex = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x000592DD File Offset: 0x000574DD
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x000592E5 File Offset: 0x000574E5
		public int Distance
		{
			get
			{
				return this.distance;
			}
			set
			{
				this.distance = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x000592EE File Offset: 0x000574EE
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x000592F6 File Offset: 0x000574F6
		public int LeaderDeckIndex
		{
			get
			{
				return this.leaderDeckIndex;
			}
			set
			{
				this.leaderDeckIndex = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x000592FF File Offset: 0x000574FF
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x00059307 File Offset: 0x00057507
		public int ReservedDungeonID
		{
			get
			{
				return this.reservedDungeonID;
			}
			set
			{
				this.reservedDungeonID = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00059310 File Offset: 0x00057510
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x00059318 File Offset: 0x00057518
		public int ReservedDeckIndex
		{
			get
			{
				return this.reservedDeckIndex;
			}
			set
			{
				this.reservedDeckIndex = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00059321 File Offset: 0x00057521
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x00059329 File Offset: 0x00057529
		public List<int> Artifacts
		{
			get
			{
				return this.artifacts;
			}
			set
			{
				this.artifacts = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00059332 File Offset: 0x00057532
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x0005933A File Offset: 0x0005753A
		public List<int> ReservedArtifacts
		{
			get
			{
				return this.reservedArtifacts;
			}
			set
			{
				this.reservedArtifacts = value;
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00059370 File Offset: 0x00057570
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKMDivePlayerState>(ref this.state);
			stream.PutOrGet(ref this.prevSlotSetIndex);
			stream.PutOrGet(ref this.prevSlotIndex);
			stream.PutOrGet(ref this.slotSetIndex);
			stream.PutOrGet(ref this.slotIndex);
			stream.PutOrGet(ref this.distance);
			stream.PutOrGet(ref this.leaderDeckIndex);
			stream.PutOrGet(ref this.reservedDungeonID);
			stream.PutOrGet(ref this.reservedDeckIndex);
			stream.PutOrGet(ref this.artifacts);
			stream.PutOrGet(ref this.reservedArtifacts);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00059404 File Offset: 0x00057604
		public void DeepCopyFromSource(NKMDivePlayerBase source)
		{
			this.state = source.state;
			this.prevSlotSetIndex = source.prevSlotSetIndex;
			this.prevSlotIndex = source.prevSlotIndex;
			this.slotSetIndex = source.slotSetIndex;
			this.slotIndex = source.slotIndex;
			this.distance = source.distance;
			this.leaderDeckIndex = source.leaderDeckIndex;
			this.reservedDungeonID = source.reservedDungeonID;
			this.reservedDeckIndex = source.reservedDeckIndex;
			this.artifacts.Clear();
			this.artifacts.AddRange(source.artifacts);
			this.reservedArtifacts.Clear();
			this.reservedArtifacts.AddRange(source.reservedArtifacts);
		}

		// Token: 0x04000EDB RID: 3803
		private NKMDivePlayerState state;

		// Token: 0x04000EDC RID: 3804
		private int prevSlotSetIndex;

		// Token: 0x04000EDD RID: 3805
		private int prevSlotIndex;

		// Token: 0x04000EDE RID: 3806
		private int slotSetIndex = -1;

		// Token: 0x04000EDF RID: 3807
		private int slotIndex;

		// Token: 0x04000EE0 RID: 3808
		private int distance;

		// Token: 0x04000EE1 RID: 3809
		private int leaderDeckIndex;

		// Token: 0x04000EE2 RID: 3810
		private int reservedDungeonID;

		// Token: 0x04000EE3 RID: 3811
		private int reservedDeckIndex = -1;

		// Token: 0x04000EE4 RID: 3812
		private List<int> artifacts = new List<int>();

		// Token: 0x04000EE5 RID: 3813
		private List<int> reservedArtifacts = new List<int>();
	}
}

using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000379 RID: 889
	public sealed class NKMDiveSyncData : ISerializable
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x000597FD File Offset: 0x000579FD
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x00059805 File Offset: 0x00057A05
		public NKMDivePlayerBase UpdatedPlayer
		{
			get
			{
				return this.updatedPlayer;
			}
			set
			{
				this.updatedPlayer = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0005980E File Offset: 0x00057A0E
		public List<NKMDiveSquad> UpdatedSquads
		{
			get
			{
				return this.updatedSquads;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00059816 File Offset: 0x00057A16
		public List<NKMDiveSlotSet> AddedSlotSets
		{
			get
			{
				return this.addedSlotSets;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0005981E File Offset: 0x00057A1E
		public List<NKMDiveSlotWithIndexes> UpdatedSlots
		{
			get
			{
				return this.updatedSlots;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00059826 File Offset: 0x00057A26
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x0005982E File Offset: 0x00057A2E
		public NKMRewardData RewardData
		{
			get
			{
				return this.rewardData;
			}
			set
			{
				this.rewardData = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00059837 File Offset: 0x00057A37
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x0005983F File Offset: 0x00057A3F
		public NKMRewardData ArtifactRewardData
		{
			get
			{
				return this.artifactRewardData;
			}
			set
			{
				this.artifactRewardData = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00059848 File Offset: 0x00057A48
		// (set) Token: 0x0600163E RID: 5694 RVA: 0x00059850 File Offset: 0x00057A50
		public NKMItemMiscData StormMiscReward
		{
			get
			{
				return this.stormMiscReward;
			}
			set
			{
				this.stormMiscReward = value;
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0005985C File Offset: 0x00057A5C
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDivePlayerBase>(ref this.updatedPlayer);
			stream.PutOrGet<NKMDiveSquad>(ref this.updatedSquads);
			stream.PutOrGet<NKMDiveSlotSet>(ref this.addedSlotSets);
			stream.PutOrGet<NKMDiveSlotWithIndexes>(ref this.updatedSlots);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMRewardData>(ref this.artifactRewardData);
			stream.PutOrGet<NKMItemMiscData>(ref this.stormMiscReward);
		}

		// Token: 0x04000EF5 RID: 3829
		private NKMDivePlayerBase updatedPlayer;

		// Token: 0x04000EF6 RID: 3830
		private List<NKMDiveSquad> updatedSquads = new List<NKMDiveSquad>();

		// Token: 0x04000EF7 RID: 3831
		private List<NKMDiveSlotSet> addedSlotSets = new List<NKMDiveSlotSet>();

		// Token: 0x04000EF8 RID: 3832
		private List<NKMDiveSlotWithIndexes> updatedSlots = new List<NKMDiveSlotWithIndexes>();

		// Token: 0x04000EF9 RID: 3833
		private NKMRewardData rewardData;

		// Token: 0x04000EFA RID: 3834
		private NKMRewardData artifactRewardData;

		// Token: 0x04000EFB RID: 3835
		private NKMItemMiscData stormMiscReward;
	}
}

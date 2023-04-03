using System;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000375 RID: 885
	public sealed class NKMDiveSlot : ISerializable
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000594B5 File Offset: 0x000576B5
		public NKM_DIVE_SECTOR_TYPE SectorType
		{
			get
			{
				return this.sectorType;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x000594BD File Offset: 0x000576BD
		public NKM_DIVE_EVENT_TYPE EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000594C5 File Offset: 0x000576C5
		public int EventValue
		{
			get
			{
				return this.eventValue;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x000594CD File Offset: 0x000576CD
		public int ArtifactEventRate
		{
			get
			{
				return this.artifactEventRate;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x000594D5 File Offset: 0x000576D5
		public int ArtifactRewardGroup
		{
			get
			{
				return this.artifactRewardGroup;
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000594DD File Offset: 0x000576DD
		public NKMDiveSlot()
		{
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000594E5 File Offset: 0x000576E5
		public NKMDiveSlot(NKM_DIVE_SECTOR_TYPE sectorType, NKM_DIVE_EVENT_TYPE eventType, int eventValue, int artifactEventRate, int artifactRewardGroup)
		{
			this.sectorType = sectorType;
			this.eventType = eventType;
			this.eventValue = eventValue;
			this.artifactEventRate = artifactEventRate;
			this.artifactRewardGroup = artifactRewardGroup;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00059512 File Offset: 0x00057712
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_DIVE_SECTOR_TYPE>(ref this.sectorType);
			stream.PutOrGetEnum<NKM_DIVE_EVENT_TYPE>(ref this.eventType);
			stream.PutOrGet(ref this.eventValue);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00059538 File Offset: 0x00057738
		public void UpdateEvent(NKM_DIVE_EVENT_TYPE type, int value)
		{
			this.eventType = type;
			this.eventValue = value;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00059548 File Offset: 0x00057748
		public NKMDiveSlot Clone()
		{
			return new NKMDiveSlot(this.sectorType, this.eventType, this.eventValue, this.artifactEventRate, this.artifactRewardGroup);
		}

		// Token: 0x04000EE6 RID: 3814
		private NKM_DIVE_SECTOR_TYPE sectorType;

		// Token: 0x04000EE7 RID: 3815
		private NKM_DIVE_EVENT_TYPE eventType;

		// Token: 0x04000EE8 RID: 3816
		private int eventValue;

		// Token: 0x04000EE9 RID: 3817
		private int artifactEventRate;

		// Token: 0x04000EEA RID: 3818
		private int artifactRewardGroup;
	}
}

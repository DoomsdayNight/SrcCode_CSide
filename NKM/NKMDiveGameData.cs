using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200036E RID: 878
	public class NKMDiveGameData : ISerializable
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00058A7B File Offset: 0x00056C7B
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x00058A83 File Offset: 0x00056C83
		public long DiveUid
		{
			get
			{
				return this.diveUid;
			}
			set
			{
				this.diveUid = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00058A8C File Offset: 0x00056C8C
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x00058A94 File Offset: 0x00056C94
		public NKMDiveFloor Floor
		{
			get
			{
				return this.floor;
			}
			set
			{
				this.floor = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00058A9D File Offset: 0x00056C9D
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x00058AA5 File Offset: 0x00056CA5
		public NKMDivePlayer Player
		{
			get
			{
				return this.player;
			}
			set
			{
				this.player = value;
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00058AB6 File Offset: 0x00056CB6
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.diveUid);
			stream.PutOrGet<NKMDiveFloor>(ref this.floor);
			stream.PutOrGet<NKMDivePlayer>(ref this.player);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00058ADC File Offset: 0x00056CDC
		public void UpdateData(bool isWin, NKMDiveSyncData cNKMDiveSyncData)
		{
			if (cNKMDiveSyncData == null)
			{
				return;
			}
			for (int i = 0; i < cNKMDiveSyncData.UpdatedSlots.Count; i++)
			{
				NKMDiveSlotWithIndexes nkmdiveSlotWithIndexes = cNKMDiveSyncData.UpdatedSlots[i];
				if (nkmdiveSlotWithIndexes != null)
				{
					NKMDiveSlot slot = this.Floor.GetSlot(nkmdiveSlotWithIndexes.SlotSetIndex, nkmdiveSlotWithIndexes.SlotIndex);
					if (slot != null)
					{
						slot.DeepCopyFrom(nkmdiveSlotWithIndexes.Slot);
					}
				}
			}
			if (cNKMDiveSyncData.UpdatedPlayer != null && isWin)
			{
				this.Floor.Rebuild(this.Player.PlayerBase.Distance, this.Player.PlayerBase.SlotSetIndex, this.Player.PlayerBase.SlotIndex);
			}
			if (cNKMDiveSyncData.UpdatedPlayer != null)
			{
				this.Player.PlayerBase.DeepCopyFromSource(cNKMDiveSyncData.UpdatedPlayer);
			}
			if (cNKMDiveSyncData.UpdatedSquads.Count > 0)
			{
				for (int j = 0; j < cNKMDiveSyncData.UpdatedSquads.Count; j++)
				{
					NKMDiveSquad nkmdiveSquad = cNKMDiveSyncData.UpdatedSquads[j];
					if (nkmdiveSquad != null)
					{
						NKMDiveSquad nkmdiveSquad2 = null;
						this.Player.Squads.TryGetValue(nkmdiveSquad.DeckIndex, out nkmdiveSquad2);
						if (nkmdiveSquad2 != null)
						{
							nkmdiveSquad2.DeepCopyFromSource(nkmdiveSquad);
						}
					}
				}
			}
		}

		// Token: 0x04000EC5 RID: 3781
		private long diveUid;

		// Token: 0x04000EC6 RID: 3782
		private NKMDiveFloor floor;

		// Token: 0x04000EC7 RID: 3783
		private NKMDivePlayer player;
	}
}

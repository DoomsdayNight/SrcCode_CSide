using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000373 RID: 883
	public class NKMDivePlayer : ISerializable
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0005903C File Offset: 0x0005723C
		public NKMDivePlayerBase PlayerBase
		{
			get
			{
				return this.playerBase;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00059044 File Offset: 0x00057244
		public Dictionary<int, NKMDiveSquad> Squads
		{
			get
			{
				return this.squads;
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0005906A File Offset: 0x0005726A
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDivePlayerBase>(ref this.playerBase);
			stream.PutOrGet<NKMDiveSquad>(ref this.squads);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00059084 File Offset: 0x00057284
		public NKMDiveSquad GetSquad(int deckIndex)
		{
			NKMDiveSquad result = null;
			if (!this.squads.TryGetValue(deckIndex, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000590A8 File Offset: 0x000572A8
		public void GetMovableSlotRange(NKMDiveFloor floor, int nextSlotSetIndex, out int minSlotIndex, out int maxSlotIndex)
		{
			int num = floor.SlotSets[nextSlotSetIndex].Slots.Count - 1;
			int num2 = floor.Templet.RandomSetCount + 1;
			if (this.playerBase.Distance == 0)
			{
				minSlotIndex = 0;
				maxSlotIndex = num;
				return;
			}
			if (this.playerBase.Distance == num2 - 1)
			{
				minSlotIndex = 0;
				maxSlotIndex = 0;
				return;
			}
			minSlotIndex = this.playerBase.SlotIndex - 1;
			maxSlotIndex = this.playerBase.SlotIndex + 1;
			minSlotIndex = Math.Max(0, minSlotIndex);
			maxSlotIndex = Math.Min(maxSlotIndex, num);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00059140 File Offset: 0x00057340
		public bool CanMove(NKMDiveFloor floor, int nextSlotSetIndex, int slotIndex)
		{
			int num = floor.Templet.RandomSetCount + 1;
			if (this.playerBase.Distance == num)
			{
				return false;
			}
			int num2;
			int num3;
			this.GetMovableSlotRange(floor, nextSlotSetIndex, out num2, out num3);
			return num2 <= slotIndex && slotIndex <= num3;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00059182 File Offset: 0x00057382
		public int GetNextSlotSetIndex()
		{
			if (this.playerBase.Distance != 0)
			{
				return this.playerBase.SlotSetIndex + 1;
			}
			return 0;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000591A0 File Offset: 0x000573A0
		public bool IsInBattle()
		{
			return this.playerBase.State == NKMDivePlayerState.BattleLoad || this.playerBase.State == NKMDivePlayerState.Battle;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000591C0 File Offset: 0x000573C0
		public bool CanGetMoreArtifact()
		{
			return this.playerBase.Artifacts.Count < 50;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000591D8 File Offset: 0x000573D8
		public NKMDiveSquad GetSquadForBattleByAuto()
		{
			List<NKMDiveSquad> list = new List<NKMDiveSquad>(this.squads.Values);
			list.Sort(new NKMDivePlayer.CompDiveSquad());
			if (list.Count > 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00059214 File Offset: 0x00057414
		public bool CheckExistPossibleSquadForBattle()
		{
			foreach (KeyValuePair<int, NKMDiveSquad> keyValuePair in this.squads)
			{
				NKMDiveSquad value = keyValuePair.Value;
				if (value != null && value.CurHp > 0f && value.Supply > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000ED8 RID: 3800
		private const int MaxArtifactCount = 50;

		// Token: 0x04000ED9 RID: 3801
		private NKMDivePlayerBase playerBase = new NKMDivePlayerBase();

		// Token: 0x04000EDA RID: 3802
		private Dictionary<int, NKMDiveSquad> squads = new Dictionary<int, NKMDiveSquad>();

		// Token: 0x02001188 RID: 4488
		private class CompDiveSquad : IComparer<NKMDiveSquad>
		{
			// Token: 0x06009FF2 RID: 40946 RVA: 0x0033D708 File Offset: 0x0033B908
			public int Compare(NKMDiveSquad x, NKMDiveSquad y)
			{
				if (x.CurHp <= 0f)
				{
					return 1;
				}
				if (y.CurHp <= 0f)
				{
					return -1;
				}
				if (x.MaxHp <= 0f)
				{
					return 1;
				}
				if (y.MaxHp <= 0f)
				{
					return -1;
				}
				float value = x.CurHp / x.MaxHp;
				return (y.CurHp / y.MaxHp).CompareTo(value);
			}
		}
	}
}

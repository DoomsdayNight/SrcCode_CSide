using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000378 RID: 888
	public sealed class NKMDiveSquad : ISerializable
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00059622 File Offset: 0x00057822
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x0005962A File Offset: 0x0005782A
		public NKMDiveSquadState State
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00059633 File Offset: 0x00057833
		// (set) Token: 0x06001624 RID: 5668 RVA: 0x0005963B File Offset: 0x0005783B
		public int DeckIndex
		{
			get
			{
				return this.deckIndex;
			}
			set
			{
				this.deckIndex = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00059644 File Offset: 0x00057844
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x0005964C File Offset: 0x0005784C
		public float CurHp
		{
			get
			{
				return this.curHp;
			}
			set
			{
				this.curHp = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x00059655 File Offset: 0x00057855
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x0005965D File Offset: 0x0005785D
		public float MaxHp
		{
			get
			{
				return this.maxHp;
			}
			set
			{
				this.maxHp = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x00059666 File Offset: 0x00057866
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x0005966E File Offset: 0x0005786E
		public int Supply
		{
			get
			{
				return this.supply;
			}
			set
			{
				this.supply = value;
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00059677 File Offset: 0x00057877
		public NKMDiveSquad()
		{
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0005967F File Offset: 0x0005787F
		public NKMDiveSquad(int deckIndex, float hp)
		{
			this.deckIndex = deckIndex;
			this.curHp = hp;
			this.maxHp = hp;
			this.supply = 2;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000596A3 File Offset: 0x000578A3
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKMDiveSquadState>(ref this.state);
			stream.PutOrGet(ref this.deckIndex);
			stream.PutOrGet(ref this.curHp);
			stream.PutOrGet(ref this.maxHp);
			stream.PutOrGet(ref this.supply);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000596E1 File Offset: 0x000578E1
		public void DeepCopyFromSource(NKMDiveSquad source)
		{
			this.state = source.state;
			this.deckIndex = source.deckIndex;
			this.curHp = source.curHp;
			this.maxHp = source.maxHp;
			this.supply = source.supply;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0005971F File Offset: 0x0005791F
		public bool isDead()
		{
			return this.State == NKMDiveSquadState.Dead;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0005972C File Offset: 0x0005792C
		public void ChangeHp(float hp, float minHp)
		{
			this.curHp += hp;
			this.curHp = Math.Max(minHp, this.curHp);
			this.curHp = Math.Min(this.curHp, this.maxHp);
			if (this.curHp <= 0f)
			{
				this.state = NKMDiveSquadState.Dead;
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00059784 File Offset: 0x00057984
		public void ChangeHpByPercentage(float percentage)
		{
			float hp = this.MaxHp * (percentage / 100f);
			float minHp = this.MaxHp * 0.01f;
			this.ChangeHp(hp, minHp);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000597B5 File Offset: 0x000579B5
		public void ChangeSupply(int supply)
		{
			this.supply += supply;
			this.supply = Math.Max(0, this.supply);
			this.supply = Math.Min(this.supply, 2);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000597E9 File Offset: 0x000579E9
		public void Kill()
		{
			this.curHp = 0f;
			this.state = NKMDiveSquadState.Dead;
		}

		// Token: 0x04000EEF RID: 3823
		private const int DEFAULT_SQUAD_SUPPLY = 2;

		// Token: 0x04000EF0 RID: 3824
		private NKMDiveSquadState state;

		// Token: 0x04000EF1 RID: 3825
		private int deckIndex;

		// Token: 0x04000EF2 RID: 3826
		private float curHp;

		// Token: 0x04000EF3 RID: 3827
		private float maxHp;

		// Token: 0x04000EF4 RID: 3828
		private int supply;
	}
}

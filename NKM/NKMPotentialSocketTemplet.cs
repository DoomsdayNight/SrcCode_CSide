using System;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200038C RID: 908
	public sealed class NKMPotentialSocketTemplet
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x0005E340 File Offset: 0x0005C540
		public NKMPotentialSocketTemplet(NKMPotentialOptionTemplet owner, int socketNumber)
		{
			this.owner = owner;
			this.SocketNumber = socketNumber;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0005E356 File Offset: 0x0005C556
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x0005E35E File Offset: 0x0005C55E
		public int SocketNumber { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0005E367 File Offset: 0x0005C567
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x0005E36F File Offset: 0x0005C56F
		public StatApplyType ApplyType { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0005E378 File Offset: 0x0005C578
		public float MinStat
		{
			get
			{
				if (this.ApplyType != StatApplyType.Addable)
				{
					return this.minStatFactor;
				}
				return this.minStatValue;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0005E38F File Offset: 0x0005C58F
		public float MaxStat
		{
			get
			{
				if (this.ApplyType != StatApplyType.Addable)
				{
					return this.maxStatFactor;
				}
				return this.maxStatValue;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0005E3A6 File Offset: 0x0005C5A6
		public bool IsPrecentStat
		{
			get
			{
				return this.ApplyType == StatApplyType.Multipliable || NKMUnitStatManager.IsPercentStat(this.owner.StatType);
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
		public static NKMPotentialSocketTemplet Create(NKMPotentialOptionTemplet owner, int socketNumber, NKMLua lua)
		{
			NKMPotentialSocketTemplet nkmpotentialSocketTemplet = new NKMPotentialSocketTemplet(owner, socketNumber);
			lua.GetData(string.Format("Socket{0}_MinStat", socketNumber), ref nkmpotentialSocketTemplet.minStatValue);
			lua.GetData(string.Format("Socket{0}_MaxStat", socketNumber), ref nkmpotentialSocketTemplet.maxStatValue);
			lua.GetData(string.Format("Socket{0}_MinStatRate", socketNumber), ref nkmpotentialSocketTemplet.minStatFactor);
			lua.GetData(string.Format("Socket{0}_MaxStatRate", socketNumber), ref nkmpotentialSocketTemplet.maxStatFactor);
			return nkmpotentialSocketTemplet;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005E450 File Offset: 0x0005C650
		public float CalcStatValue(int precision)
		{
			if (this.ApplyType == StatApplyType.Multipliable)
			{
				return 0f;
			}
			float num = (float)precision / 100f;
			float num2;
			if (this.maxStatValue < 0f && this.minStatValue < 0f)
			{
				num2 = (this.minStatValue - this.maxStatValue) * num + this.maxStatValue;
			}
			else
			{
				num2 = (this.maxStatValue - this.minStatValue) * num + this.minStatValue;
			}
			if (NKMUnitStatManager.IsPercentStat(this.owner.StatType))
			{
				return (float)(Math.Truncate((double)(num2 * 10000f)) / 10000.0);
			}
			return (float)Math.Truncate((double)num2);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0005E4F4 File Offset: 0x0005C6F4
		public float CalcStatFactor(int precision)
		{
			if (this.ApplyType == StatApplyType.Addable)
			{
				return 0f;
			}
			float num = (float)precision / 100f;
			return (float)(Math.Truncate((double)(((this.maxStatFactor - this.minStatFactor) * num + this.minStatFactor) * 10000f)) / 10000.0);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0005E546 File Offset: 0x0005C746
		public float CalcStat(int precision)
		{
			if (this.ApplyType != StatApplyType.Addable)
			{
				return this.CalcStatFactor(precision);
			}
			return this.CalcStatValue(precision);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0005E560 File Offset: 0x0005C760
		public void Validate(int optionKey)
		{
			string text = string.Format("[PotentialSocket:{0}]", optionKey);
			if (this.minStatValue != 0f && this.maxStatValue != 0f)
			{
				this.ApplyType = StatApplyType.Addable;
				if (this.minStatFactor > 0f || this.maxStatFactor > 0f)
				{
					NKMTempletError.Add(string.Format("{0} ������ ���ȿ� ���� ��ġ�� ����. socketNumber:{1}", text, this.SocketNumber), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 94);
				}
				if (this.minStatValue > this.maxStatValue)
				{
					NKMTempletError.Add(string.Format("{0} ��ġ ���� ����. socketNumber:{1} minStat:{2} maxStat:{3}", new object[]
					{
						text,
						this.SocketNumber,
						this.minStatValue,
						this.maxStatValue
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 99);
				}
				if (this.minStatValue * this.maxStatValue < 0f)
				{
					NKMTempletError.Add(string.Format("{0} ��ġ ���� minmax�� ��ȣ�� �ٸ�.. socketNumber:{1} minStat:{2} maxStat:{3}", new object[]
					{
						text,
						this.SocketNumber,
						this.minStatValue,
						this.maxStatValue
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 104);
					return;
				}
			}
			else
			{
				this.ApplyType = StatApplyType.Multipliable;
				if (this.minStatValue > 0f || this.maxStatValue > 0f)
				{
					NKMTempletError.Add(string.Format("{0} ������ ���ȿ� ���� ��ġ�� ����. socketNumber:{1}", text, this.SocketNumber), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 113);
				}
				if (this.minStatFactor > this.maxStatFactor)
				{
					NKMTempletError.Add(string.Format("{0} ��ġ ���� ����. socketNumber:{1} minStat:{2} maxStat:{3}", new object[]
					{
						text,
						this.SocketNumber,
						this.minStatFactor,
						this.maxStatFactor
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionTemplet.cs", 118);
				}
			}
		}

		// Token: 0x04000FBC RID: 4028
		private readonly NKMPotentialOptionTemplet owner;

		// Token: 0x04000FBD RID: 4029
		private float minStatValue;

		// Token: 0x04000FBE RID: 4030
		private float maxStatValue;

		// Token: 0x04000FBF RID: 4031
		private float minStatFactor;

		// Token: 0x04000FC0 RID: 4032
		private float maxStatFactor;
	}
}

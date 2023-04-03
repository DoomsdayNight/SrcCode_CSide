using System;
using System.Collections.Generic;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200046A RID: 1130
	public sealed class NKMCommandModuleRandomStatTemplet : INKMTemplet
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000918B1 File Offset: 0x0008FAB1
		public int Key
		{
			get
			{
				return this.statGroupId;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x000918B9 File Offset: 0x0008FAB9
		public int StatGroupId
		{
			get
			{
				return this.statGroupId;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000918C1 File Offset: 0x0008FAC1
		public NKM_STAT_TYPE StatType
		{
			get
			{
				return this.statType;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x000918C9 File Offset: 0x0008FAC9
		public float MinStatValue
		{
			get
			{
				return this.minStatValue;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x000918D1 File Offset: 0x0008FAD1
		public float MaxStatValue
		{
			get
			{
				return this.maxStatValue;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x000918D9 File Offset: 0x0008FAD9
		public float StatValueControl
		{
			get
			{
				return this.statValueControl;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000918E1 File Offset: 0x0008FAE1
		public float MinStatFactor
		{
			get
			{
				return this.minStatFactor;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x000918E9 File Offset: 0x0008FAE9
		public float MaxStatFactor
		{
			get
			{
				return this.maxStatFactor;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000918F1 File Offset: 0x0008FAF1
		public float StatFactorControl
		{
			get
			{
				return this.statFactorControl;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000918F9 File Offset: 0x0008FAF9
		public static IEnumerable<NKMCommandModuleRandomStatTemplet> Values
		{
			get
			{
				return NKMTempletContainer<NKMCommandModuleRandomStatTemplet>.Values;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00091900 File Offset: 0x0008FB00
		public IReadOnlyList<float> GetCandidateValues
		{
			get
			{
				return this.CandidateValues;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00091908 File Offset: 0x0008FB08
		public IReadOnlyList<float> GetCandidateFactors
		{
			get
			{
				return this.CandidateFactors;
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00091910 File Offset: 0x0008FB10
		public static NKMCommandModuleRandomStatTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 745))
			{
				return null;
			}
			NKMCommandModuleRandomStatTemplet nkmcommandModuleRandomStatTemplet = new NKMCommandModuleRandomStatTemplet
			{
				statGroupId = lua.GetInt32("StatGroupID")
			};
			nkmcommandModuleRandomStatTemplet.minStatValue = lua.GetFloat("MinStatValue", 0f);
			nkmcommandModuleRandomStatTemplet.maxStatValue = lua.GetFloat("MaxStatValue", 0f);
			nkmcommandModuleRandomStatTemplet.statValueControl = lua.GetFloat("StatValueControl", 0f);
			nkmcommandModuleRandomStatTemplet.minStatFactor = lua.GetFloat("MinStatFactor", 0f);
			nkmcommandModuleRandomStatTemplet.maxStatFactor = lua.GetFloat("MaxStatFactor", 0f);
			nkmcommandModuleRandomStatTemplet.statFactorControl = lua.GetFloat("StatFactorControl", 0f);
			lua.GetDataEnum<NKM_STAT_TYPE>("StatType", out nkmcommandModuleRandomStatTemplet.statType);
			return nkmcommandModuleRandomStatTemplet;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000919E0 File Offset: 0x0008FBE0
		public void Join()
		{
			if (this.minStatValue != 0f && this.maxStatValue != 0f && this.statValueControl > 0f)
			{
				for (float num = this.minStatValue; num <= this.maxStatValue; num = (float)(Math.Round((double)((num + this.statValueControl) * 10000f)) / 10000.0))
				{
					this.CandidateValues.Add(num);
				}
			}
			if (this.minStatFactor != 0f && this.maxStatFactor != 0f && this.statFactorControl > 0f)
			{
				for (float num2 = this.minStatFactor; num2 <= this.maxStatFactor; num2 = (float)(Math.Round((double)((num2 + this.statFactorControl) * 10000f)) / 10000.0))
				{
					this.CandidateFactors.Add(num2);
				}
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00091AB8 File Offset: 0x0008FCB8
		public void Validate()
		{
			if (this.minStatValue != 0f && this.maxStatValue != 0f && this.statValueControl > 0f)
			{
				if (this.StatType != NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE && (this.minStatValue < 0f || this.maxStatValue < 0f))
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] ���� ��� statType�� �ƴѵ� Value���� ������ �������. minStatValue:{1} minStatValue:{2} maxStatValue:{3}", new object[]
					{
						this.statGroupId,
						this.minStatValue,
						this.minStatValue,
						this.maxStatValue
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 803);
				}
				if (this.maxStatValue < this.minStatValue)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statValue���� �̻���. minStatValue:{1} maxStatValue:{2}", this.statGroupId, this.minStatValue, this.maxStatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 808);
				}
				if (this.minStatValue == this.maxStatValue && this.statValueControl != 0f)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statValue���� ������ Control���� ����. statValueControl:{1}", this.statGroupId, this.statValueControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 813);
				}
				if (this.StatType == NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE && this.minStatValue < 0f && this.maxStatValue < 0f)
				{
					if (this.maxStatValue * -1f < this.statValueControl)
					{
						NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statValueControl���� ������ maxStatValue:{1} < statValueControl:{2}.", this.statGroupId, this.maxStatValue, this.statValueControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 821);
					}
				}
				else if (this.maxStatValue < this.statValueControl)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statValueControl���� ������ maxStatValue:{1} < statValueControl:{2}.", this.statGroupId, this.maxStatValue, this.statValueControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 826);
				}
				if (this.statValueControl <= 0f)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statValueControl���� ������ statValueControl:{1} <= 0", this.statGroupId, this.statValueControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 831);
					return;
				}
			}
			else if (this.minStatFactor != 0f && this.maxStatFactor != 0f && this.statFactorControl > 0f)
			{
				if (this.StatType != NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE && (this.minStatFactor < 0f || this.maxStatFactor < 0f))
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] ���� ��� statType�� �ƴѵ� Factor���� ������ �������.. minStatValue:{1} minStatFactor:{2} maxStatFactor:{3}", new object[]
					{
						this.statGroupId,
						this.minStatValue,
						this.minStatFactor,
						this.maxStatFactor
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 841);
				}
				if (this.maxStatFactor < this.minStatFactor)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statFactor���� �̻���. minStatValue:{1} maxStatValue:{2}", this.statGroupId, this.minStatFactor, this.maxStatFactor), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 846);
				}
				if (this.minStatFactor == this.maxStatFactor && this.statFactorControl != 0f)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statFactor���� ������ Control���� ����. statValueControl:{1}", this.statGroupId, this.statValueControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 851);
				}
				if (this.StatType == NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE && this.minStatFactor < 0f && this.maxStatFactor < 0f)
				{
					if (this.maxStatFactor < this.statFactorControl)
					{
						NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statFactorControl���� ������ maxStatFactor:{1} < statFactorControl:{2}.", this.statGroupId, this.maxStatFactor, this.statFactorControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 859);
					}
				}
				else if (this.maxStatFactor < this.statFactorControl)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statFactorControl���� ������ maxStatFactor:{1} < statFactorControl:{2}.", this.statGroupId, this.maxStatFactor, this.statFactorControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 864);
				}
				if (this.statFactorControl <= 0f)
				{
					NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] statValueControl���� ������ statFactorControl:{1} <= 0", this.statGroupId, this.statFactorControl), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 869);
					return;
				}
			}
			else
			{
				string arg = string.Format("minStatValue:{0} maxStatValue:{1} statValueControl:{2}", this.minStatValue, this.maxStatValue, this.statValueControl);
				string arg2 = string.Format("minStatValue:{0} maxStatValue:{1} statFactorControl:{2}", this.minStatFactor, this.maxStatFactor, this.statFactorControl);
				NKMTempletError.Add(string.Format("[NKMCommandModuleRandomStatTemplet:{0}] ��ü���� �Է°��� �̻���. {1} {2}", this.statGroupId, arg, arg2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 876);
			}
		}

		// Token: 0x04001F35 RID: 7989
		private int statGroupId;

		// Token: 0x04001F36 RID: 7990
		private NKM_STAT_TYPE statType;

		// Token: 0x04001F37 RID: 7991
		private float minStatValue;

		// Token: 0x04001F38 RID: 7992
		private float maxStatValue;

		// Token: 0x04001F39 RID: 7993
		private float statValueControl;

		// Token: 0x04001F3A RID: 7994
		private float minStatFactor;

		// Token: 0x04001F3B RID: 7995
		private float maxStatFactor;

		// Token: 0x04001F3C RID: 7996
		private float statFactorControl;

		// Token: 0x04001F3D RID: 7997
		private List<float> CandidateValues = new List<float>();

		// Token: 0x04001F3E RID: 7998
		private List<float> CandidateFactors = new List<float>();
	}
}

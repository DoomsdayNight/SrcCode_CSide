using System;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003F5 RID: 1013
	public class NKMEquipRandomStatTemplet : INKMTemplet
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0007636B File Offset: 0x0007456B
		public int Key
		{
			get
			{
				return this.m_StatGroupID;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00076373 File Offset: 0x00074573
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0007637B File Offset: 0x0007457B
		public StatApplyType ApplyType { get; private set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00076384 File Offset: 0x00074584
		private string DebugName
		{
			get
			{
				return string.Format("[EquipStat {0} {1}]", this.m_StatGroupID, this.m_StatType);
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000763A8 File Offset: 0x000745A8
		public static NKMEquipRandomStatTemplet LoadFromLUA(NKMLua lua)
		{
			NKMEquipRandomStatTemplet nkmequipRandomStatTemplet = new NKMEquipRandomStatTemplet();
			nkmequipRandomStatTemplet.m_StatGroupID = lua.GetInt32("m_StatGroupID");
			nkmequipRandomStatTemplet.m_StatType = lua.GetEnum<NKM_STAT_TYPE>("m_StatType");
			lua.GetData("m_MinStatValue", ref nkmequipRandomStatTemplet.m_MinStatValue);
			lua.GetData("m_MaxStatValue", ref nkmequipRandomStatTemplet.m_MaxStatValue);
			lua.GetData("m_MinStatRate", ref nkmequipRandomStatTemplet.m_MinStatRate);
			lua.GetData("m_MaxStatRate", ref nkmequipRandomStatTemplet.m_MaxStatRate);
			if (!nkmequipRandomStatTemplet.Initialize())
			{
				Log.Error("NKMEquipRandomStatTemplet LoadFromLUA Fail - " + nkmequipRandomStatTemplet.DebugName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 43);
				return null;
			}
			return nkmequipRandomStatTemplet;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0007644C File Offset: 0x0007464C
		private bool Initialize()
		{
			if (this.m_MinStatValue != 0f && this.m_MaxStatValue != 0f)
			{
				this.ApplyType = StatApplyType.Addable;
			}
			else
			{
				if (this.m_MinStatRate <= 0f || this.m_MaxStatRate <= 0f)
				{
					NKMTempletError.Add(this.DebugName + " 수치값으로 타입을 정할 수 없음.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 63);
					return false;
				}
				this.ApplyType = StatApplyType.Multipliable;
			}
			return true;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000764BE File Offset: 0x000746BE
		public void Join()
		{
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000764C0 File Offset: 0x000746C0
		public void Validate()
		{
			StatApplyType applyType = this.ApplyType;
			if (applyType != StatApplyType.Addable)
			{
				if (applyType != StatApplyType.Multipliable)
				{
					throw new Exception(string.Format("unknown type:{0}", this.ApplyType));
				}
				if (this.m_MinStatValue > 0f || this.m_MaxStatValue > 0f)
				{
					NKMTempletError.Add(string.Format("{0} multipliable 타입이 덧셈수치를 가짐. rate:{1}~{2}", this.DebugName, this.m_MinStatValue, this.m_MaxStatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 115);
				}
				if (this.m_MinStatRate > this.m_MaxStatRate)
				{
					NKMTempletError.Add(string.Format("{0} multipliable min~max 수치이상. value:{1}~{2}", this.DebugName, this.m_MinStatRate, this.m_MaxStatRate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 120);
				}
				if (this.m_MinStatRate >= 1f || this.m_MaxStatRate >= 1f)
				{
					NKMTempletError.Add(string.Format("{0} multipliable min~max 수치 1.0 초과. value:{1}~{2}", this.DebugName, this.m_MinStatRate, this.m_MaxStatRate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 125);
					return;
				}
			}
			else
			{
				if (NKMUnitStatManager.IsMainStat(this.m_StatType))
				{
					if (this.m_MinStatValue < 1f || this.m_MaxStatValue < 1f)
					{
						NKMTempletError.Add(string.Format("{0} 1차스탯 min~max 덧셈(value) 수치 1.0 미만. value:{1}~{2}", this.DebugName, this.m_MinStatValue, this.m_MaxStatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 84);
					}
				}
				else if (this.m_MinStatValue >= 1f || this.m_MaxStatValue >= 1f)
				{
					NKMTempletError.Add(string.Format("{0} 2차스탯 min~max 덧셈(value) 수치 1.0 이상. value:{1}~{2}", this.DebugName, this.m_MinStatValue, this.m_MaxStatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 89);
				}
				else if (this.m_MinStatValue <= -1f || this.m_MaxStatValue <= -1f)
				{
					NKMTempletError.Add(string.Format("{0} 2차스탯(음수) min~max 덧셈(value) 수치 -1.0 이하. value:{1}~{2}", this.DebugName, this.m_MinStatValue, this.m_MaxStatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 93);
				}
				if (this.m_MinStatRate > 0f || this.m_MaxStatRate > 0f)
				{
					NKMTempletError.Add(string.Format("{0} addable 타입이 비율수치를 가짐. rate:{1}~{2}", this.DebugName, this.m_MinStatRate, this.m_MaxStatRate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 98);
				}
				if (this.m_MinStatValue > this.m_MaxStatValue)
				{
					NKMTempletError.Add(string.Format("{0} addable min~max 수치이상. value:{1}~{2}", this.DebugName, this.m_MinStatValue, this.m_MaxStatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 103);
				}
				if (this.m_MinStatValue * this.m_MaxStatValue < 0f)
				{
					NKMTempletError.Add(string.Format("{0} addable min~max값의 부호가 다름. value:{1}~{2}", this.DebugName, this.m_MinStatValue, this.m_MaxStatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 108);
					return;
				}
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000767A8 File Offset: 0x000749A8
		public EQUIP_ITEM_STAT GenerateSubStat(int precision)
		{
			return new EQUIP_ITEM_STAT
			{
				type = this.m_StatType,
				stat_value = this.CalcStatValue(precision),
				stat_factor = this.CalcStatFactor(precision)
			};
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000767D5 File Offset: 0x000749D5
		public float CalcResultStat(int precision)
		{
			if (this.ApplyType != StatApplyType.Addable)
			{
				return this.CalcStatFactor(precision);
			}
			return this.CalcStatValue(precision);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000767F0 File Offset: 0x000749F0
		public bool IsInitialPrecisionMax()
		{
			StatApplyType applyType = this.ApplyType;
			if (applyType != StatApplyType.Addable)
			{
				return applyType == StatApplyType.Multipliable && this.m_MinStatRate == this.m_MaxStatRate;
			}
			return this.m_MinStatValue == this.m_MaxStatValue;
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0007682C File Offset: 0x00074A2C
		private float CalcStatValue(int precision)
		{
			float num = (float)precision / 100f;
			float num2;
			if (this.m_MaxStatValue < 0f && this.m_MinStatValue < 0f)
			{
				num2 = (this.m_MinStatValue - this.m_MaxStatValue) * num + this.m_MaxStatValue;
			}
			else
			{
				num2 = (this.m_MaxStatValue - this.m_MinStatValue) * num + this.m_MinStatValue;
			}
			if (NKMUnitStatManager.IsPercentStat(this.m_StatType))
			{
				return (float)(Math.Truncate((double)(num2 * 10000f)) / 10000.0);
			}
			return (float)Math.Truncate((double)num2);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000768BC File Offset: 0x00074ABC
		private float CalcStatFactor(int precision)
		{
			float num = (float)precision / 100f;
			return (float)(Math.Truncate((double)(((this.m_MaxStatRate - this.m_MinStatRate) * num + this.m_MinStatRate) * 10000f)) / 10000.0);
		}

		// Token: 0x040013E3 RID: 5091
		public int m_StatGroupID;

		// Token: 0x040013E4 RID: 5092
		public NKM_STAT_TYPE m_StatType;

		// Token: 0x040013E5 RID: 5093
		public float m_MinStatValue;

		// Token: 0x040013E6 RID: 5094
		public float m_MaxStatValue;

		// Token: 0x040013E7 RID: 5095
		public float m_MinStatRate;

		// Token: 0x040013E8 RID: 5096
		public float m_MaxStatRate;
	}
}

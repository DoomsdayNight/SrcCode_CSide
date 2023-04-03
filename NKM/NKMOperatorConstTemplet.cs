using System;
using System.Collections.Generic;
using NKM.Templet;

namespace NKM
{
	// Token: 0x0200051A RID: 1306
	public class NKMOperatorConstTemplet
	{
		// Token: 0x06002554 RID: 9556 RVA: 0x000C0874 File Offset: 0x000BEA74
		public void LoadFromLua(NKMLua lua)
		{
			using (lua.OpenTable("MaterialUnit", "Operator MaterialUnit table failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorConstTemplet.cs", 17))
			{
				int num = 1;
				while (lua.OpenTable(num++))
				{
					NKM_UNIT_GRADE @enum = lua.GetEnum<NKM_UNIT_GRADE>("m_NKM_UNIT_GRADE");
					int @int = lua.GetInt32("LevelUpSuccessRatePercent");
					int int2 = lua.GetInt32("TransportSuccessRatePercent");
					this.materialUntis.Add(new NKMOperatorConstTemplet.MaterialUnit(@enum, @int, int2));
					lua.CloseTable();
				}
			}
			using (lua.OpenTable("HostUnit", "Operator HostUnit table failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorConstTemplet.cs", 30))
			{
				int num2 = 1;
				while (lua.OpenTable(num2++))
				{
					NKM_UNIT_GRADE enum2 = lua.GetEnum<NKM_UNIT_GRADE>("m_NKM_UNIT_GRADE");
					int int3 = lua.GetInt32("ItemId");
					int int4 = lua.GetInt32("ItemCount");
					this.hostUnits.Add(new NKMOperatorConstTemplet.HostUnit(enum2, int3, int4));
					lua.CloseTable();
				}
			}
			using (lua.OpenTable("Negotiation", "Operator Negotiation table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorConstTemplet.cs", 43))
			{
				lua.GetData("MaxMaterialUsageLimit", ref this.maxMaterialUsageLimit);
				using (lua.OpenTable("Materials", "Operator Negotiation loading materials table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorConstTemplet.cs", 47))
				{
					int itemId = 0;
					int exp = 0;
					int credit = 0;
					for (int i = 0; i < 3; i++)
					{
						int iIndex = i + 1;
						if (lua.OpenTable(iIndex))
						{
							lua.GetData("ItemId", ref itemId);
							lua.GetData("Exp", ref exp);
							lua.GetData("Credit", ref credit);
							this.list[i] = new NKMOperatorConstTemplet.Negotiation(itemId, exp, credit);
							lua.CloseTable();
						}
					}
				}
			}
			using (lua.OpenTable("Const", "Operator Const table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorConstTemplet.cs", 68))
			{
				lua.GetData("MaximumLevel", ref this.unitMaximumLevel);
			}
		}

		// Token: 0x040026AB RID: 9899
		private const int MaxKindOfMaterialCount = 3;

		// Token: 0x040026AC RID: 9900
		public int unitMaximumLevel;

		// Token: 0x040026AD RID: 9901
		private int maxMaterialUsageLimit = 200;

		// Token: 0x040026AE RID: 9902
		public NKMOperatorConstTemplet.Negotiation[] list = new NKMOperatorConstTemplet.Negotiation[3];

		// Token: 0x040026AF RID: 9903
		public List<NKMOperatorConstTemplet.MaterialUnit> materialUntis = new List<NKMOperatorConstTemplet.MaterialUnit>();

		// Token: 0x040026B0 RID: 9904
		public List<NKMOperatorConstTemplet.HostUnit> hostUnits = new List<NKMOperatorConstTemplet.HostUnit>();

		// Token: 0x02001241 RID: 4673
		public class Negotiation
		{
			// Token: 0x0600A28F RID: 41615 RVA: 0x00341391 File Offset: 0x0033F591
			public Negotiation(int itemId, int exp, int credit)
			{
				this.itemId = itemId;
				this.exp = exp;
				this.credit = credit;
			}

			// Token: 0x04009558 RID: 38232
			public readonly int itemId;

			// Token: 0x04009559 RID: 38233
			public readonly int exp;

			// Token: 0x0400955A RID: 38234
			public readonly int credit;
		}

		// Token: 0x02001242 RID: 4674
		public class MaterialUnit
		{
			// Token: 0x0600A290 RID: 41616 RVA: 0x003413AE File Offset: 0x0033F5AE
			public MaterialUnit(NKM_UNIT_GRADE grade, int levelUpSuccessRatePercent, int transportSuccessRatePercent)
			{
				this.m_NKM_UNIT_GRADE = grade;
				this.levelUpSuccessRatePercent = levelUpSuccessRatePercent;
				this.transportSuccessRatePercent = transportSuccessRatePercent;
			}

			// Token: 0x0400955B RID: 38235
			public NKM_UNIT_GRADE m_NKM_UNIT_GRADE;

			// Token: 0x0400955C RID: 38236
			public int levelUpSuccessRatePercent;

			// Token: 0x0400955D RID: 38237
			public int transportSuccessRatePercent;
		}

		// Token: 0x02001243 RID: 4675
		public class HostUnit
		{
			// Token: 0x0600A291 RID: 41617 RVA: 0x003413CB File Offset: 0x0033F5CB
			public HostUnit(NKM_UNIT_GRADE grade, int itemId, int itemCount)
			{
				this.m_NKM_UNIT_GRADE = grade;
				this.itemId = itemId;
				this.itemCount = itemCount;
			}

			// Token: 0x0400955E RID: 38238
			public NKM_UNIT_GRADE m_NKM_UNIT_GRADE;

			// Token: 0x0400955F RID: 38239
			public int itemId;

			// Token: 0x04009560 RID: 38240
			public int itemCount;
		}
	}
}

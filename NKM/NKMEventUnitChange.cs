using System;
using System.Collections.Generic;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004CD RID: 1229
	public class NKMEventUnitChange : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x000B3B41 File Offset: 0x000B1D41
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000B3B49 File Offset: 0x000B1D49
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x000B3B51 File Offset: 0x000B1D51
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Prohibited;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000B3B54 File Offset: 0x000B1D54
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x000B3B5C File Offset: 0x000B1D5C
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000B3B8C File Offset: 0x000B1D8C
		public void DeepCopyFromSource(NKMEventUnitChange source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_UnitStrID = source.m_UnitStrID;
			if (source.m_dicSummonSkin != null)
			{
				this.m_dicSummonSkin = new Dictionary<int, int>(source.m_dicSummonSkin);
				return;
			}
			this.m_dicSummonSkin = null;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000B3BFC File Offset: 0x000B1DFC
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_UnitStrID", ref this.m_UnitStrID);
			if (cNKMLua.OpenTable("m_dicSummonedUnitSkin"))
			{
				this.m_dicSummonSkin = new Dictionary<int, int>();
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					int key;
					int value;
					if (cNKMLua.GetData(1, out key, 0) && cNKMLua.GetData(2, out value, 0))
					{
						this.m_dicSummonSkin.Add(key, value);
					}
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x04002400 RID: 9216
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002401 RID: 9217
		public bool m_bAnimTime = true;

		// Token: 0x04002402 RID: 9218
		public float m_fEventTime;

		// Token: 0x04002403 RID: 9219
		public bool m_bStateEndTime;

		// Token: 0x04002404 RID: 9220
		public string m_UnitStrID = "";

		// Token: 0x04002405 RID: 9221
		public Dictionary<int, int> m_dicSummonSkin;
	}
}

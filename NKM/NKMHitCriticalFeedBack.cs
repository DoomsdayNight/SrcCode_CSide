using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004AC RID: 1196
	public class NKMHitCriticalFeedBack : IEventConditionOwner
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x000A9C0A File Offset: 0x000A7E0A
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000A9C4C File Offset: 0x000A7E4C
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bStartAnyTime", ref this.m_bStartAnyTime);
			cNKMLua.GetData("m_Count", ref this.m_Count);
			cNKMLua.GetData("m_StateName", ref this.m_StateName);
			cNKMLua.GetData("m_BuffStrID", ref this.m_BuffStrID);
			byte b = 0;
			if (cNKMLua.GetData("m_BuffLevel", ref b))
			{
				this.m_BuffStatLevel = b;
				this.m_BuffTimeLevel = b;
			}
			cNKMLua.GetData("m_BuffStatLevel", ref this.m_BuffStatLevel);
			cNKMLua.GetData("m_BuffTimeLevel", ref this.m_BuffTimeLevel);
			return true;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000A9D06 File Offset: 0x000A7F06
		public void DeepCopyFromSource(NKMHitCriticalFeedBack source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_Count = source.m_Count;
			this.m_StateName = source.m_StateName;
			this.m_BuffStrID = source.m_BuffStrID;
		}

		// Token: 0x0400221C RID: 8732
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400221D RID: 8733
		public bool m_bStartAnyTime;

		// Token: 0x0400221E RID: 8734
		public byte m_Count;

		// Token: 0x0400221F RID: 8735
		public string m_StateName = "";

		// Token: 0x04002220 RID: 8736
		public string m_BuffStrID = "";

		// Token: 0x04002221 RID: 8737
		public byte m_BuffStatLevel = 1;

		// Token: 0x04002222 RID: 8738
		public byte m_BuffTimeLevel = 1;
	}
}

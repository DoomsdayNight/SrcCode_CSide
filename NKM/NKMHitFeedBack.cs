using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004AB RID: 1195
	public class NKMHitFeedBack : IEventConditionOwner
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x000A9AB2 File Offset: 0x000A7CB2
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000A9AF4 File Offset: 0x000A7CF4
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bStartAnyTime", ref this.m_bStartAnyTime);
			cNKMLua.GetData("m_HitCount", ref this.m_HitCount);
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

		// Token: 0x06002143 RID: 8515 RVA: 0x000A9BB0 File Offset: 0x000A7DB0
		public void DeepCopyFromSource(NKMHitFeedBack source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_HitCount = source.m_HitCount;
			this.m_StateName = source.m_StateName;
			this.m_BuffStrID = source.m_BuffStrID;
			this.m_BuffStatLevel = source.m_BuffStatLevel;
			this.m_BuffTimeLevel = source.m_BuffTimeLevel;
		}

		// Token: 0x04002215 RID: 8725
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002216 RID: 8726
		public bool m_bStartAnyTime;

		// Token: 0x04002217 RID: 8727
		public byte m_HitCount;

		// Token: 0x04002218 RID: 8728
		public string m_StateName = "";

		// Token: 0x04002219 RID: 8729
		public string m_BuffStrID = "";

		// Token: 0x0400221A RID: 8730
		public byte m_BuffStatLevel = 1;

		// Token: 0x0400221B RID: 8731
		public byte m_BuffTimeLevel = 1;
	}
}

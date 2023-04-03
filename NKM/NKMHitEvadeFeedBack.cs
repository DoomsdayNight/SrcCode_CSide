using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004AD RID: 1197
	public class NKMHitEvadeFeedBack : IEventConditionOwner
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x000A9D3D File Offset: 0x000A7F3D
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000A9D7C File Offset: 0x000A7F7C
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

		// Token: 0x0600214B RID: 8523 RVA: 0x000A9E36 File Offset: 0x000A8036
		public void DeepCopyFromSource(NKMHitEvadeFeedBack source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_Count = source.m_Count;
			this.m_StateName = source.m_StateName;
			this.m_BuffStrID = source.m_BuffStrID;
		}

		// Token: 0x04002223 RID: 8739
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002224 RID: 8740
		public bool m_bStartAnyTime;

		// Token: 0x04002225 RID: 8741
		public byte m_Count;

		// Token: 0x04002226 RID: 8742
		public string m_StateName = "";

		// Token: 0x04002227 RID: 8743
		public string m_BuffStrID = "";

		// Token: 0x04002228 RID: 8744
		public byte m_BuffStatLevel = 1;

		// Token: 0x04002229 RID: 8745
		public byte m_BuffTimeLevel = 1;
	}
}

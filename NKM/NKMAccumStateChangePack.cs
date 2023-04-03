using System;
using System.Collections.Generic;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004AF RID: 1199
	public class NKMAccumStateChangePack : IEventConditionOwner
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x000A9FFE File Offset: 0x000A81FE
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000AA024 File Offset: 0x000A8224
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_listAccumStateChange"))
			{
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					NKMAccumStateChange nkmaccumStateChange;
					if (this.m_listAccumStateChange.Count < num)
					{
						nkmaccumStateChange = new NKMAccumStateChange();
						this.m_listAccumStateChange.Add(nkmaccumStateChange);
					}
					else
					{
						nkmaccumStateChange = this.m_listAccumStateChange[num - 1];
					}
					nkmaccumStateChange.LoadFromLUA(cNKMLua);
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000AA0BC File Offset: 0x000A82BC
		public void DeepCopyFromSource(NKMAccumStateChangePack source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_listAccumStateChange.Clear();
			for (int i = 0; i < source.m_listAccumStateChange.Count; i++)
			{
				NKMAccumStateChange nkmaccumStateChange = new NKMAccumStateChange();
				nkmaccumStateChange.DeepCopyFromSource(source.m_listAccumStateChange[i]);
				this.m_listAccumStateChange.Add(nkmaccumStateChange);
			}
		}

		// Token: 0x04002231 RID: 8753
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002232 RID: 8754
		public List<NKMAccumStateChange> m_listAccumStateChange = new List<NKMAccumStateChange>();
	}
}

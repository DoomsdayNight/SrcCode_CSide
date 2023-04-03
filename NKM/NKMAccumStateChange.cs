using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x020004AE RID: 1198
	public class NKMAccumStateChange
	{
		// Token: 0x0600214D RID: 8525 RVA: 0x000A9EAC File Offset: 0x000A80AC
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			this.m_listAccumStateName.Clear();
			if (cNKMLua.OpenTable("m_listAccumStateName"))
			{
				int num = 1;
				string item = "";
				while (cNKMLua.GetData(num, ref item))
				{
					this.m_listAccumStateName.Add(item);
					num++;
				}
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_AccumCount", ref this.m_AccumCount);
			cNKMLua.GetData("m_fAccumCountCoolTime", ref this.m_fAccumCountCoolTime);
			cNKMLua.GetData("m_fAccumMainCoolTime", ref this.m_fAccumMainCoolTime);
			cNKMLua.GetData("m_TargetStateName", ref this.m_TargetStateName);
			cNKMLua.GetData("m_AirTargetStateName", ref this.m_AirTargetStateName);
			this.m_fRange.LoadFromLua(cNKMLua, "m_fRange");
			return true;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000A9F6C File Offset: 0x000A816C
		public void DeepCopyFromSource(NKMAccumStateChange source)
		{
			this.m_listAccumStateName.Clear();
			for (int i = 0; i < source.m_listAccumStateName.Count; i++)
			{
				this.m_listAccumStateName.Add(source.m_listAccumStateName[i]);
			}
			this.m_AccumCount = source.m_AccumCount;
			this.m_fAccumCountCoolTime = source.m_fAccumCountCoolTime;
			this.m_fAccumMainCoolTime = source.m_fAccumMainCoolTime;
			this.m_TargetStateName = source.m_TargetStateName;
			this.m_AirTargetStateName = source.m_AirTargetStateName;
			this.m_fRange.DeepCopyFromSource(source.m_fRange);
		}

		// Token: 0x0400222A RID: 8746
		public List<string> m_listAccumStateName = new List<string>();

		// Token: 0x0400222B RID: 8747
		public byte m_AccumCount;

		// Token: 0x0400222C RID: 8748
		public float m_fAccumCountCoolTime;

		// Token: 0x0400222D RID: 8749
		public float m_fAccumMainCoolTime;

		// Token: 0x0400222E RID: 8750
		public string m_TargetStateName = "";

		// Token: 0x0400222F RID: 8751
		public string m_AirTargetStateName = "";

		// Token: 0x04002230 RID: 8752
		public NKMMinMaxFloat m_fRange = new NKMMinMaxFloat(-1f, -1f);
	}
}

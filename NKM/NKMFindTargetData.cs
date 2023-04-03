using System;
using System.Collections.Generic;
using NKM.Templet;

namespace NKM
{
	// Token: 0x0200048D RID: 1165
	public class NKMFindTargetData
	{
		// Token: 0x06001F7F RID: 8063 RVA: 0x000954A0 File Offset: 0x000936A0
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData<NKM_FIND_TARGET_TYPE>("m_FindTargetType", ref this.m_FindTargetType);
			cNKMLua.GetData("m_fFindTargetTime", ref this.m_fFindTargetTime);
			cNKMLua.GetData("m_bTargetNoChange", ref this.m_bTargetNoChange);
			cNKMLua.GetData("m_fSeeRange", ref this.m_fSeeRange);
			cNKMLua.GetData("m_bNoBackTarget", ref this.m_bNoBackTarget);
			cNKMLua.GetData("m_bNoFrontTarget", ref this.m_bNoFrontTarget);
			cNKMLua.GetData("m_bCanTargetBoss", ref this.m_bCanTargetBoss);
			cNKMLua.GetDataListEnum<NKM_UNIT_ROLE_TYPE>("m_hsFindTargetRolePriority", this.m_hsFindTargetRolePriority, true);
			cNKMLua.GetData("m_bPriorityOnly", ref this.m_bPriorityOnly);
			return true;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00095553 File Offset: 0x00093753
		public static bool LoadFromLUA(NKMLua cNKMLua, string tableName, out NKMFindTargetData data)
		{
			if (cNKMLua.OpenTable(tableName))
			{
				data = new NKMFindTargetData();
				bool result = data.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
				return result;
			}
			data = null;
			return false;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00095579 File Offset: 0x00093779
		public static void DeepCopyFrom(NKMFindTargetData source, out NKMFindTargetData target)
		{
			if (source == null)
			{
				target = null;
				return;
			}
			target = new NKMFindTargetData();
			target.DeepCopyFrom(source);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00095594 File Offset: 0x00093794
		public void DeepCopyFrom(NKMFindTargetData source)
		{
			this.m_FindTargetType = source.m_FindTargetType;
			this.m_fFindTargetTime = source.m_fFindTargetTime;
			this.m_bTargetNoChange = source.m_bTargetNoChange;
			this.m_fSeeRange = source.m_fSeeRange;
			this.m_bNoBackTarget = source.m_bNoBackTarget;
			this.m_bNoFrontTarget = source.m_bNoFrontTarget;
			this.m_bCanTargetBoss = source.m_bCanTargetBoss;
			this.m_hsFindTargetRolePriority.Clear();
			this.m_hsFindTargetRolePriority.UnionWith(source.m_hsFindTargetRolePriority);
			this.m_bPriorityOnly = source.m_bPriorityOnly;
		}

		// Token: 0x040020A8 RID: 8360
		public NKM_FIND_TARGET_TYPE m_FindTargetType = NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY;

		// Token: 0x040020A9 RID: 8361
		public float m_fFindTargetTime = 0.1f;

		// Token: 0x040020AA RID: 8362
		public bool m_bTargetNoChange;

		// Token: 0x040020AB RID: 8363
		public float m_fSeeRange;

		// Token: 0x040020AC RID: 8364
		public bool m_bNoBackTarget;

		// Token: 0x040020AD RID: 8365
		public bool m_bNoFrontTarget;

		// Token: 0x040020AE RID: 8366
		public bool m_bCanTargetBoss = true;

		// Token: 0x040020AF RID: 8367
		public HashSet<NKM_UNIT_ROLE_TYPE> m_hsFindTargetRolePriority = new HashSet<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x040020B0 RID: 8368
		public bool m_bPriorityOnly;
	}
}

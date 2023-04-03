using System;

namespace NKM
{
	// Token: 0x020004A1 RID: 1185
	public static class ContentsEnumExt
	{
		// Token: 0x06002110 RID: 8464 RVA: 0x000A84B6 File Offset: 0x000A66B6
		public static bool IsAteam(this NKM_TEAM_TYPE teamType)
		{
			return teamType == NKM_TEAM_TYPE.NTT_A1 || teamType == NKM_TEAM_TYPE.NTT_A2;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000A84C2 File Offset: 0x000A66C2
		public static bool IsStatHoldType(this NKM_SKILL_TYPE skillType)
		{
			return skillType == NKM_SKILL_TYPE.NST_PASSIVE || skillType == NKM_SKILL_TYPE.NST_LEADER;
		}
	}
}

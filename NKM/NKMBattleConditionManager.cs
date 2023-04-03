using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003A7 RID: 935
	public sealed class NKMBattleConditionManager
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x000631B1 File Offset: 0x000613B1
		public static Dictionary<int, NKMBattleConditionTemplet> Dic
		{
			get
			{
				return NKMBattleConditionManager.m_dicBattleConditionTemplet;
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000631B8 File Offset: 0x000613B8
		public static bool LoadFromLua()
		{
			NKMBattleConditionManager.m_dicBattleConditionTemplet = NKMTempletLoader.LoadDictionary<NKMBattleConditionTemplet>("AB_SCRIPT", "LUA_BATTLE_CONDITION_TEMPLET", "m_dicNKMBcondTemplet", new Func<NKMLua, NKMBattleConditionTemplet>(NKMBattleConditionTemplet.LoadFromLUA));
			bool flag = NKMBattleConditionManager.m_dicBattleConditionTemplet != null;
			if (flag)
			{
				NKMBattleConditionManager.m_dicBattleConditionTempletByStrID = NKMBattleConditionManager.m_dicBattleConditionTemplet.ToDictionary((KeyValuePair<int, NKMBattleConditionTemplet> t) => t.Value.BattleCondStrID, (KeyValuePair<int, NKMBattleConditionTemplet> t) => t.Value);
			}
			return flag;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00063244 File Offset: 0x00061444
		public static NKMBattleConditionTemplet GetTempletByID(int bCondID)
		{
			NKMBattleConditionTemplet result = null;
			if (!NKMBattleConditionManager.m_dicBattleConditionTemplet.TryGetValue(bCondID, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00063268 File Offset: 0x00061468
		public static NKMBattleConditionTemplet GetTempletByStrID(string bCondStrID)
		{
			NKMBattleConditionTemplet result;
			NKMBattleConditionManager.m_dicBattleConditionTempletByStrID.TryGetValue(bCondStrID, out result);
			return result;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00063284 File Offset: 0x00061484
		public static int GetBattleConditionIDByStrID(string id)
		{
			NKMBattleConditionTemplet nkmbattleConditionTemplet;
			if (!NKMBattleConditionManager.m_dicBattleConditionTempletByStrID.TryGetValue(id, out nkmbattleConditionTemplet))
			{
				return 0;
			}
			return nkmbattleConditionTemplet.BattleCondID;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000632A8 File Offset: 0x000614A8
		public static List<string> GetBattleConditionStrIDList()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, NKMBattleConditionTemplet> keyValuePair in NKMBattleConditionManager.m_dicBattleConditionTempletByStrID)
			{
				NKMBattleConditionTemplet value = keyValuePair.Value;
				list.Add(value.BattleCondStrID);
			}
			return list;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000632F0 File Offset: 0x000614F0
		public static string GetBattleConditionStrIdById(int id)
		{
			NKMBattleConditionTemplet nkmbattleConditionTemplet;
			if (!NKMBattleConditionManager.m_dicBattleConditionTemplet.TryGetValue(id, out nkmbattleConditionTemplet))
			{
				return null;
			}
			return nkmbattleConditionTemplet.BattleCondStrID;
		}

		// Token: 0x04001059 RID: 4185
		private static Dictionary<int, NKMBattleConditionTemplet> m_dicBattleConditionTemplet;

		// Token: 0x0400105A RID: 4186
		private static Dictionary<string, NKMBattleConditionTemplet> m_dicBattleConditionTempletByStrID;
	}
}

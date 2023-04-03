using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006BF RID: 1727
	public class NKCRandomBoxManager
	{
		// Token: 0x06003AE6 RID: 15078 RVA: 0x0012EC61 File Offset: 0x0012CE61
		public static bool LoadFromLUA()
		{
			NKCRandomBoxManager._dic = NKMTempletLoader<NKMRandomBoxItemTemplet>.LoadGroup("ab_script", "LUA_RANDOM_ITEM_BOX", "RANDOM_ITEM_BOX", new Func<NKMLua, NKMRandomBoxItemTemplet>(NKMRandomBoxItemTemplet.LoadFromLUA));
			return NKCRandomBoxManager._dic != null;
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x0012EC90 File Offset: 0x0012CE90
		public static List<NKMRandomBoxItemTemplet> GetRandomBoxItemTempletList(int groupID)
		{
			List<NKMRandomBoxItemTemplet> list;
			if (!NKCRandomBoxManager._dic.TryGetValue(groupID, out list))
			{
				return null;
			}
			List<NKMRandomBoxItemTemplet> list2 = null;
			foreach (NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet in list)
			{
				if (nkmrandomBoxItemTemplet.EnableByTag)
				{
					if (nkmrandomBoxItemTemplet.m_RewardGroupID == 33028 || nkmrandomBoxItemTemplet.m_RewardGroupID == 33027)
					{
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmrandomBoxItemTemplet.m_RewardID);
						if (unitTempletBase != null && (unitTempletBase.PickupEnableByTag || unitTempletBase.ContractEnableByTag))
						{
							if (list2 == null)
							{
								list2 = new List<NKMRandomBoxItemTemplet>();
							}
							list2.Add(nkmrandomBoxItemTemplet);
						}
					}
					else if (NKMRewardTemplet.IsOpenedReward(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, true))
					{
						if (list2 == null)
						{
							list2 = new List<NKMRandomBoxItemTemplet>();
						}
						list2.Add(nkmrandomBoxItemTemplet);
					}
				}
			}
			return list2;
		}

		// Token: 0x04003531 RID: 13617
		public static Dictionary<int, List<NKMRandomBoxItemTemplet>> _dic;
	}
}

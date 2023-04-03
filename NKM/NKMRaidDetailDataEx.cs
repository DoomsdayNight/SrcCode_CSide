using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using NKC;

namespace NKM
{
	// Token: 0x02000369 RID: 873
	public static class NKMRaidDetailDataEx
	{
		// Token: 0x06001517 RID: 5399 RVA: 0x0004F632 File Offset: 0x0004D832
		public static void SortJoinDataByDamage(this NKMRaidDetailData data)
		{
			data.raidJoinDataList.Sort(new NKMRaidDetailDataEx.Comp());
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0004F644 File Offset: 0x0004D844
		public static void DeepCopyFromSource(this NKMRaidDetailData data, NKMMyRaidData source)
		{
			data.raidUID = source.raidUID;
			data.stageID = source.stageID;
			data.cityID = source.cityID;
			data.curHP = source.curHP;
			data.maxHP = source.maxHP;
			data.isCoop = source.isCoop;
			data.isNew = source.isNew;
			data.expireDate = source.expireDate;
			data.seasonID = source.seasonID;
			if (NKCScenManager.CurrentUserData() != null)
			{
				data.userUID = NKCScenManager.CurrentUserData().m_UserUID;
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0004F6D4 File Offset: 0x0004D8D4
		public static void DeepCopyFromSource(this NKMRaidDetailData data, NKMRaidResultData source)
		{
			data.raidUID = source.raidUID;
			data.stageID = source.stageID;
			data.cityID = source.cityID;
			data.curHP = source.curHP;
			data.maxHP = source.maxHP;
			data.isCoop = source.isCoop;
			data.isNew = false;
			data.expireDate = source.expireDate;
			data.userUID = source.userUID;
			data.seasonID = source.seasonID;
			data.friendCode = source.friendCode;
			data.raidJoinDataList = source.raidJoinDataList;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0004F76C File Offset: 0x0004D96C
		public static NKMRaidJoinData FindJoinData(this NKMRaidDetailData data, long userUID)
		{
			if (data.raidJoinDataList == null)
			{
				return null;
			}
			for (int i = 0; i < data.raidJoinDataList.Count; i++)
			{
				NKMRaidJoinData nkmraidJoinData = data.raidJoinDataList[i];
				if (nkmraidJoinData != null && nkmraidJoinData.userUID == userUID)
				{
					return nkmraidJoinData;
				}
			}
			return null;
		}

		// Token: 0x02001182 RID: 4482
		public class Comp : IComparer<NKMRaidJoinData>
		{
			// Token: 0x06009FE6 RID: 40934 RVA: 0x0033D648 File Offset: 0x0033B848
			public int Compare(NKMRaidJoinData x, NKMRaidJoinData y)
			{
				if (y.damage > x.damage)
				{
					return 1;
				}
				if (y.damage < x.damage)
				{
					return -1;
				}
				if (y.userUID <= x.userUID)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}

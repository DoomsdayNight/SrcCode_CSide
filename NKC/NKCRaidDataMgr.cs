using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using Cs.Protocol;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006BD RID: 1725
	public class NKCRaidDataMgr
	{
		// Token: 0x06003AD9 RID: 15065 RVA: 0x0012E790 File Offset: 0x0012C990
		public void SetData(NKMRaidDetailData newRaidData)
		{
			if (this.m_RaidDataList == null)
			{
				this.m_RaidDataList = new List<NKMRaidDetailData>();
			}
			bool flag = false;
			for (int i = 0; i < this.m_RaidDataList.Count; i++)
			{
				NKMRaidDetailData nkmraidDetailData = this.m_RaidDataList[i];
				if (nkmraidDetailData.raidUID == newRaidData.raidUID)
				{
					nkmraidDetailData.DeepCopyFrom(newRaidData);
					flag = true;
				}
			}
			if (!flag)
			{
				this.m_RaidDataList.Add(newRaidData);
			}
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x0012E7FC File Offset: 0x0012C9FC
		public void SetDataList(List<NKMMyRaidData> newRaidDataList)
		{
			if (newRaidDataList != null)
			{
				if (this.m_RaidDataList == null)
				{
					this.m_RaidDataList = new List<NKMRaidDetailData>();
				}
				int i;
				Predicate<NKMRaidDetailData> <>9__0;
				int j;
				for (i = 0; i < newRaidDataList.Count; i = j + 1)
				{
					NKMRaidDetailData nkmraidDetailData = new NKMRaidDetailData();
					nkmraidDetailData.DeepCopyFromSource(newRaidDataList[i]);
					List<NKMRaidDetailData> raidDataList = this.m_RaidDataList;
					Predicate<NKMRaidDetailData> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((NKMRaidDetailData x) => x.raidUID == newRaidDataList[i].raidUID));
					}
					NKMRaidDetailData nkmraidDetailData2 = raidDataList.Find(match);
					if (nkmraidDetailData2 != null)
					{
						nkmraidDetailData.raidJoinDataList = nkmraidDetailData2.raidJoinDataList;
						this.m_RaidDataList.Remove(nkmraidDetailData2);
					}
					this.m_RaidDataList.Add(nkmraidDetailData);
					j = i;
				}
				return;
			}
			if (this.m_RaidDataList != null)
			{
				this.m_RaidDataList.Clear();
				return;
			}
			this.m_RaidDataList = new List<NKMRaidDetailData>();
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x0012E8F8 File Offset: 0x0012CAF8
		public void SetDataList(List<NKMRaidResultData> resultDataList)
		{
			if (resultDataList != null)
			{
				if (this.m_RaidDataList == null)
				{
					this.m_RaidDataList = new List<NKMRaidDetailData>();
				}
				int i;
				Predicate<NKMRaidDetailData> <>9__0;
				int j;
				for (i = 0; i < resultDataList.Count; i = j + 1)
				{
					List<NKMRaidDetailData> raidDataList = this.m_RaidDataList;
					Predicate<NKMRaidDetailData> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((NKMRaidDetailData x) => x.raidUID == resultDataList[i].raidUID));
					}
					NKMRaidDetailData nkmraidDetailData = raidDataList.Find(match);
					if (nkmraidDetailData == null)
					{
						nkmraidDetailData = new NKMRaidDetailData();
						nkmraidDetailData.DeepCopyFromSource(resultDataList[i]);
						this.m_RaidDataList.Add(nkmraidDetailData);
					}
					else
					{
						nkmraidDetailData.DeepCopyFromSource(resultDataList[i]);
					}
					j = i;
				}
			}
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x0012E9D2 File Offset: 0x0012CBD2
		public List<NKMRaidDetailData> GetDataList()
		{
			return this.m_RaidDataList;
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x0012E9DC File Offset: 0x0012CBDC
		public NKMRaidDetailData Find(int cityID, int stageID)
		{
			if (this.m_RaidDataList == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_RaidDataList.Count; i++)
			{
				NKMRaidDetailData nkmraidDetailData = this.m_RaidDataList[i];
				if (nkmraidDetailData != null && nkmraidDetailData.cityID == cityID && nkmraidDetailData.stageID == stageID)
				{
					return nkmraidDetailData;
				}
			}
			return null;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x0012EA30 File Offset: 0x0012CC30
		public NKMRaidDetailData Find(long raidUID)
		{
			if (this.m_RaidDataList == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_RaidDataList.Count; i++)
			{
				NKMRaidDetailData nkmraidDetailData = this.m_RaidDataList[i];
				if (nkmraidDetailData != null && nkmraidDetailData.raidUID == raidUID)
				{
					return nkmraidDetailData;
				}
			}
			return null;
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x0012EA7C File Offset: 0x0012CC7C
		public void Remove(long raidUID)
		{
			if (this.m_RaidDataList == null)
			{
				return;
			}
			for (int i = 0; i < this.m_RaidDataList.Count; i++)
			{
				NKMRaidDetailData nkmraidDetailData = this.m_RaidDataList[i];
				if (nkmraidDetailData != null && nkmraidDetailData.raidUID == raidUID)
				{
					this.m_RaidDataList.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x0012EAD0 File Offset: 0x0012CCD0
		public bool CheckCompletableRaid(long raidUID)
		{
			NKMRaidDetailData nkmraidDetailData = this.Find(raidUID);
			if (nkmraidDetailData == null)
			{
				return false;
			}
			if (NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate))
			{
				return true;
			}
			if (nkmraidDetailData.curHP <= 0f)
			{
				return true;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(nkmraidDetailData.stageID);
			if (nkmraidTemplet != null && nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
			{
				NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
				short? num = (nkmraidJoinData != null) ? new short?(nkmraidJoinData.tryCount) : null;
				int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
				int raidTryCount = nkmraidTemplet.RaidTryCount;
				if (num2.GetValueOrDefault() == raidTryCount & num2 != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x0012EB94 File Offset: 0x0012CD94
		public bool CheckRaidCoopOn(long raidUID)
		{
			NKMRaidDetailData nkmraidDetailData = this.Find(raidUID);
			return nkmraidDetailData != null && nkmraidDetailData.isCoop;
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x0012EBB4 File Offset: 0x0012CDB4
		public void SetRaidCoopOn(long raidUID, List<NKMRaidJoinData> lstNKMRaidJoinData)
		{
			NKMRaidDetailData nkmraidDetailData = this.Find(raidUID);
			if (nkmraidDetailData != null)
			{
				nkmraidDetailData.raidJoinDataList = lstNKMRaidJoinData;
				nkmraidDetailData.isCoop = true;
			}
		}

		// Token: 0x0400352F RID: 13615
		private List<NKMRaidDetailData> m_RaidDataList = new List<NKMRaidDetailData>();
	}
}

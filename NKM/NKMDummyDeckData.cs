using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003DC RID: 988
	public sealed class NKMDummyDeckData : ISerializable
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x0006FD98 File Offset: 0x0006DF98
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.LeaderIndex);
			stream.PutOrGet<NKMDummyUnitData>(ref this.Ship);
			stream.PutOrGet<NKMDummyUnitData>(ref this.operatorUnit);
			stream.PutOrGet<NKMDummyUnitData>(ref this.List);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0006FDCA File Offset: 0x0006DFCA
		public int GetShipUnitId()
		{
			return this.Ship.UnitId;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0006FDD7 File Offset: 0x0006DFD7
		public NKMDummyUnitData GetLeader()
		{
			if (this.LeaderIndex < 0 || (int)this.LeaderIndex >= this.List.Length)
			{
				return null;
			}
			return this.List[(int)this.LeaderIndex];
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0006FE01 File Offset: 0x0006E001
		public NKMUnitData CreateShip(long shipUid)
		{
			if (this.Ship == null)
			{
				return null;
			}
			if (NKMUnitManager.GetUnitTempletBase(this.Ship.UnitId) == null)
			{
				return null;
			}
			return this.Ship.ToUnitData(shipUid);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0006FE2D File Offset: 0x0006E02D
		public NKMUnitTempletBase GetShipTemplet()
		{
			return NKMUnitManager.GetUnitTempletBase(this.GetShipUnitId());
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0006FE3C File Offset: 0x0006E03C
		public bool Equal(NKMDummyDeckData dummyDeckData)
		{
			if (this.LeaderIndex != dummyDeckData.LeaderIndex)
			{
				return false;
			}
			if (this.Ship.UnitId != dummyDeckData.Ship.UnitId || this.Ship.UnitLevel != dummyDeckData.Ship.UnitLevel)
			{
				return false;
			}
			for (int i = 0; i < 8; i++)
			{
				NKMDummyUnitData nkmdummyUnitData = this.List[i];
				NKMDummyUnitData nkmdummyUnitData2 = dummyDeckData.List[i];
				if (nkmdummyUnitData.UnitId != nkmdummyUnitData2.UnitId || nkmdummyUnitData.UnitLevel != nkmdummyUnitData2.UnitLevel)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0006FEC8 File Offset: 0x0006E0C8
		public int CalculateOperationPower()
		{
			int num = 0;
			int num2 = 0;
			NKMUnitData nkmunitData = this.CreateShip(-1L);
			if (nkmunitData != null)
			{
				num2++;
				num += nkmunitData.CalculateOperationPower(null, 0, null, null);
			}
			for (int i = 0; i < this.List.Length; i++)
			{
				NKMDummyUnitData nkmdummyUnitData = this.List[i];
				if (nkmdummyUnitData != null)
				{
					NKMUnitData nkmunitData2 = nkmdummyUnitData.ToUnitData(-1L);
					num2++;
					num += nkmunitData2.CalculateOperationPower(null, 0, null, null);
				}
			}
			if (num2 == 0)
			{
				return 0;
			}
			return (int)((float)(num / num2) + (3f - this.CalculateSummonCost()) * (float)num / (float)num2 / 10f);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0006FF58 File Offset: 0x0006E158
		public int CalculateOperationPowerForPrivatePvp()
		{
			int num = 0;
			int num2 = 0;
			NKMUnitData nkmunitData = this.CreateShip(-1L);
			if (nkmunitData != null)
			{
				num += nkmunitData.CalculateOperationPower(null, 0, null, null);
			}
			for (int i = 0; i < this.List.Length; i++)
			{
				NKMDummyUnitData nkmdummyUnitData = this.List[i];
				if (nkmdummyUnitData != null)
				{
					NKMUnitData nkmunitData2 = nkmdummyUnitData.ToUnitData(-1L);
					num2++;
					num += nkmunitData2.CalculateOperationPower(null, 0, null, null);
				}
			}
			if (num2 == 0)
			{
				return 0;
			}
			float num3 = this.CalculateSummonCost();
			return (int)(((float)(num / (num2 + 1)) + (3f - num3) * (float)num / (float)(num2 + 1) / 10f) * ((float)num2 / 8f));
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0006FFF8 File Offset: 0x0006E1F8
		public float CalculateSummonCost()
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.List.Length; i++)
			{
				NKMDummyUnitData nkmdummyUnitData = this.List[i];
				if (nkmdummyUnitData != null)
				{
					NKMUnitData nkmunitData = new NKMUnitData();
					nkmunitData.FillDataFromDummy(nkmdummyUnitData);
					num2++;
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmunitData.m_UnitID);
					if (unitStatTemplet != null)
					{
						if ((int)this.LeaderIndex == i)
						{
							num += unitStatTemplet.GetRespawnCost(true, null, null);
						}
						else
						{
							num += unitStatTemplet.GetRespawnCost(false, null, null);
						}
					}
				}
			}
			if (num2 == 0)
			{
				return 0f;
			}
			return (float)num / (float)num2;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0007007C File Offset: 0x0006E27C
		public float CalculateAvgSummonCost(Dictionary<int, NKMBanData> dicNKMBanData = null, Dictionary<int, NKMUnitUpData> dicNKMUpData = null)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.List.Length; i++)
			{
				NKMDummyUnitData nkmdummyUnitData = this.List[i];
				if (nkmdummyUnitData != null)
				{
					NKMUnitData nkmunitData = new NKMUnitData();
					nkmunitData.FillDataFromDummy(nkmdummyUnitData);
					num2++;
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmunitData.m_UnitID);
					if (unitStatTemplet != null)
					{
						if ((int)this.LeaderIndex == i)
						{
							num += unitStatTemplet.GetRespawnCost(true, dicNKMBanData, dicNKMUpData);
						}
						else
						{
							num += unitStatTemplet.GetRespawnCost(false, dicNKMBanData, dicNKMUpData);
						}
					}
				}
			}
			if (num2 == 0)
			{
				return 0f;
			}
			return (float)num / (float)num2;
		}

		// Token: 0x04001311 RID: 4881
		public sbyte LeaderIndex;

		// Token: 0x04001312 RID: 4882
		public NKMDummyUnitData Ship = new NKMDummyUnitData();

		// Token: 0x04001313 RID: 4883
		public NKMDummyUnitData operatorUnit = new NKMDummyUnitData();

		// Token: 0x04001314 RID: 4884
		public NKMDummyUnitData[] List = new NKMDummyUnitData[8];
	}
}

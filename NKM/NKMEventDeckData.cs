using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003E0 RID: 992
	public class NKMEventDeckData : ISerializable
	{
		// Token: 0x06001A42 RID: 6722 RVA: 0x000711A0 File Offset: 0x0006F3A0
		public long GetUnitUID(int index)
		{
			long result;
			if (this.m_dicUnit.TryGetValue(index, out result))
			{
				return result;
			}
			return 0L;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000711C1 File Offset: 0x0006F3C1
		public NKMEventDeckData()
		{
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000711D4 File Offset: 0x0006F3D4
		public void DeepCopy(NKMEventDeckData src)
		{
			this.m_ShipUID = src.m_ShipUID;
			this.m_OperatorUID = src.m_OperatorUID;
			this.m_LeaderIndex = src.m_LeaderIndex;
			this.m_dicUnit.Clear();
			foreach (KeyValuePair<int, long> keyValuePair in src.m_dicUnit)
			{
				this.m_dicUnit.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00071268 File Offset: 0x0006F468
		public long CompareTo(NKMEventDeckData other)
		{
			long num = 0L;
			num = (long)this.m_ShipUID.CompareTo(other.m_ShipUID);
			num = (long)this.m_OperatorUID.CompareTo(other.m_OperatorUID);
			num = (long)this.m_LeaderIndex.CompareTo(other.m_LeaderIndex);
			if (num != 0L)
			{
				return -1L;
			}
			foreach (int key in other.m_dicUnit.Keys)
			{
				if (!this.m_dicUnit.ContainsKey(key))
				{
					num = 1L;
					break;
				}
				if (this.m_dicUnit[key] != other.m_dicUnit[key])
				{
					num = 1L;
					break;
				}
			}
			return num;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00071330 File Offset: 0x0006F530
		public NKMEventDeckData(Dictionary<int, long> dicUnit, long shipUID, long operatorUID, int leaderIndex)
		{
			this.m_dicUnit = dicUnit;
			this.m_ShipUID = shipUID;
			this.m_OperatorUID = operatorUID;
			this.m_LeaderIndex = leaderIndex;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00071360 File Offset: 0x0006F560
		public IEnumerable<NKMUnitData> GetUnits(NKMArmyData armyData)
		{
			foreach (long num in this.m_dicUnit.Values)
			{
				if (num != 0L)
				{
					yield return armyData.GetUnitFromUID(num);
				}
			}
			Dictionary<int, long>.ValueCollection.Enumerator enumerator = default(Dictionary<int, long>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00071378 File Offset: 0x0006F578
		public long GetFirstUnitUID()
		{
			using (Dictionary<int, long>.Enumerator enumerator = this.m_dicUnit.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<int, long> keyValuePair = enumerator.Current;
					return keyValuePair.Value;
				}
			}
			return 0L;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000713D4 File Offset: 0x0006F5D4
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_ShipUID);
			stream.PutOrGet(ref this.m_dicUnit);
			stream.PutOrGet(ref this.m_OperatorUID);
			stream.PutOrGet(ref this.m_LeaderIndex);
		}

		// Token: 0x04001324 RID: 4900
		public Dictionary<int, long> m_dicUnit = new Dictionary<int, long>();

		// Token: 0x04001325 RID: 4901
		public long m_ShipUID;

		// Token: 0x04001326 RID: 4902
		public long m_OperatorUID;

		// Token: 0x04001327 RID: 4903
		public int m_LeaderIndex;

		// Token: 0x04001328 RID: 4904
		public int m_OperationPower;
	}
}

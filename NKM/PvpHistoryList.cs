using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000365 RID: 869
	public class PvpHistoryList : ISerializable
	{
		// Token: 0x060014CB RID: 5323 RVA: 0x0004E34F File Offset: 0x0004C54F
		public PvpSingleHistory GetData(int index)
		{
			if (this.list == null)
			{
				return null;
			}
			if (index < 0 || index >= this.list.Count)
			{
				return null;
			}
			return this.list[index];
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0004E37C File Offset: 0x0004C57C
		public PvpSingleHistory GetDataByGameUID(long gameUID)
		{
			if (this.list == null)
			{
				return null;
			}
			return this.list.Find((PvpSingleHistory x) => x.gameUid == gameUID);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0004E3B7 File Offset: 0x0004C5B7
		public int GetCount()
		{
			if (this.list == null)
			{
				return 0;
			}
			return this.list.Count;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0004E3CE File Offset: 0x0004C5CE
		public void Sort()
		{
			this.list.Sort((PvpSingleHistory a, PvpSingleHistory b) => b.RegdateTick.CompareTo(a.RegdateTick));
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0004E3FC File Offset: 0x0004C5FC
		public void Add(List<PvpSingleHistory> lstData)
		{
			for (int i = 0; i < lstData.Count; i++)
			{
				this.Add(lstData[i]);
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0004E428 File Offset: 0x0004C628
		public void Add(PvpSingleHistory data)
		{
			if (data == null)
			{
				return;
			}
			this.list.Add(data);
			this.Sort();
			if (this.list.Count > NKMPvpCommonConst.Instance.MaxHistoryCount)
			{
				this.list.RemoveRange(NKMPvpCommonConst.Instance.MaxHistoryCount, this.list.Count - NKMPvpCommonConst.Instance.MaxHistoryCount);
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0004E48D File Offset: 0x0004C68D
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<PvpSingleHistory>(ref this.list);
		}

		// Token: 0x04000E6B RID: 3691
		private List<PvpSingleHistory> list = new List<PvpSingleHistory>();
	}
}

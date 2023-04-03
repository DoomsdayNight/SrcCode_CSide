using System;
using System.Collections.Generic;
using ClientPacket.Event;
using Cs.Logging;
using NKM.Event;

namespace NKM
{
	// Token: 0x02000527 RID: 1319
	public class EventBingo
	{
		// Token: 0x0600259F RID: 9631 RVA: 0x000C1DA4 File Offset: 0x000BFFA4
		public EventBingo(int eventID, BingoInfo bingoInfo)
		{
			this.m_eventID = eventID;
			this.m_bingoInfo = bingoInfo;
			this.m_bingoTemplet = NKMEventManager.GetBingoTemplet(eventID);
			this.m_size = this.m_bingoTemplet.m_BingoSize;
			int num = this.m_size * 2 + 2;
			this.m_bingoLines = new EventBingo.BingoLine[num];
			for (int i = 0; i < num; i++)
			{
				this.m_bingoLines[i] = new EventBingo.BingoLine();
			}
			for (int j = 0; j < bingoInfo.markTileIndexList.Count; j++)
			{
				int num2 = bingoInfo.markTileIndexList[j];
				foreach (int num3 in this.GetLineIndex(num2))
				{
					this.m_bingoLines[num3].Mark(num2);
				}
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000C1E88 File Offset: 0x000C0088
		public void MarkToLine(int tileIndex)
		{
			if (!this.m_bingoInfo.markTileIndexList.Exists((int e) => e.Equals(tileIndex)))
			{
				foreach (int num in this.GetLineIndex(tileIndex))
				{
					this.m_bingoLines[num].Mark(tileIndex);
				}
				this.m_bingoInfo.markTileIndexList.Add(tileIndex);
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000C1F30 File Offset: 0x000C0130
		public bool IsRemainNum()
		{
			int num = 0;
			for (int i = 0; i < this.m_bingoTemplet.MissionTiles.Count; i++)
			{
				if (this.m_bingoInfo.markTileIndexList.Contains(this.m_bingoTemplet.MissionTiles[i]))
				{
					num++;
				}
			}
			int num2 = this.m_bingoInfo.markTileIndexList.Count - num;
			int tileRange = this.m_bingoTemplet.TileRange;
			return num2 < tileRange;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000C1FA2 File Offset: 0x000C01A2
		public void SetMileage(int mileage)
		{
			if (this.m_bingoInfo != null)
			{
				this.m_bingoInfo.mileage = mileage;
			}
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000C1FB8 File Offset: 0x000C01B8
		public List<int> GetBingoLine()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_bingoLines.Length; i++)
			{
				if (this.m_bingoLines[i].MarkedTileCount() == this.m_size)
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000C1FFB File Offset: 0x000C01FB
		public void RecvReward(int index)
		{
			if (!this.m_bingoInfo.rewardList.Contains(index))
			{
				this.m_bingoInfo.rewardList.Add(index);
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000C2024 File Offset: 0x000C0224
		private List<int> GetLineIndex(int tileIndex)
		{
			List<int> list = new List<int>();
			list.Add(tileIndex / this.m_size);
			list.Add(tileIndex % this.m_size + this.m_size);
			if (this.RightDiagonal(tileIndex))
			{
				list.Add(this.m_size * 2);
			}
			if (this.LeftDiagonal(tileIndex))
			{
				list.Add(this.m_size * 2 + 1);
			}
			return list;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000C208C File Offset: 0x000C028C
		private bool RightDiagonal(int index)
		{
			int num = index / this.m_size;
			return (this.m_size + 1) * num == index;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000C20B0 File Offset: 0x000C02B0
		private bool LeftDiagonal(int index)
		{
			int num = index / this.m_size + 1;
			return (this.m_size - 1) * num == index;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000C20D5 File Offset: 0x000C02D5
		public int GetTileValue(int tileIndex)
		{
			if (tileIndex < this.m_bingoInfo.tileValueList.Count)
			{
				return this.m_bingoInfo.tileValueList[tileIndex];
			}
			return 0;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000C2100 File Offset: 0x000C0300
		public bool Completed()
		{
			if (this.IsRemainNum())
			{
				return false;
			}
			List<NKMEventBingoRewardTemplet> bingoRewardTempletList = NKMEventManager.GetBingoRewardTempletList(this.m_bingoTemplet.m_EventID);
			if (bingoRewardTempletList != null)
			{
				foreach (NKMEventBingoRewardTemplet nkmeventBingoRewardTemplet in bingoRewardTempletList)
				{
					if (nkmeventBingoRewardTemplet != null && NKMEventManager.IsReceiveableBingoReward(this.m_bingoTemplet.m_EventID, nkmeventBingoRewardTemplet.ZeroBaseTileIndex))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x04002721 RID: 10017
		public int m_eventID;

		// Token: 0x04002722 RID: 10018
		public int m_size;

		// Token: 0x04002723 RID: 10019
		public EventBingo.BingoLine[] m_bingoLines;

		// Token: 0x04002724 RID: 10020
		public NKMEventBingoTemplet m_bingoTemplet;

		// Token: 0x04002725 RID: 10021
		public BingoInfo m_bingoInfo;

		// Token: 0x02001249 RID: 4681
		public class BingoLine
		{
			// Token: 0x0600A297 RID: 41623 RVA: 0x0034144C File Offset: 0x0033F64C
			public NKM_ERROR_CODE Mark(int index)
			{
				if (this.list.Exists((int e) => e.Equals(index)))
				{
					Log.Error("Fatal. Bingo tile Already marked.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMEventManagerEx.cs", 442);
					return NKM_ERROR_CODE.NEC_FAIL_EVENT_BINGO_ALREADY_MARKED;
				}
				this.list.Add(index);
				return NKM_ERROR_CODE.NEC_OK;
			}

			// Token: 0x0600A298 RID: 41624 RVA: 0x003414AB File Offset: 0x0033F6AB
			public int MarkedTileCount()
			{
				return this.list.Count;
			}

			// Token: 0x04009571 RID: 38257
			private List<int> list = new List<int>();
		}
	}
}

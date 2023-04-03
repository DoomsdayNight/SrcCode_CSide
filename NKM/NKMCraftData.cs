﻿using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003C3 RID: 963
	public class NKMCraftData : ISerializable
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00067FD6 File Offset: 0x000661D6
		public Dictionary<byte, NKMCraftSlotData> SlotList
		{
			get
			{
				return this.m_dicSlot;
			}
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00067FDE File Offset: 0x000661DE
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMMoldItemData>(ref this.m_dicMoldItem);
			stream.PutOrGet<NKMCraftSlotData>(ref this.m_dicSlot);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00068016 File Offset: 0x00066216
		public Dictionary<int, NKMMoldItemData> GetDicMoldItemData()
		{
			return this.m_dicMoldItem;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00068020 File Offset: 0x00066220
		public NKMMoldItemData GetMoldItemDataByID(int id)
		{
			NKMMoldItemData result = null;
			if (!this.m_dicMoldItem.TryGetValue(id, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00068044 File Offset: 0x00066244
		public NKMMoldItemData GetMoldItemDataByIndex(int index)
		{
			if (index < 0 || index >= this.GetTotalMoldCount())
			{
				return null;
			}
			int num = 0;
			foreach (KeyValuePair<int, NKMMoldItemData> keyValuePair in this.m_dicMoldItem)
			{
				if (num == index)
				{
					return keyValuePair.Value;
				}
				num++;
			}
			return null;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000680B8 File Offset: 0x000662B8
		public void AddMoldItem(List<NKMMoldItemData> moldItemDataList)
		{
			foreach (NKMMoldItemData nkmmoldItemData in moldItemDataList)
			{
				this.AddMoldItem(nkmmoldItemData.m_MoldID, nkmmoldItemData.m_Count);
			}
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00068114 File Offset: 0x00066314
		public void AddMoldItem(NKMMoldItemData moldItemData)
		{
			this.AddMoldItem(moldItemData.m_MoldID, moldItemData.m_Count);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00068128 File Offset: 0x00066328
		public void AddMoldItem(int moldID, long count)
		{
			NKMMoldItemData nkmmoldItemData;
			if (this.m_dicMoldItem.TryGetValue(moldID, out nkmmoldItemData))
			{
				nkmmoldItemData.m_Count = this.m_dicMoldItem[moldID].m_Count + count;
				return;
			}
			this.m_dicMoldItem.Add(moldID, new NKMMoldItemData(moldID, count));
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00068172 File Offset: 0x00066372
		public void DecMoldItem(NKMMoldItemData moldItemData)
		{
			this.DecMoldItem(moldItemData.m_MoldID, moldItemData.m_Count);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00068188 File Offset: 0x00066388
		public void DecMoldItem(int moldID, long count)
		{
			NKMMoldItemData nkmmoldItemData;
			if (this.m_dicMoldItem.TryGetValue(moldID, out nkmmoldItemData))
			{
				nkmmoldItemData.m_Count = Math.Max(0L, this.m_dicMoldItem[moldID].m_Count - count);
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x000681C8 File Offset: 0x000663C8
		public void UpdateMoldItem(List<NKMMoldItemData> moldItemDataList)
		{
			foreach (NKMMoldItemData nkmmoldItemData in moldItemDataList)
			{
				this.UpdateMoldItem(nkmmoldItemData.m_MoldID, nkmmoldItemData.m_Count);
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00068224 File Offset: 0x00066424
		public void UpdateMoldItem(int moldID, long count)
		{
			NKMMoldItemData nkmmoldItemData;
			if (this.m_dicMoldItem.TryGetValue(moldID, out nkmmoldItemData))
			{
				nkmmoldItemData.m_Count = count;
				return;
			}
			this.m_dicMoldItem.Add(moldID, new NKMMoldItemData(moldID, count));
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0006825C File Offset: 0x0006645C
		public long GetMoldCount(int moldID)
		{
			long result = 0L;
			if (this.m_dicMoldItem.ContainsKey(moldID))
			{
				result = this.m_dicMoldItem[moldID].m_Count;
			}
			return result;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0006828D File Offset: 0x0006648D
		public int GetTotalMoldCount()
		{
			return this.m_dicMoldItem.Count;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0006829C File Offset: 0x0006649C
		public void AddSlotData(NKMCraftSlotData slotData)
		{
			if (this.m_dicSlot.ContainsKey(slotData.Index))
			{
				Log.Error(string.Format("CraftSlot AddSlotData Failed! slot already exists! index : {0}", slotData.Index), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCraftManager.cs", 201);
				return;
			}
			this.m_dicSlot.Add(slotData.Index, slotData);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000682F4 File Offset: 0x000664F4
		public void AddSlotData(byte index, int moldID, int count, long completeDate)
		{
			if (this.m_dicSlot.ContainsKey(index))
			{
				Log.Error(string.Format("CraftSlot AddSlotData Failed! slot already exists! index : {0}", index), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCraftManager.cs", 212);
				return;
			}
			NKMCraftSlotData nkmcraftSlotData = new NKMCraftSlotData();
			nkmcraftSlotData.Index = index;
			nkmcraftSlotData.MoldID = moldID;
			nkmcraftSlotData.Count = count;
			nkmcraftSlotData.CompleteDate = completeDate;
			this.m_dicSlot.Add(nkmcraftSlotData.Index, nkmcraftSlotData);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00068364 File Offset: 0x00066564
		public NKM_ERROR_CODE UpdateSlotData(NKMCraftSlotData slotData)
		{
			return this.UpdateSlotData(slotData.Index, slotData.MoldID, slotData.Count, slotData.CompleteDate);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00068384 File Offset: 0x00066584
		public NKM_ERROR_CODE UpdateSlotData(byte index, int moldID, int count, long completeDate)
		{
			if (index <= 0 || NKMCraftData.MAX_CRAFT_SLOT_DATA < (int)index)
			{
				return NKM_ERROR_CODE.NEC_FAIL_CRAFT_INVALID_SLOT_INDEX;
			}
			NKMCraftSlotData nkmcraftSlotData;
			if (!this.m_dicSlot.TryGetValue(index, out nkmcraftSlotData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_CRAFT_INVALID_SLOT_INDEX;
			}
			nkmcraftSlotData.Index = index;
			nkmcraftSlotData.MoldID = moldID;
			nkmcraftSlotData.Count = count;
			nkmcraftSlotData.CompleteDate = completeDate;
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000683D8 File Offset: 0x000665D8
		public NKMCraftSlotData GetSlotData(byte index)
		{
			NKMCraftSlotData result = null;
			if (!this.m_dicSlot.TryGetValue(index, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x000683FC File Offset: 0x000665FC
		public int GetReservedMoldCount()
		{
			int num = 0;
			foreach (NKMCraftSlotData nkmcraftSlotData in this.m_dicSlot.Values)
			{
				if (nkmcraftSlotData.MoldID > 0)
				{
					num += nkmcraftSlotData.Count;
				}
			}
			return num;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00068464 File Offset: 0x00066664
		public int GetEmptyMoldSlotCount()
		{
			int num = 0;
			using (Dictionary<byte, NKMCraftSlotData>.ValueCollection.Enumerator enumerator = this.m_dicSlot.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.MoldID == 0)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000684C4 File Offset: 0x000666C4
		public int GetFirstEmptySlotIndex()
		{
			foreach (NKMCraftSlotData nkmcraftSlotData in this.m_dicSlot.Values)
			{
				if (nkmcraftSlotData.MoldID == 0)
				{
					return (int)nkmcraftSlotData.Index;
				}
			}
			return -1;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0006852C File Offset: 0x0006672C
		public int GetUsedMoldSlotCount()
		{
			int num = 0;
			using (Dictionary<byte, NKMCraftSlotData>.ValueCollection.Enumerator enumerator = this.m_dicSlot.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.MoldID != 0)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0006858C File Offset: 0x0006678C
		public void ClearMoldItem()
		{
			this.m_dicMoldItem.Clear();
		}

		// Token: 0x040011B4 RID: 4532
		public static int MAX_CRAFT_SLOT_DATA = 5;

		// Token: 0x040011B5 RID: 4533
		private Dictionary<int, NKMMoldItemData> m_dicMoldItem = new Dictionary<int, NKMMoldItemData>();

		// Token: 0x040011B6 RID: 4534
		private Dictionary<byte, NKMCraftSlotData> m_dicSlot = new Dictionary<byte, NKMCraftSlotData>();
	}
}

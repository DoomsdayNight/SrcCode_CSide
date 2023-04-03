using System;
using System.Collections.Generic;
using ClientPacket.Item;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000672 RID: 1650
	public static class NKCEquipPresetDataManager
	{
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x001074AA File Offset: 0x001056AA
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x001074B4 File Offset: 0x001056B4
		public static List<NKMEquipPresetData> ListEquipPresetData
		{
			get
			{
				return NKCEquipPresetDataManager.m_listEquipPresetData;
			}
			set
			{
				List<NKMEquipPresetData> listEquipPresetData = NKCEquipPresetDataManager.m_listEquipPresetData;
				if (listEquipPresetData != null)
				{
					listEquipPresetData.Clear();
				}
				NKCEquipPresetDataManager.m_listEquipPresetData = value;
				List<NKMEquipPresetData> listEquipPresetData2 = NKCEquipPresetDataManager.m_listEquipPresetData;
				if (listEquipPresetData2 == null)
				{
					return;
				}
				listEquipPresetData2.Sort(delegate(NKMEquipPresetData e1, NKMEquipPresetData e2)
				{
					if (e1.presetIndex > e2.presetIndex)
					{
						return 1;
					}
					if (e1.presetIndex < e2.presetIndex)
					{
						return -1;
					}
					return 0;
				});
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x00107505 File Offset: 0x00105705
		public static HashSet<long> HSPresetEquipUId
		{
			get
			{
				return NKCEquipPresetDataManager.m_hsPresetEquipUId;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x0010750C File Offset: 0x0010570C
		public static List<NKMEquipPresetData> ListTempPresetData
		{
			get
			{
				return NKCEquipPresetDataManager.m_listTempPresetData;
			}
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00107513 File Offset: 0x00105713
		public static void RequestPresetData(bool _openUI)
		{
			NKCEquipPresetDataManager.ListEquipPresetData = null;
			NKCEquipPresetDataManager.m_hsPresetEquipUId.Clear();
			NKCEquipPresetDataManager.OpenUI = _openUI;
			NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_LIST_REQ(_openUI ? NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL : NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID);
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x00107537 File Offset: 0x00105737
		public static bool HasData()
		{
			return NKCEquipPresetDataManager.ListEquipPresetData != null && NKCEquipPresetDataManager.ListEquipPresetData.Count > 0;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00107550 File Offset: 0x00105750
		public static void RefreshEquipUidHash()
		{
			NKCEquipPresetDataManager.m_hsPresetEquipUId.Clear();
			if (NKCEquipPresetDataManager.m_listEquipPresetData == null)
			{
				return;
			}
			NKCEquipPresetDataManager.m_listEquipPresetData.ForEach(delegate(NKMEquipPresetData e)
			{
				int count = e.equipUids.Count;
				for (int i = 0; i < count; i++)
				{
					if (e.equipUids[i] > 0L)
					{
						NKCEquipPresetDataManager.m_hsPresetEquipUId.Add(e.equipUids[i]);
					}
				}
			});
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x00107590 File Offset: 0x00105790
		public static List<NKMEquipPresetData> GetPresetDataListForPage(int page, int maxSlotCountPerPage, bool slotMoveState)
		{
			List<NKMEquipPresetData> list = new List<NKMEquipPresetData>();
			List<NKMEquipPresetData> list2 = slotMoveState ? NKCEquipPresetDataManager.m_listTempPresetData : NKCEquipPresetDataManager.m_listEquipPresetData;
			if (list2 == null)
			{
				return list;
			}
			int num = page * maxSlotCountPerPage;
			if (num >= list2.Count)
			{
				return list;
			}
			int num2 = Mathf.Min((page + 1) * maxSlotCountPerPage, list2.Count);
			for (int i = num; i < num2; i++)
			{
				list.Add(list2[i]);
			}
			return list;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x001075F8 File Offset: 0x001057F8
		public static bool IsLastPage(int page, int maxSlotCountPerPage)
		{
			if (NKCEquipPresetDataManager.m_listEquipPresetData == null)
			{
				return true;
			}
			int num = (page + 1) * maxSlotCountPerPage - 1;
			return num >= NKCEquipPresetDataManager.m_listEquipPresetData.Count || num >= NKMCommonConst.EQUIP_PRESET_MAX_COUNT - 1;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x00107630 File Offset: 0x00105830
		public static void SwapTempPresetData(int index1, int index2)
		{
			int count = NKCEquipPresetDataManager.m_listTempPresetData.Count;
			if (index1 < 0 || index2 < 0 || index1 == index2)
			{
				return;
			}
			if (count <= index1 || count <= index2)
			{
				return;
			}
			NKMEquipPresetData value = NKCEquipPresetDataManager.m_listTempPresetData[index1];
			NKCEquipPresetDataManager.m_listTempPresetData[index1] = NKCEquipPresetDataManager.m_listTempPresetData[index2];
			NKCEquipPresetDataManager.m_listTempPresetData[index2] = value;
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x0010768C File Offset: 0x0010588C
		public static int GetTempPresetDataIndex(NKMEquipPresetData presetData)
		{
			if (presetData == null)
			{
				return -1;
			}
			return NKCEquipPresetDataManager.m_listTempPresetData.IndexOf(presetData);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x001076A0 File Offset: 0x001058A0
		public static List<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData> GetMovedSlotIndexList()
		{
			List<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData> list = new List<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData>();
			int count = NKCEquipPresetDataManager.m_listTempPresetData.Count;
			for (int i = 0; i < count; i++)
			{
				if (NKCEquipPresetDataManager.m_listTempPresetData[i] != null && i != NKCEquipPresetDataManager.m_listTempPresetData[i].presetIndex)
				{
					list.Add(new NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData
					{
						beforeIndex = NKCEquipPresetDataManager.m_listTempPresetData[i].presetIndex,
						afterIndex = i
					});
				}
			}
			return list;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00107718 File Offset: 0x00105918
		public static void UpdateTempPresetSlotData(NKMEquipPresetData equipPresetData)
		{
			int num = NKCEquipPresetDataManager.m_listTempPresetData.FindIndex((NKMEquipPresetData e) => e.presetIndex == equipPresetData.presetIndex);
			if (num < 0 || num >= NKCEquipPresetDataManager.m_listTempPresetData.Count)
			{
				return;
			}
			NKCEquipPresetDataManager.m_listTempPresetData[num] = equipPresetData;
		}

		// Token: 0x040032AF RID: 12975
		public static bool OpenUI;

		// Token: 0x040032B0 RID: 12976
		private static List<NKMEquipPresetData> m_listEquipPresetData;

		// Token: 0x040032B1 RID: 12977
		private static HashSet<long> m_hsPresetEquipUId = new HashSet<long>();

		// Token: 0x040032B2 RID: 12978
		private static List<NKMEquipPresetData> m_listTempPresetData = new List<NKMEquipPresetData>();
	}
}

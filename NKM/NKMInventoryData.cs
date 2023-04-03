using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Cs.Logging;
using Cs.Protocol;
using NKC;
using NKC.Publisher;

namespace NKM
{
	// Token: 0x020004F6 RID: 1270
	[DataContract]
	public class NKMInventoryData : Cs.Protocol.ISerializable
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x000BAB94 File Offset: 0x000B8D94
		public IReadOnlyDictionary<int, NKMItemMiscData> MiscItems
		{
			get
			{
				return this.m_ItemMiscData;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000BAB9C File Offset: 0x000B8D9C
		public IReadOnlyDictionary<long, NKMEquipItemData> EquipItems
		{
			get
			{
				return this.m_ItemEquipData;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x000BABA4 File Offset: 0x000B8DA4
		public IEnumerable<int> SkinIds
		{
			get
			{
				return this.m_ItemSkinData;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000BABAC File Offset: 0x000B8DAC
		public IReadOnlyDictionary<long, NKMEquipItemData> ItemEquipData
		{
			get
			{
				return this.m_ItemEquipData;
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000BABB4 File Offset: 0x000B8DB4
		public List<NKMItemMiscData> GetEmblemData()
		{
			List<NKMItemMiscData> list = new List<NKMItemMiscData>();
			foreach (KeyValuePair<int, NKMItemMiscData> keyValuePair in this.m_ItemMiscData)
			{
				NKMItemMiscData value = keyValuePair.Value;
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(value.ItemID);
				if (itemMiscTempletByID != null && itemMiscTempletByID.IsEmblem())
				{
					list.Add(value);
				}
			}
			return list;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000BAC30 File Offset: 0x000B8E30
		public void InitItemMisc(NKMItemMiscData MiscItemData)
		{
			NKMItemMiscData nkmitemMiscData = null;
			if (this.m_ItemMiscData.TryGetValue(MiscItemData.ItemID, out nkmitemMiscData))
			{
				nkmitemMiscData.CountFree += MiscItemData.CountFree;
				nkmitemMiscData.CountPaid += MiscItemData.CountPaid;
				return;
			}
			this.m_ItemMiscData.Add(MiscItemData.ItemID, MiscItemData);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000BAC90 File Offset: 0x000B8E90
		public void InitItemEquip(NKMEquipItemData equip_item_data)
		{
			if (this.m_ItemEquipData.ContainsKey(equip_item_data.m_ItemUid))
			{
				Log.Error("AddEquipItem, duplicated equipItemUID : " + equip_item_data.m_ItemUid.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUserData.cs", 252);
				return;
			}
			this.m_ItemEquipData.Add(equip_item_data.m_ItemUid, equip_item_data);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000BACE7 File Offset: 0x000B8EE7
		public void InitItemSkin(int skinID)
		{
			this.m_ItemSkinData.Add(skinID);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000BACF6 File Offset: 0x000B8EF6
		public void AddItemMisc(NKMItemMiscData MiscItemData)
		{
			if (MiscItemData == null)
			{
				return;
			}
			this.AddItemMisc(MiscItemData.ItemID, MiscItemData.CountFree, MiscItemData.CountPaid);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000BAD14 File Offset: 0x000B8F14
		public void AddItemMisc(List<NKMItemMiscData> lstMiscItem)
		{
			foreach (NKMItemMiscData miscItemData in lstMiscItem)
			{
				this.AddItemMisc(miscItemData);
			}
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000BAD64 File Offset: 0x000B8F64
		public void AddItemEquip(IEnumerable<NKMEquipItemData> lstEquipItem)
		{
			foreach (NKMEquipItemData equip_item_data in lstEquipItem)
			{
				this.AddItemEquip(equip_item_data);
			}
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000BADAC File Offset: 0x000B8FAC
		public void AddItemSkin(IEnumerable<int> lstSkinID)
		{
			foreach (int skinID in lstSkinID)
			{
				this.AddItemSkin(skinID);
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000BADF4 File Offset: 0x000B8FF4
		public void AddItemSkin(int skinID)
		{
			if (!this.m_ItemSkinData.Contains(skinID))
			{
				this.m_ItemSkinData.Add(skinID);
			}
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000BAE14 File Offset: 0x000B9014
		public void RemoveItemEquip(IEnumerable<long> lstUID)
		{
			if (lstUID == null)
			{
				return;
			}
			foreach (long uid in lstUID)
			{
				this.RemoveItemEquip(uid);
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000BAE60 File Offset: 0x000B9060
		public void UpdateItemInfo(List<NKMItemMiscData> lstMiscItem)
		{
			if (lstMiscItem == null)
			{
				return;
			}
			foreach (NKMItemMiscData itemData in lstMiscItem)
			{
				this.UpdateItemInfo(itemData);
			}
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000BAEB4 File Offset: 0x000B90B4
		public void UpdateItemInfo(NKMItemMiscData itemData)
		{
			if (itemData == null)
			{
				return;
			}
			this.UpdateItemInfo(itemData.ItemID, itemData.CountFree, itemData.CountPaid);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000BAED4 File Offset: 0x000B90D4
		public NKMItemMiscData GetItemMisc(int itemID)
		{
			NKMItemMiscData result;
			this.m_ItemMiscData.TryGetValue(itemID, out result);
			return result;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000BAEF4 File Offset: 0x000B90F4
		public NKMItemMiscData GetItemMisc(NKMItemMiscTemplet templet)
		{
			NKMItemMiscData result;
			this.m_ItemMiscData.TryGetValue(templet.m_ItemMiscID, out result);
			return result;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000BAF16 File Offset: 0x000B9116
		public bool GetItemMisc(int itemId, out NKMItemMiscData result)
		{
			return this.m_ItemMiscData.TryGetValue(itemId, out result);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000BAF28 File Offset: 0x000B9128
		public NKMEquipItemData GetItemEquip(long itemUid)
		{
			NKMEquipItemData result = null;
			this.m_ItemEquipData.TryGetValue(itemUid, out result);
			return result;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000BAF47 File Offset: 0x000B9147
		public bool HasItemSkin(int skinID)
		{
			return skinID == 0 || this.m_ItemSkinData.Contains(skinID);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000BAF5C File Offset: 0x000B915C
		public long GetCountMiscItem(int itemID)
		{
			NKMItemMiscData nkmitemMiscData;
			if (this.m_ItemMiscData.TryGetValue(itemID, out nkmitemMiscData))
			{
				return nkmitemMiscData.TotalCount;
			}
			return 0L;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000BAF84 File Offset: 0x000B9184
		public long GetCountMiscItem(int itemID, bool isPaid)
		{
			NKMItemMiscData nkmitemMiscData;
			if (!this.m_ItemMiscData.TryGetValue(itemID, out nkmitemMiscData))
			{
				return 0L;
			}
			if (!isPaid)
			{
				return nkmitemMiscData.CountFree;
			}
			return nkmitemMiscData.CountPaid;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000BAFB4 File Offset: 0x000B91B4
		public long GetCountMiscItem(NKMItemMiscTemplet templet)
		{
			NKMItemMiscData nkmitemMiscData;
			if (this.m_ItemMiscData.TryGetValue(templet.m_ItemMiscID, out nkmitemMiscData))
			{
				return nkmitemMiscData.TotalCount;
			}
			return 0L;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000BAFDF File Offset: 0x000B91DF
		public long GetCountDailyTrainingTicket()
		{
			return this.GetCountMiscItem(4);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000BAFE8 File Offset: 0x000B91E8
		public long GetCountDailyTrainingTicket_A()
		{
			return this.GetCountMiscItem(15);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000BAFF2 File Offset: 0x000B91F2
		public long GetCountDailyTrainingTicket_B()
		{
			return this.GetCountMiscItem(16);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000BAFFC File Offset: 0x000B91FC
		public long GetCountDailyTrainingTicket_C()
		{
			return this.GetCountMiscItem(17);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000BB006 File Offset: 0x000B9206
		public int GetCountMiscItemTypes()
		{
			return this.m_ItemMiscData.Count;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000BB014 File Offset: 0x000B9214
		public int GetCountMiscExceptCurrency()
		{
			int num = 0;
			foreach (KeyValuePair<int, NKMItemMiscData> keyValuePair in this.m_ItemMiscData)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(keyValuePair.Value.ItemID);
				if (itemMiscTempletByID == null)
				{
					Log.Error(string.Format("GetItemMiscTempletByID null itemID: {0}", keyValuePair.Value.ItemID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUserData.cs", 419);
				}
				else if (!itemMiscTempletByID.IsHideInInven() && keyValuePair.Value.TotalCount > 0L)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000BB0C0 File Offset: 0x000B92C0
		public int GetCountEquipItemTypes()
		{
			return this.m_ItemEquipData.Count;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000BB0D0 File Offset: 0x000B92D0
		public int GetCountUnEquipedItem()
		{
			int num = 0;
			using (Dictionary<long, NKMEquipItemData>.ValueCollection.Enumerator enumerator = this.m_ItemEquipData.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.m_OwnerUnitUID == -1L)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000BB130 File Offset: 0x000B9330
		public int GetCountSkinItemTypes()
		{
			return this.m_ItemSkinData.Count;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000BB13D File Offset: 0x000B933D
		public bool CanGetMoreEquipItem(int addCount)
		{
			return this.GetCountEquipItemTypes() + addCount <= this.m_MaxItemEqipCount;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000BB154 File Offset: 0x000B9354
		public void GetUsableMiscItemCount(int itemID, long itemCount, out long countFree, out long countPaid)
		{
			countFree = 0L;
			countPaid = 0L;
			NKMItemMiscData itemMisc = this.GetItemMisc(itemID);
			if (itemMisc == null)
			{
				return;
			}
			countFree = Math.Min(itemCount, itemMisc.CountFree);
			long num = itemCount - countFree;
			if (num == 0L)
			{
				return;
			}
			countPaid = Math.Min(num, itemMisc.CountPaid);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000BB1A0 File Offset: 0x000B93A0
		public int GetEquipCountByEnchantLevel(int enchantLevel)
		{
			return (from e in this.m_ItemEquipData.Values
			where e.m_EnchantLevel >= enchantLevel
			select e).Count<NKMEquipItemData>();
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000BB1DB File Offset: 0x000B93DB
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_MaxItemEqipCount);
			stream.PutOrGet<NKMItemMiscData>(ref this.m_ItemMiscData);
			stream.PutOrGet<NKMEquipItemData>(ref this.m_ItemEquipData);
			stream.PutOrGet(ref this.m_ItemSkinData);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060023F8 RID: 9208 RVA: 0x000BB210 File Offset: 0x000B9410
		// (remove) Token: 0x060023F9 RID: 9209 RVA: 0x000BB248 File Offset: 0x000B9448
		public event NKMInventoryData.OnMiscInventoryUpdate dOnMiscInventoryUpdate;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060023FA RID: 9210 RVA: 0x000BB280 File Offset: 0x000B9480
		// (remove) Token: 0x060023FB RID: 9211 RVA: 0x000BB2B8 File Offset: 0x000B94B8
		public event NKMInventoryData.OnEquipUpdate dOnEquipUpdate;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060023FC RID: 9212 RVA: 0x000BB2F0 File Offset: 0x000B94F0
		// (remove) Token: 0x060023FD RID: 9213 RVA: 0x000BB328 File Offset: 0x000B9528
		public event NKMInventoryData.OnRefreshDailyContents dOnRefreshDailyContents;

		// Token: 0x060023FE RID: 9214 RVA: 0x000BB360 File Offset: 0x000B9560
		public void AddItemMisc(int itemID, long countFree, long countPaid)
		{
			long num = countFree;
			long num2 = countPaid;
			NKMItemMiscData nkmitemMiscData = null;
			if (this.m_ItemMiscData.TryGetValue(itemID, out nkmitemMiscData))
			{
				num += nkmitemMiscData.CountFree;
				num2 += nkmitemMiscData.CountPaid;
			}
			this.UpdateItemInfo(itemID, num, num2);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000BB3A0 File Offset: 0x000B95A0
		public void AddItemEquip(NKMEquipItemData equip_item_data)
		{
			if (this.m_ItemEquipData.ContainsKey(equip_item_data.m_ItemUid))
			{
				Log.Error("AddEquipItem, duplicated equipItemUID : " + equip_item_data.m_ItemUid.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMUserDataEx.cs", 53);
				return;
			}
			this.m_ItemEquipData.Add(equip_item_data.m_ItemUid, equip_item_data);
			NKMInventoryData.OnEquipUpdate onEquipUpdate = this.dOnEquipUpdate;
			if (onEquipUpdate == null)
			{
				return;
			}
			onEquipUpdate(NKMUserData.eChangeNotifyType.Add, equip_item_data.m_ItemUid, equip_item_data);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000BB40C File Offset: 0x000B960C
		public void UpdateItemEquip(NKMEquipItemData equip_item_data)
		{
			if (equip_item_data == null)
			{
				return;
			}
			if (!this.m_ItemEquipData.ContainsKey(equip_item_data.m_ItemUid))
			{
				Log.Error("Tried to update nonexist item : " + equip_item_data.m_ItemUid.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMUserDataEx.cs", 69);
				return;
			}
			this.m_ItemEquipData[equip_item_data.m_ItemUid] = equip_item_data;
			NKMInventoryData.OnEquipUpdate onEquipUpdate = this.dOnEquipUpdate;
			if (onEquipUpdate == null)
			{
				return;
			}
			onEquipUpdate(NKMUserData.eChangeNotifyType.Update, equip_item_data.m_ItemUid, equip_item_data);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000BB47C File Offset: 0x000B967C
		public void RemoveItemMisc(int itemID, long count)
		{
			NKMItemMiscData nkmitemMiscData = null;
			if (this.m_ItemMiscData.TryGetValue(itemID, out nkmitemMiscData))
			{
				if (nkmitemMiscData.TotalCount < count)
				{
					return;
				}
				long num;
				long num2;
				this.GetUsableMiscItemCount(itemID, count, out num, out num2);
				this.UpdateItemInfo(itemID, nkmitemMiscData.CountFree - num, nkmitemMiscData.CountPaid - num2);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000BB4C8 File Offset: 0x000B96C8
		public void RemoveItemEquip(long uid)
		{
			if (this.m_ItemEquipData.ContainsKey(uid))
			{
				this.m_ItemEquipData.Remove(uid);
				NKMInventoryData.OnEquipUpdate onEquipUpdate = this.dOnEquipUpdate;
				if (onEquipUpdate == null)
				{
					return;
				}
				onEquipUpdate(NKMUserData.eChangeNotifyType.Remove, uid, null);
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000BB4F8 File Offset: 0x000B96F8
		public void UpdateItemInfo(int itemID, long countFree, long countPaid)
		{
			NKMItemMiscData nkmitemMiscData = null;
			if (this.m_ItemMiscData.TryGetValue(itemID, out nkmitemMiscData))
			{
				nkmitemMiscData.CountFree = countFree;
				nkmitemMiscData.CountPaid = countPaid;
			}
			else
			{
				nkmitemMiscData = new NKMItemMiscData(itemID, countFree, countPaid, 0);
				this.m_ItemMiscData.Add(itemID, nkmitemMiscData);
			}
			if (itemID == 2)
			{
				NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.RESOURCE_SUPPLY_COMPLETE, false);
			}
			NKMInventoryData.OnMiscInventoryUpdate onMiscInventoryUpdate = this.dOnMiscInventoryUpdate;
			if (onMiscInventoryUpdate == null)
			{
				return;
			}
			onMiscInventoryUpdate(nkmitemMiscData);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000BB560 File Offset: 0x000B9760
		public void UpdateItemInfo(int itemID, long count, NKM_ITEM_PAYMENT_TYPE payment_type)
		{
			NKMItemMiscData nkmitemMiscData = null;
			if (this.m_ItemMiscData.TryGetValue(itemID, out nkmitemMiscData))
			{
				if (payment_type == NKM_ITEM_PAYMENT_TYPE.NIPT_FREE)
				{
					nkmitemMiscData.CountFree = count;
				}
				else
				{
					nkmitemMiscData.CountPaid = count;
				}
			}
			else
			{
				long countFree = 0L;
				long countPaid = 0L;
				if (payment_type == NKM_ITEM_PAYMENT_TYPE.NIPT_FREE)
				{
					countFree = count;
				}
				else
				{
					countPaid = count;
				}
				nkmitemMiscData = new NKMItemMiscData(itemID, countFree, countPaid, 0);
				this.m_ItemMiscData.Add(itemID, nkmitemMiscData);
			}
			NKMInventoryData.OnMiscInventoryUpdate onMiscInventoryUpdate = this.dOnMiscInventoryUpdate;
			if (onMiscInventoryUpdate == null)
			{
				return;
			}
			onMiscInventoryUpdate(nkmitemMiscData);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000BB5CB File Offset: 0x000B97CB
		public bool IsFirstUpdate(int itemID, long itemCnt)
		{
			if (this.m_dicItemUpdateCheck.ContainsKey(itemID))
			{
				return this.m_dicItemUpdateCheck[itemID] != itemCnt;
			}
			this.m_dicItemUpdateCheck.Add(itemID, itemCnt);
			return false;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000BB5FC File Offset: 0x000B97FC
		public long GetPreviousItemCount(int itemID, long itemCnt)
		{
			long result = 0L;
			if (this.m_dicItemUpdateCheck.ContainsKey(itemID))
			{
				result = this.m_dicItemUpdateCheck[itemID];
				this.m_dicItemUpdateCheck[itemID] = itemCnt;
			}
			return result;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000BB638 File Offset: 0x000B9838
		public int GetSameKindEquipCount(int equipID, int setOptionID = 0)
		{
			int num = 0;
			foreach (NKMEquipItemData nkmequipItemData in this.m_ItemEquipData.Values)
			{
				if (nkmequipItemData.m_ItemEquipID == equipID && (setOptionID == 0 || nkmequipItemData.m_SetOptionId == setOptionID))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000BB6A8 File Offset: 0x000B98A8
		public void RefreshDailyContens()
		{
			NKMInventoryData.OnRefreshDailyContents onRefreshDailyContents = this.dOnRefreshDailyContents;
			if (onRefreshDailyContents == null)
			{
				return;
			}
			onRefreshDailyContents();
		}

		// Token: 0x040025B5 RID: 9653
		public int m_MaxItemEqipCount = 100;

		// Token: 0x040025B6 RID: 9654
		[DataMember]
		private Dictionary<int, NKMItemMiscData> m_ItemMiscData = new Dictionary<int, NKMItemMiscData>();

		// Token: 0x040025B7 RID: 9655
		[DataMember]
		private Dictionary<long, NKMEquipItemData> m_ItemEquipData = new Dictionary<long, NKMEquipItemData>();

		// Token: 0x040025B8 RID: 9656
		[DataMember]
		private HashSet<int> m_ItemSkinData = new HashSet<int>();

		// Token: 0x040025BC RID: 9660
		private Dictionary<int, long> m_dicItemUpdateCheck = new Dictionary<int, long>();

		// Token: 0x0200122D RID: 4653
		// (Invoke) Token: 0x0600A25F RID: 41567
		public delegate void OnMiscInventoryUpdate(NKMItemMiscData itemData);

		// Token: 0x0200122E RID: 4654
		// (Invoke) Token: 0x0600A263 RID: 41571
		public delegate void OnEquipUpdate(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipData);

		// Token: 0x0200122F RID: 4655
		// (Invoke) Token: 0x0600A267 RID: 41575
		public delegate void OnRefreshDailyContents();
	}
}

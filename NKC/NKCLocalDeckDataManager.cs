using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Logging;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007AE RID: 1966
	public static class NKCLocalDeckDataManager
	{
		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06004D94 RID: 19860 RVA: 0x0017629E File Offset: 0x0017449E
		public static bool DataLoaded
		{
			get
			{
				return NKCLocalDeckDataManager.m_dataLoaded;
			}
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x001762A5 File Offset: 0x001744A5
		public static void SetDataLoadedState(bool value)
		{
			NKCLocalDeckDataManager.m_dataLoaded = value;
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x001762AD File Offset: 0x001744AD
		public static void Clear()
		{
			NKCLocalDeckDataManager.m_localDeckData.Clear();
			NKCLocalDeckDataManager.m_deckKey.Clear();
			NKCLocalDeckDataManager.m_unitSlotCount = 0;
			NKCLocalDeckDataManager.m_dataLoaded = false;
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x001762D0 File Offset: 0x001744D0
		public static void LoadLocalDeckData(string localDeckKey, int deckIndex, int unitSlotCount)
		{
			if (NKCLocalDeckDataManager.m_dataLoaded)
			{
				return;
			}
			if (NKCLocalDeckDataManager.m_deckKey.ContainsKey(deckIndex))
			{
				Log.Error("Load Error: �� �ε����� �ߺ� �ԷµǾ����ϴ�.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCLocalDeckDataManager.cs", 46);
				return;
			}
			NKCLocalDeckDataManager.m_deckKey.Add(deckIndex, localDeckKey);
			NKCLocalDeckDataManager.m_unitSlotCount = unitSlotCount;
			string @string = PlayerPrefs.GetString(localDeckKey);
			NKMEventDeckData nkmeventDeckData = new NKMEventDeckData();
			if (!string.IsNullOrEmpty(@string))
			{
				nkmeventDeckData.FromBase64(@string);
				NKCLocalDeckDataManager.ValidateDeck(nkmeventDeckData);
			}
			else
			{
				for (int i = 0; i < unitSlotCount; i++)
				{
					nkmeventDeckData.m_dicUnit.Add(i, 0L);
				}
			}
			if (NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				NKCLocalDeckDataManager.m_localDeckData[deckIndex] = nkmeventDeckData;
				return;
			}
			NKCLocalDeckDataManager.m_localDeckData.Add(deckIndex, nkmeventDeckData);
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x00176380 File Offset: 0x00174580
		public static void SaveLocalDeck()
		{
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in NKCLocalDeckDataManager.m_localDeckData)
			{
				if (NKCLocalDeckDataManager.m_deckKey.ContainsKey(keyValuePair.Key))
				{
					string value = keyValuePair.Value.ToBase64<NKMEventDeckData>();
					PlayerPrefs.SetString(NKCLocalDeckDataManager.m_deckKey[keyValuePair.Key], value);
				}
			}
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x00176404 File Offset: 0x00174604
		public static void SetLocalUnitUId(int deckIndex, int slotIndex, long unitUId, bool prohibitSameUnitId)
		{
			if (!NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				return;
			}
			if (NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.ContainsKey(slotIndex))
			{
				NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[slotIndex] = unitUId;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMUnitData nkmunitData = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetUnitFromUID(unitUId) : null;
			if (nkmunitData == null)
			{
				return;
			}
			List<int> list = new List<int>();
			using (Dictionary<int, NKMEventDeckData>.Enumerator enumerator = NKCLocalDeckDataManager.m_localDeckData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, NKMEventDeckData> kvPair = enumerator.Current;
					bool flag = kvPair.Key == deckIndex;
					foreach (KeyValuePair<int, long> keyValuePair in kvPair.Value.m_dicUnit)
					{
						if (!flag || keyValuePair.Key != slotIndex)
						{
							long value = keyValuePair.Value;
							if (value > 0L)
							{
								if (value == unitUId)
								{
									list.Add(keyValuePair.Key);
								}
								else if (prohibitSameUnitId)
								{
									NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase((nkmuserData != null) ? nkmuserData.m_ArmyData.GetUnitFromUID(value) : null);
									if (unitTempletBase != null && unitTempletBase.IsSameBaseUnit(nkmunitData.m_UnitID))
									{
										list.Add(keyValuePair.Key);
									}
								}
							}
						}
					}
					list.ForEach(delegate(int e)
					{
						kvPair.Value.m_dicUnit[e] = 0L;
					});
					list.Clear();
				}
			}
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x001765A4 File Offset: 0x001747A4
		public static List<bool> SetLocalAutoDeckUnitUId(int deckIndex, List<long> unitList)
		{
			List<bool> list = new List<bool>();
			int count = unitList.Count;
			for (int i = 0; i < count; i++)
			{
				list.Add(false);
			}
			if (!NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex) || unitList == null)
			{
				return list;
			}
			if (NKCLocalDeckDataManager.m_localDeckData[deckIndex] == null)
			{
				return list;
			}
			for (int j = 0; j < count; j++)
			{
				if (NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.ContainsKey(j))
				{
					list[j] = (NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[j] != unitList[j]);
					NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[j] = unitList[j];
				}
			}
			return list;
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x00176660 File Offset: 0x00174860
		public static bool IsNextLocalSlotEmpty(int deckIndex, int currentIndex)
		{
			return NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex) && NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.ContainsKey(currentIndex + 1) && NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[currentIndex + 1] == 0L;
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x001766B8 File Offset: 0x001748B8
		public static List<long> GetLocalUnitDeckData(int deckIndex)
		{
			List<long> list = new List<long>();
			if (NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				foreach (KeyValuePair<int, long> keyValuePair in NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit)
				{
					list.Add(keyValuePair.Value);
				}
				return list;
			}
			for (int i = 0; i < NKCLocalDeckDataManager.m_unitSlotCount; i++)
			{
				list.Add(0L);
			}
			return list;
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x0017674C File Offset: 0x0017494C
		public static List<NKMUnitData> GetLocalUnitDeckUnitData(int deckIndex)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			List<long> localUnitDeckData = NKCLocalDeckDataManager.GetLocalUnitDeckData(deckIndex);
			int count = localUnitDeckData.Count;
			for (int i = 0; i < count; i++)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				NKMUnitData item = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetUnitFromUID(localUnitDeckData[i]) : null;
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x001767A1 File Offset: 0x001749A1
		public static long GetLocalUnitData(int deckIndex, int slotIndex)
		{
			NKCLocalDeckDataManager.GetLocalUnitDeckData(deckIndex);
			if (slotIndex >= 0 && NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.ContainsKey(slotIndex))
			{
				return NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[slotIndex];
			}
			return 0L;
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x001767DF File Offset: 0x001749DF
		public static Dictionary<int, NKMEventDeckData> GetAllLocalDeckData()
		{
			return NKCLocalDeckDataManager.m_localDeckData;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x001767E8 File Offset: 0x001749E8
		public static int SetLeaderIndex(int deckIndex, int index)
		{
			int num = -1;
			if (index >= 0 && NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex) && NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.ContainsKey(index))
			{
				if (NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[index] > 0L)
				{
					num = index;
				}
				else
				{
					foreach (KeyValuePair<int, long> keyValuePair in NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit)
					{
						if (keyValuePair.Value > 0L)
						{
							num = keyValuePair.Key;
							break;
						}
					}
				}
			}
			if (NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_LeaderIndex = num;
			}
			return num;
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x001768C4 File Offset: 0x00174AC4
		public static int GetLocalLeaderIndex(int deckIndex)
		{
			if (NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				return NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_LeaderIndex;
			}
			return -1;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x001768E8 File Offset: 0x00174AE8
		public static long GetLocalLeaderUnitUId(int deckIndex)
		{
			int localLeaderIndex = NKCLocalDeckDataManager.GetLocalLeaderIndex(deckIndex);
			return NKCLocalDeckDataManager.GetLocalUnitData(deckIndex, localLeaderIndex);
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x00176904 File Offset: 0x00174B04
		public static void SwapSlotData(int deckIndex, int firstIndex, int secondIndex)
		{
			if (firstIndex < 0 || secondIndex < 0 || deckIndex < 0)
			{
				return;
			}
			if (NKCLocalDeckDataManager.m_localDeckData.Count <= deckIndex || NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.Count <= firstIndex || NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.Count <= secondIndex)
			{
				return;
			}
			NKMEventDeckData nkmeventDeckData = NKCLocalDeckDataManager.m_localDeckData[deckIndex];
			long value = nkmeventDeckData.m_dicUnit[firstIndex];
			nkmeventDeckData.m_dicUnit[firstIndex] = nkmeventDeckData.m_dicUnit[secondIndex];
			nkmeventDeckData.m_dicUnit[secondIndex] = value;
			if (nkmeventDeckData.m_LeaderIndex == firstIndex)
			{
				nkmeventDeckData.m_LeaderIndex = secondIndex;
				return;
			}
			if (nkmeventDeckData.m_LeaderIndex == secondIndex)
			{
				nkmeventDeckData.m_LeaderIndex = firstIndex;
			}
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x001769BC File Offset: 0x00174BBC
		public static void SetLocalShipUId(int deckIndex, long shipUId, bool prohibitSameUnitId)
		{
			if (!NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				return;
			}
			NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_ShipUID = shipUId;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMUnitData nkmunitData = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetShipFromUID(shipUId) : null;
			if (nkmunitData == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in NKCLocalDeckDataManager.m_localDeckData)
			{
				if (keyValuePair.Key != deckIndex)
				{
					long shipUID = keyValuePair.Value.m_ShipUID;
					if (shipUID > 0L)
					{
						if (shipUID == shipUId)
						{
							keyValuePair.Value.m_ShipUID = 0L;
						}
						else if (prohibitSameUnitId)
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase((nkmuserData != null) ? nkmuserData.m_ArmyData.GetShipFromUID(shipUID) : null);
							NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(nkmunitData);
							if (unitTempletBase != null && unitTempletBase2 != null && unitTempletBase.m_ShipGroupID == unitTempletBase2.m_ShipGroupID)
							{
								keyValuePair.Value.m_ShipUID = 0L;
							}
						}
					}
				}
			}
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x00176AC8 File Offset: 0x00174CC8
		public static void SetLocalAutoDeckShipUId(int deckIndex, long shipUId)
		{
			if (!NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				return;
			}
			NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_ShipUID = shipUId;
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x00176AE9 File Offset: 0x00174CE9
		public static long GetShipUId(int deckIndex)
		{
			if (NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				return NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_ShipUID;
			}
			return 0L;
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00176B0C File Offset: 0x00174D0C
		public static void SetLocalOperatorUId(int deckIndex, long operatorUId, bool prohibitSameUnitId)
		{
			if (!NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				return;
			}
			NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_OperatorUID = operatorUId;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMOperator nkmoperator = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetOperatorFromUId(operatorUId) : null;
			if (nkmoperator == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in NKCLocalDeckDataManager.m_localDeckData)
			{
				if (keyValuePair.Key != deckIndex)
				{
					long operatorUID = keyValuePair.Value.m_OperatorUID;
					if (operatorUID > 0L)
					{
						if (operatorUID == operatorUId)
						{
							keyValuePair.Value.m_OperatorUID = 0L;
						}
						else if (prohibitSameUnitId)
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase((nkmuserData != null) ? nkmuserData.m_ArmyData.GetOperatorFromUId(operatorUID) : null);
							if (unitTempletBase != null && unitTempletBase.IsSameBaseUnit(nkmoperator.id))
							{
								keyValuePair.Value.m_OperatorUID = 0L;
							}
						}
					}
				}
			}
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x00176C08 File Offset: 0x00174E08
		public static long GetOperatorUId(int deckIndex)
		{
			if (NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				return NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_OperatorUID;
			}
			return 0L;
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x00176C2C File Offset: 0x00174E2C
		public static List<bool> ClearDeck(int deckIndex)
		{
			List<bool> list = new List<bool>();
			for (int i = 0; i < NKCLocalDeckDataManager.m_unitSlotCount; i++)
			{
				list.Add(false);
			}
			if (NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex))
			{
				List<int> list2 = new List<int>();
				foreach (KeyValuePair<int, long> keyValuePair in NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit)
				{
					if (keyValuePair.Key < NKCLocalDeckDataManager.m_unitSlotCount)
					{
						list[keyValuePair.Key] = (keyValuePair.Value > 0L);
					}
					list2.Add(keyValuePair.Key);
				}
				list2.ForEach(delegate(int e)
				{
					NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[e] = 0L;
				});
				NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_ShipUID = 0L;
				NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_OperatorUID = 0L;
			}
			return list;
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x00176D44 File Offset: 0x00174F44
		public static bool IsSameUnitIdInUnitDeckSlot(int deckIndex, int slotIndex, NKMUnitTempletBase unitTempletBase)
		{
			if (!NKCLocalDeckDataManager.m_localDeckData.ContainsKey(deckIndex) || NKCLocalDeckDataManager.m_localDeckData[deckIndex] == null || unitTempletBase == null)
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			if (NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit.ContainsKey(slotIndex))
			{
				long unitUid = NKCLocalDeckDataManager.m_localDeckData[deckIndex].m_dicUnit[slotIndex];
				NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(unitUid);
				if (unitFromUID == null)
				{
					return false;
				}
				if (unitTempletBase.IsSameBaseUnit(unitFromUID.m_UnitID))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00176DD0 File Offset: 0x00174FD0
		public static HashSet<int> GetAllUnitDeckUnitIdList()
		{
			HashSet<int> hashSet = new HashSet<int>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return hashSet;
			}
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in NKCLocalDeckDataManager.GetAllLocalDeckData())
			{
				int count = keyValuePair.Value.m_dicUnit.Count;
				foreach (KeyValuePair<int, long> keyValuePair2 in keyValuePair.Value.m_dicUnit)
				{
					long value = keyValuePair2.Value;
					if (value != 0L)
					{
						NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(value);
						if (unitFromUID != null)
						{
							hashSet.Add(unitFromUID.m_UnitID);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x00176EB4 File Offset: 0x001750B4
		public static int GetOperationPower(int deckIndex, bool bPVP, bool bPossibleShowBan, bool bPossibleShowUp)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0;
			}
			long shipUId = NKCLocalDeckDataManager.GetShipUId(deckIndex);
			NKMUnitData shipFromUID = nkmuserData.m_ArmyData.GetShipFromUID(shipUId);
			List<NKMUnitData> localUnitDeckUnitData = NKCLocalDeckDataManager.GetLocalUnitDeckUnitData(deckIndex);
			int operatorPower = 0;
			long operatorUId = NKCLocalDeckDataManager.GetOperatorUId(deckIndex);
			if (operatorUId != 0L)
			{
				NKMOperator operatorFromUId = nkmuserData.m_ArmyData.GetOperatorFromUId(operatorUId);
				if (operatorFromUId != null)
				{
					operatorPower = operatorFromUId.CalculateOperatorOperationPower();
				}
			}
			long localLeaderUnitUId = NKCLocalDeckDataManager.GetLocalLeaderUnitUId(deckIndex);
			Dictionary<int, NKMBanData> dicNKMBanData = bPossibleShowBan ? NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL) : null;
			Dictionary<int, NKMUnitUpData> dicNKMUpData = bPossibleShowUp ? NKCBanManager.m_dicNKMUpData : null;
			return NKMOperationPower.Calculate(shipFromUID, localUnitDeckUnitData, localLeaderUnitUId, nkmuserData.m_InventoryData, bPVP, dicNKMBanData, dicNKMUpData, operatorPower);
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x00176F48 File Offset: 0x00175148
		private static bool IsValideUnit(NKM_UNIT_TYPE unitType, long uid)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			switch (unitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				return nkmuserData.m_ArmyData.GetUnitFromUID(uid) != null;
			case NKM_UNIT_TYPE.NUT_SHIP:
				return nkmuserData.m_ArmyData.GetShipFromUID(uid) != null;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				return nkmuserData.m_ArmyData.GetOperatorFromUId(uid) != null;
			default:
				Log.Error("Invalid Unit Type", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCLocalDeckDataManager.cs", 520);
				return false;
			}
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x00176FB6 File Offset: 0x001751B6
		private static void ValidateDeck(NKMEventDeckData eventDeckData)
		{
			NKCLocalDeckDataManager.ValidateUnit(eventDeckData);
			NKCLocalDeckDataManager.ValidateShip(eventDeckData);
			NKCLocalDeckDataManager.ValidateOperator(eventDeckData);
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x00176FCC File Offset: 0x001751CC
		private static void ValidateUnit(NKMEventDeckData eventDeckData)
		{
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, long> keyValuePair in eventDeckData.m_dicUnit)
			{
				if (!NKCLocalDeckDataManager.IsValideUnit(NKM_UNIT_TYPE.NUT_NORMAL, keyValuePair.Value))
				{
					list.Add(keyValuePair.Key);
				}
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (eventDeckData.m_dicUnit.ContainsKey(list[i]))
				{
					eventDeckData.m_dicUnit[list[i]] = 0L;
				}
			}
			NKCLocalDeckDataManager.ValidateLeaderIndex(eventDeckData);
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x00177084 File Offset: 0x00175284
		private static void ValidateShip(NKMEventDeckData eventDeckData)
		{
			if (!NKCLocalDeckDataManager.IsValideUnit(NKM_UNIT_TYPE.NUT_SHIP, eventDeckData.m_ShipUID))
			{
				eventDeckData.m_ShipUID = 0L;
			}
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0017709C File Offset: 0x0017529C
		private static void ValidateOperator(NKMEventDeckData eventDeckData)
		{
			if (!NKCLocalDeckDataManager.IsValideUnit(NKM_UNIT_TYPE.NUT_OPERATOR, eventDeckData.m_OperatorUID))
			{
				eventDeckData.m_OperatorUID = 0L;
			}
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x001770B4 File Offset: 0x001752B4
		private static void ValidateLeaderIndex(NKMEventDeckData eventDeckData)
		{
			int leaderIndex = -1;
			int leaderIndex2 = eventDeckData.m_LeaderIndex;
			if (leaderIndex2 >= 0 && eventDeckData.m_dicUnit.ContainsKey(leaderIndex2))
			{
				if (eventDeckData.m_dicUnit[leaderIndex2] > 0L)
				{
					return;
				}
				foreach (KeyValuePair<int, long> keyValuePair in eventDeckData.m_dicUnit)
				{
					if (keyValuePair.Value > 0L)
					{
						leaderIndex = keyValuePair.Key;
						break;
					}
				}
			}
			eventDeckData.m_LeaderIndex = leaderIndex;
		}

		// Token: 0x04003D6E RID: 15726
		public const int NoLeaderIndex = -1;

		// Token: 0x04003D6F RID: 15727
		private static Dictionary<int, NKMEventDeckData> m_localDeckData = new Dictionary<int, NKMEventDeckData>();

		// Token: 0x04003D70 RID: 15728
		private static Dictionary<int, string> m_deckKey = new Dictionary<int, string>();

		// Token: 0x04003D71 RID: 15729
		private static int m_unitSlotCount;

		// Token: 0x04003D72 RID: 15730
		private static bool m_dataLoaded = false;
	}
}

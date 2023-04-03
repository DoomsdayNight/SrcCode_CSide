using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ClientPacket.Game;
using ClientPacket.Service;
using NKM;

namespace NKC
{
	// Token: 0x020006B7 RID: 1719
	public static class NKCPacketObjectPool
	{
		// Token: 0x0600391C RID: 14620 RVA: 0x00128076 File Offset: 0x00126276
		public static void SetUsePool(bool bUse)
		{
			NKCPacketObjectPool.bUsePool = bUse;
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x00128080 File Offset: 0x00126280
		public static void Init()
		{
			NKCPacketObjectPool.typePools.Clear();
			NKCPacketObjectPool.typePools.Add(typeof(NKMPacket_HEART_BIT_ACK), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMPacket_HEART_BIT_ACK)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMPacket_SERVER_TIME_ACK), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMPacket_SERVER_TIME_ACK)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncDataPack), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncDataPack)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_Base), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_Base)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_Fierce), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_Fierce)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_DieUnit), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_DieUnit)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_Unit), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_Unit)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncDataSimple_Unit), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncDataSimple_Unit)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMDamageData), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMDamageData)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMBuffSyncData), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMBuffSyncData)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMUnitSyncData), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMUnitSyncData)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_ShipSkill), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_ShipSkill)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_Deck), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_Deck)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_DeckAssist), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_DeckAssist)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_GameState), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_GameState)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_DungeonEvent), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_DungeonEvent)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMGameSyncData_GameEvent), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMGameSyncData_GameEvent)));
			NKCPacketObjectPool.typePools.Add(typeof(NKMUnitStatusTimeSyncData), new NKCPacketObjectPool.TypeElement(new Action<object>(NKCPacketObjectPool.Open_NKMStatusTimeSyncData)));
			foreach (KeyValuePair<Type, NKCPacketObjectPool.TypeElement> keyValuePair in NKCPacketObjectPool.typePools)
			{
				for (int i = 0; i < 10000; i++)
				{
					Type key = keyValuePair.Key;
					keyValuePair.Value.objects.Enqueue(Activator.CreateInstance(key));
				}
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x001283C4 File Offset: 0x001265C4
		public static bool IsManagedType(Type type)
		{
			return NKCPacketObjectPool.typePools.ContainsKey(type);
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x001283D1 File Offset: 0x001265D1
		public static void Open_Packet(object obj)
		{
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x001283D5 File Offset: 0x001265D5
		public static void Open_NKMPacket_HEART_BIT_ACK(object obj)
		{
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x001283D9 File Offset: 0x001265D9
		public static void Open_NKMPacket_SERVER_TIME_ACK(object obj)
		{
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x001283DD File Offset: 0x001265DD
		public static void Open_NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT(object obj)
		{
			if (obj == null)
			{
				return;
			}
			NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT = (NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT)obj;
			NKCPacketObjectPool.CloseObject(nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT.gameSyncDataPack);
			nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT.gameSyncDataPack = null;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x001283FC File Offset: 0x001265FC
		public static void Open_NKMGameSyncDataPack(object obj)
		{
			if (obj == null)
			{
				return;
			}
			NKMGameSyncDataPack nkmgameSyncDataPack = (NKMGameSyncDataPack)obj;
			for (int i = 0; i < nkmgameSyncDataPack.m_listGameSyncData.Count; i++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncDataPack.m_listGameSyncData[i]);
			}
			nkmgameSyncDataPack.m_listGameSyncData.Clear();
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x00128448 File Offset: 0x00126648
		public static void Open_NKMGameSyncData_Base(object obj)
		{
			if (obj == null)
			{
				return;
			}
			NKMGameSyncData_Base nkmgameSyncData_Base = (NKMGameSyncData_Base)obj;
			for (int i = 0; i < nkmgameSyncData_Base.m_NKMGameSyncData_DieUnit.Count; i++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_DieUnit[i]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_DieUnit.Clear();
			for (int j = 0; j < nkmgameSyncData_Base.m_NKMGameSyncData_Unit.Count; j++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_Unit[j]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_Unit.Clear();
			for (int k = 0; k < nkmgameSyncData_Base.m_NKMGameSyncDataSimple_Unit.Count; k++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncDataSimple_Unit[k]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncDataSimple_Unit.Clear();
			for (int l = 0; l < nkmgameSyncData_Base.m_NKMGameSyncData_ShipSkill.Count; l++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_ShipSkill[l]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_ShipSkill.Clear();
			for (int m = 0; m < nkmgameSyncData_Base.m_NKMGameSyncData_Deck.Count; m++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_Deck[m]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_Deck.Clear();
			for (int n = 0; n < nkmgameSyncData_Base.m_NKMGameSyncData_DeckAssist.Count; n++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_DeckAssist[n]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_DeckAssist.Clear();
			for (int num = 0; num < nkmgameSyncData_Base.m_NKMGameSyncData_GameState.Count; num++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_GameState[num]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_GameState.Clear();
			for (int num2 = 0; num2 < nkmgameSyncData_Base.m_NKMGameSyncData_DungeonEvent.Count; num2++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_DungeonEvent[num2]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_DungeonEvent.Clear();
			for (int num3 = 0; num3 < nkmgameSyncData_Base.m_NKMGameSyncData_GameEvent.Count; num3++)
			{
				NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_GameEvent[num3]);
			}
			nkmgameSyncData_Base.m_NKMGameSyncData_GameEvent.Clear();
			NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_Fierce);
			nkmgameSyncData_Base.m_NKMGameSyncData_Fierce = null;
			NKCPacketObjectPool.CloseObject(nkmgameSyncData_Base.m_NKMGameSyncData_Trim);
			nkmgameSyncData_Base.m_NKMGameSyncData_Trim = null;
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x00128664 File Offset: 0x00126864
		public static void Open_NKMGameSyncData_Fierce(object obj)
		{
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x00128668 File Offset: 0x00126868
		public static void Open_NKMGameSyncData_DieUnit(object obj)
		{
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x0012866C File Offset: 0x0012686C
		public static void Open_NKMGameSyncData_Unit(object obj)
		{
			if (obj == null)
			{
				return;
			}
			NKMGameSyncData_Unit nkmgameSyncData_Unit = (NKMGameSyncData_Unit)obj;
			NKCPacketObjectPool.CloseObject(nkmgameSyncData_Unit.m_NKMGameUnitSyncData);
			nkmgameSyncData_Unit.m_NKMGameUnitSyncData = null;
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x0012868C File Offset: 0x0012688C
		public static void Open_NKMGameSyncDataSimple_Unit(object obj)
		{
			if (obj == null)
			{
				return;
			}
			NKMGameSyncDataSimple_Unit nkmgameSyncDataSimple_Unit = (NKMGameSyncDataSimple_Unit)obj;
			foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair in nkmgameSyncDataSimple_Unit.m_dicBuffData)
			{
				NKCPacketObjectPool.CloseObject(keyValuePair.Value);
			}
			nkmgameSyncDataSimple_Unit.m_dicBuffData.Clear();
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x001286FC File Offset: 0x001268FC
		public static void Open_NKMDamageData(object obj)
		{
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x00128700 File Offset: 0x00126900
		public static void Open_NKMBuffSyncData(object obj)
		{
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x00128704 File Offset: 0x00126904
		public static void Open_NKMStatusTimeSyncData(object obj)
		{
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x00128708 File Offset: 0x00126908
		public static void Open_NKMUnitSyncData(object obj)
		{
			if (obj == null)
			{
				return;
			}
			NKMUnitSyncData nkmunitSyncData = (NKMUnitSyncData)obj;
			for (int i = 0; i < nkmunitSyncData.m_listDamageData.Count; i++)
			{
				NKCPacketObjectPool.CloseObject(nkmunitSyncData.m_listDamageData[i]);
			}
			nkmunitSyncData.m_listDamageData.Clear();
			foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair in nkmunitSyncData.m_dicBuffData)
			{
				NKCPacketObjectPool.CloseObject(keyValuePair.Value);
			}
			nkmunitSyncData.m_dicBuffData.Clear();
			foreach (NKMUnitStatusTimeSyncData obj2 in nkmunitSyncData.m_listStatusTimeData)
			{
				NKCPacketObjectPool.CloseObject(obj2);
			}
			nkmunitSyncData.m_listStatusTimeData.Clear();
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x001287F4 File Offset: 0x001269F4
		public static void Open_NKMGameSyncData_ShipSkill(object obj)
		{
			if (obj == null)
			{
				return;
			}
			NKMGameSyncData_ShipSkill nkmgameSyncData_ShipSkill = (NKMGameSyncData_ShipSkill)obj;
			NKCPacketObjectPool.CloseObject(nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData);
			nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData = null;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x00128811 File Offset: 0x00126A11
		public static void Open_NKMGameSyncData_Deck(object obj)
		{
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x00128815 File Offset: 0x00126A15
		public static void Open_NKMGameSyncData_DeckAssist(object obj)
		{
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x00128819 File Offset: 0x00126A19
		public static void Open_NKMGameSyncData_GameState(object obj)
		{
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x0012881D File Offset: 0x00126A1D
		public static void Open_NKMGameSyncData_DungeonEvent(object obj)
		{
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00128821 File Offset: 0x00126A21
		public static void Open_NKMGameSyncData_GameEvent(object obj)
		{
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00128828 File Offset: 0x00126A28
		public static object OpenObject(Type type)
		{
			if (!NKCPacketObjectPool.bUsePool)
			{
				return Activator.CreateInstance(type);
			}
			NKCPacketObjectPool.TypeElement typeElement;
			if (!NKCPacketObjectPool.typePools.TryGetValue(type, out typeElement))
			{
				return Activator.CreateInstance(type);
			}
			object result;
			if (!typeElement.TryPop(out result))
			{
				result = Activator.CreateInstance(type);
			}
			return result;
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x0012886C File Offset: 0x00126A6C
		public static T OpenObject<T>() where T : class, new()
		{
			if (!NKCPacketObjectPool.bUsePool)
			{
				return Activator.CreateInstance<T>();
			}
			Type typeFromHandle = typeof(T);
			NKCPacketObjectPool.TypeElement typeElement;
			if (!NKCPacketObjectPool.typePools.TryGetValue(typeFromHandle, out typeElement))
			{
				return Activator.CreateInstance<T>();
			}
			object obj;
			if (!typeElement.TryPop(out obj))
			{
				obj = Activator.CreateInstance(typeFromHandle);
			}
			return obj as T;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x001288C4 File Offset: 0x00126AC4
		public static void CloseObject(object obj)
		{
			if (!NKCPacketObjectPool.bUsePool || obj == null)
			{
				return;
			}
			Type type = obj.GetType();
			NKCPacketObjectPool.TypeElement typeElement;
			if (!NKCPacketObjectPool.typePools.TryGetValue(type, out typeElement))
			{
				return;
			}
			typeElement.cleaner(obj);
			typeElement.Push(obj);
		}

		// Token: 0x04003501 RID: 13569
		private static bool bUsePool = true;

		// Token: 0x04003502 RID: 13570
		private static readonly Dictionary<Type, NKCPacketObjectPool.TypeElement> typePools = new Dictionary<Type, NKCPacketObjectPool.TypeElement>();

		// Token: 0x02001384 RID: 4996
		private sealed class TypeElement
		{
			// Token: 0x0600A60B RID: 42507 RVA: 0x003465B0 File Offset: 0x003447B0
			public TypeElement(Action<object> cleaner)
			{
				this.cleaner = cleaner;
			}

			// Token: 0x0600A60C RID: 42508 RVA: 0x003465CA File Offset: 0x003447CA
			public bool TryPop(out object data)
			{
				return this.objects.TryDequeue(out data);
			}

			// Token: 0x0600A60D RID: 42509 RVA: 0x003465D8 File Offset: 0x003447D8
			public void Push(object data)
			{
				this.objects.Enqueue(data);
			}

			// Token: 0x04009A6A RID: 39530
			public readonly Action<object> cleaner;

			// Token: 0x04009A6B RID: 39531
			public readonly ConcurrentQueue<object> objects = new ConcurrentQueue<object>();
		}
	}
}

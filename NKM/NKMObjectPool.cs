using System;
using System.Collections.Generic;
using System.Threading;
using Cs.Logging;

namespace NKM
{
	// Token: 0x02000446 RID: 1094
	public class NKMObjectPool
	{
		// Token: 0x06001DBA RID: 7610 RVA: 0x0008D8EB File Offset: 0x0008BAEB
		public static long GetObjUID()
		{
			return Interlocked.Increment(ref NKMObjectPool.m_ObjUidIndex);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0008D920 File Offset: 0x0008BB20
		public virtual void Init()
		{
			List<NKMGameUnitRespawnData> list = new List<NKMGameUnitRespawnData>();
			for (int i = 0; i < 100; i++)
			{
				NKMGameUnitRespawnData item = (NKMGameUnitRespawnData)this.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMGameUnitRespawnData, "", "", false);
				list.Add(item);
			}
			for (int j = 0; j < list.Count; j++)
			{
				this.CloseObj(list[j]);
			}
			List<NKMStateCoolTime> list2 = new List<NKMStateCoolTime>();
			for (int k = 0; k < 100; k++)
			{
				NKMStateCoolTime item2 = (NKMStateCoolTime)this.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMStateCoolTime, "", "", false);
				list2.Add(item2);
			}
			for (int l = 0; l < list2.Count; l++)
			{
				this.CloseObj(list2[l]);
			}
			List<NKMTimeStamp> list3 = new List<NKMTimeStamp>();
			for (int m = 0; m < 100; m++)
			{
				NKMTimeStamp item3 = (NKMTimeStamp)this.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMTimeStamp, "", "", false);
				list3.Add(item3);
			}
			for (int n = 0; n < list3.Count; n++)
			{
				this.CloseObj(list3[n]);
			}
			List<NKMDamageInst> list4 = new List<NKMDamageInst>();
			for (int num = 0; num < 100; num++)
			{
				NKMDamageInst item4 = (NKMDamageInst)this.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageInst, "", "", false);
				list4.Add(item4);
			}
			for (int num2 = 0; num2 < list4.Count; num2++)
			{
				this.CloseObj(list4[num2]);
			}
			List<NKMBuffData> list5 = new List<NKMBuffData>();
			for (int num3 = 0; num3 < 100; num3++)
			{
				NKMBuffData item5 = (NKMBuffData)this.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMBuffData, "", "", false);
				list5.Add(item5);
			}
			for (int num4 = 0; num4 < list5.Count; num4++)
			{
				this.CloseObj(list5[num4]);
			}
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0008DAF8 File Offset: 0x0008BCF8
		public virtual void Unload()
		{
			foreach (KeyValuePair<NKM_OBJECT_POOL_TYPE, NKMObjectPoolList> keyValuePair in this.m_dicObjectPoolList)
			{
				foreach (KeyValuePair<string, NKMObjectPoolListBundle> keyValuePair2 in keyValuePair.Value.m_dicNKCObjectPoolDataBundle)
				{
					Dictionary<string, NKMObjectPoolListName>.Enumerator enumerator3 = keyValuePair2.Value.m_dicNKCObjectPoolDataName.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						this.m_listUnloadObjectTemp.Clear();
						KeyValuePair<string, NKMObjectPoolListName> keyValuePair3 = enumerator3.Current;
						NKMObjectPoolListName value = keyValuePair3.Value;
						foreach (KeyValuePair<long, NKMObjectPoolData> keyValuePair4 in value.m_dicNKCObjectPoolData)
						{
							NKMObjectPoolData value2 = keyValuePair4.Value;
							if (value2.m_bUnloadable)
							{
								this.m_listUnloadObjectTemp.Add(value2);
							}
						}
						for (int i = 0; i < this.m_listUnloadObjectTemp.Count; i++)
						{
							NKMObjectPoolData nkmobjectPoolData = this.m_listUnloadObjectTemp[i];
							if (value.m_dicNKCObjectPoolData.ContainsKey(nkmobjectPoolData.m_ObjUID))
							{
								nkmobjectPoolData.Unload();
								value.m_dicNKCObjectPoolData.Remove(nkmobjectPoolData.m_ObjUID);
								this.m_ObjectCount--;
							}
						}
						this.m_listUnloadObjectTemp.Clear();
					}
				}
			}
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0008DC49 File Offset: 0x0008BE49
		public virtual void Update()
		{
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0008DC4B File Offset: 0x0008BE4B
		public bool IsLoadComplete()
		{
			if (this.m_qAsyncLoadObject.Count > 0)
			{
				return false;
			}
			this.m_LoadCountMax = 0;
			return true;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0008DC68 File Offset: 0x0008BE68
		public float GetLoadProgress()
		{
			float num = 0f;
			if (this.m_LoadCountMax == 0)
			{
				num += 1f;
			}
			else
			{
				num += 1f - (float)this.m_qAsyncLoadObject.Count / (float)this.m_LoadCountMax;
			}
			return num;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0008DCAC File Offset: 0x0008BEAC
		protected virtual NKMObjectPoolData CreateNewObj(NKM_OBJECT_POOL_TYPE typeT, string bundleName = "", string name = "", bool bAsync = false)
		{
			NKMObjectPoolData result = null;
			switch (typeT)
			{
			case NKM_OBJECT_POOL_TYPE.NOPT_NKMGameUnitRespawnData:
				result = new NKMGameUnitRespawnData();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKMStateCoolTime:
				result = new NKMStateCoolTime();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKMTimeStamp:
				result = new NKMTimeStamp();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageInst:
				result = new NKMDamageInst();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageEffect:
				result = new NKMDamageEffect();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKMBuffData:
				result = new NKMBuffData();
				break;
			}
			return result;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0008DD0C File Offset: 0x0008BF0C
		public virtual NKMObjectPoolData OpenObj(NKM_OBJECT_POOL_TYPE typeT, NKMAssetName cNKMAssetName, bool bAsync = false)
		{
			return this.OpenObj(typeT, cNKMAssetName.m_BundleName, cNKMAssetName.m_AssetName, bAsync);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0008DD24 File Offset: 0x0008BF24
		public virtual NKMObjectPoolData OpenObj(NKM_OBJECT_POOL_TYPE typeT, string bundleName = "", string name = "", bool bAsync = false)
		{
			NKMObjectPoolData nkmobjectPoolData = null;
			if (this.m_dicObjectPoolList.ContainsKey(typeT))
			{
				NKMObjectPoolList nkmobjectPoolList = this.m_dicObjectPoolList[typeT];
				if (nkmobjectPoolList.m_dicNKCObjectPoolDataBundle.ContainsKey(bundleName))
				{
					NKMObjectPoolListBundle nkmobjectPoolListBundle = nkmobjectPoolList.m_dicNKCObjectPoolDataBundle[bundleName];
					if (nkmobjectPoolListBundle.m_dicNKCObjectPoolDataName.ContainsKey(name))
					{
						NKMObjectPoolListName nkmobjectPoolListName = nkmobjectPoolListBundle.m_dicNKCObjectPoolDataName[name];
						if (nkmobjectPoolListName.m_dicNKCObjectPoolData.Count > 0)
						{
							Dictionary<long, NKMObjectPoolData>.Enumerator enumerator = nkmobjectPoolListName.m_dicNKCObjectPoolData.GetEnumerator();
							enumerator.MoveNext();
							KeyValuePair<long, NKMObjectPoolData> keyValuePair = enumerator.Current;
							nkmobjectPoolData = keyValuePair.Value;
							Dictionary<long, NKMObjectPoolData> dicNKCObjectPoolData = nkmobjectPoolListName.m_dicNKCObjectPoolData;
							keyValuePair = enumerator.Current;
							dicNKCObjectPoolData.Remove(keyValuePair.Key);
							this.m_ObjectCount--;
						}
					}
				}
			}
			if (nkmobjectPoolData == null)
			{
				nkmobjectPoolData = this.CreateNewObj(typeT, bundleName, name, bAsync);
				if (nkmobjectPoolData == null)
				{
					return null;
				}
				if (!bAsync)
				{
					if (!nkmobjectPoolData.LoadComplete())
					{
						nkmobjectPoolData = this.CreateNewObj(typeT, bundleName, name, false);
						if (nkmobjectPoolData == null)
						{
							return null;
						}
						if (!nkmobjectPoolData.LoadComplete())
						{
							Log.ErrorAndExit("LoadComplete 실패 [m_ObjectPoolBundleName :" + nkmobjectPoolData.m_ObjectPoolBundleName + "] m_ObjectPoolName:" + nkmobjectPoolData.m_ObjectPoolName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMObjectPool.cs", 331);
							return null;
						}
					}
					nkmobjectPoolData.m_bIsLoaded = true;
					nkmobjectPoolData.Open();
				}
				else
				{
					this.m_qAsyncLoadObject.Enqueue(nkmobjectPoolData);
					this.m_LoadCountMax = this.m_qAsyncLoadObject.Count;
				}
			}
			else
			{
				nkmobjectPoolData.Open();
			}
			return nkmobjectPoolData;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0008DE88 File Offset: 0x0008C088
		public virtual void CloseObj(NKMObjectPoolData closeObj)
		{
			if (closeObj == null)
			{
				return;
			}
			NKM_OBJECT_POOL_TYPE nkm_OBJECT_POOL_TYPE = closeObj.m_NKM_OBJECT_POOL_TYPE;
			NKMObjectPoolList nkmobjectPoolList;
			if (this.m_dicObjectPoolList.ContainsKey(nkm_OBJECT_POOL_TYPE))
			{
				nkmobjectPoolList = this.m_dicObjectPoolList[nkm_OBJECT_POOL_TYPE];
			}
			else
			{
				nkmobjectPoolList = new NKMObjectPoolList();
				this.m_dicObjectPoolList.Add(nkm_OBJECT_POOL_TYPE, nkmobjectPoolList);
			}
			NKMObjectPoolListBundle nkmobjectPoolListBundle;
			if (nkmobjectPoolList.m_dicNKCObjectPoolDataBundle.ContainsKey(closeObj.m_ObjectPoolBundleName))
			{
				nkmobjectPoolListBundle = nkmobjectPoolList.m_dicNKCObjectPoolDataBundle[closeObj.m_ObjectPoolBundleName];
			}
			else
			{
				nkmobjectPoolListBundle = new NKMObjectPoolListBundle();
				nkmobjectPoolList.m_dicNKCObjectPoolDataBundle.Add(closeObj.m_ObjectPoolBundleName, nkmobjectPoolListBundle);
			}
			NKMObjectPoolListName nkmobjectPoolListName;
			if (nkmobjectPoolListBundle.m_dicNKCObjectPoolDataName.ContainsKey(closeObj.m_ObjectPoolName))
			{
				nkmobjectPoolListName = nkmobjectPoolListBundle.m_dicNKCObjectPoolDataName[closeObj.m_ObjectPoolName];
			}
			else
			{
				nkmobjectPoolListName = new NKMObjectPoolListName();
				nkmobjectPoolListBundle.m_dicNKCObjectPoolDataName.Add(closeObj.m_ObjectPoolName, nkmobjectPoolListName);
			}
			if (!nkmobjectPoolListName.m_dicNKCObjectPoolData.ContainsKey(closeObj.m_ObjUID))
			{
				nkmobjectPoolListName.m_dicNKCObjectPoolData.Add(closeObj.m_ObjUID, closeObj);
				this.m_ObjectCount++;
			}
			closeObj.Close();
		}

		// Token: 0x04001E13 RID: 7699
		private static long m_ObjUidIndex = 1L;

		// Token: 0x04001E14 RID: 7700
		protected Dictionary<NKM_OBJECT_POOL_TYPE, NKMObjectPoolList> m_dicObjectPoolList = new Dictionary<NKM_OBJECT_POOL_TYPE, NKMObjectPoolList>();

		// Token: 0x04001E15 RID: 7701
		protected Queue<NKMObjectPoolData> m_qAsyncLoadObject = new Queue<NKMObjectPoolData>();

		// Token: 0x04001E16 RID: 7702
		protected List<NKMObjectPoolData> m_listUnloadObjectTemp = new List<NKMObjectPoolData>();

		// Token: 0x04001E17 RID: 7703
		protected int m_ObjectCount;

		// Token: 0x04001E18 RID: 7704
		private int m_LoadCountMax;
	}
}

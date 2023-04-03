using System;
using System.Collections.Generic;
using Cs.Logging;

namespace NKM
{
	// Token: 0x02000399 RID: 921
	public class NKMObjectAnimTimeBundleData
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0005F4DF File Offset: 0x0005D6DF
		public Dictionary<string, NKMObjectAnimTimeData> GetAnimBundleData
		{
			get
			{
				return this.m_dicObjectAnimBundleData;
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0005F500 File Offset: 0x0005D700
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			int num = 1;
			string bundleName = "";
			while (cNKMLua.OpenTable(num))
			{
				cNKMLua.GetData("bundleName", ref bundleName);
				this.GetBundleData(bundleName).LoadFromLUA(cNKMLua);
				num++;
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0005F548 File Offset: 0x0005D748
		public NKMObjectAnimTimeData GetBundleData(string bundleName)
		{
			if (this.m_dicObjectAnimBundleData.ContainsKey(bundleName))
			{
				return this.m_dicObjectAnimBundleData[bundleName];
			}
			NKMObjectAnimTimeData nkmobjectAnimTimeData = new NKMObjectAnimTimeData();
			this.m_dicObjectAnimBundleData.Add(bundleName, nkmobjectAnimTimeData);
			return nkmobjectAnimTimeData;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0005F584 File Offset: 0x0005D784
		public float GetAnimTimeMax(string bundleName, string objectName, string animName)
		{
			if (!this.m_dicObjectAnimBundleData.ContainsKey(bundleName))
			{
				Log.Error(string.Concat(new string[]
				{
					"NKMAnimDataManager No Exist ANIM Bundle: ",
					bundleName,
					" : ",
					objectName,
					" : ",
					animName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAnimDataManager.cs", 147);
				return 0f;
			}
			NKMObjectAnimTimeData nkmobjectAnimTimeData = this.m_dicObjectAnimBundleData[bundleName];
			if (nkmobjectAnimTimeData == null)
			{
				Log.Error(string.Concat(new string[]
				{
					"NKMAnimDataManager No Exist ANIM Bundle: ",
					bundleName,
					" : ",
					objectName,
					" : ",
					animName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAnimDataManager.cs", 154);
				return 0f;
			}
			return nkmobjectAnimTimeData.GetAnimTimeMax(objectName, animName);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0005F63F File Offset: 0x0005D83F
		public void SetAnimTimeMax(string bundleName, string objectName, string animName, float animTimeMax)
		{
			this.GetBundleData(bundleName).SetAnimTimeMax(objectName, animName, animTimeMax);
		}

		// Token: 0x04000FFB RID: 4091
		public Dictionary<string, NKMObjectAnimTimeData> m_dicObjectAnimBundleData = new Dictionary<string, NKMObjectAnimTimeData>(StringComparer.OrdinalIgnoreCase);
	}
}

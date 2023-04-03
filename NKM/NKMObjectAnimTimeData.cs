using System;
using System.Collections.Generic;
using Cs.Logging;

namespace NKM
{
	// Token: 0x02000398 RID: 920
	public class NKMObjectAnimTimeData
	{
		// Token: 0x06001797 RID: 6039 RVA: 0x0005F378 File Offset: 0x0005D578
		public float GetAnimTimeMax(string objectName, string animName)
		{
			if (!this.m_dicObjectAnim.ContainsKey(objectName))
			{
				Log.Error("NKMAnimDataManager No Exist Anim: " + objectName + " : " + animName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAnimDataManager.cs", 48);
				return 0f;
			}
			NKMAnimTimeData nkmanimTimeData = this.m_dicObjectAnim[objectName];
			if (nkmanimTimeData == null)
			{
				Log.Error("NKMAnimDataManager No Exist Anim: " + objectName + " : " + animName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAnimDataManager.cs", 55);
				return 0f;
			}
			if (!nkmanimTimeData.m_dicAnimTime.ContainsKey(animName))
			{
				Log.Error("NKMAnimDataManager No Exist Anim: " + objectName + " : " + animName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAnimDataManager.cs", 61);
				return 0f;
			}
			return nkmanimTimeData.m_dicAnimTime[animName];
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0005F428 File Offset: 0x0005D628
		public void SetAnimTimeMax(string objectName, string animName, float animTimeMax)
		{
			NKMAnimTimeData nkmanimTimeData;
			if (!this.m_dicObjectAnim.ContainsKey(objectName))
			{
				nkmanimTimeData = new NKMAnimTimeData();
				this.m_dicObjectAnim.Add(objectName, nkmanimTimeData);
			}
			nkmanimTimeData = this.m_dicObjectAnim[objectName];
			if (!nkmanimTimeData.m_dicAnimTime.ContainsKey(animName))
			{
				nkmanimTimeData.m_dicAnimTime.Add(animName, 0f);
			}
			nkmanimTimeData.m_dicAnimTime[animName] = animTimeMax;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0005F494 File Offset: 0x0005D694
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			string key = "";
			cNKMLua.GetData("animName", ref key);
			if (!this.m_dicObjectAnim.ContainsKey(key))
			{
				NKMAnimTimeData nkmanimTimeData = new NKMAnimTimeData();
				nkmanimTimeData.LoadFromLUA(cNKMLua);
				this.m_dicObjectAnim.Add(key, nkmanimTimeData);
			}
			return true;
		}

		// Token: 0x04000FFA RID: 4090
		public Dictionary<string, NKMAnimTimeData> m_dicObjectAnim = new Dictionary<string, NKMAnimTimeData>();
	}
}

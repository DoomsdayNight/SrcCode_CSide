using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x02000397 RID: 919
	public class NKMAnimTimeData
	{
		// Token: 0x06001795 RID: 6037 RVA: 0x0005F304 File Offset: 0x0005D504
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			int num = 1;
			string key = "";
			float value = 0f;
			while (cNKMLua.OpenTable(num))
			{
				cNKMLua.GetData(1, ref key);
				cNKMLua.GetData(2, ref value);
				if (!this.m_dicAnimTime.ContainsKey(key))
				{
					this.m_dicAnimTime.Add(key, value);
				}
				num++;
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x04000FF9 RID: 4089
		public Dictionary<string, float> m_dicAnimTime = new Dictionary<string, float>();
	}
}

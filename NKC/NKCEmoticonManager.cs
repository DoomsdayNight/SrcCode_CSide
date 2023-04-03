using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x0200080C RID: 2060
	public class NKCEmoticonManager
	{
		// Token: 0x060051BA RID: 20922 RVA: 0x0018CCBA File Offset: 0x0018AEBA
		public static bool HasEmoticon(int id)
		{
			return NKCEmoticonManager.m_hsEmoticonCollection.Contains(id);
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0018CCC7 File Offset: 0x0018AEC7
		public static List<int> GetAllEmoticonID()
		{
			return new List<int>(from e in NKMTempletContainer<NKMEmoticonTemplet>.Values
			select e.m_EmoticonID);
		}

		// Token: 0x04004211 RID: 16913
		public static List<int> m_lstAniPreset = new List<int>();

		// Token: 0x04004212 RID: 16914
		public static List<int> m_lstTextPreset = new List<int>();

		// Token: 0x04004213 RID: 16915
		public static HashSet<int> m_hsEmoticonCollection = new HashSet<int>();

		// Token: 0x04004214 RID: 16916
		public static bool m_bReceivedEmoticonData = false;
	}
}

using System;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003B0 RID: 944
	public class NKMBuffManager
	{
		// Token: 0x060018CA RID: 6346 RVA: 0x00064A28 File Offset: 0x00062C28
		public static void LoadFromLUA()
		{
			string[] fileNames = new string[]
			{
				"LUA_BUFF_TEMPLET",
				"LUA_BUFF_TEMPLET2",
				"LUA_BUFF_TEMPLET3"
			};
			NKMTempletContainer<NKMBuffTemplet>.Load("AB_SCRIPT", fileNames, "m_dicNKMBuffTemplet", new Func<NKMLua, NKMBuffTemplet>(NKMBuffTemplet.LoadFromLUA), (NKMBuffTemplet e) => e.m_BuffStrID);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00064A8F File Offset: 0x00062C8F
		public static NKMBuffTemplet GetBuffTempletByID(short buffID)
		{
			buffID = Math.Abs(buffID);
			return NKMTempletContainer<NKMBuffTemplet>.Find((int)buffID);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00064A9F File Offset: 0x00062C9F
		public static NKMBuffTemplet GetBuffTempletByStrID(string buffStrID)
		{
			return NKMTempletContainer<NKMBuffTemplet>.Find(buffStrID);
		}
	}
}

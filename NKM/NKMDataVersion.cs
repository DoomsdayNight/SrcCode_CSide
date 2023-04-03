using System;

namespace NKM
{
	// Token: 0x020003D5 RID: 981
	public static class NKMDataVersion
	{
		// Token: 0x060019DD RID: 6621 RVA: 0x0006F300 File Offset: 0x0006D500
		public static bool LoadFromLUA()
		{
			using (NKMLua nkmlua = new NKMLua())
			{
				if (nkmlua.LoadCommonPath("AB_SCRIPT", "LUA_DATA_VERSION", true) && nkmlua.OpenTable("m_DataVersion"))
				{
					nkmlua.GetData("DataVersion", ref NKMDataVersion.DataVersion);
					nkmlua.CloseTable();
				}
			}
			return true;
		}

		// Token: 0x040012EA RID: 4842
		public static int DataVersion;

		// Token: 0x040012EB RID: 4843
		public const string FILENAME = "LUA_DATA_VERSION";
	}
}

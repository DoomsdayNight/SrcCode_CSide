using System;

namespace NKM
{
	// Token: 0x0200039A RID: 922
	public class NKMAnimDataManager
	{
		// Token: 0x060017A1 RID: 6049 RVA: 0x0005F65C File Offset: 0x0005D85C
		public static bool LoadFromLUA(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT_ANIM_DATA", fileName, true) && nkmlua.OpenTable("m_dicUnitAnim"))
			{
				NKMAnimDataManager.m_NKMObjectAnimTimeBundleData.LoadFromLUA(nkmlua);
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0005F6A5 File Offset: 0x0005D8A5
		public static float GetAnimTimeMax(string bundleName, string objectName, string animName)
		{
			return NKMAnimDataManager.m_NKMObjectAnimTimeBundleData.GetAnimTimeMax(bundleName, objectName, animName);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0005F6B4 File Offset: 0x0005D8B4
		public static void SetAnimTimeMax(string bundleName, string objectName, string animName, float animTimeMax)
		{
			NKMAnimDataManager.m_NKMObjectAnimTimeBundleData.SetAnimTimeMax(bundleName, objectName, animName, animTimeMax);
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005F6C4 File Offset: 0x0005D8C4
		public static NKMObjectAnimTimeBundleData GetBundleData
		{
			get
			{
				return NKMAnimDataManager.m_NKMObjectAnimTimeBundleData;
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0005F6CC File Offset: 0x0005D8CC
		public static bool LoadFromLUABasePath(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			string text = "";
			if (nkmlua.LoadCommonPathBase("AB_SCRIPT_ANIM_DATA", fileName, true, false, ref text) && nkmlua.OpenTable("m_dicUnitAnim"))
			{
				NKMAnimDataManager.m_NKMObjectAnimTimeBundleData.LoadFromLUA(nkmlua);
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x04000FFC RID: 4092
		public static NKMObjectAnimTimeBundleData m_NKMObjectAnimTimeBundleData = new NKMObjectAnimTimeBundleData();
	}
}

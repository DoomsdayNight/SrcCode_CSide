using System;
using NKC.Templet.Base;
using NKM;

namespace NKC
{
	// Token: 0x0200064B RID: 1611
	public class NKCCollectionTagTemplet : INKCTemplet
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000FBA70 File Offset: 0x000F9C70
		public int Key
		{
			get
			{
				return (int)this.m_TagOrder;
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000FBA78 File Offset: 0x000F9C78
		public static NKCCollectionTagTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKCCollectionTagTemplet nkccollectionTagTemplet = new NKCCollectionTagTemplet();
			if (!(true & cNKMLua.GetData("m_TagOrder", ref nkccollectionTagTemplet.m_TagOrder) & cNKMLua.GetData("m_TagName", ref nkccollectionTagTemplet.m_TagName)))
			{
				return null;
			}
			return nkccollectionTagTemplet;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000FBAB5 File Offset: 0x000F9CB5
		public string GetTagName()
		{
			return NKCStringTable.GetString(this.m_TagName, false);
		}

		// Token: 0x0400315B RID: 12635
		public short m_TagOrder;

		// Token: 0x0400315C RID: 12636
		private string m_TagName;
	}
}

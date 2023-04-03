using System;
using NKM;

namespace NKC.Templet
{
	// Token: 0x0200084F RID: 2127
	public class ItemDropInfo
	{
		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x0019C6B3 File Offset: 0x0019A8B3
		public int ItemID
		{
			get
			{
				return this.m_itemId;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x0019C6BB File Offset: 0x0019A8BB
		public DropContent ContentType
		{
			get
			{
				return this.m_contentType;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06005497 RID: 21655 RVA: 0x0019C6C3 File Offset: 0x0019A8C3
		public int ContentID
		{
			get
			{
				return this.m_contentID;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06005498 RID: 21656 RVA: 0x0019C6CB File Offset: 0x0019A8CB
		// (set) Token: 0x06005499 RID: 21657 RVA: 0x0019C6D3 File Offset: 0x0019A8D3
		public bool Summary
		{
			get
			{
				return this.m_summary;
			}
			set
			{
				this.m_summary = value;
			}
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0019C6DC File Offset: 0x0019A8DC
		public ItemDropInfo(int itemID, DropContent contentType, int contentID)
		{
			this.m_itemId = itemID;
			this.m_contentType = contentType;
			this.m_contentID = contentID;
			this.m_summary = false;
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0019C700 File Offset: 0x0019A900
		public static ItemDropInfo LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCItemDropInfoTemplet.cs", 48))
			{
				return null;
			}
			int itemID = 0;
			DropContent contentType = DropContent.Stage;
			int contentID = 0;
			if (!(true & lua.GetData("ItemID", ref itemID) & lua.GetData<DropContent>("DropContent", ref contentType) & lua.GetData("ID", ref contentID)))
			{
				return null;
			}
			return new ItemDropInfo(itemID, contentType, contentID);
		}

		// Token: 0x0400438F RID: 17295
		private int m_itemId;

		// Token: 0x04004390 RID: 17296
		private DropContent m_contentType;

		// Token: 0x04004391 RID: 17297
		private int m_contentID;

		// Token: 0x04004392 RID: 17298
		private bool m_summary;
	}
}

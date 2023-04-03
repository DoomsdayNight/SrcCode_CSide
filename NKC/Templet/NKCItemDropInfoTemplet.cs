using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000850 RID: 2128
	public class NKCItemDropInfoTemplet : INKMTemplet
	{
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600549C RID: 21660 RVA: 0x0019C75B File Offset: 0x0019A95B
		public int Key
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x0600549D RID: 21661 RVA: 0x0019C763 File Offset: 0x0019A963
		public List<ItemDropInfo> ItemDropInfoList
		{
			get
			{
				if (this.m_DropInfoList != null)
				{
					return this.m_DropInfoList;
				}
				return new List<ItemDropInfo>();
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0019C779 File Offset: 0x0019A979
		public static NKCItemDropInfoTemplet Find(int key)
		{
			return NKMTempletContainer<NKCItemDropInfoTemplet>.Find(key);
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0019C781 File Offset: 0x0019A981
		private NKCItemDropInfoTemplet(IGrouping<int, ItemDropInfo> group)
		{
			this.itemId = group.Key;
			this.m_DropInfoList = group.ToList<ItemDropInfo>();
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0019C7A4 File Offset: 0x0019A9A4
		public static void LoadFromLua()
		{
			(from e in NKMTempletLoader.LoadCommonPath<ItemDropInfo>("AB_SCRIPT", "LUA_ITEM_DROP_LIST", "ItemDropList", new Func<NKMLua, ItemDropInfo>(ItemDropInfo.LoadFromLUA))
			group e by e.ItemID into e
			select new NKCItemDropInfoTemplet(e)).AddToContainer<NKCItemDropInfoTemplet>();
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0019C81E File Offset: 0x0019AA1E
		public void Join()
		{
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x0019C820 File Offset: 0x0019AA20
		public void Validate()
		{
		}

		// Token: 0x04004393 RID: 17299
		private readonly int itemId;

		// Token: 0x04004394 RID: 17300
		private readonly List<ItemDropInfo> m_DropInfoList;
	}
}

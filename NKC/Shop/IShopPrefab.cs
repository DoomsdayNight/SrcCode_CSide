using System;
using NKM.Shop;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE3 RID: 2787
	public interface IShopPrefab
	{
		// Token: 0x06007D42 RID: 32066
		void SetData(ShopItemTemplet productTemplet);

		// Token: 0x06007D43 RID: 32067
		bool IsHideLockObject();
	}
}

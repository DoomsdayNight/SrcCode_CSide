using System;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x0200074B RID: 1867
	public class NKCShopBannerTemplet : INKMTemplet
	{
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x0016564A File Offset: 0x0016384A
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x00165657 File Offset: 0x00163857
		public DateTime m_EventDateStartUTC
		{
			get
			{
				return NKCSynchronizedTime.GetIntervalStartUtc(this.m_DateStrID);
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x00165664 File Offset: 0x00163864
		public DateTime m_EventDateEndUTC
		{
			get
			{
				return NKCSynchronizedTime.GetIntervalEndUtc(this.m_DateStrID);
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06004A86 RID: 19078 RVA: 0x00165671 File Offset: 0x00163871
		public int Key
		{
			get
			{
				return this.IDX;
			}
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00165679 File Offset: 0x00163879
		public static NKCShopBannerTemplet Find(int idx)
		{
			return NKMTempletContainer<NKCShopBannerTemplet>.Find(idx);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x00165684 File Offset: 0x00163884
		public static NKCShopBannerTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopBannerTemplet.cs", 55))
			{
				return null;
			}
			NKCShopBannerTemplet nkcshopBannerTemplet = new NKCShopBannerTemplet();
			lua.GetData("m_ContentsVersionStart", ref nkcshopBannerTemplet.m_ContentsVersionStart);
			lua.GetData("m_ContentsVersionEnd", ref nkcshopBannerTemplet.m_ContentsVersionEnd);
			bool flag = true & lua.GetData("IDX", ref nkcshopBannerTemplet.IDX) & lua.GetData("m_Enable", ref nkcshopBannerTemplet.m_Enable);
			lua.GetData("m_ShopHome_BannerImage", ref nkcshopBannerTemplet.m_ShopHome_BannerImage);
			lua.GetData("m_ShopHome_BannerPrefab", ref nkcshopBannerTemplet.m_ShopHome_BannerPrefab);
			bool flag2 = flag & (!string.IsNullOrEmpty(nkcshopBannerTemplet.m_ShopHome_BannerImage) || !string.IsNullOrEmpty(nkcshopBannerTemplet.m_ShopHome_BannerPrefab)) & lua.GetData("m_TabID", ref nkcshopBannerTemplet.m_TabID) & lua.GetData("m_TabSubIndex", ref nkcshopBannerTemplet.m_TabSubIndex);
			lua.GetData("m_DateStrID", ref nkcshopBannerTemplet.m_DateStrID);
			lua.GetData("m_ProductID", ref nkcshopBannerTemplet.m_ProductID);
			lua.GetData<SHOP_RECOMMEND_COND>("m_DisplayCond", ref nkcshopBannerTemplet.m_DisplayCond);
			lua.GetData("m_DisplayCondValue", ref nkcshopBannerTemplet.m_DisplayCondValue);
			lua.GetData("m_OpenTag", ref nkcshopBannerTemplet.m_OpenTag);
			if (!flag2)
			{
				return null;
			}
			return nkcshopBannerTemplet;
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x001657B7 File Offset: 0x001639B7
		public void Join()
		{
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x001657BC File Offset: 0x001639BC
		public void Validate()
		{
			if (ShopTabTemplet.Find(this.m_TabID, this.m_TabSubIndex) == null)
			{
				NKMTempletError.Add(string.Format("[BannerTemplet] tabID / subIndex 에 해당하는 ShopTabTemplet 이 없음. tabId:{0} subIndex:{1}", this.m_TabID, this.m_TabSubIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopBannerTemplet.cs", 96);
			}
			if (this.m_ProductID > 0)
			{
				if (ShopItemTemplet.Find(this.m_ProductID) == null)
				{
					NKMTempletError.Add(string.Format("[BannerTemplet] m_ProductID 에 해당하는 상품이 없음. m_ProductID :{0}", this.m_ProductID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopBannerTemplet.cs", 103);
				}
				if (ShopItemTemplet.Find(this.m_ProductID).m_TabID != this.m_TabID || ShopItemTemplet.Find(this.m_ProductID).m_TabSubIndex != this.m_TabSubIndex)
				{
					NKMTempletError.Add(string.Format("[BannerTemplet] 상품의 탭 정보와 일치하지 않음. m_ProductID :{0} tabId:{1} subIndex:{2}", this.m_ProductID, this.m_TabID, this.m_TabSubIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopBannerTemplet.cs", 108);
				}
			}
			if (NKMIntervalTemplet.Find(this.m_DateStrID) == null)
			{
				NKMTempletError.Add(string.Format("NKCShopRecommendTemplet(IDX {0}) : Interval Templet {1} 존재하지 않음", this.IDX, this.m_DateStrID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopBannerTemplet.cs", 114);
			}
		}

		// Token: 0x0400395A RID: 14682
		public string m_ContentsVersionStart = "";

		// Token: 0x0400395B RID: 14683
		public string m_ContentsVersionEnd = "";

		// Token: 0x0400395C RID: 14684
		public int IDX;

		// Token: 0x0400395D RID: 14685
		public bool m_Enable;

		// Token: 0x0400395E RID: 14686
		public string m_ShopHome_BannerImage;

		// Token: 0x0400395F RID: 14687
		public string m_ShopHome_BannerPrefab;

		// Token: 0x04003960 RID: 14688
		public string m_TabID;

		// Token: 0x04003961 RID: 14689
		public int m_TabSubIndex;

		// Token: 0x04003962 RID: 14690
		public int m_ProductID;

		// Token: 0x04003963 RID: 14691
		public SHOP_RECOMMEND_COND m_DisplayCond;

		// Token: 0x04003964 RID: 14692
		public string m_DisplayCondValue;

		// Token: 0x04003965 RID: 14693
		public string m_DateStrID;

		// Token: 0x04003966 RID: 14694
		public string m_OpenTag;
	}
}

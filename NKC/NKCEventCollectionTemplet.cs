using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x02000744 RID: 1860
	public class NKCEventCollectionTemplet : INKMTemplet
	{
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x001651F1 File Offset: 0x001633F1
		public int Key
		{
			get
			{
				return this.m_Idx;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x001651F9 File Offset: 0x001633F9
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x00165206 File Offset: 0x00163406
		public static NKCEventCollectionTemplet Find(int index)
		{
			return NKMTempletContainer<NKCEventCollectionTemplet>.Find(index);
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x00165210 File Offset: 0x00163410
		public static NKCEventCollectionTemplet LoadFromLUA(NKMLua lua)
		{
			NKCEventCollectionTemplet nkceventCollectionTemplet = new NKCEventCollectionTemplet();
			bool flag = true;
			flag &= lua.GetData("EventID", ref nkceventCollectionTemplet.m_Idx);
			flag &= lua.GetData("OpenTag", ref nkceventCollectionTemplet.m_OpenTag);
			flag &= lua.GetData("DateStrID", ref nkceventCollectionTemplet.DateStrID);
			flag &= lua.GetData("EventBannerStrID", ref nkceventCollectionTemplet.EventBannerStrID);
			flag &= lua.GetData("EventPrefabID", ref nkceventCollectionTemplet.EventPrefabID);
			flag &= lua.GetData("BgmAssetID", ref nkceventCollectionTemplet.BgmAssetID);
			flag &= lua.GetData("BgmVolume", ref nkceventCollectionTemplet.BgmVolume);
			flag &= lua.GetData("EventContractID", ref nkceventCollectionTemplet.EventContractID);
			flag &= lua.GetData("EventContractPrefabID", ref nkceventCollectionTemplet.EventContractPrefabID);
			flag &= lua.GetData("EventMissionPrefabID", ref nkceventCollectionTemplet.EventMissionPrefabID);
			if (lua.OpenTable("EventMissionTabID"))
			{
				nkceventCollectionTemplet.EventMissionTabID = new HashSet<int>();
				int num = 1;
				int item = 0;
				while (lua.GetData(num, ref item))
				{
					nkceventCollectionTemplet.EventMissionTabID.Add(item);
					num++;
				}
				lua.CloseTable();
			}
			flag &= lua.GetData("EventMergePrefabID", ref nkceventCollectionTemplet.EventMergePrefabID);
			flag &= lua.GetData("CollectionMergeID", ref nkceventCollectionTemplet.CollectionMergeID);
			flag &= lua.GetData("EventCollectionPrefabID", ref nkceventCollectionTemplet.EventCollectionPrefabID);
			flag &= lua.GetData("EventCollectionGroupID", ref nkceventCollectionTemplet.EventCollectionGroupID);
			flag &= lua.GetData("EventShopPrefabID", ref nkceventCollectionTemplet.EventShopPrefabID);
			flag &= lua.GetData("ShopShortCutType", ref nkceventCollectionTemplet.ShopShortCutType);
			if (!(flag & lua.GetData("ShopShortCut", ref nkceventCollectionTemplet.ShopShortCut)))
			{
				return null;
			}
			return nkceventCollectionTemplet;
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x001653C0 File Offset: 0x001635C0
		public void Join()
		{
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x001653C2 File Offset: 0x001635C2
		public void Validate()
		{
		}

		// Token: 0x0400391E RID: 14622
		public int m_Idx;

		// Token: 0x0400391F RID: 14623
		public string m_OpenTag;

		// Token: 0x04003920 RID: 14624
		public string DateStrID;

		// Token: 0x04003921 RID: 14625
		public string EventBannerStrID;

		// Token: 0x04003922 RID: 14626
		public string EventBannerTitleStrID;

		// Token: 0x04003923 RID: 14627
		public string EventPrefabID;

		// Token: 0x04003924 RID: 14628
		public string BgmAssetID;

		// Token: 0x04003925 RID: 14629
		public int BgmVolume;

		// Token: 0x04003926 RID: 14630
		public string EventContractPrefabID;

		// Token: 0x04003927 RID: 14631
		public int EventContractID;

		// Token: 0x04003928 RID: 14632
		public string EventMissionPrefabID;

		// Token: 0x04003929 RID: 14633
		public HashSet<int> EventMissionTabID;

		// Token: 0x0400392A RID: 14634
		public string EventMergePrefabID;

		// Token: 0x0400392B RID: 14635
		public int CollectionMergeID;

		// Token: 0x0400392C RID: 14636
		public string EventCollectionPrefabID;

		// Token: 0x0400392D RID: 14637
		public int EventCollectionGroupID;

		// Token: 0x0400392E RID: 14638
		public string EventShopPrefabID;

		// Token: 0x0400392F RID: 14639
		public string ShopShortCutType;

		// Token: 0x04003930 RID: 14640
		public string ShopShortCut;
	}
}

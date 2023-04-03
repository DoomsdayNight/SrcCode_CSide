using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x0200085B RID: 2139
	public class NKCThemeGroupTemplet : INKMTemplet
	{
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x0019D6B7 File Offset: 0x0019B8B7
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.OpenTag);
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060054EC RID: 21740 RVA: 0x0019D6C4 File Offset: 0x0019B8C4
		public int Key
		{
			get
			{
				return this.ThemeGroupID;
			}
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0019D6CC File Offset: 0x0019B8CC
		public static void Load()
		{
			if (!NKMTempletContainer<NKCThemeGroupTemplet>.HasValue())
			{
				NKMTempletContainer<NKCThemeGroupTemplet>.Load("ab_script", "LUA_THEME_GROUP_TEMPLET", "THEME_GROUP_TEMPLET", new Func<NKMLua, NKCThemeGroupTemplet>(NKCThemeGroupTemplet.LoadFromLUA));
			}
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0019D6F5 File Offset: 0x0019B8F5
		public static NKCThemeGroupTemplet Find(int id)
		{
			NKCThemeGroupTemplet.Load();
			return NKMTempletContainer<NKCThemeGroupTemplet>.Find(id);
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0019D704 File Offset: 0x0019B904
		public static NKCThemeGroupTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCThemeGroupTemplet.cs", 38))
			{
				return null;
			}
			NKCThemeGroupTemplet nkcthemeGroupTemplet = new NKCThemeGroupTemplet();
			bool flag = true & cNKMLua.GetData("ThemeGroupID", ref nkcthemeGroupTemplet.ThemeGroupID);
			cNKMLua.GetData("OpenTag", ref nkcthemeGroupTemplet.OpenTag);
			if (!(flag & cNKMLua.GetData("GroupNumber", ref nkcthemeGroupTemplet.GroupNumber) & cNKMLua.GetData("GroupID", nkcthemeGroupTemplet.GroupID) & cNKMLua.GetData("GroupStringKey", ref nkcthemeGroupTemplet.GroupStringKey) & cNKMLua.GetData("GroupIconName", ref nkcthemeGroupTemplet.GroupIconName)))
			{
				return null;
			}
			return nkcthemeGroupTemplet;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x0019D79A File Offset: 0x0019B99A
		public void Join()
		{
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0019D79C File Offset: 0x0019B99C
		public void Validate()
		{
		}

		// Token: 0x040043D5 RID: 17365
		public int ThemeGroupID;

		// Token: 0x040043D6 RID: 17366
		public string OpenTag;

		// Token: 0x040043D7 RID: 17367
		public int GroupNumber;

		// Token: 0x040043D8 RID: 17368
		public HashSet<string> GroupID = new HashSet<string>();

		// Token: 0x040043D9 RID: 17369
		public string GroupStringKey;

		// Token: 0x040043DA RID: 17370
		public string GroupIconName;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200038B RID: 907
	public sealed class NKMPotentialOptionGroupTemplet : INKMTemplet
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x0005E1FE File Offset: 0x0005C3FE
		private NKMPotentialOptionGroupTemplet(IGrouping<int, NKMPotentialOptionTemplet> group)
		{
			this.groupId = group.Key;
			this.optionList = group.ToList<NKMPotentialOptionTemplet>();
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0005E21E File Offset: 0x0005C41E
		public static bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened("EQUIP_POTENTIAL");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0005E22A File Offset: 0x0005C42A
		public static IEnumerable<NKMPotentialOptionGroupTemplet> Values
		{
			get
			{
				return NKMTempletContainer<NKMPotentialOptionGroupTemplet>.Values;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0005E231 File Offset: 0x0005C431
		public int Key
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0005E239 File Offset: 0x0005C439
		public IReadOnlyList<NKMPotentialOptionTemplet> OptionList
		{
			get
			{
				return this.optionList;
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0005E244 File Offset: 0x0005C444
		public static void LoadFromLua()
		{
			(from e in NKMTempletLoader.LoadCommonPath<NKMPotentialOptionTemplet>("AB_SCRIPT", "LUA_ITEM_EQUIP_POTENTIAL_OPTION", "ITEM_EQUIP_POTENTIAL_OPTION", new Func<NKMLua, NKMPotentialOptionTemplet>(NKMPotentialOptionTemplet.LoadFromLUA))
			group e by e.groupId into e
			select new NKMPotentialOptionGroupTemplet(e)).AddToContainer<NKMPotentialOptionGroupTemplet>();
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0005E2BE File Offset: 0x0005C4BE
		public static NKMPotentialOptionGroupTemplet Find(int key)
		{
			return NKMTempletContainer<NKMPotentialOptionGroupTemplet>.Find(key);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0005E2C6 File Offset: 0x0005C4C6
		public void Join()
		{
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005E2C8 File Offset: 0x0005C4C8
		public void Validate()
		{
			if (!NKMPotentialOptionGroupTemplet.EnableByTag)
			{
				return;
			}
			if (!this.optionList.Any<NKMPotentialOptionTemplet>())
			{
				NKMTempletError.Add("[NKMPotentialOptionGroup] OptionList is Empty", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Item/NKMPotentialOptionGroupTemplet.cs", 49);
			}
			foreach (NKMPotentialOptionTemplet nkmpotentialOptionTemplet in this.optionList)
			{
				nkmpotentialOptionTemplet.Validate();
			}
		}

		// Token: 0x04000FB9 RID: 4025
		private const string OpenTag = "EQUIP_POTENTIAL";

		// Token: 0x04000FBA RID: 4026
		private readonly int groupId;

		// Token: 0x04000FBB RID: 4027
		private readonly List<NKMPotentialOptionTemplet> optionList;
	}
}

using System;
using System.Collections.Generic;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000469 RID: 1129
	public sealed class NKMCommandModulePassiveTemplet : INKMTemplet
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00091707 File Offset: 0x0008F907
		public int Key
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x0009170F File Offset: 0x0008F90F
		public int PassiveGroupId
		{
			get
			{
				return this.cmdPassiveGroupId;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x00091717 File Offset: 0x0008F917
		public int StatGroupId
		{
			get
			{
				return this.statGroupId;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0009171F File Offset: 0x0008F91F
		public int Ratio
		{
			get
			{
				return this.ratio;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00091727 File Offset: 0x0008F927
		public HashSet<NKM_UNIT_STYLE_TYPE> StyleTypes
		{
			get
			{
				return this.allowStyleTypes;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0009172F File Offset: 0x0008F92F
		public HashSet<NKM_UNIT_ROLE_TYPE> RoleTypes
		{
			get
			{
				return this.allowRoleTypes;
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00091738 File Offset: 0x0008F938
		public static NKMCommandModulePassiveTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 653))
			{
				return null;
			}
			NKMCommandModulePassiveTemplet nkmcommandModulePassiveTemplet = new NKMCommandModulePassiveTemplet
			{
				id = lua.GetInt32("ID"),
				cmdPassiveGroupId = lua.GetInt32("CMDPassiveGroupID"),
				ratio = lua.GetInt32("Ratio"),
				statGroupId = lua.GetInt32("StatGroupID")
			};
			nkmcommandModulePassiveTemplet.allowStyleTypes = new HashSet<NKM_UNIT_STYLE_TYPE>();
			if (lua.OpenTable("ListRangeSonAllowStyleType"))
			{
				int num = 1;
				NKM_UNIT_STYLE_TYPE item;
				while (lua.GetDataEnum<NKM_UNIT_STYLE_TYPE>(num, out item))
				{
					nkmcommandModulePassiveTemplet.allowStyleTypes.Add(item);
					num++;
				}
				lua.CloseTable();
			}
			nkmcommandModulePassiveTemplet.allowRoleTypes = new HashSet<NKM_UNIT_ROLE_TYPE>();
			if (lua.OpenTable("ListRangeSonAllowRoleType"))
			{
				int num2 = 1;
				NKM_UNIT_ROLE_TYPE item2 = NKM_UNIT_ROLE_TYPE.NURT_INVALID;
				while (lua.GetData<NKM_UNIT_ROLE_TYPE>(num2, ref item2))
				{
					nkmcommandModulePassiveTemplet.allowRoleTypes.Add(item2);
					num2++;
				}
				lua.CloseTable();
			}
			return nkmcommandModulePassiveTemplet;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00091826 File Offset: 0x0008FA26
		public void Join()
		{
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00091828 File Offset: 0x0008FA28
		public void Validate()
		{
			if (this.ratio < 0)
			{
				NKMTempletError.Add(string.Format("[NKMCommandModulePassive:{0}] ratio���� �̻���. ratio:{1}", this.id, this.ratio), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 706);
			}
			if (NKMShipModuleGroupTemplet.GetStatListsByGroupId(this.statGroupId) == null)
			{
				NKMTempletError.Add(string.Format("[NKMCommandModulePassive:{0}] statGroupId���� CommandModuleRandomStatTemplet�� ����. statGroupId:{1}", this.id, this.statGroupId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMShipManager.cs", 711);
			}
		}

		// Token: 0x04001F2F RID: 7983
		private int id;

		// Token: 0x04001F30 RID: 7984
		private int cmdPassiveGroupId;

		// Token: 0x04001F31 RID: 7985
		private int ratio;

		// Token: 0x04001F32 RID: 7986
		private HashSet<NKM_UNIT_STYLE_TYPE> allowStyleTypes;

		// Token: 0x04001F33 RID: 7987
		private HashSet<NKM_UNIT_ROLE_TYPE> allowRoleTypes;

		// Token: 0x04001F34 RID: 7988
		private int statGroupId;
	}
}

using System;
using System.Collections.Generic;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000521 RID: 1313
	public class NKMTacticUpdateTemplet : INKMTemplet
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000C0ED8 File Offset: 0x000BF0D8
		public int Key
		{
			get
			{
				return this.m_TacticPhase;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000C0EE0 File Offset: 0x000BF0E0
		public static IEnumerable<NKMTacticUpdateTemplet> Values
		{
			get
			{
				return NKMTempletContainer<NKMTacticUpdateTemplet>.Values;
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000C0EE7 File Offset: 0x000BF0E7
		public static NKMTacticUpdateTemplet Find(int key)
		{
			return NKMTempletContainer<NKMTacticUpdateTemplet>.Find(key);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000C0EF0 File Offset: 0x000BF0F0
		public static NKMTacticUpdateTemplet LoadFromLua(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMTacticUpdateTemplet.cs", 32))
			{
				return null;
			}
			NKMTacticUpdateTemplet nkmtacticUpdateTemplet = new NKMTacticUpdateTemplet();
			if (!(lua.GetData("TacticPhase", ref nkmtacticUpdateTemplet.m_TacticPhase) & lua.GetDataEnum<NKM_STAT_TYPE>("StatType", out nkmtacticUpdateTemplet.m_StatType) & lua.GetData("StatValue", ref nkmtacticUpdateTemplet.m_StatValue) & lua.GetData("StatIcon", ref nkmtacticUpdateTemplet.m_StatIcon) & lua.GetData("StringKey", ref nkmtacticUpdateTemplet.m_StringKey)))
			{
				return null;
			}
			return nkmtacticUpdateTemplet;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000C0F72 File Offset: 0x000BF172
		public virtual void Join()
		{
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000C0F74 File Offset: 0x000BF174
		public virtual void Validate()
		{
			if (this.m_TacticPhase < 1 || this.m_TacticPhase > 6)
			{
				NKMTempletError.Add(string.Format("[NKMTacticUpdateTemplet] Invalid TacticPhase:{0}", this.m_TacticPhase), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMTacticUpdateTemplet.cs", 57);
			}
			if (this.m_StatValue < -10000f || this.m_StatValue > 10000f)
			{
				NKMTempletError.Add(string.Format("[NKMTacticUpdateTemplet:{0}] Invalid StatValue:{1}", this.m_TacticPhase, this.m_StatValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMTacticUpdateTemplet.cs", 62);
			}
		}

		// Token: 0x040026CA RID: 9930
		public const int MaxTacticLevel = 6;

		// Token: 0x040026CB RID: 9931
		public int m_TacticPhase;

		// Token: 0x040026CC RID: 9932
		public NKM_STAT_TYPE m_StatType;

		// Token: 0x040026CD RID: 9933
		public float m_StatValue;

		// Token: 0x040026CE RID: 9934
		public string m_StatIcon;

		// Token: 0x040026CF RID: 9935
		public string m_StringKey;
	}
}

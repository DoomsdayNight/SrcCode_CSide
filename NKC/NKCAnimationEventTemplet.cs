using System;
using NKM;

namespace NKC
{
	// Token: 0x02000740 RID: 1856
	public class NKCAnimationEventTemplet
	{
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x00164E81 File Offset: 0x00163081
		public string Key
		{
			get
			{
				return this.m_AniEventStrID;
			}
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x00164E8C File Offset: 0x0016308C
		public static NKCAnimationEventTemplet LoadFromLua(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCAnimationEventTemplet.cs", 51))
			{
				return null;
			}
			NKCAnimationEventTemplet nkcanimationEventTemplet = new NKCAnimationEventTemplet();
			bool flag = true & cNKMLua.GetData("m_AniEventStrID", ref nkcanimationEventTemplet.m_AniEventStrID) & cNKMLua.GetData("m_StartTime", ref nkcanimationEventTemplet.m_StartTime) & cNKMLua.GetData<AnimationEventType>("m_AniEventType", ref nkcanimationEventTemplet.m_AniEventType);
			cNKMLua.GetData("m_StrValue", ref nkcanimationEventTemplet.m_StrValue);
			cNKMLua.GetData("m_FloatValue", ref nkcanimationEventTemplet.m_FloatValue);
			cNKMLua.GetData("m_BoolValue", ref nkcanimationEventTemplet.m_BoolValue);
			cNKMLua.GetData("m_FloatValue2", ref nkcanimationEventTemplet.m_FloatValue2);
			if (!flag)
			{
				return null;
			}
			return nkcanimationEventTemplet;
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x00164F34 File Offset: 0x00163134
		public void Join()
		{
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x00164F36 File Offset: 0x00163136
		public void Validate()
		{
		}

		// Token: 0x04003901 RID: 14593
		public string m_AniEventStrID = string.Empty;

		// Token: 0x04003902 RID: 14594
		public float m_StartTime;

		// Token: 0x04003903 RID: 14595
		public AnimationEventType m_AniEventType;

		// Token: 0x04003904 RID: 14596
		public string m_StrValue = string.Empty;

		// Token: 0x04003905 RID: 14597
		public float m_FloatValue;

		// Token: 0x04003906 RID: 14598
		public bool m_BoolValue;

		// Token: 0x04003907 RID: 14599
		public float m_FloatValue2;
	}
}

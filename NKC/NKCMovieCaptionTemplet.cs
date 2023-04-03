using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x02000749 RID: 1865
	public class NKCMovieCaptionTemplet : INKMTemplet
	{
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x00165591 File Offset: 0x00163791
		public int Key
		{
			get
			{
				return this.m_Idx;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x00165599 File Offset: 0x00163799
		public static IEnumerable<NKCMovieCaptionTemplet> Values
		{
			get
			{
				return NKMTempletContainer<NKCMovieCaptionTemplet>.Values;
			}
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x001655A0 File Offset: 0x001637A0
		public static NKCMovieCaptionTemplet LoadFromLUA(NKMLua lua)
		{
			NKCMovieCaptionTemplet nkcmovieCaptionTemplet = new NKCMovieCaptionTemplet();
			bool flag = true & lua.GetData("m_Idx", ref nkcmovieCaptionTemplet.m_Idx);
			string nationalPostfix = NKCStringTable.GetNationalPostfix(NKCStringTable.GetNationalCode());
			bool flag2 = flag & lua.GetData("m_Caption" + nationalPostfix, ref nkcmovieCaptionTemplet.m_Caption) & lua.GetData("m_StringKey", ref nkcmovieCaptionTemplet.m_StringKey) & lua.GetData("m_StartSecond", ref nkcmovieCaptionTemplet.m_StartSecond);
			if (!lua.GetData("m_ShowSecond", ref nkcmovieCaptionTemplet.m_ShowSecond))
			{
				nkcmovieCaptionTemplet.m_ShowSecond = 3;
			}
			lua.GetData("m_bHideBackground", ref nkcmovieCaptionTemplet.m_bHideBackground);
			if (!flag2)
			{
				return null;
			}
			return nkcmovieCaptionTemplet;
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x0016563E File Offset: 0x0016383E
		public void Join()
		{
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00165640 File Offset: 0x00163840
		public void Validate()
		{
		}

		// Token: 0x0400394F RID: 14671
		public int m_Idx;

		// Token: 0x04003950 RID: 14672
		public string m_Caption;

		// Token: 0x04003951 RID: 14673
		public string m_StringKey;

		// Token: 0x04003952 RID: 14674
		public int m_StartSecond;

		// Token: 0x04003953 RID: 14675
		public int m_ShowSecond;

		// Token: 0x04003954 RID: 14676
		public bool m_bHideBackground;

		// Token: 0x04003955 RID: 14677
		private const int DEFAULT_DISPLAY_CAPTION_TIME = 3;
	}
}

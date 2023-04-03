using System;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x02000743 RID: 1859
	public class NKCBackgroundTemplet : INKMTemplet
	{
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x00165174 File Offset: 0x00163374
		public int Key
		{
			get
			{
				return this.m_ItemMiscID;
			}
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x0016517C File Offset: 0x0016337C
		public static NKCBackgroundTemplet Find(int id)
		{
			return NKMTempletContainer<NKCBackgroundTemplet>.Find(id);
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00165184 File Offset: 0x00163384
		public static NKCBackgroundTemplet LoadFromLUA(NKMLua lua)
		{
			NKCBackgroundTemplet nkcbackgroundTemplet = new NKCBackgroundTemplet();
			bool flag = true & lua.GetData("m_ItemMiscID", ref nkcbackgroundTemplet.m_ItemMiscID) & lua.GetData("m_Background_Prefab", ref nkcbackgroundTemplet.m_Background_Prefab) & lua.GetData("m_Background_Music", ref nkcbackgroundTemplet.m_Background_Music);
			lua.GetData("m_bBackground_CamMove", ref nkcbackgroundTemplet.m_bBackground_CamMove);
			if (!flag)
			{
				return null;
			}
			return nkcbackgroundTemplet;
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x001651E5 File Offset: 0x001633E5
		public void Join()
		{
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x001651E7 File Offset: 0x001633E7
		public void Validate()
		{
		}

		// Token: 0x0400391A RID: 14618
		public int m_ItemMiscID;

		// Token: 0x0400391B RID: 14619
		public string m_Background_Prefab;

		// Token: 0x0400391C RID: 14620
		public string m_Background_Music;

		// Token: 0x0400391D RID: 14621
		public bool m_bBackground_CamMove;
	}
}

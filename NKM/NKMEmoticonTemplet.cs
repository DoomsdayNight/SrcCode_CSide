using System;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000519 RID: 1305
	public sealed class NKMEmoticonTemplet : INKMTemplet
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x000C075D File Offset: 0x000BE95D
		public int Key
		{
			get
			{
				return this.m_EmoticonID;
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000C0768 File Offset: 0x000BE968
		public static NKMEmoticonTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMEmoticonTemplet nkmemoticonTemplet = new NKMEmoticonTemplet();
			cNKMLua.GetData("m_EmoticonID", ref nkmemoticonTemplet.m_EmoticonID);
			cNKMLua.GetData("m_EmoticonStrID", ref nkmemoticonTemplet.m_EmoticonStrID);
			cNKMLua.GetData<NKM_EMOTICON_TYPE>("m_EmoticonType", ref nkmemoticonTemplet.m_EmoticonType);
			cNKMLua.GetData<NKM_EMOTICON_GRADE>("m_EmoticonGrade", ref nkmemoticonTemplet.m_EmoticonGrade);
			cNKMLua.GetData("m_EmoticonName", ref nkmemoticonTemplet.m_EmoticonName);
			cNKMLua.GetData("m_EmoticonDesc", ref nkmemoticonTemplet.m_EmoticonDesc);
			cNKMLua.GetData("m_EmoticonAssetName", ref nkmemoticonTemplet.m_EmoticonAssetName);
			cNKMLua.GetData("m_EmoticonaAnimationName", ref nkmemoticonTemplet.m_EmoticonaAnimationName);
			cNKMLua.GetData("m_EmoticonaIconName", ref nkmemoticonTemplet.m_EmoticonaIconName);
			cNKMLua.GetData("m_EmoticonSound", ref nkmemoticonTemplet.m_EmoticonSound);
			return nkmemoticonTemplet;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000C0830 File Offset: 0x000BEA30
		public static NKMEmoticonTemplet Find(int key)
		{
			return NKMTempletContainer<NKMEmoticonTemplet>.Find(key);
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000C0838 File Offset: 0x000BEA38
		public void Join()
		{
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000C083A File Offset: 0x000BEA3A
		public void Validate()
		{
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000C083C File Offset: 0x000BEA3C
		public string GetEmoticonName()
		{
			return NKCStringTable.GetString(this.m_EmoticonName, false);
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000C084A File Offset: 0x000BEA4A
		public string GetEmoticonDesc()
		{
			return NKCStringTable.GetString(this.m_EmoticonDesc, false);
		}

		// Token: 0x040026A0 RID: 9888
		public static readonly NKMEmoticonTemplet Invalid = new NKMEmoticonTemplet
		{
			m_EmoticonID = 0
		};

		// Token: 0x040026A1 RID: 9889
		public int m_EmoticonID;

		// Token: 0x040026A2 RID: 9890
		public string m_EmoticonStrID;

		// Token: 0x040026A3 RID: 9891
		public NKM_EMOTICON_TYPE m_EmoticonType;

		// Token: 0x040026A4 RID: 9892
		public NKM_EMOTICON_GRADE m_EmoticonGrade;

		// Token: 0x040026A5 RID: 9893
		public string m_EmoticonName;

		// Token: 0x040026A6 RID: 9894
		public string m_EmoticonDesc;

		// Token: 0x040026A7 RID: 9895
		public string m_EmoticonAssetName;

		// Token: 0x040026A8 RID: 9896
		public string m_EmoticonaAnimationName;

		// Token: 0x040026A9 RID: 9897
		public string m_EmoticonaIconName;

		// Token: 0x040026AA RID: 9898
		public string m_EmoticonSound;
	}
}

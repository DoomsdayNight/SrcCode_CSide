using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;
using NKM.Templet.Office;

namespace NKC
{
	// Token: 0x02000742 RID: 1858
	public class NKCBGMInfoTemplet : INKMTemplet
	{
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x00164F56 File Offset: 0x00163156
		public int Key
		{
			get
			{
				return this.m_Idx;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004A58 RID: 19032 RVA: 0x00164F5E File Offset: 0x0016315E
		public float BGMVolume
		{
			get
			{
				return (float)this.m_BgmVolume * 0.01f;
			}
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00164F70 File Offset: 0x00163170
		public static NKCBGMInfoTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCBGMInfoTemplet.cs", 35))
			{
				return null;
			}
			NKCBGMInfoTemplet nkcbgminfoTemplet = new NKCBGMInfoTemplet();
			bool flag = true & cNKMLua.GetData("IDX", ref nkcbgminfoTemplet.m_Idx) & cNKMLua.GetData("OrderIdx", ref nkcbgminfoTemplet.m_OrderIdx) & cNKMLua.GetData("BgmID", ref nkcbgminfoTemplet.m_BgmID) & cNKMLua.GetData("BgmNameStringID", ref nkcbgminfoTemplet.m_BgmNameStringID) & cNKMLua.GetData("BgmAssetID", ref nkcbgminfoTemplet.m_BgmAssetID) & cNKMLua.GetData("BgmVolume", ref nkcbgminfoTemplet.m_BgmVolume) & cNKMLua.GetData("BgmCoverIMGID", ref nkcbgminfoTemplet.m_BgmCoverIMGID);
			cNKMLua.GetData<NKC_BGM_COND>("UnlockCond", ref nkcbgminfoTemplet.m_UnlockCond);
			nkcbgminfoTemplet.m_UnlockCondValue1 = new List<int>();
			cNKMLua.GetData("UnlockCondValue1", nkcbgminfoTemplet.m_UnlockCondValue1);
			cNKMLua.GetData("UnlockCondValue2", out nkcbgminfoTemplet.m_UnlockCondValue2, 0);
			cNKMLua.GetData("AndTrue", out nkcbgminfoTemplet.bAllCollecte, false);
			if (!flag)
			{
				return null;
			}
			return nkcbgminfoTemplet;
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0016506D File Offset: 0x0016326D
		public static NKCBGMInfoTemplet Find(string bgmID)
		{
			return NKMTempletContainer<NKCBGMInfoTemplet>.Find((NKCBGMInfoTemplet x) => x.m_BgmID == bgmID);
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0016508B File Offset: 0x0016328B
		public void Join()
		{
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x00165090 File Offset: 0x00163290
		public void Validate()
		{
			if (this.m_UnlockCond != NKC_BGM_COND.STAGE_CLEAR_TOTAL_CNT && this.m_UnlockCondValue2 != 0)
			{
				NKMTempletError.Add(string.Format("[NKCBGMInfoTemplet:{0}] {1}는 m_UnlockCondValue1 : {2}를 사용하지 않습니다.", this.Key, this.m_UnlockCond, this.m_UnlockCondValue2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCBGMInfoTemplet.cs", 67);
			}
			if (this.m_UnlockCond == NKC_BGM_COND.COLLECT_ITEM_MISC)
			{
				foreach (int num in this.m_UnlockCondValue1)
				{
					if (NKMTempletContainer<NKMOfficeInteriorTemplet>.Find(num) == null)
					{
						NKMTempletError.Add(string.Format("[NKCBGMInfoTemplet:{0}] {1}는 NKMOfficeInteriorTemplet : {2}이 존재 하지 않습니다.", this.Key, this.m_UnlockCond, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCBGMInfoTemplet.cs", 76);
					}
				}
			}
		}

		// Token: 0x0400390F RID: 14607
		public int m_Idx;

		// Token: 0x04003910 RID: 14608
		public int m_OrderIdx;

		// Token: 0x04003911 RID: 14609
		public string m_BgmID;

		// Token: 0x04003912 RID: 14610
		public string m_BgmNameStringID;

		// Token: 0x04003913 RID: 14611
		public string m_BgmAssetID;

		// Token: 0x04003914 RID: 14612
		private int m_BgmVolume;

		// Token: 0x04003915 RID: 14613
		public string m_BgmCoverIMGID;

		// Token: 0x04003916 RID: 14614
		public NKC_BGM_COND m_UnlockCond;

		// Token: 0x04003917 RID: 14615
		public List<int> m_UnlockCondValue1;

		// Token: 0x04003918 RID: 14616
		public int m_UnlockCondValue2;

		// Token: 0x04003919 RID: 14617
		public bool bAllCollecte;
	}
}

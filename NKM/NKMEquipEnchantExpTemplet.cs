using System;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000385 RID: 901
	public class NKMEquipEnchantExpTemplet : INKMTemplet
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0005BA67 File Offset: 0x00059C67
		public int Key
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0005BA70 File Offset: 0x00059C70
		public static NKMEquipEnchantExpTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMEquipEnchantExpTemplet nkmequipEnchantExpTemplet = new NKMEquipEnchantExpTemplet();
			cNKMLua.GetData("m_EquipTier", ref nkmequipEnchantExpTemplet.m_EquipTier);
			cNKMLua.GetData("m_EquipEnchantLevel", ref nkmequipEnchantExpTemplet.m_EquipEnchantLevel);
			cNKMLua.GetData("m_ReqLevelupEXP_N", ref nkmequipEnchantExpTemplet.m_ReqLevelupExp_N);
			cNKMLua.GetData("m_ReqLevelupEXP_R", ref nkmequipEnchantExpTemplet.m_ReqLevelupExp_R);
			cNKMLua.GetData("m_ReqLevelupEXP_SR", ref nkmequipEnchantExpTemplet.m_ReqLevelupExp_SR);
			cNKMLua.GetData("m_ReqLevelupEXP_SSR", ref nkmequipEnchantExpTemplet.m_ReqLevelupExp_SSR);
			cNKMLua.GetData("m_ReqEnchantFeedEXPBonusRate", ref nkmequipEnchantExpTemplet.m_ReqEnchantFeedEXPBonusRate);
			return nkmequipEnchantExpTemplet;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0005BB04 File Offset: 0x00059D04
		public override int GetHashCode()
		{
			return new ValueTuple<int, int>(this.m_EquipTier, this.m_EquipEnchantLevel).GetHashCode();
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0005BB30 File Offset: 0x00059D30
		public void Join()
		{
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0005BB32 File Offset: 0x00059D32
		public void Validate()
		{
		}

		// Token: 0x04000F69 RID: 3945
		public int m_EquipTier;

		// Token: 0x04000F6A RID: 3946
		public int m_EquipEnchantLevel;

		// Token: 0x04000F6B RID: 3947
		public int m_ReqLevelupExp_N;

		// Token: 0x04000F6C RID: 3948
		public int m_ReqLevelupExp_R;

		// Token: 0x04000F6D RID: 3949
		public int m_ReqLevelupExp_SR;

		// Token: 0x04000F6E RID: 3950
		public int m_ReqLevelupExp_SSR;

		// Token: 0x04000F6F RID: 3951
		public float m_ReqEnchantFeedEXPBonusRate;
	}
}

using System;

namespace NKM
{
	// Token: 0x020004E9 RID: 1257
	public class NKMDangerCharge
	{
		// Token: 0x06002379 RID: 9081 RVA: 0x000B6F54 File Offset: 0x000B5154
		public void DeepCopyFromSource(NKMDangerCharge source)
		{
			this.m_fChargeTime = source.m_fChargeTime;
			this.m_fCancelDamageRate = source.m_fCancelDamageRate;
			this.m_CancelHitCount = source.m_CancelHitCount;
			this.m_SuccessState = source.m_SuccessState;
			this.m_CancelState = source.m_CancelState;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000B6F94 File Offset: 0x000B5194
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_fChargeTime", ref this.m_fChargeTime);
			cNKMLua.GetData("m_fCancelDamageRate", ref this.m_fCancelDamageRate);
			cNKMLua.GetData("m_CancelHitCount", ref this.m_CancelHitCount);
			cNKMLua.GetData("m_SuccessState", ref this.m_SuccessState);
			cNKMLua.GetData("m_CancelState", ref this.m_CancelState);
			return true;
		}

		// Token: 0x04002504 RID: 9476
		public float m_fChargeTime = -1f;

		// Token: 0x04002505 RID: 9477
		public float m_fCancelDamageRate;

		// Token: 0x04002506 RID: 9478
		public int m_CancelHitCount;

		// Token: 0x04002507 RID: 9479
		public string m_SuccessState = "";

		// Token: 0x04002508 RID: 9480
		public string m_CancelState = "";
	}
}

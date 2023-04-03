using System;
using NKM;

namespace NKC
{
	// Token: 0x02000670 RID: 1648
	public class NKCEffectReserveData : NKMObjectPoolData
	{
		// Token: 0x0600342B RID: 13355 RVA: 0x00106BD6 File Offset: 0x00104DD6
		public NKCEffectReserveData()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCEffectReserveData;
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x00106BE6 File Offset: 0x00104DE6
		public override void Unload()
		{
			this.m_NKCASEffect = null;
		}

		// Token: 0x040032A3 RID: 12963
		public NKCASEffect m_NKCASEffect;

		// Token: 0x040032A4 RID: 12964
		public float m_PosX;

		// Token: 0x040032A5 RID: 12965
		public float m_PosY;

		// Token: 0x040032A6 RID: 12966
		public float m_PosZ;

		// Token: 0x040032A7 RID: 12967
		public bool m_bNotStart;

		// Token: 0x040032A8 RID: 12968
		public float m_fReserveTime;
	}
}

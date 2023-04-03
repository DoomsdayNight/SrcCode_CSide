using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200051B RID: 1307
	public class NKMOperatorExpTemplet : INKMTemplet
	{
		// Token: 0x06002556 RID: 9558 RVA: 0x000C0AF4 File Offset: 0x000BECF4
		public NKMOperatorExpTemplet(NKM_UNIT_GRADE m_NKM_UNIT_GRADE, List<NKMOperatorExpData> datas)
		{
			if (datas == null || datas.Count == 0)
			{
				Log.ErrorAndExit(string.Format("Invalid data list. UnitGrade:{0}", m_NKM_UNIT_GRADE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorExpTemplet.cs", 19);
				return;
			}
			this.m_NKM_UNIT_GRADE = m_NKM_UNIT_GRADE;
			this.datas.AddRange(datas);
			NKMOperatorExpTemplet.MaxLevel = Math.Max(NKMOperatorExpTemplet.MaxLevel, datas.Max((NKMOperatorExpData e) => e.m_iLevel));
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000C0B81 File Offset: 0x000BED81
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x000C0B88 File Offset: 0x000BED88
		public static int MaxLevel { get; private set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000C0B90 File Offset: 0x000BED90
		public int Key
		{
			get
			{
				return (int)this.m_NKM_UNIT_GRADE;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000C0B98 File Offset: 0x000BED98
		public List<NKMOperatorExpData> values
		{
			get
			{
				return this.datas;
			}
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000C0BA0 File Offset: 0x000BEDA0
		public static NKMOperatorExpTemplet Find(NKM_UNIT_GRADE key)
		{
			return NKMTempletContainer<NKMOperatorExpTemplet>.Find((int)key);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000C0BA8 File Offset: 0x000BEDA8
		public void Join()
		{
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000C0BAA File Offset: 0x000BEDAA
		public void Validate()
		{
		}

		// Token: 0x040026B1 RID: 9905
		private NKM_UNIT_GRADE m_NKM_UNIT_GRADE;

		// Token: 0x040026B2 RID: 9906
		private List<NKMOperatorExpData> datas = new List<NKMOperatorExpData>();
	}
}

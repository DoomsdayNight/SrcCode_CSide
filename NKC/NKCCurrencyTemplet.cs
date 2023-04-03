using System;
using NKM;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006C8 RID: 1736
	public class NKCCurrencyTemplet : INKMTemplet
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x00134968 File Offset: 0x00132B68
		public int Key
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00134970 File Offset: 0x00132B70
		public static NKCCurrencyTemplet LoadFromLua(NKMLua cNKMLua)
		{
			NKCCurrencyTemplet nkccurrencyTemplet = new NKCCurrencyTemplet();
			if (!(true & cNKMLua.GetData("m_Type", ref nkccurrencyTemplet.m_Type) & cNKMLua.GetData("m_Code", ref nkccurrencyTemplet.m_Code)))
			{
				return null;
			}
			return nkccurrencyTemplet;
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x001349AD File Offset: 0x00132BAD
		public void Join()
		{
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x001349AF File Offset: 0x00132BAF
		public void Validate()
		{
		}

		// Token: 0x040035D1 RID: 13777
		public int m_Type;

		// Token: 0x040035D2 RID: 13778
		public string m_Code;
	}
}

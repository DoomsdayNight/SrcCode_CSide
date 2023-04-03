using System;

namespace Cs.GameLog.CountryDescription
{
	// Token: 0x020010D4 RID: 4308
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public sealed class CountryDescriptionAttribute : Attribute
	{
		// Token: 0x06009E21 RID: 40481 RVA: 0x0033A63C File Offset: 0x0033883C
		public CountryDescriptionAttribute(string desc, CountryCode code = CountryCode.KOR)
		{
			this.Description = desc;
			this.CountryCode = code;
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06009E22 RID: 40482 RVA: 0x0033A652 File Offset: 0x00338852
		public string Description { get; }

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06009E23 RID: 40483 RVA: 0x0033A65A File Offset: 0x0033885A
		public CountryCode CountryCode { get; }
	}
}

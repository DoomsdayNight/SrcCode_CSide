using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000307 RID: 775
	public interface IFancyScrollRectContext
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06001135 RID: 4405
		// (set) Token: 0x06001136 RID: 4406
		ScrollDirection ScrollDirection { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06001137 RID: 4407
		// (set) Token: 0x06001138 RID: 4408
		[TupleElementNames(new string[]
		{
			"ScrollSize",
			"ReuseMargin"
		})]
		Func<ValueTuple<float, float>> CalculateScrollSize { [return: TupleElementNames(new string[]
		{
			"ScrollSize",
			"ReuseMargin"
		})] get; [param: TupleElementNames(new string[]
		{
			"ScrollSize",
			"ReuseMargin"
		})] set; }
	}
}

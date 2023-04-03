using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000306 RID: 774
	public class FancyScrollRectContext : IFancyScrollRectContext
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0003C21B File Offset: 0x0003A41B
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x0003C223 File Offset: 0x0003A423
		ScrollDirection IFancyScrollRectContext.ScrollDirection { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0003C22C File Offset: 0x0003A42C
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x0003C234 File Offset: 0x0003A434
		[TupleElementNames(new string[]
		{
			"ScrollSize",
			"ReuseMargin"
		})]
		Func<ValueTuple<float, float>> IFancyScrollRectContext.CalculateScrollSize { [return: TupleElementNames(new string[]
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

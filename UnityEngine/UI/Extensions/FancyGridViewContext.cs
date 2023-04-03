using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002FF RID: 767
	public class FancyGridViewContext : IFancyGridViewContext, IFancyScrollRectContext, IFancyCellGroupContext
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0003BB6A File Offset: 0x00039D6A
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x0003BB72 File Offset: 0x00039D72
		ScrollDirection IFancyScrollRectContext.ScrollDirection { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0003BB7B File Offset: 0x00039D7B
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x0003BB83 File Offset: 0x00039D83
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0003BB8C File Offset: 0x00039D8C
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x0003BB94 File Offset: 0x00039D94
		GameObject IFancyCellGroupContext.CellTemplate { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0003BB9D File Offset: 0x00039D9D
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x0003BBA5 File Offset: 0x00039DA5
		Func<int> IFancyCellGroupContext.GetGroupCount { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0003BBAE File Offset: 0x00039DAE
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x0003BBB6 File Offset: 0x00039DB6
		Func<float> IFancyGridViewContext.GetStartAxisSpacing { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0003BBBF File Offset: 0x00039DBF
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x0003BBC7 File Offset: 0x00039DC7
		Func<float> IFancyGridViewContext.GetCellSize { get; set; }
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E6 RID: 230
	[TaskIcon("{SkinColor}EntryIcon.png")]
	public class EntryTask : ParentTask
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x00019D37 File Offset: 0x00017F37
		public override int MaxChildren()
		{
			return 1;
		}
	}
}

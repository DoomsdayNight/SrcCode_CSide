using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace NKC.BT
{
	// Token: 0x02000812 RID: 2066
	public class BTSetBoolVariable : BehaviorDesigner.Runtime.Tasks.Action
	{
		// Token: 0x060051D1 RID: 20945 RVA: 0x0018D4A4 File Offset: 0x0018B6A4
		public override void OnStart()
		{
			base.OnAwake();
			SharedBool sharedBool = base.Owner.GetVariable(this.name) as SharedBool;
			if (sharedBool != null)
			{
				sharedBool.Value = this.value;
			}
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0018D4DD File Offset: 0x0018B6DD
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}

		// Token: 0x0400421E RID: 16926
		public string name;

		// Token: 0x0400421F RID: 16927
		public bool value;
	}
}

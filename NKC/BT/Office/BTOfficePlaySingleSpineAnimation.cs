using System;
using BehaviorDesigner.Runtime.Tasks;

namespace NKC.BT.Office
{
	// Token: 0x02000821 RID: 2081
	public class BTOfficePlaySingleSpineAnimation : BTOfficeActionBase
	{
		// Token: 0x06005207 RID: 20999 RVA: 0x0018EBE4 File Offset: 0x0018CDE4
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			this.m_Character.EnqueueSimpleAni(this.AnimName, this.bNow, this.bInvert);
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0018EC39 File Offset: 0x0018CE39
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.m_Character.PlayAnimCompleted())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x04004240 RID: 16960
		public string AnimName;

		// Token: 0x04004241 RID: 16961
		public bool bInvert;

		// Token: 0x04004242 RID: 16962
		public bool bNow;
	}
}

using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NKC.Office;

namespace NKC.BT.Office
{
	// Token: 0x02000826 RID: 2086
	public class BTTargetUnitSelector : BTOfficeActionBase
	{
		// Token: 0x06005219 RID: 21017 RVA: 0x0018F2E0 File Offset: 0x0018D4E0
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			if (this.lstUnitID == null || this.lstUnitID.Value == null || this.lstUnitID.Value.Count == 0)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			if (this.KeepSelectedUnit.Value && this.TargetUnit.Value != null)
			{
				this.bActionSuccessFlag = true;
				return;
			}
			if (this.hsActTargetGroupID == null || this.hsActTargetGroupID.Count != this.lstUnitID.Value.Count)
			{
				this.hsActTargetGroupID = new HashSet<string>(this.lstUnitID.Value);
			}
			ActTargetType value = this.ActorTargetType.Value;
			foreach (NKCOfficeCharacter nkcofficeCharacter in this.m_OfficeBuilding.GetCharacterEnumerator())
			{
				if (NKCOfficeManager.IsActTarget(nkcofficeCharacter, value, this.hsActTargetGroupID) && !(nkcofficeCharacter == this.m_Character))
				{
					this.TargetUnit.Value = nkcofficeCharacter;
				}
			}
			this.bActionSuccessFlag = true;
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x0018F420 File Offset: 0x0018D620
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.TargetUnit.Value == null)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04004249 RID: 16969
		public BTSharedTargetUnitType ActorTargetType = ActTargetType.Unit;

		// Token: 0x0400424A RID: 16970
		public BTSharedStringList lstUnitID;

		// Token: 0x0400424B RID: 16971
		public SharedBool KeepSelectedUnit = true;

		// Token: 0x0400424C RID: 16972
		public BTSharedOfficeCharacter TargetUnit;

		// Token: 0x0400424D RID: 16973
		private HashSet<string> hsActTargetGroupID;
	}
}

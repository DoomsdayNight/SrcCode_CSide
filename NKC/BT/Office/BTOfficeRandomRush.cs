using System;
using BehaviorDesigner.Runtime;
using NKC.Office;
using NKM;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000822 RID: 2082
	public class BTOfficeRandomRush : BTOfficeActionBase
	{
		// Token: 0x0600520A RID: 21002 RVA: 0x0018EC60 File Offset: 0x0018CE60
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			OfficeFloorPosition pos = new OfficeFloorPosition(NKMRandom.Range(0, this.m_OfficeBuilding.m_SizeX), NKMRandom.Range(0, this.m_OfficeBuilding.m_SizeY));
			pos = this.m_OfficeBuilding.FindNearestEmptyCell(pos);
			Vector3 localPos = this.m_OfficeBuilding.m_Floor.GetLocalPos(pos);
			NKCAnimationInstance runInstance = base.GetRunInstance(this.transform.localPosition, localPos, this.RunSpeed.Value, this.RushAnimName.Value);
			this.m_Character.EnqueueAnimation(runInstance);
		}

		// Token: 0x04004243 RID: 16963
		public SharedString RushAnimName;

		// Token: 0x04004244 RID: 16964
		public SharedFloat RunSpeed = 600f;
	}
}

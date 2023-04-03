using System;
using NKC.Office;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x0200081E RID: 2078
	public class BTOfficeMoveToEmptyCell : BTOfficeActionBase
	{
		// Token: 0x060051FE RID: 20990 RVA: 0x0018E834 File Offset: 0x0018CA34
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			OfficeFloorPosition officeFloorPosition = this.m_OfficeBuilding.CalculateFloorPosition(this.transform.localPosition, 1, 1, false);
			OfficeFloorPosition officeFloorPosition2 = new OfficeFloorPosition(Mathf.Clamp(officeFloorPosition.x, 0, this.m_OfficeBuilding.m_SizeX - 1), Mathf.Clamp(officeFloorPosition.y, 0, this.m_OfficeBuilding.m_SizeY - 1));
			officeFloorPosition2 = this.m_OfficeBuilding.FindNearestEmptyCell(officeFloorPosition2);
			if (!officeFloorPosition.Equals(officeFloorPosition2))
			{
				Vector3 localPos = base.Floor.GetLocalPos(officeFloorPosition2);
				if (this.bWarp)
				{
					this.transform.localPosition = localPos;
					return;
				}
				base.Move(officeFloorPosition2, true);
			}
		}

		// Token: 0x04004239 RID: 16953
		public bool bWarp;
	}
}

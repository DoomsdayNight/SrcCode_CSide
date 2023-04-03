using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NKC.Office;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x0200081D RID: 2077
	public class BTOfficeMoveFar : BTOfficeActionBase
	{
		// Token: 0x060051FB RID: 20987 RVA: 0x0018E658 File Offset: 0x0018C858
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			if (string.IsNullOrEmpty(this.MoveAnimEventName.Value))
			{
				Debug.LogError("BTOfficeMoveFar : has no warpAnimation!");
				this.bActionSuccessFlag = false;
				return;
			}
			List<NKCAnimationEventTemplet> list = NKCAnimationEventManager.Find(this.MoveAnimEventName.Value);
			if (list == null)
			{
				Debug.LogError("BTWarp : animation " + this.MoveAnimEventName.Value + " not found!");
				this.bActionSuccessFlag = false;
				return;
			}
			Vector3 localPosition = this.transform.localPosition;
			float num = this.MinRange.Value * this.MinRange.Value;
			List<OfficeFloorPosition> list2 = new List<OfficeFloorPosition>();
			for (int i = 0; i < this.m_OfficeBuilding.m_SizeX; i++)
			{
				for (int j = 0; j < this.m_OfficeBuilding.m_SizeY; j++)
				{
					if (this.m_OfficeBuilding.FloorMap[i, j] == 0L)
					{
						Vector3 localPos = this.m_OfficeBuilding.m_Floor.GetLocalPos(i, j);
						if ((localPosition - localPos).sqrMagnitude >= num)
						{
							list2.Add(new OfficeFloorPosition(i, j));
						}
					}
				}
			}
			if (list2.Count == 0)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			OfficeFloorPosition pos = list2[UnityEngine.Random.Range(0, list2.Count)];
			Vector3 localPos2 = this.m_OfficeBuilding.m_Floor.GetLocalPos(pos);
			if (!base.Move(list, localPos2, this.m_bIgnoreObstacle.Value))
			{
				this.bActionSuccessFlag = false;
				return;
			}
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x0018E7F2 File Offset: 0x0018C9F2
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

		// Token: 0x04004236 RID: 16950
		public SharedString MoveAnimEventName;

		// Token: 0x04004237 RID: 16951
		public SharedFloat MinRange = 600f;

		// Token: 0x04004238 RID: 16952
		public SharedBool m_bIgnoreObstacle = false;
	}
}

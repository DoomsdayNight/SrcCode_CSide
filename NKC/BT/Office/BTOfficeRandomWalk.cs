using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;
using NKC.Office;
using NKM;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000823 RID: 2083
	public class BTOfficeRandomWalk : BTOfficeActionBase
	{
		// Token: 0x0600520C RID: 21004 RVA: 0x0018ED2D File Offset: 0x0018CF2D
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			this.randomWalkStepLeft = UnityEngine.Random.Range(1, this.RandomWalkCount);
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0018ED6C File Offset: 0x0018CF6C
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.m_Character.PlayAnimCompleted())
			{
				if (this.randomWalkStepLeft <= 0)
				{
					return TaskStatus.Success;
				}
				this.randomWalkStepLeft--;
				OfficeFloorPosition origin = this.m_OfficeBuilding.CalculateFloorPosition(this.transform.localPosition, 1, 1, false);
				Vector3 localPosition = this.transform.localPosition;
				OfficeFloorPosition randomWalkCell = this.GetRandomWalkCell(origin, NKMRandom.Range(3, 5));
				Vector3 localPos = this.m_Character.GetLocalPos(randomWalkCell, true);
				this.m_Character.EnqueueAnimation(base.GetWalkInstance(localPosition, localPos, 150f, ""));
				if (NKMRandom.Range(0, 3) == 0)
				{
					this.m_Character.EnqueueAnimation(base.GetIdleInstance(UnityEngine.Random.Range(1f, 3f), localPos, ""));
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0018EE44 File Offset: 0x0018D044
		private OfficeFloorPosition GetRandomWalkCell(OfficeFloorPosition origin, int step)
		{
			BTOfficeRandomWalk.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.FloorMap = this.m_OfficeBuilding.FloorMap;
			if (CS$<>8__locals1.FloorMap == null)
			{
				return origin;
			}
			List<OfficeFloorPosition> list = new List<OfficeFloorPosition>(4);
			OfficeFloorPosition officeFloorPosition = origin;
			for (int i = 0; i < step; i++)
			{
				OfficeFloorPosition officeFloorPosition2 = new OfficeFloorPosition(officeFloorPosition.x - 1, officeFloorPosition.y);
				OfficeFloorPosition officeFloorPosition3 = new OfficeFloorPosition(officeFloorPosition.x + 1, officeFloorPosition.y);
				OfficeFloorPosition officeFloorPosition4 = new OfficeFloorPosition(officeFloorPosition.x, officeFloorPosition.y + 1);
				OfficeFloorPosition officeFloorPosition5 = new OfficeFloorPosition(officeFloorPosition.x, officeFloorPosition.y - 1);
				if (BTOfficeRandomWalk.<GetRandomWalkCell>g__Possible|4_0(officeFloorPosition2, ref CS$<>8__locals1))
				{
					list.Add(officeFloorPosition2);
				}
				if (BTOfficeRandomWalk.<GetRandomWalkCell>g__Possible|4_0(officeFloorPosition3, ref CS$<>8__locals1))
				{
					list.Add(officeFloorPosition3);
				}
				if (BTOfficeRandomWalk.<GetRandomWalkCell>g__Possible|4_0(officeFloorPosition4, ref CS$<>8__locals1))
				{
					list.Add(officeFloorPosition4);
				}
				if (BTOfficeRandomWalk.<GetRandomWalkCell>g__Possible|4_0(officeFloorPosition5, ref CS$<>8__locals1))
				{
					list.Add(officeFloorPosition5);
				}
				if (list.Count == 0)
				{
					return officeFloorPosition;
				}
				officeFloorPosition = list[UnityEngine.Random.Range(0, list.Count)];
			}
			return officeFloorPosition;
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0018EF58 File Offset: 0x0018D158
		[CompilerGenerated]
		internal static bool <GetRandomWalkCell>g__Possible|4_0(OfficeFloorPosition pos, ref BTOfficeRandomWalk.<>c__DisplayClass4_0 A_1)
		{
			return pos.x >= 0 && pos.y >= 0 && pos.x < A_1.FloorMap.GetLength(0) && pos.y < A_1.FloorMap.GetLength(1) && A_1.FloorMap[pos.x, pos.y] == 0L;
		}

		// Token: 0x04004245 RID: 16965
		[Header("한번에 최대 몇번까지 걷기 액션을 하는가")]
		public int RandomWalkCount = 10;

		// Token: 0x04004246 RID: 16966
		private int randomWalkStepLeft;
	}
}

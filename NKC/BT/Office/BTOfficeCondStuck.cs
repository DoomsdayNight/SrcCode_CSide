using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;
using NKC.Office;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000816 RID: 2070
	public class BTOfficeCondStuck : BTOfficeConditionBase
	{
		// Token: 0x060051E8 RID: 20968 RVA: 0x0018DDC0 File Offset: 0x0018BFC0
		public override TaskStatus OnUpdate()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				return TaskStatus.Failure;
			}
			OfficeFloorPosition origin = this.m_OfficeBuilding.CalculateFloorPosition(this.transform.localPosition, 1, 1, false);
			if (this.IsStuck(this.freeSpaceCount, origin))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0018DE18 File Offset: 0x0018C018
		private bool IsStuck(int spaceCount, OfficeFloorPosition origin)
		{
			BTOfficeCondStuck.<>c__DisplayClass2_0 CS$<>8__locals1;
			CS$<>8__locals1.FloorMap = this.m_OfficeBuilding.FloorMap;
			if (CS$<>8__locals1.FloorMap == null)
			{
				return false;
			}
			Queue<OfficeFloorPosition> queue = new Queue<OfficeFloorPosition>();
			HashSet<OfficeFloorPosition> hashSet = new HashSet<OfficeFloorPosition>();
			queue.Enqueue(origin);
			while (queue.Count > 0)
			{
				OfficeFloorPosition officeFloorPosition = queue.Dequeue();
				hashSet.Add(officeFloorPosition);
				OfficeFloorPosition officeFloorPosition2 = new OfficeFloorPosition(officeFloorPosition.x - 1, officeFloorPosition.y);
				OfficeFloorPosition officeFloorPosition3 = new OfficeFloorPosition(officeFloorPosition.x + 1, officeFloorPosition.y);
				OfficeFloorPosition officeFloorPosition4 = new OfficeFloorPosition(officeFloorPosition.x, officeFloorPosition.y + 1);
				OfficeFloorPosition officeFloorPosition5 = new OfficeFloorPosition(officeFloorPosition.x, officeFloorPosition.y - 1);
				if (!hashSet.Contains(officeFloorPosition2) && BTOfficeCondStuck.<IsStuck>g__Possible|2_0(officeFloorPosition2, ref CS$<>8__locals1))
				{
					queue.Enqueue(officeFloorPosition2);
				}
				if (!hashSet.Contains(officeFloorPosition3) && BTOfficeCondStuck.<IsStuck>g__Possible|2_0(officeFloorPosition3, ref CS$<>8__locals1))
				{
					queue.Enqueue(officeFloorPosition3);
				}
				if (!hashSet.Contains(officeFloorPosition4) && BTOfficeCondStuck.<IsStuck>g__Possible|2_0(officeFloorPosition4, ref CS$<>8__locals1))
				{
					queue.Enqueue(officeFloorPosition4);
				}
				if (!hashSet.Contains(officeFloorPosition5) && BTOfficeCondStuck.<IsStuck>g__Possible|2_0(officeFloorPosition5, ref CS$<>8__locals1))
				{
					queue.Enqueue(officeFloorPosition5);
				}
				if (hashSet.Count + queue.Count > spaceCount)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0018DF58 File Offset: 0x0018C158
		[CompilerGenerated]
		internal static bool <IsStuck>g__Possible|2_0(OfficeFloorPosition pos, ref BTOfficeCondStuck.<>c__DisplayClass2_0 A_1)
		{
			return pos.x >= 0 && pos.y >= 0 && pos.x < A_1.FloorMap.GetLength(0) && pos.y < A_1.FloorMap.GetLength(1) && A_1.FloorMap[pos.x, pos.y] == 0L;
		}

		// Token: 0x04004225 RID: 16933
		[Header("이동 가능한 칸 수가 이 칸보다 작거나 같음")]
		public int freeSpaceCount;
	}
}

using System;
using NKC.Office;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200009A RID: 154
	public class BTSharedTargetUnitType : BTSharedEnum<ActTargetType>
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00016A0E File Offset: 0x00014C0E
		public new static implicit operator BTSharedTargetUnitType(ActTargetType value)
		{
			return new BTSharedTargetUnitType
			{
				Value = value
			};
		}
	}
}

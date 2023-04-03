using System;
using NKM;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public class BTSharedEnum<T> : BTSharedNKCValue<T> where T : Enum
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x000169D6 File Offset: 0x00014BD6
		public static implicit operator BTSharedEnum<T>(T value)
		{
			return new BTSharedEnum<T>
			{
				Value = value
			};
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000169E4 File Offset: 0x00014BE4
		public override bool TryParse(string parameter)
		{
			T value;
			if (parameter.TryParse(out value, false))
			{
				base.Value = value;
				return true;
			}
			return false;
		}
	}
}

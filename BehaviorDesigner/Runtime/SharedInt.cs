using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class SharedInt : SharedVariable<int>
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00016AFA File Offset: 0x00014CFA
		public static implicit operator SharedInt(int value)
		{
			return new SharedInt
			{
				mValue = value
			};
		}
	}
}

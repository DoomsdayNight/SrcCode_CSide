using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	public class SharedBool : SharedVariable<bool>
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00016A76 File Offset: 0x00014C76
		public static implicit operator SharedBool(bool value)
		{
			return new SharedBool
			{
				mValue = value
			};
		}
	}
}

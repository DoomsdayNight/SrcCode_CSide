using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	public class SharedString : SharedVariable<string>
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x00016B94 File Offset: 0x00014D94
		public static implicit operator SharedString(string value)
		{
			return new SharedString
			{
				mValue = value
			};
		}
	}
}

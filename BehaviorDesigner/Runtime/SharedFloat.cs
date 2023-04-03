using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	public class SharedFloat : SharedVariable<float>
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00016AB8 File Offset: 0x00014CB8
		public static implicit operator SharedFloat(float value)
		{
			return new SharedFloat
			{
				Value = value
			};
		}
	}
}

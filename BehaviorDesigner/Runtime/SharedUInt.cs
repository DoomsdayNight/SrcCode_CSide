using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public class SharedUInt : SharedVariable<uint>
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x00016BD6 File Offset: 0x00014DD6
		public static implicit operator SharedUInt(uint value)
		{
			return new SharedUInt
			{
				mValue = value
			};
		}
	}
}

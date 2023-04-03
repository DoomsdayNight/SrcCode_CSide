using System;
using NKC.Office;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200009B RID: 155
	public class BTSharedOfficeCharacter : SharedVariable<NKCOfficeCharacter>
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x00016A24 File Offset: 0x00014C24
		public static implicit operator BTSharedOfficeCharacter(NKCOfficeCharacter value)
		{
			return new BTSharedOfficeCharacter
			{
				Value = value
			};
		}
	}
}

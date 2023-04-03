using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class BTSharedStringList : BTSharedNKCValue<List<string>>
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x00016968 File Offset: 0x00014B68
		public static implicit operator BTSharedStringList(List<string> value)
		{
			return new BTSharedStringList
			{
				Value = value
			};
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00016976 File Offset: 0x00014B76
		public override bool TryParse(string parameters)
		{
			return this.TryParse(parameters, this.defaultSeperator);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00016985 File Offset: 0x00014B85
		public override string ToString()
		{
			return BTSharedNKCValue.ToDebugString<string>(base.Value);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00016994 File Offset: 0x00014B94
		public bool TryParse(string parameters, char[] seperator)
		{
			string[] collection = parameters.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
			base.Value = new List<string>(collection);
			return true;
		}

		// Token: 0x040002CD RID: 717
		private readonly char[] defaultSeperator = new char[]
		{
			'\n',
			' ',
			'\t',
			','
		};
	}
}

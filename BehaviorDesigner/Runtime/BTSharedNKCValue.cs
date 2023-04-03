using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000095 RID: 149
	public abstract class BTSharedNKCValue : SharedVariable
	{
		// Token: 0x060005E1 RID: 1505
		public abstract bool TryParse(string paramater);

		// Token: 0x060005E2 RID: 1506 RVA: 0x000165D8 File Offset: 0x000147D8
		public static string ToDebugString<T>(IEnumerable<T> target)
		{
			if (target == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(target.GetType().ToString());
			stringBuilder.Append("[");
			bool flag = false;
			foreach (T t in target)
			{
				stringBuilder.Append(t.ToString());
				stringBuilder.Append(", ");
				flag = true;
			}
			if (flag)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}

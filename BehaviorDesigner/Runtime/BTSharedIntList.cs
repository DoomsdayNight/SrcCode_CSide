using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	public class BTSharedIntList : BTSharedNKCValue<List<int>>
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x000168D4 File Offset: 0x00014AD4
		public static implicit operator BTSharedIntList(List<int> value)
		{
			return new BTSharedIntList
			{
				Value = value
			};
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000168E2 File Offset: 0x00014AE2
		public override bool TryParse(string parameters)
		{
			return this.TryParse(parameters, this.defaultSeperator);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000168F1 File Offset: 0x00014AF1
		public override string ToString()
		{
			return BTSharedNKCValue.ToDebugString<int>(base.Value);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00016900 File Offset: 0x00014B00
		public bool TryParse(string parameters, char[] seperator)
		{
			string[] array = parameters.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
			List<int> list = new List<int>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int item;
				if (!int.TryParse(array2[i], out item))
				{
					return false;
				}
				list.Add(item);
			}
			base.Value = list;
			return true;
		}

		// Token: 0x040002CC RID: 716
		private readonly char[] defaultSeperator = new char[]
		{
			'\n',
			' ',
			'\t',
			','
		};
	}
}

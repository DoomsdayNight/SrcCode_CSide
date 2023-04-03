using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x02000395 RID: 917
	public class PriorityQueue<T>
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0005F06B File Offset: 0x0005D26B
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0005F078 File Offset: 0x0005D278
		public void Enqueue(T item, float priority)
		{
			this.elements.Add(new KeyValuePair<T, float>(item, priority));
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0005F08C File Offset: 0x0005D28C
		public T Dequeue()
		{
			int index = 0;
			for (int i = 0; i < this.elements.Count; i++)
			{
				if (this.elements[i].Value < this.elements[index].Value)
				{
					index = i;
				}
			}
			T key = this.elements[index].Key;
			this.elements.RemoveAt(index);
			return key;
		}

		// Token: 0x04000FF4 RID: 4084
		private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();
	}
}

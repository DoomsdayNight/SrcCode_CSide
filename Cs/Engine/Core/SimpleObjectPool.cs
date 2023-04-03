using System;
using System.Collections.Concurrent;

namespace Cs.Engine.Core
{
	// Token: 0x020010B6 RID: 4278
	public sealed class SimpleObjectPool<T>
	{
		// Token: 0x06009CA6 RID: 40102 RVA: 0x003363A1 File Offset: 0x003345A1
		public SimpleObjectPool(Func<T> factoryMethod)
		{
			this.factoryMethod = factoryMethod;
		}

		// Token: 0x06009CA7 RID: 40103 RVA: 0x003363BC File Offset: 0x003345BC
		public T CreateInstance()
		{
			T result;
			if (this.objects.TryDequeue(out result))
			{
				return result;
			}
			return this.factoryMethod();
		}

		// Token: 0x06009CA8 RID: 40104 RVA: 0x003363E5 File Offset: 0x003345E5
		public void ToRecycleBin(T item)
		{
			this.objects.Enqueue(item);
		}

		// Token: 0x04009072 RID: 36978
		private readonly ConcurrentQueue<T> objects = new ConcurrentQueue<T>();

		// Token: 0x04009073 RID: 36979
		private readonly Func<T> factoryMethod;
	}
}

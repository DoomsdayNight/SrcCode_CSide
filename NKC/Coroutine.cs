using System;
using System.Collections;
using System.Collections.Generic;
using Cs.Logging;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006F1 RID: 1777
	public class Coroutine<T>
	{
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x00148057 File Offset: 0x00146257
		public T Value
		{
			get
			{
				if (this.e != null)
				{
					throw this.e;
				}
				return this.returnVal;
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x0014806E File Offset: 0x0014626E
		public IEnumerator InternalRoutine(IEnumerator coroutine)
		{
			Stack<IEnumerator> stack = new Stack<IEnumerator>();
			stack.Push(coroutine);
			while (stack.Count != 0)
			{
				IEnumerator enumerator = stack.Peek();
				object obj;
				try
				{
					if (!enumerator.MoveNext())
					{
						stack.Pop();
						continue;
					}
					obj = enumerator.Current;
				}
				catch (Exception ex)
				{
					Exception ex2 = new Exception(ex.Message, ex);
					this.e = ex2;
					Log.Error(string.Format("Coroutine Exception : {0}", ex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUtil.cs", 4100);
					yield break;
				}
				IEnumerator enumerator2 = obj as IEnumerator;
				if (enumerator2 != null)
				{
					stack.Push(enumerator2);
				}
				else
				{
					if (obj != null && obj.GetType() == typeof(T))
					{
						this.returnVal = (T)((object)obj);
						yield break;
					}
					yield return obj;
				}
			}
			yield break;
		}

		// Token: 0x0400373A RID: 14138
		private T returnVal;

		// Token: 0x0400373B RID: 14139
		private Exception e;

		// Token: 0x0400373C RID: 14140
		public Coroutine coroutine;
	}
}

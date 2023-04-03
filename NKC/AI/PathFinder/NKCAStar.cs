using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.AI.PathFinder
{
	// Token: 0x02000841 RID: 2113
	public class NKCAStar
	{
		// Token: 0x0600543D RID: 21565 RVA: 0x0019B568 File Offset: 0x00199768
		public NKCAStar(long[,] _map, ValueTuple<int, int> startPos, ValueTuple<int, int> endPos)
		{
			this.map = _map;
			if (this.map[startPos.Item1, startPos.Item2] != 0L)
			{
				return;
			}
			if (this.map[endPos.Item1, endPos.Item2] != 0L)
			{
				return;
			}
			this.size = new ValueTuple<int, int>(this.map.GetLength(0), this.map.GetLength(1));
			this.start = startPos;
			this.goal = endPos;
			NKCAStar.PriorityQueue<ValueTuple<int, int>> priorityQueue = new NKCAStar.PriorityQueue<ValueTuple<int, int>>();
			priorityQueue.Enqueue(startPos, 0f);
			this.cameFrom.Add(startPos, startPos);
			this.costSoFar.Add(startPos, 0f);
			while (priorityQueue.Count > 0)
			{
				ValueTuple<int, int> valueTuple = priorityQueue.Dequeue();
				ValueTuple<int, int> valueTuple2 = valueTuple;
				if (valueTuple2.Item1 == endPos.Item1 && valueTuple2.Item2 == endPos.Item2)
				{
					break;
				}
				foreach (ValueTuple<int, int> valueTuple3 in this.GetNeighbors(valueTuple))
				{
					if (this.map[valueTuple3.Item1, valueTuple3.Item2] == 0L)
					{
						float num = this.costSoFar[valueTuple] + this.CalcSimpleCost(valueTuple, valueTuple3);
						if (!this.costSoFar.ContainsKey(valueTuple3) || num < this.costSoFar[valueTuple3])
						{
							this.costSoFar[valueTuple3] = num;
							this.cameFrom[valueTuple3] = valueTuple;
							float priority = num + this.Heuristic(valueTuple3, endPos);
							priorityQueue.Enqueue(valueTuple3, priority);
						}
					}
				}
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0019B738 File Offset: 0x00199938
		public List<ValueTuple<int, int>> GetPath(bool smoothen)
		{
			List<ValueTuple<int, int>> list = new List<ValueTuple<int, int>>();
			ValueTuple<int, int> valueTuple = this.goal;
			while (!valueTuple.Equals(this.start))
			{
				if (!this.cameFrom.ContainsKey(valueTuple))
				{
					return null;
				}
				list.Add(valueTuple);
				valueTuple = this.cameFrom[valueTuple];
			}
			if (!smoothen)
			{
				list.Reverse();
				return list;
			}
			list.Add(this.start);
			list.Reverse();
			return this.SmoothenPath(list);
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0019B7AC File Offset: 0x001999AC
		private List<ValueTuple<int, int>> SmoothenPath(List<ValueTuple<int, int>> path)
		{
			if (this.map == null)
			{
				return path;
			}
			if (path.Count <= 2)
			{
				return path;
			}
			LinkedList<ValueTuple<int, int>> linkedList = new LinkedList<ValueTuple<int, int>>(path);
			LinkedListNode<ValueTuple<int, int>> linkedListNode = linkedList.First;
			LinkedListNode<ValueTuple<int, int>> next = linkedListNode.Next;
			while (next.Next != null)
			{
				if (this.Walkable(linkedListNode.Value, next.Next.Value))
				{
					LinkedListNode<ValueTuple<int, int>> node = next;
					next = next.Next;
					linkedList.Remove(node);
				}
				else
				{
					linkedListNode = next;
					next = next.Next;
				}
			}
			linkedList.RemoveFirst();
			return new List<ValueTuple<int, int>>(linkedList);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0019B830 File Offset: 0x00199A30
		private bool Walkable(ValueTuple<int, int> start, ValueTuple<int, int> end)
		{
			if (start.Item1 == end.Item1 && start.Item2 == end.Item2)
			{
				return true;
			}
			if (start.Item1 == end.Item1)
			{
				int num = Mathf.Max(start.Item2, end.Item2);
				for (int i = Mathf.Min(start.Item2, end.Item2); i <= num; i++)
				{
					if (this.map[start.Item1, i] != 0L)
					{
						return false;
					}
				}
				return true;
			}
			if (start.Item2 == end.Item2)
			{
				int num2 = Mathf.Max(start.Item1, end.Item1);
				for (int j = Mathf.Min(start.Item1, end.Item1); j <= num2; j++)
				{
					if (this.map[j, start.Item2] != 0L)
					{
						return false;
					}
				}
				return true;
			}
			float num3 = (float)(end.Item2 - start.Item2) / (float)(end.Item1 - start.Item1);
			float num4 = (float)start.Item2 - num3 * (float)start.Item1;
			int num5 = Mathf.Min(start.Item1, end.Item1);
			int num6 = Mathf.Max(start.Item1, end.Item1);
			for (int k = num5; k <= num6; k++)
			{
				float num7 = (k == num5) ? ((float)k) : ((float)k - 0.5f);
				float num8 = (k == num6) ? ((float)k) : ((float)k + 0.5f);
				float num9 = num3 * num7 + num4;
				float num10 = num3 * num8 + num4;
				int a = Mathf.FloorToInt(num9 + 0.5f);
				int b = Mathf.FloorToInt(num10 + 0.5f);
				int num11 = Mathf.Min(a, b);
				int num12 = Mathf.Max(a, b);
				for (int l = num11; l <= num12; l++)
				{
					if (this.map[k, l] != 0L)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0019BA10 File Offset: 0x00199C10
		private IEnumerable<ValueTuple<int, int>> GetNeighbors(ValueTuple<int, int> position)
		{
			if (this.IsInBound(position.Item1, position.Item2 + 1))
			{
				yield return new ValueTuple<int, int>(position.Item1, position.Item2 + 1);
			}
			if (this.IsInBound(position.Item1 + 1, position.Item2))
			{
				yield return new ValueTuple<int, int>(position.Item1 + 1, position.Item2);
			}
			if (this.IsInBound(position.Item1, position.Item2 - 1))
			{
				yield return new ValueTuple<int, int>(position.Item1, position.Item2 - 1);
			}
			if (this.IsInBound(position.Item1 - 1, position.Item2))
			{
				yield return new ValueTuple<int, int>(position.Item1 - 1, position.Item2);
			}
			if (this.IsInBound(position.Item1 - 1, position.Item2 + 1))
			{
				yield return new ValueTuple<int, int>(position.Item1 - 1, position.Item2 + 1);
			}
			if (this.IsInBound(position.Item1 + 1, position.Item2 + 1))
			{
				yield return new ValueTuple<int, int>(position.Item1 + 1, position.Item2 + 1);
			}
			if (this.IsInBound(position.Item1 + 1, position.Item2 - 1))
			{
				yield return new ValueTuple<int, int>(position.Item1 + 1, position.Item2 - 1);
			}
			if (this.IsInBound(position.Item1 - 1, position.Item2 - 1))
			{
				yield return new ValueTuple<int, int>(position.Item1 - 1, position.Item2 - 1);
			}
			yield break;
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0019BA27 File Offset: 0x00199C27
		private float Heuristic(ValueTuple<int, int> pos1, ValueTuple<int, int> pos2)
		{
			return this.CalcSimpleCost(pos1, pos2);
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0019BA34 File Offset: 0x00199C34
		private float CalcSimpleCost(ValueTuple<int, int> pos1, ValueTuple<int, int> pos2)
		{
			int num = Mathf.Abs(pos1.Item1 - pos2.Item1);
			int num2 = Mathf.Abs(pos1.Item2 - pos2.Item2);
			if (num > num2)
			{
				return (float)num + (float)num2 * 0.4f;
			}
			return (float)num2 + (float)num * 0.4f;
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0019BA82 File Offset: 0x00199C82
		private bool IsInBound(int x, int y)
		{
			return x >= 0 && y >= 0 && x < this.size.Item1 && y < this.size.Item2;
		}

		// Token: 0x0400433A RID: 17210
		private Dictionary<ValueTuple<int, int>, ValueTuple<int, int>> cameFrom = new Dictionary<ValueTuple<int, int>, ValueTuple<int, int>>();

		// Token: 0x0400433B RID: 17211
		private Dictionary<ValueTuple<int, int>, float> costSoFar = new Dictionary<ValueTuple<int, int>, float>();

		// Token: 0x0400433C RID: 17212
		private ValueTuple<int, int> start;

		// Token: 0x0400433D RID: 17213
		private ValueTuple<int, int> goal;

		// Token: 0x0400433E RID: 17214
		private ValueTuple<int, int> size;

		// Token: 0x0400433F RID: 17215
		private long[,] map;

		// Token: 0x020014E8 RID: 5352
		private class PriorityQueue<T>
		{
			// Token: 0x1700185D RID: 6237
			// (get) Token: 0x0600AA38 RID: 43576 RVA: 0x0034E7E0 File Offset: 0x0034C9E0
			public int Count
			{
				get
				{
					return this.elements.Count;
				}
			}

			// Token: 0x0600AA39 RID: 43577 RVA: 0x0034E7F0 File Offset: 0x0034C9F0
			public void Enqueue(T item, float priority)
			{
				KeyValuePair<T, float> value = new KeyValuePair<T, float>(item, priority);
				for (LinkedListNode<KeyValuePair<T, float>> linkedListNode = this.elements.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
				{
					if (priority > linkedListNode.Value.Value)
					{
						this.elements.AddAfter(linkedListNode, value);
						return;
					}
				}
				this.elements.AddFirst(value);
			}

			// Token: 0x0600AA3A RID: 43578 RVA: 0x0034E84C File Offset: 0x0034CA4C
			public T Dequeue()
			{
				KeyValuePair<T, float> value = this.elements.First.Value;
				this.elements.RemoveFirst();
				return value.Key;
			}

			// Token: 0x04009F56 RID: 40790
			private LinkedList<KeyValuePair<T, float>> elements = new LinkedList<KeyValuePair<T, float>>();
		}
	}
}

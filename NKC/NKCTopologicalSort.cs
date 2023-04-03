using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200062B RID: 1579
	public class NKCTopologicalSort<T>
	{
		// Token: 0x060030C5 RID: 12485 RVA: 0x000F1B4C File Offset: 0x000EFD4C
		public T GetObject(int index)
		{
			return this.m_lstTarget[index];
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000F1B5C File Offset: 0x000EFD5C
		private NKCTopologicalSort<T>.Relation GetRelation(int indexA, int indexB)
		{
			NKCTopologicalSort<T>.Relation result;
			if (this.m_dicRelation.TryGetValue(new ValueTuple<int, int>(indexA, indexB), out result))
			{
				return result;
			}
			NKCTopologicalSort<T>.Relation relation = new NKCTopologicalSort<T>.Relation(this.dRelationFunction(this.GetObject(indexA), this.GetObject(indexB)));
			NKCTopologicalSort<T>.Relation value = new NKCTopologicalSort<T>.Relation(relation.hasDependancy, -1 * relation.compairison);
			this.m_dicRelation[new ValueTuple<int, int>(indexA, indexB)] = relation;
			this.m_dicRelation[new ValueTuple<int, int>(indexB, indexA)] = value;
			return relation;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000F1BE0 File Offset: 0x000EFDE0
		private bool IsADefendsOnB(int indexA, int indexB)
		{
			NKCTopologicalSort<T>.Relation relation = this.GetRelation(indexA, indexB);
			return relation.hasDependancy && relation.compairison > 0;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000F1C09 File Offset: 0x000EFE09
		public NKCTopologicalSort(NKCTopologicalSort<T>.RelationFunction relationFunc)
		{
			this.dRelationFunction = relationFunc;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000F1C24 File Offset: 0x000EFE24
		public List<T> DoSort(List<T> lstTarget)
		{
			this.m_lstTarget = lstTarget;
			List<T> result;
			try
			{
				List<NKCTopologicalSort<T>.Graph> lstSortedGraph = this.MakeGraphGropus(lstTarget);
				if (this.m_bHasCycle)
				{
					result = null;
				}
				else
				{
					List<int> list = this.SortAndMerge(lstSortedGraph);
					List<T> list2 = new List<T>();
					foreach (int index in list)
					{
						list2.Add(this.GetObject(index));
					}
					list2.Reverse();
					result = list2;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				result = lstTarget;
			}
			return result;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000F1CC8 File Offset: 0x000EFEC8
		private List<NKCTopologicalSort<T>.Graph> MakeGraphGropus(List<T> lstTarget)
		{
			NKCTopologicalSort<T>.<>c__DisplayClass13_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.lstTarget = lstTarget;
			List<HashSet<int>> list = new List<HashSet<int>>();
			CS$<>8__locals1.dicGroupID = new Dictionary<int, int>();
			CS$<>8__locals1.currentGroupID = 0;
			for (int i = 0; i < CS$<>8__locals1.lstTarget.Count; i++)
			{
				if (!CS$<>8__locals1.dicGroupID.ContainsKey(i))
				{
					CS$<>8__locals1.hsCurrentGroup = new HashSet<int>();
					list.Add(CS$<>8__locals1.hsCurrentGroup);
					CS$<>8__locals1.dicGroupID[i] = CS$<>8__locals1.currentGroupID;
					CS$<>8__locals1.hsCurrentGroup.Add(i);
					this.<MakeGraphGropus>g__GroupRelation|13_0(i, CS$<>8__locals1.currentGroupID, ref CS$<>8__locals1);
					int currentGroupID = CS$<>8__locals1.currentGroupID;
					CS$<>8__locals1.currentGroupID = currentGroupID + 1;
				}
			}
			List<NKCTopologicalSort<T>.Graph> list2 = new List<NKCTopologicalSort<T>.Graph>();
			foreach (HashSet<int> lstIndices in list)
			{
				NKCTopologicalSort<T>.Graph item = this.BuildPrunedGraph(lstIndices);
				if (this.m_bHasCycle)
				{
					return list2;
				}
				list2.Add(item);
			}
			return list2;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000F1DE4 File Offset: 0x000EFFE4
		private NKCTopologicalSort<T>.Graph BuildPrunedGraph(IEnumerable<int> lstIndices)
		{
			NKCTopologicalSort<T>.Graph graph = new NKCTopologicalSort<T>.Graph();
			foreach (int index in lstIndices)
			{
				NKCTopologicalSort<T>.Node node = this.MakePrunedNode(index, lstIndices);
				graph.AddNode(index, node);
			}
			HashSet<int> hashSet = new HashSet<int>();
			Stack<int> stack = new Stack<int>();
			foreach (KeyValuePair<int, NKCTopologicalSort<T>.Node> keyValuePair in graph.m_dicNode)
			{
				if (this.HasCycle(keyValuePair.Key, graph, ref hashSet, ref stack))
				{
					this.m_bHasCycle = true;
					return graph;
				}
			}
			return graph;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000F1EB0 File Offset: 0x000F00B0
		private bool HasCycle(int index, NKCTopologicalSort<T>.Graph graph, ref HashSet<int> hsVisited, ref Stack<int> stkRoute)
		{
			if (!hsVisited.Contains(index))
			{
				hsVisited.Add(index);
				stkRoute.Push(index);
				foreach (int num in graph.GetNode(index).GetDependencies)
				{
					if (!hsVisited.Contains(num) && this.HasCycle(num, graph, ref hsVisited, ref stkRoute))
					{
						return true;
					}
					if (stkRoute.Contains(num))
					{
						Debug.LogWarning("Cycle Detected");
						return true;
					}
				}
				if (stkRoute.Pop() != index)
				{
					Debug.LogError("Logic Error");
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000F1F68 File Offset: 0x000F0168
		private void BreakCycleDFS(int index, NKCTopologicalSort<T>.Graph graph, ref HashSet<int> hsVisited, ref Stack<int> stkRoute, ref List<ValueTuple<int, int>> lstToRemove)
		{
			if (!hsVisited.Contains(index))
			{
				hsVisited.Add(index);
				stkRoute.Push(index);
				foreach (int num in graph.GetNode(index).GetDependencies)
				{
					if (!hsVisited.Contains(num))
					{
						this.BreakCycleDFS(num, graph, ref hsVisited, ref stkRoute, ref lstToRemove);
					}
					else if (stkRoute.Contains(num))
					{
						Debug.LogWarning("Cycle Detected!");
						List<int> list = new List<int>(stkRoute.ToArray());
						list.Reverse();
						int count = list.IndexOf(num);
						list.RemoveRange(0, count);
						int num2 = list[0];
						foreach (int num3 in list)
						{
							if (num3 < num2)
							{
								num2 = num3;
							}
						}
						int num4 = list.IndexOf(num2);
						int index2 = (num4 + 1) % list.Count;
						lstToRemove.Add(new ValueTuple<int, int>(list[num4], list[index2]));
					}
				}
				if (stkRoute.Pop() != index)
				{
					Debug.LogError("Logic Error");
				}
			}
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000F20C8 File Offset: 0x000F02C8
		private NKCTopologicalSort<T>.Node MakePrunedNode(int index, IEnumerable<int> lstIndices)
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (int num in lstIndices)
			{
				if (index != num && this.IsADefendsOnB(index, num))
				{
					bool flag = false;
					foreach (int num2 in lstIndices)
					{
						if (num2 != index && num2 != num && this.IsADefendsOnB(index, num2) && this.IsADefendsOnB(num2, num))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						hashSet.Add(num);
					}
				}
			}
			return new NKCTopologicalSort<T>.Node(index, this.GetObject(index), hashSet);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000F2194 File Offset: 0x000F0394
		private List<int> SortAndMerge(List<NKCTopologicalSort<T>.Graph> lstSortedGraph)
		{
			List<int> list = new List<int>();
			foreach (NKCTopologicalSort<T>.Graph graph in lstSortedGraph)
			{
				graph.Sort();
				if (graph.UnsortedList != null)
				{
					List<int> unsortedList = graph.UnsortedList;
					unsortedList.Sort((int a, int b) => this.GetRelation(b, a).compairison);
					List<int> lstB = this.MergeSortedIndices(graph.SortedList, unsortedList);
					list = this.MergeSortedIndices(list, lstB);
				}
				else
				{
					list = this.MergeSortedIndices(list, graph.SortedList);
				}
			}
			return list;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000F2234 File Offset: 0x000F0434
		private List<int> MergeSortedIndices(List<int> lstA, List<int> lstB)
		{
			NKCTopologicalSort<T>.<>c__DisplayClass19_0 CS$<>8__locals1;
			CS$<>8__locals1.lstA = lstA;
			CS$<>8__locals1.lstB = lstB;
			CS$<>8__locals1.retval = new List<int>(CS$<>8__locals1.lstA.Count + CS$<>8__locals1.lstB.Count);
			CS$<>8__locals1.indexA = 0;
			CS$<>8__locals1.indexB = 0;
			while (CS$<>8__locals1.indexA < CS$<>8__locals1.lstA.Count || CS$<>8__locals1.indexB < CS$<>8__locals1.lstB.Count)
			{
				if (CS$<>8__locals1.indexA >= CS$<>8__locals1.lstA.Count)
				{
					NKCTopologicalSort<T>.<MergeSortedIndices>g__AddB|19_1(ref CS$<>8__locals1);
				}
				else if (CS$<>8__locals1.indexB >= CS$<>8__locals1.lstB.Count)
				{
					NKCTopologicalSort<T>.<MergeSortedIndices>g__AddA|19_0(ref CS$<>8__locals1);
				}
				else
				{
					NKCTopologicalSort<T>.Relation relation = this.GetRelation(CS$<>8__locals1.lstA[CS$<>8__locals1.indexA], CS$<>8__locals1.lstB[CS$<>8__locals1.indexB]);
					if (relation.compairison < 0)
					{
						NKCTopologicalSort<T>.<MergeSortedIndices>g__AddB|19_1(ref CS$<>8__locals1);
					}
					else if (relation.compairison > 0)
					{
						NKCTopologicalSort<T>.<MergeSortedIndices>g__AddA|19_0(ref CS$<>8__locals1);
					}
					else if (CS$<>8__locals1.lstA[CS$<>8__locals1.indexA] < CS$<>8__locals1.lstB[CS$<>8__locals1.indexB])
					{
						NKCTopologicalSort<T>.<MergeSortedIndices>g__AddA|19_0(ref CS$<>8__locals1);
					}
					else
					{
						NKCTopologicalSort<T>.<MergeSortedIndices>g__AddB|19_1(ref CS$<>8__locals1);
					}
				}
			}
			return CS$<>8__locals1.retval;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000F237C File Offset: 0x000F057C
		[CompilerGenerated]
		private void <MakeGraphGropus>g__GroupRelation|13_0(int targetIndex, int groupID, ref NKCTopologicalSort<T>.<>c__DisplayClass13_0 A_3)
		{
			for (int i = 0; i < A_3.lstTarget.Count; i++)
			{
				if (!A_3.dicGroupID.ContainsKey(i) && i != targetIndex && this.GetRelation(targetIndex, i).hasDependancy)
				{
					A_3.dicGroupID[i] = A_3.currentGroupID;
					A_3.hsCurrentGroup.Add(i);
					this.<MakeGraphGropus>g__GroupRelation|13_0(i, A_3.currentGroupID, ref A_3);
				}
			}
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000F23FC File Offset: 0x000F05FC
		[CompilerGenerated]
		internal static void <MergeSortedIndices>g__AddA|19_0(ref NKCTopologicalSort<T>.<>c__DisplayClass19_0 A_0)
		{
			A_0.retval.Add(A_0.lstA[A_0.indexA]);
			int indexA = A_0.indexA;
			A_0.indexA = indexA + 1;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000F2438 File Offset: 0x000F0638
		[CompilerGenerated]
		internal static void <MergeSortedIndices>g__AddB|19_1(ref NKCTopologicalSort<T>.<>c__DisplayClass19_0 A_0)
		{
			A_0.retval.Add(A_0.lstB[A_0.indexB]);
			int indexB = A_0.indexB;
			A_0.indexB = indexB + 1;
		}

		// Token: 0x04003038 RID: 12344
		private NKCTopologicalSort<T>.RelationFunction dRelationFunction;

		// Token: 0x04003039 RID: 12345
		private bool m_bHasCycle;

		// Token: 0x0400303A RID: 12346
		private Dictionary<ValueTuple<int, int>, NKCTopologicalSort<T>.Relation> m_dicRelation = new Dictionary<ValueTuple<int, int>, NKCTopologicalSort<T>.Relation>();

		// Token: 0x0400303B RID: 12347
		private List<T> m_lstTarget;

		// Token: 0x020012E2 RID: 4834
		private class Node
		{
			// Token: 0x0600A4A4 RID: 42148 RVA: 0x00344116 File Offset: 0x00342316
			public Node(int index, T target, HashSet<int> dependancy)
			{
				this.Index = index;
				this.Target = target;
				this.m_hsDependancy = dependancy;
			}

			// Token: 0x0600A4A5 RID: 42149 RVA: 0x00344133 File Offset: 0x00342333
			public bool IsDependantTo(int index)
			{
				return this.m_hsDependancy.Contains(index);
			}

			// Token: 0x0600A4A6 RID: 42150 RVA: 0x00344141 File Offset: 0x00342341
			public void RemoveDependancy(int index)
			{
				this.m_hsDependancy.Remove(index);
			}

			// Token: 0x170017E5 RID: 6117
			// (get) Token: 0x0600A4A7 RID: 42151 RVA: 0x00344150 File Offset: 0x00342350
			public IEnumerable<int> GetDependencies
			{
				get
				{
					return this.m_hsDependancy;
				}
			}

			// Token: 0x170017E6 RID: 6118
			// (get) Token: 0x0600A4A8 RID: 42152 RVA: 0x00344158 File Offset: 0x00342358
			public int DependancyCount
			{
				get
				{
					return this.m_hsDependancy.Count;
				}
			}

			// Token: 0x0400973A RID: 38714
			public readonly int Index;

			// Token: 0x0400973B RID: 38715
			public readonly T Target;

			// Token: 0x0400973C RID: 38716
			private readonly HashSet<int> m_hsDependancy;
		}

		// Token: 0x020012E3 RID: 4835
		private class Graph
		{
			// Token: 0x0600A4A9 RID: 42153 RVA: 0x00344165 File Offset: 0x00342365
			public void AddNode(int index, NKCTopologicalSort<T>.Node node)
			{
				this.m_dicNode.Add(index, node);
			}

			// Token: 0x170017E7 RID: 6119
			// (get) Token: 0x0600A4AA RID: 42154 RVA: 0x00344174 File Offset: 0x00342374
			public List<int> SortedList
			{
				get
				{
					return this.m_lstSorted;
				}
			}

			// Token: 0x170017E8 RID: 6120
			// (get) Token: 0x0600A4AB RID: 42155 RVA: 0x0034417C File Offset: 0x0034237C
			public List<int> UnsortedList
			{
				get
				{
					return this.m_lstUnSorted;
				}
			}

			// Token: 0x0600A4AC RID: 42156 RVA: 0x00344184 File Offset: 0x00342384
			public void Sort()
			{
				this.m_lstSorted.Clear();
				this.TopologicalSort();
			}

			// Token: 0x0600A4AD RID: 42157 RVA: 0x00344197 File Offset: 0x00342397
			public NKCTopologicalSort<T>.Node GetNode(int index)
			{
				return this.m_dicNode[index];
			}

			// Token: 0x0600A4AE RID: 42158 RVA: 0x003441A8 File Offset: 0x003423A8
			private void TopologicalSort()
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				foreach (NKCTopologicalSort<T>.Node node in this.m_dicNode.Values)
				{
					foreach (int key in node.GetDependencies)
					{
						if (dictionary.ContainsKey(key))
						{
							dictionary[key]++;
						}
						else
						{
							dictionary.Add(key, 1);
						}
					}
				}
				Queue<int> queue = new Queue<int>();
				foreach (KeyValuePair<int, NKCTopologicalSort<T>.Node> keyValuePair in this.m_dicNode)
				{
					if (!dictionary.ContainsKey(keyValuePair.Key) || dictionary[keyValuePair.Key] == 0)
					{
						queue.Enqueue(keyValuePair.Key);
					}
				}
				HashSet<int> hashSet = new HashSet<int>();
				while (queue.Count > 0)
				{
					int num = queue.Dequeue();
					if (hashSet.Contains(num))
					{
						Debug.LogError("logic error?");
					}
					else
					{
						this.m_lstSorted.Add(num);
						hashSet.Add(num);
						foreach (int num2 in this.GetNode(num).GetDependencies)
						{
							dictionary[num2]--;
							if (dictionary[num2] == 0)
							{
								queue.Enqueue(num2);
							}
						}
					}
				}
				if (this.m_lstSorted.Count != this.m_dicNode.Count)
				{
					Debug.LogError("Top sort impossible case!. doing fallback");
					this.m_lstUnSorted = new List<int>();
					foreach (KeyValuePair<int, NKCTopologicalSort<T>.Node> keyValuePair2 in this.m_dicNode)
					{
						if (!this.m_lstSorted.Contains(keyValuePair2.Key))
						{
							this.m_lstUnSorted.Add(keyValuePair2.Key);
						}
					}
				}
			}

			// Token: 0x0400973D RID: 38717
			public Dictionary<int, NKCTopologicalSort<T>.Node> m_dicNode = new Dictionary<int, NKCTopologicalSort<T>.Node>();

			// Token: 0x0400973E RID: 38718
			private List<int> m_lstSorted = new List<int>();

			// Token: 0x0400973F RID: 38719
			private List<int> m_lstUnSorted;
		}

		// Token: 0x020012E4 RID: 4836
		// (Invoke) Token: 0x0600A4B1 RID: 42161
		public delegate ValueTuple<bool, int> RelationFunction(T a, T b);

		// Token: 0x020012E5 RID: 4837
		private struct Relation
		{
			// Token: 0x0600A4B4 RID: 42164 RVA: 0x0034442E File Offset: 0x0034262E
			public Relation(bool _dependancy, int _compairison)
			{
				this.hasDependancy = _dependancy;
				this.compairison = _compairison;
			}

			// Token: 0x0600A4B5 RID: 42165 RVA: 0x0034443E File Offset: 0x0034263E
			public Relation(ValueTuple<bool, int> relation)
			{
				this.hasDependancy = relation.Item1;
				this.compairison = relation.Item2;
			}

			// Token: 0x0600A4B6 RID: 42166 RVA: 0x00344458 File Offset: 0x00342658
			public override string ToString()
			{
				return string.Format("{0}, {1}", this.hasDependancy, this.compairison);
			}

			// Token: 0x04009740 RID: 38720
			public bool hasDependancy;

			// Token: 0x04009741 RID: 38721
			public int compairison;
		}
	}
}

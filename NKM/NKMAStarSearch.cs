using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x02000396 RID: 918
	public class NKMAStarSearch
	{
		// Token: 0x06001791 RID: 6033 RVA: 0x0005F110 File Offset: 0x0005D310
		public static float Heuristic(NKMAStarSearchLocation a, NKMAStarSearchLocation b)
		{
			return (float)(Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y));
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0005F138 File Offset: 0x0005D338
		public NKMAStarSearch(SquareGrid graph, NKMAStarSearchLocation start, NKMAStarSearchLocation goal)
		{
			this.start = start;
			this.goal = goal;
			PriorityQueue<NKMAStarSearchLocation> priorityQueue = new PriorityQueue<NKMAStarSearchLocation>();
			priorityQueue.Enqueue(start, 0f);
			this.cameFrom.Add(start, start);
			this.costSoFar.Add(start, 0f);
			while ((float)priorityQueue.Count > 0f)
			{
				NKMAStarSearchLocation nkmastarSearchLocation = priorityQueue.Dequeue();
				if (nkmastarSearchLocation.Equals(goal))
				{
					break;
				}
				foreach (NKMAStarSearchLocation nkmastarSearchLocation2 in graph.Neighbors(nkmastarSearchLocation))
				{
					float num = this.costSoFar[nkmastarSearchLocation] + graph.Cost(nkmastarSearchLocation, nkmastarSearchLocation2);
					if (!this.costSoFar.ContainsKey(nkmastarSearchLocation2) || num < this.costSoFar[nkmastarSearchLocation2])
					{
						if (this.costSoFar.ContainsKey(nkmastarSearchLocation2))
						{
							this.costSoFar.Remove(nkmastarSearchLocation2);
							this.cameFrom.Remove(nkmastarSearchLocation2);
						}
						this.costSoFar.Add(nkmastarSearchLocation2, num);
						this.cameFrom.Add(nkmastarSearchLocation2, nkmastarSearchLocation);
						float priority = num + NKMAStarSearch.Heuristic(nkmastarSearchLocation2, goal);
						priorityQueue.Enqueue(nkmastarSearchLocation2, priority);
					}
				}
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0005F298 File Offset: 0x0005D498
		public List<NKMAStarSearchLocation> FindPath()
		{
			List<NKMAStarSearchLocation> list = new List<NKMAStarSearchLocation>();
			NKMAStarSearchLocation nkmastarSearchLocation = this.goal;
			while (!nkmastarSearchLocation.Equals(this.start))
			{
				if (!this.cameFrom.ContainsKey(nkmastarSearchLocation))
				{
					return new List<NKMAStarSearchLocation>();
				}
				list.Add(nkmastarSearchLocation);
				nkmastarSearchLocation = this.cameFrom[nkmastarSearchLocation];
			}
			list.Reverse();
			return list;
		}

		// Token: 0x04000FF5 RID: 4085
		public Dictionary<NKMAStarSearchLocation, NKMAStarSearchLocation> cameFrom = new Dictionary<NKMAStarSearchLocation, NKMAStarSearchLocation>();

		// Token: 0x04000FF6 RID: 4086
		public Dictionary<NKMAStarSearchLocation, float> costSoFar = new Dictionary<NKMAStarSearchLocation, float>();

		// Token: 0x04000FF7 RID: 4087
		private NKMAStarSearchLocation start;

		// Token: 0x04000FF8 RID: 4088
		private NKMAStarSearchLocation goal;
	}
}

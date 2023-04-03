using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007A6 RID: 1958
	public class NKCDiveGameSectorLines : MonoBehaviour
	{
		// Token: 0x06004D1B RID: 19739 RVA: 0x001724BC File Offset: 0x001706BC
		public void Init()
		{
			for (int i = 0; i < this.m_lstLinesEven.Count; i++)
			{
				this.m_dlstLinesEven.Add(new List<GameObject>());
			}
			for (int i = 0; i < this.m_lstLinesEven0.Count; i++)
			{
				this.m_dlstLinesEven[0].Add(this.m_lstLinesEven0[i]);
			}
			for (int i = 0; i < this.m_lstLinesEven1.Count; i++)
			{
				this.m_dlstLinesEven[1].Add(this.m_lstLinesEven1[i]);
			}
			for (int i = 0; i < this.m_lstLinesEven2.Count; i++)
			{
				this.m_dlstLinesEven[2].Add(this.m_lstLinesEven2[i]);
			}
			for (int i = 0; i < this.m_lstLinesEven3.Count; i++)
			{
				this.m_dlstLinesEven[3].Add(this.m_lstLinesEven3[i]);
			}
			for (int i = 0; i < this.m_lstLinesOdd.Count; i++)
			{
				this.m_dlstLinesOdd.Add(new List<GameObject>());
			}
			for (int i = 0; i < this.m_lstLinesOdd0.Count; i++)
			{
				this.m_dlstLinesOdd[0].Add(this.m_lstLinesOdd0[i]);
			}
			for (int i = 0; i < this.m_lstLinesOdd1.Count; i++)
			{
				this.m_dlstLinesOdd[1].Add(this.m_lstLinesOdd1[i]);
			}
			for (int i = 0; i < this.m_lstLinesOdd2.Count; i++)
			{
				this.m_dlstLinesOdd[2].Add(this.m_lstLinesOdd2[i]);
			}
			for (int i = 0; i < this.m_lstLinesOdd3.Count; i++)
			{
				this.m_dlstLinesOdd[3].Add(this.m_lstLinesOdd3[i]);
			}
			for (int i = 0; i < this.m_lstLinesOdd4.Count; i++)
			{
				this.m_dlstLinesOdd[4].Add(this.m_lstLinesOdd4[i]);
			}
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x001726E4 File Offset: 0x001708E4
		private void SetInActiveList(List<GameObject> lstLines)
		{
			for (int i = 0; i < lstLines.Count; i++)
			{
				NKCUtil.SetGameobjectActive(lstLines[i], false);
			}
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x00172714 File Offset: 0x00170914
		private void SetActiveList(List<GameObject> lstLines)
		{
			for (int i = 0; i < lstLines.Count; i++)
			{
				NKCUtil.SetGameobjectActive(lstLines[i], true);
			}
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x00172741 File Offset: 0x00170941
		private void SetActiveGO(List<GameObject> lstLines, int index)
		{
			if (index < 0 || index >= lstLines.Count)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(lstLines[index], true);
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x00172760 File Offset: 0x00170960
		private int GetStartIndex(bool bEven, int maxEvenCount, int maxOddCount, int realSetSize)
		{
			int num;
			if (bEven)
			{
				num = maxEvenCount / 2 - 1;
				num -= realSetSize / 2 - 1;
			}
			else
			{
				num = maxOddCount / 2;
				num -= realSetSize / 2;
			}
			return num;
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x00172790 File Offset: 0x00170990
		public void OpenFromMyPos(int realSetSize, int toRealSetSize, int uiIndex, int realIndex, bool bFromStart, bool bToBoss)
		{
			bool flag = realSetSize % 2 == 0;
			bool flag2 = toRealSetSize % 2 == 0;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetInActiveList(this.m_lstLinesEven);
			this.SetInActiveList(this.m_lstLinesOdd);
			this.SetInActiveList(this.m_lstBossLinesEven);
			this.SetInActiveList(this.m_lstBossLinesOdd);
			this.SetInActiveList(this.m_lstStartLinesEven);
			this.SetInActiveList(this.m_lstStartLinesOdd);
			if (bFromStart)
			{
				if (flag2)
				{
					int startIndex = this.GetStartIndex(true, this.m_lstStartLinesEven.Count, this.m_lstStartLinesOdd.Count, toRealSetSize);
					for (int i = 0; i < this.m_lstStartLinesEven.Count; i++)
					{
						if (i >= startIndex && i < toRealSetSize + startIndex)
						{
							this.SetActiveGO(this.m_lstStartLinesEven, i);
						}
					}
				}
				else
				{
					int startIndex2 = this.GetStartIndex(false, this.m_lstStartLinesEven.Count, this.m_lstStartLinesOdd.Count, toRealSetSize);
					for (int i = 0; i < this.m_lstStartLinesOdd.Count; i++)
					{
						if (i >= startIndex2 && i < toRealSetSize + startIndex2)
						{
							this.SetActiveGO(this.m_lstStartLinesOdd, i);
						}
					}
				}
			}
			else if (bToBoss)
			{
				if (flag)
				{
					this.SetActiveGO(this.m_lstBossLinesEven, uiIndex);
				}
				else
				{
					this.SetActiveGO(this.m_lstBossLinesOdd, uiIndex);
				}
			}
			else if (flag)
			{
				this.SetActiveGO(this.m_lstLinesEven, uiIndex);
				if (uiIndex == 0 || uiIndex == this.m_dlstLinesEven.Count - 1)
				{
					this.SetActiveList(this.m_dlstLinesEven[uiIndex]);
				}
				else if (realIndex == 0)
				{
					for (int i = 0; i < this.m_dlstLinesEven[uiIndex].Count; i++)
					{
						if (i == 0)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], true);
						}
					}
				}
				else if (realIndex == realSetSize - 1)
				{
					for (int i = 0; i < this.m_dlstLinesEven[uiIndex].Count; i++)
					{
						if (i == this.m_dlstLinesEven[uiIndex].Count - 1)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], true);
						}
					}
				}
				else
				{
					this.SetActiveList(this.m_dlstLinesEven[uiIndex]);
				}
			}
			else
			{
				this.SetActiveGO(this.m_lstLinesOdd, uiIndex);
				if (uiIndex == 0 || uiIndex == this.m_dlstLinesOdd.Count - 1)
				{
					this.SetActiveList(this.m_dlstLinesOdd[uiIndex]);
				}
				else if (realSetSize == 1)
				{
					for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
					{
						if (i == 1)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
						}
					}
				}
				else if (realIndex == 0)
				{
					for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
					{
						if (i == 0)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
						}
					}
				}
				else if (realIndex == realSetSize - 1)
				{
					for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
					{
						if (i == this.m_dlstLinesOdd[uiIndex].Count - 1)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
						}
					}
				}
				else
				{
					this.SetActiveList(this.m_dlstLinesOdd[uiIndex]);
				}
			}
			if (this.m_Animator != null)
			{
				this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SECTOR_LINES_INTRO");
			}
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x00172B80 File Offset: 0x00170D80
		public void OpenFromSelectedMyPos(int realSetSize, int toRealSetSize, int uiIndex, int toUIIndex, int realIndex, int toRealIndex, bool bFromStart, bool bToBoss)
		{
			bool flag = realSetSize % 2 == 0;
			bool flag2 = toRealSetSize % 2 == 0;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetInActiveList(this.m_lstLinesEven);
			this.SetInActiveList(this.m_lstLinesOdd);
			this.SetInActiveList(this.m_lstBossLinesEven);
			this.SetInActiveList(this.m_lstBossLinesOdd);
			this.SetInActiveList(this.m_lstStartLinesEven);
			this.SetInActiveList(this.m_lstStartLinesOdd);
			if (bFromStart)
			{
				if (flag2)
				{
					this.SetActiveGO(this.m_lstStartLinesEven, toUIIndex);
					return;
				}
				this.SetActiveGO(this.m_lstStartLinesOdd, toUIIndex);
				return;
			}
			else if (bToBoss)
			{
				if (flag)
				{
					this.SetActiveGO(this.m_lstBossLinesEven, uiIndex);
					return;
				}
				this.SetActiveGO(this.m_lstBossLinesOdd, uiIndex);
				return;
			}
			else
			{
				int num;
				if (uiIndex >= 1)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
				if (flag)
				{
					this.SetActiveGO(this.m_lstLinesEven, uiIndex);
					for (int i = 0; i < this.m_dlstLinesEven[uiIndex].Count; i++)
					{
						if (i != num + (toUIIndex - uiIndex))
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], true);
						}
					}
					return;
				}
				this.SetActiveGO(this.m_lstLinesOdd, uiIndex);
				if (realSetSize == 1)
				{
					for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
					{
						if (i == 1)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
						}
					}
					return;
				}
				for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
				{
					if (i != num + (toUIIndex - uiIndex))
					{
						NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
					}
				}
				return;
			}
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x00172D68 File Offset: 0x00170F68
		public void OpenFromSelected(int realSetSize, int uiIndex, int realIndex, bool bToBoss)
		{
			bool flag = realSetSize % 2 == 0;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetInActiveList(this.m_lstLinesEven);
			this.SetInActiveList(this.m_lstLinesOdd);
			this.SetInActiveList(this.m_lstBossLinesEven);
			this.SetInActiveList(this.m_lstBossLinesOdd);
			this.SetInActiveList(this.m_lstStartLinesEven);
			this.SetInActiveList(this.m_lstStartLinesOdd);
			if (bToBoss)
			{
				if (flag)
				{
					this.SetActiveGO(this.m_lstBossLinesEven, uiIndex);
				}
				else
				{
					this.SetActiveGO(this.m_lstBossLinesOdd, uiIndex);
				}
			}
			else if (flag)
			{
				this.SetActiveGO(this.m_lstLinesEven, uiIndex);
				if (uiIndex == 0 || uiIndex == this.m_dlstLinesEven.Count - 1)
				{
					this.SetActiveList(this.m_dlstLinesEven[uiIndex]);
				}
				else if (realIndex == 0)
				{
					for (int i = 0; i < this.m_dlstLinesEven[uiIndex].Count; i++)
					{
						if (i == 0)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], true);
						}
					}
				}
				else if (realIndex == realSetSize - 1)
				{
					for (int i = 0; i < this.m_dlstLinesEven[uiIndex].Count; i++)
					{
						if (i == this.m_dlstLinesEven[uiIndex].Count - 1)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesEven[uiIndex][i], true);
						}
					}
				}
				else
				{
					this.SetActiveList(this.m_dlstLinesEven[uiIndex]);
				}
			}
			else
			{
				this.SetActiveGO(this.m_lstLinesOdd, uiIndex);
				if (uiIndex == 0 || uiIndex == this.m_dlstLinesOdd.Count - 1)
				{
					this.SetActiveList(this.m_dlstLinesOdd[uiIndex]);
				}
				else if (realSetSize == 1)
				{
					for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
					{
						if (i == 1)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
						}
					}
				}
				else if (realIndex == 0)
				{
					for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
					{
						if (i == 0)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
						}
					}
				}
				else if (realIndex == realSetSize - 1)
				{
					for (int i = 0; i < this.m_dlstLinesOdd[uiIndex].Count; i++)
					{
						if (i == this.m_dlstLinesOdd[uiIndex].Count - 1)
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_dlstLinesOdd[uiIndex][i], true);
						}
					}
				}
				else
				{
					this.SetActiveList(this.m_dlstLinesOdd[uiIndex]);
				}
			}
			if (this.m_Animator != null)
			{
				this.m_Animator.Play("NKM_UI_DIVE_PROCESS_SECTOR_LINES_INTRO");
			}
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0017309D File Offset: 0x0017129D
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04003D06 RID: 15622
		public List<GameObject> m_lstLinesEven;

		// Token: 0x04003D07 RID: 15623
		public List<GameObject> m_lstLinesOdd;

		// Token: 0x04003D08 RID: 15624
		public List<GameObject> m_lstLinesEven0;

		// Token: 0x04003D09 RID: 15625
		public List<GameObject> m_lstLinesEven1;

		// Token: 0x04003D0A RID: 15626
		public List<GameObject> m_lstLinesEven2;

		// Token: 0x04003D0B RID: 15627
		public List<GameObject> m_lstLinesEven3;

		// Token: 0x04003D0C RID: 15628
		public List<GameObject> m_lstLinesOdd0;

		// Token: 0x04003D0D RID: 15629
		public List<GameObject> m_lstLinesOdd1;

		// Token: 0x04003D0E RID: 15630
		public List<GameObject> m_lstLinesOdd2;

		// Token: 0x04003D0F RID: 15631
		public List<GameObject> m_lstLinesOdd3;

		// Token: 0x04003D10 RID: 15632
		public List<GameObject> m_lstLinesOdd4;

		// Token: 0x04003D11 RID: 15633
		private List<List<GameObject>> m_dlstLinesEven = new List<List<GameObject>>();

		// Token: 0x04003D12 RID: 15634
		private List<List<GameObject>> m_dlstLinesOdd = new List<List<GameObject>>();

		// Token: 0x04003D13 RID: 15635
		public List<GameObject> m_lstBossLinesEven;

		// Token: 0x04003D14 RID: 15636
		public List<GameObject> m_lstBossLinesOdd;

		// Token: 0x04003D15 RID: 15637
		public List<GameObject> m_lstStartLinesEven;

		// Token: 0x04003D16 RID: 15638
		public List<GameObject> m_lstStartLinesOdd;

		// Token: 0x04003D17 RID: 15639
		public Animator m_Animator;
	}
}

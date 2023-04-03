using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200076F RID: 1903
	public class NKCUIRectMoveSet : MonoBehaviour
	{
		// Token: 0x06004BF4 RID: 19444 RVA: 0x0016B8B8 File Offset: 0x00169AB8
		public void PlayMoveSet(string Name, bool bAnimate = true, NKCUIRectMove.OnTrackingComplete onComplete = null)
		{
			if (this.m_lstMoveSet != null)
			{
				NKCUIRectMoveSet.MoveSet moveset = this.m_lstMoveSet.Find((NKCUIRectMoveSet.MoveSet x) => x.Name == Name);
				this.PlayMoveSet(moveset, bAnimate, onComplete);
			}
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0016B8FC File Offset: 0x00169AFC
		private void PlayMoveSet(NKCUIRectMoveSet.MoveSet moveset, bool bAnimate, NKCUIRectMove.OnTrackingComplete onComplete)
		{
			if (moveset != null)
			{
				base.StopAllCoroutines();
				if (bAnimate && base.gameObject.activeInHierarchy)
				{
					base.StartCoroutine(this._PlayMoveSet(moveset, onComplete));
					return;
				}
				foreach (NKCUIRectMoveSet.MoveEvent moveEvent in moveset.m_lstMoveEvent)
				{
					if (moveEvent.targetRectMove != null)
					{
						moveEvent.targetRectMove.Set(moveEvent.Name);
					}
				}
				if (onComplete != null)
				{
					onComplete();
				}
			}
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0016B99C File Offset: 0x00169B9C
		private IEnumerator _PlayMoveSet(NKCUIRectMoveSet.MoveSet moveset, NKCUIRectMove.OnTrackingComplete onComplete)
		{
			if (moveset.m_lstMoveEvent.Count < 0)
			{
				yield break;
			}
			float ProcessedTime = 0f;
			moveset.m_lstMoveEvent.Sort((NKCUIRectMoveSet.MoveEvent a, NKCUIRectMoveSet.MoveEvent b) => a.StartTime.CompareTo(b.StartTime));
			int currentIndex = 0;
			int lastMoveIndex = 0;
			float moveEventFinishTime = this.GetMoveEventFinishTime(moveset.m_lstMoveEvent[0]);
			for (int i = 1; i < moveset.m_lstMoveEvent.Count; i++)
			{
				NKCUIRectMoveSet.MoveEvent move = moveset.m_lstMoveEvent[i];
				if (this.GetMoveEventFinishTime(move) > moveEventFinishTime)
				{
					lastMoveIndex = i;
				}
			}
			while (currentIndex < moveset.m_lstMoveEvent.Count)
			{
				NKCUIRectMoveSet.MoveEvent moveEvent = moveset.m_lstMoveEvent[currentIndex];
				if (moveEvent.StartTime <= ProcessedTime)
				{
					if (moveEvent.bAnimate)
					{
						if (moveEvent.targetRectMove != null)
						{
							if (lastMoveIndex == currentIndex)
							{
								moveEvent.targetRectMove.Transit(moveEvent.Name, onComplete);
							}
							else
							{
								moveEvent.targetRectMove.Transit(moveEvent.Name, null);
							}
						}
					}
					else
					{
						if (moveEvent.targetRectMove != null)
						{
							moveEvent.targetRectMove.Set(moveEvent.Name);
						}
						if (lastMoveIndex == currentIndex && onComplete != null)
						{
							onComplete();
						}
					}
					int num = currentIndex;
					currentIndex = num + 1;
				}
				else
				{
					yield return null;
					ProcessedTime += Time.deltaTime;
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0016B9BC File Offset: 0x00169BBC
		private float GetMoveEventFinishTime(NKCUIRectMoveSet.MoveEvent move)
		{
			float result;
			if (move.bAnimate && move.targetRectMove != null)
			{
				NKCUIRectMove.MoveInfo moveInfo = move.targetRectMove.GetMoveInfo(move.Name);
				result = move.StartTime + moveInfo.TrackTime;
			}
			else
			{
				result = move.StartTime;
			}
			return result;
		}

		// Token: 0x04003A61 RID: 14945
		public List<NKCUIRectMoveSet.MoveSet> m_lstMoveSet;

		// Token: 0x02001444 RID: 5188
		[Serializable]
		public class MoveEvent
		{
			// Token: 0x04009DE7 RID: 40423
			public NKCUIRectMove targetRectMove;

			// Token: 0x04009DE8 RID: 40424
			public string Name;

			// Token: 0x04009DE9 RID: 40425
			public float StartTime;

			// Token: 0x04009DEA RID: 40426
			public bool bAnimate = true;
		}

		// Token: 0x02001445 RID: 5189
		[Serializable]
		public class MoveSet
		{
			// Token: 0x04009DEB RID: 40427
			public string Name;

			// Token: 0x04009DEC RID: 40428
			public List<NKCUIRectMoveSet.MoveEvent> m_lstMoveEvent;
		}
	}
}

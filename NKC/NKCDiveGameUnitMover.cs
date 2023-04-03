using System;
using System.Collections;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007AC RID: 1964
	public class NKCDiveGameUnitMover : MonoBehaviour
	{
		// Token: 0x06004D63 RID: 19811 RVA: 0x00174D7E File Offset: 0x00172F7E
		private void SetMoveCoroutine(Coroutine _Coroutine)
		{
			this.m_MoveCoroutine = _Coroutine;
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x00174D87 File Offset: 0x00172F87
		private Coroutine GetMoveCoroutine()
		{
			return this.m_MoveCoroutine;
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x00174D8F File Offset: 0x00172F8F
		public void SetPause(bool bSet)
		{
			this.m_bPause = bSet;
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x00174D98 File Offset: 0x00172F98
		public bool IsRunning()
		{
			return this.m_bRunning;
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x00174DA0 File Offset: 0x00172FA0
		private void CommonProcess(NKCDiveGameUnitMover.OnCompleteMove _OnCompleteMove)
		{
			this.m_OnCompleteMove = _OnCompleteMove;
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x00174DA9 File Offset: 0x00172FA9
		public void Move(Vector3 _EndPos, float _fTrackingTime, NKCDiveGameUnitMover.OnCompleteMove _OnCompleteMove = null)
		{
			this.CommonProcess(_OnCompleteMove);
			if (this.IsRunning())
			{
				this.Stop();
			}
			this.m_MoveCoroutine = base.StartCoroutine(this._Move(_EndPos, _fTrackingTime));
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x00174DD4 File Offset: 0x00172FD4
		private IEnumerator _Move(Vector3 _EndPos, float _fTrackingTime)
		{
			this.m_bRunning = true;
			this.m_bPause = false;
			float fDeltaTime = 0f;
			this.m_EndPos = _EndPos;
			this.m_BeginPos = base.transform.localPosition;
			yield return null;
			fDeltaTime += Time.deltaTime;
			while (fDeltaTime < _fTrackingTime)
			{
				float progress = NKMTrackingFloat.TrackRatio(TRACKING_DATA_TYPE.TDT_SLOWER, fDeltaTime, _fTrackingTime, 3f);
				base.transform.localPosition = NKCUtil.Lerp(this.m_BeginPos, this.m_EndPos, progress);
				yield return null;
				if (!this.m_bPause)
				{
					fDeltaTime += Time.deltaTime;
				}
			}
			base.gameObject.transform.localPosition = this.m_EndPos;
			this.m_bRunning = false;
			if (this.m_OnCompleteMove != null)
			{
				this.m_OnCompleteMove();
			}
			yield break;
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x00174DF1 File Offset: 0x00172FF1
		public void Stop()
		{
			if (this.m_MoveCoroutine != null)
			{
				base.StopCoroutine(this.m_MoveCoroutine);
			}
			this.m_MoveCoroutine = null;
			this.m_bRunning = false;
			this.m_bPause = false;
		}

		// Token: 0x04003D46 RID: 15686
		private NKCDiveGameUnitMover.OnCompleteMove m_OnCompleteMove;

		// Token: 0x04003D47 RID: 15687
		private Vector3 m_BeginPos;

		// Token: 0x04003D48 RID: 15688
		private Vector3 m_EndPos;

		// Token: 0x04003D49 RID: 15689
		private bool m_bRunning;

		// Token: 0x04003D4A RID: 15690
		private bool m_bPause;

		// Token: 0x04003D4B RID: 15691
		private Coroutine m_MoveCoroutine;

		// Token: 0x02001468 RID: 5224
		// (Invoke) Token: 0x0600A8AA RID: 43178
		public delegate void OnCompleteMove();
	}
}

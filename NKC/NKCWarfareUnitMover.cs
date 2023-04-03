using System;
using System.Collections;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007E9 RID: 2025
	public class NKCWarfareUnitMover : MonoBehaviour
	{
		// Token: 0x06005053 RID: 20563 RVA: 0x00184DD0 File Offset: 0x00182FD0
		private void SetMoveCoroutine(Coroutine _Coroutine)
		{
			this.m_MoveCoroutine = _Coroutine;
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x00184DD9 File Offset: 0x00182FD9
		private Coroutine GetMoveCoroutine()
		{
			return this.m_MoveCoroutine;
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x00184DE1 File Offset: 0x00182FE1
		public void SetPause(bool bSet)
		{
			this.m_bPause = bSet;
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x00184DEA File Offset: 0x00182FEA
		public bool IsRunning()
		{
			return this.m_bRunning;
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x00184DF2 File Offset: 0x00182FF2
		private void CommonProcess(NKCWarfareUnitMover.OnCompleteMove _OnCompleteMove)
		{
			this.m_OnCompleteMove = _OnCompleteMove;
			this.m_bPlayMoveEndAni = false;
			this.m_NKCWarfareGameUnit.SetState(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_MOVING);
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x00184E10 File Offset: 0x00183010
		public void Jump(Vector3 _EndPos, float _fTrackingTime, NKCWarfareUnitMover.OnCompleteMove _OnCompleteMove = null)
		{
			_fTrackingTime -= 0.5f;
			if (_fTrackingTime <= 0f)
			{
				_fTrackingTime = 0f;
			}
			this.CommonProcess(_OnCompleteMove);
			if (this.IsRunning())
			{
				this.Stop();
			}
			this.m_MoveCoroutine = base.StartCoroutine(this._Jump(_EndPos, _fTrackingTime));
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x00184E5E File Offset: 0x0018305E
		public void Move(Vector3 _EndPos, float _fTrackingTime, NKCWarfareUnitMover.OnCompleteMove _OnCompleteMove = null)
		{
			this.CommonProcess(_OnCompleteMove);
			if (this.IsRunning())
			{
				this.Stop();
			}
			this.m_NKCWarfareGameUnit.transform.SetAsLastSibling();
			this.m_MoveCoroutine = base.StartCoroutine(this._Move(_EndPos, _fTrackingTime));
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00184E99 File Offset: 0x00183099
		private IEnumerator _Move(Vector3 _EndPos, float _fTrackingTime)
		{
			this.m_bRunning = true;
			this.m_bPause = false;
			float fDeltaTime = 0f;
			this.m_EndPos = _EndPos;
			this.m_BeginPos = this.m_NKCWarfareGameUnit.transform.localPosition;
			yield return null;
			float num = Time.deltaTime;
			if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
			{
				num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
			}
			fDeltaTime += num;
			while (fDeltaTime < _fTrackingTime)
			{
				float progress = NKMTrackingFloat.TrackRatio(TRACKING_DATA_TYPE.TDT_SLOWER, fDeltaTime, _fTrackingTime, 3f);
				this.m_NKCWarfareGameUnit.transform.localPosition = NKCUtil.Lerp(this.m_BeginPos, this.m_EndPos, progress);
				yield return null;
				if (!this.m_bPause)
				{
					num = Time.deltaTime;
					if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
					{
						num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
					}
					fDeltaTime += num;
				}
				if (!this.m_bPlayMoveEndAni && fDeltaTime > _fTrackingTime * 0.7f)
				{
					this.m_bPlayMoveEndAni = true;
					this.m_NKCWarfareGameUnit.PlayClickAni();
				}
			}
			this.m_NKCWarfareGameUnit.gameObject.transform.localPosition = this.m_EndPos;
			this.m_NKCWarfareGameUnit.SetState(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_IDLE);
			if (this.m_OnCompleteMove != null)
			{
				this.m_OnCompleteMove(this.m_NKCWarfareGameUnit);
			}
			this.m_bRunning = false;
			yield break;
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00184EB6 File Offset: 0x001830B6
		private IEnumerator _Jump(Vector3 _EndPos, float _fTrackingTime)
		{
			this.m_bRunning = true;
			this.m_bPause = false;
			float fDeltaTime = 0f;
			this.m_EndPos = _EndPos;
			this.m_BeginPos = this.m_NKCWarfareGameUnit.transform.localPosition;
			yield return null;
			float num = Time.deltaTime;
			if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
			{
				num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
			}
			fDeltaTime += num;
			while (fDeltaTime < _fTrackingTime)
			{
				float progress = NKMTrackingFloat.TrackRatio(TRACKING_DATA_TYPE.TDT_SLOWER, fDeltaTime, _fTrackingTime, 3f);
				float progress2 = NKMTrackingFloat.TrackRatio(TRACKING_DATA_TYPE.TDT_SLOWER, fDeltaTime, _fTrackingTime / 3f, 3f);
				float progress3 = NKMTrackingFloat.TrackRatio(TRACKING_DATA_TYPE.TDT_FASTER, fDeltaTime - _fTrackingTime / 3f, _fTrackingTime * 2f / 3f, 3f);
				this.m_NKCWarfareGameUnit.gameObject.transform.localPosition = NKCUtil.Lerp(this.m_BeginPos, this.m_EndPos, progress);
				if (fDeltaTime <= _fTrackingTime / 3f)
				{
					Vector3 localPosition = this.m_NKCWarfareGameUnit.gameObject.transform.localPosition;
					localPosition.z = NKCUtil.Lerp(0f, -125f, progress2);
					this.m_NKCWarfareGameUnit.gameObject.transform.localPosition = localPosition;
				}
				else
				{
					Vector3 localPosition2 = this.m_NKCWarfareGameUnit.gameObject.transform.localPosition;
					localPosition2.z = NKCUtil.Lerp(-125f, 0f, progress3);
					this.m_NKCWarfareGameUnit.gameObject.transform.localPosition = localPosition2;
				}
				yield return null;
				if (!this.m_bPause)
				{
					num = Time.deltaTime;
					if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
					{
						num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
					}
					fDeltaTime += num;
				}
			}
			this.m_NKCWarfareGameUnit.PlayClickAni();
			this.m_NKCWarfareGameUnit.gameObject.transform.localPosition = this.m_EndPos;
			while (fDeltaTime < _fTrackingTime + 0.5f)
			{
				yield return null;
				if (!this.m_bPause)
				{
					num = Time.deltaTime;
					if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
					{
						num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
					}
					fDeltaTime += num;
				}
			}
			this.m_NKCWarfareGameUnit.SetState(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_IDLE);
			if (this.m_OnCompleteMove != null)
			{
				this.m_OnCompleteMove(this.m_NKCWarfareGameUnit);
			}
			this.m_bRunning = false;
			yield break;
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00184ED3 File Offset: 0x001830D3
		public void Stop()
		{
			if (this.m_MoveCoroutine != null)
			{
				base.StopCoroutine(this.m_MoveCoroutine);
			}
			this.m_MoveCoroutine = null;
			this.m_bRunning = false;
			this.m_bPlayMoveEndAni = false;
			this.m_bPause = false;
			this.m_NKCWarfareGameUnit.SetState(NKCWarfareGameUnit.NKC_WARFARE_GAME_UNIT_STATE.NWGUS_IDLE);
		}

		// Token: 0x0400403C RID: 16444
		public NKCWarfareGameUnit m_NKCWarfareGameUnit;

		// Token: 0x0400403D RID: 16445
		private NKCWarfareUnitMover.OnCompleteMove m_OnCompleteMove;

		// Token: 0x0400403E RID: 16446
		private Vector3 m_BeginPos;

		// Token: 0x0400403F RID: 16447
		private Vector3 m_EndPos;

		// Token: 0x04004040 RID: 16448
		private bool m_bRunning;

		// Token: 0x04004041 RID: 16449
		private bool m_bPause;

		// Token: 0x04004042 RID: 16450
		private bool m_bPlayMoveEndAni;

		// Token: 0x04004043 RID: 16451
		private Coroutine m_MoveCoroutine;

		// Token: 0x04004044 RID: 16452
		public const float WAIT_TIME_AFTER_LANDING = 0.5f;

		// Token: 0x020014A2 RID: 5282
		// (Invoke) Token: 0x0600A971 RID: 43377
		public delegate void OnCompleteMove(NKCWarfareGameUnit cNKCWarfareGameUnit);
	}
}

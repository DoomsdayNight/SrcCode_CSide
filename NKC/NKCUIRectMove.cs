using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200076E RID: 1902
	[RequireComponent(typeof(RectTransform))]
	public class NKCUIRectMove : MonoBehaviour
	{
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x0016B2E1 File Offset: 0x001694E1
		private RectTransform RectTransform
		{
			get
			{
				if (this.m_RectTransform == null)
				{
					this.m_RectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_RectTransform;
			}
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0016B303 File Offset: 0x00169503
		public bool IsRunning()
		{
			return this.m_bRunning;
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0016B30B File Offset: 0x0016950B
		private void Awake()
		{
			this.m_RectTransform = base.GetComponent<RectTransform>();
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0016B31C File Offset: 0x0016951C
		public void CopyFromRect(int index)
		{
			NKCUIRectMove.MoveInfo moveInfo = this.m_lstMoveInfo[index];
			NKCUIRectMove.MoveInfo value = new NKCUIRectMove.MoveInfo
			{
				AnchoredPosition = this.RectTransform.anchoredPosition,
				Pivot = this.RectTransform.pivot,
				SizeDelta = this.RectTransform.sizeDelta,
				anchorMax = this.RectTransform.anchorMax,
				anchorMin = this.RectTransform.anchorMin,
				Scale = this.RectTransform.localScale,
				Name = moveInfo.Name,
				TrackTime = moveInfo.TrackTime,
				TrackType = moveInfo.TrackType
			};
			this.m_lstMoveInfo[index] = value;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0016B3D4 File Offset: 0x001695D4
		public void CopyFromRect()
		{
			NKCUIRectMove.MoveInfo item = new NKCUIRectMove.MoveInfo
			{
				AnchoredPosition = this.RectTransform.anchoredPosition,
				Pivot = this.RectTransform.pivot,
				SizeDelta = this.RectTransform.sizeDelta,
				anchorMax = this.RectTransform.anchorMax,
				anchorMin = this.RectTransform.anchorMin,
				Scale = this.RectTransform.localScale,
				Name = "New Moveinfo",
				TrackTime = 0.4f,
				TrackType = TRACKING_DATA_TYPE.TDT_SLOWER
			};
			if (this.m_lstMoveInfo == null)
			{
				this.m_lstMoveInfo = new List<NKCUIRectMove.MoveInfo>();
			}
			this.m_lstMoveInfo.Add(item);
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0016B489 File Offset: 0x00169689
		public void StopTracking()
		{
			if (this.TransitCoroutine != null)
			{
				base.StopCoroutine(this.TransitCoroutine);
			}
			this.TransitCoroutine = null;
			this.m_bRunning = false;
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0016B4B0 File Offset: 0x001696B0
		public NKCUIRectMove.MoveInfo GetMoveInfo(string name)
		{
			return this.m_lstMoveInfo.Find((NKCUIRectMove.MoveInfo x) => x.Name == name);
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0016B4E4 File Offset: 0x001696E4
		public void AddMoveInfo(NKCUIRectMove.MoveInfo info)
		{
			if (this.m_lstMoveInfo.Exists((NKCUIRectMove.MoveInfo x) => x.Name == info.Name))
			{
				Debug.LogError("Moveinfo of same name exists!");
				return;
			}
			this.m_lstMoveInfo.Add(info);
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0016B534 File Offset: 0x00169734
		public void Set(int index)
		{
			if (index >= this.m_lstMoveInfo.Count)
			{
				return;
			}
			NKCUIRectMove.MoveInfo info = this.m_lstMoveInfo[index];
			this.Set(info);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0016B564 File Offset: 0x00169764
		public void Set(string name)
		{
			NKCUIRectMove.MoveInfo info = this.m_lstMoveInfo.Find((NKCUIRectMove.MoveInfo x) => x.Name == name);
			this.Set(info);
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0016B5A0 File Offset: 0x001697A0
		public void Set(string startName, string endName, float fPercent)
		{
			NKCUIRectMove.MoveInfo moveInfo = this.m_lstMoveInfo.Find((NKCUIRectMove.MoveInfo x) => x.Name == startName);
			NKCUIRectMove.MoveInfo moveInfo2 = this.m_lstMoveInfo.Find((NKCUIRectMove.MoveInfo x) => x.Name == endName);
			if (moveInfo != null && moveInfo2 != null)
			{
				this.RectTransform.anchoredPosition = moveInfo.AnchoredPosition + (moveInfo2.AnchoredPosition - moveInfo.AnchoredPosition) * fPercent;
				this.RectTransform.localScale = moveInfo.Scale * (1f - fPercent) + moveInfo2.Scale * fPercent;
			}
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0016B654 File Offset: 0x00169854
		private void Set(NKCUIRectMove.MoveInfo info)
		{
			if (info != null)
			{
				this.StopTracking();
				if (!info.bNoApplyAnchoredPosition)
				{
					Vector2 vector = info.AnchoredPosition;
					if (this.m_comSafeArea != null)
					{
						vector = this.m_comSafeArea.GetSafeAreaPos(vector);
						this.m_comSafeArea.SetInit();
					}
					this.RectTransform.anchoredPosition = vector;
				}
				Vector3 localScale = info.Scale;
				if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseScale)
				{
					localScale = this.m_comSafeArea.GetSafeAreaScale();
					this.m_comSafeArea.SetInit();
				}
				this.RectTransform.localScale = localScale;
				this.RectTransform.anchorMax = info.anchorMax;
				this.RectTransform.anchorMin = info.anchorMin;
				this.RectTransform.pivot = info.Pivot;
				Vector2 sizeDelta = info.SizeDelta;
				if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseRectSide)
				{
					float safeAreaWidth = this.m_comSafeArea.GetSafeAreaWidth(sizeDelta.x);
					sizeDelta.x = safeAreaWidth;
					this.m_comSafeArea.SetInit();
				}
				if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseRectHeight)
				{
					float safeAreaHeight = this.m_comSafeArea.GetSafeAreaHeight(sizeDelta.y);
					sizeDelta.y = safeAreaHeight;
					this.m_comSafeArea.SetInit();
				}
				this.RectTransform.sizeDelta = sizeDelta;
			}
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0016B7BB File Offset: 0x001699BB
		public void Move(string Name, bool bAnimate)
		{
			if (bAnimate)
			{
				this.Transit(Name, null);
				return;
			}
			this.Set(Name);
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0016B7D0 File Offset: 0x001699D0
		public void Transit(string name, NKCUIRectMove.OnTrackingComplete onComplete = null)
		{
			NKCUIRectMove.MoveInfo info = this.m_lstMoveInfo.Find((NKCUIRectMove.MoveInfo x) => x.Name == name);
			this.Transit(info, onComplete);
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0016B80A File Offset: 0x00169A0A
		public void Transit(int index, NKCUIRectMove.OnTrackingComplete onComplete = null)
		{
			if (index >= 0 && index < this.m_lstMoveInfo.Count)
			{
				this.Transit(this.m_lstMoveInfo[index], onComplete);
			}
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0016B834 File Offset: 0x00169A34
		private void Transit(NKCUIRectMove.MoveInfo info, NKCUIRectMove.OnTrackingComplete onComplete)
		{
			if (info == null)
			{
				return;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				this.Set(info);
				return;
			}
			this.StopTracking();
			if (info.TrackTime == 0f)
			{
				this.Set(info);
				if (onComplete != null)
				{
					onComplete();
					return;
				}
			}
			else
			{
				this.TransitCoroutine = base.StartCoroutine(this._Transit(info, onComplete));
			}
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0016B892 File Offset: 0x00169A92
		private IEnumerator _Transit(NKCUIRectMove.MoveInfo info, NKCUIRectMove.OnTrackingComplete onComplete)
		{
			this.m_bRunning = true;
			Vector2 PositionBegin = this.RectTransform.anchoredPosition;
			Vector2 vector = info.AnchoredPosition;
			if (this.m_comSafeArea != null)
			{
				vector = this.m_comSafeArea.GetSafeAreaPos(vector);
				this.m_comSafeArea.SetInit();
			}
			Vector2 PositionEnd = vector;
			Vector3 scaleBegin = this.RectTransform.localScale;
			Vector3 vector2 = info.Scale;
			if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseScale)
			{
				vector2 = this.m_comSafeArea.GetSafeAreaScale();
				this.m_comSafeArea.SetInit();
			}
			Vector3 scaleEnd = vector2;
			Vector2 anchorMaxBegin = this.RectTransform.anchorMax;
			Vector2 anchorMaxEnd = info.anchorMax;
			Vector2 anchorMinBegin = this.RectTransform.anchorMin;
			Vector2 anchorMinEnd = info.anchorMin;
			Vector2 pivotBegin = this.RectTransform.pivot;
			Vector2 pivotEnd = info.Pivot;
			Vector2 sizeDelta = this.RectTransform.sizeDelta;
			if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseRectSide)
			{
				float safeAreaWidth = this.m_comSafeArea.GetSafeAreaWidth(sizeDelta.x);
				sizeDelta.x = safeAreaWidth;
				this.m_comSafeArea.SetInit();
			}
			if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseRectHeight)
			{
				float safeAreaHeight = this.m_comSafeArea.GetSafeAreaHeight(sizeDelta.y);
				sizeDelta.y = safeAreaHeight;
				this.m_comSafeArea.SetInit();
			}
			Vector2 sizeDeltaBegin = sizeDelta;
			sizeDelta = info.SizeDelta;
			if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseRectSide)
			{
				float safeAreaWidth2 = this.m_comSafeArea.GetSafeAreaWidth(sizeDelta.x);
				sizeDelta.x = safeAreaWidth2;
				this.m_comSafeArea.SetInit();
			}
			if (this.m_comSafeArea != null && this.m_comSafeArea.m_bUseRectHeight)
			{
				float safeAreaHeight2 = this.m_comSafeArea.GetSafeAreaHeight(sizeDelta.y);
				sizeDelta.y = safeAreaHeight2;
				this.m_comSafeArea.SetInit();
			}
			Vector2 sizeDeltaEnd = sizeDelta;
			float fDeltaTime = 0f;
			yield return null;
			for (fDeltaTime += Time.deltaTime; fDeltaTime < info.TrackTime; fDeltaTime += Time.deltaTime)
			{
				float progress = NKMTrackingFloat.TrackRatio(info.TrackType, fDeltaTime, info.TrackTime, 3f);
				if (!info.bNoApplyAnchoredPosition)
				{
					this.RectTransform.anchoredPosition = NKCUtil.Lerp(PositionBegin, PositionEnd, progress);
				}
				this.RectTransform.localScale = NKCUtil.Lerp(scaleBegin, scaleEnd, progress);
				this.RectTransform.anchorMax = NKCUtil.Lerp(anchorMaxBegin, anchorMaxEnd, progress);
				this.RectTransform.anchorMin = NKCUtil.Lerp(anchorMinBegin, anchorMinEnd, progress);
				this.RectTransform.pivot = NKCUtil.Lerp(pivotBegin, pivotEnd, progress);
				this.RectTransform.sizeDelta = NKCUtil.Lerp(sizeDeltaBegin, sizeDeltaEnd, progress);
				yield return null;
			}
			if (!info.bNoApplyAnchoredPosition)
			{
				this.RectTransform.anchoredPosition = PositionEnd;
			}
			this.RectTransform.localScale = scaleEnd;
			this.RectTransform.anchorMax = anchorMaxEnd;
			this.RectTransform.anchorMin = anchorMinEnd;
			this.RectTransform.pivot = pivotEnd;
			this.RectTransform.sizeDelta = sizeDeltaEnd;
			if (onComplete != null)
			{
				onComplete();
			}
			this.m_bRunning = false;
			yield break;
		}

		// Token: 0x04003A5C RID: 14940
		public List<NKCUIRectMove.MoveInfo> m_lstMoveInfo;

		// Token: 0x04003A5D RID: 14941
		private RectTransform m_RectTransform;

		// Token: 0x04003A5E RID: 14942
		public NKCUIComSafeArea m_comSafeArea;

		// Token: 0x04003A5F RID: 14943
		private bool m_bRunning;

		// Token: 0x04003A60 RID: 14944
		private Coroutine TransitCoroutine;

		// Token: 0x0200143C RID: 5180
		[Serializable]
		public class MoveInfo
		{
			// Token: 0x04009DC5 RID: 40389
			public string Name;

			// Token: 0x04009DC6 RID: 40390
			public bool bNoApplyAnchoredPosition;

			// Token: 0x04009DC7 RID: 40391
			public Vector2 AnchoredPosition;

			// Token: 0x04009DC8 RID: 40392
			public Vector2 SizeDelta;

			// Token: 0x04009DC9 RID: 40393
			public Vector2 anchorMin;

			// Token: 0x04009DCA RID: 40394
			public Vector2 anchorMax;

			// Token: 0x04009DCB RID: 40395
			public Vector2 Pivot;

			// Token: 0x04009DCC RID: 40396
			public Vector3 Scale;

			// Token: 0x04009DCD RID: 40397
			public float TrackTime;

			// Token: 0x04009DCE RID: 40398
			public TRACKING_DATA_TYPE TrackType;
		}

		// Token: 0x0200143D RID: 5181
		// (Invoke) Token: 0x0600A835 RID: 43061
		public delegate void OnTrackingComplete();
	}
}

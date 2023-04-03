using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200033F RID: 831
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/ScrollRectTweener")]
	public class ScrollRectTweener : MonoBehaviour, IDragHandler, IEventSystemHandler
	{
		// Token: 0x0600137F RID: 4991 RVA: 0x000491B8 File Offset: 0x000473B8
		private void Awake()
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
			this.wasHorizontal = this.scrollRect.horizontal;
			this.wasVertical = this.scrollRect.vertical;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000491E8 File Offset: 0x000473E8
		public void ScrollHorizontal(float normalizedX)
		{
			this.Scroll(new Vector2(normalizedX, this.scrollRect.verticalNormalizedPosition));
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00049201 File Offset: 0x00047401
		public void ScrollHorizontal(float normalizedX, float duration)
		{
			this.Scroll(new Vector2(normalizedX, this.scrollRect.verticalNormalizedPosition), duration);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004921B File Offset: 0x0004741B
		public void ScrollVertical(float normalizedY)
		{
			this.Scroll(new Vector2(this.scrollRect.horizontalNormalizedPosition, normalizedY));
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00049234 File Offset: 0x00047434
		public void ScrollVertical(float normalizedY, float duration)
		{
			this.Scroll(new Vector2(this.scrollRect.horizontalNormalizedPosition, normalizedY), duration);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004924E File Offset: 0x0004744E
		public void Scroll(Vector2 normalizedPos)
		{
			this.Scroll(normalizedPos, this.GetScrollDuration(normalizedPos));
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00049260 File Offset: 0x00047460
		private float GetScrollDuration(Vector2 normalizedPos)
		{
			Vector2 currentPos = this.GetCurrentPos();
			return Vector2.Distance(this.DeNormalize(currentPos), this.DeNormalize(normalizedPos)) / this.moveSpeed;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00049290 File Offset: 0x00047490
		private Vector2 DeNormalize(Vector2 normalizedPos)
		{
			return new Vector2(normalizedPos.x * this.scrollRect.content.rect.width, normalizedPos.y * this.scrollRect.content.rect.height);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000492E0 File Offset: 0x000474E0
		private Vector2 GetCurrentPos()
		{
			return new Vector2(this.scrollRect.horizontalNormalizedPosition, this.scrollRect.verticalNormalizedPosition);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000492FD File Offset: 0x000474FD
		public void Scroll(Vector2 normalizedPos, float duration)
		{
			this.startPos = this.GetCurrentPos();
			this.targetPos = normalizedPos;
			if (this.disableDragWhileTweening)
			{
				this.LockScrollability();
			}
			base.StopAllCoroutines();
			base.StartCoroutine(this.DoMove(duration));
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00049334 File Offset: 0x00047534
		private IEnumerator DoMove(float duration)
		{
			if (duration < 0.05f)
			{
				yield break;
			}
			Vector2 posOffset = this.targetPos - this.startPos;
			float currentTime = 0f;
			while (currentTime < duration)
			{
				currentTime += Time.deltaTime;
				this.scrollRect.normalizedPosition = this.EaseVector(currentTime, this.startPos, posOffset, duration);
				yield return null;
			}
			this.scrollRect.normalizedPosition = this.targetPos;
			if (this.disableDragWhileTweening)
			{
				this.RestoreScrollability();
			}
			yield break;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004934C File Offset: 0x0004754C
		public Vector2 EaseVector(float currentTime, Vector2 startValue, Vector2 changeInValue, float duration)
		{
			return new Vector2(changeInValue.x * Mathf.Sin(currentTime / duration * 1.5707964f) + startValue.x, changeInValue.y * Mathf.Sin(currentTime / duration * 1.5707964f) + startValue.y);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00049398 File Offset: 0x00047598
		public void OnDrag(PointerEventData eventData)
		{
			if (!this.disableDragWhileTweening)
			{
				this.StopScroll();
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000493A8 File Offset: 0x000475A8
		private void StopScroll()
		{
			base.StopAllCoroutines();
			if (this.disableDragWhileTweening)
			{
				this.RestoreScrollability();
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000493BE File Offset: 0x000475BE
		private void LockScrollability()
		{
			this.scrollRect.horizontal = false;
			this.scrollRect.vertical = false;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000493D8 File Offset: 0x000475D8
		private void RestoreScrollability()
		{
			this.scrollRect.horizontal = this.wasHorizontal;
			this.scrollRect.vertical = this.wasVertical;
		}

		// Token: 0x04000D7C RID: 3452
		private ScrollRect scrollRect;

		// Token: 0x04000D7D RID: 3453
		private Vector2 startPos;

		// Token: 0x04000D7E RID: 3454
		private Vector2 targetPos;

		// Token: 0x04000D7F RID: 3455
		private bool wasHorizontal;

		// Token: 0x04000D80 RID: 3456
		private bool wasVertical;

		// Token: 0x04000D81 RID: 3457
		public float moveSpeed = 5000f;

		// Token: 0x04000D82 RID: 3458
		public bool disableDragWhileTweening;
	}
}

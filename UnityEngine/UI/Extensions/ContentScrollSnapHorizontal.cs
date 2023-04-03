using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002F3 RID: 755
	[ExecuteInEditMode]
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/ContentSnapScrollHorizontal")]
	public class ContentScrollSnapHorizontal : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0003A4BE File Offset: 0x000386BE
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x0003A4C6 File Offset: 0x000386C6
		public ContentScrollSnapHorizontal.StartMovementEvent MovementStarted
		{
			get
			{
				return this.m_StartMovementEvent;
			}
			set
			{
				this.m_StartMovementEvent = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0003A4CF File Offset: 0x000386CF
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x0003A4D7 File Offset: 0x000386D7
		public ContentScrollSnapHorizontal.CurrentItemChangeEvent CurrentItemChanged
		{
			get
			{
				return this.m_CurrentItemChangeEvent;
			}
			set
			{
				this.m_CurrentItemChangeEvent = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0003A4E0 File Offset: 0x000386E0
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x0003A4E8 File Offset: 0x000386E8
		public ContentScrollSnapHorizontal.FoundItemToSnapToEvent ItemFoundToSnap
		{
			get
			{
				return this.m_FoundItemToSnapToEvent;
			}
			set
			{
				this.m_FoundItemToSnapToEvent = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0003A4F1 File Offset: 0x000386F1
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x0003A4F9 File Offset: 0x000386F9
		public ContentScrollSnapHorizontal.SnappedToItemEvent ItemSnappedTo
		{
			get
			{
				return this.m_SnappedToItemEvent;
			}
			set
			{
				this.m_SnappedToItemEvent = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0003A502 File Offset: 0x00038702
		private bool ContentIsHorizonalLayoutGroup
		{
			get
			{
				return this.contentTransform.GetComponent<HorizontalLayoutGroup>() != null;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0003A515 File Offset: 0x00038715
		public bool Moving
		{
			get
			{
				return this.Sliding || this.Lerping;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0003A527 File Offset: 0x00038727
		public bool Sliding
		{
			get
			{
				return this.mSliding;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0003A52F File Offset: 0x0003872F
		public bool Lerping
		{
			get
			{
				return this.mLerping;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0003A537 File Offset: 0x00038737
		public int ClosestItemIndex
		{
			get
			{
				return this.contentPositions.IndexOf(this.FindClosestFrom(this.contentTransform.localPosition));
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0003A555 File Offset: 0x00038755
		public int LerpTargetIndex
		{
			get
			{
				return this.contentPositions.IndexOf(this.lerpTarget);
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0003A568 File Offset: 0x00038768
		private void Awake()
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
			this.scrollRectTransform = (RectTransform)this.scrollRect.transform;
			this.contentTransform = this.scrollRect.content;
			if (this.nextButton)
			{
				this.nextButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.NextItem();
				});
			}
			if (this.prevButton)
			{
				this.prevButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.PreviousItem();
				});
			}
			if (this.IsScrollRectAvailable)
			{
				this.SetupDrivenTransforms();
				this.SetupSnapScroll();
				this.scrollRect.horizontalNormalizedPosition = 0f;
				this._closestItem = 0;
				this.GoTo(this.startInfo);
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0003A63C File Offset: 0x0003883C
		public void SetNewItems(ref List<Transform> newItems)
		{
			if (this.scrollRect && this.contentTransform)
			{
				for (int i = this.scrollRect.content.childCount - 1; i >= 0; i--)
				{
					Transform child = this.contentTransform.GetChild(i);
					child.SetParent(null);
					Object.DestroyImmediate(child.gameObject);
				}
				foreach (Transform transform in newItems)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject.IsPrefab())
					{
						gameObject = Object.Instantiate<GameObject>(transform.gameObject, this.contentTransform);
					}
					else
					{
						gameObject.transform.SetParent(this.contentTransform);
					}
				}
				this.SetupDrivenTransforms();
				this.SetupSnapScroll();
				this.scrollRect.horizontalNormalizedPosition = 0f;
				this._closestItem = 0;
				this.GoTo(this.startInfo);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0003A744 File Offset: 0x00038944
		private bool IsScrollRectAvailable
		{
			get
			{
				return this.scrollRect && this.contentTransform && this.contentTransform.childCount > 0;
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003A771 File Offset: 0x00038971
		private void OnDisable()
		{
			this.tracker.Clear();
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003A780 File Offset: 0x00038980
		private void SetupDrivenTransforms()
		{
			this.tracker = default(DrivenRectTransformTracker);
			this.tracker.Clear();
			foreach (object obj in this.contentTransform)
			{
				RectTransform rectTransform = (RectTransform)obj;
				this.tracker.Add(this, rectTransform, DrivenTransformProperties.Anchors);
				rectTransform.anchorMax = new Vector2(0f, 1f);
				rectTransform.anchorMin = new Vector2(0f, 1f);
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0003A828 File Offset: 0x00038A28
		private void SetupSnapScroll()
		{
			if (this.ContentIsHorizonalLayoutGroup)
			{
				this.SetupWithHorizontalLayoutGroup();
				return;
			}
			this.SetupWithCalculatedSpacing();
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0003A840 File Offset: 0x00038A40
		private void SetupWithHorizontalLayoutGroup()
		{
			HorizontalLayoutGroup component = this.contentTransform.GetComponent<HorizontalLayoutGroup>();
			float num = 0f;
			int num2 = 0;
			for (int i = 0; i < this.contentTransform.childCount; i++)
			{
				if (!this.ignoreInactiveItems || this.contentTransform.GetChild(i).gameObject.activeInHierarchy)
				{
					num += ((RectTransform)this.contentTransform.GetChild(i)).sizeDelta.x;
					num2++;
				}
			}
			float num3 = (float)(num2 - 1) * component.spacing;
			float num4 = num + num3 + (float)component.padding.left + (float)component.padding.right;
			this.contentTransform.sizeDelta = new Vector2(num4, this.contentTransform.sizeDelta.y);
			float x = Mathf.Min(((RectTransform)this.contentTransform.GetChild(0)).sizeDelta.x, ((RectTransform)this.contentTransform.GetChild(this.contentTransform.childCount - 1)).sizeDelta.x);
			this.scrollRectTransform.sizeDelta = new Vector2(x, this.scrollRectTransform.sizeDelta.y);
			this.contentPositions = new List<Vector3>();
			float x2 = this.scrollRectTransform.sizeDelta.x;
			this.totalScrollableWidth = num4 - x2;
			float num5 = (float)component.padding.left;
			int num6 = 0;
			for (int j = 0; j < this.contentTransform.childCount; j++)
			{
				if (!this.ignoreInactiveItems || this.contentTransform.GetChild(j).gameObject.activeInHierarchy)
				{
					float x3 = ((RectTransform)this.contentTransform.GetChild(j)).sizeDelta.x;
					float num7 = num5 + component.spacing * (float)num6 + (x3 - x2) / 2f;
					this.scrollRect.horizontalNormalizedPosition = num7 / this.totalScrollableWidth;
					this.contentPositions.Add(this.contentTransform.localPosition);
					num5 += x3;
					num6++;
				}
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0003AA64 File Offset: 0x00038C64
		private void SetupWithCalculatedSpacing()
		{
			List<RectTransform> list = new List<RectTransform>();
			for (int i = 0; i < this.contentTransform.childCount; i++)
			{
				if (!this.ignoreInactiveItems || this.contentTransform.GetChild(i).gameObject.activeInHierarchy)
				{
					RectTransform rectTransform = (RectTransform)this.contentTransform.GetChild(i);
					int index = list.Count;
					for (int j = 0; j < list.Count; j++)
					{
						if (this.DstFromTopLeftOfTransformToTopLeftOfParent(rectTransform).x < this.DstFromTopLeftOfTransformToTopLeftOfParent(list[j]).x)
						{
							index = j;
							break;
						}
					}
					list.Insert(index, rectTransform);
				}
			}
			RectTransform rectTransform2 = list[list.Count - 1];
			float num = this.DstFromTopLeftOfTransformToTopLeftOfParent(rectTransform2).x + rectTransform2.sizeDelta.x;
			this.contentTransform.sizeDelta = new Vector2(num, this.contentTransform.sizeDelta.y);
			float x = Mathf.Min(list[0].sizeDelta.x, list[list.Count - 1].sizeDelta.x);
			this.scrollRectTransform.sizeDelta = new Vector2(x, this.scrollRectTransform.sizeDelta.y);
			this.contentPositions = new List<Vector3>();
			float x2 = this.scrollRectTransform.sizeDelta.x;
			this.totalScrollableWidth = num - x2;
			for (int k = 0; k < list.Count; k++)
			{
				float num2 = this.DstFromTopLeftOfTransformToTopLeftOfParent(list[k]).x + (list[k].sizeDelta.x - x2) / 2f;
				this.scrollRect.horizontalNormalizedPosition = num2 / this.totalScrollableWidth;
				this.contentPositions.Add(this.contentTransform.localPosition);
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0003AC4C File Offset: 0x00038E4C
		public void GoTo(ContentScrollSnapHorizontal.MoveInfo info)
		{
			if (!this.Moving && info.index != this.ClosestItemIndex)
			{
				this.MovementStarted.Invoke();
			}
			if (info.indexType == ContentScrollSnapHorizontal.MoveInfo.IndexType.childIndex)
			{
				this.mLerpTime = info.duration;
				this.GoToChild(info.index, info.jump);
				return;
			}
			if (info.indexType == ContentScrollSnapHorizontal.MoveInfo.IndexType.positionIndex)
			{
				this.mLerpTime = info.duration;
				this.GoToContentPos(info.index, info.jump);
			}
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0003ACC8 File Offset: 0x00038EC8
		private void GoToChild(int index, bool jump)
		{
			int num = Mathf.Clamp(index, 0, this.contentPositions.Count - 1);
			if (!this.ContentIsHorizonalLayoutGroup)
			{
				int num2 = 0;
				Vector3 localPosition = this.contentTransform.localPosition;
				for (int i = 0; i < this.contentTransform.childCount; i++)
				{
					if (!this.ignoreInactiveItems || this.contentTransform.GetChild(i).gameObject.activeInHierarchy)
					{
						if (num2 == num)
						{
							RectTransform rectTransform = (RectTransform)this.contentTransform.GetChild(i);
							float num3 = this.DstFromTopLeftOfTransformToTopLeftOfParent(rectTransform).x + (rectTransform.sizeDelta.x - this.scrollRectTransform.sizeDelta.x) / 2f;
							this.scrollRect.horizontalNormalizedPosition = num3 / this.totalScrollableWidth;
							this.lerpTarget = this.contentTransform.localPosition;
							if (!jump)
							{
								this.contentTransform.localPosition = localPosition;
								this.StopMovement();
								base.StartCoroutine("LerpToContent");
							}
							return;
						}
						num2++;
					}
				}
				return;
			}
			this.lerpTarget = this.contentPositions[num];
			if (jump)
			{
				this.contentTransform.localPosition = this.lerpTarget;
				return;
			}
			this.StopMovement();
			base.StartCoroutine("LerpToContent");
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0003AE14 File Offset: 0x00039014
		private void GoToContentPos(int index, bool jump)
		{
			int index2 = Mathf.Clamp(index, 0, this.contentPositions.Count - 1);
			this.lerpTarget = this.contentPositions[index2];
			if (jump)
			{
				this.contentTransform.localPosition = this.lerpTarget;
				return;
			}
			this.StopMovement();
			base.StartCoroutine("LerpToContent");
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0003AE70 File Offset: 0x00039070
		public void NextItem()
		{
			int index;
			if (this.Sliding)
			{
				index = this.ClosestItemIndex + 1;
			}
			else
			{
				index = this.LerpTargetIndex + 1;
			}
			ContentScrollSnapHorizontal.MoveInfo info = new ContentScrollSnapHorizontal.MoveInfo(ContentScrollSnapHorizontal.MoveInfo.IndexType.positionIndex, index, this.jumpToItem, this.lerpTime);
			this.GoTo(info);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0003AEB8 File Offset: 0x000390B8
		public void PreviousItem()
		{
			int index;
			if (this.Sliding)
			{
				index = this.ClosestItemIndex - 1;
			}
			else
			{
				index = this.LerpTargetIndex - 1;
			}
			ContentScrollSnapHorizontal.MoveInfo info = new ContentScrollSnapHorizontal.MoveInfo(ContentScrollSnapHorizontal.MoveInfo.IndexType.positionIndex, index, this.jumpToItem, this.lerpTime);
			this.GoTo(info);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0003AEFD File Offset: 0x000390FD
		public void UpdateLayout()
		{
			this.SetupDrivenTransforms();
			this.SetupSnapScroll();
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003AF0B File Offset: 0x0003910B
		public void UpdateLayoutAndMoveTo(ContentScrollSnapHorizontal.MoveInfo info)
		{
			this.SetupDrivenTransforms();
			this.SetupSnapScroll();
			this.GoTo(info);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003AF20 File Offset: 0x00039120
		public void OnBeginDrag(PointerEventData ped)
		{
			if (this.contentPositions.Count < 2)
			{
				return;
			}
			this.StopMovement();
			if (!this.Moving)
			{
				this.MovementStarted.Invoke();
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003AF4A File Offset: 0x0003914A
		public void OnEndDrag(PointerEventData ped)
		{
			if (this.contentPositions.Count <= 1)
			{
				return;
			}
			if (this.IsScrollRectAvailable)
			{
				base.StartCoroutine("SlideAndLerp");
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003AF70 File Offset: 0x00039170
		private void Update()
		{
			if (this.IsScrollRectAvailable && this._closestItem != this.ClosestItemIndex)
			{
				this.CurrentItemChanged.Invoke(this.ClosestItemIndex);
				this.ChangePaginationInfo(this.ClosestItemIndex);
				this._closestItem = this.ClosestItemIndex;
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0003AFBC File Offset: 0x000391BC
		private IEnumerator SlideAndLerp()
		{
			this.mSliding = true;
			while (Mathf.Abs(this.scrollRect.velocity.x) > (float)this.snappingVelocityThreshold)
			{
				yield return null;
			}
			this.lerpTarget = this.FindClosestFrom(this.contentTransform.localPosition);
			this.ItemFoundToSnap.Invoke(this.LerpTargetIndex);
			while (Vector3.Distance(this.contentTransform.localPosition, this.lerpTarget) > 1f)
			{
				this.contentTransform.localPosition = Vector3.Lerp(this.scrollRect.content.localPosition, this.lerpTarget, 7.5f * Time.deltaTime);
				yield return null;
			}
			this.mSliding = false;
			this.scrollRect.velocity = Vector2.zero;
			this.contentTransform.localPosition = this.lerpTarget;
			this.ItemSnappedTo.Invoke(this.LerpTargetIndex);
			yield break;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003AFCB File Offset: 0x000391CB
		private IEnumerator LerpToContent()
		{
			this.ItemFoundToSnap.Invoke(this.LerpTargetIndex);
			this.mLerping = true;
			Vector3 originalContentPos = this.contentTransform.localPosition;
			float elapsedTime = 0f;
			while (elapsedTime < this.mLerpTime)
			{
				elapsedTime += Time.deltaTime;
				this.contentTransform.localPosition = Vector3.Lerp(originalContentPos, this.lerpTarget, elapsedTime / this.mLerpTime);
				yield return null;
			}
			this.ItemSnappedTo.Invoke(this.LerpTargetIndex);
			this.mLerping = false;
			yield break;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003AFDA File Offset: 0x000391DA
		private void StopMovement()
		{
			this.scrollRect.velocity = Vector2.zero;
			base.StopCoroutine("SlideAndLerp");
			base.StopCoroutine("LerpToContent");
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0003B004 File Offset: 0x00039204
		private void ChangePaginationInfo(int targetScreen)
		{
			if (this.pagination)
			{
				for (int i = 0; i < this.pagination.transform.childCount; i++)
				{
					this.pagination.transform.GetChild(i).GetComponent<Toggle>().isOn = (targetScreen == i);
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003B058 File Offset: 0x00039258
		private Vector2 DstFromTopLeftOfTransformToTopLeftOfParent(RectTransform rt)
		{
			return new Vector2(rt.anchoredPosition.x - rt.sizeDelta.x * rt.pivot.x, rt.anchoredPosition.y + rt.sizeDelta.y * (1f - rt.pivot.y));
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0003B0B8 File Offset: 0x000392B8
		private Vector3 FindClosestFrom(Vector3 start)
		{
			Vector3 result = Vector3.zero;
			float num = float.PositiveInfinity;
			foreach (Vector3 vector in this.contentPositions)
			{
				if (Vector3.Distance(start, vector) < num)
				{
					num = Vector3.Distance(start, vector);
					result = vector;
				}
			}
			return result;
		}

		// Token: 0x04000BAD RID: 2989
		public bool ignoreInactiveItems = true;

		// Token: 0x04000BAE RID: 2990
		public ContentScrollSnapHorizontal.MoveInfo startInfo = new ContentScrollSnapHorizontal.MoveInfo(ContentScrollSnapHorizontal.MoveInfo.IndexType.positionIndex, 0);

		// Token: 0x04000BAF RID: 2991
		public GameObject prevButton;

		// Token: 0x04000BB0 RID: 2992
		public GameObject nextButton;

		// Token: 0x04000BB1 RID: 2993
		public GameObject pagination;

		// Token: 0x04000BB2 RID: 2994
		[Tooltip("The velocity below which the scroll rect will start to snap")]
		public int snappingVelocityThreshold = 50;

		// Token: 0x04000BB3 RID: 2995
		[Header("Paging Info")]
		[Tooltip("Should the pagination & buttons jump or lerp to the items")]
		public bool jumpToItem;

		// Token: 0x04000BB4 RID: 2996
		[Tooltip("The time it will take for the pagination or buttons to move between items")]
		public float lerpTime = 0.3f;

		// Token: 0x04000BB5 RID: 2997
		[Header("Events")]
		[SerializeField]
		[Tooltip("Event is triggered whenever the scroll rect starts to move, even when triggered programmatically")]
		private ContentScrollSnapHorizontal.StartMovementEvent m_StartMovementEvent = new ContentScrollSnapHorizontal.StartMovementEvent();

		// Token: 0x04000BB6 RID: 2998
		[SerializeField]
		[Tooltip("Event is triggered whenever the closest item to the center of the scrollrect changes")]
		private ContentScrollSnapHorizontal.CurrentItemChangeEvent m_CurrentItemChangeEvent = new ContentScrollSnapHorizontal.CurrentItemChangeEvent();

		// Token: 0x04000BB7 RID: 2999
		[SerializeField]
		[Tooltip("Event is triggered when the ContentSnapScroll decides which item it is going to snap to. Returns the index of the closest position.")]
		private ContentScrollSnapHorizontal.FoundItemToSnapToEvent m_FoundItemToSnapToEvent = new ContentScrollSnapHorizontal.FoundItemToSnapToEvent();

		// Token: 0x04000BB8 RID: 3000
		[SerializeField]
		[Tooltip("Event is triggered when we finally settle on an element. Returns the index of the item's position.")]
		private ContentScrollSnapHorizontal.SnappedToItemEvent m_SnappedToItemEvent = new ContentScrollSnapHorizontal.SnappedToItemEvent();

		// Token: 0x04000BB9 RID: 3001
		private ScrollRect scrollRect;

		// Token: 0x04000BBA RID: 3002
		private RectTransform scrollRectTransform;

		// Token: 0x04000BBB RID: 3003
		private RectTransform contentTransform;

		// Token: 0x04000BBC RID: 3004
		private List<Vector3> contentPositions = new List<Vector3>();

		// Token: 0x04000BBD RID: 3005
		private Vector3 lerpTarget = Vector3.zero;

		// Token: 0x04000BBE RID: 3006
		private float totalScrollableWidth;

		// Token: 0x04000BBF RID: 3007
		private DrivenRectTransformTracker tracker;

		// Token: 0x04000BC0 RID: 3008
		private float mLerpTime;

		// Token: 0x04000BC1 RID: 3009
		private int _closestItem;

		// Token: 0x04000BC2 RID: 3010
		private bool mSliding;

		// Token: 0x04000BC3 RID: 3011
		private bool mLerping;

		// Token: 0x02001145 RID: 4421
		[Serializable]
		public class StartMovementEvent : UnityEvent
		{
		}

		// Token: 0x02001146 RID: 4422
		[Serializable]
		public class CurrentItemChangeEvent : UnityEvent<int>
		{
		}

		// Token: 0x02001147 RID: 4423
		[Serializable]
		public class FoundItemToSnapToEvent : UnityEvent<int>
		{
		}

		// Token: 0x02001148 RID: 4424
		[Serializable]
		public class SnappedToItemEvent : UnityEvent<int>
		{
		}

		// Token: 0x02001149 RID: 4425
		[Serializable]
		public struct MoveInfo
		{
			// Token: 0x06009F70 RID: 40816 RVA: 0x0033C784 File Offset: 0x0033A984
			public MoveInfo(ContentScrollSnapHorizontal.MoveInfo.IndexType _indexType, int _index)
			{
				this.indexType = _indexType;
				this.index = _index;
				this.jump = true;
				this.duration = 0f;
			}

			// Token: 0x06009F71 RID: 40817 RVA: 0x0033C7A6 File Offset: 0x0033A9A6
			public MoveInfo(ContentScrollSnapHorizontal.MoveInfo.IndexType _indexType, int _index, bool _jump, float _duration)
			{
				this.indexType = _indexType;
				this.index = _index;
				this.jump = _jump;
				this.duration = _duration;
			}

			// Token: 0x040091E7 RID: 37351
			[Tooltip("Child Index means the Index corresponds to the content item at that index in the hierarchy.\nPosition Index means the Index corresponds to the content item in that snap position.\nA higher Position Index in a Horizontal Scroll Snap means it would be further to the right.")]
			public ContentScrollSnapHorizontal.MoveInfo.IndexType indexType;

			// Token: 0x040091E8 RID: 37352
			[Tooltip("Zero based")]
			public int index;

			// Token: 0x040091E9 RID: 37353
			[Tooltip("If this is true the snap scroll will jump to the index, otherwise it will lerp there.")]
			public bool jump;

			// Token: 0x040091EA RID: 37354
			[Tooltip("If jump is false this is the time it will take to lerp to the index")]
			public float duration;

			// Token: 0x02001A4B RID: 6731
			public enum IndexType
			{
				// Token: 0x0400AE22 RID: 44578
				childIndex,
				// Token: 0x0400AE23 RID: 44579
				positionIndex
			}
		}
	}
}

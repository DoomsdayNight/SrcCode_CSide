using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000347 RID: 839
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/UIScrollToSelection")]
	public class UIScrollToSelection : MonoBehaviour
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00049ABD File Offset: 0x00047CBD
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x00049AC5 File Offset: 0x00047CC5
		public RectTransform ViewRectTransform
		{
			get
			{
				return this.viewportRectTransform;
			}
			set
			{
				this.viewportRectTransform = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00049ACE File Offset: 0x00047CCE
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x00049AD6 File Offset: 0x00047CD6
		public ScrollRect TargetScrollRect
		{
			get
			{
				return this.targetScrollRect;
			}
			set
			{
				this.targetScrollRect = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00049ADF File Offset: 0x00047CDF
		public UIScrollToSelection.Axis ScrollAxes
		{
			get
			{
				return this.scrollAxes;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00049AE7 File Offset: 0x00047CE7
		public UIScrollToSelection.ScrollMethod UsedScrollMethod
		{
			get
			{
				return this.usedScrollMethod;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00049AEF File Offset: 0x00047CEF
		public float ScrollSpeed
		{
			get
			{
				return this.scrollSpeed;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00049AF7 File Offset: 0x00047CF7
		public float EndOfListJumpScrollSpeed
		{
			get
			{
				return this.endOfListJumpScrollSpeed;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00049AFF File Offset: 0x00047CFF
		public float JumpOffsetThreshold
		{
			get
			{
				return this.jumpOffsetThreshold;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00049B07 File Offset: 0x00047D07
		public UIScrollToSelection.MouseButton CancelScrollMouseButtons
		{
			get
			{
				return this.cancelScrollMouseButtons;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00049B0F File Offset: 0x00047D0F
		public KeyCode[] CancelScrollKeys
		{
			get
			{
				return this.cancelScrollKeys;
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00049B17 File Offset: 0x00047D17
		protected void Awake()
		{
			this.ValidateReferences();
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00049B1F File Offset: 0x00047D1F
		protected void LateUpdate()
		{
			this.TryToScrollToSelection();
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00049B27 File Offset: 0x00047D27
		protected void Reset()
		{
			this.TargetScrollRect = (base.gameObject.GetComponentInParent<ScrollRect>() ?? base.gameObject.GetComponentInChildren<ScrollRect>());
			this.ViewRectTransform = base.gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00049B5C File Offset: 0x00047D5C
		private void ValidateReferences()
		{
			if (!this.targetScrollRect)
			{
				this.targetScrollRect = base.GetComponent<ScrollRect>();
			}
			if (!this.targetScrollRect)
			{
				Debug.LogError("[UIScrollToSelection] No ScrollRect found. Either attach this script to a ScrollRect or assign on in the 'Target Scroll Rect' property");
				base.gameObject.SetActive(false);
				return;
			}
			if (this.ViewRectTransform == null)
			{
				this.ViewRectTransform = this.TargetScrollRect.GetComponent<RectTransform>();
			}
			if (this.TargetScrollRect != null)
			{
				this.scrollRectContentTransform = this.TargetScrollRect.content;
			}
			if (EventSystem.current == null)
			{
				Debug.LogError("[UIScrollToSelection] Unity UI EventSystem not found. It is required to check current selected object.");
				base.gameObject.SetActive(false);
				return;
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00049C08 File Offset: 0x00047E08
		private void TryToScrollToSelection()
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject == null || !currentSelectedGameObject.activeInHierarchy || currentSelectedGameObject == this.lastCheckedSelection || !currentSelectedGameObject.transform.IsChildOf(base.transform))
			{
				return;
			}
			RectTransform component = currentSelectedGameObject.GetComponent<RectTransform>();
			this.ViewRectTransform.GetWorldCorners(this.viewRectCorners);
			component.GetWorldCorners(this.selectedElementCorners);
			this.ScrollToSelection(currentSelectedGameObject);
			this.lastCheckedSelection = currentSelectedGameObject;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00049C84 File Offset: 0x00047E84
		private void ScrollToSelection(GameObject selection)
		{
			if (selection == null)
			{
				return;
			}
			Vector3[] array = this.viewRectCorners;
			Vector3[] array2 = this.selectedElementCorners;
			Vector2 zero = Vector2.zero;
			zero.x = ((array2[0].x < array[0].x) ? (array2[0].x - array[0].x) : 0f) + ((array2[2].x > array[2].x) ? (array2[2].x - array[2].x) : 0f);
			zero.y = ((array2[0].y < array[0].y) ? (array2[0].y - array[0].y) : 0f) + ((array2[1].y > array[1].y) ? (array2[1].y - array[1].y) : 0f);
			float speed = this.ScrollSpeed;
			if (Math.Abs(zero.x) / (float)Screen.width >= this.JumpOffsetThreshold || Math.Abs(zero.y) / (float)Screen.height >= this.JumpOffsetThreshold)
			{
				speed = this.EndOfListJumpScrollSpeed;
			}
			Vector2 targetPosition = this.scrollRectContentTransform.localPosition - zero;
			if (this.animationCoroutine != null)
			{
				base.StopCoroutine(this.animationCoroutine);
			}
			this.animationCoroutine = base.StartCoroutine(this.ScrollToPosition(targetPosition, speed));
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00049E2A File Offset: 0x0004802A
		private IEnumerator ScrollToPosition(Vector2 targetPosition, float speed)
		{
			Vector3 localPosition = this.scrollRectContentTransform.localPosition;
			targetPosition.x = (((this.ScrollAxes | UIScrollToSelection.Axis.HORIZONTAL) == this.ScrollAxes) ? targetPosition.x : localPosition.x);
			targetPosition.y = (((this.ScrollAxes | UIScrollToSelection.Axis.VERTICAL) == this.ScrollAxes) ? targetPosition.y : localPosition.y);
			Vector2 currentPosition2D = localPosition;
			float horizontalSpeed = (float)Screen.width / Screen.dpi * speed;
			float verticalSpeed = (float)Screen.height / Screen.dpi * speed;
			while (currentPosition2D != targetPosition && !this.CheckIfScrollInterrupted())
			{
				currentPosition2D.x = this.MoveTowardsValue(currentPosition2D.x, targetPosition.x, horizontalSpeed, this.UsedScrollMethod);
				currentPosition2D.y = this.MoveTowardsValue(currentPosition2D.y, targetPosition.y, verticalSpeed, this.UsedScrollMethod);
				this.scrollRectContentTransform.localPosition = currentPosition2D;
				yield return null;
			}
			this.scrollRectContentTransform.localPosition = currentPosition2D;
			yield break;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00049E48 File Offset: 0x00048048
		private bool CheckIfScrollInterrupted()
		{
			bool flag = false;
			UIScrollToSelection.MouseButton mouseButton = this.CancelScrollMouseButtons;
			if (mouseButton != UIScrollToSelection.MouseButton.LEFT)
			{
				if (mouseButton != UIScrollToSelection.MouseButton.RIGHT)
				{
					if (mouseButton == UIScrollToSelection.MouseButton.MIDDLE)
					{
						flag |= Input.GetMouseButtonDown(2);
					}
				}
				else
				{
					flag |= Input.GetMouseButtonDown(1);
				}
			}
			else
			{
				flag |= Input.GetMouseButtonDown(0);
			}
			if (flag)
			{
				return true;
			}
			for (int i = 0; i < this.CancelScrollKeys.Length; i++)
			{
				if (Input.GetKeyDown(this.CancelScrollKeys[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00049EBA File Offset: 0x000480BA
		private float MoveTowardsValue(float from, float to, float delta, UIScrollToSelection.ScrollMethod method)
		{
			if (method == UIScrollToSelection.ScrollMethod.MOVE_TOWARDS)
			{
				return Mathf.MoveTowards(from, to, delta * Time.unscaledDeltaTime);
			}
			if (method != UIScrollToSelection.ScrollMethod.LERP)
			{
				return from;
			}
			return Mathf.Lerp(from, to, delta * Time.unscaledDeltaTime);
		}

		// Token: 0x04000D99 RID: 3481
		[Header("[ References ]")]
		[SerializeField]
		[Tooltip("View (boundaries/mask) rect transform. Used to check if automatic scroll to selection is required.")]
		private RectTransform viewportRectTransform;

		// Token: 0x04000D9A RID: 3482
		[SerializeField]
		[Tooltip("Scroll rect used to reach selected element.")]
		private ScrollRect targetScrollRect;

		// Token: 0x04000D9B RID: 3483
		[Header("[ Scrolling ]")]
		[SerializeField]
		[Tooltip("Allow automatic scrolling only on these axes.")]
		private UIScrollToSelection.Axis scrollAxes = UIScrollToSelection.Axis.ANY;

		// Token: 0x04000D9C RID: 3484
		[SerializeField]
		[Tooltip("MOVE_TOWARDS: stiff movement, LERP: smoothed out movement")]
		private UIScrollToSelection.ScrollMethod usedScrollMethod;

		// Token: 0x04000D9D RID: 3485
		[SerializeField]
		private float scrollSpeed = 50f;

		// Token: 0x04000D9E RID: 3486
		[Space(5f)]
		[SerializeField]
		[Tooltip("Scroll speed used when element to select is out of \"JumpOffsetThreshold\" range")]
		private float endOfListJumpScrollSpeed = 150f;

		// Token: 0x04000D9F RID: 3487
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("If next element to scroll to is located over this screen percentage, use \"EndOfListJumpScrollSpeed\" to reach this element faster.")]
		private float jumpOffsetThreshold = 1f;

		// Token: 0x04000DA0 RID: 3488
		[Header("[ Input ]")]
		[SerializeField]
		private UIScrollToSelection.MouseButton cancelScrollMouseButtons = UIScrollToSelection.MouseButton.ANY;

		// Token: 0x04000DA1 RID: 3489
		[SerializeField]
		private KeyCode[] cancelScrollKeys = new KeyCode[0];

		// Token: 0x04000DA2 RID: 3490
		private Vector3[] viewRectCorners = new Vector3[4];

		// Token: 0x04000DA3 RID: 3491
		private Vector3[] selectedElementCorners = new Vector3[4];

		// Token: 0x04000DA4 RID: 3492
		private RectTransform scrollRectContentTransform;

		// Token: 0x04000DA5 RID: 3493
		private GameObject lastCheckedSelection;

		// Token: 0x04000DA6 RID: 3494
		private Coroutine animationCoroutine;

		// Token: 0x02001172 RID: 4466
		[Flags]
		public enum Axis
		{
			// Token: 0x0400925F RID: 37471
			NONE = 0,
			// Token: 0x04009260 RID: 37472
			HORIZONTAL = 1,
			// Token: 0x04009261 RID: 37473
			VERTICAL = 16,
			// Token: 0x04009262 RID: 37474
			ANY = 17
		}

		// Token: 0x02001173 RID: 4467
		[Flags]
		public enum MouseButton
		{
			// Token: 0x04009264 RID: 37476
			NONE = 0,
			// Token: 0x04009265 RID: 37477
			LEFT = 1,
			// Token: 0x04009266 RID: 37478
			RIGHT = 16,
			// Token: 0x04009267 RID: 37479
			MIDDLE = 256,
			// Token: 0x04009268 RID: 37480
			ANY = 273
		}

		// Token: 0x02001174 RID: 4468
		public enum ScrollMethod
		{
			// Token: 0x0400926A RID: 37482
			MOVE_TOWARDS,
			// Token: 0x0400926B RID: 37483
			LERP
		}
	}
}

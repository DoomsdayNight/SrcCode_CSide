using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000316 RID: 790
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("Layout/Extensions/Vertical Scroller")]
	public class UIVerticalScroller : MonoBehaviour
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x000409F5 File Offset: 0x0003EBF5
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x000409FD File Offset: 0x0003EBFD
		public int focusedElementIndex { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00040A06 File Offset: 0x0003EC06
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00040A0E File Offset: 0x0003EC0E
		public string result { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00040A17 File Offset: 0x0003EC17
		[HideInInspector]
		public RectTransform scrollingPanel
		{
			get
			{
				return this.scrollRect.content;
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00040A24 File Offset: 0x0003EC24
		public UIVerticalScroller()
		{
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00040A78 File Offset: 0x0003EC78
		public UIVerticalScroller(RectTransform center, RectTransform elementSize, ScrollRect scrollRect, GameObject[] arrayOfElements)
		{
			this.center = center;
			this.elementSize = elementSize;
			this.scrollRect = scrollRect;
			this._arrayOfElements = arrayOfElements;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00040AE8 File Offset: 0x0003ECE8
		public void Awake()
		{
			if (!this.scrollRect)
			{
				this.scrollRect = base.GetComponent<ScrollRect>();
			}
			if (!this.center)
			{
				Debug.LogError("Please define the RectTransform for the Center viewport of the scrollable area");
			}
			if (!this.elementSize)
			{
				this.elementSize = this.center;
			}
			if (this._arrayOfElements == null || this._arrayOfElements.Length == 0)
			{
				this._arrayOfElements = new GameObject[this.scrollingPanel.childCount];
				for (int i = 0; i < this.scrollingPanel.childCount; i++)
				{
					this._arrayOfElements[i] = this.scrollingPanel.GetChild(i).gameObject;
				}
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00040B94 File Offset: 0x0003ED94
		public void updateChildren(int startingIndex = -1, GameObject[] arrayOfElements = null)
		{
			if (arrayOfElements != null)
			{
				this._arrayOfElements = arrayOfElements;
			}
			else
			{
				this._arrayOfElements = new GameObject[this.scrollingPanel.childCount];
				for (int i = 0; i < this.scrollingPanel.childCount; i++)
				{
					this._arrayOfElements[i] = this.scrollingPanel.GetChild(i).gameObject;
				}
			}
			for (int k = 0; k < this._arrayOfElements.Length; k++)
			{
				int j = k;
				this._arrayOfElements[k].GetComponent<Button>().onClick.RemoveAllListeners();
				if (this.OnButtonClicked != null)
				{
					this._arrayOfElements[k].GetComponent<Button>().onClick.AddListener(delegate()
					{
						this.OnButtonClicked.Invoke(j);
					});
				}
				RectTransform component = this._arrayOfElements[k].GetComponent<RectTransform>();
				Vector2 vector = new Vector2(0.5f, 0.5f);
				component.pivot = vector;
				component.anchorMax = (component.anchorMin = vector);
				component.localPosition = new Vector2(0f, (float)k * this.elementSize.rect.size.y);
				component.sizeDelta = this.elementSize.rect.size;
			}
			this.distance = new float[this._arrayOfElements.Length];
			this.distReposition = new float[this._arrayOfElements.Length];
			this.focusedElementIndex = -1;
			if (startingIndex > -1)
			{
				startingIndex = ((startingIndex > this._arrayOfElements.Length) ? (this._arrayOfElements.Length - 1) : startingIndex);
				this.SnapToElement(startingIndex);
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00040D34 File Offset: 0x0003EF34
		public void Start()
		{
			if (this.scrollUpButton)
			{
				this.scrollUpButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.ScrollUp();
				});
			}
			if (this.scrollDownButton)
			{
				this.scrollDownButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.ScrollDown();
				});
			}
			this.updateChildren(this.startingIndex, this._arrayOfElements);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		public void Update()
		{
			if (this._arrayOfElements.Length < 1)
			{
				return;
			}
			for (int i = 0; i < this._arrayOfElements.Length; i++)
			{
				this.distReposition[i] = this.center.GetComponent<RectTransform>().position.y - this._arrayOfElements[i].GetComponent<RectTransform>().position.y;
				this.distance[i] = Mathf.Abs(this.distReposition[i]);
				Vector2 vector = Vector2.Max(this.minScale, new Vector2(1f / (1f + this.distance[i] * this.elementShrinkage.x), 1f / (1f + this.distance[i] * this.elementShrinkage.y)));
				this._arrayOfElements[i].GetComponent<RectTransform>().transform.localScale = new Vector3(vector.x, vector.y, 1f);
			}
			float num = Mathf.Min(this.distance);
			int focusedElementIndex = this.focusedElementIndex;
			for (int j = 0; j < this._arrayOfElements.Length; j++)
			{
				this._arrayOfElements[j].GetComponent<CanvasGroup>().interactable = (!this.disableUnfocused || num == this.distance[j]);
				if (num == this.distance[j])
				{
					this.focusedElementIndex = j;
					this.result = this._arrayOfElements[j].GetComponentInChildren<Text>().text;
				}
			}
			if (this.focusedElementIndex != focusedElementIndex && this.OnFocusChanged != null)
			{
				this.OnFocusChanged.Invoke(this.focusedElementIndex);
			}
			if (!UIExtensionsInputManager.GetMouseButton(0))
			{
				this.ScrollingElements();
			}
			if (this.stopMomentumOnEnd && (this._arrayOfElements[0].GetComponent<RectTransform>().position.y > this.center.position.y || this._arrayOfElements[this._arrayOfElements.Length - 1].GetComponent<RectTransform>().position.y < this.center.position.y))
			{
				this.scrollRect.velocity = Vector2.zero;
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00040FD0 File Offset: 0x0003F1D0
		private void ScrollingElements()
		{
			float y = Mathf.Lerp(this.scrollingPanel.anchoredPosition.y, this.scrollingPanel.anchoredPosition.y + this.distReposition[this.focusedElementIndex], Time.deltaTime * 2f);
			Vector2 anchoredPosition = new Vector2(this.scrollingPanel.anchoredPosition.x, y);
			this.scrollingPanel.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00041040 File Offset: 0x0003F240
		public void SnapToElement(int element)
		{
			float num = this.elementSize.rect.height * (float)element;
			Vector2 anchoredPosition = new Vector2(this.scrollingPanel.anchoredPosition.x, -num);
			this.scrollingPanel.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004108C File Offset: 0x0003F28C
		public void ScrollUp()
		{
			float num = this.elementSize.rect.height / 1.2f;
			Vector2 b = new Vector2(this.scrollingPanel.anchoredPosition.x, this.scrollingPanel.anchoredPosition.y - num);
			this.scrollingPanel.anchoredPosition = Vector2.Lerp(this.scrollingPanel.anchoredPosition, b, 1f);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00041100 File Offset: 0x0003F300
		public void ScrollDown()
		{
			float num = this.elementSize.rect.height / 1.2f;
			Vector2 anchoredPosition = new Vector2(this.scrollingPanel.anchoredPosition.x, this.scrollingPanel.anchoredPosition.y + num);
			this.scrollingPanel.anchoredPosition = anchoredPosition;
		}

		// Token: 0x04000C87 RID: 3207
		[Tooltip("desired ScrollRect")]
		public ScrollRect scrollRect;

		// Token: 0x04000C88 RID: 3208
		[Tooltip("Center display area (position of zoomed content)")]
		public RectTransform center;

		// Token: 0x04000C89 RID: 3209
		[Tooltip("Size / spacing of elements")]
		public RectTransform elementSize;

		// Token: 0x04000C8A RID: 3210
		[Tooltip("Scale = 1/ (1+distance from center * shrinkage)")]
		public Vector2 elementShrinkage = new Vector2(0.005f, 0.005f);

		// Token: 0x04000C8B RID: 3211
		[Tooltip("Minimum element scale (furthest from center)")]
		public Vector2 minScale = new Vector2(0.7f, 0.7f);

		// Token: 0x04000C8C RID: 3212
		[Tooltip("Select the item to be in center on start.")]
		public int startingIndex = -1;

		// Token: 0x04000C8D RID: 3213
		[Tooltip("Stop scrolling past last element from inertia.")]
		public bool stopMomentumOnEnd = true;

		// Token: 0x04000C8E RID: 3214
		[Tooltip("Set Items out of center to not interactible.")]
		public bool disableUnfocused = true;

		// Token: 0x04000C8F RID: 3215
		[Tooltip("Button to go to the next page. (optional)")]
		public GameObject scrollUpButton;

		// Token: 0x04000C90 RID: 3216
		[Tooltip("Button to go to the previous page. (optional)")]
		public GameObject scrollDownButton;

		// Token: 0x04000C91 RID: 3217
		[Tooltip("Event fired when a specific item is clicked, exposes index number of item. (optional)")]
		public UIVerticalScroller.IntEvent OnButtonClicked;

		// Token: 0x04000C92 RID: 3218
		[Tooltip("Event fired when the focused item is Changed. (optional)")]
		public UIVerticalScroller.IntEvent OnFocusChanged;

		// Token: 0x04000C93 RID: 3219
		[HideInInspector]
		public GameObject[] _arrayOfElements;

		// Token: 0x04000C96 RID: 3222
		private float[] distReposition;

		// Token: 0x04000C97 RID: 3223
		private float[] distance;

		// Token: 0x0200115C RID: 4444
		[Serializable]
		public class IntEvent : UnityEvent<int>
		{
		}
	}
}

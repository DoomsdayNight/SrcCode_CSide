using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B8 RID: 696
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/Dropdown List")]
	public class DropDownList : MonoBehaviour
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0002BE9F File Offset: 0x0002A09F
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x0002BEA7 File Offset: 0x0002A0A7
		public DropDownListItem SelectedItem { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
		public float ScrollBarWidth
		{
			get
			{
				return this._scrollBarWidth;
			}
			set
			{
				this._scrollBarWidth = value;
				this.RedrawPanel();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0002BEC7 File Offset: 0x0002A0C7
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0002BECF File Offset: 0x0002A0CF
		public int ItemsToDisplay
		{
			get
			{
				return this._itemsToDisplay;
			}
			set
			{
				this._itemsToDisplay = value;
				this.RedrawPanel();
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002BEDE File Offset: 0x0002A0DE
		public void Start()
		{
			this.Initialize();
			if (this.SelectFirstItemOnStart && this.Items.Count > 0)
			{
				this.ToggleDropdownPanel(false);
				this.OnItemClicked(0);
			}
			this.RedrawPanel();
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002BF14 File Offset: 0x0002A114
		private bool Initialize()
		{
			bool result = true;
			try
			{
				this._rectTransform = base.GetComponent<RectTransform>();
				this._mainButton = new DropDownListButton(this._rectTransform.Find("MainButton").gameObject);
				this._overlayRT = this._rectTransform.Find("Overlay").GetComponent<RectTransform>();
				this._overlayRT.gameObject.SetActive(false);
				this._scrollPanelRT = this._overlayRT.Find("ScrollPanel").GetComponent<RectTransform>();
				this._scrollBarRT = this._scrollPanelRT.Find("Scrollbar").GetComponent<RectTransform>();
				this._slidingAreaRT = this._scrollBarRT.Find("SlidingArea").GetComponent<RectTransform>();
				this._scrollHandleRT = this._slidingAreaRT.Find("Handle").GetComponent<RectTransform>();
				this._itemsPanelRT = this._scrollPanelRT.Find("Items").GetComponent<RectTransform>();
				this._canvas = base.GetComponentInParent<Canvas>();
				this._canvasRT = this._canvas.GetComponent<RectTransform>();
				this._scrollRect = this._scrollPanelRT.GetComponent<ScrollRect>();
				this._scrollRect.scrollSensitivity = this._rectTransform.sizeDelta.y / 2f;
				this._scrollRect.movementType = ScrollRect.MovementType.Clamped;
				this._scrollRect.content = this._itemsPanelRT;
				this._itemTemplate = this._rectTransform.Find("ItemTemplate").gameObject;
				this._itemTemplate.SetActive(false);
			}
			catch (NullReferenceException exception)
			{
				Debug.LogException(exception);
				Debug.LogError("Something is setup incorrectly with the dropdownlist component causing a Null Reference Exception");
				result = false;
			}
			this._panelItems = new List<DropDownListButton>();
			this.RebuildPanel();
			this.RedrawPanel();
			return result;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
		public void RefreshItems(params object[] list)
		{
			this.Items.Clear();
			List<DropDownListItem> list2 = new List<DropDownListItem>();
			foreach (object obj in list)
			{
				if (obj is DropDownListItem)
				{
					list2.Add((DropDownListItem)obj);
				}
				else if (obj is string)
				{
					list2.Add(new DropDownListItem((string)obj, "", null, false, null));
				}
				else
				{
					if (!(obj is Sprite))
					{
						throw new Exception("Only ComboBoxItems, Strings, and Sprite types are allowed");
					}
					list2.Add(new DropDownListItem("", "", (Sprite)obj, false, null));
				}
			}
			this.Items.AddRange(list2);
			this.RebuildPanel();
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002C18F File Offset: 0x0002A38F
		public void AddItem(DropDownListItem item)
		{
			this.Items.Add(item);
			this.RebuildPanel();
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0002C1A3 File Offset: 0x0002A3A3
		public void AddItem(string item)
		{
			this.Items.Add(new DropDownListItem(item, "", null, false, null));
			this.RebuildPanel();
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		public void AddItem(Sprite item)
		{
			this.Items.Add(new DropDownListItem("", "", item, false, null));
			this.RebuildPanel();
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0002C1E9 File Offset: 0x0002A3E9
		public void RemoveItem(DropDownListItem item)
		{
			this.Items.Remove(item);
			this.RebuildPanel();
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0002C1FE File Offset: 0x0002A3FE
		public void RemoveItem(string item)
		{
			this.Items.Remove(new DropDownListItem(item, "", null, false, null));
			this.RebuildPanel();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002C220 File Offset: 0x0002A420
		public void RemoveItem(Sprite item)
		{
			this.Items.Remove(new DropDownListItem("", "", item, false, null));
			this.RebuildPanel();
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0002C246 File Offset: 0x0002A446
		public void ResetItems()
		{
			this.Items.Clear();
			this.RebuildPanel();
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002C25C File Offset: 0x0002A45C
		private void RebuildPanel()
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			int num = this._panelItems.Count;
			while (this._panelItems.Count < this.Items.Count)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this._itemTemplate);
				gameObject.name = "Item " + num.ToString();
				gameObject.transform.SetParent(this._itemsPanelRT, false);
				this._panelItems.Add(new DropDownListButton(gameObject));
				num++;
			}
			for (int i = 0; i < this._panelItems.Count; i++)
			{
				if (i < this.Items.Count)
				{
					DropDownListItem item = this.Items[i];
					this._panelItems[i].txt.text = item.Caption;
					if (item.IsDisabled)
					{
						this._panelItems[i].txt.color = this.disabledTextColor;
					}
					if (this._panelItems[i].btnImg != null)
					{
						this._panelItems[i].btnImg.sprite = null;
					}
					this._panelItems[i].img.sprite = item.Image;
					this._panelItems[i].img.color = ((item.Image == null) ? new Color(1f, 1f, 1f, 0f) : (item.IsDisabled ? new Color(1f, 1f, 1f, 0.5f) : Color.white));
					int ii = i;
					this._panelItems[i].btn.onClick.RemoveAllListeners();
					this._panelItems[i].btn.onClick.AddListener(delegate()
					{
						this.OnItemClicked(ii);
						if (item.OnSelect != null)
						{
							item.OnSelect();
						}
					});
				}
				this._panelItems[i].gameobject.SetActive(i < this.Items.Count);
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		private void OnItemClicked(int indx)
		{
			if (indx != this._selectedIndex && this.OnSelectionChanged != null)
			{
				this.OnSelectionChanged.Invoke(indx);
			}
			this._selectedIndex = indx;
			this.ToggleDropdownPanel(true);
			this.UpdateSelected();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
		private void UpdateSelected()
		{
			this.SelectedItem = ((this._selectedIndex > -1 && this._selectedIndex < this.Items.Count) ? this.Items[this._selectedIndex] : null);
			if (this.SelectedItem == null)
			{
				return;
			}
			if (this.SelectedItem.Image != null)
			{
				this._mainButton.img.sprite = this.SelectedItem.Image;
				this._mainButton.img.color = Color.white;
			}
			else
			{
				this._mainButton.img.sprite = null;
			}
			this._mainButton.txt.text = this.SelectedItem.Caption;
			if (this.OverrideHighlighted)
			{
				for (int i = 0; i < this._itemsPanelRT.childCount; i++)
				{
					this._panelItems[i].btnImg.color = ((this._selectedIndex == i) ? this._mainButton.btn.colors.highlightedColor : new Color(0f, 0f, 0f, 0f));
				}
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002C610 File Offset: 0x0002A810
		private void RedrawPanel()
		{
			float num = (this.Items.Count > this.ItemsToDisplay) ? this._scrollBarWidth : 0f;
			if (!this._hasDrawnOnce || this._rectTransform.sizeDelta != this._mainButton.rectTransform.sizeDelta)
			{
				this._hasDrawnOnce = true;
				this._mainButton.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._rectTransform.sizeDelta.x);
				this._mainButton.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._rectTransform.sizeDelta.y);
				this._mainButton.txt.rectTransform.offsetMax = new Vector2(4f, 0f);
				this._scrollPanelRT.SetParent(base.transform, true);
				this._scrollPanelRT.anchoredPosition = (this._displayPanelAbove ? new Vector2(0f, this._rectTransform.sizeDelta.y * (float)this.ItemsToDisplay - 1f) : new Vector2(0f, -this._rectTransform.sizeDelta.y));
				this._overlayRT.SetParent(this._canvas.transform, false);
				this._overlayRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._canvasRT.sizeDelta.x);
				this._overlayRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._canvasRT.sizeDelta.y);
				this._overlayRT.SetParent(base.transform, true);
				this._scrollPanelRT.SetParent(this._overlayRT, true);
			}
			if (this.Items.Count < 1)
			{
				return;
			}
			float num2 = this._rectTransform.sizeDelta.y * (float)Mathf.Min(this._itemsToDisplay, this.Items.Count);
			this._scrollPanelRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
			this._scrollPanelRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._rectTransform.sizeDelta.x);
			this._itemsPanelRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._scrollPanelRT.sizeDelta.x - num - 5f);
			this._itemsPanelRT.anchoredPosition = new Vector2(5f, 0f);
			this._scrollBarRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num);
			this._scrollBarRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
			if (num == 0f)
			{
				this._scrollHandleRT.gameObject.SetActive(false);
			}
			else
			{
				this._scrollHandleRT.gameObject.SetActive(true);
			}
			this._slidingAreaRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
			this._slidingAreaRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2 - this._scrollBarRT.sizeDelta.x);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002C8CC File Offset: 0x0002AACC
		public void ToggleDropdownPanel(bool directClick)
		{
			this._overlayRT.transform.localScale = new Vector3(1f, 1f, 1f);
			this._scrollBarRT.transform.localScale = new Vector3(1f, 1f, 1f);
			this._isPanelActive = !this._isPanelActive;
			this._overlayRT.gameObject.SetActive(this._isPanelActive);
			if (this._isPanelActive)
			{
				base.transform.SetAsLastSibling();
				return;
			}
		}

		// Token: 0x04000A0B RID: 2571
		public Color disabledTextColor;

		// Token: 0x04000A0D RID: 2573
		public List<DropDownListItem> Items;

		// Token: 0x04000A0E RID: 2574
		public bool OverrideHighlighted = true;

		// Token: 0x04000A0F RID: 2575
		private bool _isPanelActive;

		// Token: 0x04000A10 RID: 2576
		private bool _hasDrawnOnce;

		// Token: 0x04000A11 RID: 2577
		private DropDownListButton _mainButton;

		// Token: 0x04000A12 RID: 2578
		private RectTransform _rectTransform;

		// Token: 0x04000A13 RID: 2579
		private RectTransform _overlayRT;

		// Token: 0x04000A14 RID: 2580
		private RectTransform _scrollPanelRT;

		// Token: 0x04000A15 RID: 2581
		private RectTransform _scrollBarRT;

		// Token: 0x04000A16 RID: 2582
		private RectTransform _slidingAreaRT;

		// Token: 0x04000A17 RID: 2583
		private RectTransform _scrollHandleRT;

		// Token: 0x04000A18 RID: 2584
		private RectTransform _itemsPanelRT;

		// Token: 0x04000A19 RID: 2585
		private Canvas _canvas;

		// Token: 0x04000A1A RID: 2586
		private RectTransform _canvasRT;

		// Token: 0x04000A1B RID: 2587
		private ScrollRect _scrollRect;

		// Token: 0x04000A1C RID: 2588
		private List<DropDownListButton> _panelItems;

		// Token: 0x04000A1D RID: 2589
		private GameObject _itemTemplate;

		// Token: 0x04000A1E RID: 2590
		[SerializeField]
		private float _scrollBarWidth = 20f;

		// Token: 0x04000A1F RID: 2591
		private int _selectedIndex = -1;

		// Token: 0x04000A20 RID: 2592
		[SerializeField]
		private int _itemsToDisplay;

		// Token: 0x04000A21 RID: 2593
		public bool SelectFirstItemOnStart;

		// Token: 0x04000A22 RID: 2594
		[SerializeField]
		private bool _displayPanelAbove;

		// Token: 0x04000A23 RID: 2595
		public DropDownList.SelectionChangedEvent OnSelectionChanged;

		// Token: 0x0200112C RID: 4396
		[Serializable]
		public class SelectionChangedEvent : UnityEvent<int>
		{
		}
	}
}

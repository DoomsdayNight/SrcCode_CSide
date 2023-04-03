using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B7 RID: 695
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/ComboBox")]
	public class ComboBox : MonoBehaviour
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0002B651 File Offset: 0x00029851
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0002B659 File Offset: 0x00029859
		public DropDownListItem SelectedItem { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0002B662 File Offset: 0x00029862
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x0002B66A File Offset: 0x0002986A
		public string Text { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0002B673 File Offset: 0x00029873
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x0002B67B File Offset: 0x0002987B
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0002B68A File Offset: 0x0002988A
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0002B692 File Offset: 0x00029892
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

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002B6A1 File Offset: 0x000298A1
		public void Awake()
		{
			this.Initialize();
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0002B6AA File Offset: 0x000298AA
		public void Start()
		{
			this.RedrawPanel();
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002B6B4 File Offset: 0x000298B4
		private bool Initialize()
		{
			bool result = true;
			try
			{
				this._rectTransform = base.GetComponent<RectTransform>();
				this._inputRT = this._rectTransform.Find("InputField").GetComponent<RectTransform>();
				this._mainInput = this._inputRT.GetComponent<InputField>();
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
				this.itemTemplate = this._rectTransform.Find("ItemTemplate").gameObject;
				this.itemTemplate.SetActive(false);
			}
			catch (NullReferenceException exception)
			{
				Debug.LogException(exception);
				Debug.LogError("Something is setup incorrectly with the dropdownlist component causing a Null Reference Exception");
				result = false;
			}
			this.panelObjects = new Dictionary<string, GameObject>();
			this._panelItems = this.AvailableOptions.ToList<string>();
			this.RebuildPanel();
			return result;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002B898 File Offset: 0x00029A98
		public void AddItem(string item)
		{
			this.AvailableOptions.Add(item);
			this.RebuildPanel();
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0002B8AC File Offset: 0x00029AAC
		public void RemoveItem(string item)
		{
			this.AvailableOptions.Remove(item);
			this.RebuildPanel();
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002B8C1 File Offset: 0x00029AC1
		public void SetAvailableOptions(List<string> newOptions)
		{
			this.AvailableOptions.Clear();
			this.AvailableOptions = newOptions;
			this.RebuildPanel();
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0002B8DC File Offset: 0x00029ADC
		public void SetAvailableOptions(string[] newOptions)
		{
			this.AvailableOptions.Clear();
			for (int i = 0; i < newOptions.Length; i++)
			{
				this.AvailableOptions.Add(newOptions[i]);
			}
			this.RebuildPanel();
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0002B916 File Offset: 0x00029B16
		public void ResetItems()
		{
			this.AvailableOptions.Clear();
			this.RebuildPanel();
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0002B92C File Offset: 0x00029B2C
		private void RebuildPanel()
		{
			this._panelItems.Clear();
			foreach (string text in this.AvailableOptions)
			{
				this._panelItems.Add(text.ToLower());
			}
			List<GameObject> list = new List<GameObject>(this.panelObjects.Values);
			this.panelObjects.Clear();
			int num = 0;
			while (list.Count < this.AvailableOptions.Count)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemTemplate);
				gameObject.name = "Item " + num.ToString();
				gameObject.transform.SetParent(this._itemsPanelRT, false);
				list.Add(gameObject);
				num++;
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].SetActive(i <= this.AvailableOptions.Count);
				if (i < this.AvailableOptions.Count)
				{
					list[i].name = "Item " + i.ToString() + " " + this._panelItems[i];
					list[i].transform.Find("Text").GetComponent<Text>().text = this.AvailableOptions[i];
					Button component = list[i].GetComponent<Button>();
					component.onClick.RemoveAllListeners();
					string textOfItem = this._panelItems[i];
					component.onClick.AddListener(delegate()
					{
						this.OnItemClicked(textOfItem);
					});
					this.panelObjects[this._panelItems[i]] = list[i];
				}
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0002BB28 File Offset: 0x00029D28
		private void OnItemClicked(string item)
		{
			this.Text = item;
			this._mainInput.text = this.Text;
			this.ToggleDropdownPanel(true);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0002BB4C File Offset: 0x00029D4C
		private void RedrawPanel()
		{
			float num = (this._panelItems.Count > this.ItemsToDisplay) ? this._scrollBarWidth : 0f;
			this._scrollBarRT.gameObject.SetActive(this._panelItems.Count > this.ItemsToDisplay);
			if (!this._hasDrawnOnce || this._rectTransform.sizeDelta != this._inputRT.sizeDelta)
			{
				this._hasDrawnOnce = true;
				this._inputRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._rectTransform.sizeDelta.x);
				this._inputRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._rectTransform.sizeDelta.y);
				this._scrollPanelRT.SetParent(base.transform, true);
				this._scrollPanelRT.anchoredPosition = (this._displayPanelAbove ? new Vector2(0f, this._rectTransform.sizeDelta.y * (float)this.ItemsToDisplay - 1f) : new Vector2(0f, -this._rectTransform.sizeDelta.y));
				this._overlayRT.SetParent(this._canvas.transform, false);
				this._overlayRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._canvasRT.sizeDelta.x);
				this._overlayRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._canvasRT.sizeDelta.y);
				this._overlayRT.SetParent(base.transform, true);
				this._scrollPanelRT.SetParent(this._overlayRT, true);
			}
			if (this._panelItems.Count < 1)
			{
				return;
			}
			float num2 = this._rectTransform.sizeDelta.y * (float)Mathf.Min(this._itemsToDisplay, this._panelItems.Count);
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

		// Token: 0x06000E6C RID: 3692 RVA: 0x0002BDF8 File Offset: 0x00029FF8
		public void OnValueChanged(string currText)
		{
			this.Text = currText;
			this.RedrawPanel();
			if (this._panelItems.Count == 0)
			{
				this._isPanelActive = true;
				this.ToggleDropdownPanel(false);
			}
			else if (!this._isPanelActive)
			{
				this.ToggleDropdownPanel(false);
			}
			this.OnSelectionChanged.Invoke(this.Text);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0002BE4F File Offset: 0x0002A04F
		public void ToggleDropdownPanel(bool directClick)
		{
			this._isPanelActive = !this._isPanelActive;
			this._overlayRT.gameObject.SetActive(this._isPanelActive);
			if (this._isPanelActive)
			{
				base.transform.SetAsLastSibling();
				return;
			}
		}

		// Token: 0x040009F2 RID: 2546
		public Color disabledTextColor;

		// Token: 0x040009F4 RID: 2548
		public List<string> AvailableOptions;

		// Token: 0x040009F5 RID: 2549
		[SerializeField]
		private float _scrollBarWidth = 20f;

		// Token: 0x040009F6 RID: 2550
		[SerializeField]
		private int _itemsToDisplay;

		// Token: 0x040009F7 RID: 2551
		[SerializeField]
		private bool _displayPanelAbove;

		// Token: 0x040009F8 RID: 2552
		public ComboBox.SelectionChangedEvent OnSelectionChanged;

		// Token: 0x040009F9 RID: 2553
		private bool _isPanelActive;

		// Token: 0x040009FA RID: 2554
		private bool _hasDrawnOnce;

		// Token: 0x040009FB RID: 2555
		private InputField _mainInput;

		// Token: 0x040009FC RID: 2556
		private RectTransform _inputRT;

		// Token: 0x040009FD RID: 2557
		private RectTransform _rectTransform;

		// Token: 0x040009FE RID: 2558
		private RectTransform _overlayRT;

		// Token: 0x040009FF RID: 2559
		private RectTransform _scrollPanelRT;

		// Token: 0x04000A00 RID: 2560
		private RectTransform _scrollBarRT;

		// Token: 0x04000A01 RID: 2561
		private RectTransform _slidingAreaRT;

		// Token: 0x04000A02 RID: 2562
		private RectTransform _scrollHandleRT;

		// Token: 0x04000A03 RID: 2563
		private RectTransform _itemsPanelRT;

		// Token: 0x04000A04 RID: 2564
		private Canvas _canvas;

		// Token: 0x04000A05 RID: 2565
		private RectTransform _canvasRT;

		// Token: 0x04000A06 RID: 2566
		private ScrollRect _scrollRect;

		// Token: 0x04000A07 RID: 2567
		private List<string> _panelItems;

		// Token: 0x04000A08 RID: 2568
		private Dictionary<string, GameObject> panelObjects;

		// Token: 0x04000A09 RID: 2569
		private GameObject itemTemplate;

		// Token: 0x0200112A RID: 4394
		[Serializable]
		public class SelectionChangedEvent : UnityEvent<string>
		{
		}
	}
}

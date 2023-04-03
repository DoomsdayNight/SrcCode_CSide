using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002B6 RID: 694
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/AutoComplete ComboBox")]
	public class AutoCompleteComboBox : MonoBehaviour
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0002A975 File Offset: 0x00028B75
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x0002A97D File Offset: 0x00028B7D
		public DropDownListItem SelectedItem { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0002A986 File Offset: 0x00028B86
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0002A98E File Offset: 0x00028B8E
		public string Text { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0002A997 File Offset: 0x00028B97
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0002A99F File Offset: 0x00028B9F
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0002A9AE File Offset: 0x00028BAE
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0002A9B6 File Offset: 0x00028BB6
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0002A9C5 File Offset: 0x00028BC5
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x0002A9CD File Offset: 0x00028BCD
		public bool InputColorMatching
		{
			get
			{
				return this._ChangeInputTextColorBasedOnMatchingItems;
			}
			set
			{
				this._ChangeInputTextColorBasedOnMatchingItems = value;
				if (this._ChangeInputTextColorBasedOnMatchingItems)
				{
					this.SetInputTextColor();
				}
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002A9E4 File Offset: 0x00028BE4
		public void Awake()
		{
			this.Initialize();
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002A9ED File Offset: 0x00028BED
		public void Start()
		{
			if (this.SelectFirstItemOnStart && this.AvailableOptions.Count > 0)
			{
				this.ToggleDropdownPanel(false);
				this.OnItemClicked(this.AvailableOptions[0]);
			}
			this.RedrawPanel();
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0002AA24 File Offset: 0x00028C24
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
			this._prunedPanelItems = new List<string>();
			this._panelItems = new List<string>();
			this.RebuildPanel();
			return result;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0002AC0C File Offset: 0x00028E0C
		public void AddItem(string item)
		{
			if (!this.AvailableOptions.Contains(item))
			{
				this.AvailableOptions.Add(item);
				this.RebuildPanel();
				return;
			}
			Debug.LogWarning("AutoCompleteComboBox.AddItem: items may only exists once. '" + item + "' can not be added.");
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0002AC44 File Offset: 0x00028E44
		public void RemoveItem(string item)
		{
			if (this.AvailableOptions.Contains(item))
			{
				this.AvailableOptions.Remove(item);
				this.RebuildPanel();
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0002AC68 File Offset: 0x00028E68
		public void SetAvailableOptions(List<string> newOptions)
		{
			List<string> list = newOptions.Distinct<string>().ToList<string>();
			if (newOptions.Count != list.Count)
			{
				Debug.LogWarning(string.Format("{0}.{1}: items may only exists once. {2} duplicates.", "AutoCompleteComboBox", "SetAvailableOptions", newOptions.Count - list.Count));
			}
			this.AvailableOptions.Clear();
			this.AvailableOptions = list;
			this.RebuildPanel();
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		public void SetAvailableOptions(string[] newOptions)
		{
			List<string> list = newOptions.Distinct<string>().ToList<string>();
			if (newOptions.Length != list.Count)
			{
				Debug.LogWarning(string.Format("{0}.{1}: items may only exists once. {2} duplicates.", "AutoCompleteComboBox", "SetAvailableOptions", newOptions.Length - list.Count));
			}
			this.AvailableOptions.Clear();
			for (int i = 0; i < newOptions.Length; i++)
			{
				this.AvailableOptions.Add(newOptions[i]);
			}
			this.RebuildPanel();
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0002AD4D File Offset: 0x00028F4D
		public void ResetItems()
		{
			this.AvailableOptions.Clear();
			this.RebuildPanel();
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002AD60 File Offset: 0x00028F60
		private void RebuildPanel()
		{
			if (this._isPanelActive)
			{
				this.ToggleDropdownPanel(false);
			}
			this._panelItems.Clear();
			this._prunedPanelItems.Clear();
			this.panelObjects.Clear();
			foreach (object obj in this._itemsPanelRT.transform)
			{
				Object.Destroy(((Transform)obj).gameObject);
			}
			foreach (string text in this.AvailableOptions)
			{
				this._panelItems.Add(text.ToLower());
			}
			List<GameObject> list = new List<GameObject>(this.panelObjects.Values);
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
			this.SetInputTextColor();
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0002AFD0 File Offset: 0x000291D0
		private void OnItemClicked(string item)
		{
			this.Text = item;
			this._mainInput.text = this.Text;
			this.ToggleDropdownPanel(true);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0002AFF4 File Offset: 0x000291F4
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
				this._scrollPanelRT.anchoredPosition = (this._displayPanelAbove ? new Vector2(0f, this.DropdownOffset + this._rectTransform.sizeDelta.y * (float)this._panelItems.Count - 1f) : new Vector2(0f, -this._rectTransform.sizeDelta.y));
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
			float num2 = this._rectTransform.sizeDelta.y * (float)Mathf.Min(this._itemsToDisplay, this._panelItems.Count) + this.DropdownOffset;
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

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002B2B0 File Offset: 0x000294B0
		public void OnValueChanged(string currText)
		{
			this.Text = currText;
			this.PruneItems(currText);
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
			bool flag = this._panelItems.Contains(this.Text) != this._selectionIsValid;
			this._selectionIsValid = this._panelItems.Contains(this.Text);
			this.OnSelectionChanged.Invoke(this.Text, this._selectionIsValid);
			this.OnSelectionTextChanged.Invoke(this.Text);
			if (flag)
			{
				this.OnSelectionValidityChanged.Invoke(this._selectionIsValid);
			}
			this.SetInputTextColor();
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0002B374 File Offset: 0x00029574
		private void SetInputTextColor()
		{
			if (this.InputColorMatching)
			{
				if (this._selectionIsValid)
				{
					this._mainInput.textComponent.color = this.ValidSelectionTextColor;
					return;
				}
				if (this._panelItems.Count > 0)
				{
					this._mainInput.textComponent.color = this.MatchingItemsRemainingTextColor;
					return;
				}
				this._mainInput.textComponent.color = this.NoItemsRemainingTextColor;
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0002B3E3 File Offset: 0x000295E3
		public void ToggleDropdownPanel(bool directClick = false)
		{
			this._isPanelActive = !this._isPanelActive;
			this._overlayRT.gameObject.SetActive(this._isPanelActive);
			if (this._isPanelActive)
			{
				base.transform.SetAsLastSibling();
				return;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0002B420 File Offset: 0x00029620
		private void PruneItems(string currText)
		{
			if (this.autocompleteSearchType == AutoCompleteSearchType.Linq)
			{
				this.PruneItemsLinq(currText);
				return;
			}
			this.PruneItemsArray(currText);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0002B43C File Offset: 0x0002963C
		private void PruneItemsLinq(string currText)
		{
			currText = currText.ToLower();
			foreach (string text in (from x in this._panelItems
			where !x.Contains(currText)
			select x).ToArray<string>())
			{
				this.panelObjects[text].SetActive(false);
				this._panelItems.Remove(text);
				this._prunedPanelItems.Add(text);
			}
			foreach (string text2 in (from x in this._prunedPanelItems
			where x.Contains(currText)
			select x).ToArray<string>())
			{
				this.panelObjects[text2].SetActive(true);
				this._panelItems.Add(text2);
				this._prunedPanelItems.Remove(text2);
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0002B520 File Offset: 0x00029720
		private void PruneItemsArray(string currText)
		{
			string value = currText.ToLower();
			for (int i = this._panelItems.Count - 1; i >= 0; i--)
			{
				string text = this._panelItems[i];
				if (!text.Contains(value))
				{
					this.panelObjects[this._panelItems[i]].SetActive(false);
					this._panelItems.RemoveAt(i);
					this._prunedPanelItems.Add(text);
				}
			}
			for (int j = this._prunedPanelItems.Count - 1; j >= 0; j--)
			{
				string text2 = this._prunedPanelItems[j];
				if (text2.Contains(value))
				{
					this.panelObjects[this._prunedPanelItems[j]].SetActive(true);
					this._prunedPanelItems.RemoveAt(j);
					this._panelItems.Add(text2);
				}
			}
		}

		// Token: 0x040009CE RID: 2510
		public Color disabledTextColor;

		// Token: 0x040009D0 RID: 2512
		public List<string> AvailableOptions;

		// Token: 0x040009D1 RID: 2513
		private bool _isPanelActive;

		// Token: 0x040009D2 RID: 2514
		private bool _hasDrawnOnce;

		// Token: 0x040009D3 RID: 2515
		private InputField _mainInput;

		// Token: 0x040009D4 RID: 2516
		private RectTransform _inputRT;

		// Token: 0x040009D5 RID: 2517
		private RectTransform _rectTransform;

		// Token: 0x040009D6 RID: 2518
		private RectTransform _overlayRT;

		// Token: 0x040009D7 RID: 2519
		private RectTransform _scrollPanelRT;

		// Token: 0x040009D8 RID: 2520
		private RectTransform _scrollBarRT;

		// Token: 0x040009D9 RID: 2521
		private RectTransform _slidingAreaRT;

		// Token: 0x040009DA RID: 2522
		private RectTransform _scrollHandleRT;

		// Token: 0x040009DB RID: 2523
		private RectTransform _itemsPanelRT;

		// Token: 0x040009DC RID: 2524
		private Canvas _canvas;

		// Token: 0x040009DD RID: 2525
		private RectTransform _canvasRT;

		// Token: 0x040009DE RID: 2526
		private ScrollRect _scrollRect;

		// Token: 0x040009DF RID: 2527
		private List<string> _panelItems;

		// Token: 0x040009E0 RID: 2528
		private List<string> _prunedPanelItems;

		// Token: 0x040009E1 RID: 2529
		private Dictionary<string, GameObject> panelObjects;

		// Token: 0x040009E2 RID: 2530
		private GameObject itemTemplate;

		// Token: 0x040009E4 RID: 2532
		[SerializeField]
		private float _scrollBarWidth = 20f;

		// Token: 0x040009E5 RID: 2533
		[SerializeField]
		private int _itemsToDisplay;

		// Token: 0x040009E6 RID: 2534
		public bool SelectFirstItemOnStart;

		// Token: 0x040009E7 RID: 2535
		[SerializeField]
		[Tooltip("Change input text color based on matching items")]
		private bool _ChangeInputTextColorBasedOnMatchingItems;

		// Token: 0x040009E8 RID: 2536
		public float DropdownOffset = 10f;

		// Token: 0x040009E9 RID: 2537
		public Color ValidSelectionTextColor = Color.green;

		// Token: 0x040009EA RID: 2538
		public Color MatchingItemsRemainingTextColor = Color.black;

		// Token: 0x040009EB RID: 2539
		public Color NoItemsRemainingTextColor = Color.red;

		// Token: 0x040009EC RID: 2540
		public AutoCompleteSearchType autocompleteSearchType = AutoCompleteSearchType.Linq;

		// Token: 0x040009ED RID: 2541
		[SerializeField]
		private bool _displayPanelAbove;

		// Token: 0x040009EE RID: 2542
		private bool _selectionIsValid;

		// Token: 0x040009EF RID: 2543
		public AutoCompleteComboBox.SelectionTextChangedEvent OnSelectionTextChanged;

		// Token: 0x040009F0 RID: 2544
		public AutoCompleteComboBox.SelectionValidityChangedEvent OnSelectionValidityChanged;

		// Token: 0x040009F1 RID: 2545
		public AutoCompleteComboBox.SelectionChangedEvent OnSelectionChanged;

		// Token: 0x02001125 RID: 4389
		[Serializable]
		public class SelectionChangedEvent : UnityEvent<string, bool>
		{
		}

		// Token: 0x02001126 RID: 4390
		[Serializable]
		public class SelectionTextChangedEvent : UnityEvent<string>
		{
		}

		// Token: 0x02001127 RID: 4391
		[Serializable]
		public class SelectionValidityChangedEvent : UnityEvent<bool>
		{
		}
	}
}

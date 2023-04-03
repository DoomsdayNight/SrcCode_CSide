using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Component
{
	// Token: 0x02000C57 RID: 3159
	public class NKCUIComFoldableList : MonoBehaviour
	{
		// Token: 0x06009334 RID: 37684 RVA: 0x00323A5A File Offset: 0x00321C5A
		private void Start()
		{
			this.m_uiBase = NKCUIManager.FindRootUIBase(base.transform);
		}

		// Token: 0x06009335 RID: 37685 RVA: 0x00323A70 File Offset: 0x00321C70
		public void BuildList(List<NKCUIComFoldableList.Element> lstElement, NKCUIComFoldableList.OnSelectList onSelect)
		{
			if (this.m_scrollRect == null)
			{
				Debug.LogError("ScrollRect null!!");
				return;
			}
			if (this.m_scrollRect.content == null)
			{
				Debug.LogError("ScrollRect content null!!");
				return;
			}
			this.ValidateInput(lstElement);
			this.dOnSelectList = onSelect;
			this._BuildList(lstElement);
		}

		// Token: 0x06009336 RID: 37686 RVA: 0x00323ACC File Offset: 0x00321CCC
		public void SelectMajorSlot(int majorKey)
		{
			NKCUIComFoldableListSlot nkcuicomFoldableListSlot;
			if (this.m_dicMajorSlot.TryGetValue(majorKey, out nkcuicomFoldableListSlot))
			{
				nkcuicomFoldableListSlot.Select(true, true);
				this.FoldSlot(majorKey, true);
				this.m_SelectedMajorIndex = majorKey;
				this.m_SelectedMinorIndex = 0;
				NKCUIComFoldableListSlot nkcuicomFoldableListSlot2;
				if (!this.m_bCanSelectMajor && this.m_dicFirstMinorSlot.TryGetValue(majorKey, out nkcuicomFoldableListSlot2))
				{
					nkcuicomFoldableListSlot2.Select(true, true);
				}
			}
		}

		// Token: 0x06009337 RID: 37687 RVA: 0x00323B28 File Offset: 0x00321D28
		public void SelectMinorSlot(int majorKey, int minorKey)
		{
			if (this.m_SelectedMajorIndex != majorKey)
			{
				this.SelectMajorSlot(majorKey);
			}
			NKCUIComFoldableListSlot nkcuicomFoldableListSlot;
			if (this.m_dicMinorSlot.TryGetValue(new Tuple<int, int>(majorKey, minorKey), out nkcuicomFoldableListSlot))
			{
				this.m_SelectedMinorIndex = minorKey;
				nkcuicomFoldableListSlot.Select(true, true);
			}
		}

		// Token: 0x06009338 RID: 37688 RVA: 0x00323B6A File Offset: 0x00321D6A
		public NKCUIComFoldableListSlot GetSlot(bool bMajor, int majorKey, int minorKey)
		{
			if (bMajor)
			{
				return this.GetMajorSlot(majorKey);
			}
			return this.GetMinorSlot(majorKey, minorKey);
		}

		// Token: 0x06009339 RID: 37689 RVA: 0x00323B80 File Offset: 0x00321D80
		public NKCUIComFoldableListSlot GetMajorSlot(int majorKey)
		{
			NKCUIComFoldableListSlot result;
			if (this.m_dicMajorSlot.TryGetValue(majorKey, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600933A RID: 37690 RVA: 0x00323BA0 File Offset: 0x00321DA0
		public NKCUIComFoldableListSlot GetMinorSlot(int majorKey, int minorKey)
		{
			NKCUIComFoldableListSlot result;
			if (this.m_dicMinorSlot.TryGetValue(new Tuple<int, int>(majorKey, minorKey), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600933B RID: 37691 RVA: 0x00323BC8 File Offset: 0x00321DC8
		public void UnselectAll()
		{
			this.m_SelectedMajorIndex = -1;
			this.m_SelectedMinorIndex = -1;
			if (this.m_eMode == NKCUIComFoldableList.Mode.AllOpened)
			{
				foreach (KeyValuePair<int, NKCUIComFoldableListSlot> keyValuePair in this.m_dicMajorSlot)
				{
					NKCUIComFoldableListSlot value = keyValuePair.Value;
					if (value != null)
					{
						value.Select(false, true);
					}
				}
				using (Dictionary<Tuple<int, int>, NKCUIComFoldableListSlot>.Enumerator enumerator2 = this.m_dicMinorSlot.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<Tuple<int, int>, NKCUIComFoldableListSlot> keyValuePair2 = enumerator2.Current;
						NKCUIComFoldableListSlot value2 = keyValuePair2.Value;
						if (value2 != null)
						{
							value2.Select(false, true);
						}
					}
					return;
				}
			}
			foreach (KeyValuePair<int, NKCUIComFoldableListSlot> keyValuePair3 in this.m_dicMajorSlot)
			{
				this.FoldSlot(keyValuePair3.Key, false);
				NKCUIComFoldableListSlot value3 = keyValuePair3.Value;
				if (value3 != null)
				{
					value3.Select(false, true);
				}
			}
		}

		// Token: 0x0600933C RID: 37692 RVA: 0x00323CF0 File Offset: 0x00321EF0
		private void _BuildList(List<NKCUIComFoldableList.Element> lstElement)
		{
			this.m_SelectedMajorIndex = -1;
			this.m_SelectedMinorIndex = -1;
			this.m_lstSelectableElements.Clear();
			Stack<NKCUIComFoldableListSlot> stack = new Stack<NKCUIComFoldableListSlot>(this.m_dicMajorSlot.Values);
			Stack<NKCUIComFoldableListSlot> stack2 = new Stack<NKCUIComFoldableListSlot>(this.m_dicMinorSlot.Values);
			foreach (NKCUIComFoldableListSlot nkcuicomFoldableListSlot in this.m_dicMajorSlot.Values)
			{
				nkcuicomFoldableListSlot.ClearChild();
			}
			this.m_dicFirstMinorSlot.Clear();
			lstElement.Sort();
			Dictionary<int, NKCUIComFoldableListSlot> dictionary = new Dictionary<int, NKCUIComFoldableListSlot>();
			Dictionary<Tuple<int, int>, NKCUIComFoldableListSlot> dictionary2 = new Dictionary<Tuple<int, int>, NKCUIComFoldableListSlot>();
			List<NKCUIComFoldableListSlot> list = new List<NKCUIComFoldableListSlot>();
			using (List<NKCUIComFoldableList.Element>.Enumerator enumerator2 = lstElement.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					NKCUIComFoldableList.Element element = enumerator2.Current;
					NKCUIComFoldableListSlot nkcuicomFoldableListSlot3;
					if (element.isMajor)
					{
						NKCUIComFoldableListSlot nkcuicomFoldableListSlot2;
						if (stack.Count > 0)
						{
							nkcuicomFoldableListSlot2 = stack.Pop();
						}
						else
						{
							nkcuicomFoldableListSlot2 = this.GetNewSlot(true);
						}
						nkcuicomFoldableListSlot2.SetData(element, new NKCUIComFoldableListSlot.OnSelectSlot(this.OnSelectSlot));
						nkcuicomFoldableListSlot2.SetToggleGroup(this.m_MajorToggleGroup);
						dictionary.Add(element.MajorKey, nkcuicomFoldableListSlot2);
						list.Add(nkcuicomFoldableListSlot2);
						if (this.m_bCanSelectMajor)
						{
							this.m_lstSelectableElements.Add(element);
						}
					}
					else if (!dictionary.TryGetValue(element.MajorKey, out nkcuicomFoldableListSlot3))
					{
						Debug.LogError(string.Format("Logic Error : MajorKey {0} slot not created! ", element.MajorKey));
					}
					else
					{
						NKCUIComFoldableListSlot nkcuicomFoldableListSlot4;
						if (stack2.Count > 0)
						{
							nkcuicomFoldableListSlot4 = stack2.Pop();
						}
						else
						{
							nkcuicomFoldableListSlot4 = this.GetNewSlot(false);
						}
						nkcuicomFoldableListSlot4.SetData(element, new NKCUIComFoldableListSlot.OnSelectSlot(this.OnSelectSlot));
						nkcuicomFoldableListSlot4.SetToggleGroup(this.m_MinorToggleGroup);
						nkcuicomFoldableListSlot3.AddChild(nkcuicomFoldableListSlot4);
						if (!this.m_dicFirstMinorSlot.ContainsKey(element.MajorKey))
						{
							this.m_dicFirstMinorSlot.Add(element.MajorKey, nkcuicomFoldableListSlot4);
						}
						dictionary2.Add(element.KeyPair, nkcuicomFoldableListSlot4);
						list.Add(nkcuicomFoldableListSlot4);
						this.m_lstSelectableElements.Add(element);
					}
				}
				goto IL_21B;
			}
			IL_20B:
			UnityEngine.Object.Destroy(stack.Pop().gameObject);
			IL_21B:
			if (stack.Count <= 0)
			{
				while (stack2.Count > 0)
				{
					UnityEngine.Object.Destroy(stack2.Pop().gameObject);
				}
				foreach (NKCUIComFoldableListSlot nkcuicomFoldableListSlot5 in list)
				{
					nkcuicomFoldableListSlot5.transform.SetParent(this.m_scrollRect.content);
					nkcuicomFoldableListSlot5.transform.SetAsLastSibling();
				}
				this.m_dicMajorSlot = dictionary;
				this.m_dicMinorSlot = dictionary2;
				foreach (KeyValuePair<int, NKCUIComFoldableListSlot> keyValuePair in dictionary)
				{
					this.FoldSlot(keyValuePair.Key, false);
				}
				return;
			}
			goto IL_20B;
		}

		// Token: 0x0600933D RID: 37693 RVA: 0x00324034 File Offset: 0x00322234
		private void FoldSlot(int majorKey, bool bOpen)
		{
			switch (this.m_eMode)
			{
			case NKCUIComFoldableList.Mode.SingleOpenOnly:
				if (!bOpen)
				{
					NKCUIComFoldableListSlot nkcuicomFoldableListSlot = this.m_dicMajorSlot[majorKey];
					if (nkcuicomFoldableListSlot == null)
					{
						return;
					}
					nkcuicomFoldableListSlot.ActivateChild(false);
					return;
				}
				else
				{
					using (Dictionary<int, NKCUIComFoldableListSlot>.Enumerator enumerator = this.m_dicMajorSlot.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<int, NKCUIComFoldableListSlot> keyValuePair = enumerator.Current;
							NKCUIComFoldableListSlot value = keyValuePair.Value;
							if (value != null)
							{
								value.ActivateChild(majorKey == keyValuePair.Key);
							}
						}
						return;
					}
				}
				break;
			case NKCUIComFoldableList.Mode.AllOpened:
				break;
			case NKCUIComFoldableList.Mode.MultipleOpenClose:
			{
				NKCUIComFoldableListSlot nkcuicomFoldableListSlot2 = this.m_dicMajorSlot[majorKey];
				if (nkcuicomFoldableListSlot2 == null)
				{
					return;
				}
				nkcuicomFoldableListSlot2.ActivateChild(bOpen);
				return;
			}
			default:
				return;
			}
			NKCUIComFoldableListSlot nkcuicomFoldableListSlot3 = this.m_dicMajorSlot[majorKey];
			if (nkcuicomFoldableListSlot3 == null)
			{
				return;
			}
			nkcuicomFoldableListSlot3.ActivateChild(true);
		}

		// Token: 0x0600933E RID: 37694 RVA: 0x00324100 File Offset: 0x00322300
		private void OnSelectSlot(int majorKey, int minorKey, bool bToggleValue, bool isMajor)
		{
			if (isMajor)
			{
				if (bToggleValue)
				{
					this.m_SelectedMajorIndex = majorKey;
					this.m_SelectedMinorIndex = minorKey;
				}
				this.FoldSlot(majorKey, bToggleValue);
				if (bToggleValue)
				{
					NKCUIComFoldableListSlot nkcuicomFoldableListSlot;
					if (this.m_bCanSelectMajor)
					{
						NKCUIComFoldableList.OnSelectList onSelectList = this.dOnSelectList;
						if (onSelectList == null)
						{
							return;
						}
						onSelectList(majorKey, minorKey);
						return;
					}
					else if (this.m_dicFirstMinorSlot.TryGetValue(majorKey, out nkcuicomFoldableListSlot))
					{
						nkcuicomFoldableListSlot.Select(true, true);
						NKCUIComFoldableList.OnSelectList onSelectList2 = this.dOnSelectList;
						if (onSelectList2 == null)
						{
							return;
						}
						onSelectList2(nkcuicomFoldableListSlot.MajorKey, nkcuicomFoldableListSlot.MinorKey);
						return;
					}
				}
			}
			else if (bToggleValue)
			{
				NKCUIComFoldableList.OnSelectList onSelectList3 = this.dOnSelectList;
				if (onSelectList3 == null)
				{
					return;
				}
				onSelectList3(majorKey, minorKey);
			}
		}

		// Token: 0x0600933F RID: 37695 RVA: 0x00324192 File Offset: 0x00322392
		private NKCUIComFoldableListSlot GetNewSlot(bool bMajor)
		{
			if (bMajor)
			{
				return UnityEngine.Object.Instantiate<NKCUIComFoldableListSlot>(this.m_pfbMajor);
			}
			return UnityEngine.Object.Instantiate<NKCUIComFoldableListSlot>(this.m_pfbMinor);
		}

		// Token: 0x06009340 RID: 37696 RVA: 0x003241B0 File Offset: 0x003223B0
		private bool ValidateInput(List<NKCUIComFoldableList.Element> lstElement)
		{
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<Tuple<int, int>> hashSet2 = new HashSet<Tuple<int, int>>();
			foreach (NKCUIComFoldableList.Element element in lstElement)
			{
				if (element.isMajor)
				{
					if (hashSet.Contains(element.MajorKey))
					{
						Debug.LogError("Major key duplicate!");
						return false;
					}
					hashSet.Add(element.MajorKey);
				}
			}
			foreach (NKCUIComFoldableList.Element element2 in lstElement)
			{
				if (element2.isMinor)
				{
					if (!hashSet.Contains(element2.MajorKey))
					{
						Debug.LogError(string.Format("There is no Major Key {0}", element2.MajorKey));
						return false;
					}
					Tuple<int, int> keyPair = element2.KeyPair;
					if (hashSet2.Contains(keyPair))
					{
						Debug.LogError(string.Format("Minor Key Duplicate : {0}, {1}", element2.MajorKey, element2.MinorKey));
						return false;
					}
					hashSet2.Add(keyPair);
				}
			}
			return true;
		}

		// Token: 0x06009341 RID: 37697 RVA: 0x003242FC File Offset: 0x003224FC
		private void Update()
		{
			this.ProcessHotkey();
		}

		// Token: 0x06009342 RID: 37698 RVA: 0x00324304 File Offset: 0x00322504
		public void ProcessHotkey()
		{
			if (!NKCUIManager.IsTopmostUI(this.m_uiBase))
			{
				return;
			}
			if (this.m_scrollRect != null)
			{
				if (this.m_scrollRect.vertical)
				{
					if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.Up) && this.MoveSelection(-1))
					{
						NKCInputManager.ConsumeHotKeyEvent(HotkeyEventType.Up);
					}
					if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.Down) && this.MoveSelection(1))
					{
						NKCInputManager.ConsumeHotKeyEvent(HotkeyEventType.Down);
					}
				}
				if (this.m_scrollRect.horizontal)
				{
					if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.Left) && this.MoveSelection(-1))
					{
						NKCInputManager.ConsumeHotKeyEvent(HotkeyEventType.Left);
					}
					if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.Right) && this.MoveSelection(1))
					{
						NKCInputManager.ConsumeHotKeyEvent(HotkeyEventType.Right);
					}
				}
				if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
				{
					NKCUIComHotkeyDisplay.OpenInstance(this.m_scrollRect, base.transform);
				}
			}
		}

		// Token: 0x06009343 RID: 37699 RVA: 0x003243C0 File Offset: 0x003225C0
		private bool MoveSelection(int delta)
		{
			int num = this.m_lstSelectableElements.FindIndex((NKCUIComFoldableList.Element x) => x.MajorKey == this.m_SelectedMajorIndex && x.MinorKey == this.m_SelectedMinorIndex) + delta;
			if (num < 0)
			{
				return false;
			}
			if (num >= this.m_lstSelectableElements.Count)
			{
				return false;
			}
			NKCUIComFoldableList.Element element = this.m_lstSelectableElements[num];
			this.OnSelectSlot(element.MajorKey, element.MinorKey, true, element.isMajor);
			return true;
		}

		// Token: 0x04008027 RID: 32807
		[Header("스크롤")]
		public ScrollRect m_scrollRect;

		// Token: 0x04008028 RID: 32808
		[Header("모드")]
		public NKCUIComFoldableList.Mode m_eMode;

		// Token: 0x04008029 RID: 32809
		[Header("대분류의 선택이 가능한지? false라면 대분류 선택시 첫 소분류가 자동 선택된다.")]
		public bool m_bCanSelectMajor;

		// Token: 0x0400802A RID: 32810
		[Header("대분류")]
		public NKCUIComFoldableListSlot m_pfbMajor;

		// Token: 0x0400802B RID: 32811
		public NKCUIComToggleGroup m_MajorToggleGroup;

		// Token: 0x0400802C RID: 32812
		[Header("소분류")]
		public NKCUIComFoldableListSlot m_pfbMinor;

		// Token: 0x0400802D RID: 32813
		public NKCUIComToggleGroup m_MinorToggleGroup;

		// Token: 0x0400802E RID: 32814
		private Dictionary<int, NKCUIComFoldableListSlot> m_dicMajorSlot = new Dictionary<int, NKCUIComFoldableListSlot>();

		// Token: 0x0400802F RID: 32815
		private Dictionary<int, NKCUIComFoldableListSlot> m_dicFirstMinorSlot = new Dictionary<int, NKCUIComFoldableListSlot>();

		// Token: 0x04008030 RID: 32816
		private Dictionary<Tuple<int, int>, NKCUIComFoldableListSlot> m_dicMinorSlot = new Dictionary<Tuple<int, int>, NKCUIComFoldableListSlot>();

		// Token: 0x04008031 RID: 32817
		private NKCUIComFoldableList.OnSelectList dOnSelectList;

		// Token: 0x04008032 RID: 32818
		private NKCUIBase m_uiBase;

		// Token: 0x04008033 RID: 32819
		private int m_SelectedMajorIndex = -1;

		// Token: 0x04008034 RID: 32820
		private int m_SelectedMinorIndex = -1;

		// Token: 0x04008035 RID: 32821
		private List<NKCUIComFoldableList.Element> m_lstSelectableElements = new List<NKCUIComFoldableList.Element>();

		// Token: 0x02001A17 RID: 6679
		public struct Element : IComparable<NKCUIComFoldableList.Element>
		{
			// Token: 0x170019F3 RID: 6643
			// (get) Token: 0x0600BB0A RID: 47882 RVA: 0x0036E687 File Offset: 0x0036C887
			public Tuple<int, int> KeyPair
			{
				get
				{
					return new Tuple<int, int>(this.MajorKey, this.MinorKey);
				}
			}

			// Token: 0x170019F4 RID: 6644
			// (get) Token: 0x0600BB0B RID: 47883 RVA: 0x0036E69A File Offset: 0x0036C89A
			public bool isMinor
			{
				get
				{
					return !this.isMajor;
				}
			}

			// Token: 0x0600BB0C RID: 47884 RVA: 0x0036E6A8 File Offset: 0x0036C8A8
			public int CompareTo(NKCUIComFoldableList.Element other)
			{
				if (this.MajorKey != other.MajorKey)
				{
					return this.MajorKey.CompareTo(other.MajorKey);
				}
				if (this.isMajor != other.isMajor)
				{
					return other.isMajor.CompareTo(this.isMajor);
				}
				if (this.MinorSortKey != other.MinorSortKey)
				{
					return this.MinorSortKey.CompareTo(other.MinorSortKey);
				}
				return this.MinorKey.CompareTo(other.MinorKey);
			}

			// Token: 0x0400ADB0 RID: 44464
			public int MajorKey;

			// Token: 0x0400ADB1 RID: 44465
			public int MinorKey;

			// Token: 0x0400ADB2 RID: 44466
			public int MinorSortKey;

			// Token: 0x0400ADB3 RID: 44467
			public bool isMajor;
		}

		// Token: 0x02001A18 RID: 6680
		public enum Mode
		{
			// Token: 0x0400ADB5 RID: 44469
			SingleOpenOnly,
			// Token: 0x0400ADB6 RID: 44470
			AllOpened,
			// Token: 0x0400ADB7 RID: 44471
			MultipleOpenClose
		}

		// Token: 0x02001A19 RID: 6681
		// (Invoke) Token: 0x0600BB0E RID: 47886
		public delegate void OnSelectList(int major, int minor);
	}
}

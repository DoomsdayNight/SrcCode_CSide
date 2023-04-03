using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002C8 RID: 712
	[AddComponentMenu("UI/Extensions/Segmented Control/Segmented Control")]
	[RequireComponent(typeof(RectTransform))]
	public class SegmentedControl : UIBehaviour
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0002FC20 File Offset: 0x0002DE20
		protected float SeparatorWidth
		{
			get
			{
				if (this.m_separatorWidth == 0f && this.separator)
				{
					this.m_separatorWidth = this.separator.rectTransform.rect.width;
					Image component = this.separator.GetComponent<Image>();
					if (component)
					{
						this.m_separatorWidth /= component.pixelsPerUnit;
					}
				}
				return this.m_separatorWidth;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0002FC92 File Offset: 0x0002DE92
		public Selectable[] segments
		{
			get
			{
				if (this.m_segments == null || this.m_segments.Length == 0)
				{
					this.m_segments = this.GetChildSegments();
				}
				return this.m_segments;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0002FCB7 File Offset: 0x0002DEB7
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x0002FCBF File Offset: 0x0002DEBF
		public Graphic separator
		{
			get
			{
				return this.m_separator;
			}
			set
			{
				this.m_separator = value;
				this.m_separatorWidth = 0f;
				this.LayoutSegments();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0002FCD9 File Offset: 0x0002DED9
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x0002FCE1 File Offset: 0x0002DEE1
		public bool allowSwitchingOff
		{
			get
			{
				return this.m_allowSwitchingOff;
			}
			set
			{
				this.m_allowSwitchingOff = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0002FCEA File Offset: 0x0002DEEA
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0002FD00 File Offset: 0x0002DF00
		public int selectedSegmentIndex
		{
			get
			{
				return Array.IndexOf<Selectable>(this.segments, this.selectedSegment);
			}
			set
			{
				value = Math.Max(value, -1);
				value = Math.Min(value, this.segments.Length - 1);
				if (this.m_selectedSegmentIndex == value)
				{
					return;
				}
				this.m_selectedSegmentIndex = value;
				if (this.selectedSegment)
				{
					Segment component = this.selectedSegment.GetComponent<Segment>();
					if (component)
					{
						component.selected = false;
					}
					this.selectedSegment = null;
				}
				if (value != -1)
				{
					this.selectedSegment = this.segments[value];
					Segment component2 = this.selectedSegment.GetComponent<Segment>();
					if (component2)
					{
						component2.selected = true;
					}
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0002FD95 File Offset: 0x0002DF95
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0002FD9D File Offset: 0x0002DF9D
		public SegmentedControl.SegmentSelectedEvent onValueChanged
		{
			get
			{
				return this.m_onValueChanged;
			}
			set
			{
				this.m_onValueChanged = value;
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0002FDA6 File Offset: 0x0002DFA6
		protected SegmentedControl()
		{
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0002FDC0 File Offset: 0x0002DFC0
		protected override void Start()
		{
			base.Start();
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.DelayedInit());
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0002FDDD File Offset: 0x0002DFDD
		protected override void OnEnable()
		{
			base.StartCoroutine(this.DelayedInit());
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0002FDEC File Offset: 0x0002DFEC
		private IEnumerator DelayedInit()
		{
			yield return null;
			this.LayoutSegments();
			if (this.m_selectedSegmentIndex != -1)
			{
				this.selectedSegmentIndex = this.m_selectedSegmentIndex;
			}
			yield break;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0002FDFC File Offset: 0x0002DFFC
		private Selectable[] GetChildSegments()
		{
			Selectable[] componentsInChildren = base.GetComponentsInChildren<Selectable>();
			if (componentsInChildren.Length < 2)
			{
				throw new InvalidOperationException("A segmented control must have at least two Button children");
			}
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Segment component = componentsInChildren[i].GetComponent<Segment>();
				if (component != null)
				{
					component.index = i;
					component.segmentedControl = this;
				}
			}
			return componentsInChildren;
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002FE50 File Offset: 0x0002E050
		private void RecreateSprites()
		{
			for (int i = 0; i < this.segments.Length; i++)
			{
				if (!(this.segments[i].image == null))
				{
					Sprite sprite = SegmentedControl.CutSprite(this.segments[i].image.sprite, i == 0, i == this.segments.Length - 1);
					Segment component = this.segments[i].GetComponent<Segment>();
					if (component)
					{
						component.cutSprite = sprite;
					}
					this.segments[i].image.overrideSprite = sprite;
				}
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002FEE0 File Offset: 0x0002E0E0
		internal static Sprite CutSprite(Sprite sprite, bool leftmost, bool rightmost)
		{
			if (sprite.border.x == 0f || sprite.border.z == 0f)
			{
				return sprite;
			}
			Rect rect = sprite.rect;
			Vector4 border = sprite.border;
			if (!leftmost)
			{
				rect.xMin = border.x;
				border.x = 0f;
			}
			if (!rightmost)
			{
				rect.xMax = border.z;
				border.z = 0f;
			}
			return Sprite.Create(sprite.texture, rect, sprite.pivot, sprite.pixelsPerUnit, 0U, SpriteMeshType.FullRect, border);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002FF74 File Offset: 0x0002E174
		public void LayoutSegments()
		{
			this.RecreateSprites();
			RectTransform rectTransform = base.transform as RectTransform;
			float num = rectTransform.rect.width / (float)this.segments.Length - this.SeparatorWidth * (float)(this.segments.Length - 1);
			for (int i = 0; i < this.segments.Length; i++)
			{
				float num2 = (num + this.SeparatorWidth) * (float)i;
				RectTransform component = this.segments[i].GetComponent<RectTransform>();
				component.anchorMin = Vector2.zero;
				component.anchorMax = Vector2.zero;
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, num2, num);
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
				if (this.separator && i > 0)
				{
					Transform transform = base.gameObject.transform.Find("Separator " + i.ToString());
					Graphic graphic = (transform != null) ? transform.GetComponent<Graphic>() : Object.Instantiate<GameObject>(this.separator.gameObject).GetComponent<Graphic>();
					graphic.gameObject.name = "Separator " + i.ToString();
					graphic.gameObject.SetActive(true);
					graphic.rectTransform.SetParent(base.transform, false);
					graphic.rectTransform.anchorMin = Vector2.zero;
					graphic.rectTransform.anchorMax = Vector2.zero;
					graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, num2 - this.SeparatorWidth, this.SeparatorWidth);
					graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
				}
			}
		}

		// Token: 0x04000AAA RID: 2730
		private Selectable[] m_segments;

		// Token: 0x04000AAB RID: 2731
		[SerializeField]
		[Tooltip("A GameObject with an Image to use as a separator between segments. Size of the RectTransform will determine the size of the separator used.\nNote, make sure to disable the separator GO so that it does not affect the scene")]
		private Graphic m_separator;

		// Token: 0x04000AAC RID: 2732
		private float m_separatorWidth;

		// Token: 0x04000AAD RID: 2733
		[SerializeField]
		[Tooltip("When True, it allows each button to be toggled on/off")]
		private bool m_allowSwitchingOff;

		// Token: 0x04000AAE RID: 2734
		[SerializeField]
		[Tooltip("The selected default for the control (zero indexed array)")]
		private int m_selectedSegmentIndex = -1;

		// Token: 0x04000AAF RID: 2735
		[SerializeField]
		[Tooltip("Event to fire once the selection has been changed")]
		private SegmentedControl.SegmentSelectedEvent m_onValueChanged = new SegmentedControl.SegmentSelectedEvent();

		// Token: 0x04000AB0 RID: 2736
		internal Selectable selectedSegment;

		// Token: 0x02001137 RID: 4407
		[Serializable]
		public class SegmentSelectedEvent : UnityEvent<int>
		{
		}
	}
}

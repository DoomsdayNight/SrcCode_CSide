using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002CC RID: 716
	[AddComponentMenu("UI/Extensions/Stepper")]
	[RequireComponent(typeof(RectTransform))]
	public class Stepper : UIBehaviour
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x00030930 File Offset: 0x0002EB30
		private float separatorWidth
		{
			get
			{
				if (this._separatorWidth == 0f && this.separator)
				{
					this._separatorWidth = this.separator.rectTransform.rect.width;
					Image component = this.separator.GetComponent<Image>();
					if (component)
					{
						this._separatorWidth /= component.pixelsPerUnit;
					}
				}
				return this._separatorWidth;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000309A2 File Offset: 0x0002EBA2
		public Selectable[] sides
		{
			get
			{
				if (this._sides == null || this._sides.Length == 0)
				{
					this._sides = this.GetSides();
				}
				return this._sides;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000309C7 File Offset: 0x0002EBC7
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x000309CF File Offset: 0x0002EBCF
		public int value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x000309D8 File Offset: 0x0002EBD8
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x000309E0 File Offset: 0x0002EBE0
		public int minimum
		{
			get
			{
				return this._minimum;
			}
			set
			{
				this._minimum = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000309E9 File Offset: 0x0002EBE9
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x000309F1 File Offset: 0x0002EBF1
		public int maximum
		{
			get
			{
				return this._maximum;
			}
			set
			{
				this._maximum = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x000309FA File Offset: 0x0002EBFA
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x00030A02 File Offset: 0x0002EC02
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00030A0B File Offset: 0x0002EC0B
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x00030A13 File Offset: 0x0002EC13
		public bool wrap
		{
			get
			{
				return this._wrap;
			}
			set
			{
				this._wrap = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00030A1C File Offset: 0x0002EC1C
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x00030A24 File Offset: 0x0002EC24
		public Graphic separator
		{
			get
			{
				return this._separator;
			}
			set
			{
				this._separator = value;
				this._separatorWidth = 0f;
				this.LayoutSides(this.sides);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00030A44 File Offset: 0x0002EC44
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public Stepper.StepperValueChangedEvent onValueChanged
		{
			get
			{
				return this._onValueChanged;
			}
			set
			{
				this._onValueChanged = value;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00030A55 File Offset: 0x0002EC55
		protected Stepper()
		{
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00030A77 File Offset: 0x0002EC77
		protected override void Start()
		{
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.DelayedInit());
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00030A8E File Offset: 0x0002EC8E
		protected override void OnEnable()
		{
			base.StartCoroutine(this.DelayedInit());
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00030A9D File Offset: 0x0002EC9D
		private IEnumerator DelayedInit()
		{
			yield return null;
			this.RecreateSprites(this.sides);
			yield break;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00030AAC File Offset: 0x0002ECAC
		private Selectable[] GetSides()
		{
			Selectable[] componentsInChildren = base.GetComponentsInChildren<Selectable>();
			if (componentsInChildren.Length != 2)
			{
				throw new InvalidOperationException("A stepper must have two Button children");
			}
			if (!this.wrap)
			{
				this.DisableAtExtremes(componentsInChildren);
			}
			this.LayoutSides(componentsInChildren);
			return componentsInChildren;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00030AE8 File Offset: 0x0002ECE8
		public void StepUp()
		{
			this.Step(this.step);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00030AF6 File Offset: 0x0002ECF6
		public void StepDown()
		{
			this.Step(-this.step);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00030B08 File Offset: 0x0002ED08
		private void Step(int amount)
		{
			this.value += amount;
			if (this.wrap)
			{
				if (this.value > this.maximum)
				{
					this.value = this.minimum;
				}
				if (this.value < this.minimum)
				{
					this.value = this.maximum;
				}
			}
			else
			{
				this.value = Math.Max(this.minimum, this.value);
				this.value = Math.Min(this.maximum, this.value);
				this.DisableAtExtremes(this.sides);
			}
			this._onValueChanged.Invoke(this.value);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00030BAC File Offset: 0x0002EDAC
		private void DisableAtExtremes(Selectable[] sides)
		{
			sides[0].interactable = (this.wrap || this.value > this.minimum);
			sides[1].interactable = (this.wrap || this.value < this.maximum);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00030BFC File Offset: 0x0002EDFC
		private void RecreateSprites(Selectable[] sides)
		{
			for (int i = 0; i < 2; i++)
			{
				if (!(sides[i].image == null))
				{
					Sprite sprite = Stepper.CutSprite(sides[i].image.sprite, i == 0);
					StepperSide component = sides[i].GetComponent<StepperSide>();
					if (component)
					{
						component.cutSprite = sprite;
					}
					sides[i].image.overrideSprite = sprite;
				}
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00030C64 File Offset: 0x0002EE64
		internal static Sprite CutSprite(Sprite sprite, bool leftmost)
		{
			if (sprite.border.x == 0f || sprite.border.z == 0f)
			{
				return sprite;
			}
			Rect rect = sprite.rect;
			Vector4 border = sprite.border;
			if (leftmost)
			{
				rect.xMax = border.z;
				border.z = 0f;
			}
			else
			{
				rect.xMin = border.x;
				border.x = 0f;
			}
			return Sprite.Create(sprite.texture, rect, sprite.pivot, sprite.pixelsPerUnit, 0U, SpriteMeshType.FullRect, border);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00030CF8 File Offset: 0x0002EEF8
		public void LayoutSides(Selectable[] sides = null)
		{
			sides = (sides ?? this.sides);
			this.RecreateSprites(sides);
			RectTransform rectTransform = base.transform as RectTransform;
			float num = rectTransform.rect.width / 2f - this.separatorWidth;
			for (int i = 0; i < 2; i++)
			{
				float inset = (i == 0) ? 0f : (num + this.separatorWidth);
				RectTransform component = sides[i].GetComponent<RectTransform>();
				component.anchorMin = Vector2.zero;
				component.anchorMax = Vector2.zero;
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, inset, num);
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
			}
			if (this.separator)
			{
				Transform transform = base.gameObject.transform.Find("Separator");
				Graphic graphic = (transform != null) ? transform.GetComponent<Graphic>() : Object.Instantiate<GameObject>(this.separator.gameObject).GetComponent<Graphic>();
				graphic.gameObject.name = "Separator";
				graphic.gameObject.SetActive(true);
				graphic.rectTransform.SetParent(base.transform, false);
				graphic.rectTransform.anchorMin = Vector2.zero;
				graphic.rectTransform.anchorMax = Vector2.zero;
				graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, num, this.separatorWidth);
				graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
			}
		}

		// Token: 0x04000AC0 RID: 2752
		private Selectable[] _sides;

		// Token: 0x04000AC1 RID: 2753
		[SerializeField]
		[Tooltip("The current step value of the control")]
		private int _value;

		// Token: 0x04000AC2 RID: 2754
		[SerializeField]
		[Tooltip("The minimum step value allowed by the control. When reached it will disable the '-' button")]
		private int _minimum;

		// Token: 0x04000AC3 RID: 2755
		[SerializeField]
		[Tooltip("The maximum step value allowed by the control. When reached it will disable the '+' button")]
		private int _maximum = 100;

		// Token: 0x04000AC4 RID: 2756
		[SerializeField]
		[Tooltip("The step increment used to increment / decrement the step value")]
		private int _step = 1;

		// Token: 0x04000AC5 RID: 2757
		[SerializeField]
		[Tooltip("Does the step value loop around from end to end")]
		private bool _wrap;

		// Token: 0x04000AC6 RID: 2758
		[SerializeField]
		[Tooltip("A GameObject with an Image to use as a separator between segments. Size of the RectTransform will determine the size of the separator used.\nNote, make sure to disable the separator GO so that it does not affect the scene")]
		private Graphic _separator;

		// Token: 0x04000AC7 RID: 2759
		private float _separatorWidth;

		// Token: 0x04000AC8 RID: 2760
		[SerializeField]
		private Stepper.StepperValueChangedEvent _onValueChanged = new Stepper.StepperValueChangedEvent();

		// Token: 0x0200113A RID: 4410
		[Serializable]
		public class StepperValueChangedEvent : UnityEvent<int>
		{
		}
	}
}

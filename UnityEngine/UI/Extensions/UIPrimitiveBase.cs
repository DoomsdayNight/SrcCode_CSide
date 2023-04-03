using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000326 RID: 806
	[RequireComponent(typeof(CanvasRenderer))]
	public class UIPrimitiveBase : MaskableGraphic, ILayoutElement, ICanvasRaycastFilter
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0004518E File Offset: 0x0004338E
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x00045196 File Offset: 0x00043396
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_Sprite, value))
				{
					this.GeneratedUVs();
				}
				this.SetAllDirty();
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x000451B2 File Offset: 0x000433B2
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x000451BA File Offset: 0x000433BA
		public Sprite overrideSprite
		{
			get
			{
				return this.activeSprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
				{
					this.GeneratedUVs();
				}
				this.SetAllDirty();
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x000451D6 File Offset: 0x000433D6
		protected Sprite activeSprite
		{
			get
			{
				if (!(this.m_OverrideSprite != null))
				{
					return this.sprite;
				}
				return this.m_OverrideSprite;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x000451F3 File Offset: 0x000433F3
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x000451FB File Offset: 0x000433FB
		public float eventAlphaThreshold
		{
			get
			{
				return this.m_EventAlphaThreshold;
			}
			set
			{
				this.m_EventAlphaThreshold = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00045204 File Offset: 0x00043404
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x0004520C File Offset: 0x0004340C
		public ResolutionMode ImproveResolution
		{
			get
			{
				return this.m_improveResolution;
			}
			set
			{
				this.m_improveResolution = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0004521B File Offset: 0x0004341B
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x00045223 File Offset: 0x00043423
		public float Resolution
		{
			get
			{
				return this.m_Resolution;
			}
			set
			{
				this.m_Resolution = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00045232 File Offset: 0x00043432
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x0004523A File Offset: 0x0004343A
		public bool UseNativeSize
		{
			get
			{
				return this.m_useNativeSize;
			}
			set
			{
				this.m_useNativeSize = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00045249 File Offset: 0x00043449
		protected UIPrimitiveBase()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0004526E File Offset: 0x0004346E
		public static Material defaultETC1GraphicMaterial
		{
			get
			{
				if (UIPrimitiveBase.s_ETC1DefaultUI == null)
				{
					UIPrimitiveBase.s_ETC1DefaultUI = Canvas.GetETC1SupportedCanvasMaterial();
				}
				return UIPrimitiveBase.s_ETC1DefaultUI;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0004528C File Offset: 0x0004348C
		public override Texture mainTexture
		{
			get
			{
				if (!(this.activeSprite == null))
				{
					return this.activeSprite.texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x000452E8 File Offset: 0x000434E8
		public bool hasBorder
		{
			get
			{
				return this.activeSprite != null && this.activeSprite.border.sqrMagnitude > 0f;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00045320 File Offset: 0x00043520
		public float pixelsPerUnit
		{
			get
			{
				float num = 100f;
				if (this.activeSprite)
				{
					num = this.activeSprite.pixelsPerUnit;
				}
				float num2 = 100f;
				if (base.canvas)
				{
					num2 = base.canvas.referencePixelsPerUnit;
				}
				return num / num2;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00045370 File Offset: 0x00043570
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x000453BE File Offset: 0x000435BE
		public override Material material
		{
			get
			{
				if (this.m_Material != null)
				{
					return this.m_Material;
				}
				if (this.activeSprite && this.activeSprite.associatedAlphaSplitTexture != null)
				{
					return UIPrimitiveBase.defaultETC1GraphicMaterial;
				}
				return this.defaultMaterial;
			}
			set
			{
				base.material = value;
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000453C8 File Offset: 0x000435C8
		protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
		{
			UIVertex[] array = new UIVertex[4];
			for (int i = 0; i < vertices.Length; i++)
			{
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.color = this.color;
				simpleVert.position = vertices[i];
				simpleVert.uv0 = uvs[i];
				array[i] = simpleVert;
			}
			return array;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00045431 File Offset: 0x00043631
		protected Vector2[] IncreaseResolution(Vector2[] input)
		{
			return this.IncreaseResolution(new List<Vector2>(input)).ToArray();
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00045444 File Offset: 0x00043644
		protected List<Vector2> IncreaseResolution(List<Vector2> input)
		{
			this.outputList.Clear();
			ResolutionMode improveResolution = this.ImproveResolution;
			if (improveResolution != ResolutionMode.PerSegment)
			{
				if (improveResolution == ResolutionMode.PerLine)
				{
					float num = 0f;
					for (int i = 0; i < input.Count - 1; i++)
					{
						num += Vector2.Distance(input[i], input[i + 1]);
					}
					this.ResolutionToNativeSize(num);
					float num2 = num / this.m_Resolution;
					int num3 = 0;
					for (int j = 0; j < input.Count - 1; j++)
					{
						Vector2 vector = input[j];
						this.outputList.Add(vector);
						Vector2 vector2 = input[j + 1];
						float num4 = Vector2.Distance(vector, vector2) / num2;
						float num5 = 1f / num4;
						int num6 = 0;
						while ((float)num6 < num4)
						{
							this.outputList.Add(Vector2.Lerp(vector, vector2, (float)num6 * num5));
							num3++;
							num6++;
						}
						this.outputList.Add(vector2);
					}
				}
			}
			else
			{
				for (int k = 0; k < input.Count - 1; k++)
				{
					Vector2 vector3 = input[k];
					this.outputList.Add(vector3);
					Vector2 vector4 = input[k + 1];
					this.ResolutionToNativeSize(Vector2.Distance(vector3, vector4));
					float num2 = 1f / this.m_Resolution;
					for (float num7 = 1f; num7 < this.m_Resolution; num7 += 1f)
					{
						this.outputList.Add(Vector2.Lerp(vector3, vector4, num2 * num7));
					}
					this.outputList.Add(vector4);
				}
			}
			return this.outputList;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000455FA File Offset: 0x000437FA
		protected virtual void GeneratedUVs()
		{
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000455FC File Offset: 0x000437FC
		protected virtual void ResolutionToNativeSize(float distance)
		{
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000455FE File Offset: 0x000437FE
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00045600 File Offset: 0x00043800
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00045602 File Offset: 0x00043802
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0004560C File Offset: 0x0004380C
		public virtual float preferredWidth
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				return this.overrideSprite.rect.size.x / this.pixelsPerUnit;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0004564C File Offset: 0x0004384C
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00045653 File Offset: 0x00043853
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0004565C File Offset: 0x0004385C
		public virtual float preferredHeight
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				return this.overrideSprite.rect.size.y / this.pixelsPerUnit;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0004569C File Offset: 0x0004389C
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000456A3 File Offset: 0x000438A3
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000456A8 File Offset: 0x000438A8
		public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (this.m_EventAlphaThreshold >= 1f)
			{
				return true;
			}
			Sprite overrideSprite = this.overrideSprite;
			if (overrideSprite == null)
			{
				return true;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, screenPoint, eventCamera, out vector);
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			vector.x += base.rectTransform.pivot.x * pixelAdjustedRect.width;
			vector.y += base.rectTransform.pivot.y * pixelAdjustedRect.height;
			vector = this.MapCoordinate(vector, pixelAdjustedRect);
			Rect textureRect = overrideSprite.textureRect;
			Vector2 vector2 = new Vector2(vector.x / textureRect.width, vector.y / textureRect.height);
			float u = Mathf.Lerp(textureRect.x, textureRect.xMax, vector2.x) / (float)overrideSprite.texture.width;
			float v = Mathf.Lerp(textureRect.y, textureRect.yMax, vector2.y) / (float)overrideSprite.texture.height;
			bool result;
			try
			{
				result = (overrideSprite.texture.GetPixelBilinear(u, v).a >= this.m_EventAlphaThreshold);
			}
			catch (UnityException ex)
			{
				Debug.LogError("Using clickAlphaThreshold lower than 1 on Image whose sprite texture cannot be read. " + ex.Message + " Also make sure to disable sprite packing for this sprite.", this);
				result = true;
			}
			return result;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00045810 File Offset: 0x00043A10
		private Vector2 MapCoordinate(Vector2 local, Rect rect)
		{
			Rect rect2 = this.sprite.rect;
			return new Vector2(local.x * rect.width, local.y * rect.height);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00045840 File Offset: 0x00043A40
		private Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
		{
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (rect.size[i] < num && num != 0f)
				{
					float num2 = rect.size[i] / num;
					ref Vector4 ptr = ref border;
					int index = i;
					ptr[index] *= num2;
					ptr = ref border;
					index = i + 2;
					ptr[index] *= num2;
				}
			}
			return border;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000458D7 File Offset: 0x00043AD7
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetAllDirty();
		}

		// Token: 0x04000CFC RID: 3324
		protected static Material s_ETC1DefaultUI;

		// Token: 0x04000CFD RID: 3325
		private List<Vector2> outputList = new List<Vector2>();

		// Token: 0x04000CFE RID: 3326
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x04000CFF RID: 3327
		[NonSerialized]
		private Sprite m_OverrideSprite;

		// Token: 0x04000D00 RID: 3328
		internal float m_EventAlphaThreshold = 1f;

		// Token: 0x04000D01 RID: 3329
		[SerializeField]
		private ResolutionMode m_improveResolution;

		// Token: 0x04000D02 RID: 3330
		[SerializeField]
		protected float m_Resolution;

		// Token: 0x04000D03 RID: 3331
		[SerializeField]
		private bool m_useNativeSize;
	}
}

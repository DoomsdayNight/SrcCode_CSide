using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002CE RID: 718
	[AddComponentMenu("UI/Extensions/TextPic")]
	[ExecuteInEditMode]
	public class TextPic : Text, IPointerClickHandler, IEventSystemHandler, IPointerExitHandler, IPointerEnterHandler, ISelectHandler
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00030F98 File Offset: 0x0002F198
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x00030FA0 File Offset: 0x0002F1A0
		public TextPic.HrefClickEvent onHrefClick
		{
			get
			{
				return this.m_OnHrefClick;
			}
			set
			{
				this.m_OnHrefClick = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00030FA9 File Offset: 0x0002F1A9
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00030FB1 File Offset: 0x0002F1B1
		public bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00030FBA File Offset: 0x0002F1BA
		public void ResetIconList()
		{
			this.Reset_m_HrefInfos();
			base.Start();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00030FC8 File Offset: 0x0002F1C8
		protected void UpdateQuadImage()
		{
			this.m_OutputText = this.GetOutputText();
			this.matches = TextPic.s_Regex.Matches(this.m_OutputText);
			if (this.matches != null && this.matches.Count > 0)
			{
				for (int i = 0; i < this.matches.Count; i++)
				{
					this.m_ImagesPool.RemoveAll((Image image) => image == null);
					if (this.m_ImagesPool.Count == 0)
					{
						base.GetComponentsInChildren<Image>(true, this.m_ImagesPool);
					}
					if (this.matches.Count > this.m_ImagesPool.Count)
					{
						GameObject gameObject = DefaultControls.CreateImage(default(DefaultControls.Resources));
						gameObject.layer = base.gameObject.layer;
						RectTransform rectTransform = gameObject.transform as RectTransform;
						if (rectTransform)
						{
							rectTransform.SetParent(base.rectTransform);
							rectTransform.anchoredPosition3D = Vector3.zero;
							rectTransform.localRotation = Quaternion.identity;
							rectTransform.localScale = Vector3.one;
						}
						this.m_ImagesPool.Add(gameObject.GetComponent<Image>());
					}
					string value = this.matches[i].Groups[1].Value;
					Image image2 = this.m_ImagesPool[i];
					Vector2 b = Vector2.zero;
					if ((image2.sprite == null || image2.sprite.name != value) && this.inspectorIconList != null && this.inspectorIconList.Length != 0)
					{
						for (int j = 0; j < this.inspectorIconList.Length; j++)
						{
							if (this.inspectorIconList[j].name == value)
							{
								image2.sprite = this.inspectorIconList[j].sprite;
								image2.preserveAspect = true;
								image2.rectTransform.sizeDelta = new Vector2((float)base.fontSize * this.ImageScalingFactor * this.inspectorIconList[j].scale.x, (float)base.fontSize * this.ImageScalingFactor * this.inspectorIconList[j].scale.y);
								b = this.inspectorIconList[j].offset;
								break;
							}
						}
					}
					image2.enabled = true;
					if (this.positions.Count > 0 && i < this.positions.Count)
					{
						RectTransform rectTransform2 = image2.rectTransform;
						List<Vector2> list = this.positions;
						int index = i;
						rectTransform2.anchoredPosition = (list[index] += b);
					}
				}
			}
			else
			{
				for (int k = this.m_ImagesPool.Count - 1; k > 0; k--)
				{
					if (this.m_ImagesPool[k] && !this.culled_ImagesPool.Contains(this.m_ImagesPool[k].gameObject))
					{
						this.culled_ImagesPool.Add(this.m_ImagesPool[k].gameObject);
						this.m_ImagesPool.Remove(this.m_ImagesPool[k]);
					}
				}
			}
			for (int l = this.m_ImagesPool.Count - 1; l >= this.matches.Count; l--)
			{
				if (l >= 0 && this.m_ImagesPool.Count > 0 && this.m_ImagesPool[l] && !this.culled_ImagesPool.Contains(this.m_ImagesPool[l].gameObject))
				{
					this.culled_ImagesPool.Add(this.m_ImagesPool[l].gameObject);
					this.m_ImagesPool.Remove(this.m_ImagesPool[l]);
				}
			}
			if (this.culled_ImagesPool.Count > 0)
			{
				this.clearImages = true;
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000313DB File Offset: 0x0002F5DB
		private void Reset_m_HrefInfos()
		{
			this.previousText = this.text;
			this.m_HrefInfos.Clear();
			this.isCreating_m_HrefInfos = true;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000313FC File Offset: 0x0002F5FC
		protected string GetOutputText()
		{
			TextPic.s_TextBuilder.Length = 0;
			this.indexText = 0;
			this.fixedString = this.text;
			if (this.inspectorIconList != null && this.inspectorIconList.Length != 0)
			{
				for (int i = 0; i < this.inspectorIconList.Length; i++)
				{
					if (!string.IsNullOrEmpty(this.inspectorIconList[i].name))
					{
						this.fixedString = this.fixedString.Replace(this.inspectorIconList[i].name, string.Concat(new string[]
						{
							"<quad name=",
							this.inspectorIconList[i].name,
							" size=",
							base.fontSize.ToString(),
							" width=1 />"
						}));
					}
				}
			}
			this.count = 0;
			this.href_matches = TextPic.s_HrefRegex.Matches(this.fixedString);
			if (this.href_matches != null && this.href_matches.Count > 0)
			{
				for (int j = 0; j < this.href_matches.Count; j++)
				{
					TextPic.s_TextBuilder.Append(this.fixedString.Substring(this.indexText, this.href_matches[j].Index - this.indexText));
					TextPic.s_TextBuilder.Append("<color=" + this.hyperlinkColor + ">");
					Group group = this.href_matches[j].Groups[1];
					if (this.isCreating_m_HrefInfos)
					{
						TextPic.HrefInfo item = new TextPic.HrefInfo
						{
							startIndex = (this.usesNewRendering ? TextPic.s_TextBuilder.Length : (TextPic.s_TextBuilder.Length * 4)),
							endIndex = (this.usesNewRendering ? (TextPic.s_TextBuilder.Length + this.href_matches[j].Groups[2].Length - 1) : ((TextPic.s_TextBuilder.Length + this.href_matches[j].Groups[2].Length - 1) * 4 + 3)),
							name = group.Value
						};
						this.m_HrefInfos.Add(item);
					}
					else if (this.count <= this.m_HrefInfos.Count - 1)
					{
						this.m_HrefInfos[this.count].startIndex = (this.usesNewRendering ? TextPic.s_TextBuilder.Length : (TextPic.s_TextBuilder.Length * 4));
						this.m_HrefInfos[this.count].endIndex = (this.usesNewRendering ? (TextPic.s_TextBuilder.Length + this.href_matches[j].Groups[2].Length - 1) : ((TextPic.s_TextBuilder.Length + this.href_matches[j].Groups[2].Length - 1) * 4 + 3));
						this.count++;
					}
					TextPic.s_TextBuilder.Append(this.href_matches[j].Groups[2].Value);
					TextPic.s_TextBuilder.Append("</color>");
					this.indexText = this.href_matches[j].Index + this.href_matches[j].Length;
				}
			}
			if (this.isCreating_m_HrefInfos)
			{
				this.isCreating_m_HrefInfos = false;
			}
			TextPic.s_TextBuilder.Append(this.fixedString.Substring(this.indexText, this.fixedString.Length - this.indexText));
			this.m_OutputText = TextPic.s_TextBuilder.ToString();
			this.m_ImagesVertexIndex.Clear();
			this.matches = TextPic.s_Regex.Matches(this.m_OutputText);
			this.href_matches = TextPic.s_HrefRegex.Matches(this.m_OutputText);
			this.indexes.Clear();
			for (int k = 0; k < this.matches.Count; k++)
			{
				this.indexes.Add(this.matches[k].Index);
			}
			if (this.matches != null && this.matches.Count > 0)
			{
				for (int l = 0; l < this.matches.Count; l++)
				{
					this.picIndex = this.matches[l].Index;
					if (this.usesNewRendering)
					{
						this.charactersRemoved = 0;
						this.removeCharacters = TextPic.remove_Regex.Matches(this.m_OutputText);
						for (int m = 0; m < this.removeCharacters.Count; m++)
						{
							if (this.removeCharacters[m].Index < this.picIndex && !this.indexes.Contains(this.removeCharacters[m].Index))
							{
								this.charactersRemoved += this.removeCharacters[m].Length;
							}
						}
						for (int n = 0; n < l; n++)
						{
							this.charactersRemoved += this.matches[n].Length - 1;
						}
						this.picIndex -= this.charactersRemoved;
					}
					this.vertIndex = this.picIndex * 4 + 3;
					this.m_ImagesVertexIndex.Add(this.vertIndex);
				}
			}
			if (this.usesNewRendering && this.m_HrefInfos != null && this.m_HrefInfos.Count > 0)
			{
				for (int num = 0; num < this.m_HrefInfos.Count; num++)
				{
					this.startCharactersRemoved = 0;
					this.endCharactersRemoved = 0;
					this.removeCharacters = TextPic.remove_Regex.Matches(this.m_OutputText);
					for (int num2 = 0; num2 < this.removeCharacters.Count; num2++)
					{
						if (this.removeCharacters[num2].Index < this.m_HrefInfos[num].startIndex && !this.indexes.Contains(this.removeCharacters[num2].Index))
						{
							this.startCharactersRemoved += this.removeCharacters[num2].Length;
						}
						else if (this.removeCharacters[num2].Index < this.m_HrefInfos[num].startIndex && this.indexes.Contains(this.removeCharacters[num2].Index))
						{
							this.startCharactersRemoved += this.removeCharacters[num2].Length - 1;
						}
						if (this.removeCharacters[num2].Index < this.m_HrefInfos[num].endIndex && !this.indexes.Contains(this.removeCharacters[num2].Index))
						{
							this.endCharactersRemoved += this.removeCharacters[num2].Length;
						}
						else if (this.removeCharacters[num2].Index < this.m_HrefInfos[num].endIndex && this.indexes.Contains(this.removeCharacters[num2].Index))
						{
							this.endCharactersRemoved += this.removeCharacters[num2].Length - 1;
						}
					}
					this.m_HrefInfos[num].startIndex -= this.startCharactersRemoved;
					this.m_HrefInfos[num].startIndex = this.m_HrefInfos[num].startIndex * 4;
					this.m_HrefInfos[num].endIndex -= this.endCharactersRemoved;
					this.m_HrefInfos[num].endIndex = this.m_HrefInfos[num].endIndex * 4 + 3;
				}
			}
			return this.m_OutputText;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00031C57 File Offset: 0x0002FE57
		public virtual void OnHrefClick(string hrefName)
		{
			Application.OpenURL(hrefName);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00031C60 File Offset: 0x0002FE60
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			this.originalText = this.m_Text;
			this.m_Text = this.GetOutputText();
			base.OnPopulateMesh(toFill);
			this.m_DisableFontTextureRebuiltCallback = true;
			this.m_Text = this.originalText;
			this.positions.Clear();
			this.vert = default(UIVertex);
			for (int i = 0; i < this.m_ImagesVertexIndex.Count; i++)
			{
				int num = this.m_ImagesVertexIndex[i];
				if (num < toFill.currentVertCount)
				{
					toFill.PopulateUIVertex(ref this.vert, num);
					this.positions.Add(new Vector2(this.vert.position.x + (float)(base.fontSize / 2), this.vert.position.y + (float)(base.fontSize / 2)) + this.imageOffset);
					toFill.PopulateUIVertex(ref this.vert, num - 3);
					Vector3 position = this.vert.position;
					int j = num;
					int num2 = num - 3;
					while (j > num2)
					{
						toFill.PopulateUIVertex(ref this.vert, num);
						this.vert.position = position;
						toFill.SetUIVertex(this.vert, j);
						j--;
					}
				}
			}
			for (int k = 0; k < this.m_HrefInfos.Count; k++)
			{
				this.m_HrefInfos[k].boxes.Clear();
				if (this.m_HrefInfos[k].startIndex < toFill.currentVertCount)
				{
					toFill.PopulateUIVertex(ref this.vert, this.m_HrefInfos[k].startIndex);
					Vector3 position2 = this.vert.position;
					Bounds bounds = new Bounds(position2, Vector3.zero);
					int num3 = this.m_HrefInfos[k].startIndex;
					int endIndex = this.m_HrefInfos[k].endIndex;
					while (num3 < endIndex && num3 < toFill.currentVertCount)
					{
						toFill.PopulateUIVertex(ref this.vert, num3);
						position2 = this.vert.position;
						if (position2.x < bounds.min.x)
						{
							this.m_HrefInfos[k].boxes.Add(new Rect(bounds.min, bounds.size));
							bounds = new Bounds(position2, Vector3.zero);
						}
						else
						{
							bounds.Encapsulate(position2);
						}
						num3++;
					}
					this.m_HrefInfos[k].boxes.Add(new Rect(bounds.min, bounds.size));
				}
			}
			this.updateQuad = true;
			this.m_DisableFontTextureRebuiltCallback = false;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00031F30 File Offset: 0x00030130
		public void OnPointerClick(PointerEventData eventData)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, eventData.position, eventData.pressEventCamera, out this.lp);
			for (int i = 0; i < this.m_HrefInfos.Count; i++)
			{
				for (int j = 0; j < this.m_HrefInfos[i].boxes.Count; j++)
				{
					if (this.m_HrefInfos[i].boxes[j].Contains(this.lp))
					{
						this.m_OnHrefClick.Invoke(this.m_HrefInfos[i].name);
						return;
					}
				}
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00031FD8 File Offset: 0x000301D8
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.selected = true;
			if (this.m_ImagesPool.Count >= 1)
			{
				for (int i = 0; i < this.m_ImagesPool.Count; i++)
				{
					if (this.button != null && this.button.isActiveAndEnabled)
					{
						this.m_ImagesPool[i].color = this.button.colors.highlightedColor;
					}
				}
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00032050 File Offset: 0x00030250
		public void OnPointerExit(PointerEventData eventData)
		{
			this.selected = false;
			if (this.m_ImagesPool.Count >= 1)
			{
				for (int i = 0; i < this.m_ImagesPool.Count; i++)
				{
					if (this.button != null && this.button.isActiveAndEnabled)
					{
						this.m_ImagesPool[i].color = this.button.colors.normalColor;
					}
					else
					{
						this.m_ImagesPool[i].color = this.color;
					}
				}
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000320E0 File Offset: 0x000302E0
		public void OnSelect(BaseEventData eventData)
		{
			this.selected = true;
			if (this.m_ImagesPool.Count >= 1)
			{
				for (int i = 0; i < this.m_ImagesPool.Count; i++)
				{
					if (this.button != null && this.button.isActiveAndEnabled)
					{
						this.m_ImagesPool[i].color = this.button.colors.highlightedColor;
					}
				}
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00032158 File Offset: 0x00030358
		public void OnDeselect(BaseEventData eventData)
		{
			this.selected = false;
			if (this.m_ImagesPool.Count >= 1)
			{
				for (int i = 0; i < this.m_ImagesPool.Count; i++)
				{
					if (this.button != null && this.button.isActiveAndEnabled)
					{
						this.m_ImagesPool[i].color = this.button.colors.normalColor;
					}
				}
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000321CF File Offset: 0x000303CF
		public override void SetVerticesDirty()
		{
			base.SetVerticesDirty();
			this.updateQuad = true;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000321E0 File Offset: 0x000303E0
		protected override void OnEnable()
		{
			this.usesNewRendering = false;
			if (Application.unityVersion.StartsWith("2019.1."))
			{
				if (!char.IsDigit(Application.unityVersion[8]))
				{
					if (Convert.ToInt32(Application.unityVersion[7].ToString()) > 4)
					{
						this.usesNewRendering = true;
					}
				}
				else
				{
					this.usesNewRendering = true;
				}
			}
			else
			{
				this.usesNewRendering = true;
			}
			base.OnEnable();
			base.supportRichText = true;
			base.alignByGeometry = true;
			if (this.m_ImagesPool.Count >= 1)
			{
				for (int i = 0; i < this.m_ImagesPool.Count; i++)
				{
					if (this.m_ImagesPool[i] != null)
					{
						this.m_ImagesPool[i].enabled = true;
					}
				}
			}
			this.updateQuad = true;
			this.onHrefClick.AddListener(new UnityAction<string>(this.OnHrefClick));
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000322C8 File Offset: 0x000304C8
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.m_ImagesPool.Count >= 1)
			{
				for (int i = 0; i < this.m_ImagesPool.Count; i++)
				{
					if (this.m_ImagesPool[i] != null)
					{
						this.m_ImagesPool[i].enabled = false;
					}
				}
			}
			this.onHrefClick.RemoveListener(new UnityAction<string>(this.OnHrefClick));
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003233D File Offset: 0x0003053D
		private new void Start()
		{
			this.button = base.GetComponent<Button>();
			this.ResetIconList();
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00032354 File Offset: 0x00030554
		private void LateUpdate()
		{
			if (this.previousText != this.text)
			{
				this.Reset_m_HrefInfos();
				this.updateQuad = true;
			}
			Object obj = this.thisLock;
			lock (obj)
			{
				if (this.updateQuad)
				{
					this.UpdateQuadImage();
					this.updateQuad = false;
				}
				if (this.clearImages)
				{
					for (int i = 0; i < this.culled_ImagesPool.Count; i++)
					{
						Object.DestroyImmediate(this.culled_ImagesPool[i]);
					}
					this.culled_ImagesPool.Clear();
					this.clearImages = false;
				}
			}
		}

		// Token: 0x04000ACA RID: 2762
		public TextPic.IconName[] inspectorIconList;

		// Token: 0x04000ACB RID: 2763
		[Tooltip("Global scaling factor for all images")]
		public float ImageScalingFactor = 1f;

		// Token: 0x04000ACC RID: 2764
		public string hyperlinkColor = "blue";

		// Token: 0x04000ACD RID: 2765
		public Vector2 imageOffset = Vector2.zero;

		// Token: 0x04000ACE RID: 2766
		public bool isCreating_m_HrefInfos = true;

		// Token: 0x04000ACF RID: 2767
		[SerializeField]
		private TextPic.HrefClickEvent m_OnHrefClick = new TextPic.HrefClickEvent();

		// Token: 0x04000AD0 RID: 2768
		private readonly List<Image> m_ImagesPool = new List<Image>();

		// Token: 0x04000AD1 RID: 2769
		private readonly List<GameObject> culled_ImagesPool = new List<GameObject>();

		// Token: 0x04000AD2 RID: 2770
		private bool clearImages;

		// Token: 0x04000AD3 RID: 2771
		private Object thisLock = new Object();

		// Token: 0x04000AD4 RID: 2772
		private readonly List<int> m_ImagesVertexIndex = new List<int>();

		// Token: 0x04000AD5 RID: 2773
		private static readonly Regex s_Regex = new Regex("<quad name=(.+?) size=(\\d*\\.?\\d+%?) width=(\\d*\\.?\\d+%?) />", RegexOptions.Singleline);

		// Token: 0x04000AD6 RID: 2774
		private static readonly Regex s_HrefRegex = new Regex("<a href=([^>\\n\\s]+)>(.*?)(</a>)", RegexOptions.Singleline);

		// Token: 0x04000AD7 RID: 2775
		private string fixedString;

		// Token: 0x04000AD8 RID: 2776
		private bool updateQuad;

		// Token: 0x04000AD9 RID: 2777
		private string m_OutputText;

		// Token: 0x04000ADA RID: 2778
		private Button button;

		// Token: 0x04000ADB RID: 2779
		private bool selected;

		// Token: 0x04000ADC RID: 2780
		private List<Vector2> positions = new List<Vector2>();

		// Token: 0x04000ADD RID: 2781
		private string previousText = "";

		// Token: 0x04000ADE RID: 2782
		private readonly List<TextPic.HrefInfo> m_HrefInfos = new List<TextPic.HrefInfo>();

		// Token: 0x04000ADF RID: 2783
		private static readonly StringBuilder s_TextBuilder = new StringBuilder();

		// Token: 0x04000AE0 RID: 2784
		private MatchCollection matches;

		// Token: 0x04000AE1 RID: 2785
		private MatchCollection href_matches;

		// Token: 0x04000AE2 RID: 2786
		private MatchCollection removeCharacters;

		// Token: 0x04000AE3 RID: 2787
		private int picIndex;

		// Token: 0x04000AE4 RID: 2788
		private int vertIndex;

		// Token: 0x04000AE5 RID: 2789
		private bool usesNewRendering;

		// Token: 0x04000AE6 RID: 2790
		private static readonly Regex remove_Regex = new Regex("<b>|</b>|<i>|</i>|<size=.*?>|</size>|<color=.*?>|</color>|<material=.*?>|</material>|<quad name=(.+?) size=(\\d*\\.?\\d+%?) width=(\\d*\\.?\\d+%?) />|<a href=([^>\\n\\s]+)>|</a>|\\s", RegexOptions.Singleline);

		// Token: 0x04000AE7 RID: 2791
		private List<int> indexes = new List<int>();

		// Token: 0x04000AE8 RID: 2792
		private int charactersRemoved;

		// Token: 0x04000AE9 RID: 2793
		private int startCharactersRemoved;

		// Token: 0x04000AEA RID: 2794
		private int endCharactersRemoved;

		// Token: 0x04000AEB RID: 2795
		private int count;

		// Token: 0x04000AEC RID: 2796
		private int indexText;

		// Token: 0x04000AED RID: 2797
		private string originalText;

		// Token: 0x04000AEE RID: 2798
		private UIVertex vert;

		// Token: 0x04000AEF RID: 2799
		private Vector2 lp;

		// Token: 0x0200113C RID: 4412
		[Serializable]
		public struct IconName
		{
			// Token: 0x040091CB RID: 37323
			public string name;

			// Token: 0x040091CC RID: 37324
			public Sprite sprite;

			// Token: 0x040091CD RID: 37325
			public Vector2 offset;

			// Token: 0x040091CE RID: 37326
			public Vector2 scale;
		}

		// Token: 0x0200113D RID: 4413
		[Serializable]
		public class HrefClickEvent : UnityEvent<string>
		{
		}

		// Token: 0x0200113E RID: 4414
		[Serializable]
		public class HrefInfo
		{
			// Token: 0x040091CF RID: 37327
			public int startIndex;

			// Token: 0x040091D0 RID: 37328
			public int endIndex;

			// Token: 0x040091D1 RID: 37329
			public string name;

			// Token: 0x040091D2 RID: 37330
			public readonly List<Rect> boxes = new List<Rect>();
		}
	}
}

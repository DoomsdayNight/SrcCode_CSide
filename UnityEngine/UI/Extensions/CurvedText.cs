using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D7 RID: 727
	[RequireComponent(typeof(Text), typeof(RectTransform))]
	[AddComponentMenu("UI/Effects/Extensions/Curved Text")]
	public class CurvedText : BaseMeshEffect
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00034676 File Offset: 0x00032876
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x0003467E File Offset: 0x0003287E
		public AnimationCurve CurveForText
		{
			get
			{
				return this._curveForText;
			}
			set
			{
				this._curveForText = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00034692 File Offset: 0x00032892
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x0003469A File Offset: 0x0003289A
		public float CurveMultiplier
		{
			get
			{
				return this._curveMultiplier;
			}
			set
			{
				this._curveMultiplier = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000346AE File Offset: 0x000328AE
		protected override void Awake()
		{
			base.Awake();
			this.rectTrans = base.GetComponent<RectTransform>();
			this.OnRectTransformDimensionsChange();
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000346C8 File Offset: 0x000328C8
		protected override void OnEnable()
		{
			base.OnEnable();
			this.rectTrans = base.GetComponent<RectTransform>();
			this.OnRectTransformDimensionsChange();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000346E4 File Offset: 0x000328E4
		public override void ModifyMesh(VertexHelper vh)
		{
			int currentVertCount = vh.currentVertCount;
			if (!this.IsActive() || currentVertCount == 0)
			{
				return;
			}
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				UIVertex uivertex = default(UIVertex);
				vh.PopulateUIVertex(ref uivertex, i);
				uivertex.position.y = uivertex.position.y + this._curveForText.Evaluate(this.rectTrans.rect.width * this.rectTrans.pivot.x + uivertex.position.x) * this._curveMultiplier;
				vh.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00034780 File Offset: 0x00032980
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.rectTrans)
			{
				Keyframe key = this._curveForText[this._curveForText.length - 1];
				key.time = this.rectTrans.rect.width;
				this._curveForText.MoveKey(this._curveForText.length - 1, key);
			}
		}

		// Token: 0x04000B15 RID: 2837
		[SerializeField]
		private AnimationCurve _curveForText = AnimationCurve.Linear(0f, 0f, 1f, 10f);

		// Token: 0x04000B16 RID: 2838
		[SerializeField]
		private float _curveMultiplier = 1f;

		// Token: 0x04000B17 RID: 2839
		private RectTransform rectTrans;
	}
}

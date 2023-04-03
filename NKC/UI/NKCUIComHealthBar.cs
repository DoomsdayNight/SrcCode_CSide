using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000936 RID: 2358
	[RequireComponent(typeof(Image))]
	public class NKCUIComHealthBar : MonoBehaviour
	{
		// Token: 0x06005E30 RID: 24112 RVA: 0x001D2811 File Offset: 0x001D0A11
		protected void Awake()
		{
			this.Init();
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x001D281C File Offset: 0x001D0A1C
		public void Init()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_bInit = true;
			this.image = base.GetComponent<Image>();
			this.imageRectTransform = this.image.GetComponent<RectTransform>();
			this.image.material = UnityEngine.Object.Instantiate<Material>(this.image.material);
			this.image.material.SetFloat("_BigStep", this.m_fBigStep);
			this.image.material.SetFloat("_SmallStep", this.m_fSmallStep);
			this.image.material.SetFloat("_SmallStepSize", this.m_fSmallStepSize);
			this.image.material.SetColor("_BarrierColor", this.m_colBarrierColor);
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x001D28DD File Offset: 0x001D0ADD
		public void SetColor(bool bEnemy)
		{
			this.image.material.SetColor("_Color", bEnemy ? this.m_colEnemyHealth : this.m_colAllyHealth);
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x001D2908 File Offset: 0x001D0B08
		public void SetStepRatio(float ratio)
		{
			this.m_fStepRatio = ratio;
			this.image.material.SetFloat("_BigStep", this.m_fBigStep * this.m_fStepRatio);
			this.image.material.SetFloat("_SmallStep", this.m_fSmallStep * this.m_fStepRatio);
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x001D2960 File Offset: 0x001D0B60
		public void SetData(float currentHP, float barrier, float maxHP)
		{
			if (this.image == null)
			{
				return;
			}
			this.m_HPMax = Mathf.Max(currentHP + barrier, maxHP);
			this.image.material.SetFloat("_Max", this.m_HPMax);
			this.image.material.SetFloat("_Percent", (this.m_HPMax != 0f) ? (currentHP / this.m_HPMax) : 0f);
			this.image.material.SetFloat("_BarrierPercent", (this.m_HPMax != 0f) ? (barrier / this.m_HPMax) : 0f);
			if ((float)this.BIG_STEP_THRESHOLD * this.m_fBigStep * this.m_fStepRatio < this.m_HPMax)
			{
				this.image.material.SetFloat("_SmallStepSize", 0f);
				return;
			}
			this.image.material.SetFloat("_SmallStepSize", this.m_fSmallStepSize);
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x001D2A5C File Offset: 0x001D0C5C
		private void Update()
		{
			this.imageRectTransform.GetWorldCorners(this.buffer);
			Vector3 vector = NKCCamera.GetCamera().WorldToScreenPoint(this.buffer[1]);
			float num = Mathf.Abs(NKCCamera.GetCamera().WorldToScreenPoint(this.buffer[2]).x - vector.x);
			this.image.material.SetFloat("_hpDotSize", this.m_HPMax * this.m_fBorderWidth / num);
			this.image.material.SetFloat("_hpDotSmallSize", this.m_HPMax * this.m_fBorderSmallWidth / num);
		}

		// Token: 0x04004A62 RID: 19042
		public Color m_colAllyHealth;

		// Token: 0x04004A63 RID: 19043
		public Color m_colEnemyHealth;

		// Token: 0x04004A64 RID: 19044
		public Color m_colBarrierColor;

		// Token: 0x04004A65 RID: 19045
		public float m_fBorderWidth = 2f;

		// Token: 0x04004A66 RID: 19046
		public float m_fBorderSmallWidth = 1f;

		// Token: 0x04004A67 RID: 19047
		public float m_fBigStep = 20000f;

		// Token: 0x04004A68 RID: 19048
		public float m_fSmallStep = 5000f;

		// Token: 0x04004A69 RID: 19049
		public float m_fSmallStepSize = 0.5f;

		// Token: 0x04004A6A RID: 19050
		public int BIG_STEP_THRESHOLD = 10;

		// Token: 0x04004A6B RID: 19051
		private Image image;

		// Token: 0x04004A6C RID: 19052
		private RectTransform imageRectTransform;

		// Token: 0x04004A6D RID: 19053
		private float m_fStepRatio = 1f;

		// Token: 0x04004A6E RID: 19054
		private Vector3[] buffer = new Vector3[4];

		// Token: 0x04004A6F RID: 19055
		private float m_HPMax;

		// Token: 0x04004A70 RID: 19056
		private bool m_bInit;
	}
}

using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002EC RID: 748
	public class CardExpanding2D : MonoBehaviour
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x0003883C File Offset: 0x00036A3C
		private void Start()
		{
			this.rectTrans = base.GetComponent<RectTransform>();
			this.buttonRect.GetComponent<Image>().color = new Color32(228, 0, 0, 0);
			this.closeButtonMin = new Vector2(this.pageMin.x + this.pageSize.x - 64f, this.pageMin.y + this.pageSize.y - 64f);
			this.closeButtonMax = new Vector2(this.pageMax.x - 16f, this.pageMax.y - 16f);
			this.cardMin = new Vector2(this.cardCenter.x - this.cardSize.x * 0.5f, this.cardCenter.y - this.cardSize.y * 0.5f);
			this.cardMax = new Vector2(this.cardCenter.x + this.cardSize.x * 0.5f, this.cardCenter.y + this.cardSize.y * 0.5f);
			this.pageMin = new Vector2(this.pageCenter.x - this.pageSize.x * 0.5f, this.pageCenter.y - this.pageSize.y * 0.5f);
			this.pageMax = new Vector2(this.pageCenter.x + this.pageSize.x * 0.5f, this.pageCenter.y + this.pageSize.y * 0.5f);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00038A00 File Offset: 0x00036C00
		private void Update()
		{
			if (this.animationActive == 1)
			{
				this.rectTrans.offsetMin = Vector2.Lerp(this.rectTrans.offsetMin, this.pageMin, Time.deltaTime * this.lerpSpeed);
				this.rectTrans.offsetMax = Vector2.Lerp(this.rectTrans.offsetMax, this.pageMax, Time.deltaTime * this.lerpSpeed);
				if (this.rectTrans.offsetMin.x < this.pageMin.x * 0.995f && this.rectTrans.offsetMin.y < this.pageMin.y * 0.995f && this.rectTrans.offsetMax.x > this.pageMax.x * 0.995f && this.rectTrans.offsetMax.y > this.pageMax.y * 0.995f)
				{
					this.rectTrans.offsetMin = this.pageMin;
					this.rectTrans.offsetMax = this.pageMax;
					this.buttonRect.GetComponent<Image>().color = Color32.Lerp(this.buttonRect.GetComponent<Image>().color, new Color32(228, 0, 0, 191), Time.deltaTime * this.lerpSpeed);
					if (Mathf.Abs(this.buttonRect.GetComponent<Image>().color.a - 191f) < 2f)
					{
						this.buttonRect.GetComponent<Image>().color = new Color32(228, 0, 0, 191);
						this.animationActive = 0;
						CardStack2D.canUseHorizontalAxis = true;
						return;
					}
				}
			}
			else if (this.animationActive == -1)
			{
				this.buttonRect.GetComponent<Image>().color = Color32.Lerp(this.buttonRect.GetComponent<Image>().color, new Color32(228, 0, 0, 0), Time.deltaTime * this.lerpSpeed * 1.25f);
				this.rectTrans.offsetMin = Vector2.Lerp(this.rectTrans.offsetMin, this.cardMin, Time.deltaTime * this.lerpSpeed);
				this.rectTrans.offsetMax = Vector2.Lerp(this.rectTrans.offsetMax, this.cardMax, Time.deltaTime * this.lerpSpeed);
				if (this.rectTrans.offsetMin.x > this.cardMin.x * 1.005f && this.rectTrans.offsetMin.y > this.cardMin.y * 1.005f && this.rectTrans.offsetMax.x < this.cardMax.x * 1.005f && this.rectTrans.offsetMax.y < this.cardMax.y * 1.005f)
				{
					this.rectTrans.offsetMin = this.cardMin;
					this.rectTrans.offsetMax = this.cardMax;
					this.buttonRect.offsetMin = Vector2.zero;
					this.buttonRect.offsetMax = Vector2.zero;
					this.animationActive = 0;
					CardStack2D.canUseHorizontalAxis = true;
				}
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00038D6C File Offset: 0x00036F6C
		public void ToggleCard()
		{
			CardStack2D.canUseHorizontalAxis = false;
			if (this.animationActive != 1)
			{
				this.animationActive = 1;
				this.cardCenter = base.transform.localPosition;
				this.buttonRect.offsetMin = this.closeButtonMin;
				this.buttonRect.offsetMax = this.closeButtonMax;
				return;
			}
			if (this.animationActive != -1)
			{
				this.animationActive = -1;
			}
		}

		// Token: 0x04000B69 RID: 2921
		[SerializeField]
		private float lerpSpeed = 8f;

		// Token: 0x04000B6A RID: 2922
		[SerializeField]
		private RectTransform buttonRect;

		// Token: 0x04000B6B RID: 2923
		private Vector2 closeButtonMin = Vector2.zero;

		// Token: 0x04000B6C RID: 2924
		private Vector2 closeButtonMax = Vector2.zero;

		// Token: 0x04000B6D RID: 2925
		[SerializeField]
		private Vector2 cardSize = Vector2.zero;

		// Token: 0x04000B6E RID: 2926
		[SerializeField]
		private Vector2 pageSize = Vector2.zero;

		// Token: 0x04000B6F RID: 2927
		private Vector2 cardCenter = Vector2.zero;

		// Token: 0x04000B70 RID: 2928
		private Vector2 pageCenter = Vector2.zero;

		// Token: 0x04000B71 RID: 2929
		private Vector2 cardMin = Vector2.zero;

		// Token: 0x04000B72 RID: 2930
		private Vector2 cardMax = Vector2.zero;

		// Token: 0x04000B73 RID: 2931
		private Vector2 pageMin = Vector2.zero;

		// Token: 0x04000B74 RID: 2932
		private Vector2 pageMax = Vector2.zero;

		// Token: 0x04000B75 RID: 2933
		private RectTransform rectTrans;

		// Token: 0x04000B76 RID: 2934
		private int animationActive = -1;
	}
}

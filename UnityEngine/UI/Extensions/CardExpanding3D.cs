using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002EF RID: 751
	[ExecuteInEditMode]
	public class CardExpanding3D : MonoBehaviour
	{
		// Token: 0x06001081 RID: 4225 RVA: 0x000393CC File Offset: 0x000375CC
		private void Start()
		{
			if (this.cardAutoSize)
			{
				this.cardSize = new Vector2(this.cardCorners[0].localScale.x * 2f + this.cardEdges[0].localScale.x, this.cardCorners[0].localScale.y * 2f + this.cardEdges[0].localScale.y);
				this.cardPosition = this.cardCenter.localPosition;
			}
			if (this.pageAutoSize)
			{
				this.pageSize = new Vector2((float)Screen.width, (float)(Screen.height / 3));
				this.pagePosition = new Vector2(0f, (float)(Screen.height / 2) - this.pageSize.y / 2f);
			}
			this.rect = base.GetComponent<RectTransform>();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000394B0 File Offset: 0x000376B0
		private void Update()
		{
			if (this.animationActive == 1 || this.animationActive == -1)
			{
				for (int i = 0; i < this.cardCorners.Length; i++)
				{
					this.cardCorners[i].localPosition = Vector3.Lerp(this.cardCorners[i].localPosition, this.nextCornerPos[i], Time.deltaTime * this.lerpSpeed);
					this.cardCorners[i].GetComponent<SuperellipsePoints>().superness = Mathf.Lerp(this.cardCorners[i].GetComponent<SuperellipsePoints>().superness, (float)this.nextSuperness, Time.deltaTime * this.lerpSpeed);
					if (Mathf.Abs(this.cardCorners[i].GetComponent<SuperellipsePoints>().superness - (float)this.nextSuperness) <= 1f)
					{
						this.cardCorners[i].localPosition = this.nextCornerPos[i];
						this.cardEdges[i].localPosition = this.nextEdgePos[i];
						this.cardEdges[i].localScale = new Vector3(this.nextEdgeScale[i].x, this.nextEdgeScale[i].y, 1f);
						base.transform.localPosition = this.nextPos;
						this.cardCenter.localScale = new Vector3(this.nextCenterScale.x, this.nextCenterScale.y, 1f);
						this.cardCorners[i].GetComponent<SuperellipsePoints>().superness = (float)this.nextSuperness;
						this.rect.offsetMin = this.nextMin;
						this.rect.offsetMax = this.nextMax;
					}
				}
				for (int j = 0; j < this.cardEdges.Length; j++)
				{
					this.cardEdges[j].localPosition = Vector3.Lerp(this.cardEdges[j].localPosition, this.nextEdgePos[j], Time.deltaTime * this.lerpSpeed);
					this.cardEdges[j].localScale = Vector3.Lerp(this.cardEdges[j].localScale, new Vector3(this.nextEdgeScale[j].x, this.nextEdgeScale[j].y, 1f), Time.deltaTime * this.lerpSpeed);
				}
				base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.nextPos, Time.deltaTime * this.lerpSpeed);
				this.cardCenter.localScale = Vector3.Lerp(this.cardCenter.localScale, new Vector3(this.nextCenterScale.x, this.nextCenterScale.y, 1f), Time.deltaTime * this.lerpSpeed);
				this.rect.offsetMin = Vector3.Lerp(this.rect.offsetMin, this.nextMin, Time.deltaTime * this.lerpSpeed);
				this.rect.offsetMax = Vector3.Lerp(this.rect.offsetMax, this.nextMax, Time.deltaTime * this.lerpSpeed);
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0003981C File Offset: 0x00037A1C
		public void ToggleCard()
		{
			if (this.animationActive != 1 || this.animationActive == 0)
			{
				this.animationActive = 1;
				for (int i = 0; i < this.cardCorners.Length; i++)
				{
					float x = this.pageSize.x / 2f * Mathf.Sign(this.cardCorners[i].localScale.x) - this.cardCorners[i].localScale.x;
					float y = this.pageSize.y / 2f * Mathf.Sign(this.cardCorners[i].localScale.y) - this.cardCorners[i].localScale.y;
					this.nextCornerPos[i] = new Vector2(x, y);
				}
				for (int j = 0; j < this.cardEdges.Length; j++)
				{
					float x2 = 0f;
					float y2 = 0f;
					float x3 = 0f;
					float y3 = 0f;
					if (this.cardEdges[j].localPosition.x != 0f)
					{
						x2 = Mathf.Sign(this.cardEdges[j].localPosition.x) * (this.pageSize.x / 2f - this.cardEdges[j].localScale.x / 2f);
						y2 = 0f;
						x3 = this.cornerSize;
						y3 = this.pageSize.y - this.cornerSize * 2f;
					}
					else if (this.cardEdges[j].localPosition.y != 0f)
					{
						x2 = 0f;
						y2 = Mathf.Sign(this.cardEdges[j].localPosition.y) * (this.pageSize.y / 2f - this.cardEdges[j].localScale.y / 2f);
						x3 = this.pageSize.x - this.cornerSize * 2f;
						y3 = this.cornerSize;
					}
					this.nextEdgePos[j] = new Vector2(x2, y2);
					this.nextEdgeScale[j] = new Vector2(x3, y3);
				}
				this.nextCenterScale = this.pageSize - new Vector2(this.cornerSize * 2f, this.cornerSize * 2f);
				this.nextPos = this.pagePosition;
				this.nextSuperness = this.pageSuperness;
				this.nextMin = new Vector2(-this.pageSize.x / 2f, -this.pageSize.y / 2f) + this.nextPos;
				this.nextMax = new Vector2(this.pageSize.x / 2f, this.pageSize.y / 2f) + this.nextPos;
				return;
			}
			if (this.animationActive != -1)
			{
				this.animationActive = -1;
				for (int k = 0; k < this.cardCorners.Length; k++)
				{
					float x4 = Mathf.Sign(this.cardCorners[k].localScale.x) * (this.cardSize.x / 2f) - this.cardCorners[k].localScale.x;
					float y4 = Mathf.Sign(this.cardCorners[k].localScale.y) * (this.cardSize.y / 2f) - this.cardCorners[k].localScale.y;
					this.nextCornerPos[k] = new Vector2(x4, y4);
				}
				for (int l = 0; l < this.cardEdges.Length; l++)
				{
					float x5 = 0f;
					float y5 = 0f;
					float x6 = 0f;
					float y6 = 0f;
					if (this.cardEdges[l].localPosition.x != 0f)
					{
						x5 = Mathf.Sign(this.cardEdges[l].localPosition.x) * (this.cardSize.x / 2f) - Mathf.Sign(this.cardEdges[l].localPosition.x) * (this.cardEdges[l].localScale.x / 2f);
						y5 = 0f;
						x6 = this.cornerSize;
						y6 = this.cardSize.y - this.cornerSize * 2f;
					}
					else if (this.cardEdges[l].localPosition.y != 0f)
					{
						x5 = 0f;
						y5 = Mathf.Sign(this.cardEdges[l].localPosition.y) * (this.cardSize.y / 2f) - Mathf.Sign(this.cardEdges[l].localPosition.y) * (this.cardEdges[l].localScale.y / 2f);
						x6 = this.cardSize.x - this.cornerSize * 2f;
						y6 = this.cornerSize;
					}
					this.nextEdgePos[l] = new Vector2(x5, y5);
					this.nextEdgeScale[l] = new Vector2(x6, y6);
				}
				this.nextCenterScale = this.cardSize - new Vector2(this.cornerSize * 2f, this.cornerSize * 2f);
				this.nextPos = this.cardPosition;
				this.nextSuperness = this.cardSuperness;
				this.nextMin = new Vector2(-this.cardSize.x / 2f, -this.cardSize.y / 2f) + this.nextPos;
				this.nextMax = new Vector2(this.cardSize.x / 2f, this.cardSize.y / 2f) + this.nextPos;
			}
		}

		// Token: 0x04000B8B RID: 2955
		[SerializeField]
		private float lerpSpeed = 12f;

		// Token: 0x04000B8C RID: 2956
		[SerializeField]
		private float cornerSize = 64f;

		// Token: 0x04000B8D RID: 2957
		[Header("Parts")]
		public RectTransform[] cardCorners;

		// Token: 0x04000B8E RID: 2958
		public RectTransform[] cardEdges;

		// Token: 0x04000B8F RID: 2959
		public RectTransform cardCenter;

		// Token: 0x04000B90 RID: 2960
		[Header("Card Info")]
		[Tooltip("Positions and sizes card to its current transform.")]
		public bool cardAutoSize = true;

		// Token: 0x04000B91 RID: 2961
		public Vector2 cardSize;

		// Token: 0x04000B92 RID: 2962
		public Vector2 cardPosition;

		// Token: 0x04000B93 RID: 2963
		[Range(1f, 96f)]
		public int cardSuperness = 4;

		// Token: 0x04000B94 RID: 2964
		[Header("Page Info")]
		[Tooltip("Positions and sizes the page to the top third of the screen.")]
		public bool pageAutoSize = true;

		// Token: 0x04000B95 RID: 2965
		public Vector2 pageSize;

		// Token: 0x04000B96 RID: 2966
		public Vector2 pagePosition;

		// Token: 0x04000B97 RID: 2967
		[Range(1f, 96f)]
		public int pageSuperness = 96;

		// Token: 0x04000B98 RID: 2968
		private int animationActive;

		// Token: 0x04000B99 RID: 2969
		private Vector2[] nextCornerPos = new Vector2[4];

		// Token: 0x04000B9A RID: 2970
		private Vector2[] nextEdgePos = new Vector2[4];

		// Token: 0x04000B9B RID: 2971
		private Vector2[] nextEdgeScale = new Vector2[4];

		// Token: 0x04000B9C RID: 2972
		private Vector2 nextCenterScale;

		// Token: 0x04000B9D RID: 2973
		private Vector2 nextPos;

		// Token: 0x04000B9E RID: 2974
		private int nextSuperness;

		// Token: 0x04000B9F RID: 2975
		private RectTransform rect;

		// Token: 0x04000BA0 RID: 2976
		private Vector2 nextMin;

		// Token: 0x04000BA1 RID: 2977
		private Vector2 nextMax;
	}
}

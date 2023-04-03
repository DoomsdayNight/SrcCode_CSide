using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200097B RID: 2427
	[AddComponentMenu("UI/Custom Image")]
	[ExecuteInEditMode]
	public class NKCUICustomImage : Image
	{
		// Token: 0x060062AA RID: 25258 RVA: 0x001EF8F4 File Offset: 0x001EDAF4
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			base.OnPopulateMesh(vh);
			if (this.vertOffset == null)
			{
				this.vertOffset = new Vector2[vh.currentVertCount];
			}
			if (this.vertRelativeOffset == null)
			{
				this.vertRelativeOffset = new Vector2[vh.currentVertCount];
			}
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				UIVertex simpleVert = UIVertex.simpleVert;
				vh.PopulateUIVertex(ref simpleVert, i);
				Vector3 vector = simpleVert.position;
				if (this.relativeOffset)
				{
					if (base.rectTransform != null)
					{
						Vector3 vector2 = new Vector3(base.rectTransform.rect.width * this.vertRelativeOffset[i].x, base.rectTransform.rect.height * this.vertRelativeOffset[i].y, 0f);
						vector += vector2;
						this.vertOffset[i] = vector2;
						if (this.maintainImage)
						{
							Vector2 v = simpleVert.uv0;
							v.x += this.vertRelativeOffset[i].x;
							v.y += this.vertRelativeOffset[i].y;
							simpleVert.uv0 = v;
						}
					}
				}
				else
				{
					vector += new Vector3(this.vertOffset[i].x, this.vertOffset[i].y, 0f);
					if (base.rectTransform != null && base.rectTransform.rect.width != 0f && base.rectTransform.rect.height != 0f)
					{
						this.vertRelativeOffset[i] = new Vector3(this.vertOffset[i].x / base.rectTransform.rect.width, this.vertOffset[i].y / base.rectTransform.rect.height, 0f);
						if (this.maintainImage)
						{
							Vector2 v2 = simpleVert.uv0;
							v2.x += this.vertRelativeOffset[i].x;
							v2.y += this.vertRelativeOffset[i].y;
							simpleVert.uv0 = v2;
						}
					}
				}
				simpleVert.position = vector;
				vh.SetUIVertex(simpleVert, i);
			}
		}

		// Token: 0x04004E73 RID: 20083
		[HideInInspector]
		[Header("이미지 마스킹 영역 조정(절대 좌표)")]
		public Vector2[] vertOffset;

		// Token: 0x04004E74 RID: 20084
		[HideInInspector]
		[Header("이미지 마스킹 영역 조정(상대 좌표)")]
		public Vector2[] vertRelativeOffset;

		// Token: 0x04004E75 RID: 20085
		[Header("이미지 원본 형태 유지 설정")]
		public bool maintainImage = true;

		// Token: 0x04004E76 RID: 20086
		[Header("이미지 마스킹 좌표 타입(상대 좌표)")]
		public bool relativeOffset;
	}
}

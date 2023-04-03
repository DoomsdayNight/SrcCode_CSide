using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200080E RID: 2062
	public class TextureForceRenderer : MonoBehaviour
	{
		// Token: 0x060051C1 RID: 20929 RVA: 0x0018CE9C File Offset: 0x0018B09C
		private void Start()
		{
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x0018CE9E File Offset: 0x0018B09E
		private void Update()
		{
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x0018CEA0 File Offset: 0x0018B0A0
		private void OnPreRender()
		{
			if (this.m_tex != null)
			{
				Camera camera = NKCCamera.GetCamera();
				if (camera != null)
				{
					camera.clearFlags = CameraClearFlags.Nothing;
					Graphics.DrawTexture(new Rect
					{
						width = (float)Screen.width,
						height = (float)Screen.height,
						center = new Vector2((float)Screen.width * 0.5f, (float)Screen.height * 0.5f)
					}, this.m_tex);
				}
			}
		}

		// Token: 0x04004219 RID: 16921
		public Texture m_tex;
	}
}

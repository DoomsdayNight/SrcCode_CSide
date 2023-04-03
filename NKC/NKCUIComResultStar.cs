using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200075D RID: 1885
	public class NKCUIComResultStar : MonoBehaviour
	{
		// Token: 0x06004B45 RID: 19269 RVA: 0x001688D8 File Offset: 0x00166AD8
		public void SetTranscendence(bool value)
		{
			if (value)
			{
				using (List<Image>.Enumerator enumerator = this.m_lstStarImages.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Image image = enumerator.Current;
						image.sprite = this.m_spStarPurple;
					}
					return;
				}
			}
			foreach (Image image2 in this.m_lstStarImages)
			{
				image2.sprite = this.m_spStarYellow;
			}
		}

		// Token: 0x040039DC RID: 14812
		public List<Image> m_lstStarImages;

		// Token: 0x040039DD RID: 14813
		public Sprite m_spStarYellow;

		// Token: 0x040039DE RID: 14814
		public Sprite m_spStarPurple;
	}
}

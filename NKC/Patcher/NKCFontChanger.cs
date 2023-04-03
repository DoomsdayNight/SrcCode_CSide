using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NKC.Patcher
{
	// Token: 0x02000870 RID: 2160
	public class NKCFontChanger : MonoBehaviour
	{
		// Token: 0x060055D2 RID: 21970 RVA: 0x0019F7C4 File Offset: 0x0019D9C4
		public void ChagneAllMainFont(NKM_NATIONAL_CODE nationCode)
		{
			if (nationCode == NKM_NATIONAL_CODE.NNC_END)
			{
				return;
			}
			if (this.m_listLocalizedFont == null)
			{
				return;
			}
			NKCFontChanger.LocalizedFont localizedFont = this.m_listLocalizedFont.Find((NKCFontChanger.LocalizedFont x) => x.m_nationCode == nationCode);
			if (localizedFont == null)
			{
				return;
			}
			this.ChangeFontInScene(localizedFont.m_mainFont, localizedFont.m_mainTMPFont, "MainFont", "TmpFont");
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x0019F82C File Offset: 0x0019DA2C
		public void ChangeFontInScene(Font font, TMP_FontAsset tmpFont, string fontName, string tmpFontName)
		{
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			if (rootGameObjects == null)
			{
				return;
			}
			foreach (GameObject gameObject in rootGameObjects)
			{
				if (!(gameObject == null))
				{
					if (font != null)
					{
						Text[] componentsInChildren = gameObject.GetComponentsInChildren<Text>(true);
						if (componentsInChildren != null)
						{
							for (int j = 0; j < componentsInChildren.Length; j++)
							{
								if (!(componentsInChildren[j] == null) && !(componentsInChildren[j].font == null) && !(componentsInChildren[j].font.name != fontName))
								{
									componentsInChildren[j].font = font;
								}
							}
						}
					}
					if (tmpFont != null)
					{
						TextMeshProUGUI[] componentsInChildren2 = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
						if (componentsInChildren2 != null)
						{
							for (int k = 0; k < componentsInChildren2.Length; k++)
							{
								if (!(componentsInChildren2[k] == null) && !(componentsInChildren2[k].font == null) && !(componentsInChildren2[k].font.name != tmpFontName))
								{
									componentsInChildren2[k].font = tmpFont;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04004482 RID: 17538
		public List<NKCFontChanger.LocalizedFont> m_listLocalizedFont = new List<NKCFontChanger.LocalizedFont>();

		// Token: 0x02001527 RID: 5415
		[Serializable]
		public class LocalizedFont
		{
			// Token: 0x04009FFE RID: 40958
			public NKM_NATIONAL_CODE m_nationCode;

			// Token: 0x04009FFF RID: 40959
			public Font m_mainFont;

			// Token: 0x0400A000 RID: 40960
			public TMP_FontAsset m_mainTMPFont;
		}
	}
}

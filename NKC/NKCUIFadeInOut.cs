using System;
using NKC.UI;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007BF RID: 1983
	public class NKCUIFadeInOut
	{
		// Token: 0x06004E93 RID: 20115 RVA: 0x0017AF2C File Offset: 0x0017912C
		public static void InitUI()
		{
			NKCUIFadeInOut.m_NKM_FADE_IN_OUT = NKCUIManager.OpenUI("NKM_FADE_IN_OUT");
			NKCUIFadeInOut.m_CanvasRenderer = NKCUIFadeInOut.m_NKM_FADE_IN_OUT.GetComponent<CanvasRenderer>();
			NKCUIFadeInOut.m_Image = NKCUIFadeInOut.m_NKM_FADE_IN_OUT.GetComponent<Image>();
			if (NKCUIFadeInOut.m_NKM_FADE_IN_OUT.activeSelf)
			{
				NKCUIFadeInOut.m_NKM_FADE_IN_OUT.SetActive(false);
			}
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x0017AF80 File Offset: 0x00179180
		private static void InitFade(float fTime)
		{
			if (NKCUIFadeInOut.m_NKM_FADE_IN_OUT != null && !NKCUIFadeInOut.m_NKM_FADE_IN_OUT.activeSelf)
			{
				NKCUIFadeInOut.m_NKM_FADE_IN_OUT.SetActive(true);
			}
			NKCUIFadeInOut.m_fTotalTime = fTime;
			NKCUIFadeInOut.m_fElapsedTime = 0f;
			NKCUIFadeInOut.m_bReservedTimeOutForFadeOut = false;
			NKCUIFadeInOut.m_bTimeOutForFadeOut = false;
			NKCUIFadeInOut.m_fTimeOutForFadeOut = 0f;
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x0017AFD8 File Offset: 0x001791D8
		public static void Finish()
		{
			if (NKCUIFadeInOut.m_bFadeIn)
			{
				if (NKCUIFadeInOut.m_NKM_FADE_IN_OUT != null && NKCUIFadeInOut.m_NKM_FADE_IN_OUT.activeSelf)
				{
					NKCUIFadeInOut.m_NKM_FADE_IN_OUT.SetActive(false);
				}
				if (NKCUIFadeInOut.m_Image != null)
				{
					NKCUtil.SetImageColor(NKCUIFadeInOut.m_Image, new Color(NKCUIFadeInOut.m_Image.color.r, NKCUIFadeInOut.m_Image.color.g, NKCUIFadeInOut.m_Image.color.b, 1f));
				}
				NKCUIFadeInOut.m_bTimeOutForFadeOut = false;
				NKCUIFadeInOut.m_bReservedTimeOutForFadeOut = false;
				NKCUIFadeInOut.m_fTimeOutForFadeOut = 0f;
			}
			else if (NKCUIFadeInOut.m_bFadeOut)
			{
				if (NKCUIFadeInOut.m_NKM_FADE_IN_OUT != null && !NKCUIFadeInOut.m_NKM_FADE_IN_OUT.activeSelf)
				{
					NKCUIFadeInOut.m_NKM_FADE_IN_OUT.SetActive(true);
				}
				if (NKCUIFadeInOut.m_Image != null)
				{
					NKCUtil.SetImageColor(NKCUIFadeInOut.m_Image, new Color(NKCUIFadeInOut.m_Image.color.r, NKCUIFadeInOut.m_Image.color.g, NKCUIFadeInOut.m_Image.color.b, 1f));
				}
				if (NKCUIFadeInOut.m_bReservedTimeOutForFadeOut)
				{
					NKCUIFadeInOut.m_bReservedTimeOutForFadeOut = false;
					NKCUIFadeInOut.m_bTimeOutForFadeOut = true;
				}
			}
			NKCUIFadeInOut.m_bFadeOut = false;
			NKCUIFadeInOut.m_bFadeIn = false;
			NKCUIFadeInOut.m_CallBack = null;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0017B120 File Offset: 0x00179320
		public static void Close(bool bDoCallBack = false)
		{
			if (NKCUIFadeInOut.m_NKM_FADE_IN_OUT != null && NKCUIFadeInOut.m_NKM_FADE_IN_OUT.activeSelf)
			{
				NKCUIFadeInOut.m_NKM_FADE_IN_OUT.SetActive(false);
			}
			if (NKCUIFadeInOut.m_Image != null)
			{
				NKCUtil.SetImageColor(NKCUIFadeInOut.m_Image, new Color(NKCUIFadeInOut.m_Image.color.r, NKCUIFadeInOut.m_Image.color.g, NKCUIFadeInOut.m_Image.color.b, 1f));
			}
			NKCUIFadeInOut.m_bFadeOut = false;
			NKCUIFadeInOut.m_bFadeIn = false;
			if (bDoCallBack && NKCUIFadeInOut.m_CallBack != null)
			{
				NKCUIFadeInOut.m_CallBack();
			}
			NKCUIFadeInOut.m_CallBack = null;
			NKCUIFadeInOut.m_bReservedTimeOutForFadeOut = false;
			NKCUIFadeInOut.m_fTimeOutForFadeOut = 0f;
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0017B1D5 File Offset: 0x001793D5
		public static bool IsFinshed()
		{
			return !NKCUIFadeInOut.m_bFadeOut && !NKCUIFadeInOut.m_bFadeIn;
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x0017B1E8 File Offset: 0x001793E8
		public static void FadeIn(float fTime, NKCUIFadeInOut.FadeCallBack fadeCallBack = null, bool bWhite = false)
		{
			if (bWhite)
			{
				if (NKCUIFadeInOut.m_Image != null)
				{
					NKCUIFadeInOut.m_Image.color = Color.white;
				}
			}
			else if (NKCUIFadeInOut.m_Image != null)
			{
				NKCUIFadeInOut.m_Image.color = Color.black;
			}
			if (NKCUIFadeInOut.m_Image != null)
			{
				NKCUtil.SetImageColor(NKCUIFadeInOut.m_Image, new Color(NKCUIFadeInOut.m_Image.color.r, NKCUIFadeInOut.m_Image.color.g, NKCUIFadeInOut.m_Image.color.b, 1f));
			}
			NKCUIFadeInOut.InitFade(fTime);
			NKCUIFadeInOut.m_bFadeIn = true;
			NKCUIFadeInOut.m_bFadeOut = false;
			NKCUIFadeInOut.m_CallBack = fadeCallBack;
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0017B298 File Offset: 0x00179498
		public static void FadeOut(float fTime, NKCUIFadeInOut.FadeCallBack fadeCallBack = null, bool bWhite = false, float fTimeOutForFadeOut = -1f)
		{
			if (bWhite)
			{
				if (NKCUIFadeInOut.m_Image != null)
				{
					NKCUIFadeInOut.m_Image.color = Color.white;
				}
			}
			else if (NKCUIFadeInOut.m_Image != null)
			{
				NKCUIFadeInOut.m_Image.color = Color.black;
			}
			if (NKCUIFadeInOut.m_Image != null)
			{
				NKCUtil.SetImageColor(NKCUIFadeInOut.m_Image, new Color(NKCUIFadeInOut.m_Image.color.r, NKCUIFadeInOut.m_Image.color.g, NKCUIFadeInOut.m_Image.color.b, 0f));
			}
			NKCUIFadeInOut.InitFade(fTime);
			NKCUIFadeInOut.m_bFadeOut = true;
			NKCUIFadeInOut.m_bFadeIn = false;
			if (fTimeOutForFadeOut > 0f)
			{
				NKCUIFadeInOut.m_bReservedTimeOutForFadeOut = true;
				NKCUIFadeInOut.m_fTimeOutForFadeOut = fTimeOutForFadeOut;
			}
			NKCUIFadeInOut.m_CallBack = fadeCallBack;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0017B35C File Offset: 0x0017955C
		public static void Update(float deltaTime)
		{
			if (deltaTime > 0.04f)
			{
				deltaTime = 0.04f;
			}
			if (NKCUIFadeInOut.m_bFadeIn)
			{
				if (NKCUIFadeInOut.m_fElapsedTime > NKCUIFadeInOut.m_fTotalTime)
				{
					NKCUIFadeInOut.m_bFadeIn = false;
					if (NKCUIFadeInOut.m_NKM_FADE_IN_OUT != null && NKCUIFadeInOut.m_NKM_FADE_IN_OUT.activeSelf)
					{
						NKCUIFadeInOut.m_NKM_FADE_IN_OUT.SetActive(false);
					}
					if (NKCUIFadeInOut.m_CallBack != null)
					{
						NKCUIFadeInOut.m_CallBack();
					}
					NKCUIFadeInOut.m_CallBack = null;
				}
				float num = (NKCUIFadeInOut.m_fTotalTime - NKCUIFadeInOut.m_fElapsedTime) / NKCUIFadeInOut.m_fTotalTime;
				if (num < 0f)
				{
					num = 0f;
				}
				if (num > 1f)
				{
					num = 1f;
				}
				if (NKCUIFadeInOut.m_Image != null)
				{
					NKCUtil.SetImageColor(NKCUIFadeInOut.m_Image, new Color(NKCUIFadeInOut.m_Image.color.r, NKCUIFadeInOut.m_Image.color.g, NKCUIFadeInOut.m_Image.color.b, num));
				}
				NKCUIFadeInOut.m_fElapsedTime += deltaTime;
			}
			else if (NKCUIFadeInOut.m_bFadeOut)
			{
				NKCUIFadeInOut.m_fElapsedTime += deltaTime;
				float num2 = NKCUIFadeInOut.m_fElapsedTime / NKCUIFadeInOut.m_fTotalTime;
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				if (NKCUIFadeInOut.m_Image != null)
				{
					NKCUtil.SetImageColor(NKCUIFadeInOut.m_Image, new Color(NKCUIFadeInOut.m_Image.color.r, NKCUIFadeInOut.m_Image.color.g, NKCUIFadeInOut.m_Image.color.b, num2));
				}
				if (NKCUIFadeInOut.m_fElapsedTime > NKCUIFadeInOut.m_fTotalTime)
				{
					NKCUIFadeInOut.m_bFadeOut = false;
					if (NKCUIFadeInOut.m_bReservedTimeOutForFadeOut)
					{
						NKCUIFadeInOut.m_bReservedTimeOutForFadeOut = false;
						NKCUIFadeInOut.m_bTimeOutForFadeOut = true;
					}
					if (NKCUIFadeInOut.m_CallBack != null)
					{
						NKCUIFadeInOut.m_CallBack();
					}
					NKCUIFadeInOut.m_CallBack = null;
				}
			}
			if (NKCUIFadeInOut.m_bTimeOutForFadeOut)
			{
				NKCUIFadeInOut.m_fTimeOutForFadeOut -= deltaTime;
				if (NKCUIFadeInOut.m_fTimeOutForFadeOut <= 0f)
				{
					NKCUIFadeInOut.m_bTimeOutForFadeOut = false;
					NKCUIFadeInOut.Close(false);
				}
			}
		}

		// Token: 0x04003E3A RID: 15930
		private static bool m_bOpen;

		// Token: 0x04003E3B RID: 15931
		private static GameObject m_NKM_FADE_IN_OUT;

		// Token: 0x04003E3C RID: 15932
		private static CanvasRenderer m_CanvasRenderer;

		// Token: 0x04003E3D RID: 15933
		private static Image m_Image;

		// Token: 0x04003E3E RID: 15934
		private static bool m_bFadeIn;

		// Token: 0x04003E3F RID: 15935
		private static bool m_bFadeOut;

		// Token: 0x04003E40 RID: 15936
		private static float m_fTotalTime;

		// Token: 0x04003E41 RID: 15937
		private static float m_fElapsedTime;

		// Token: 0x04003E42 RID: 15938
		private static NKCUIFadeInOut.FadeCallBack m_CallBack;

		// Token: 0x04003E43 RID: 15939
		private static bool m_bReservedTimeOutForFadeOut;

		// Token: 0x04003E44 RID: 15940
		private static bool m_bTimeOutForFadeOut;

		// Token: 0x04003E45 RID: 15941
		private static float m_fTimeOutForFadeOut;

		// Token: 0x0200147E RID: 5246
		// (Invoke) Token: 0x0600A8FB RID: 43259
		public delegate void FadeCallBack();
	}
}

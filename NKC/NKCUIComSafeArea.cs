using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200075E RID: 1886
	[DisallowMultipleComponent]
	public class NKCUIComSafeArea : MonoBehaviour
	{
		// Token: 0x06004B47 RID: 19271 RVA: 0x00168980 File Offset: 0x00166B80
		public static void RevertCalculatedSafeArea()
		{
			NKCUIComSafeArea.m_sbCalculatedSafeArea = false;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x00168988 File Offset: 0x00166B88
		public bool CheckInit()
		{
			return this.m_bInit;
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x00168990 File Offset: 0x00166B90
		private static bool IsZlongAndroidDevice()
		{
			return NKCDefineManager.DEFINE_ZLONG() && NKCDefineManager.DEFINE_ANDROID() && !NKCDefineManager.DEFINE_UNITY_EDITOR();
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x001689AC File Offset: 0x00166BAC
		private void Awake()
		{
			this.m_RectTransform = base.gameObject.GetComponent<RectTransform>();
			if (!NKCUIComSafeArea.s_bCompleteAddDevice && NKCUIComSafeArea.IsZlongAndroidDevice())
			{
				NKCUIComSafeArea.s_bCompleteAddDevice = true;
				NKCUIComSafeArea.s_dicDeviceSafeArea.Add("OPPO R15", 80f);
				NKCUIComSafeArea.s_dicDeviceSafeArea.Add("OPPO CPH1903", 54f);
			}
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x00168A06 File Offset: 0x00166C06
		private static string GetDeviceModelCached()
		{
			if (string.IsNullOrWhiteSpace(NKCUIComSafeArea.s_DeviceModelCached))
			{
				NKCUIComSafeArea.s_DeviceModelCached = SystemInfo.deviceModel.ToUpper();
			}
			return NKCUIComSafeArea.s_DeviceModelCached;
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00168A28 File Offset: 0x00166C28
		private static float GetSafeAreaX_Final()
		{
			float result;
			if (NKCUIComSafeArea.s_dicDeviceSafeArea.TryGetValue(NKCUIComSafeArea.GetDeviceModelCached(), out result))
			{
				return result;
			}
			return Screen.safeArea.x;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x00168A58 File Offset: 0x00166C58
		private static float GetSafeAreaY_Final()
		{
			return Screen.safeArea.y;
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00168A74 File Offset: 0x00166C74
		private static float GetSafeAreaWidth_Final()
		{
			if (NKCUIComSafeArea.s_dicDeviceSafeArea.ContainsKey(NKCUIComSafeArea.GetDeviceModelCached()))
			{
				return (float)Screen.currentResolution.width - NKCUIComSafeArea.GetSafeAreaX_Final() * 2f;
			}
			return Screen.safeArea.width;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x00168ABC File Offset: 0x00166CBC
		private static float GetSafeAreaHeight_Final()
		{
			return Screen.safeArea.height;
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00168AD8 File Offset: 0x00166CD8
		public static void InitSafeArea()
		{
			if (!NKCUIComSafeArea.m_sbCalculatedSafeArea)
			{
				NKCUIComSafeArea.m_sbCalculatedSafeArea = true;
				NKCUIComSafeArea.m_sfSafeAreaXPrev = NKCUIComSafeArea.m_sfSafeAreaX;
				NKCUIComSafeArea.m_sfSafeAreaYPrev = NKCUIComSafeArea.m_sfSafeAreaY;
				float safeAreaX_Final = NKCUIComSafeArea.GetSafeAreaX_Final();
				float safeAreaY_Final = NKCUIComSafeArea.GetSafeAreaY_Final();
				float safeAreaWidth_Final = NKCUIComSafeArea.GetSafeAreaWidth_Final();
				float safeAreaHeight_Final = NKCUIComSafeArea.GetSafeAreaHeight_Final();
				NKCUIComSafeArea.m_sfSafeAreaX = safeAreaX_Final;
				NKCUIComSafeArea.m_sfSafeAreaY = safeAreaY_Final;
				Vector2 vector = default(Vector2);
				vector.x = safeAreaWidth_Final / (float)Screen.currentResolution.width;
				vector.y = safeAreaHeight_Final / (float)Screen.currentResolution.height;
				if (vector.x > vector.y)
				{
					vector.x = vector.y;
				}
				else
				{
					vector.y = vector.x;
				}
				NKCUIComSafeArea.m_sfSafeAreaScale = vector.x;
			}
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x00168B98 File Offset: 0x00166D98
		public static Vector2 GetSafeAreaPos(Vector2 vec2, bool left, bool right, bool top, bool bottom)
		{
			NKCUIComSafeArea.InitSafeArea();
			if (left)
			{
				vec2.x += NKCUIComSafeArea.m_sfSafeAreaX;
			}
			if (right)
			{
				vec2.x -= NKCUIComSafeArea.m_sfSafeAreaX;
			}
			if (top)
			{
				vec2.y -= NKCUIComSafeArea.m_sfSafeAreaY;
			}
			if (bottom)
			{
				vec2.y += NKCUIComSafeArea.m_sfSafeAreaY;
			}
			return vec2;
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00168BF8 File Offset: 0x00166DF8
		public void SetInit()
		{
			this.m_bInit = true;
			this.m_bInitUI = true;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00168C08 File Offset: 0x00166E08
		public Vector2 GetSafeAreaPos(Vector2 vec2)
		{
			return NKCUIComSafeArea.GetSafeAreaPos(vec2, this.m_bUseLeft, this.m_bUseRight, this.m_bUseTop, this.m_bUseBottom);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x00168C28 File Offset: 0x00166E28
		public static Vector2 GetSafeAreaScale(Vector2 vec2)
		{
			NKCUIComSafeArea.InitSafeArea();
			vec2.x = NKCUIComSafeArea.m_sfSafeAreaScale;
			vec2.y = NKCUIComSafeArea.m_sfSafeAreaScale;
			return vec2;
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x00168C48 File Offset: 0x00166E48
		public Vector3 GetSafeAreaScale()
		{
			return new Vector3(NKCUIComSafeArea.m_sfSafeAreaScale, NKCUIComSafeArea.m_sfSafeAreaScale, 1f);
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x00168C5E File Offset: 0x00166E5E
		public float GetSafeAreaWidth(float width)
		{
			if (this.m_bUseRectSide)
			{
				width -= NKCUIComSafeArea.m_sfSafeAreaX * 2f + this.m_fAddRectSide;
				this.m_RectTransform.SetWidth(width);
			}
			return width;
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x00168C8B File Offset: 0x00166E8B
		public float GetSafeAreaHeight(float height)
		{
			if (this.m_bUseRectHeight && NKCUIComSafeArea.m_sfSafeAreaY > 0f)
			{
				height -= NKCUIComSafeArea.m_sfSafeAreaY + this.m_fAddRectHeight;
				this.m_RectTransform.SetHeight(height);
			}
			return height;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00168CBE File Offset: 0x00166EBE
		private void Start()
		{
			this.SetSafeAreaBase();
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x00168CC6 File Offset: 0x00166EC6
		public void SetSafeAreaBase()
		{
			if (this.m_bInit)
			{
				return;
			}
			if (!NKCUIComSafeArea.IsSafeAreaRequired())
			{
				return;
			}
			this.m_bInit = true;
			this.SetSafeArea();
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x00168CE6 File Offset: 0x00166EE6
		public void SetSafeAreaUI()
		{
			if (this.m_bInitUI)
			{
				return;
			}
			if (!NKCUIComSafeArea.IsSafeAreaRequired())
			{
				return;
			}
			this.m_bInitUI = true;
			this.m_bInit = true;
			this.SetSafeArea();
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x00168D10 File Offset: 0x00166F10
		public void Rollback()
		{
			if (this.m_bInit)
			{
				if (this.m_RectTransform == null)
				{
					return;
				}
				Vector2 anchoredPosition = this.m_RectTransform.anchoredPosition;
				if (this.m_bUseLeft)
				{
					anchoredPosition.x -= NKCUIComSafeArea.m_sfSafeAreaXPrev;
				}
				if (this.m_bUseRight)
				{
					anchoredPosition.x += NKCUIComSafeArea.m_sfSafeAreaXPrev;
				}
				if (this.m_bUseTop)
				{
					anchoredPosition.y += NKCUIComSafeArea.m_sfSafeAreaYPrev;
				}
				if (this.m_bUseBottom)
				{
					anchoredPosition.y -= NKCUIComSafeArea.m_sfSafeAreaYPrev;
				}
				this.m_RectTransform.anchoredPosition = anchoredPosition;
				if (this.m_bUseScale)
				{
					Vector3 localScale = this.m_RectTransform.localScale;
					localScale.x = 1f;
					localScale.y = 1f;
					this.m_RectTransform.localScale = localScale;
				}
				if (this.m_bUseRectHeight)
				{
					float num = this.m_RectTransform.GetHeight();
					num += NKCUIComSafeArea.m_sfSafeAreaYPrev + this.m_fAddRectHeight;
					this.m_RectTransform.SetHeight(num);
				}
				this.m_bOrgHeightCalc = false;
				if (this.m_bUseRectSide)
				{
					float num2 = this.m_RectTransform.GetWidth();
					num2 += NKCUIComSafeArea.m_sfSafeAreaXPrev * 2f + this.m_fAddRectSide;
					this.m_RectTransform.SetWidth(num2);
				}
				this.m_bOrgWidthCalc = false;
			}
			this.m_bInit = false;
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00168E60 File Offset: 0x00167060
		public void SetSafeArea()
		{
			if (!NKCUIComSafeArea.IsSafeAreaRequired())
			{
				return;
			}
			if (this.m_RectTransform == null)
			{
				return;
			}
			Vector3 localScale = this.m_RectTransform.localScale;
			Vector2 vector = new Vector2(localScale.x, localScale.y);
			if (this.m_bUseScale)
			{
				vector = NKCUIComSafeArea.GetSafeAreaScale(vector);
			}
			localScale.x = vector.x;
			localScale.y = vector.y;
			this.m_RectTransform.localScale = localScale;
			vector = this.m_RectTransform.anchoredPosition;
			vector = this.GetSafeAreaPos(vector);
			if (!this.m_bOrgWidthCalc)
			{
				this.m_bOrgWidthCalc = true;
				this.m_fOrgWidth = this.m_RectTransform.GetWidth();
			}
			if (!this.m_bOrgHeightCalc)
			{
				this.m_bOrgHeightCalc = true;
				this.m_fOrgHeight = this.m_RectTransform.GetHeight();
			}
			float safeAreaWidth = this.GetSafeAreaWidth(this.m_fOrgWidth);
			this.m_RectTransform.SetWidth(safeAreaWidth);
			float safeAreaHeight = this.GetSafeAreaHeight(this.m_fOrgHeight);
			this.m_RectTransform.SetHeight(safeAreaHeight);
			this.m_RectTransform.anchoredPosition = vector;
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x00168F6C File Offset: 0x0016716C
		private static bool IsSafeAreaRequired()
		{
			if (NKCUIComSafeArea.IsZlongAndroidDevice())
			{
				NKCUIComSafeArea.InitSafeArea();
				if (NKCUIComSafeArea.m_sfSafeAreaX > 0f || NKCUIComSafeArea.m_sfSafeAreaY > 0f)
				{
					return true;
				}
			}
			else if (Screen.safeArea.x > 0f || Screen.safeArea.y > 0f)
			{
				return true;
			}
			return false;
		}

		// Token: 0x040039DF RID: 14815
		private RectTransform m_RectTransform;

		// Token: 0x040039E0 RID: 14816
		private static bool m_sbCalculatedSafeArea = false;

		// Token: 0x040039E1 RID: 14817
		private static float m_sfSafeAreaX = 0f;

		// Token: 0x040039E2 RID: 14818
		private static float m_sfSafeAreaY = 0f;

		// Token: 0x040039E3 RID: 14819
		private static float m_sfSafeAreaScale = 1f;

		// Token: 0x040039E4 RID: 14820
		private static float m_sfSafeAreaXPrev = 0f;

		// Token: 0x040039E5 RID: 14821
		private static float m_sfSafeAreaYPrev = 0f;

		// Token: 0x040039E6 RID: 14822
		public bool m_bUseLeft;

		// Token: 0x040039E7 RID: 14823
		public bool m_bUseRight;

		// Token: 0x040039E8 RID: 14824
		public bool m_bUseTop;

		// Token: 0x040039E9 RID: 14825
		public bool m_bUseBottom;

		// Token: 0x040039EA RID: 14826
		public bool m_bUseScale;

		// Token: 0x040039EB RID: 14827
		public bool m_bUseRectSide;

		// Token: 0x040039EC RID: 14828
		public float m_fAddRectSide;

		// Token: 0x040039ED RID: 14829
		public bool m_bUseRectHeight;

		// Token: 0x040039EE RID: 14830
		public float m_fAddRectHeight;

		// Token: 0x040039EF RID: 14831
		private bool m_bInit;

		// Token: 0x040039F0 RID: 14832
		private bool m_bInitUI;

		// Token: 0x040039F1 RID: 14833
		private bool m_bOrgWidthCalc;

		// Token: 0x040039F2 RID: 14834
		private float m_fOrgWidth;

		// Token: 0x040039F3 RID: 14835
		private bool m_bOrgHeightCalc;

		// Token: 0x040039F4 RID: 14836
		private float m_fOrgHeight;

		// Token: 0x040039F5 RID: 14837
		private static Dictionary<string, float> s_dicDeviceSafeArea = new Dictionary<string, float>();

		// Token: 0x040039F6 RID: 14838
		private static bool s_bCompleteAddDevice = false;

		// Token: 0x040039F7 RID: 14839
		private static string s_DeviceModelCached = "";
	}
}

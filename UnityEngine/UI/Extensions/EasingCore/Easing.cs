using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UI.Extensions.EasingCore
{
	// Token: 0x02000351 RID: 849
	public static class Easing
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x0004B5A0 File Offset: 0x000497A0
		public static EasingFunction Get(Ease type)
		{
			switch (type)
			{
			case Ease.Linear:
				return new EasingFunction(Easing.<Get>g__linear|0_0);
			case Ease.InBack:
				return new EasingFunction(Easing.<Get>g__inBack|0_1);
			case Ease.InBounce:
				return new EasingFunction(Easing.<Get>g__inBounce|0_4);
			case Ease.InCirc:
				return new EasingFunction(Easing.<Get>g__inCirc|0_7);
			case Ease.InCubic:
				return new EasingFunction(Easing.<Get>g__inCubic|0_10);
			case Ease.InElastic:
				return new EasingFunction(Easing.<Get>g__inElastic|0_13);
			case Ease.InExpo:
				return new EasingFunction(Easing.<Get>g__inExpo|0_16);
			case Ease.InQuad:
				return new EasingFunction(Easing.<Get>g__inQuad|0_19);
			case Ease.InQuart:
				return new EasingFunction(Easing.<Get>g__inQuart|0_22);
			case Ease.InQuint:
				return new EasingFunction(Easing.<Get>g__inQuint|0_25);
			case Ease.InSine:
				return new EasingFunction(Easing.<Get>g__inSine|0_28);
			case Ease.OutBack:
				return new EasingFunction(Easing.<Get>g__outBack|0_2);
			case Ease.OutBounce:
				return new EasingFunction(Easing.<Get>g__outBounce|0_5);
			case Ease.OutCirc:
				return new EasingFunction(Easing.<Get>g__outCirc|0_8);
			case Ease.OutCubic:
				return new EasingFunction(Easing.<Get>g__outCubic|0_11);
			case Ease.OutElastic:
				return new EasingFunction(Easing.<Get>g__outElastic|0_14);
			case Ease.OutExpo:
				return new EasingFunction(Easing.<Get>g__outExpo|0_17);
			case Ease.OutQuad:
				return new EasingFunction(Easing.<Get>g__outQuad|0_20);
			case Ease.OutQuart:
				return new EasingFunction(Easing.<Get>g__outQuart|0_23);
			case Ease.OutQuint:
				return new EasingFunction(Easing.<Get>g__outQuint|0_26);
			case Ease.OutSine:
				return new EasingFunction(Easing.<Get>g__outSine|0_29);
			case Ease.InOutBack:
				return new EasingFunction(Easing.<Get>g__inOutBack|0_3);
			case Ease.InOutBounce:
				return new EasingFunction(Easing.<Get>g__inOutBounce|0_6);
			case Ease.InOutCirc:
				return new EasingFunction(Easing.<Get>g__inOutCirc|0_9);
			case Ease.InOutCubic:
				return new EasingFunction(Easing.<Get>g__inOutCubic|0_12);
			case Ease.InOutElastic:
				return new EasingFunction(Easing.<Get>g__inOutElastic|0_15);
			case Ease.InOutExpo:
				return new EasingFunction(Easing.<Get>g__inOutExpo|0_18);
			case Ease.InOutQuad:
				return new EasingFunction(Easing.<Get>g__inOutQuad|0_21);
			case Ease.InOutQuart:
				return new EasingFunction(Easing.<Get>g__inOutQuart|0_24);
			case Ease.InOutQuint:
				return new EasingFunction(Easing.<Get>g__inOutQuint|0_27);
			case Ease.InOutSine:
				return new EasingFunction(Easing.<Get>g__inOutSine|0_30);
			default:
				return new EasingFunction(Easing.<Get>g__linear|0_0);
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0004B7D3 File Offset: 0x000499D3
		[CompilerGenerated]
		internal static float <Get>g__linear|0_0(float t)
		{
			return t;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0004B7D6 File Offset: 0x000499D6
		[CompilerGenerated]
		internal static float <Get>g__inBack|0_1(float t)
		{
			return t * t * t - t * Mathf.Sin(t * 3.1415927f);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0004B7EC File Offset: 0x000499EC
		[CompilerGenerated]
		internal static float <Get>g__outBack|0_2(float t)
		{
			return 1f - Easing.<Get>g__inBack|0_1(1f - t);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0004B800 File Offset: 0x00049A00
		[CompilerGenerated]
		internal static float <Get>g__inOutBack|0_3(float t)
		{
			if (t >= 0.5f)
			{
				return 0.5f * Easing.<Get>g__outBack|0_2(2f * t - 1f) + 0.5f;
			}
			return 0.5f * Easing.<Get>g__inBack|0_1(2f * t);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0004B83B File Offset: 0x00049A3B
		[CompilerGenerated]
		internal static float <Get>g__inBounce|0_4(float t)
		{
			return 1f - Easing.<Get>g__outBounce|0_5(1f - t);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0004B850 File Offset: 0x00049A50
		[CompilerGenerated]
		internal static float <Get>g__outBounce|0_5(float t)
		{
			if (t < 0.36363637f)
			{
				return 121f * t * t / 16f;
			}
			if (t < 0.72727275f)
			{
				return 9.075f * t * t - 9.9f * t + 3.4f;
			}
			if (t >= 0.9f)
			{
				return 10.8f * t * t - 20.52f * t + 10.72f;
			}
			return 12.066482f * t * t - 19.635458f * t + 8.898061f;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0004B8CC File Offset: 0x00049ACC
		[CompilerGenerated]
		internal static float <Get>g__inOutBounce|0_6(float t)
		{
			if (t >= 0.5f)
			{
				return 0.5f * Easing.<Get>g__outBounce|0_5(2f * t - 1f) + 0.5f;
			}
			return 0.5f * Easing.<Get>g__inBounce|0_4(2f * t);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0004B907 File Offset: 0x00049B07
		[CompilerGenerated]
		internal static float <Get>g__inCirc|0_7(float t)
		{
			return 1f - Mathf.Sqrt(1f - t * t);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0004B91D File Offset: 0x00049B1D
		[CompilerGenerated]
		internal static float <Get>g__outCirc|0_8(float t)
		{
			return Mathf.Sqrt((2f - t) * t);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0004B930 File Offset: 0x00049B30
		[CompilerGenerated]
		internal static float <Get>g__inOutCirc|0_9(float t)
		{
			if (t >= 0.5f)
			{
				return 0.5f * (Mathf.Sqrt(-(2f * t - 3f) * (2f * t - 1f)) + 1f);
			}
			return 0.5f * (1f - Mathf.Sqrt(1f - 4f * (t * t)));
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0004B993 File Offset: 0x00049B93
		[CompilerGenerated]
		internal static float <Get>g__inCubic|0_10(float t)
		{
			return t * t * t;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0004B99A File Offset: 0x00049B9A
		[CompilerGenerated]
		internal static float <Get>g__outCubic|0_11(float t)
		{
			return Easing.<Get>g__inCubic|0_10(t - 1f) + 1f;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0004B9AE File Offset: 0x00049BAE
		[CompilerGenerated]
		internal static float <Get>g__inOutCubic|0_12(float t)
		{
			if (t >= 0.5f)
			{
				return 0.5f * Easing.<Get>g__inCubic|0_10(2f * t - 2f) + 1f;
			}
			return 4f * t * t * t;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0004B9E2 File Offset: 0x00049BE2
		[CompilerGenerated]
		internal static float <Get>g__inElastic|0_13(float t)
		{
			return Mathf.Sin(20.420353f * t) * Mathf.Pow(2f, 10f * (t - 1f));
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0004BA08 File Offset: 0x00049C08
		[CompilerGenerated]
		internal static float <Get>g__outElastic|0_14(float t)
		{
			return Mathf.Sin(-20.420353f * (t + 1f)) * Mathf.Pow(2f, -10f * t) + 1f;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0004BA34 File Offset: 0x00049C34
		[CompilerGenerated]
		internal static float <Get>g__inOutElastic|0_15(float t)
		{
			if (t >= 0.5f)
			{
				return 0.5f * (Mathf.Sin(-20.420353f * (2f * t - 1f + 1f)) * Mathf.Pow(2f, -10f * (2f * t - 1f)) + 2f);
			}
			return 0.5f * Mathf.Sin(20.420353f * (2f * t)) * Mathf.Pow(2f, 10f * (2f * t - 1f));
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0004BAC8 File Offset: 0x00049CC8
		[CompilerGenerated]
		internal static float <Get>g__inExpo|0_16(float t)
		{
			if (!Mathf.Approximately(0f, t))
			{
				return Mathf.Pow(2f, 10f * (t - 1f));
			}
			return t;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0004BAF0 File Offset: 0x00049CF0
		[CompilerGenerated]
		internal static float <Get>g__outExpo|0_17(float t)
		{
			if (!Mathf.Approximately(1f, t))
			{
				return 1f - Mathf.Pow(2f, -10f * t);
			}
			return t;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0004BB18 File Offset: 0x00049D18
		[CompilerGenerated]
		internal static float <Get>g__inOutExpo|0_18(float v)
		{
			if (Mathf.Approximately(0f, v) || Mathf.Approximately(1f, v))
			{
				return v;
			}
			if (v >= 0.5f)
			{
				return -0.5f * Mathf.Pow(2f, -20f * v + 10f) + 1f;
			}
			return 0.5f * Mathf.Pow(2f, 20f * v - 10f);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0004BB8A File Offset: 0x00049D8A
		[CompilerGenerated]
		internal static float <Get>g__inQuad|0_19(float t)
		{
			return t * t;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0004BB8F File Offset: 0x00049D8F
		[CompilerGenerated]
		internal static float <Get>g__outQuad|0_20(float t)
		{
			return -t * (t - 2f);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0004BB9B File Offset: 0x00049D9B
		[CompilerGenerated]
		internal static float <Get>g__inOutQuad|0_21(float t)
		{
			if (t >= 0.5f)
			{
				return -2f * t * t + 4f * t - 1f;
			}
			return 2f * t * t;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004BBC6 File Offset: 0x00049DC6
		[CompilerGenerated]
		internal static float <Get>g__inQuart|0_22(float t)
		{
			return t * t * t * t;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004BBD0 File Offset: 0x00049DD0
		[CompilerGenerated]
		internal static float <Get>g__outQuart|0_23(float t)
		{
			float num = t - 1f;
			return num * num * num * (1f - t) + 1f;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0004BBF8 File Offset: 0x00049DF8
		[CompilerGenerated]
		internal static float <Get>g__inOutQuart|0_24(float t)
		{
			if (t >= 0.5f)
			{
				return -8f * Easing.<Get>g__inQuart|0_22(t - 1f) + 1f;
			}
			return 8f * Easing.<Get>g__inQuart|0_22(t);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0004BC27 File Offset: 0x00049E27
		[CompilerGenerated]
		internal static float <Get>g__inQuint|0_25(float t)
		{
			return t * t * t * t * t;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0004BC32 File Offset: 0x00049E32
		[CompilerGenerated]
		internal static float <Get>g__outQuint|0_26(float t)
		{
			return Easing.<Get>g__inQuint|0_25(t - 1f) + 1f;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0004BC46 File Offset: 0x00049E46
		[CompilerGenerated]
		internal static float <Get>g__inOutQuint|0_27(float t)
		{
			if (t >= 0.5f)
			{
				return 0.5f * Easing.<Get>g__inQuint|0_25(2f * t - 2f) + 1f;
			}
			return 16f * Easing.<Get>g__inQuint|0_25(t);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0004BC7B File Offset: 0x00049E7B
		[CompilerGenerated]
		internal static float <Get>g__inSine|0_28(float t)
		{
			return Mathf.Sin((t - 1f) * 1.5707964f) + 1f;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0004BC95 File Offset: 0x00049E95
		[CompilerGenerated]
		internal static float <Get>g__outSine|0_29(float t)
		{
			return Mathf.Sin(t * 1.5707964f);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0004BCA3 File Offset: 0x00049EA3
		[CompilerGenerated]
		internal static float <Get>g__inOutSine|0_30(float t)
		{
			return 0.5f * (1f - Mathf.Cos(t * 3.1415927f));
		}
	}
}

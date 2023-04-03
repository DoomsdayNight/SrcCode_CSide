using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace NextGenSprites
{
	// Token: 0x02000046 RID: 70
	public static class ExtensionShortcuts
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000099E4 File Offset: 0x00007BE4
		public static string GetString(this ShaderFloat slot)
		{
			return ExtensionShortcuts.FloatProperties[(int)slot];
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000099ED File Offset: 0x00007BED
		public static string GetString(this ShaderVector4 slot)
		{
			return ExtensionShortcuts.Vector4Properties[(int)slot];
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000099F6 File Offset: 0x00007BF6
		public static string GetString(this ShaderTexture slot)
		{
			return ExtensionShortcuts.TextureProperties[(int)slot];
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000099FF File Offset: 0x00007BFF
		public static string GetString(this ShaderColor slot)
		{
			return ExtensionShortcuts.TintProperties[(int)slot];
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009A08 File Offset: 0x00007C08
		public static string GetString(this ShaderFeature slot)
		{
			return ExtensionShortcuts.ShaderKeywordProperties[(int)slot];
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009A11 File Offset: 0x00007C11
		public static string GetString(this ShaderFeatureRuntime slot)
		{
			return ExtensionShortcuts.ShaderRuntimeKeywordProperties[(int)slot];
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009A1A File Offset: 0x00007C1A
		public static float GetMax(this ShaderFloat slot)
		{
			return ExtensionShortcuts.MinMaxFloatProperties[(int)slot, 1];
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009A28 File Offset: 0x00007C28
		public static float GetMin(this ShaderFloat slot)
		{
			return ExtensionShortcuts.MinMaxFloatProperties[(int)slot, 0];
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009A38 File Offset: 0x00007C38
		public static void ToggleShadowCasting(this GameObject go, bool toggle)
		{
			ShadowCastingMode shadowCastingMode = toggle ? ShadowCastingMode.On : ShadowCastingMode.Off;
			go.GetComponent<SpriteRenderer>().shadowCastingMode = shadowCastingMode;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009A5C File Offset: 0x00007C5C
		public static void CopyToPropertyBlock(this MaterialPropertyBlock mBlock, Material mat)
		{
			string[] names = Enum.GetNames(typeof(ShaderFloat));
			for (int i = 0; i < names.Length; i++)
			{
				string name = ExtensionShortcuts.FloatProperties[i];
				if (mat.HasProperty(name))
				{
					mBlock.SetFloat(name, mat.GetFloat(name));
				}
			}
			string[] names2 = Enum.GetNames(typeof(ShaderColor));
			for (int j = 0; j < names2.Length; j++)
			{
				string name2 = ExtensionShortcuts.TintProperties[j];
				if (mat.HasProperty(name2))
				{
					mBlock.SetColor(name2, mat.GetColor(name2));
				}
			}
			string[] names3 = Enum.GetNames(typeof(ShaderTexture));
			for (int k = 1; k < names3.Length; k++)
			{
				string name3 = ExtensionShortcuts.TextureProperties[k];
				if (mat.HasProperty(name3))
				{
					Texture texture = mat.GetTexture(name3);
					if (texture)
					{
						mBlock.SetTexture(name3, texture);
					}
				}
			}
		}

		// Token: 0x0400019F RID: 415
		private static readonly string[] FloatProperties = new string[]
		{
			"_CurvatureDepth",
			"_Specular",
			"_ReflectionStrength",
			"_ReflectionBlur",
			"_ReflectionScrollingX",
			"_ReflectionScrollingY",
			"_EmissionStrength",
			"_EmissionBlendAnimation1",
			"_EmissionPulseSpeed1",
			"_EmissionStrength2",
			"_EmissionBlendAnimation2",
			"_EmissionPulseSpeed2",
			"_EmissionStrength3",
			"_EmissionBlendAnimation3",
			"_EmissionPulseSpeed3",
			"_TransmissionDensity",
			"_DissolveBlend",
			"_DissolveBorderWidth",
			"_DissolveGlowStrength",
			"_RefractionStrength",
			"_FlowIntensity",
			"_FlowSpeed",
			"_Layer0ScrollingX",
			"_Layer0ScrollingY",
			"_Layer1Opacity",
			"_Layer1ScrollingX",
			"_Layer1ScrollingY",
			"_Layer2Opacity",
			"_Layer2ScrollingX",
			"_Layer2ScrollingY",
			"_Layer3Opacity",
			"_Layer3ScrollingX",
			"_Layer3ScrollingY",
			"_Layer0AutoScrollSpeed",
			"m_fOutlineWide",
			"_SpriteBlending"
		};

		// Token: 0x040001A0 RID: 416
		private static readonly string[] Vector4Properties = new string[]
		{
			"_HSBC"
		};

		// Token: 0x040001A1 RID: 417
		private static readonly float[,] MinMaxFloatProperties = new float[,]
		{
			{
				-1f,
				1f
			},
			{
				0f,
				0.7f
			},
			{
				0f,
				1f
			},
			{
				0f,
				9f
			},
			{
				0f,
				5f
			},
			{
				0f,
				5f
			},
			{
				0f,
				5f
			},
			{
				0f,
				1f
			},
			{
				0f,
				10f
			},
			{
				0f,
				5f
			},
			{
				0f,
				1f
			},
			{
				0f,
				10f
			},
			{
				0f,
				5f
			},
			{
				0f,
				1f
			},
			{
				0f,
				10f
			},
			{
				0f,
				1f
			},
			{
				0f,
				1f
			},
			{
				0f,
				100f
			},
			{
				0f,
				5f
			},
			{
				-1f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				-10f,
				10f
			},
			{
				-1f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				0f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				0f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				0f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				-1f,
				1f
			},
			{
				0f,
				2f
			},
			{
				0f,
				5f
			}
		};

		// Token: 0x040001A2 RID: 418
		private static readonly string[] TextureProperties = new string[]
		{
			"_MainTex",
			"_Layer1",
			"_Layer2",
			"_Layer3",
			"_StencilMask",
			"_BumpMap",
			"_Illum",
			"_ReflectionTex",
			"_ReflectionMask",
			"_TransmissionTex",
			"_DissolveTex",
			"_RefractionNormal",
			"_FlowMap",
			"_RenderTexture"
		};

		// Token: 0x040001A3 RID: 419
		private static readonly string[] TintProperties = new string[]
		{
			"_Color",
			"_Layer1Color",
			"_Layer2Color",
			"_Layer3Color",
			"_SpecColor",
			"_EmissionTint",
			"_EmissionTint2",
			"_EmissionTint3",
			"_DissolveGlowColor",
			"m_v4OutlineColor",
			"m_AmbientColor"
		};

		// Token: 0x040001A4 RID: 420
		private static readonly string[] ShaderKeywordProperties = new string[]
		{
			"SPRITE_MULTILAYER_ON",
			"SPRITE_SCROLLING_ON",
			"SPRITE_STENCIL_ON",
			"CURVATURE_ON",
			"REFLECTION_ON",
			"EMISSION_ON",
			"EMISSION_PULSE_ON",
			"TRANSMISSION_ON",
			"DISSOLVE_ON",
			"DOUBLESIDED_ON",
			"PIXELSNAP_ON",
			"AUTOSCROLL_ON",
			"HSB_TINT_ON",
			"RENDER_TEXTURE_ON"
		};

		// Token: 0x040001A5 RID: 421
		private static readonly string[] ShaderRuntimeKeywordProperties = new string[]
		{
			"CURVATURE_ON",
			"REFLECTION_ON",
			"EMISSION_ON",
			"DISSOLVE_ON"
		};
	}
}

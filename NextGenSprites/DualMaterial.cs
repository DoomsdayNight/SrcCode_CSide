using System;
using System.Collections.Generic;
using UnityEngine;

namespace NextGenSprites
{
	// Token: 0x0200003F RID: 63
	public class DualMaterial
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000096C1 File Offset: 0x000078C1
		public Material FusedMaterial
		{
			get
			{
				return this._mainMaterial;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000096C9 File Offset: 0x000078C9
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000096D1 File Offset: 0x000078D1
		public float Lerp
		{
			get
			{
				return this._lerpAmount;
			}
			set
			{
				this._lerpAmount = Mathf.Clamp01(value);
				this.LerpMaterial();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000096E8 File Offset: 0x000078E8
		public DualMaterial(Material firstMaterial, Material secondMaterial, string materialName = "New Dual Material")
		{
			if (firstMaterial && secondMaterial)
			{
				if (string.CompareOrdinal(firstMaterial.shader.name, secondMaterial.shader.name) != 0)
				{
					Debug.LogError("Invalid Materials. Both must use the same NextGenSprite shader");
					this._mainMaterial = null;
				}
				this._mainMaterial = new Material(Shader.Find(firstMaterial.shader.name));
				this._mainMaterial.CopyPropertiesFromMaterial(firstMaterial);
				this._mainMaterial.name = materialName;
				this.SetKeywords(firstMaterial);
				this.BuildProperties(firstMaterial, secondMaterial);
				return;
			}
			Debug.LogError("One or both of the provided Materials are null");
			this._mainMaterial = null;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009790 File Offset: 0x00007990
		private void LerpMaterial()
		{
			foreach (DualMaterial.FloatPropertyRange floatPropertyRange in this.FloatPropertyValues)
			{
				float value = Mathf.Lerp(floatPropertyRange.ValueMain, floatPropertyRange.ValueSecond, this._lerpAmount);
				this._mainMaterial.SetFloat(floatPropertyRange.Property.GetString(), value);
			}
			foreach (DualMaterial.ColorPropertyRange colorPropertyRange in this.ColorPropertyValues)
			{
				Color value2 = Color.Lerp(colorPropertyRange.ValueMain, colorPropertyRange.ValueSecond, this._lerpAmount);
				this._mainMaterial.SetColor(colorPropertyRange.Property.GetString(), value2);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009838 File Offset: 0x00007A38
		private void BuildProperties(Material firstMat, Material secondMat)
		{
			List<DualMaterial.FloatPropertyRange> list = new List<DualMaterial.FloatPropertyRange>();
			List<DualMaterial.ColorPropertyRange> list2 = new List<DualMaterial.ColorPropertyRange>();
			foreach (string value in Enum.GetNames(typeof(ShaderFloat)))
			{
				ShaderFloat shaderFloat = (ShaderFloat)Enum.Parse(typeof(ShaderFloat), value);
				if (firstMat.HasProperty(shaderFloat.GetString()))
				{
					list.Add(new DualMaterial.FloatPropertyRange
					{
						Property = shaderFloat,
						ValueMain = firstMat.GetFloat(shaderFloat.GetString()),
						ValueSecond = secondMat.GetFloat(shaderFloat.GetString())
					});
				}
			}
			foreach (string value2 in Enum.GetNames(typeof(ShaderColor)))
			{
				ShaderColor shaderColor = (ShaderColor)Enum.Parse(typeof(ShaderColor), value2);
				if (firstMat.HasProperty(shaderColor.GetString()))
				{
					list2.Add(new DualMaterial.ColorPropertyRange
					{
						Property = shaderColor,
						ValueMain = firstMat.GetColor(shaderColor.GetString()),
						ValueSecond = secondMat.GetColor(shaderColor.GetString())
					});
				}
			}
			this.FloatPropertyValues = list.ToArray();
			this.ColorPropertyValues = list2.ToArray();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009980 File Offset: 0x00007B80
		private void SetKeywords(Material sourceMaterial)
		{
			foreach (string value in Enum.GetNames(typeof(ShaderFeature)))
			{
				ShaderFeature slot = (ShaderFeature)Enum.Parse(typeof(ShaderFeature), value);
				if (sourceMaterial.IsKeywordEnabled(slot.GetString()))
				{
					this._mainMaterial.EnableKeyword(slot.GetString());
				}
			}
		}

		// Token: 0x04000145 RID: 325
		private float _lerpAmount;

		// Token: 0x04000146 RID: 326
		private Material _mainMaterial;

		// Token: 0x04000147 RID: 327
		private DualMaterial.FloatPropertyRange[] FloatPropertyValues;

		// Token: 0x04000148 RID: 328
		private DualMaterial.ColorPropertyRange[] ColorPropertyValues;

		// Token: 0x020010F6 RID: 4342
		private class FloatPropertyRange
		{
			// Token: 0x1700175A RID: 5978
			// (get) Token: 0x06009EC0 RID: 40640 RVA: 0x0033B92D File Offset: 0x00339B2D
			// (set) Token: 0x06009EC1 RID: 40641 RVA: 0x0033B935 File Offset: 0x00339B35
			public ShaderFloat Property { get; set; }

			// Token: 0x1700175B RID: 5979
			// (get) Token: 0x06009EC2 RID: 40642 RVA: 0x0033B93E File Offset: 0x00339B3E
			// (set) Token: 0x06009EC3 RID: 40643 RVA: 0x0033B946 File Offset: 0x00339B46
			public float ValueMain { get; set; }

			// Token: 0x1700175C RID: 5980
			// (get) Token: 0x06009EC4 RID: 40644 RVA: 0x0033B94F File Offset: 0x00339B4F
			// (set) Token: 0x06009EC5 RID: 40645 RVA: 0x0033B957 File Offset: 0x00339B57
			public float ValueSecond { get; set; }
		}

		// Token: 0x020010F7 RID: 4343
		private class ColorPropertyRange
		{
			// Token: 0x1700175D RID: 5981
			// (get) Token: 0x06009EC7 RID: 40647 RVA: 0x0033B968 File Offset: 0x00339B68
			// (set) Token: 0x06009EC8 RID: 40648 RVA: 0x0033B970 File Offset: 0x00339B70
			public ShaderColor Property { get; set; }

			// Token: 0x1700175E RID: 5982
			// (get) Token: 0x06009EC9 RID: 40649 RVA: 0x0033B979 File Offset: 0x00339B79
			// (set) Token: 0x06009ECA RID: 40650 RVA: 0x0033B981 File Offset: 0x00339B81
			public Color ValueMain { get; set; }

			// Token: 0x1700175F RID: 5983
			// (get) Token: 0x06009ECB RID: 40651 RVA: 0x0033B98A File Offset: 0x00339B8A
			// (set) Token: 0x06009ECC RID: 40652 RVA: 0x0033B992 File Offset: 0x00339B92
			public Color ValueSecond { get; set; }
		}
	}
}

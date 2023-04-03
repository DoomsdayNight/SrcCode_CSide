using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E2 RID: 738
	[AddComponentMenu("UI/Effects/Extensions/UIImageCrop")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class UIImageCrop : MonoBehaviour
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x00036D5F File Offset: 0x00034F5F
		private void Start()
		{
			this.SetMaterial();
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00036D68 File Offset: 0x00034F68
		public void SetMaterial()
		{
			this.mGraphic = base.GetComponent<MaskableGraphic>();
			this.XCropProperty = Shader.PropertyToID("_XCrop");
			this.YCropProperty = Shader.PropertyToID("_YCrop");
			if (this.mGraphic != null)
			{
				if (this.mGraphic.material == null || this.mGraphic.material.name == "Default UI Material")
				{
					this.mGraphic.material = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/UI Image Crop"));
				}
				this.mat = this.mGraphic.material;
				return;
			}
			Debug.LogError("Please attach component to a Graphical UI component");
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00036E14 File Offset: 0x00035014
		public void OnValidate()
		{
			this.SetMaterial();
			this.SetXCrop(this.XCrop);
			this.SetYCrop(this.YCrop);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00036E34 File Offset: 0x00035034
		public void SetXCrop(float xcrop)
		{
			this.XCrop = Mathf.Clamp01(xcrop);
			this.mat.SetFloat(this.XCropProperty, this.XCrop);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00036E59 File Offset: 0x00035059
		public void SetYCrop(float ycrop)
		{
			this.YCrop = Mathf.Clamp01(ycrop);
			this.mat.SetFloat(this.YCropProperty, this.YCrop);
		}

		// Token: 0x04000B3A RID: 2874
		private MaskableGraphic mGraphic;

		// Token: 0x04000B3B RID: 2875
		private Material mat;

		// Token: 0x04000B3C RID: 2876
		private int XCropProperty;

		// Token: 0x04000B3D RID: 2877
		private int YCropProperty;

		// Token: 0x04000B3E RID: 2878
		public float XCrop;

		// Token: 0x04000B3F RID: 2879
		public float YCrop;
	}
}

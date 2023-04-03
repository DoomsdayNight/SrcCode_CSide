using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E1 RID: 737
	[AddComponentMenu("UI/Effects/Extensions/UIAdditiveEffect")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class UIAdditiveEffect : MonoBehaviour
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x00036CCC File Offset: 0x00034ECC
		private void Start()
		{
			this.SetMaterial();
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00036CD4 File Offset: 0x00034ED4
		public void SetMaterial()
		{
			this.mGraphic = base.GetComponent<MaskableGraphic>();
			if (this.mGraphic != null)
			{
				if (this.mGraphic.material == null || this.mGraphic.material.name == "Default UI Material")
				{
					this.mGraphic.material = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/UIAdditive"));
					return;
				}
			}
			else
			{
				Debug.LogError("Please attach component to a Graphical UI component");
			}
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00036D4F File Offset: 0x00034F4F
		public void OnValidate()
		{
			this.SetMaterial();
		}

		// Token: 0x04000B39 RID: 2873
		private MaskableGraphic mGraphic;
	}
}

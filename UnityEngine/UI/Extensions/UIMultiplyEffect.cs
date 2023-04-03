using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E4 RID: 740
	[AddComponentMenu("UI/Effects/Extensions/UIMultiplyEffect")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class UIMultiplyEffect : MonoBehaviour
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x00036F1B File Offset: 0x0003511B
		private void Start()
		{
			this.SetMaterial();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00036F24 File Offset: 0x00035124
		public void SetMaterial()
		{
			this.mGraphic = base.GetComponent<MaskableGraphic>();
			if (this.mGraphic != null)
			{
				if (this.mGraphic.material == null || this.mGraphic.material.name == "Default UI Material")
				{
					this.mGraphic.material = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/UIMultiply"));
					return;
				}
			}
			else
			{
				Debug.LogError("Please attach component to a Graphical UI component");
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00036F9F File Offset: 0x0003519F
		public void OnValidate()
		{
			this.SetMaterial();
		}

		// Token: 0x04000B41 RID: 2881
		private MaskableGraphic mGraphic;
	}
}

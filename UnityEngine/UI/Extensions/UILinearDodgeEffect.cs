using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E3 RID: 739
	[AddComponentMenu("UI/Effects/Extensions/UILinearDodgeEffect")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class UILinearDodgeEffect : MonoBehaviour
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x00036E86 File Offset: 0x00035086
		private void Start()
		{
			this.SetMaterial();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00036E90 File Offset: 0x00035090
		public void SetMaterial()
		{
			this.mGraphic = base.GetComponent<MaskableGraphic>();
			if (this.mGraphic != null)
			{
				if (this.mGraphic.material == null || this.mGraphic.material.name == "Default UI Material")
				{
					this.mGraphic.material = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/UILinearDodge"));
					return;
				}
			}
			else
			{
				Debug.LogError("Please attach component to a Graphical UI component");
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00036F0B File Offset: 0x0003510B
		public void OnValidate()
		{
			this.SetMaterial();
		}

		// Token: 0x04000B40 RID: 2880
		private MaskableGraphic mGraphic;
	}
}

using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E5 RID: 741
	[AddComponentMenu("UI/Effects/Extensions/UIScreenEffect")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class UIScreenEffect : MonoBehaviour
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x00036FAF File Offset: 0x000351AF
		private void Start()
		{
			this.SetMaterial();
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00036FB8 File Offset: 0x000351B8
		public void SetMaterial()
		{
			this.mGraphic = base.GetComponent<MaskableGraphic>();
			if (this.mGraphic != null)
			{
				if (this.mGraphic.material == null || this.mGraphic.material.name == "Default UI Material")
				{
					this.mGraphic.material = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/UIScreen"));
					return;
				}
			}
			else
			{
				Debug.LogError("Please attach component to a Graphical UI component");
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00037033 File Offset: 0x00035233
		public void OnValidate()
		{
			this.SetMaterial();
		}

		// Token: 0x04000B42 RID: 2882
		private MaskableGraphic mGraphic;
	}
}

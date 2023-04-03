using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002E6 RID: 742
	[AddComponentMenu("UI/Effects/Extensions/UISoftAdditiveEffect")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class UISoftAdditiveEffect : MonoBehaviour
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x00037043 File Offset: 0x00035243
		private void Start()
		{
			this.SetMaterial();
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003704C File Offset: 0x0003524C
		public void SetMaterial()
		{
			this.mGraphic = base.GetComponent<MaskableGraphic>();
			if (this.mGraphic != null)
			{
				if (this.mGraphic.material == null || this.mGraphic.material.name == "Default UI Material")
				{
					this.mGraphic.material = new Material(ShaderLibrary.GetShaderInstance("UI Extensions/UISoftAdditive"));
					return;
				}
			}
			else
			{
				Debug.LogError("Please attach component to a Graphical UI component");
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000370C7 File Offset: 0x000352C7
		public void OnValidate()
		{
			this.SetMaterial();
		}

		// Token: 0x04000B43 RID: 2883
		private MaskableGraphic mGraphic;
	}
}

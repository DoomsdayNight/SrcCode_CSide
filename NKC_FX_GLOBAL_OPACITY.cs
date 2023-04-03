using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002A RID: 42
public class NKC_FX_GLOBAL_OPACITY : MonoBehaviour
{
	// Token: 0x06000146 RID: 326 RVA: 0x000067BC File Offset: 0x000049BC
	private void Start()
	{
		float globalFloat = Shader.GetGlobalFloat("_FxGlobalTransparency");
		Slider component = base.GetComponent<Slider>();
		if (component != null)
		{
			component.value = globalFloat;
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x000067EB File Offset: 0x000049EB
	private void OnDestroy()
	{
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000067ED File Offset: 0x000049ED
	public static void Execute(float _factor)
	{
		Shader.SetGlobalFloat("_FxGlobalTransparency", _factor);
	}
}

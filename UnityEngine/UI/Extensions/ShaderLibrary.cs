using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000342 RID: 834
	public static class ShaderLibrary
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x000495CC File Offset: 0x000477CC
		public static Shader GetShaderInstance(string shaderName)
		{
			if (ShaderLibrary.shaderInstances.ContainsKey(shaderName))
			{
				return ShaderLibrary.shaderInstances[shaderName];
			}
			Shader shader = Shader.Find(shaderName);
			if (shader != null)
			{
				ShaderLibrary.shaderInstances.Add(shaderName, shader);
			}
			return shader;
		}

		// Token: 0x04000D88 RID: 3464
		public static Dictionary<string, Shader> shaderInstances = new Dictionary<string, Shader>();

		// Token: 0x04000D89 RID: 3465
		public static Shader[] preLoadedShaders;
	}
}

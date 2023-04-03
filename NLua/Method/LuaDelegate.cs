using System;

namespace NLua.Method
{
	// Token: 0x02000071 RID: 113
	public class LuaDelegate
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x00013E5C File Offset: 0x0001205C
		public LuaDelegate()
		{
			this.Function = null;
			this.ReturnTypes = null;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00013E74 File Offset: 0x00012074
		public object CallFunction(object[] args, object[] inArgs, int[] outArgs)
		{
			object[] array = this.Function.Call(inArgs, this.ReturnTypes);
			object result;
			int num;
			if (this.ReturnTypes[0] == typeof(void))
			{
				result = null;
				num = 0;
			}
			else
			{
				result = array[0];
				num = 1;
			}
			for (int i = 0; i < outArgs.Length; i++)
			{
				args[outArgs[i]] = array[num];
				num++;
			}
			return result;
		}

		// Token: 0x04000245 RID: 581
		public LuaFunction Function;

		// Token: 0x04000246 RID: 582
		public Type[] ReturnTypes;
	}
}

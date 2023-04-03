using System;
using KeraLua;

namespace NLua
{
	// Token: 0x0200005F RID: 95
	internal class DelegateGenerator
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000E2C6 File Offset: 0x0000C4C6
		public DelegateGenerator(ObjectTranslator objectTranslator, Type type)
		{
			this._translator = objectTranslator;
			this._delegateType = type;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		public object ExtractGenerated(Lua luaState, int stackPos)
		{
			return CodeGeneration.Instance.GetDelegate(this._delegateType, this._translator.GetFunction(luaState, stackPos));
		}

		// Token: 0x04000200 RID: 512
		private readonly ObjectTranslator _translator;

		// Token: 0x04000201 RID: 513
		private readonly Type _delegateType;
	}
}

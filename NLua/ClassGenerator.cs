using System;
using KeraLua;

namespace NLua
{
	// Token: 0x0200005D RID: 93
	internal class ClassGenerator
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000CE23 File Offset: 0x0000B023
		public ClassGenerator(ObjectTranslator objTranslator, Type typeClass)
		{
			this._translator = objTranslator;
			this._klass = typeClass;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000CE39 File Offset: 0x0000B039
		public object ExtractGenerated(Lua luaState, int stackPos)
		{
			return CodeGeneration.Instance.GetClassInstance(this._klass, this._translator.GetTable(luaState, stackPos));
		}

		// Token: 0x040001F4 RID: 500
		private readonly ObjectTranslator _translator;

		// Token: 0x040001F5 RID: 501
		private readonly Type _klass;
	}
}

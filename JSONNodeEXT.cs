using System;
using SimpleJSON;

namespace NKM
{
	// Token: 0x02000424 RID: 1060
	internal static class JSONNodeEXT
	{
		// Token: 0x06001C9D RID: 7325 RVA: 0x00084FD8 File Offset: 0x000831D8
		public static T AsEnum<T>(this JSONNode self) where T : struct, Enum
		{
			T result;
			Enum.TryParse<T>(self.Value, out result);
			return result;
		}
	}
}

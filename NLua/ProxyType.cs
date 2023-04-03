using System;
using System.Reflection;

namespace NLua
{
	// Token: 0x0200006E RID: 110
	public class ProxyType
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x00013C75 File Offset: 0x00011E75
		public ProxyType(Type proxy)
		{
			this._proxy = proxy;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00013C84 File Offset: 0x00011E84
		public override string ToString()
		{
			string str = "ProxyType(";
			Type underlyingSystemType = this.UnderlyingSystemType;
			return str + ((underlyingSystemType != null) ? underlyingSystemType.ToString() : null) + ")";
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00013CA7 File Offset: 0x00011EA7
		public Type UnderlyingSystemType
		{
			get
			{
				return this._proxy;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public override bool Equals(object obj)
		{
			if (obj is Type)
			{
				return this._proxy == (Type)obj;
			}
			if (obj is ProxyType)
			{
				return this._proxy == ((ProxyType)obj).UnderlyingSystemType;
			}
			return this._proxy.Equals(obj);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00013D02 File Offset: 0x00011F02
		public override int GetHashCode()
		{
			return this._proxy.GetHashCode();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00013D0F File Offset: 0x00011F0F
		public MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this._proxy.GetMember(name, bindingAttr);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00013D1E File Offset: 0x00011F1E
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Type[] signature)
		{
			return this._proxy.GetMethod(name, bindingAttr, null, signature, null);
		}

		// Token: 0x04000243 RID: 579
		private readonly Type _proxy;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using Cs.Protocol;

namespace Cs.Engine.Network
{
	// Token: 0x020010AC RID: 4268
	internal sealed class PacketHandler
	{
		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06009C3E RID: 39998 RVA: 0x0033518C File Offset: 0x0033338C
		public ushort PacketId
		{
			get
			{
				return this.packetId_;
			}
		}

		// Token: 0x06009C3F RID: 39999 RVA: 0x00335194 File Offset: 0x00333394
		public static IEnumerable<PacketHandler> Extract(Type containerType)
		{
			MethodInfo[] array = containerType.GetMethods();
			int i = 0;
			while (i < array.Length)
			{
				MethodInfo methodInfo = array[i];
				if (containerType == typeof(Connection))
				{
					if (!methodInfo.IsStatic)
					{
						goto IL_71;
					}
				}
				else if (methodInfo.IsStatic)
				{
					goto IL_71;
				}
				IL_E9:
				i++;
				continue;
				IL_71:
				if (methodInfo.Name != "OnRecv" || methodInfo.ReturnParameter.ParameterType != typeof(void))
				{
					goto IL_E9;
				}
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length != 1)
				{
					goto IL_E9;
				}
				Type parameterType = parameters[0].ParameterType;
				ushort id = PacketController.Instance.GetId(parameterType);
				if (id != 65535)
				{
					yield return new PacketHandler(methodInfo, id);
					goto IL_E9;
				}
				goto IL_E9;
			}
			array = null;
			yield break;
		}

		// Token: 0x06009C40 RID: 40000 RVA: 0x003351A4 File Offset: 0x003333A4
		public void Execute(ISerializable message, Connection connection)
		{
			object obj = this.methodInfo_.IsStatic ? null : connection;
			this.methodInfo_.Invoke(obj, new object[]
			{
				message
			});
		}

		// Token: 0x06009C41 RID: 40001 RVA: 0x003351DA File Offset: 0x003333DA
		private PacketHandler(MethodInfo methodInfo, ushort packetId)
		{
			this.methodInfo_ = methodInfo;
			this.packetId_ = packetId;
		}

		// Token: 0x04009055 RID: 36949
		private readonly MethodInfo methodInfo_;

		// Token: 0x04009056 RID: 36950
		private readonly ushort packetId_;
	}
}

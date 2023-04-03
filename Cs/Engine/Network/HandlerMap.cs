using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Protocol;
using Protocol;

namespace Cs.Engine.Network
{
	// Token: 0x020010AB RID: 4267
	internal sealed class HandlerMap
	{
		// Token: 0x06009C3B RID: 39995 RVA: 0x00335078 File Offset: 0x00333278
		public void RegisterHandler(Type containerType)
		{
			foreach (PacketHandler packetHandler in PacketHandler.Extract(containerType))
			{
				this.handlers_.Add(packetHandler.PacketId, packetHandler);
			}
			if (this.handlers_.Count == 0)
			{
				throw new Exception("No packet handlers registered.");
			}
		}

		// Token: 0x06009C3C RID: 39996 RVA: 0x003350E8 File Offset: 0x003332E8
		public void Process(ISerializable message, ushort packetId, Connection connection)
		{
			PacketHandler packetHandler;
			if (!this.handlers_.TryGetValue(packetId, out packetHandler))
			{
				Log.Error("packet handler not found. packetId:" + PacketController.Instance.GetIdStr(packetId) + ", Connection Type:" + connection.ServerType, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/HandlerMap.cs", 35);
				return;
			}
			if (packetId != 822 && packetId != 601)
			{
				string str = "<color=#00FF00FF>";
				ClientPacketId clientPacketId = (ClientPacketId)packetId;
				Log.Info(str + clientPacketId.ToString() + "</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/HandlerMap.cs", 44);
			}
			packetHandler.Execute(message, connection);
		}

		// Token: 0x04009054 RID: 36948
		private readonly Dictionary<ushort, PacketHandler> handlers_ = new Dictionary<ushort, PacketHandler>(300);
	}
}

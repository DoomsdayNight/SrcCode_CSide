using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA1 RID: 3233
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_AUTO_REQ)]
	public sealed class NKMPacket_WARFARE_GAME_AUTO_REQ : ISerializable
	{
		// Token: 0x0600943F RID: 37951 RVA: 0x00328667 File Offset: 0x00326867
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isAuto);
			stream.PutOrGet(ref this.isAutoRepair);
		}

		// Token: 0x0400858D RID: 34189
		public bool isAuto;

		// Token: 0x0400858E RID: 34190
		public bool isAutoRepair;
	}
}

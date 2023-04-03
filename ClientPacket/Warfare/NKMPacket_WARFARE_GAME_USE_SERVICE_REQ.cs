using System;
using Cs.Protocol;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA3 RID: 3235
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_USE_SERVICE_REQ)]
	public sealed class NKMPacket_WARFARE_GAME_USE_SERVICE_REQ : ISerializable
	{
		// Token: 0x06009443 RID: 37955 RVA: 0x003286B7 File Offset: 0x003268B7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.warfareGameUnitUID);
			stream.PutOrGetEnum<NKM_WARFARE_SERVICE_TYPE>(ref this.serviceType);
		}

		// Token: 0x04008592 RID: 34194
		public int warfareGameUnitUID;

		// Token: 0x04008593 RID: 34195
		public NKM_WARFARE_SERVICE_TYPE serviceType;
	}
}

using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C8E RID: 3214
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_EVENT_CANCEL_ACK)]
	public sealed class NKMPacket_WORLDMAP_EVENT_CANCEL_ACK : ISerializable
	{
		// Token: 0x0600941B RID: 37915 RVA: 0x003280B9 File Offset: 0x003262B9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
		}

		// Token: 0x04008531 RID: 34097
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008532 RID: 34098
		public int cityID;
	}
}

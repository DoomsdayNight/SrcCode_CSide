using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D44 RID: 3396
	[PacketId(ClientPacketId.kNKMPacket_UI_SCEN_CHANGED_REQ)]
	public sealed class NKMPacket_UI_SCEN_CHANGED_REQ : ISerializable
	{
		// Token: 0x06009585 RID: 38277 RVA: 0x0032A46E File Offset: 0x0032866E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_SCEN_ID>(ref this.scenID);
		}

		// Token: 0x04008739 RID: 34617
		public NKM_SCEN_ID scenID;
	}
}

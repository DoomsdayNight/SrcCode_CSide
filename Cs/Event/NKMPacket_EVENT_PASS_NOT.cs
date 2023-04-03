using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F7F RID: 3967
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_NOT)]
	public sealed class NKMPacket_EVENT_PASS_NOT : ISerializable
	{
		// Token: 0x060099DA RID: 39386 RVA: 0x003309B9 File Offset: 0x0032EBB9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventPassId);
		}

		// Token: 0x04008CFA RID: 36090
		public int eventPassId;
	}
}

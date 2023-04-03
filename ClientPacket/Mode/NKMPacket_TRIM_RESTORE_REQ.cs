using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E5A RID: 3674
	[PacketId(ClientPacketId.kNKMPacket_TRIM_RESTORE_REQ)]
	public sealed class NKMPacket_TRIM_RESTORE_REQ : ISerializable
	{
		// Token: 0x060097A4 RID: 38820 RVA: 0x0032D3E6 File Offset: 0x0032B5E6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.trimIntervalId);
		}

		// Token: 0x040089D1 RID: 35281
		public int trimIntervalId;
	}
}

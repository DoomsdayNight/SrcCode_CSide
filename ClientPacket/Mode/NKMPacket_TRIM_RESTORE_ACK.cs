using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E5B RID: 3675
	[PacketId(ClientPacketId.kNKMPacket_TRIM_RESTORE_ACK)]
	public sealed class NKMPacket_TRIM_RESTORE_ACK : ISerializable
	{
		// Token: 0x060097A6 RID: 38822 RVA: 0x0032D3FC File Offset: 0x0032B5FC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x040089D2 RID: 35282
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089D3 RID: 35283
		public NKMItemMiscData costItemData;
	}
}

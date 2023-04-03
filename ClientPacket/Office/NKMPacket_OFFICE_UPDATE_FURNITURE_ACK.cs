using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DFD RID: 3581
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_UPDATE_FURNITURE_ACK)]
	public sealed class NKMPacket_OFFICE_UPDATE_FURNITURE_ACK : ISerializable
	{
		// Token: 0x060096EE RID: 38638 RVA: 0x0032C469 File Offset: 0x0032A669
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMOfficeFurniture>(ref this.furniture);
		}

		// Token: 0x040088FF RID: 35071
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008900 RID: 35072
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x04008901 RID: 35073
		public NKMOfficeFurniture furniture = new NKMOfficeFurniture();
	}
}

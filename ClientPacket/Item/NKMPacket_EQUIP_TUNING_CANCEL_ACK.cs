using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB6 RID: 3766
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_CANCEL_ACK)]
	public sealed class NKMPacket_EQUIP_TUNING_CANCEL_ACK : ISerializable
	{
		// Token: 0x06009858 RID: 39000 RVA: 0x0032E47F File Offset: 0x0032C67F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipTuningCandidate>(ref this.equipTuningCandidate);
		}

		// Token: 0x04008AB7 RID: 35511
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008AB8 RID: 35512
		public NKMEquipTuningCandidate equipTuningCandidate = new NKMEquipTuningCandidate();
	}
}

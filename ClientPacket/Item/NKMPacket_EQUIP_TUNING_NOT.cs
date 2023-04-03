using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB7 RID: 3767
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_NOT)]
	public sealed class NKMPacket_EQUIP_TUNING_NOT : ISerializable
	{
		// Token: 0x0600985A RID: 39002 RVA: 0x0032E4AC File Offset: 0x0032C6AC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipTuningCandidate>(ref this.equipTuningCandidate);
		}

		// Token: 0x04008AB9 RID: 35513
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008ABA RID: 35514
		public NKMEquipTuningCandidate equipTuningCandidate = new NKMEquipTuningCandidate();
	}
}

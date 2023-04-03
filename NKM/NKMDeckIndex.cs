using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003DA RID: 986
	public struct NKMDeckIndex : ISerializable, IEquatable<NKMDeckIndex>
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x0006F7C5 File Offset: 0x0006D9C5
		public NKMDeckIndex(NKM_DECK_TYPE eDeckType)
		{
			this.m_eDeckType = eDeckType;
			this.m_iIndex = 0;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0006F7D5 File Offset: 0x0006D9D5
		public NKMDeckIndex(NKM_DECK_TYPE eDeckType, int Index)
		{
			this.m_eDeckType = ((Index < 0) ? NKM_DECK_TYPE.NDT_NONE : eDeckType);
			this.m_iIndex = (byte)Index;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0006F7ED File Offset: 0x0006D9ED
		public bool Compare(NKMDeckIndex rhs)
		{
			return this.m_eDeckType == rhs.m_eDeckType && this.m_iIndex == rhs.m_iIndex;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0006F80D File Offset: 0x0006DA0D
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_DECK_TYPE>(ref this.m_eDeckType);
			stream.PutOrGet(ref this.m_iIndex);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0006F827 File Offset: 0x0006DA27
		public override string ToString()
		{
			return string.Format("DeckIndex {0} {1}", this.m_eDeckType.ToString(), this.m_iIndex);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0006F84F File Offset: 0x0006DA4F
		public static bool operator ==(NKMDeckIndex lhs, NKMDeckIndex rhs)
		{
			return lhs.m_eDeckType == rhs.m_eDeckType && lhs.m_iIndex == rhs.m_iIndex;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0006F86F File Offset: 0x0006DA6F
		public static bool operator !=(NKMDeckIndex lhs, NKMDeckIndex rhs)
		{
			return lhs.m_eDeckType != rhs.m_eDeckType || lhs.m_iIndex != rhs.m_iIndex;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0006F894 File Offset: 0x0006DA94
		public override bool Equals(object obj)
		{
			if (!(obj is NKMDeckIndex))
			{
				return false;
			}
			NKMDeckIndex nkmdeckIndex = (NKMDeckIndex)obj;
			return this.m_eDeckType == nkmdeckIndex.m_eDeckType && this.m_iIndex == nkmdeckIndex.m_iIndex;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0006F8D0 File Offset: 0x0006DAD0
		public override int GetHashCode()
		{
			return new ValueTuple<NKM_DECK_TYPE, byte>(this.m_eDeckType, this.m_iIndex).GetHashCode();
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0006F8FC File Offset: 0x0006DAFC
		public bool Equals(NKMDeckIndex other)
		{
			return this.m_eDeckType == other.m_eDeckType && this.m_iIndex == other.m_iIndex;
		}

		// Token: 0x0400130C RID: 4876
		public static NKMDeckIndex None = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE);

		// Token: 0x0400130D RID: 4877
		public NKM_DECK_TYPE m_eDeckType;

		// Token: 0x0400130E RID: 4878
		public byte m_iIndex;
	}
}

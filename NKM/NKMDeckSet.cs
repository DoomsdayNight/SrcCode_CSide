using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003DB RID: 987
	public sealed class NKMDeckSet : ISerializable
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x0006F929 File Offset: 0x0006DB29
		public NKMDeckSet()
		{
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0006F93C File Offset: 0x0006DB3C
		public NKMDeckSet(NKM_DECK_TYPE type)
		{
			this.type = type;
			if (this.type == NKM_DECK_TYPE.NDT_RAID)
			{
				for (int i = 0; i < NKMCommonConst.Deck.DefaultRaidDeckCount; i++)
				{
					this.decks.Add(new NKMDeckData(this.type));
				}
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0006F995 File Offset: 0x0006DB95
		public int Count
		{
			get
			{
				return this.decks.Count;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0006F9A2 File Offset: 0x0006DBA2
		public IReadOnlyList<NKMDeckData> Values
		{
			get
			{
				return this.decks;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0006F9AA File Offset: 0x0006DBAA
		public NKM_DECK_TYPE DeckType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0006F9B2 File Offset: 0x0006DBB2
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_DECK_TYPE>(ref this.type);
			stream.PutOrGet<NKMDeckData>(ref this.decks);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0006F9CC File Offset: 0x0006DBCC
		public void AddDeck(NKMDeckData deck)
		{
			this.decks.Add(deck);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0006F9DA File Offset: 0x0006DBDA
		public void SetDeck(int index, NKMDeckData deck)
		{
			this.decks[index] = deck;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0006F9EC File Offset: 0x0006DBEC
		public bool FindDeckByUnitUid(long unitUid, out NKMDeckData result)
		{
			foreach (NKMDeckData nkmdeckData in this.decks)
			{
				int num;
				if (nkmdeckData != null && nkmdeckData.HasUnitUid(unitUid, out num))
				{
					result = nkmdeckData;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0006FA54 File Offset: 0x0006DC54
		public bool FindDeckByShipUid(long shipUid, out NKMDeckData result)
		{
			foreach (NKMDeckData nkmdeckData in this.decks)
			{
				if (nkmdeckData != null && nkmdeckData.m_ShipUID == shipUid)
				{
					result = nkmdeckData;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0006FABC File Offset: 0x0006DCBC
		public bool FindDeckByOperatporUid(long operatorUID)
		{
			foreach (NKMDeckData nkmdeckData in this.decks)
			{
				if (nkmdeckData != null && nkmdeckData.m_OperatorUID == operatorUID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0006FB1C File Offset: 0x0006DD1C
		public bool FindDeckIndexByUnitUid(long unitUid, out NKMDeckIndex result)
		{
			for (int i = 0; i < this.decks.Count; i++)
			{
				NKMDeckData nkmdeckData = this.decks[i];
				int num;
				if (nkmdeckData != null && nkmdeckData.HasUnitUid(unitUid, out num))
				{
					result = new NKMDeckIndex(this.type, i);
					return true;
				}
			}
			result = NKMDeckIndex.None;
			return false;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0006FB7C File Offset: 0x0006DD7C
		public bool FindDeckIndexByUnitUid(long unitUid, out NKMDeckIndex deckIndex, out sbyte unitSlotIndex)
		{
			for (int i = 0; i < this.decks.Count; i++)
			{
				NKMDeckData nkmdeckData = this.decks[i];
				int num;
				if (nkmdeckData != null && nkmdeckData.HasUnitUid(unitUid, out num))
				{
					deckIndex = new NKMDeckIndex(this.type, i);
					unitSlotIndex = (sbyte)num;
					return true;
				}
			}
			deckIndex = default(NKMDeckIndex);
			unitSlotIndex = 0;
			return false;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0006FBE0 File Offset: 0x0006DDE0
		public bool FindDeckIndexByShipUid(long shipUid, out NKMDeckIndex result)
		{
			for (int i = 0; i < this.decks.Count; i++)
			{
				NKMDeckData nkmdeckData = this.decks[i];
				if (nkmdeckData != null && nkmdeckData.m_ShipUID == shipUid)
				{
					result = new NKMDeckIndex(this.type, i);
					return true;
				}
			}
			result = default(NKMDeckIndex);
			return false;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0006FC38 File Offset: 0x0006DE38
		public bool FindDeckIndexByOperatorUid(long operatorUid, out NKMDeckIndex result)
		{
			for (int i = 0; i < this.decks.Count; i++)
			{
				NKMDeckData nkmdeckData = this.decks[i];
				if (nkmdeckData != null && nkmdeckData.m_OperatorUID == operatorUid)
				{
					result = new NKMDeckIndex(this.type, i);
					return true;
				}
			}
			result = default(NKMDeckIndex);
			return false;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0006FC90 File Offset: 0x0006DE90
		public int GetAvailableDeckIndex(NKMArmyData armyData)
		{
			for (int i = 0; i < this.decks.Count; i++)
			{
				NKMDeckData nkmdeckData = this.decks[i];
				if (nkmdeckData != null && !NKMMain.IsBusyDeck(nkmdeckData) && NKMMain.IsValidDeck(armyData, this.type, (byte)i) == NKM_ERROR_CODE.NEC_OK)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0006FCDE File Offset: 0x0006DEDE
		public void Clear()
		{
			if (this.type != NKM_DECK_TYPE.NDT_FRIEND)
			{
				throw new Exception(string.Format("[NKMDeckSet] only friend deck can clear. DeckType:{0}", this.type));
			}
			this.decks.Clear();
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0006FD10 File Offset: 0x0006DF10
		public void ClearDeck()
		{
			foreach (NKMDeckData nkmdeckData in this.decks)
			{
				nkmdeckData.m_ShipUID = 0L;
				nkmdeckData.m_OperatorUID = 0L;
				for (int i = 0; i < nkmdeckData.m_listDeckUnitUID.Count; i++)
				{
					nkmdeckData.m_listDeckUnitUID[i] = 0L;
				}
				nkmdeckData.m_LeaderIndex = -1;
			}
		}

		// Token: 0x0400130F RID: 4879
		private NKM_DECK_TYPE type;

		// Token: 0x04001310 RID: 4880
		private List<NKMDeckData> decks = new List<NKMDeckData>();
	}
}

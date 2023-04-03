using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000410 RID: 1040
	public class NKMGameTeamDeckData : ISerializable
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x00077444 File Offset: 0x00075644
		public void AddListUnitDeck(long unitUID)
		{
			long item = 0L;
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref item, unitUID);
			this.m_listUnitDeck.Add(item);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00077470 File Offset: 0x00075670
		public void SetListUnitDeck(int index, long unitUID)
		{
			long value = 0L;
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref value, unitUID);
			this.m_listUnitDeck[index] = value;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0007749B File Offset: 0x0007569B
		public int GetListUnitDeckCount()
		{
			return this.m_listUnitDeck.Count;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000774A8 File Offset: 0x000756A8
		public long GetListUnitDeck(int index)
		{
			return NKMUtil.SimpleDecrypt(this.m_DataEncryptSeed, this.m_listUnitDeck[index]);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000774C4 File Offset: 0x000756C4
		public void AddListUnitDeckUsed(long unitUID)
		{
			long item = 0L;
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref item, unitUID);
			this.m_listUnitDeckUsed.Add(item);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000774EE File Offset: 0x000756EE
		public void RemoveAtListUnitDeckUsed(int index)
		{
			this.m_listUnitDeckUsed.RemoveAt(index);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000774FC File Offset: 0x000756FC
		public void ClearListUnitDeckUsed()
		{
			this.m_listUnitDeckUsed.Clear();
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0007750C File Offset: 0x0007570C
		public void SetListUnitDeckUsed(int index, long unitUID)
		{
			long value = 0L;
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref value, unitUID);
			this.m_listUnitDeckUsed[index] = value;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00077537 File Offset: 0x00075737
		public int GetListUnitDeckUsedCount()
		{
			return this.m_listUnitDeckUsed.Count;
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00077544 File Offset: 0x00075744
		public long GetListUnitDeckUsed(int index)
		{
			return NKMUtil.SimpleDecrypt(this.m_DataEncryptSeed, this.m_listUnitDeckUsed[index]);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00077560 File Offset: 0x00075760
		public void AddListUnitDeckTomb(long unitUID)
		{
			long item = 0L;
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref item, unitUID);
			this.m_listUnitDeckTomb.Add(item);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0007758A File Offset: 0x0007578A
		public int GetListUnitDeckTombCount()
		{
			return this.m_listUnitDeckTomb.Count;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00077597 File Offset: 0x00075797
		public void SetNextDeck(long nextDeck)
		{
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref this.m_NextDeck, nextDeck);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000775AB File Offset: 0x000757AB
		public void SetAutoRespawnIndex(int autoRespawnIndex)
		{
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref this.m_AutoRespawnIndex, autoRespawnIndex);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000775BF File Offset: 0x000757BF
		public void SetAutoRespawnIndexAssist(int autoRespawnIndexAssist)
		{
			NKMUtil.SimpleEncrypt(this.m_DataEncryptSeed, ref this.m_AutoRespawnIndexAssist, autoRespawnIndexAssist);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000775D3 File Offset: 0x000757D3
		public long GetNextDeck()
		{
			return NKMUtil.SimpleDecrypt(this.m_DataEncryptSeed, this.m_NextDeck);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000775E6 File Offset: 0x000757E6
		public int GetAutoRespawnIndex()
		{
			return NKMUtil.SimpleDecrypt(this.m_DataEncryptSeed, this.m_AutoRespawnIndex);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000775F9 File Offset: 0x000757F9
		public int GetAutoRespawnIndexAssist()
		{
			return NKMUtil.SimpleDecrypt(this.m_DataEncryptSeed, this.m_AutoRespawnIndexAssist);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0007760C File Offset: 0x0007580C
		public NKMGameTeamDeckData()
		{
			this.m_DataEncryptSeed = (byte)NKMRandom.Range(10, 100);
			for (int i = 0; i < 4; i++)
			{
				this.AddListUnitDeck(0L);
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00077664 File Offset: 0x00075864
		public void Init()
		{
			this.m_DataEncryptSeed = (byte)NKMRandom.Range(10, 100);
			this.SetNextDeck(0L);
			for (int i = 0; i < this.GetListUnitDeckCount(); i++)
			{
				this.SetListUnitDeck(i, 0L);
			}
			this.m_listUnitDeckUsed.Clear();
			this.m_listUnitDeckTomb.Clear();
			this.SetAutoRespawnIndex(0);
			this.SetAutoRespawnIndexAssist(0);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000776C8 File Offset: 0x000758C8
		public virtual void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_DataEncryptSeed);
			stream.PutOrGet(ref this.m_listUnitDeck);
			stream.PutOrGet(ref this.m_NextDeck);
			stream.PutOrGet(ref this.m_listUnitDeckUsed);
			stream.PutOrGet(ref this.m_listUnitDeckTomb);
			stream.PutOrGet(ref this.m_AutoRespawnIndex);
			stream.PutOrGet(ref this.m_AutoRespawnIndexAssist);
		}

		// Token: 0x04001AFA RID: 6906
		private byte m_DataEncryptSeed;

		// Token: 0x04001AFB RID: 6907
		private List<long> m_listUnitDeck = new List<long>();

		// Token: 0x04001AFC RID: 6908
		private long m_NextDeck;

		// Token: 0x04001AFD RID: 6909
		private List<long> m_listUnitDeckUsed = new List<long>();

		// Token: 0x04001AFE RID: 6910
		private List<long> m_listUnitDeckTomb = new List<long>();

		// Token: 0x04001AFF RID: 6911
		private int m_AutoRespawnIndex;

		// Token: 0x04001B00 RID: 6912
		private int m_AutoRespawnIndexAssist;
	}
}

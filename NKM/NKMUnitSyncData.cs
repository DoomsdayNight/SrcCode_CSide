using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200048B RID: 1163
	public class NKMUnitSyncData : ISerializable
	{
		// Token: 0x06001F77 RID: 8055 RVA: 0x0009505C File Offset: 0x0009325C
		public NKMUnitSyncData()
		{
			this.m_DataEncryptSeed = (byte)NKMRandom.Range(10, 100);
			this.SetHP(0f);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000950C0 File Offset: 0x000932C0
		public void RespawnInit(bool bUsedRollback)
		{
			this.m_bRespawnThisFrame = true;
			this.m_bRespawnUsedRollback = bUsedRollback;
			this.m_TargetUID = 0;
			this.m_SubTargetUID = 0;
			this.m_usSpeedX = 0;
			this.m_usSpeedY = 0;
			this.m_usSpeedZ = 0;
			this.m_usDamageSpeedX = 0;
			this.m_usDamageSpeedZ = 0;
			this.m_usDamageSpeedJumpY = 0;
			this.m_usDamageSpeedKeepTimeX = 0;
			this.m_usDamageSpeedKeepTimeZ = 0;
			this.m_usDamageSpeedKeepTimeJumpY = 0;
			this.m_usSkillCoolTime = 0;
			this.m_usHyperSkillCoolTime = 0;
			this.m_StateID = 0;
			this.m_CatcherGameUnitUID = 0;
			this.m_listDamageData.Clear();
			this.m_listStatusTimeData.Clear();
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0009515C File Offset: 0x0009335C
		public void Encrypt()
		{
			float hp = this.GetHP();
			this.m_DataEncryptSeed = (byte)NKMRandom.Range(10, 100);
			this.SetHP(hp);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00095187 File Offset: 0x00093387
		public float GetHP()
		{
			return this.m_fHP - (float)this.m_DataEncryptSeed;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00095197 File Offset: 0x00093397
		public void SetHP(float fHP)
		{
			this.m_fHP = fHP + (float)this.m_DataEncryptSeed;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000951A8 File Offset: 0x000933A8
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_DataEncryptSeed);
			stream.PutOrGetEnum<NKM_UNIT_PLAY_STATE>(ref this.m_NKM_UNIT_PLAY_STATE);
			stream.PutOrGet(ref this.m_listNKM_UNIT_EVENT_MARK);
			stream.PutOrGet(ref this.m_bRespawnThisFrame);
			stream.PutOrGet(ref this.m_bRespawnUsedRollback);
			stream.PutOrGet(ref this.m_GameUnitUID);
			stream.PutOrGet(ref this.m_TargetUID);
			stream.PutOrGet(ref this.m_SubTargetUID);
			stream.PutOrGet(ref this.m_fHP);
			stream.PutOrGet(ref this.m_PosX);
			stream.PutOrGet(ref this.m_PosZ);
			stream.PutOrGet(ref this.m_JumpYPos);
			stream.PutOrGet(ref this.m_usSpeedX);
			stream.PutOrGet(ref this.m_usSpeedY);
			stream.PutOrGet(ref this.m_usSpeedZ);
			stream.PutOrGet(ref this.m_bRight);
			stream.PutOrGet(ref this.m_StateID);
			stream.PutOrGet(ref this.m_StateChangeCount);
			stream.PutOrGet(ref this.m_bDamageSpeedXNegative);
			stream.PutOrGet(ref this.m_bAttackerZUp);
			stream.PutOrGet(ref this.m_usDamageSpeedX);
			stream.PutOrGet(ref this.m_usDamageSpeedZ);
			stream.PutOrGet(ref this.m_usDamageSpeedJumpY);
			stream.PutOrGet(ref this.m_usDamageSpeedKeepTimeX);
			stream.PutOrGet(ref this.m_usDamageSpeedKeepTimeZ);
			stream.PutOrGet(ref this.m_usDamageSpeedKeepTimeJumpY);
			stream.PutOrGet(ref this.m_usSkillCoolTime);
			stream.PutOrGet(ref this.m_usHyperSkillCoolTime);
			stream.PutOrGet(ref this.m_CatcherGameUnitUID);
			stream.PutOrGet<NKMDamageData>(ref this.m_listDamageData);
			stream.PutOrGet<NKMBuffSyncData>(ref this.m_dicBuffData);
			stream.PutOrGet<NKMUnitStatusTimeSyncData>(ref this.m_listStatusTimeData);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00095338 File Offset: 0x00093538
		public void DeepCopyWithoutDamageAndMarkFrom(NKMUnitSyncData source)
		{
			this.m_DataEncryptSeed = source.m_DataEncryptSeed;
			this.m_NKM_UNIT_PLAY_STATE = source.m_NKM_UNIT_PLAY_STATE;
			this.m_bRespawnThisFrame = source.m_bRespawnThisFrame;
			this.m_bRespawnUsedRollback = source.m_bRespawnUsedRollback;
			this.m_GameUnitUID = source.m_GameUnitUID;
			this.m_TargetUID = source.m_TargetUID;
			this.m_SubTargetUID = source.m_SubTargetUID;
			this.m_fHP = source.m_fHP;
			this.m_PosX = source.m_PosX;
			this.m_PosZ = source.m_PosZ;
			this.m_JumpYPos = source.m_JumpYPos;
			this.m_usSpeedX = source.m_usSpeedX;
			this.m_usSpeedY = source.m_usSpeedY;
			this.m_usSpeedZ = source.m_usSpeedZ;
			this.m_bRight = source.m_bRight;
			this.m_StateID = source.m_StateID;
			this.m_StateChangeCount = source.m_StateChangeCount;
			this.m_bDamageSpeedXNegative = source.m_bDamageSpeedXNegative;
			this.m_bAttackerZUp = source.m_bAttackerZUp;
			this.m_usDamageSpeedX = source.m_usDamageSpeedX;
			this.m_usDamageSpeedZ = source.m_usDamageSpeedZ;
			this.m_usDamageSpeedJumpY = source.m_usDamageSpeedJumpY;
			this.m_usDamageSpeedKeepTimeX = source.m_usDamageSpeedKeepTimeX;
			this.m_usDamageSpeedKeepTimeZ = source.m_usDamageSpeedKeepTimeZ;
			this.m_usDamageSpeedKeepTimeJumpY = source.m_usDamageSpeedKeepTimeJumpY;
			this.m_CatcherGameUnitUID = source.m_CatcherGameUnitUID;
			this.m_usSkillCoolTime = source.m_usSkillCoolTime;
			this.m_usHyperSkillCoolTime = source.m_usHyperSkillCoolTime;
		}

		// Token: 0x04002083 RID: 8323
		private byte m_DataEncryptSeed;

		// Token: 0x04002084 RID: 8324
		public NKM_UNIT_PLAY_STATE m_NKM_UNIT_PLAY_STATE;

		// Token: 0x04002085 RID: 8325
		public List<byte> m_listNKM_UNIT_EVENT_MARK = new List<byte>();

		// Token: 0x04002086 RID: 8326
		public bool m_bRespawnThisFrame;

		// Token: 0x04002087 RID: 8327
		public bool m_bRespawnUsedRollback;

		// Token: 0x04002088 RID: 8328
		public short m_GameUnitUID;

		// Token: 0x04002089 RID: 8329
		public short m_TargetUID;

		// Token: 0x0400208A RID: 8330
		public short m_SubTargetUID;

		// Token: 0x0400208B RID: 8331
		private float m_fHP;

		// Token: 0x0400208C RID: 8332
		public float m_PosX;

		// Token: 0x0400208D RID: 8333
		public float m_PosZ;

		// Token: 0x0400208E RID: 8334
		public float m_JumpYPos;

		// Token: 0x0400208F RID: 8335
		public ushort m_usSpeedX;

		// Token: 0x04002090 RID: 8336
		public ushort m_usSpeedY;

		// Token: 0x04002091 RID: 8337
		public ushort m_usSpeedZ;

		// Token: 0x04002092 RID: 8338
		public bool m_bRight = true;

		// Token: 0x04002093 RID: 8339
		public byte m_StateID;

		// Token: 0x04002094 RID: 8340
		public sbyte m_StateChangeCount;

		// Token: 0x04002095 RID: 8341
		public bool m_bDamageSpeedXNegative;

		// Token: 0x04002096 RID: 8342
		public bool m_bAttackerZUp;

		// Token: 0x04002097 RID: 8343
		public ushort m_usDamageSpeedX;

		// Token: 0x04002098 RID: 8344
		public ushort m_usDamageSpeedZ;

		// Token: 0x04002099 RID: 8345
		public ushort m_usDamageSpeedJumpY;

		// Token: 0x0400209A RID: 8346
		public ushort m_usDamageSpeedKeepTimeX;

		// Token: 0x0400209B RID: 8347
		public ushort m_usDamageSpeedKeepTimeZ;

		// Token: 0x0400209C RID: 8348
		public ushort m_usDamageSpeedKeepTimeJumpY;

		// Token: 0x0400209D RID: 8349
		public ushort m_usSkillCoolTime;

		// Token: 0x0400209E RID: 8350
		public ushort m_usHyperSkillCoolTime;

		// Token: 0x0400209F RID: 8351
		public short m_CatcherGameUnitUID;

		// Token: 0x040020A0 RID: 8352
		public List<NKMDamageData> m_listDamageData = new List<NKMDamageData>();

		// Token: 0x040020A1 RID: 8353
		public Dictionary<short, NKMBuffSyncData> m_dicBuffData = new Dictionary<short, NKMBuffSyncData>();

		// Token: 0x040020A2 RID: 8354
		public List<NKMUnitStatusTimeSyncData> m_listStatusTimeData = new List<NKMUnitStatusTimeSyncData>();
	}
}

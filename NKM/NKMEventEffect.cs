using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E3 RID: 1251
	public class NKMEventEffect : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000B6127 File Offset: 0x000B4327
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x000B612F File Offset: 0x000B432F
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000B6137 File Offset: 0x000B4337
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000B613A File Offset: 0x000B433A
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000B6142 File Offset: 0x000B4342
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000B61B4 File Offset: 0x000B43B4
		public string GetEffectName(int unitSkinID)
		{
			if (this.m_dicSkinEffectName == null)
			{
				return this.m_EffectName;
			}
			string result;
			if (this.m_dicSkinEffectName.TryGetValue(unitSkinID, out result))
			{
				return result;
			}
			return this.m_EffectName;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000B61E8 File Offset: 0x000B43E8
		public string GetEffectName(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return this.m_EffectName;
			}
			return this.GetEffectName(unitData.m_SkinID);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000B6200 File Offset: 0x000B4400
		public string GetEffectName(NKMDamageEffectData DEData)
		{
			if (DEData == null)
			{
				return this.m_EffectName;
			}
			return this.GetEffectName(DEData.GetMasterSkinID());
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000B6218 File Offset: 0x000B4418
		public bool IsRightSkin(int unitSkinID)
		{
			return this.m_SkinID < 0 || this.m_SkinID == unitSkinID;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000B6230 File Offset: 0x000B4430
		public void DeepCopyFromSource(NKMEventEffect source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_SkinID = source.m_SkinID;
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_EffectName = source.m_EffectName;
			this.m_ParentType = source.m_ParentType;
			this.m_fScaleFactor = source.m_fScaleFactor;
			this.m_bForceRight = source.m_bForceRight;
			this.m_bFixedPos = source.m_bFixedPos;
			this.m_OffsetX = source.m_OffsetX;
			this.m_OffsetY = source.m_OffsetY;
			this.m_OffsetZ = source.m_OffsetZ;
			this.m_bUseOffsetZtoY = source.m_bUseOffsetZtoY;
			this.m_fAddRotate = source.m_fAddRotate;
			this.m_bUseZScale = source.m_bUseZScale;
			this.m_bLandConnect = source.m_bLandConnect;
			this.m_BoneName = source.m_BoneName;
			this.m_bUseBoneRotate = source.m_bUseBoneRotate;
			this.m_AnimName = source.m_AnimName;
			this.m_fReserveTime = source.m_fReserveTime;
			this.m_bStateEndStop = source.m_bStateEndStop;
			this.m_bStateEndStopForce = source.m_bStateEndStopForce;
			this.m_bHold = source.m_bHold;
			this.m_bCutIn = source.m_bCutIn;
			this.m_UseMasterAnimSpeed = source.m_UseMasterAnimSpeed;
			this.m_ApplyStopTime = source.m_ApplyStopTime;
			if (source.m_dicSkinEffectName != null)
			{
				this.m_dicSkinEffectName = new Dictionary<int, string>();
				using (Dictionary<int, string>.Enumerator enumerator = source.m_dicSkinEffectName.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, string> keyValuePair = enumerator.Current;
						this.m_dicSkinEffectName.Add(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			this.m_dicSkinEffectName = null;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000B63F8 File Offset: 0x000B45F8
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_SkinID", ref this.m_SkinID);
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_EffectName", ref this.m_EffectName);
			cNKMLua.GetData<NKM_EFFECT_PARENT_TYPE>("m_ParentType", ref this.m_ParentType);
			cNKMLua.GetData("m_fScaleFactor", ref this.m_fScaleFactor);
			cNKMLua.GetData("m_bForceRight", ref this.m_bForceRight);
			cNKMLua.GetData("m_bFixedPos", ref this.m_bFixedPos);
			cNKMLua.GetData("m_OffsetX", ref this.m_OffsetX);
			cNKMLua.GetData("m_OffsetY", ref this.m_OffsetY);
			cNKMLua.GetData("m_OffsetZ", ref this.m_OffsetZ);
			cNKMLua.GetData("m_bUseOffsetZtoY", ref this.m_bUseOffsetZtoY);
			cNKMLua.GetData("m_fAddRotate", ref this.m_fAddRotate);
			cNKMLua.GetData("m_bUseZScale", ref this.m_bUseZScale);
			cNKMLua.GetData("m_bLandConnect", ref this.m_bLandConnect);
			cNKMLua.GetData("m_BoneName", ref this.m_BoneName);
			cNKMLua.GetData("m_bUseBoneRotate", ref this.m_bUseBoneRotate);
			cNKMLua.GetData("m_AnimName", ref this.m_AnimName);
			cNKMLua.GetData("m_fReserveTime", ref this.m_fReserveTime);
			cNKMLua.GetData("m_bStateEndStop", ref this.m_bStateEndStop);
			cNKMLua.GetData("m_bStateEndStopForce", ref this.m_bStateEndStopForce);
			cNKMLua.GetData("m_bHold", ref this.m_bHold);
			cNKMLua.GetData("m_bCutIn", ref this.m_bCutIn);
			cNKMLua.GetData("m_UseMasterAnimSpeed", ref this.m_UseMasterAnimSpeed);
			cNKMLua.GetData("m_ApplyStopTime", ref this.m_ApplyStopTime);
			if (cNKMLua.OpenTable("m_dicSkinEffectName"))
			{
				if (this.m_SkinID >= 0)
				{
					Log.Warn(string.Format("m_dicSkinEffectName m_SkinID {0}과 함께 사용되었습니다.. m_dicSkinEffectName이 무시될 가능성이 높습니다.", this.m_SkinID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 3363);
				}
				this.m_dicSkinEffectName = new Dictionary<int, string>();
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					int key;
					string value;
					if (cNKMLua.GetData(1, out key, 0) && cNKMLua.GetData(2, out value, ""))
					{
						this.m_dicSkinEffectName.Add(key, value);
					}
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x040024B8 RID: 9400
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040024B9 RID: 9401
		public int m_SkinID = -1;

		// Token: 0x040024BA RID: 9402
		public bool m_bAnimTime = true;

		// Token: 0x040024BB RID: 9403
		public float m_fEventTime;

		// Token: 0x040024BC RID: 9404
		public bool m_bStateEndTime;

		// Token: 0x040024BD RID: 9405
		public string m_EffectName = "";

		// Token: 0x040024BE RID: 9406
		public NKM_EFFECT_PARENT_TYPE m_ParentType;

		// Token: 0x040024BF RID: 9407
		public float m_fScaleFactor = 1f;

		// Token: 0x040024C0 RID: 9408
		public bool m_bForceRight;

		// Token: 0x040024C1 RID: 9409
		public bool m_bFixedPos;

		// Token: 0x040024C2 RID: 9410
		public float m_OffsetX;

		// Token: 0x040024C3 RID: 9411
		public float m_OffsetY;

		// Token: 0x040024C4 RID: 9412
		public float m_OffsetZ;

		// Token: 0x040024C5 RID: 9413
		public bool m_bUseOffsetZtoY;

		// Token: 0x040024C6 RID: 9414
		public float m_fAddRotate;

		// Token: 0x040024C7 RID: 9415
		public bool m_bUseZScale = true;

		// Token: 0x040024C8 RID: 9416
		public bool m_bLandConnect;

		// Token: 0x040024C9 RID: 9417
		public string m_BoneName = "";

		// Token: 0x040024CA RID: 9418
		public bool m_bUseBoneRotate;

		// Token: 0x040024CB RID: 9419
		public string m_AnimName = "";

		// Token: 0x040024CC RID: 9420
		public float m_fReserveTime;

		// Token: 0x040024CD RID: 9421
		public bool m_bStateEndStop;

		// Token: 0x040024CE RID: 9422
		public bool m_bStateEndStopForce;

		// Token: 0x040024CF RID: 9423
		public bool m_bHold;

		// Token: 0x040024D0 RID: 9424
		public bool m_bCutIn;

		// Token: 0x040024D1 RID: 9425
		public bool m_UseMasterAnimSpeed;

		// Token: 0x040024D2 RID: 9426
		public bool m_ApplyStopTime = true;

		// Token: 0x040024D3 RID: 9427
		public Dictionary<int, string> m_dicSkinEffectName;
	}
}

using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004DB RID: 1243
	public class NKMEventSound : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000B52AF File Offset: 0x000B34AF
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000B52B7 File Offset: 0x000B34B7
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000B52BF File Offset: 0x000B34BF
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000B52C2 File Offset: 0x000B34C2
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x000B52CA File Offset: 0x000B34CA
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000B532C File Offset: 0x000B352C
		public List<string> GetTargetSoundList(int skinID)
		{
			List<string> result;
			if (this.m_dicSkinSound != null && this.m_dicSkinSound.TryGetValue(skinID, out result))
			{
				return result;
			}
			return this.m_listSoundName;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000B5359 File Offset: 0x000B3559
		public List<string> GetTargetSoundList(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return this.m_listSoundName;
			}
			return this.GetTargetSoundList(unitData.m_SkinID);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000B5371 File Offset: 0x000B3571
		public List<string> GetTargetSoundList(NKMDamageEffectData DEData)
		{
			if (DEData == null)
			{
				return this.m_listSoundName;
			}
			return this.GetTargetSoundList(DEData.GetMasterSkinID());
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000B538C File Offset: 0x000B358C
		public bool GetRandomSound(int skinID, out string soundName)
		{
			List<string> targetSoundList = this.GetTargetSoundList(skinID);
			if (targetSoundList.Count > 0)
			{
				int index = NKMRandom.Range(0, targetSoundList.Count);
				soundName = targetSoundList[index];
				return true;
			}
			soundName = null;
			return false;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000B53C6 File Offset: 0x000B35C6
		public bool GetRandomSound(NKMUnitData unitData, out string soundName)
		{
			if (unitData == null)
			{
				return this.GetRandomSound(0, out soundName);
			}
			return this.GetRandomSound(unitData.m_SkinID, out soundName);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000B53E1 File Offset: 0x000B35E1
		public bool GetRandomSound(NKMDamageEffectData DEData, out string soundName)
		{
			if (DEData == null)
			{
				return this.GetRandomSound(0, out soundName);
			}
			return this.GetRandomSound(DEData.GetMasterSkinID(), out soundName);
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000B53FC File Offset: 0x000B35FC
		public bool IsRightSkin(int unitSkinID)
		{
			return this.m_SkinID < 0 || this.m_SkinID == unitSkinID;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000B5414 File Offset: 0x000B3614
		public void DeepCopyFromSource(NKMEventSound source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_SkinID = source.m_SkinID;
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_listSoundName.Clear();
			for (int i = 0; i < source.m_listSoundName.Count; i++)
			{
				this.m_listSoundName.Add(source.m_listSoundName[i]);
			}
			this.m_bVoice = source.m_bVoice;
			this.m_fLocalVol = source.m_fLocalVol;
			this.m_fFocusRange = source.m_fFocusRange;
			this.m_b3D = source.m_b3D;
			this.m_bLoop = source.m_bLoop;
			this.m_PlayRate = source.m_PlayRate;
			this.m_bStopSound = source.m_bStopSound;
			if (source.m_dicSkinSound != null)
			{
				this.m_dicSkinSound = new Dictionary<int, List<string>>();
				using (Dictionary<int, List<string>>.Enumerator enumerator = source.m_dicSkinSound.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, List<string>> keyValuePair = enumerator.Current;
						this.m_dicSkinSound.Add(keyValuePair.Key, new List<string>(keyValuePair.Value));
					}
					return;
				}
			}
			this.m_dicSkinSound = null;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000B5568 File Offset: 0x000B3768
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
			if (cNKMLua.OpenTable("m_listSoundName"))
			{
				this.m_listSoundName.Clear();
				int num = 1;
				string item = "";
				while (cNKMLua.GetData(num, ref item))
				{
					this.m_listSoundName.Add(item);
					num++;
				}
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bVoice", ref this.m_bVoice);
			cNKMLua.GetData("m_fLocalVol", ref this.m_fLocalVol);
			cNKMLua.GetData("m_fFocusRange", ref this.m_fFocusRange);
			cNKMLua.GetData("m_b3D", ref this.m_b3D);
			cNKMLua.GetData("m_bLoop", ref this.m_bLoop);
			cNKMLua.GetData("m_PlayRate", ref this.m_PlayRate);
			cNKMLua.GetData("m_bStopSound", ref this.m_bStopSound);
			if (cNKMLua.OpenTable("m_dicSkinSound"))
			{
				if (this.m_SkinID >= 0)
				{
					Log.Warn(string.Format("m_dicSkinSound이 m_SkinID {0}과 함께 사용되었습니다.. m_dicSkinSound가 무시될 가능성이 높습니다.", this.m_SkinID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 2741);
				}
				this.m_dicSkinSound = new Dictionary<int, List<string>>();
				int num2 = 1;
				while (cNKMLua.OpenTable(num2))
				{
					int key;
					if (!cNKMLua.GetData(1, out key, 0))
					{
						Log.Error("Bad data from m_dicSkinSound for skinID " + this.m_SkinID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 2752);
					}
					else
					{
						List<string> list = new List<string>();
						int num3 = 2;
						string item2;
						while (cNKMLua.GetData(num3, out item2, ""))
						{
							list.Add(item2);
							num3++;
						}
						this.m_dicSkinSound.Add(key, list);
						num2++;
						cNKMLua.CloseTable();
					}
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x0400246C RID: 9324
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400246D RID: 9325
		public int m_SkinID = -1;

		// Token: 0x0400246E RID: 9326
		public bool m_bAnimTime = true;

		// Token: 0x0400246F RID: 9327
		public float m_fEventTime;

		// Token: 0x04002470 RID: 9328
		public bool m_bStateEndTime;

		// Token: 0x04002471 RID: 9329
		private List<string> m_listSoundName = new List<string>();

		// Token: 0x04002472 RID: 9330
		public bool m_bVoice;

		// Token: 0x04002473 RID: 9331
		public float m_fLocalVol = 1f;

		// Token: 0x04002474 RID: 9332
		public float m_fFocusRange = 1200f;

		// Token: 0x04002475 RID: 9333
		public bool m_b3D;

		// Token: 0x04002476 RID: 9334
		public bool m_bLoop;

		// Token: 0x04002477 RID: 9335
		public float m_PlayRate = 1f;

		// Token: 0x04002478 RID: 9336
		public bool m_bStopSound;

		// Token: 0x04002479 RID: 9337
		public Dictionary<int, List<string>> m_dicSkinSound;
	}
}

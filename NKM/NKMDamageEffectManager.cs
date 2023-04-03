using System;
using System.Collections.Generic;
using Cs.Logging;

namespace NKM
{
	// Token: 0x020003C7 RID: 967
	public class NKMDamageEffectManager
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x0006C410 File Offset: 0x0006A610
		public void Init(NKMGame cNKMGame)
		{
			this.m_DEUIDIndex = 1;
			this.m_fDeltaTime = 0f;
			this.m_NKMGame = cNKMGame;
			foreach (KeyValuePair<short, NKMDamageEffect> keyValuePair in this.m_dicNKMDamageEffect)
			{
				NKMDamageEffect value = keyValuePair.Value;
				this.CloseDamageEffect(value);
			}
			foreach (NKMDamageEffect cNKMDamageEffect in this.m_listNKMDamageEffectNextFrame)
			{
				this.CloseDamageEffect(cNKMDamageEffect);
			}
			this.m_dicNKMDamageEffect.Clear();
			this.m_listNKMDamageEffectNextFrame.Clear();
			this.m_listDEUIDDelete.Clear();
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0006C4D0 File Offset: 0x0006A6D0
		private short GetDEUID()
		{
			short deuidindex = this.m_DEUIDIndex;
			this.m_DEUIDIndex = deuidindex + 1;
			return deuidindex;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0006C4F0 File Offset: 0x0006A6F0
		public void Update(float fDeltaTime)
		{
			NKMProfiler.BeginSample("NKMDamageEffectManager.Update");
			this.m_fDeltaTime = fDeltaTime;
			this.m_listDEUIDDelete.Clear();
			foreach (KeyValuePair<short, NKMDamageEffect> keyValuePair in this.m_dicNKMDamageEffect)
			{
				NKMDamageEffect value = keyValuePair.Value;
				if (value != null && (this.m_NKMGame.GetWorldStopTime() <= 0f || (value.GetTemplet() != null && value.GetTemplet().m_CanIgnoreStopTime && value.GetMasterUnit() != null && !value.GetMasterUnit().IsStopTime())))
				{
					value.Update(this.m_fDeltaTime);
					if (value.IsEnd())
					{
						this.m_listDEUIDDelete.Add(value.GetDEUID());
						this.CloseDamageEffect(value);
					}
				}
			}
			for (int i = 0; i < this.m_listDEUIDDelete.Count; i++)
			{
				this.m_dicNKMDamageEffect.Remove(this.m_listDEUIDDelete[i]);
			}
			foreach (NKMDamageEffect nkmdamageEffect in this.m_listNKMDamageEffectNextFrame)
			{
				if (!this.m_dicNKMDamageEffect.ContainsKey(nkmdamageEffect.GetDEUID()))
				{
					this.m_dicNKMDamageEffect.Add(nkmdamageEffect.GetDEUID(), nkmdamageEffect);
				}
				else
				{
					Log.Error(string.Format("NKMDamageEffectManager Update Duplicate DEUID: {0} ", nkmdamageEffect.GetDEUID()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffectManager.cs", 99);
				}
			}
			this.m_listNKMDamageEffectNextFrame.Clear();
			NKMProfiler.EndSample();
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0006C67C File Offset: 0x0006A87C
		public bool IsLiveEffect(short effectUID)
		{
			return this.m_dicNKMDamageEffect.ContainsKey(effectUID);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006C68A File Offset: 0x0006A88A
		public NKMDamageEffect GetDamageEffect(short DEUID)
		{
			if (this.m_dicNKMDamageEffect.ContainsKey(DEUID))
			{
				return this.m_dicNKMDamageEffect[DEUID];
			}
			return null;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
		public NKMDamageEffect UseDamageEffect(string templetID, short masterGameUID, short targetGameUID, NKMUnitSkillTemplet cUnitSkillTemplet, int masterUnitPhase, float posX, float posY, float posZ, bool bRight = true, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f, float fAddRotate = 0f, bool bUseZScale = true, float fSpeedFactorX = 0f, float fSpeedFactorY = 0f, float reserveTime = 0f, bool bNextFrame = false)
		{
			NKMDamageEffect nkmdamageEffect = this.CreateDamageEffect();
			if (!nkmdamageEffect.SetDamageEffect(this.m_NKMGame, this, cUnitSkillTemplet, masterUnitPhase, this.GetDEUID(), templetID, masterGameUID, targetGameUID, posX, posY, posZ, bRight, offsetX, offsetY, offsetZ, fAddRotate, bUseZScale, fSpeedFactorX, fSpeedFactorY))
			{
				this.CloseDamageEffect(nkmdamageEffect);
				return null;
			}
			nkmdamageEffect.DoStateEndStart();
			if (!bNextFrame)
			{
				if (!this.m_dicNKMDamageEffect.ContainsKey(nkmdamageEffect.GetDEUID()))
				{
					this.m_dicNKMDamageEffect.Add(nkmdamageEffect.GetDEUID(), nkmdamageEffect);
				}
				else
				{
					Log.Error(string.Format("UseDamageEffect Duplicate DEUID: {0}", nkmdamageEffect.GetDEUID()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffectManager.cs", 145);
				}
			}
			else
			{
				this.m_listNKMDamageEffectNextFrame.Add(nkmdamageEffect);
			}
			return nkmdamageEffect;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0006C75B File Offset: 0x0006A95B
		protected virtual NKMDamageEffect CreateDamageEffect()
		{
			return (NKMDamageEffect)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageEffect, "", "", false);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0006C780 File Offset: 0x0006A980
		public void DeleteDE(short DEUID)
		{
			if (this.m_dicNKMDamageEffect.ContainsKey(DEUID))
			{
				NKMDamageEffect nkmdamageEffect = this.m_dicNKMDamageEffect[DEUID];
				if (nkmdamageEffect != null)
				{
					nkmdamageEffect.SetDie(false, true);
					this.CloseDamageEffect(nkmdamageEffect);
				}
				this.m_dicNKMDamageEffect.Remove(DEUID);
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0006C7C7 File Offset: 0x0006A9C7
		protected virtual void CloseDamageEffect(NKMDamageEffect cNKMDamageEffect)
		{
			this.m_NKMGame.GetObjectPool().CloseObj(cNKMDamageEffect);
		}

		// Token: 0x0400120B RID: 4619
		private short m_DEUIDIndex;

		// Token: 0x0400120C RID: 4620
		private float m_fDeltaTime;

		// Token: 0x0400120D RID: 4621
		protected NKMGame m_NKMGame;

		// Token: 0x0400120E RID: 4622
		private Dictionary<short, NKMDamageEffect> m_dicNKMDamageEffect = new Dictionary<short, NKMDamageEffect>();

		// Token: 0x0400120F RID: 4623
		private List<NKMDamageEffect> m_listNKMDamageEffectNextFrame = new List<NKMDamageEffect>();

		// Token: 0x04001210 RID: 4624
		private List<short> m_listDEUIDDelete = new List<short>();
	}
}

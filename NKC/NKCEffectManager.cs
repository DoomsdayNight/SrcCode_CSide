using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x02000671 RID: 1649
	public class NKCEffectManager
	{
		// Token: 0x0600342E RID: 13358 RVA: 0x00106C2C File Offset: 0x00104E2C
		public void Init()
		{
			foreach (NKCEffectReserveData closeObj in this.m_linklistEffectReserveData)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(closeObj);
			}
			this.m_linklistEffectReserveData.Clear();
			foreach (KeyValuePair<int, NKCASEffect> keyValuePair in this.m_dicEffect)
			{
				NKCASEffect value = keyValuePair.Value;
				if (value != null)
				{
					NKCScenManager.GetScenManager().GetObjectPool().CloseObj(value);
				}
			}
			this.m_dicEffect.Clear();
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x00106CDC File Offset: 0x00104EDC
		private int GetEffectUID()
		{
			int effectUIDIndex = this.m_EffectUIDIndex;
			this.m_EffectUIDIndex = effectUIDIndex + 1;
			return effectUIDIndex;
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x00106CFC File Offset: 0x00104EFC
		public bool LoadFromLUA(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT_EFFECT", fileName, true) && nkmlua.OpenTable("m_dicEffectTemplet"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKCEffectTemplet nkceffectTemplet = new NKCEffectTemplet();
					nkceffectTemplet.LoadFromLUA(nkmlua);
					if (!this.m_dicEffectTemplet.ContainsKey(nkceffectTemplet.m_Name))
					{
						this.m_dicEffectTemplet.Add(nkceffectTemplet.m_Name, nkceffectTemplet);
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x00106D84 File Offset: 0x00104F84
		public void ObjectParentWait()
		{
			foreach (KeyValuePair<int, NKCASEffect> keyValuePair in this.m_dicEffect)
			{
				NKCASEffect value = keyValuePair.Value;
				if (value != null)
				{
					value.ObjectParentWait();
				}
			}
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x00106DC4 File Offset: 0x00104FC4
		public void ObjectParentRestore()
		{
			foreach (KeyValuePair<int, NKCASEffect> keyValuePair in this.m_dicEffect)
			{
				NKCASEffect value = keyValuePair.Value;
				if (value != null)
				{
					value.ObjectParentRestore();
				}
			}
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x00106E04 File Offset: 0x00105004
		public void Update(float fDeltaTime)
		{
			this.m_fDeltaTime = fDeltaTime;
			for (LinkedListNode<NKCEffectReserveData> linkedListNode = this.m_linklistEffectReserveData.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				NKCEffectReserveData value = linkedListNode.Value;
				if (value != null)
				{
					value.m_fReserveTime -= this.m_fDeltaTime;
					if (value.m_fReserveTime <= 0f)
					{
						this.EffectStart(value.m_NKCASEffect, value.m_PosX, value.m_PosY, value.m_PosZ, value.m_bNotStart);
						NKCScenManager.GetScenManager().GetObjectPool().CloseObj(value);
						LinkedListNode<NKCEffectReserveData> next = linkedListNode.Next;
						this.m_linklistEffectReserveData.Remove(linkedListNode);
						linkedListNode = next;
						continue;
					}
				}
			}
			this.m_listEffectUIDDelete.Clear();
			foreach (KeyValuePair<int, NKCASEffect> keyValuePair in this.m_dicEffect)
			{
				NKCASEffect value2 = keyValuePair.Value;
				if (value2 != null)
				{
					bool flag = false;
					bool flag2 = false;
					if (NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetWorldStopTime() > 0f)
					{
						flag = true;
						if (value2.GetMasterUnit() != null)
						{
							flag2 = value2.GetMasterUnit().IsStopTime();
						}
					}
					if (value2.m_bDEEffect)
					{
						if (value2.CanIgnoreStopTime && !flag2)
						{
							flag = false;
						}
					}
					else
					{
						flag = (value2.ApplyStopTime && flag2);
					}
					if (!flag)
					{
						value2.Update(this.m_fDeltaTime);
					}
					if (value2.m_bAutoDie && value2.IsEnd())
					{
						this.m_listEffectUIDDelete.Add(value2.m_EffectUID);
						NKCScenManager.GetScenManager().GetObjectPool().CloseObj(value2);
					}
				}
			}
			for (int i = 0; i < this.m_listEffectUIDDelete.Count; i++)
			{
				this.m_dicEffect.Remove(this.m_listEffectUIDDelete[i]);
			}
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x00106FC4 File Offset: 0x001051C4
		public bool IsLiveEffect(int effectUID)
		{
			return this.m_dicEffect.ContainsKey(effectUID);
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x00106FD4 File Offset: 0x001051D4
		public void DeleteAllEffect()
		{
			foreach (int effectUID in new List<int>(this.m_dicEffect.Keys))
			{
				this.DeleteEffect(effectUID);
			}
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x00107034 File Offset: 0x00105234
		public void DeleteEffect(NKCASEffect cNKCASEffect)
		{
			if (cNKCASEffect == null)
			{
				return;
			}
			this.DeleteEffect(cNKCASEffect.m_EffectUID);
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x00107048 File Offset: 0x00105248
		public void DeleteEffect(int effectUID)
		{
			if (this.m_dicEffect.ContainsKey(effectUID))
			{
				NKCASEffect closeObj = this.m_dicEffect[effectUID];
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(closeObj);
				this.m_dicEffect.Remove(effectUID);
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x00107090 File Offset: 0x00105290
		public NKCASEffect UseEffect(short masterUnitGameUID, NKMAssetName name, NKM_EFFECT_PARENT_TYPE eNKM_EFFECT_PARENT_TYPE, float posX, float posY, float posZ, bool bRight = true, float fScaleFactor = 1f, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f, bool m_bUseZtoY = false, float fAddRotate = 0f, bool bUseZScale = false, string boneName = "", bool bUseBoneRotate = false, bool bAutoDie = true, string animName = "", float fAnimSpeed = 1f, bool bNotStart = false, bool bCutIn = false, float reserveTime = 0f, float reserveDieTime = -1f, bool bDEEffect = false)
		{
			return this.UseEffect(masterUnitGameUID, name.m_BundleName, name.m_AssetName, eNKM_EFFECT_PARENT_TYPE, posX, posY, posZ, bRight, fScaleFactor, offsetX, offsetY, offsetZ, m_bUseZtoY, fAddRotate, bUseZScale, boneName, bUseBoneRotate, bAutoDie, animName, fAnimSpeed, bNotStart, bCutIn, reserveTime, reserveDieTime, bDEEffect);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x001070DC File Offset: 0x001052DC
		public NKCASEffect UseEffect(short masterUnitGameUID, string bundleName, string name, NKM_EFFECT_PARENT_TYPE eNKM_EFFECT_PARENT_TYPE, float posX, float posY, float posZ, bool bRight = true, float fScaleFactor = 1f, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f, bool m_bUseZtoY = false, float fAddRotate = 0f, bool bUseZScale = false, string boneName = "", bool bUseBoneRotate = false, bool bAutoDie = true, string animName = "", float fAnimSpeed = 1f, bool bNotStart = false, bool bCutIn = false, float reserveTime = 0f, float reserveDieTime = -1f, bool bDEEffect = false)
		{
			if (!bDEEffect && NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable())
			{
				return null;
			}
			NKCASEffect nkcaseffect = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, bundleName, name, false);
			if (nkcaseffect == null)
			{
				return null;
			}
			if (nkcaseffect.GetLoadFail())
			{
				this.DeleteEffect(nkcaseffect.m_EffectUID);
				return null;
			}
			nkcaseffect.m_EffectUID = this.GetEffectUID();
			nkcaseffect.m_NKM_EFFECT_PARENT_TYPE = eNKM_EFFECT_PARENT_TYPE;
			nkcaseffect.m_OffsetX = offsetX;
			if (!m_bUseZtoY)
			{
				nkcaseffect.m_OffsetY = offsetY;
			}
			else
			{
				nkcaseffect.m_OffsetY = offsetY + offsetZ;
			}
			nkcaseffect.m_OffsetZ = offsetZ;
			nkcaseffect.m_fAddRotate = fAddRotate;
			nkcaseffect.m_bRight = bRight;
			nkcaseffect.m_MasterUnitGameUID = masterUnitGameUID;
			nkcaseffect.m_BoneName = boneName;
			nkcaseffect.m_bUseBoneRotate = bUseBoneRotate;
			nkcaseffect.m_bAutoDie = bAutoDie;
			nkcaseffect.m_AnimName = animName;
			nkcaseffect.m_fAnimSpeed = fAnimSpeed;
			nkcaseffect.m_bCutIn = bCutIn;
			nkcaseffect.m_bUseZScale = bUseZScale;
			nkcaseffect.m_bDEEffect = bDEEffect;
			nkcaseffect.SetReserveDieTime(reserveDieTime);
			nkcaseffect.SetScaleFactor(fScaleFactor, fScaleFactor, fScaleFactor);
			if (reserveTime > 0f)
			{
				if (nkcaseffect.m_EffectInstant != null && nkcaseffect.m_EffectInstant.m_Instant != null && nkcaseffect.m_EffectInstant.m_Instant.activeSelf)
				{
					nkcaseffect.m_EffectInstant.m_Instant.SetActive(false);
				}
				NKCEffectReserveData nkceffectReserveData = (NKCEffectReserveData)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCEffectReserveData, "", "", false);
				nkceffectReserveData.m_NKCASEffect = nkcaseffect;
				nkceffectReserveData.m_PosX = posX;
				nkceffectReserveData.m_PosY = posY;
				nkceffectReserveData.m_PosZ = posZ;
				nkceffectReserveData.m_bNotStart = bNotStart;
				nkceffectReserveData.m_fReserveTime = reserveTime;
				this.m_linklistEffectReserveData.AddLast(nkceffectReserveData);
				return nkceffectReserveData.m_NKCASEffect;
			}
			return this.EffectStart(nkcaseffect, posX, posY, posZ, bNotStart);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x0010729C File Offset: 0x0010549C
		public NKCASEffect EffectStart(NKCASEffect cNKCASEffect, float posX, float posY, float posZ, bool bNotStart)
		{
			if (!bNotStart)
			{
				if (cNKCASEffect.m_EffectInstant != null && cNKCASEffect.m_EffectInstant.m_Instant != null && !cNKCASEffect.m_EffectInstant.m_Instant.activeSelf)
				{
					cNKCASEffect.m_EffectInstant.m_Instant.SetActive(true);
				}
				cNKCASEffect.m_bPlayed = true;
			}
			else if (cNKCASEffect.m_EffectInstant != null && cNKCASEffect.m_EffectInstant.m_Instant != null && cNKCASEffect.m_EffectInstant.m_Instant.activeSelf)
			{
				cNKCASEffect.m_EffectInstant.m_Instant.SetActive(false);
			}
			cNKCASEffect.SetRight(cNKCASEffect.m_bRight);
			cNKCASEffect.ObjectParentRestore();
			if (!cNKCASEffect.m_bCutIn)
			{
				if (posX != -1f || posY != -1f || posZ != -1f)
				{
					cNKCASEffect.SetPos(posX, posY, posZ);
				}
				if (cNKCASEffect.m_fAddRotate != -1f)
				{
					cNKCASEffect.SetRotate(cNKCASEffect.m_fAddRotate);
				}
			}
			else
			{
				cNKCASEffect.m_bUseZScale = false;
			}
			if (!bNotStart && cNKCASEffect.m_AnimName.Length > 1)
			{
				float fAnimSpeed = cNKCASEffect.m_fAnimSpeed;
				cNKCASEffect.PlayAnim(cNKCASEffect.m_AnimName, false, fAnimSpeed);
			}
			if (cNKCASEffect.m_EffectInstant != null && cNKCASEffect.m_EffectInstant.m_Instant != null)
			{
				cNKCASEffect.m_EffectInstant.m_Instant.transform.SetAsLastSibling();
			}
			this.m_dicEffect.Add(cNKCASEffect.m_EffectUID, cNKCASEffect);
			return cNKCASEffect;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x001073FC File Offset: 0x001055FC
		public void StopCutInEffect()
		{
			this.m_listEffectUIDDelete.Clear();
			foreach (KeyValuePair<int, NKCASEffect> keyValuePair in this.m_dicEffect)
			{
				NKCASEffect value = keyValuePair.Value;
				if (value != null && value.m_bCutIn && (value.m_bAutoDie || value.m_fReserveDieTime != -1f))
				{
					this.m_listEffectUIDDelete.Add(value.m_EffectUID);
					NKCScenManager.GetScenManager().GetObjectPool().CloseObj(value);
				}
			}
			for (int i = 0; i < this.m_listEffectUIDDelete.Count; i++)
			{
				this.m_dicEffect.Remove(this.m_listEffectUIDDelete[i]);
			}
		}

		// Token: 0x040032A9 RID: 12969
		private int m_EffectUIDIndex = 1;

		// Token: 0x040032AA RID: 12970
		private Dictionary<string, NKCEffectTemplet> m_dicEffectTemplet = new Dictionary<string, NKCEffectTemplet>();

		// Token: 0x040032AB RID: 12971
		private Dictionary<int, NKCASEffect> m_dicEffect = new Dictionary<int, NKCASEffect>();

		// Token: 0x040032AC RID: 12972
		private List<int> m_listEffectUIDDelete = new List<int>();

		// Token: 0x040032AD RID: 12973
		private LinkedList<NKCEffectReserveData> m_linklistEffectReserveData = new LinkedList<NKCEffectReserveData>();

		// Token: 0x040032AE RID: 12974
		private float m_fDeltaTime;
	}
}

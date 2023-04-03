using System;
using System.Collections.Generic;
using NKC.Office;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000794 RID: 1940
	public class NKCAnimationInstance
	{
		// Token: 0x06004C3A RID: 19514 RVA: 0x0016C8BC File Offset: 0x0016AABC
		private int GetEffectKey()
		{
			int num = this.m_iEffectKey + 1;
			this.m_iEffectKey = num;
			return num;
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0016C8DC File Offset: 0x0016AADC
		public NKCAnimationInstance(INKCAnimationActor actor, Transform effectParentTr, List<NKCAnimationEventTemplet> lstEventTempletSet, Vector3 startPos, Vector3 endPos)
		{
			this.m_Actor = actor;
			this.m_targetObj = null;
			this.m_effectParentTr = effectParentTr;
			this.m_startPos = startPos;
			this.m_endPos = endPos;
			this.m_lstAniEvent = lstEventTempletSet;
			if (this.m_lstAniEvent == null)
			{
				this.m_lstAniEvent = new List<NKCAnimationEventTemplet>();
			}
			this.m_lstActivated = new List<bool>();
			for (int i = 0; i < this.m_lstAniEvent.Count; i++)
			{
				this.m_lstActivated.Add(false);
			}
			this.m_NormalizedPos = 0f;
			this.m_Time = 0f;
			this.m_iEffectKey = 0;
			this.m_bIsMoveAnimtion = this.IsMovingAnimation();
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0016C9A8 File Offset: 0x0016ABA8
		private bool IsMovingAnimation()
		{
			foreach (NKCAnimationEventTemplet nkcanimationEventTemplet in this.m_lstAniEvent)
			{
				AnimationEventType aniEventType = nkcanimationEventTemplet.m_AniEventType;
				if (aniEventType - AnimationEventType.SET_MOVE_SPEED <= 1 || aniEventType == AnimationEventType.SET_ABSOLUTE_MOVE_SPEED)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x0016CA0C File Offset: 0x0016AC0C
		public void Update(float deltaTime)
		{
			if (this.IsFinished())
			{
				return;
			}
			this.DrawDebugLine(Color.blue);
			this.m_Time += deltaTime;
			if (this.m_dicEffect.Count > 0)
			{
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				foreach (KeyValuePair<int, NKCAnimationInstance.EffectData> keyValuePair in this.m_dicEffect)
				{
					if (keyValuePair.Value.m_fEffectEndTime < serverUTCTime)
					{
						UnityEngine.Object.Destroy(keyValuePair.Value.m_objEffect);
						this.m_dicEffect.Remove(keyValuePair.Key);
						break;
					}
				}
			}
			for (int i = this.m_lstAniEvent.Count - 1; i >= 0; i--)
			{
				NKCAnimationEventTemplet nkcanimationEventTemplet = this.m_lstAniEvent[i];
				if (!this.m_lstActivated[i] && this.m_Time >= nkcanimationEventTemplet.m_StartTime)
				{
					switch (nkcanimationEventTemplet.m_AniEventType)
					{
					case AnimationEventType.FINISH_EVENT:
						this.m_bForceFinished = true;
						break;
					case AnimationEventType.ANIMATION_SPINE:
						if (this.m_Actor != null)
						{
							bool bDefaultAnim = nkcanimationEventTemplet.m_StartTime == 0f && nkcanimationEventTemplet.m_BoolValue;
							this.m_Actor.PlaySpineAnimation(NKCAnimationEventManager.GetAnimationType(nkcanimationEventTemplet.m_StrValue), nkcanimationEventTemplet.m_BoolValue, nkcanimationEventTemplet.m_FloatValue, bDefaultAnim);
						}
						this.m_LastAnimationTemplet = nkcanimationEventTemplet;
						break;
					case AnimationEventType.ANIMATION_NAME_SPINE:
						if (this.m_Actor != null)
						{
							this.m_Actor.PlaySpineAnimation(nkcanimationEventTemplet.m_StrValue, nkcanimationEventTemplet.m_BoolValue, nkcanimationEventTemplet.m_FloatValue);
						}
						this.m_LastAnimationTemplet = nkcanimationEventTemplet;
						break;
					case AnimationEventType.ANIMATION_UNITY:
						if (this.m_Actor != null && this.m_Actor.Animator != null)
						{
							this.m_Actor.Animator.Play(nkcanimationEventTemplet.m_StrValue);
							if (nkcanimationEventTemplet.m_FloatValue != 0f)
							{
								this.m_Actor.Animator.speed = nkcanimationEventTemplet.m_FloatValue;
							}
						}
						this.m_LastAnimationTemplet = nkcanimationEventTemplet;
						break;
					case AnimationEventType.EFFECT_SPAWN:
					{
						string[] array = nkcanimationEventTemplet.m_StrValue.Split(new char[]
						{
							'@'
						});
						if (array.Length > 1)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(NKCResourceUtility.GetOrLoadAssetResource<GameObject>(array[0], array[1], false), this.m_effectParentTr);
							if (2 < array.Length)
							{
								gameObject.transform.position = this.m_Actor.GetBonePosition(array[2]);
							}
							else
							{
								gameObject.transform.position = this.m_Actor.Transform.position;
							}
							if (nkcanimationEventTemplet.m_FloatValue != 0f)
							{
								gameObject.transform.localScale = Vector3.one * nkcanimationEventTemplet.m_FloatValue;
							}
							NKCAnimationInstance.EffectData value = default(NKCAnimationInstance.EffectData);
							value.m_objEffect = gameObject;
							if (nkcanimationEventTemplet.m_FloatValue2 > 0f)
							{
								value.m_fEffectEndTime = NKCSynchronizedTime.GetServerUTCTime(0.0).AddSeconds((double)nkcanimationEventTemplet.m_FloatValue2);
							}
							else
							{
								value.m_fEffectEndTime = DateTime.MaxValue;
							}
							this.m_dicEffect.Add(this.GetEffectKey(), value);
						}
						break;
					}
					case AnimationEventType.EFFECT_SPAWN_FOLLOW:
					{
						string[] array2 = nkcanimationEventTemplet.m_StrValue.Split(new char[]
						{
							'@'
						});
						if (array2.Length > 1)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(NKCResourceUtility.GetOrLoadAssetResource<GameObject>(array2[0], array2[1], false), this.m_Actor.SDParent);
							if (2 < array2.Length)
							{
								gameObject2.transform.position = this.m_Actor.GetBonePosition(array2[2]);
							}
							if (nkcanimationEventTemplet.m_FloatValue != 0f)
							{
								gameObject2.transform.localScale = Vector3.one * nkcanimationEventTemplet.m_FloatValue;
							}
							NKCAnimationInstance.EffectData value2 = default(NKCAnimationInstance.EffectData);
							value2.m_objEffect = gameObject2;
							if (nkcanimationEventTemplet.m_FloatValue2 > 0f)
							{
								value2.m_fEffectEndTime = NKCSynchronizedTime.GetServerUTCTime(0.0).AddSeconds((double)nkcanimationEventTemplet.m_FloatValue2);
							}
							else
							{
								value2.m_fEffectEndTime = DateTime.MaxValue;
							}
							this.m_dicEffect.Add(this.GetEffectKey(), value2);
						}
						break;
					}
					case AnimationEventType.SET_MOVE_SPEED:
						this.m_fSpeed = nkcanimationEventTemplet.m_FloatValue;
						break;
					case AnimationEventType.SET_POSITION:
						this.m_NormalizedPos = nkcanimationEventTemplet.m_FloatValue;
						break;
					case AnimationEventType.PLAY_SOUND:
						if (nkcanimationEventTemplet.m_FloatValue == 0f || NKMRandom.Range(0f, 1f) <= nkcanimationEventTemplet.m_FloatValue)
						{
							NKCSoundManager.PlaySound(nkcanimationEventTemplet.m_StrValue, 1f, 0f, 0f, false, 0f, false, 0f);
						}
						break;
					case AnimationEventType.PLAY_EMOTION:
						if (this.m_Actor != null && (nkcanimationEventTemplet.m_FloatValue2 == 0f || NKMRandom.Range(0f, 1f) <= nkcanimationEventTemplet.m_FloatValue2))
						{
							this.m_Actor.PlayEmotion(nkcanimationEventTemplet.m_StrValue, nkcanimationEventTemplet.m_FloatValue);
						}
						break;
					case AnimationEventType.SET_ABSOLUTE_MOVE_SPEED:
					{
						float magnitude = (this.m_endPos - this.m_startPos).magnitude;
						if (magnitude == 0f)
						{
							this.m_fSpeed = 100f;
						}
						else
						{
							this.m_fSpeed = nkcanimationEventTemplet.m_FloatValue / magnitude;
						}
						break;
					}
					case AnimationEventType.INVERT_MODEL_X:
					{
						bool boolValue = nkcanimationEventTemplet.m_BoolValue;
						this.m_Actor.Transform.localScale = new Vector3(boolValue ? -1f : (1f * Mathf.Abs(this.m_Actor.Transform.localScale.x)), this.m_Actor.Transform.localScale.y, this.m_Actor.Transform.localScale.z);
						break;
					}
					case AnimationEventType.INVERT_MODEL_X_BY_DIRECTION:
					{
						Vector3 vector = this.m_Actor.Transform.parent.TransformPoint(this.m_startPos);
						bool flag = this.m_Actor.Transform.parent.TransformPoint(this.m_endPos).x < vector.x == nkcanimationEventTemplet.m_BoolValue;
						this.m_Actor.Transform.localScale = new Vector3(flag ? -1f : (1f * Mathf.Abs(this.m_Actor.Transform.localScale.x)), this.m_Actor.Transform.localScale.y, this.m_Actor.Transform.localScale.z);
						break;
					}
					case AnimationEventType.FLIP_MODEL_X:
						this.m_Actor.Transform.localScale = new Vector3(this.m_Actor.Transform.localScale.x * -1f, this.m_Actor.Transform.localScale.y, this.m_Actor.Transform.localScale.z);
						break;
					case AnimationEventType.OFFICE_CHAR_SHADOW:
						if (this.m_Actor != null && this.m_Actor.Transform != null)
						{
							NKCOfficeCharacter component = this.m_Actor.Transform.GetComponent<NKCOfficeCharacter>();
							if (component != null)
							{
								component.SetShadow(nkcanimationEventTemplet.m_BoolValue);
							}
						}
						break;
					}
					this.m_lstActivated[i] = true;
				}
			}
			if (this.m_Actor != null && this.m_bIsMoveAnimtion)
			{
				this.m_NormalizedPos += this.m_fSpeed * deltaTime;
				if (this.m_NormalizedPos > 1f)
				{
					this.m_NormalizedPos = 1f;
				}
				this.m_Actor.Transform.localPosition = NKCUtil.Lerp(this.m_startPos, this.m_endPos, this.m_NormalizedPos);
			}
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0016D1E4 File Offset: 0x0016B3E4
		public bool IsFinished()
		{
			if (this.m_bForceFinished)
			{
				return true;
			}
			for (int i = 0; i < this.m_lstAniEvent.Count; i++)
			{
				if (!this.m_lstActivated[i])
				{
					return false;
				}
			}
			if (this.m_bIsMoveAnimtion)
			{
				if (this.m_NormalizedPos < 1f)
				{
					return false;
				}
			}
			else if (this.m_LastAnimationTemplet != null && !this.m_LastAnimationTemplet.m_BoolValue)
			{
				switch (this.m_LastAnimationTemplet.m_AniEventType)
				{
				case AnimationEventType.ANIMATION_SPINE:
					if (this.m_Actor != null && !this.m_Actor.IsSpineAnimationFinished(NKCAnimationEventManager.GetAnimationType(this.m_LastAnimationTemplet.m_StrValue)))
					{
						return false;
					}
					break;
				case AnimationEventType.ANIMATION_NAME_SPINE:
					if (this.m_Actor != null && !this.m_Actor.IsSpineAnimationFinished(this.m_LastAnimationTemplet.m_StrValue))
					{
						return false;
					}
					break;
				case AnimationEventType.ANIMATION_UNITY:
					if (this.m_Actor != null && this.m_Actor.Animator != null && this.m_Actor.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0016D304 File Offset: 0x0016B504
		public void RemoveEffect()
		{
			foreach (KeyValuePair<int, NKCAnimationInstance.EffectData> keyValuePair in this.m_dicEffect)
			{
				UnityEngine.Object.Destroy(keyValuePair.Value.m_objEffect);
			}
			this.m_dicEffect.Clear();
			this.m_iEffectKey = 0;
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0016D374 File Offset: 0x0016B574
		public void Close()
		{
			if (this.m_targetObj != null)
			{
				if (this.m_targetObj.GetComponent<NKCASUISpineIllust>() != null)
				{
					this.m_targetObj.GetComponent<NKCASUISpineIllust>().Unload();
				}
				else
				{
					UnityEngine.Object.Destroy(this.m_targetObj);
				}
				this.m_targetObj = null;
			}
			this.RemoveEffect();
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0016D3C8 File Offset: 0x0016B5C8
		public float GetTotalEventAnimationTime()
		{
			List<NKCAnimationEventTemplet> list = this.m_lstAniEvent.FindAll((NKCAnimationEventTemplet x) => x.m_AniEventType == AnimationEventType.SET_MOVE_SPEED);
			list.Sort(new Comparison<NKCAnimationEventTemplet>(this.CompByStartTime));
			float num = 0f;
			float num2 = 0f;
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					float num3;
					if (i == 0)
					{
						num2 += list[0].m_StartTime * this.m_fSpeed;
						num += list[0].m_StartTime;
						num3 = 1f - num2;
					}
					else
					{
						num2 += (list[i].m_StartTime - list[i - 1].m_StartTime) * list[i - 1].m_FloatValue;
						num = list[i].m_StartTime;
						num3 = 1f - num2;
					}
					if (i == list.Count - 1)
					{
						num += num3 / list[i].m_FloatValue;
					}
				}
			}
			return num;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0016D4E3 File Offset: 0x0016B6E3
		private int CompByStartTime(NKCAnimationEventTemplet left, NKCAnimationEventTemplet right)
		{
			return left.m_StartTime.CompareTo(right.m_StartTime);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0016D4F8 File Offset: 0x0016B6F8
		public void DrawDebugLine(Color color)
		{
			if (this.m_Actor != null && this.m_Actor.Transform.parent != null)
			{
				Debug.DrawLine(this.m_Actor.Transform.parent.TransformPoint(this.m_startPos), this.m_Actor.Transform.parent.TransformPoint(this.m_endPos), color);
			}
		}

		// Token: 0x04003BD6 RID: 15318
		public INKCAnimationActor m_Actor;

		// Token: 0x04003BD7 RID: 15319
		private GameObject m_targetObj;

		// Token: 0x04003BD8 RID: 15320
		private List<NKCAnimationEventTemplet> m_lstAniEvent = new List<NKCAnimationEventTemplet>();

		// Token: 0x04003BD9 RID: 15321
		private List<bool> m_lstActivated = new List<bool>();

		// Token: 0x04003BDA RID: 15322
		private Transform m_effectParentTr;

		// Token: 0x04003BDB RID: 15323
		private Vector3 m_startPos;

		// Token: 0x04003BDC RID: 15324
		private Vector3 m_endPos;

		// Token: 0x04003BDD RID: 15325
		private float m_fSpeed;

		// Token: 0x04003BDE RID: 15326
		private float m_NormalizedPos;

		// Token: 0x04003BDF RID: 15327
		private Dictionary<int, NKCAnimationInstance.EffectData> m_dicEffect = new Dictionary<int, NKCAnimationInstance.EffectData>();

		// Token: 0x04003BE0 RID: 15328
		private int m_iEffectKey;

		// Token: 0x04003BE1 RID: 15329
		private bool m_bForceFinished;

		// Token: 0x04003BE2 RID: 15330
		private bool m_bIsMoveAnimtion;

		// Token: 0x04003BE3 RID: 15331
		private NKCAnimationEventTemplet m_LastAnimationTemplet;

		// Token: 0x04003BE4 RID: 15332
		private float m_Time;

		// Token: 0x02001457 RID: 5207
		public struct EffectData
		{
			// Token: 0x04009E1C RID: 40476
			public DateTime m_fEffectEndTime;

			// Token: 0x04009E1D RID: 40477
			public GameObject m_objEffect;
		}
	}
}

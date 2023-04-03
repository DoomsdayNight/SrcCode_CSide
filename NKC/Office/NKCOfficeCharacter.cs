using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using ClientPacket.Common;
using Cs.Logging;
using NKC.Templet;
using NKC.Templet.Office;
using NKC.UI;
using NKC.UI.Component;
using NKC.UI.Component.Office;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.Office
{
	// Token: 0x0200082A RID: 2090
	public class NKCOfficeCharacter : MonoBehaviour, INKCAnimationActor, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x060052D0 RID: 21200 RVA: 0x00193E1F File Offset: 0x0019201F
		Animator INKCAnimationActor.Animator
		{
			get
			{
				return this.m_animator;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x060052D1 RID: 21201 RVA: 0x00193E27 File Offset: 0x00192027
		Transform INKCAnimationActor.SDParent
		{
			get
			{
				return this.m_trSDParent;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x00193E2F File Offset: 0x0019202F
		Transform INKCAnimationActor.Transform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060052D3 RID: 21203 RVA: 0x00193E37 File Offset: 0x00192037
		public NKCOfficeBuildingBase OfficeBuilding
		{
			get
			{
				return this.m_OfficeBuilding;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x00193E3F File Offset: 0x0019203F
		private NKCOfficeFloorBase Floor
		{
			get
			{
				return this.m_OfficeBuilding.m_Floor;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060052D5 RID: 21205 RVA: 0x00193E4C File Offset: 0x0019204C
		private long[,] FloorMap
		{
			get
			{
				NKCOfficeBuildingBase officeBuilding = this.m_OfficeBuilding;
				if (officeBuilding == null)
				{
					return null;
				}
				return officeBuilding.FloorMap;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060052D6 RID: 21206 RVA: 0x00193E5F File Offset: 0x0019205F
		// (set) Token: 0x060052D7 RID: 21207 RVA: 0x00193E67 File Offset: 0x00192067
		public int UnitID { get; private set; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060052D8 RID: 21208 RVA: 0x00193E70 File Offset: 0x00192070
		// (set) Token: 0x060052D9 RID: 21209 RVA: 0x00193E78 File Offset: 0x00192078
		public int SkinID { get; private set; }

		// Token: 0x060052DA RID: 21210 RVA: 0x00193E81 File Offset: 0x00192081
		public static NKCOfficeCharacter GetInstance(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return null;
			}
			return NKCOfficeCharacter.GetInstance(unitData.m_UnitID, unitData.m_SkinID);
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00193E9C File Offset: 0x0019209C
		public static NKCOfficeCharacter GetInstance(int unitID, int skinID)
		{
			NKCOfficeCharacterTemplet nkcofficeCharacterTemplet = NKCOfficeCharacterTemplet.Find(unitID, skinID);
			NKMAssetName cNKMAssetName;
			if (nkcofficeCharacterTemplet != null && !string.IsNullOrEmpty(nkcofficeCharacterTemplet.PrefabAsset))
			{
				cNKMAssetName = new NKMAssetName("AB_UNIT_OFFICE_SD", nkcofficeCharacterTemplet.PrefabAsset);
			}
			else
			{
				cNKMAssetName = new NKMAssetName("AB_UNIT_OFFICE_SD", "UNIT_OFFICE_SD");
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(NKCResourceUtility.GetOrLoadAssetResource<GameObject>(cNKMAssetName));
			NKCOfficeCharacter component = gameObject.GetComponent<NKCOfficeCharacter>();
			if (component == null)
			{
				Debug.LogError("NKCUIOfficeCharacter loadprefab failed!");
				UnityEngine.Object.DestroyImmediate(gameObject);
				return null;
			}
			component.SetSpineIllust(NKCResourceUtility.OpenSpineSD(unitID, skinID, false), true);
			if (component.m_SDIllust != null)
			{
				component.m_SDIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				component.m_SDIllust.GetRectTransform().localPosition = Vector3.zero;
				component.m_SDIllust.GetRectTransform().pivot = new Vector2(0.5f, 0.5f);
				component.m_SDIllust.GetRectTransform().anchorMin = new Vector2(0.5f, 0.5f);
				component.m_SDIllust.GetRectTransform().anchorMax = new Vector2(0.5f, 0.5f);
				component.m_SDIllust.GetRectTransform().SetWidth(100f);
				component.m_SDIllust.GetRectTransform().SetHeight(100f);
				component.transform.rotation = Quaternion.identity;
				return component;
			}
			Debug.LogError(string.Format("SD Illust load failed!! unitID {0}, skinID {1}", unitID, skinID));
			UnityEngine.Object.DestroyImmediate(gameObject);
			return null;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x00194018 File Offset: 0x00192218
		public static NKCOfficeCharacter GetInstance(NKMAssetName sdAssetName)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(NKCResourceUtility.GetOrLoadAssetResource<GameObject>(new NKMAssetName("AB_UNIT_OFFICE_SD", "UNIT_OFFICE_SD")));
			NKCOfficeCharacter component = gameObject.GetComponent<NKCOfficeCharacter>();
			if (component == null)
			{
				Debug.LogError("NKCUIOfficeCharacter loadprefab failed!");
				UnityEngine.Object.DestroyImmediate(gameObject);
				return null;
			}
			component.SetSpineIllust(NKCResourceUtility.OpenSpineSD(sdAssetName.m_BundleName, sdAssetName.m_BundleName, false), true);
			if (component.m_SDIllust != null)
			{
				component.m_SDIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				component.m_SDIllust.GetRectTransform().localPosition = Vector3.zero;
			}
			component.transform.rotation = Quaternion.identity;
			return component;
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x001940C0 File Offset: 0x001922C0
		public void Init(NKCOfficeBuildingBase officeBuilding, NKMUnitData unitData)
		{
			this.CommonInit(officeBuilding, unitData.m_UnitID, unitData.m_SkinID);
			this.m_UnitData = unitData;
			NKCUtil.SetGameobjectActive(this.m_comLoyalty, unitData != null);
			if (unitData != null && this.m_comLoyalty != null)
			{
				this.m_comLoyalty.SetData(this.m_UnitData);
			}
			NKCUtil.SetGameobjectActive(this.m_comFriendInfo, false);
			this.m_FriendUID = 0L;
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0019412C File Offset: 0x0019232C
		public void Init(NKCOfficeBuildingBase officeBuilding, NKMUserProfileData profileData)
		{
			this.CommonInit(officeBuilding, profileData.commonProfile.mainUnitId, profileData.commonProfile.mainUnitSkinId);
			NKCUtil.SetGameobjectActive(this.m_comLoyalty, false);
			NKCUtil.SetGameobjectActive(this.m_comFriendInfo, true);
			this.m_FriendUID = profileData.commonProfile.userUid;
			this.m_comFriendInfo.SetData(profileData);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0019418B File Offset: 0x0019238B
		public void Init(NKCOfficeBuildingBase officeBuilding, int unitID, int skinID)
		{
			this.CommonInit(officeBuilding, unitID, skinID);
			NKCUtil.SetGameobjectActive(this.m_comLoyalty, false);
			NKCUtil.SetGameobjectActive(this.m_comFriendInfo, false);
			this.m_FriendUID = 0L;
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x001941B8 File Offset: 0x001923B8
		private void CommonInit(NKCOfficeBuildingBase officeBuilding, int unitID, int skinID)
		{
			this.UnitID = unitID;
			this.SkinID = skinID;
			this.m_bTakeHeartSent = false;
			this.m_OfficeBuilding = officeBuilding;
			this.BT = base.GetComponent<BehaviorTree>();
			if (this.BT == null)
			{
				Debug.LogWarning("Office SD : BT Not found. Using Default BT");
				this.BT = base.gameObject.AddComponent<BehaviorTree>();
				this.BT.StartWhenEnabled = false;
			}
			else
			{
				this.BT.StartWhenEnabled = false;
			}
			this.BT.DisableBehavior();
			NKCOfficeCharacterTemplet nkcofficeCharacterTemplet = NKCOfficeCharacterTemplet.Find(unitID, skinID);
			if (nkcofficeCharacterTemplet != null && !string.IsNullOrEmpty(nkcofficeCharacterTemplet.BTAsset))
			{
				ExternalBehavior externalBehavior = this.LoadBT("ab_ui_office_bt", nkcofficeCharacterTemplet.BTAsset);
				if (externalBehavior != null)
				{
					this.BT.ExternalBehavior = externalBehavior;
					Log.Info("[NKCOfficeCharacter] External Behaivor " + nkcofficeCharacterTemplet.BTAsset + " loaded", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Office/NKCOfficeCharacter.cs", 248);
					NKCDebugUtil.LogBehaivorTree(this.BT);
				}
				else
				{
					Log.Warn("[NKCOfficeCharacter] External Behaivor " + nkcofficeCharacterTemplet.BTAsset + " load failed, using default", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Office/NKCOfficeCharacter.cs", 255);
					externalBehavior = this.LoadBT("ab_ui_office_bt", "OFFICE_BT_DEFAULT");
					this.BT.ExternalBehavior = externalBehavior;
				}
			}
			else
			{
				ExternalBehavior externalBehavior2 = this.LoadBT("ab_ui_office_bt", "OFFICE_BT_DEFAULT");
				this.BT.ExternalBehavior = externalBehavior2;
			}
			this.AddCommonBTVariable();
			this.ApplyBTVariables(nkcofficeCharacterTemplet);
			this.BT.RestartWhenComplete = true;
			this.BT.OnBehaviorRestart += this.OnBTRestart;
			base.transform.SetParent(officeBuilding.trActorRoot);
			if (this.m_comEmotion != null)
			{
				this.m_comEmotion.Init();
			}
			this.m_lstUnitInteractionCache = null;
			this.m_lstSoloInteractionCache = null;
			this.SetCheckIntervalCooltime();
			this.SetShadow(true);
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x00194388 File Offset: 0x00192588
		private ExternalBehavior LoadBT(string bundleName, string assetName)
		{
			ExternalBehavior orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<ExternalBehavior>(bundleName, assetName, false);
			if (orLoadAssetResource == null)
			{
				return null;
			}
			ExternalBehavior externalBehavior = UnityEngine.Object.Instantiate<ExternalBehavior>(orLoadAssetResource);
			externalBehavior.Init();
			return externalBehavior;
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x001943B5 File Offset: 0x001925B5
		private void AddCommonBTVariable()
		{
			if (this.BT.GetVariable("GrabEmotion") == null)
			{
				this.BT.SetVariable("GrabEmotion", new SharedString());
			}
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x001943E0 File Offset: 0x001925E0
		private void ApplyBTVariables(NKCOfficeCharacterTemplet templet)
		{
			if (templet == null || string.IsNullOrEmpty(templet.Variables))
			{
				return;
			}
			foreach (KeyValuePair<string, string> keyValuePair in NKCUtil.ParseStringTable(templet.Variables))
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				SharedVariable variable = this.BT.GetVariable(key);
				if (variable == null)
				{
					Debug.LogError("BT variable not found! name : " + key);
				}
				else
				{
					Debug.Log("Set Variable <" + key + "> : " + value);
					if (variable is SharedString)
					{
						this.BT.SetVariableValue(key, value.Trim());
					}
					else if (variable is SharedInt)
					{
						int num;
						if (int.TryParse(value, out num))
						{
							this.BT.SetVariableValue(key, num);
						}
						else
						{
							Debug.LogError(string.Format("[OfficeCharacterTemplet] {0} {1} BT Variable parse failed : {2} not followed by int(param {3}).", new object[]
							{
								templet.Type,
								templet.ID,
								key,
								value
							}));
						}
					}
					else if (variable is SharedBool)
					{
						bool flag;
						if (bool.TryParse(value, out flag))
						{
							this.BT.SetVariableValue(key, flag);
						}
						else
						{
							Debug.LogError(string.Format("[OfficeCharacterTemplet] {0} {1} BT Variable parse failed : {2} not followed by bool(param {3}).", new object[]
							{
								templet.Type,
								templet.ID,
								key,
								value
							}));
						}
					}
					else if (variable is SharedFloat)
					{
						float num2;
						if (float.TryParse(value, out num2))
						{
							this.BT.SetVariableValue(key, num2);
						}
						else
						{
							Debug.LogError(string.Format("OfficeCharacterTemplet : {0} {1} BT Variable parse failed : {2} not followed by float(param {3}).", new object[]
							{
								templet.Type,
								templet.ID,
								key,
								value
							}));
						}
					}
					else if (variable is BTSharedNKCValue)
					{
						BTSharedNKCValue btsharedNKCValue = variable as BTSharedNKCValue;
						if (btsharedNKCValue.TryParse(value))
						{
							this.BT.SetVariable(key, btsharedNKCValue);
						}
						else
						{
							Debug.LogError(string.Format("OfficeCharacterTemplet : {0} {1} BT Variable parse {2} failed(param {3}).", new object[]
							{
								templet.Type,
								templet.ID,
								key,
								value
							}));
						}
					}
					else if (variable is SharedUInt)
					{
						uint num3;
						if (uint.TryParse(value, out num3))
						{
							this.BT.SetVariableValue(key, num3);
						}
						else
						{
							Debug.LogError(string.Format("OfficeCharacterTemplet : {0} {1} BT Variable parse failed : {2} not followed by uint(param {3}).", new object[]
							{
								templet.Type,
								templet.ID,
								key,
								value
							}));
						}
					}
					else
					{
						Debug.LogError(string.Format("OfficeCharacterTemplet : {0} {1} BT Variable parse failed - Unexpected type", templet.Type, templet.ID));
					}
				}
			}
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x001946F4 File Offset: 0x001928F4
		public void SetEnableExtraUI(bool value)
		{
			if (value)
			{
				NKCUtil.SetGameobjectActive(this.m_comLoyalty, this.m_UnitData != null);
				if (this.m_UnitData != null && this.m_comLoyalty != null)
				{
					this.m_comLoyalty.SetData(this.m_UnitData);
				}
				NKCUtil.SetGameobjectActive(this.m_comFriendInfo, this.m_FriendUID > 0L);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_comLoyalty, false);
			NKCUtil.SetGameobjectActive(this.m_comFriendInfo, false);
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0019476D File Offset: 0x0019296D
		public void OnUnitUpdated(NKMUnitData unitData)
		{
			this.m_UnitData = unitData;
			if (this.m_comLoyalty != null)
			{
				this.m_comLoyalty.SetData(this.m_UnitData);
			}
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x00194798 File Offset: 0x00192998
		public void OnUnitTakeHeart(NKMUnitData unitData)
		{
			this.m_bTakeHeartSent = false;
			this.m_UnitData = unitData;
			if (this.m_comLoyalty != null)
			{
				this.m_comLoyalty.SetData(this.m_UnitData);
				this.m_comLoyalty.PlayTakeHeartEffect();
			}
			this.SetState(NKCOfficeCharacter.State.AI, true);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x001947E5 File Offset: 0x001929E5
		public void Cleanup()
		{
			this.UnregisterInteraction();
			this.CleanupAnimInstances();
			this.CleanupCharacter();
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x001947F9 File Offset: 0x001929F9
		private void CleanupCharacter()
		{
			if (this.m_SDIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_SDIllust);
			}
			this.m_SDIllust = null;
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0019481F File Offset: 0x00192A1F
		protected void OnBTRestart(Behavior behavior)
		{
			this.CleanupAnimInstances();
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x00194828 File Offset: 0x00192A28
		public void StopAllAnimInstances()
		{
			this.m_qAnimInstances.Clear();
			if (this.m_animInstance != null)
			{
				this.m_lstFinishedInstances.Add(this.m_animInstance);
			}
			if (this.m_emotionAnimInstance != null)
			{
				this.m_lstFinishedInstances.Add(this.m_emotionAnimInstance);
			}
			this.m_animInstance = null;
			this.m_emotionAnimInstance = null;
			this.SetShadow(true);
			this.CleanupFinishedInstances();
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x00194890 File Offset: 0x00192A90
		private void CleanupAnimInstances()
		{
			this.m_qAnimInstances.Clear();
			if (this.m_animInstance != null)
			{
				this.m_animInstance.RemoveEffect();
				this.m_animInstance = null;
			}
			if (this.m_emotionAnimInstance != null)
			{
				this.m_emotionAnimInstance.RemoveEffect();
				this.m_emotionAnimInstance = null;
			}
			this.CleanupFinishedInstances();
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x001948E4 File Offset: 0x00192AE4
		private void CleanupFinishedInstances()
		{
			foreach (NKCAnimationInstance nkcanimationInstance in this.m_lstFinishedInstances)
			{
				if (nkcanimationInstance != null)
				{
					nkcanimationInstance.RemoveEffect();
				}
			}
			this.m_lstFinishedInstances.Clear();
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x00194944 File Offset: 0x00192B44
		private void SetCheckIntervalCooltime()
		{
			this.m_fInteractionCheckInterval = (float)UnityEngine.Random.Range(1, 2);
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x00194954 File Offset: 0x00192B54
		public void SetFurnitureInteractionCooltime()
		{
			this.m_fFurnitureInteractionCooltime = UnityEngine.Random.Range(NKMCommonConst.Office.OfficeInteraction.ActInteriorCoolTime, NKMCommonConst.Office.OfficeInteraction.ActInteriorCoolTime * 1.5f);
			this.SetCheckIntervalCooltime();
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0019498B File Offset: 0x00192B8B
		public void ResetUnitInteractionCooltime()
		{
			this.m_fUnitInteractionCooltime = 0f;
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x00194998 File Offset: 0x00192B98
		public void SetUnitInteractionCooltime()
		{
			this.m_fUnitInteractionCooltime = UnityEngine.Random.Range(NKMCommonConst.Office.OfficeInteraction.ActUnitCoolTime, NKMCommonConst.Office.OfficeInteraction.ActUnitCoolTime * 1.5f);
			this.SetCheckIntervalCooltime();
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x001949CF File Offset: 0x00192BCF
		public void SetSoloInteractionCooltime()
		{
			this.m_fSoloInteractionCooltime = UnityEngine.Random.Range(NKMCommonConst.Office.OfficeInteraction.ActSoloCoolTime, NKMCommonConst.Office.OfficeInteraction.ActSoloCoolTime * 1.5f);
			this.SetCheckIntervalCooltime();
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00194A08 File Offset: 0x00192C08
		protected virtual void Update()
		{
			this.m_bRectWorldValid = false;
			this.m_fInteractionCheckInterval -= Time.deltaTime;
			this.m_fFurnitureInteractionCooltime -= Time.deltaTime;
			this.m_fUnitInteractionCooltime -= Time.deltaTime;
			this.m_fSoloInteractionCooltime -= Time.deltaTime;
			NKCOfficeCharacter.State eState = this.m_eState;
			if (eState != NKCOfficeCharacter.State.AI)
			{
				if (eState != NKCOfficeCharacter.State.WaitGrab)
				{
					return;
				}
				if (Input.touchCount > 1)
				{
					this.SetState(NKCOfficeCharacter.State.AI, false);
					return;
				}
				if (this.m_fGrabWaitTime < this.GRAB_WAIT_TIME)
				{
					this.m_fGrabWaitTime += Time.deltaTime;
					return;
				}
				if (!this.m_bCanGrab)
				{
					return;
				}
				if (!NKCUIHoldLoading.IsOpen)
				{
					NKCUIHoldLoading.Instance.Open(this.m_touchPos, this.grabUITime);
					return;
				}
				if (!NKCUIHoldLoading.Instance.IsPlaying())
				{
					this.SetState(NKCOfficeCharacter.State.Grab, false);
				}
			}
			else
			{
				if (this.m_animInstance != null)
				{
					if (this.m_animInstance.IsFinished())
					{
						this.m_lstFinishedInstances.Add(this.m_animInstance);
						this.m_animInstance = null;
					}
					else
					{
						this.m_animInstance.Update(Time.deltaTime);
					}
				}
				if (this.m_emotionAnimInstance != null)
				{
					if (this.m_emotionAnimInstance.IsFinished())
					{
						this.m_lstFinishedInstances.Add(this.m_emotionAnimInstance);
						this.m_emotionAnimInstance = null;
					}
					else
					{
						this.m_emotionAnimInstance.Update(Time.deltaTime);
					}
				}
				if (this.m_animInstance == null && this.m_qAnimInstances.Count > 0)
				{
					this.m_animInstance = this.m_qAnimInstances.Dequeue();
				}
				if (!this.HasInteractionTarget() && this.m_fInteractionCheckInterval <= 0f)
				{
					this.SetCheckIntervalCooltime();
					this.CheckFurnitureInteraction();
					this.CheckUnitInteraction();
					this.CheckSoloInteraction();
					return;
				}
			}
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x00194BBD File Offset: 0x00192DBD
		public void EnqueueAnimation(List<NKCAnimationEventTemplet> lstAnimEvent)
		{
			this.EnqueueAnimation(new NKCAnimationInstance(this, this.m_OfficeBuilding.transform, lstAnimEvent, base.transform.localPosition, base.transform.localPosition));
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00194BED File Offset: 0x00192DED
		public void EnqueueAnimation(NKCAnimationInstance instance)
		{
			this.m_qAnimInstances.Enqueue(instance);
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x00194BFB File Offset: 0x00192DFB
		private void PlayEmotionAnimation(List<NKCAnimationEventTemplet> lstAnim)
		{
			this.PlayEmotionAnimation(new NKCAnimationInstance(this, this.m_OfficeBuilding.transform, lstAnim, base.transform.localPosition, base.transform.localPosition));
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00194C2B File Offset: 0x00192E2B
		private void PlayEmotionAnimation(NKCAnimationInstance instance)
		{
			if (this.m_emotionAnimInstance != null)
			{
				this.m_lstFinishedInstances.Add(this.m_emotionAnimInstance);
				this.m_emotionAnimInstance = null;
			}
			this.m_emotionAnimInstance = instance;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x00194C54 File Offset: 0x00192E54
		public bool PlayAnimCompleted()
		{
			return this.m_qAnimInstances.Count == 0 && this.m_animInstance == null;
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x00194C6E File Offset: 0x00192E6E
		private void ClearHolding()
		{
			if (NKCUIHoldLoading.IsOpen)
			{
				NKCUIHoldLoading.Instance.Close();
			}
			this.m_fGrabWaitTime = 0f;
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00194C8C File Offset: 0x00192E8C
		public void StartAI()
		{
			this.SetState(NKCOfficeCharacter.State.AI, true);
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00194C98 File Offset: 0x00192E98
		private void SetState(NKCOfficeCharacter.State state, bool bForce = false)
		{
			if (!bForce && state == this.m_eState)
			{
				return;
			}
			this.ClearHolding();
			bool flag = false;
			bool flag2 = false;
			if (state != NKCOfficeCharacter.State.AI)
			{
				if (this.PlayingInteractionAnimation)
				{
					this.UnregisterInteraction();
				}
				this.StopAllAnimInstances();
				this.BT.DisableBehavior();
				if (this.m_comEmotion != null)
				{
					this.m_comEmotion.Stop();
				}
			}
			if (this.m_eState == NKCOfficeCharacter.State.Grab)
			{
				base.transform.SetParent(this.m_OfficeBuilding.trActorRoot, true);
				this.PlayEmotion(NKCUIComCharacterEmotion.Type.NONE, 1f);
				flag2 = true;
			}
			switch (state)
			{
			case NKCOfficeCharacter.State.AI:
				this.BT.EnableBehavior();
				this.BT.Start();
				break;
			case NKCOfficeCharacter.State.WaitGrab:
				this.PlaySpineAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 1f, false);
				this.m_fGrabWaitTime = 0f;
				break;
			case NKCOfficeCharacter.State.Grab:
			{
				this.PlaySpineAnimation(NKCASUIUnitIllust.eAnimation.SD_DRAG, true, 1f, false);
				string btvalue = this.GetBTValue<string>("GrabEmotion", "");
				if (string.IsNullOrEmpty(btvalue))
				{
					this.PlayEmotion(NKCUIComCharacterEmotion.Type.Sweat, 1f);
				}
				else
				{
					this.PlayEmotion(btvalue, 1f);
				}
				base.transform.SetParent(this.Floor.m_rtSelectedFunitureRoot, true);
				flag = true;
				this.UnregisterInteraction();
				break;
			}
			}
			this.m_eState = state;
			if (flag)
			{
				this.m_OfficeBuilding.OnCharacterBeginDrag(this);
				return;
			}
			if (flag2)
			{
				this.m_OfficeBuilding.OnCharacterEndDrag(this);
			}
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x00194E04 File Offset: 0x00193004
		public Vector3 GetLocalPos(ValueTuple<int, int> pos, bool bRandomize = true)
		{
			return this.GetLocalPos(pos.Item1, pos.Item2, bRandomize);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00194E19 File Offset: 0x00193019
		public Vector3 GetLocalPos(OfficeFloorPosition pos, bool bRandomize = true)
		{
			return this.GetLocalPos(pos.x, pos.y, bRandomize);
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00194E30 File Offset: 0x00193030
		public Vector3 GetLocalPos(int x, int y, bool bRandomize = true)
		{
			Vector3 localPos = this.Floor.GetLocalPos(x, y);
			if (bRandomize)
			{
				localPos.x += NKMRandom.Range(-this.m_OfficeBuilding.TileSize * 0.25f, this.m_OfficeBuilding.TileSize * 0.25f);
				localPos.y += NKMRandom.Range(-this.m_OfficeBuilding.TileSize * 0.25f, this.m_OfficeBuilding.TileSize * 0.25f);
			}
			return localPos;
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x00194EB8 File Offset: 0x001930B8
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_bTakeHeartSent)
			{
				return;
			}
			if (this.m_eState == NKCOfficeCharacter.State.AI)
			{
				this.SetState(NKCOfficeCharacter.State.WaitGrab, false);
				this.m_touchPos = eventData.position;
				Vector3 localPosFromScreenPos = this.Floor.GetLocalPosFromScreenPos(this.m_touchPos);
				this.m_touchLocalOffset = base.transform.localPosition - localPosFromScreenPos;
			}
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00194F14 File Offset: 0x00193114
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_eState == NKCOfficeCharacter.State.WaitGrab && this.m_fGrabWaitTime < this.GRAB_WAIT_TIME && !this.OnTouchAction() && this.m_bCanTouch)
			{
				this.PlayTouchAnimation();
				this.PlayTouchVoice();
			}
			this.SetState(NKCOfficeCharacter.State.AI, false);
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x00194F54 File Offset: 0x00193154
		protected virtual bool OnTouchAction()
		{
			if (this.m_bTakeHeartSent)
			{
				return true;
			}
			if (this.m_UnitData != null && this.m_UnitData.CheckOfficeRoomHeartFull())
			{
				this.PlayTakeHeartAnimation();
				this.PlayTakeHeartVoice();
				this.m_bTakeHeartSent = true;
				NKCPacketSender.Send_NKMPacket_OFFICE_TAKE_HEART_REQ(this.m_UnitData.m_UnitUID);
				return true;
			}
			if (this.m_UnitData == null && this.m_FriendUID > 0L)
			{
				NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_FriendUID, NKM_DECK_TYPE.NDT_NORMAL);
				return true;
			}
			return this.dOnClick != null && this.dOnClick();
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x00194FDD File Offset: 0x001931DD
		protected virtual void PlayTouchVoice()
		{
			if (this.m_UnitData == null)
			{
				return;
			}
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_TOUCH, this.m_UnitData, false, true);
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x00194FF7 File Offset: 0x001931F7
		private void PlayTouchAnimation()
		{
			this.EnqueueSimpleAni(NKCASUIUnitIllust.eAnimation.SD_TOUCH, false);
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x00195005 File Offset: 0x00193205
		private void PlayTakeHeartVoice()
		{
			NKMUnitData unitData = this.m_UnitData;
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x0019500E File Offset: 0x0019320E
		private void PlayTakeHeartAnimation()
		{
			this.EnqueueSimpleAni(NKCASUIUnitIllust.eAnimation.SD_WIN, true);
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0019501C File Offset: 0x0019321C
		public void EnqueueSimpleAni(string animName, bool bNow, bool bInvert)
		{
			if (bNow)
			{
				this.StopAllAnimInstances();
			}
			float animationTime = this.m_SDIllust.GetAnimationTime(animName);
			string aniEventStrID = "SimpleAni" + animName;
			List<NKCAnimationEventTemplet> list = new List<NKCAnimationEventTemplet>();
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.INVERT_MODEL_X,
				m_BoolValue = bInvert
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.ANIMATION_NAME_SPINE,
				m_StrValue = animName,
				m_FloatValue = 1f,
				m_BoolValue = false
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_MOVE_SPEED,
				m_FloatValue = 0f
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_POSITION,
				m_FloatValue = 0f
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = animationTime,
				m_AniEventType = AnimationEventType.SET_POSITION,
				m_FloatValue = 1f
			});
			NKCAnimationInstance instance = new NKCAnimationInstance(this, this.m_OfficeBuilding.transform, list, base.transform.localPosition, base.transform.localPosition);
			this.EnqueueAnimation(instance);
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x00195174 File Offset: 0x00193374
		private void EnqueueSimpleAni(NKCASUIUnitIllust.eAnimation animation, bool bNow)
		{
			if (bNow)
			{
				this.StopAllAnimInstances();
			}
			float animationTime = this.m_SDIllust.GetAnimationTime(animation);
			string aniEventStrID = "SimpleAni" + animation.ToString();
			List<NKCAnimationEventTemplet> list = new List<NKCAnimationEventTemplet>();
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.ANIMATION_SPINE,
				m_StrValue = animation.ToString(),
				m_FloatValue = 1f,
				m_BoolValue = false
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_MOVE_SPEED,
				m_FloatValue = 0f
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_POSITION,
				m_FloatValue = 0f
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = aniEventStrID,
				m_StartTime = animationTime,
				m_AniEventType = AnimationEventType.SET_POSITION,
				m_FloatValue = 1f
			});
			NKCAnimationInstance instance = new NKCAnimationInstance(this, this.m_OfficeBuilding.transform, list, base.transform.localPosition, base.transform.localPosition);
			this.EnqueueAnimation(instance);
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x001952B7 File Offset: 0x001934B7
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x001952BC File Offset: 0x001934BC
		public void OnDrag(PointerEventData eventData)
		{
			if (this.m_eState != NKCOfficeCharacter.State.Grab)
			{
				return;
			}
			Vector3 vector = this.Floor.GetLocalPosFromScreenPos(eventData.position) + this.m_touchLocalOffset;
			vector = this.Floor.Rect.ClampLocalPos(vector);
			base.transform.localPosition = vector;
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0019530E File Offset: 0x0019350E
		public void OnEndDrag(PointerEventData eventData)
		{
			this.SetState(NKCOfficeCharacter.State.AI, false);
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x00195318 File Offset: 0x00193518
		private void DebugDrawRoute()
		{
			foreach (NKCAnimationInstance nkcanimationInstance in this.m_lstFinishedInstances)
			{
				if (nkcanimationInstance != null)
				{
					nkcanimationInstance.DrawDebugLine(Color.gray);
				}
			}
			foreach (NKCAnimationInstance nkcanimationInstance2 in this.m_qAnimInstances)
			{
				nkcanimationInstance2.DrawDebugLine(Color.green);
			}
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x001953B8 File Offset: 0x001935B8
		public void SetSpineIllust(NKCASUIUnitIllust illust, bool bSetParent)
		{
			if (illust == null)
			{
				return;
			}
			if (bSetParent)
			{
				illust.SetParent(this.m_trSDParent, false);
			}
			this.m_SDIllust = illust;
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x001953D5 File Offset: 0x001935D5
		public NKCASUIUnitIllust GetSpineIllust()
		{
			return this.m_SDIllust;
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x001953DD File Offset: 0x001935DD
		public void PlaySpineAnimation(string name, bool loop, float timeScale)
		{
			if (this.m_SDIllust == null)
			{
				return;
			}
			this.m_SDIllust.SetAnimation(name, loop, 0, true, 0f, false);
			this.m_SDIllust.SetTimeScale(timeScale);
			this.m_SDIllust.InvalidateWorldRect();
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x00195414 File Offset: 0x00193614
		public void PlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, float timeScale, bool bDefaultAnim)
		{
			if (this.m_SDIllust == null)
			{
				return;
			}
			if (bDefaultAnim)
			{
				this.m_SDIllust.SetDefaultAnimation(eAnim, true, false, 0f);
			}
			else
			{
				this.m_SDIllust.SetAnimation(eAnim, loop, 0, true, 0f, false);
			}
			this.m_SDIllust.SetTimeScale(timeScale);
			this.m_SDIllust.InvalidateWorldRect();
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x00195470 File Offset: 0x00193670
		public bool IsSpineAnimationFinished(NKCASUIUnitIllust.eAnimation eAnim)
		{
			if (this.m_SDIllust == null)
			{
				return true;
			}
			string animationName = NKCASUIUnitIllust.GetAnimationName(eAnim);
			return this.IsSpineAnimationFinished(animationName);
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x00195495 File Offset: 0x00193695
		public bool IsSpineAnimationFinished(string name)
		{
			return this.m_SDIllust == null || !(this.m_SDIllust.GetCurrentAnimationName(0) == name) || this.m_SDIllust.GetAnimationTime(name) <= this.m_SDIllust.GetCurrentAnimationTime(0);
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x001954D2 File Offset: 0x001936D2
		public Vector3 GetBonePosition(string name)
		{
			if (this.m_SDIllust == null)
			{
				return Vector3.zero;
			}
			return this.m_SDIllust.GetBoneWorldPosition(name);
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x001954EE File Offset: 0x001936EE
		public bool CanPlaySpineAnimation(string name)
		{
			return this.m_SDIllust != null && this.m_SDIllust.HasAnimation(name);
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x00195506 File Offset: 0x00193706
		public bool CanPlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return this.m_SDIllust != null && this.m_SDIllust.HasAnimation(eAnim);
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0019551E File Offset: 0x0019371E
		public void SetEnableTouch(bool value)
		{
			if (this.m_gpTouchTarget != null)
			{
				this.m_gpTouchTarget.raycastTarget = value;
			}
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x0019553A File Offset: 0x0019373A
		public float GetAnimTime(string animName)
		{
			if (this.m_SDIllust != null)
			{
				return this.m_SDIllust.GetAnimationTime(animName);
			}
			return 0f;
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x00195556 File Offset: 0x00193756
		public float GetAnimTime(NKCASUIUnitIllust.eAnimation anim)
		{
			if (this.m_SDIllust != null)
			{
				return this.m_SDIllust.GetAnimationTime(anim);
			}
			return 0f;
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x00195572 File Offset: 0x00193772
		public string GetCurrentAnimName(int trackIndex = 0)
		{
			if (this.m_SDIllust != null)
			{
				this.m_SDIllust.GetCurrentAnimationName(trackIndex);
			}
			return "";
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x0019558E File Offset: 0x0019378E
		public void SetOnClick(NKCOfficeCharacter.OnClick onClick)
		{
			this.dOnClick = onClick;
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x00195597 File Offset: 0x00193797
		public void SetShadow(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objShadow, value);
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x001955A8 File Offset: 0x001937A8
		public T GetBTValue<T>(string valueName, T defaultValue)
		{
			if (this.BT == null)
			{
				return defaultValue;
			}
			object value = this.BT.GetVariable(valueName).GetValue();
			if (value is T)
			{
				return (T)((object)value);
			}
			return defaultValue;
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x001955EC File Offset: 0x001937EC
		public Rect GetWorldRect()
		{
			if (!this.m_bRectWorldValid)
			{
				Rect worldRect;
				if (this.m_SDIllust != null)
				{
					worldRect = this.m_SDIllust.GetWorldRect(false);
				}
				else if (this.m_gpTouchTarget != null)
				{
					worldRect = this.m_gpTouchTarget.GetComponent<RectTransform>().GetWorldRect();
				}
				else
				{
					worldRect = new Rect(base.transform.position.x, base.transform.position.y, 141f, 315f);
				}
				if (this.m_comLoyalty != null && this.m_comLoyalty.gameObject.activeInHierarchy)
				{
					RectTransform component = this.m_comLoyalty.GetComponent<RectTransform>();
					if (component != null)
					{
						Rect worldRect2 = component.GetWorldRect();
						worldRect.yMax = Mathf.Max(worldRect.yMax, worldRect2.yMax);
					}
				}
				else if (this.m_comFriendInfo != null && this.m_comFriendInfo.gameObject.activeInHierarchy)
				{
					RectTransform component2 = this.m_comFriendInfo.GetComponent<RectTransform>();
					if (component2 != null)
					{
						Rect worldRect3 = component2.GetWorldRect();
						worldRect.xMin = Mathf.Min(worldRect.xMin, worldRect3.xMin);
						worldRect.xMax = Mathf.Max(worldRect.xMax, worldRect3.xMax);
						worldRect.yMax = Mathf.Max(worldRect.yMax, worldRect3.yMax);
					}
				}
				this.m_bRectWorldValid = true;
				this.m_rectWorld = worldRect;
			}
			return this.m_rectWorld;
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x0019576A File Offset: 0x0019396A
		// (set) Token: 0x0600531D RID: 21277 RVA: 0x00195772 File Offset: 0x00193972
		public bool PlayingInteractionAnimation { get; private set; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x0019577B File Offset: 0x0019397B
		// (set) Token: 0x0600531F RID: 21279 RVA: 0x00195783 File Offset: 0x00193983
		public NKCOfficeFuniture CurrentInteractionTargetFurniture { get; private set; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x0019578C File Offset: 0x0019398C
		// (set) Token: 0x06005321 RID: 21281 RVA: 0x00195794 File Offset: 0x00193994
		public NKCOfficeFurnitureInteractionTemplet CurrentFurnitureInteractionTemplet { get; private set; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x0019579D File Offset: 0x0019399D
		// (set) Token: 0x06005323 RID: 21283 RVA: 0x001957A5 File Offset: 0x001939A5
		public NKCOfficeUnitInteractionTemplet CurrentUnitInteractionTemplet { get; private set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x001957AE File Offset: 0x001939AE
		// (set) Token: 0x06005325 RID: 21285 RVA: 0x001957B6 File Offset: 0x001939B6
		public NKCOfficeCharacter CurrentUnitInteractionTarget { get; private set; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x001957BF File Offset: 0x001939BF
		// (set) Token: 0x06005327 RID: 21287 RVA: 0x001957C7 File Offset: 0x001939C7
		public Vector3 CurrentUnitInteractionPosition { get; private set; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x001957D0 File Offset: 0x001939D0
		// (set) Token: 0x06005329 RID: 21289 RVA: 0x001957D8 File Offset: 0x001939D8
		public bool CurrentUnitInteractionIsMainActor { get; private set; }

		// Token: 0x0600532A RID: 21290 RVA: 0x001957E4 File Offset: 0x001939E4
		private void BuildInteractionCache()
		{
			List<NKCOfficeUnitInteractionTemplet> interactionTempletList = NKCOfficeUnitInteractionTemplet.GetInteractionTempletList(this);
			if (interactionTempletList == null)
			{
				this.m_lstUnitInteractionCache = new List<NKCOfficeUnitInteractionTemplet>();
				this.m_lstSoloInteractionCache = new List<NKCOfficeUnitInteractionTemplet>();
			}
			this.m_lstUnitInteractionCache = interactionTempletList.FindAll((NKCOfficeUnitInteractionTemplet x) => !x.IsSoloAction);
			this.m_lstSoloInteractionCache = interactionTempletList.FindAll((NKCOfficeUnitInteractionTemplet x) => x.IsSoloAction);
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x0600532B RID: 21291 RVA: 0x00195867 File Offset: 0x00193A67
		public List<NKCOfficeUnitInteractionTemplet> UnitInteractionCache
		{
			get
			{
				if (this.m_lstUnitInteractionCache == null)
				{
					this.BuildInteractionCache();
				}
				return this.m_lstUnitInteractionCache;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x0019587D File Offset: 0x00193A7D
		public List<NKCOfficeUnitInteractionTemplet> SoloInteractionCache
		{
			get
			{
				if (this.m_lstSoloInteractionCache == null)
				{
					this.BuildInteractionCache();
				}
				return this.m_lstSoloInteractionCache;
			}
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x00195894 File Offset: 0x00193A94
		public bool RegisterInteraction(NKCOfficeFuniture furniture, NKCOfficeFurnitureInteractionTemplet templet)
		{
			if (templet == null)
			{
				return false;
			}
			List<NKCAnimationEventTemplet> lstAnim = NKCAnimationEventManager.Find(templet.UnitAni);
			if (NKCAnimationEventManager.IsEmotionOnly(lstAnim))
			{
				this.PlayEmotionAnimation(lstAnim);
			}
			else
			{
				this.BT.DisableBehavior();
				this.StopAllAnimInstances();
				this.CurrentInteractionTargetFurniture = furniture;
				this.CurrentFurnitureInteractionTemplet = templet;
				this.StartAI();
				if (templet.eActType == NKCOfficeFurnitureInteractionTemplet.ActType.Common)
				{
					furniture.RegisterInteractionCharacter(this);
				}
			}
			if (templet.eActType == NKCOfficeFurnitureInteractionTemplet.ActType.Common)
			{
				this.SetFurnitureInteractionCooltime();
			}
			return true;
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00195908 File Offset: 0x00193B08
		public bool RegisterInteraction(NKCOfficeUnitInteractionTemplet soloTemplet)
		{
			if (soloTemplet == null)
			{
				return false;
			}
			if (!soloTemplet.IsSoloAction)
			{
				Debug.LogError("Logic Error!");
				return false;
			}
			List<NKCAnimationEventTemplet> lstAnim = NKCAnimationEventManager.Find(soloTemplet.ActorAni);
			if (NKCAnimationEventManager.IsEmotionOnly(lstAnim))
			{
				this.PlayEmotionAnimation(lstAnim);
			}
			else
			{
				this.BT.DisableBehavior();
				this.StopAllAnimInstances();
				this.CurrentUnitInteractionTemplet = soloTemplet;
				this.StartAI();
			}
			this.SetSoloInteractionCooltime();
			return true;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00195970 File Offset: 0x00193B70
		public bool RegisterInteraction(NKCOfficeUnitInteractionTemplet templet, NKCOfficeCharacter targetCharacter, bool IsMainActor, Vector3 actionPosition)
		{
			if (templet == null)
			{
				return false;
			}
			List<NKCAnimationEventTemplet> lstAnim = NKCAnimationEventManager.Find(IsMainActor ? templet.ActorAni : templet.TargetAni);
			bool flag = NKCAnimationEventManager.IsEmotionOnly(lstAnim);
			if (!templet.AlignUnit && flag)
			{
				this.PlayEmotionAnimation(lstAnim);
			}
			else
			{
				this.BT.DisableBehavior();
				this.StopAllAnimInstances();
				this.CurrentUnitInteractionTemplet = templet;
				this.CurrentUnitInteractionTarget = targetCharacter;
				this.CurrentUnitInteractionIsMainActor = IsMainActor;
				this.CurrentUnitInteractionPosition = actionPosition;
				this.StartAI();
			}
			this.SetUnitInteractionCooltime();
			return true;
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x001959F4 File Offset: 0x00193BF4
		public void UnregisterInteraction()
		{
			if (this.PlayingInteractionAnimation)
			{
				this.StopAllAnimInstances();
			}
			if (this.CurrentInteractionTargetFurniture != null)
			{
				this.CurrentInteractionTargetFurniture.CleanupInteraction();
				this.SetFurnitureInteractionCooltime();
			}
			if (this.CurrentUnitInteractionTemplet != null)
			{
				if (this.CurrentUnitInteractionTemplet.IsSoloAction)
				{
					this.SetSoloInteractionCooltime();
				}
				else
				{
					this.SetUnitInteractionCooltime();
				}
			}
			base.transform.SetParent(this.m_OfficeBuilding.trActorRoot);
			base.transform.localScale = Vector3.one;
			base.transform.rotation = Quaternion.identity;
			Vector3 localPosition = this.m_OfficeBuilding.m_Floor.m_rtFunitureRoot.ProjectPointToPlane(base.transform.position);
			base.transform.localPosition = localPosition;
			this.PlayingInteractionAnimation = false;
			this.CurrentInteractionTargetFurniture = null;
			this.CurrentFurnitureInteractionTemplet = null;
			this.CurrentUnitInteractionTemplet = null;
			this.CurrentUnitInteractionTarget = null;
			this.SetShadow(true);
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x00195ADE File Offset: 0x00193CDE
		public bool IsUnitInteractTargetable()
		{
			return this.m_eState == NKCOfficeCharacter.State.AI && this.m_fUnitInteractionCooltime <= 0f && !this.HasInteractionTarget();
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x00195B05 File Offset: 0x00193D05
		public bool HasInteractionTarget()
		{
			return (this.CurrentInteractionTargetFurniture != null && this.CurrentFurnitureInteractionTemplet != null) || this.CurrentUnitInteractionTemplet != null;
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x00195B2C File Offset: 0x00193D2C
		public void SetPlayingInteractionAnimation(bool value)
		{
			this.PlayingInteractionAnimation = value;
			if (value && this.CurrentInteractionTargetFurniture != null && this.CurrentFurnitureInteractionTemplet.eActType == NKCOfficeFurnitureInteractionTemplet.ActType.Common)
			{
				GameObject interactionPoint = this.CurrentInteractionTargetFurniture.GetInteractionPoint();
				if (interactionPoint != null)
				{
					base.transform.SetParent(interactionPoint.transform);
				}
				this.CurrentInteractionTargetFurniture.InvalidateWorldRect();
			}
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x00195B90 File Offset: 0x00193D90
		public Vector3 GetInteractionPosition()
		{
			if (this.CurrentFurnitureInteractionTemplet != null)
			{
				if (this.CurrentFurnitureInteractionTemplet.eActType == NKCOfficeFurnitureInteractionTemplet.ActType.Reaction)
				{
					return base.transform.localPosition;
				}
				if (this.CurrentInteractionTargetFurniture != null)
				{
					GameObject interactionPoint = this.CurrentInteractionTargetFurniture.GetInteractionPoint();
					if (interactionPoint != null)
					{
						return this.m_OfficeBuilding.m_Floor.Rect.InverseTransformPoint(interactionPoint.transform.position);
					}
					return base.transform.localPosition;
				}
			}
			if (this.CurrentUnitInteractionTemplet == null)
			{
				return base.transform.localPosition;
			}
			if (this.CurrentUnitInteractionTemplet.IsSoloAction)
			{
				return base.transform.localPosition;
			}
			return this.CurrentUnitInteractionPosition;
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x00195C44 File Offset: 0x00193E44
		private void CheckFurnitureInteraction()
		{
			if (this.m_fFurnitureInteractionCooltime > 0f)
			{
				return;
			}
			if (this.HasInteractionTarget())
			{
				return;
			}
			if (UnityEngine.Random.Range(0, 100) >= NKMCommonConst.Office.OfficeInteraction.ActInteriorRatePercent)
			{
				return;
			}
			NKCOfficeFuniture nkcofficeFuniture = this.m_OfficeBuilding.FindInteractableInterior(this);
			if (nkcofficeFuniture == null)
			{
				return;
			}
			NKCOfficeManager.PlayInteraction(this, nkcofficeFuniture);
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x00195CA4 File Offset: 0x00193EA4
		private void CheckUnitInteraction()
		{
			if (this.HasInteractionTarget())
			{
				return;
			}
			if (this.m_fUnitInteractionCooltime > 0f)
			{
				return;
			}
			if (UnityEngine.Random.Range(0, 100) >= NKMCommonConst.Office.OfficeInteraction.ActUnitRatePercent)
			{
				return;
			}
			NKCOfficeCharacter nkcofficeCharacter = this.m_OfficeBuilding.FindInteractableCharacter(this);
			if (nkcofficeCharacter == null)
			{
				return;
			}
			NKCOfficeManager.PlayInteraction(this, nkcofficeCharacter, false, false);
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x00195D04 File Offset: 0x00193F04
		private void CheckSoloInteraction()
		{
			if (this.HasInteractionTarget())
			{
				return;
			}
			if (this.m_fSoloInteractionCooltime > 0f)
			{
				return;
			}
			if (UnityEngine.Random.Range(0, 100) >= NKMCommonConst.Office.OfficeInteraction.ActSoloRatePercent)
			{
				return;
			}
			if (this.SoloInteractionCache.Count == 0)
			{
				return;
			}
			NKCOfficeUnitInteractionTemplet soloTemplet = this.SoloInteractionCache[UnityEngine.Random.Range(0, this.SoloInteractionCache.Count)];
			this.RegisterInteraction(soloTemplet);
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x00195D75 File Offset: 0x00193F75
		public void PlayEmotion(string animName, float speed = 1f)
		{
			if (this.m_comEmotion != null)
			{
				this.m_comEmotion.Play(animName, speed);
			}
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x00195D92 File Offset: 0x00193F92
		public void PlayEmotion(NKCUIComCharacterEmotion.Type type, float speed = 1f)
		{
			if (this.m_comEmotion != null)
			{
				this.m_comEmotion.Play(type, speed);
			}
		}

		// Token: 0x04004290 RID: 17040
		public Animator m_animator;

		// Token: 0x04004291 RID: 17041
		public Transform m_trSDParent;

		// Token: 0x04004292 RID: 17042
		public GameObject m_objShadow;

		// Token: 0x04004293 RID: 17043
		public NKCUIComOfficeLoyalty m_comLoyalty;

		// Token: 0x04004294 RID: 17044
		public NKCUIComOfficeFriendInfo m_comFriendInfo;

		// Token: 0x04004295 RID: 17045
		public NKCUIComCharacterEmotion m_comEmotion;

		// Token: 0x04004296 RID: 17046
		public bool m_bCanTouch = true;

		// Token: 0x04004297 RID: 17047
		public bool m_bCanGrab = true;

		// Token: 0x04004298 RID: 17048
		public Graphic m_gpTouchTarget;

		// Token: 0x04004299 RID: 17049
		protected BehaviorTree BT;

		// Token: 0x0400429A RID: 17050
		protected NKCOfficeBuildingBase m_OfficeBuilding;

		// Token: 0x0400429B RID: 17051
		private NKCOfficeCharacter.State m_eState;

		// Token: 0x0400429C RID: 17052
		private NKCOfficeCharacter.OnClick dOnClick;

		// Token: 0x0400429D RID: 17053
		private NKCAnimationInstance m_animInstance;

		// Token: 0x0400429E RID: 17054
		private NKCAnimationInstance m_emotionAnimInstance;

		// Token: 0x0400429F RID: 17055
		private Queue<NKCAnimationInstance> m_qAnimInstances = new Queue<NKCAnimationInstance>();

		// Token: 0x040042A0 RID: 17056
		private List<NKCAnimationInstance> m_lstFinishedInstances = new List<NKCAnimationInstance>();

		// Token: 0x040042A3 RID: 17059
		protected NKCASUIUnitIllust m_SDIllust;

		// Token: 0x040042A4 RID: 17060
		protected NKMUnitData m_UnitData;

		// Token: 0x040042A5 RID: 17061
		protected long m_FriendUID;

		// Token: 0x040042A6 RID: 17062
		private const string UNIT_OFFICE_SD_BUNDLENAME = "AB_UNIT_OFFICE_SD";

		// Token: 0x040042A7 RID: 17063
		private const string UNIT_OFFICE_SD_ASSETNAME = "UNIT_OFFICE_SD";

		// Token: 0x040042A8 RID: 17064
		private bool m_bTakeHeartSent;

		// Token: 0x040042A9 RID: 17065
		public float GrabYOffset = 20f;

		// Token: 0x040042AA RID: 17066
		public float GRAB_WAIT_TIME = 0.2f;

		// Token: 0x040042AB RID: 17067
		private float m_fGrabWaitTime;

		// Token: 0x040042AC RID: 17068
		private Vector2 m_touchPos = Vector2.zero;

		// Token: 0x040042AD RID: 17069
		private Vector3 m_touchLocalOffset = Vector3.zero;

		// Token: 0x040042AE RID: 17070
		public float grabUITime = 0.5f;

		// Token: 0x040042AF RID: 17071
		private float m_fInteractionCheckInterval;

		// Token: 0x040042B0 RID: 17072
		private float m_fFurnitureInteractionCooltime;

		// Token: 0x040042B1 RID: 17073
		private float m_fUnitInteractionCooltime;

		// Token: 0x040042B2 RID: 17074
		private float m_fSoloInteractionCooltime;

		// Token: 0x040042B3 RID: 17075
		private bool m_bRectWorldValid;

		// Token: 0x040042B4 RID: 17076
		private Rect m_rectWorld;

		// Token: 0x040042BC RID: 17084
		private List<NKCOfficeUnitInteractionTemplet> m_lstUnitInteractionCache;

		// Token: 0x040042BD RID: 17085
		private List<NKCOfficeUnitInteractionTemplet> m_lstSoloInteractionCache;

		// Token: 0x020014DD RID: 5341
		private enum State
		{
			// Token: 0x04009F41 RID: 40769
			NONE,
			// Token: 0x04009F42 RID: 40770
			AI,
			// Token: 0x04009F43 RID: 40771
			WaitGrab,
			// Token: 0x04009F44 RID: 40772
			Grab
		}

		// Token: 0x020014DE RID: 5342
		// (Invoke) Token: 0x0600AA16 RID: 43542
		public delegate bool OnClick();
	}
}

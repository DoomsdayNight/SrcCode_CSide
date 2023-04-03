using System;
using System.Collections.Generic;
using System.Text;
using NKC.Publisher;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NKC.UI
{
	// Token: 0x020009A0 RID: 2464
	public class NKCUIGameResultGetUnit : NKCUIBase
	{
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600667A RID: 26234 RVA: 0x0020BC58 File Offset: 0x00209E58
		public static NKCUIGameResultGetUnit Instance
		{
			get
			{
				if (NKCUIGameResultGetUnit.m_Instance == null)
				{
					NKCUIGameResultGetUnit.m_Instance = NKCUIManager.OpenNewInstance<NKCUIGameResultGetUnit>("ab_ui_nkm_ui_result", "NKM_UI_RESULT_GET_UNIT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGameResultGetUnit.CleanupInstance)).GetInstance<NKCUIGameResultGetUnit>();
					NKCUIGameResultGetUnit.m_Instance.InitUI();
				}
				return NKCUIGameResultGetUnit.m_Instance;
			}
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x0020BCA7 File Offset: 0x00209EA7
		private static void CleanupInstance()
		{
			NKCUIGameResultGetUnit.m_Instance = null;
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x0020BCAF File Offset: 0x00209EAF
		private void OnDestroy()
		{
			this.ClearIllustMem();
			NKCUIGameResultGetUnit.m_Instance = null;
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x0020BCC0 File Offset: 0x00209EC0
		public static void ShowNewSkinGetUI(HashSet<int> hsSkinID, NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack callBack, bool bEnableAutoSkip = false, bool bSkipDuplicateNormalUnit = false)
		{
			List<NKCUIGameResultGetUnit.GetUnitResultData> list = new List<NKCUIGameResultGetUnit.GetUnitResultData>();
			foreach (int skinID in hsSkinID)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				if (skinTemplet == null)
				{
					return;
				}
				list.Add(new NKCUIGameResultGetUnit.GetUnitResultData(skinTemplet));
			}
			NKCUIGameResultGetUnit.Instance.Open(list, callBack, bEnableAutoSkip, true, bSkipDuplicateNormalUnit, NKCUIGameResultGetUnit.Type.Skin);
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x0020BD34 File Offset: 0x00209F34
		public static void ShowNewUnitGetUI(NKMRewardData rewardData, NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack callBack, bool bEnableAutoSkip = false, bool bUseDefaultSort = true, bool bSkipDuplicateNormalUnit = false)
		{
			NKCUIGameResultGetUnit.Instance.Open(NKCUIGameResultGetUnit.GetUnitResultData.ConvertList(rewardData), callBack, bEnableAutoSkip, bUseDefaultSort, bSkipDuplicateNormalUnit, NKCUIGameResultGetUnit.Type.Get);
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x0020BD4C File Offset: 0x00209F4C
		public static void ShowNewUnitGetUI(List<NKMRewardData> lstRewardData, NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack callBack, bool bEnableAutoSkip = false, bool bUseDefaultSort = true, bool bSkipDuplicateNormalUnit = false)
		{
			NKCUIGameResultGetUnit.Instance.Open(NKCUIGameResultGetUnit.GetUnitResultData.ConvertList(lstRewardData), callBack, bEnableAutoSkip, bUseDefaultSort, bSkipDuplicateNormalUnit, NKCUIGameResultGetUnit.Type.Get);
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x0020BD64 File Offset: 0x00209F64
		public static void ShowNewUnitGetUIForSelectableContract(List<NKMUnitData> lstUnitData, NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack callBack)
		{
			NKCUIGameResultGetUnit.Instance.m_bForceHideGetUnitMark = true;
			NKCUIGameResultGetUnit.Instance.Open(NKCUIGameResultGetUnit.GetUnitResultData.ConvertList(lstUnitData, null, null), callBack, false, true, false, NKCUIGameResultGetUnit.Type.Get);
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x0020BD88 File Offset: 0x00209F88
		public static void ShowUnitTranscendence(NKMUnitData unitData, NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack callBack = null)
		{
			NKCUIGameResultGetUnit.Instance.Open(new NKCUIGameResultGetUnit.GetUnitResultData(unitData), callBack, false, false, false, NKCUIGameResultGetUnit.Type.LimitBreak);
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x0020BD9F File Offset: 0x00209F9F
		public static void ShowShipTranscendence(NKMUnitData shipData, int curSkillCnt, int curMaxLv, int nextMaxLv)
		{
			NKCUIGameResultGetUnit.Instance.SetShipData(curSkillCnt, curMaxLv, nextMaxLv);
			NKCUIGameResultGetUnit.Instance.Open(new NKCUIGameResultGetUnit.GetUnitResultData(shipData), null, false, false, false, NKCUIGameResultGetUnit.Type.Ship);
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x0020BDC3 File Offset: 0x00209FC3
		public static void CheckInstanceAndClose()
		{
			if (NKCUIGameResultGetUnit.m_Instance != null && NKCUIGameResultGetUnit.m_Instance.IsOpen)
			{
				NKCUIGameResultGetUnit.m_Instance.Close();
				NKCUIGameResultGetUnit.m_Instance.InvokeCallback();
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06006684 RID: 26244 RVA: 0x0020BDF2 File Offset: 0x00209FF2
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGameResultGetUnit.m_Instance != null && NKCUIGameResultGetUnit.m_Instance.IsOpen;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06006685 RID: 26245 RVA: 0x0020BE0D File Offset: 0x0020A00D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06006686 RID: 26246 RVA: 0x0020BE10 File Offset: 0x0020A010
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06006687 RID: 26247 RVA: 0x0020BE13 File Offset: 0x0020A013
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GET_UNIT;
			}
		}

		// Token: 0x06006688 RID: 26248 RVA: 0x0020BE1C File Offset: 0x0020A01C
		public void InitUI()
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnTouchAnywhere();
			});
			this.m_evtScreen.triggers.Clear();
			this.m_evtScreen.triggers.Add(entry);
			base.gameObject.SetActive(false);
			this.m_bEnableNextChar = true;
			this.m_lstStarOFF.Clear();
			this.m_lstStarOFF.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_OFF/NKM_STAR0").gameObject);
			this.m_lstStarOFF.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_OFF/NKM_STAR1").gameObject);
			this.m_lstStarOFF.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_OFF/NKM_STAR2").gameObject);
			this.m_lstStarOFF.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_OFF/NKM_STAR3").gameObject);
			this.m_lstStarOFF.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_OFF/NKM_STAR4").gameObject);
			this.m_lstStarOFF.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_OFF/NKM_STAR5").gameObject);
			this.m_lstStarONParent.Clear();
			this.m_lstStarONParent.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_AFTER/NKM_STAR0").gameObject);
			this.m_lstStarONParent.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_AFTER/NKM_STAR1").gameObject);
			this.m_lstStarONParent.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_AFTER/NKM_STAR2").gameObject);
			this.m_lstStarONParent.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_AFTER/NKM_STAR3").gameObject);
			this.m_lstStarONParent.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_AFTER/NKM_STAR4").gameObject);
			this.m_lstStarONParent.Add(base.gameObject.transform.Find("Canvas_INFO/NKM_UI_RESULT_GET_UNIT_SUMMARY_Panel/NKM_UI_RESULT_GET_UNIT_NAME/NKM_UI_RESULT_GET_UNIT_STAR/NKM_UI_RESULT_GET_UNIT_INFO_STAR_AFTER/NKM_STAR5").gameObject);
			this.m_btnSkip.PointerClick.RemoveAllListeners();
			this.m_btnSkip.PointerClick.AddListener(new UnityAction(this.SkipAll));
			AnimationClip[] animationClips = this.m_animGetUnit.runtimeAnimatorController.animationClips;
			if (animationClips != null)
			{
				foreach (AnimationClip animationClip in animationClips)
				{
					if (string.Equals(animationClip.name, "NKM_UI_RESULT_GET_UNIT_TOP_BG_ON"))
					{
						animationClip.AddEvent(new AnimationEvent
						{
							functionName = "ActiveSFXStar",
							time = 0f
						});
						break;
					}
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnShare, new UnityAction(this.OnShare));
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x0020C0F7 File Offset: 0x0020A2F7
		public void ActiveSFXStar()
		{
			NKCUtil.SetGameobjectActive(this.m_SFX_STAR, true);
			NKCSoundManager.PlaySound("FX_UI_UNIT_GET_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x0600668A RID: 26250 RVA: 0x0020C12B File Offset: 0x0020A32B
		private void DeactiveSFXStar()
		{
			NKCUtil.SetGameobjectActive(this.m_SFX_STAR, false);
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x0020C13C File Offset: 0x0020A33C
		public void Open(NKCUIGameResultGetUnit.GetUnitResultData getUnitData, NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack _NKCUIGRGetUnitCallBack = null, bool bEnableAutoSkip = false, bool bUseDefaultSort = true, bool bSkipDuplicateNormalUnit = false, NKCUIGameResultGetUnit.Type type = NKCUIGameResultGetUnit.Type.Get)
		{
			this.Open(new List<NKCUIGameResultGetUnit.GetUnitResultData>
			{
				getUnitData
			}, _NKCUIGRGetUnitCallBack, bEnableAutoSkip, bUseDefaultSort, bSkipDuplicateNormalUnit, type);
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x0020C168 File Offset: 0x0020A368
		public void Open(List<NKCUIGameResultGetUnit.GetUnitResultData> listUnitData, NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack _NKCUIGRGetUnitCallBack = null, bool bEnableAutoSkip = false, bool bUseDefaultSort = true, bool bSkipDuplicateNormalUnit = false, NKCUIGameResultGetUnit.Type type = NKCUIGameResultGetUnit.Type.Get)
		{
			if (listUnitData == null || listUnitData.Count <= 0)
			{
				if (_NKCUIGRGetUnitCallBack != null)
				{
					_NKCUIGRGetUnitCallBack();
				}
				return;
			}
			if (bSkipDuplicateNormalUnit)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				List<NKCUIGameResultGetUnit.GetUnitResultData> list = new List<NKCUIGameResultGetUnit.GetUnitResultData>();
				foreach (NKCUIGameResultGetUnit.GetUnitResultData getUnitResultData in listUnitData)
				{
					if (!NKCUIGameResultGetUnit.HaveFirstGetUnit(getUnitResultData.m_UnitID))
					{
						bool flag = false;
						bool? flag2 = (nkmuserData != null) ? new bool?(nkmuserData.m_ArmyData.IsFirstGetUnit(getUnitResultData.m_UnitID)) : null;
						if ((flag == flag2.GetValueOrDefault() & flag2 != null) && NKMUnitManager.GetUnitTempletBase(getUnitResultData.m_UnitID).m_NKM_UNIT_GRADE != NKM_UNIT_GRADE.NUG_SSR)
						{
							continue;
						}
					}
					list.Add(getUnitResultData);
				}
				if (list.Count == 0)
				{
					if (_NKCUIGRGetUnitCallBack != null)
					{
						_NKCUIGRGetUnitCallBack();
					}
					return;
				}
				this.m_listUnitData = list;
			}
			else
			{
				this.m_listUnitData = listUnitData;
			}
			this.m_bEnableTimeAutoSkip = bEnableAutoSkip;
			this.m_bCheckTimeAutoSkip = false;
			this.m_Type = type;
			if (bUseDefaultSort)
			{
				this.m_listUnitData.Sort(new Comparison<NKCUIGameResultGetUnit.GetUnitResultData>(this.DefaultUnitSort));
			}
			this.m_CurrIdx = 0;
			this.m_NKCUIGRGetUnitCallBack = _NKCUIGRGetUnitCallBack;
			base.gameObject.SetActive(true);
			base.UIOpened(true);
			this.SetBG();
			this.m_bEnableNextChar = true;
			this.ShowNext(true);
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x0020C2C4 File Offset: 0x0020A4C4
		private int DefaultUnitSort(NKCUIGameResultGetUnit.GetUnitResultData A, NKCUIGameResultGetUnit.GetUnitResultData B)
		{
			if (A.m_bSkin && B.m_bSkin)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(A.m_SkinID);
				return NKMSkinManager.GetSkinTemplet(B.m_SkinID).m_SkinGrade.CompareTo(skinTemplet.m_SkinGrade);
			}
			if (A.m_bSkin != B.m_bSkin)
			{
				return B.m_bSkin.CompareTo(A.m_bSkin);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(A.m_UnitID);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(B.m_UnitID);
			if (unitTempletBase.m_bAwaken != unitTempletBase2.m_bAwaken)
			{
				return unitTempletBase2.m_bAwaken.CompareTo(unitTempletBase.m_bAwaken);
			}
			if (unitTempletBase.m_NKM_UNIT_GRADE != unitTempletBase2.m_NKM_UNIT_GRADE)
			{
				return unitTempletBase2.m_NKM_UNIT_GRADE.CompareTo(unitTempletBase.m_NKM_UNIT_GRADE);
			}
			return A.m_UnitID.CompareTo(B.m_UnitID);
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x0020C3AA File Offset: 0x0020A5AA
		private void SetAutoSkipTimeStamp()
		{
			if (!this.m_bEnableTimeAutoSkip)
			{
				return;
			}
			this.m_fTimeAutoSkip = Time.time;
			this.m_bCheckTimeAutoSkip = true;
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x0020C3C7 File Offset: 0x0020A5C7
		private void OnLoginCutinFinished()
		{
			this.UnHide();
			this.ShowNext(false);
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x0020C3D8 File Offset: 0x0020A5D8
		public void ShowNext(bool bWillPlayCutin = true)
		{
			this.DeactiveSFXStar();
			if (this.m_CurrIdx < this.m_listUnitData.Count && this.m_CurrIdx >= 0)
			{
				NKCUIGameResultGetUnit.GetUnitResultData getUnitResultData = this.m_listUnitData[this.m_CurrIdx];
				this.m_animGetUnit.Rebind();
				bool flag = false;
				if (this.m_animGetUnit != null)
				{
					if (getUnitResultData.m_bSkin)
					{
						NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(getUnitResultData.m_SkinID);
						if (skinTemplet != null && skinTemplet.HasLoginCutin)
						{
							if (bWillPlayCutin)
							{
								this.Hide();
								NKCUIEventSequence.PlaySkinCutin(skinTemplet, new NKCUIEventSequence.OnClose(this.OnLoginCutinFinished));
								return;
							}
							this.m_animGetUnit.SetTrigger("NOINTRO");
							flag = true;
						}
					}
					if (!flag)
					{
						this.m_animGetUnit.SetTrigger("RESTART");
					}
				}
				NKCUIVoiceManager.StopVoice();
				NKCUtil.SetGameobjectActive(this.m_objGameOpenAwakenFX, false);
				this.m_CurrIdx++;
				this.SetData(getUnitResultData);
				if (flag)
				{
					NKCUtil.SetGameobjectActive(this.m_objGameOpenAwakenFX, false);
				}
				this.m_bPlayVoice = false;
				this.SetAutoSkipTimeStamp();
				return;
			}
			NKCUIVoiceManager.StopVoice();
			this.m_bForceHideGetUnitMark = false;
			base.Close();
			this.InvokeCallback();
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x0020C4F5 File Offset: 0x0020A6F5
		public override void OnBackButton()
		{
			this.OnTouchAnywhere();
		}

		// Token: 0x06006692 RID: 26258 RVA: 0x0020C500 File Offset: 0x0020A700
		private void SetData(NKCUIGameResultGetUnit.GetUnitResultData resultUnitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(resultUnitData.m_UnitID);
			if (unitTempletBase != null)
			{
				this.m_CurrentUnitResultData = resultUnitData;
				this.m_NKMUnitTempletBase = unitTempletBase;
				this.m_skinID = resultUnitData.m_SkinID;
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(resultUnitData.m_UnitUID);
				int layerIndex = this.m_animGetUnit.GetLayerIndex("STAR");
				int layerIndex2 = this.m_animGetUnit.GetLayerIndex("STAR_SKIN");
				if (resultUnitData.m_bSkin)
				{
					this.m_animGetUnit.SetLayerWeight(layerIndex, 0f);
					this.m_animGetUnit.SetLayerWeight(layerIndex2, 1f);
				}
				else
				{
					this.m_animGetUnit.SetLayerWeight(layerIndex, 1f);
					this.m_animGetUnit.SetLayerWeight(layerIndex2, 0f);
				}
				this.m_lbTitle.text = unitTempletBase.GetUnitTitle();
				this.m_lbName.text = unitTempletBase.GetUnitName();
				this.m_lbNameShadow.text = unitTempletBase.GetUnitName();
				int num = NKCUtil.GetStarCntByUnitGrade(unitTempletBase) + 3;
				int num2 = unitTempletBase.m_StarGradeMax;
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					num = 6;
					num2 = 6;
				}
				else if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					num = 0;
					num2 = 0;
				}
				for (int i = 0; i < this.m_lstUnitGradeStarOFF.Count; i++)
				{
					if (i < num)
					{
						this.m_lstUnitGradeStarOFF[i].SetActive(true);
					}
					else
					{
						this.m_lstUnitGradeStarOFF[i].SetActive(false);
					}
				}
				NKCUtil.SetStarRank(this.m_lstUnitGradeStar, resultUnitData.limitBreakStarCount, resultUnitData.StarMaxCount);
				for (int i = 0; i < this.m_lstResultStar.Count; i++)
				{
					if (this.m_lstResultStar[i] != null)
					{
						this.m_lstResultStar[i].SetTranscendence(resultUnitData.bFullTranscendence);
					}
				}
				for (int i = 0; i < this.m_lstStarOFF.Count; i++)
				{
					if (i < num2)
					{
						this.m_lstStarOFF[i].SetActive(true);
					}
					else
					{
						this.m_lstStarOFF[i].SetActive(false);
					}
				}
				for (int i = 0; i < this.m_lstStarONParent.Count; i++)
				{
					if (i < num2)
					{
						this.m_lstStarONParent[i].SetActive(true);
					}
					else
					{
						this.m_lstStarONParent[i].SetActive(false);
					}
				}
				NKCUtil.SetStarRank(this.m_lstStar, resultUnitData.limitBreakStarCount, resultUnitData.StarMaxCount);
				for (int i = 0; i < 4; i++)
				{
					if ((int)this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE == i)
					{
						NKCUtil.SetGameobjectActive(this.m_lstUnitRank[i], true);
						NKCUtil.SetGameobjectActive(this.m_lstUnitLiiustFX[i], true);
						NKCUtil.SetGameobjectActive(this.m_lstUnitGradeFX[i], true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstUnitRank[i], false);
						NKCUtil.SetGameobjectActive(this.m_lstUnitLiiustFX[i], false);
						NKCUtil.SetGameobjectActive(this.m_lstUnitGradeFX[i], false);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_SFX_STAR_SSR, this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR);
				NKCUtil.SetGameobjectActive(this.m_SFX_STAR_OTHER, this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE != NKM_UNIT_GRADE.NUG_SSR);
				NKCUtil.SetGameobjectActive(this.m_objRankAwakenSR, this.m_NKMUnitTempletBase.m_bAwaken && this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR);
				NKCUtil.SetGameobjectActive(this.m_objRankAwakenSSR, this.m_NKMUnitTempletBase.m_bAwaken && this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR);
				NKCUtil.SetGameobjectActive(this.m_objBGAwakenSR, this.m_NKMUnitTempletBase.m_bAwaken && this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR);
				NKCUtil.SetGameobjectActive(this.m_objBGAwakenSSR, this.m_NKMUnitTempletBase.m_bAwaken && this.m_NKMUnitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR);
				NKCUtil.SetImageSprite(this.m_NKM_UI_RESULT_GET_UNIT_MARK, NKCResourceUtility.GetOrLoadUnitStyleIcon(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, false), false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS, unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL);
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					NKCUtil.SetImageSprite(this.m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS_ICON, NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, true), false);
					NKCUtil.SetLabelText(this.m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS_TEXT, NKCUtilString.GetRoleText(unitTempletBase));
				}
				else if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					NKCUtil.SetImageSprite(this.m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS_ICON, NKCResourceUtility.GetOrLoadUnitStyleIcon(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, true), false);
					NKCUtil.SetLabelText(this.m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS_TEXT, "");
				}
				NKCDescTemplet descTemplet = NKCDescMgr.GetDescTemplet(resultUnitData.m_UnitID, resultUnitData.m_SkinID);
				if (descTemplet != null)
				{
					string text = descTemplet.m_arrDescData[this.TypeToDescType(this.m_Type)].GetDesc();
					text = NKCUtil.TextSplitLine(text, this.m_lbDailoogue, 0f);
					this.m_lbDailoogue.text = text;
				}
				else
				{
					this.m_lbDailoogue.text = "";
				}
				string unitStyleMarkString = NKCUtilString.GetUnitStyleMarkString(unitTempletBase);
				NKCUtil.SetLabelText(this.m_lbNKM_UI_RESULT_GET_UNIT_SUMMARY_STYLE_NAME, unitStyleMarkString);
				bool flag = true;
				if (NKCScenManager.CurrentUserData().m_ArmyData != null)
				{
					flag = NKCUIGameResultGetUnit.HaveFirstGetUnit(resultUnitData.m_UnitID);
				}
				if (resultUnitData.m_bSkin)
				{
					this.SetTag(NKCUIGameResultGetUnit.eUnitTagType.GetSkin);
				}
				else
				{
					switch (this.m_Type)
					{
					case NKCUIGameResultGetUnit.Type.Get:
						if (flag)
						{
							this.SetTag(NKCUIGameResultGetUnit.eUnitTagType.NewUnit);
						}
						else if (this.m_bForceHideGetUnitMark)
						{
							this.SetTag(NKCUIGameResultGetUnit.eUnitTagType.None);
						}
						else
						{
							this.SetTag(NKCUIGameResultGetUnit.eUnitTagType.GetUnit);
						}
						break;
					case NKCUIGameResultGetUnit.Type.LimitBreak:
						if (resultUnitData.m_LimitBreakLevel > 3)
						{
							this.SetTag(NKCUIGameResultGetUnit.eUnitTagType.Transcendence);
						}
						else
						{
							this.SetTag(NKCUIGameResultGetUnit.eUnitTagType.LimitBreak);
						}
						break;
					case NKCUIGameResultGetUnit.Type.Ship:
						this.SetTag(NKCUIGameResultGetUnit.eUnitTagType.ShipUpgrade);
						break;
					}
				}
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_SHIP_UPGRADE_SKILL, false);
				if (this.m_Type == NKCUIGameResultGetUnit.Type.LimitBreak)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_UNIT_TRANSCENDENCE_INFO, true);
					if (resultUnitData.m_LimitBreakLevel > 3)
					{
						NKCUtil.SetGameobjectActive(this.m_objTranscendenceTextRoot, true);
						NKCUtil.SetGameobjectActive(this.m_LimitBreakTextRoot, false);
						NKCUtil.SetGameobjectActive(this.m_TRANSCENDENCE_TEXT2, false);
						NKCUtil.SetLabelText(this.m_lbTCCount, NKCUtilString.GET_STRING_LIMITBREAK_TRANSCENDENCE_LEVEL_ONE_PARAM, new object[]
						{
							NKMUnitLimitBreakManager.GetTranscendenceCount(unitFromUID)
						});
						StringBuilder stringBuilder = new StringBuilder();
						NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo(resultUnitData.m_LimitBreakLevel - 1);
						NKMLimitBreakTemplet lbinfo2 = NKMUnitLimitBreakManager.GetLBInfo(resultUnitData.m_LimitBreakLevel);
						if (lbinfo != null && lbinfo2 != null)
						{
							if (lbinfo.m_iMaxLevel == 100)
							{
								stringBuilder.AppendFormat(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo.m_iMaxLevel);
							}
							else
							{
								stringBuilder.Append("<color=#C57BF4>");
								stringBuilder.AppendFormat(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo.m_iMaxLevel);
								stringBuilder.Append("</color>");
							}
							stringBuilder.Append(" > ");
							stringBuilder.Append("<color=#C57BF4>");
							stringBuilder.AppendFormat(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, lbinfo2.m_iMaxLevel);
							stringBuilder.Append("</color>");
							NKCUtil.SetLabelText(this.m_lbTCMaxLevel, stringBuilder.ToString());
							float num3 = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier(resultUnitData.m_LimitBreakLevel) - NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier(resultUnitData.m_LimitBreakLevel - 1);
							NKCUtil.SetLabelText(this.m_lbTCGrowRate, NKCStringTable.GetString("SI_DP_RESULT_TRANSCENDENCE_UNIT_GROWTH_DIFFERENCE_ONE_PARAM", false), new object[]
							{
								Mathf.RoundToInt(num3 * 100f)
							});
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_objTranscendenceTextRoot, false);
						NKCUtil.SetGameobjectActive(this.m_LimitBreakTextRoot, true);
						NKCUtil.SetGameobjectActive(this.m_TRANSCENDENCE_TEXT2, unitFromUID != null && unitFromUID.IsUnlockAccessory2());
						bool flag2 = false;
						string skillStrID = unitTempletBase.GetSkillStrID(3);
						if (!string.Equals("", skillStrID))
						{
							NKMUnitSkillTemplet unitSkillTemplet = NKMUnitSkillManager.GetUnitSkillTemplet(skillStrID, unitFromUID);
							if (unitSkillTemplet != null)
							{
								int unlockReqUpgradeFromSkillId = NKMUnitSkillManager.GetUnlockReqUpgradeFromSkillId(unitSkillTemplet.m_ID);
								if (resultUnitData.m_LimitBreakLevel == unlockReqUpgradeFromSkillId)
								{
									flag2 = true;
								}
							}
						}
						NKCUtil.SetGameobjectActive(this.m_lbTRANSCENDENCE_TEXT1.gameObject, true);
						string format = flag2 ? NKCUtilString.GET_STRING_RESULT_LIMIT_BREAK_UNIT_ONE_PARAM_UNLOCK_HYPER_SKILL : NKCUtilString.GET_STRING_RESULT_LIMIT_BREAK_UNIT_ONE_PARAM;
						float num4 = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier(resultUnitData.m_LimitBreakLevel) - NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier(resultUnitData.m_LimitBreakLevel - 1);
						NKCUtil.SetLabelText(this.m_lbTRANSCENDENCE_TEXT1, string.Format(format, num4 * 100f));
						NKMLimitBreakTemplet lbinfo3 = NKMUnitLimitBreakManager.GetLBInfo(resultUnitData.m_LimitBreakLevel - 1);
						NKMLimitBreakTemplet lbinfo4 = NKMUnitLimitBreakManager.GetLBInfo(resultUnitData.m_LimitBreakLevel);
						if (lbinfo3 != null && lbinfo4 != null)
						{
							NKCUtil.SetLabelText(this.m_lbMAXLEVEL_COUNT, string.Format(NKCUtilString.GET_STRING_RESULT_LIMIT_BREAK_UNIT_MAX_LEVEL_TWO_PARAM, lbinfo3.m_iMaxLevel, lbinfo4.m_iMaxLevel));
						}
					}
				}
				else if (this.m_Type == NKCUIGameResultGetUnit.Type.Ship)
				{
					if (this.m_CurrentUnitResultData.m_LimitBreakLevel > 0)
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_UNIT_TRANSCENDENCE_INFO, true);
						NKCUtil.SetGameobjectActive(this.m_objTranscendenceTextRoot, true);
						NKCUtil.SetGameobjectActive(this.m_LimitBreakTextRoot, false);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_SHIP_UPGRADE_SKILL, false);
						StringBuilder stringBuilder2 = new StringBuilder();
						NKMShipLevelUpTemplet shipLevelupTemplet = NKMShipManager.GetShipLevelupTemplet(this.m_CurrentUnitResultData.limitBreakStarCount, this.m_CurrentUnitResultData.m_LimitBreakLevel - 1);
						NKMShipLevelUpTemplet shipLevelupTemplet2 = NKMShipManager.GetShipLevelupTemplet(this.m_CurrentUnitResultData.limitBreakStarCount, this.m_CurrentUnitResultData.m_LimitBreakLevel);
						if (shipLevelupTemplet2 != null && shipLevelupTemplet != null)
						{
							stringBuilder2.Clear();
							if (shipLevelupTemplet.ShipMaxLevel == 100)
							{
								stringBuilder2.AppendFormat(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipLevelupTemplet.ShipMaxLevel);
							}
							else
							{
								stringBuilder2.Append("<color=#C57BF4>");
								stringBuilder2.AppendFormat(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipLevelupTemplet.ShipMaxLevel);
								stringBuilder2.Append("</color>");
							}
							stringBuilder2.Append(" > ");
							stringBuilder2.Append("<color=#C57BF4>");
							stringBuilder2.AppendFormat(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, shipLevelupTemplet2.ShipMaxLevel);
							stringBuilder2.Append("</color>");
							NKCUtil.SetLabelText(this.m_lbTCMaxLevel, stringBuilder2.ToString());
						}
						if (NKMShipManager.GetShipLimitBreakTemplet(this.m_CurrentUnitResultData.m_UnitID, this.m_CurrentUnitResultData.m_LimitBreakLevel) != null)
						{
							stringBuilder2.Clear();
							stringBuilder2.Append(string.Format(NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_OPEN, this.m_CurrentUnitResultData.m_LimitBreakLevel));
							NKCUtil.SetLabelText(this.m_lbTCCount, stringBuilder2.ToString());
						}
						float num5 = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplierForShip(resultUnitData.m_LimitBreakLevel) - NKMUnitLimitBreakManager.GetLimitBreakStatMultiplierForShip(resultUnitData.m_LimitBreakLevel - 1);
						NKCUtil.SetLabelText(this.m_lbTCGrowRate, NKCStringTable.GetString("SI_DP_RESULT_TRANSCENDENCE_UNIT_GROWTH_DIFFERENCE_ONE_PARAM", false), new object[]
						{
							Mathf.RoundToInt(num5 * 100f)
						});
					}
					else
					{
						for (int i = 0; i < this.m_lstNewSkill.Count; i++)
						{
							NKCUtil.SetGameobjectActive(this.m_lstNewSkill[i], false);
						}
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_UNIT_TRANSCENDENCE_INFO, false);
						NKCUtil.SetGameobjectActive(this.m_lbTRANSCENDENCE_TEXT1.gameObject, false);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_SHIP_UPGRADE_SKILL, true);
						NKCUtil.SetLabelText(this.m_lbSHIP_MAXLEVEL_COUNT, string.Format(NKCUtilString.GET_STRING_RESULT_LIMIT_BREAK_UNIT_MAX_LEVEL_TWO_PARAM, this.m_iCurMaxShipLv, this.m_iNextMaxShipLv));
						NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(resultUnitData.m_UnitID);
						if (unitTempletBase2 != null)
						{
							for (int i = 0; i < this.m_lstSkill.Count; i++)
							{
								NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase2, i);
								if (shipSkillTempletByIndex != null)
								{
									NKCUtil.SetImageSprite(this.m_lstSkill[i], NKCUtil.GetSkillIconSprite(shipSkillTempletByIndex), false);
								}
								else
								{
									NKCUtil.SetGameobjectActive(this.m_lstSkill[i].gameObject, false);
								}
								if (i > 1 && this.m_iCurSkillCnt - 1 < i)
								{
									NKCUtil.SetGameobjectActive(this.m_lstNewSkill[i - 1], true);
								}
							}
						}
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_UNIT_TRANSCENDENCE_INFO, false);
				}
				this.SetBlackIllust(unitTempletBase, resultUnitData);
				this.SetIllust(unitTempletBase, resultUnitData);
				NKCUtil.SetGameobjectActive(this.m_objGameOpenAwakenFX, unitTempletBase.m_bAwaken);
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.SNS_SHARE_BUTTON) && NKCPublisherModule.Marketing.SnsShareEnabled(unitFromUID))
				{
					if (NKCPublisherModule.Marketing.IsOnlyUnitShare())
					{
						NKCUtil.SetGameobjectActive(this.m_csbtnShare, unitFromUID != null && this.m_NKMUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL);
						NKCUtil.SetGameobjectActive(this.m_objFacebookMark, true);
						return;
					}
					NKCUtil.SetGameobjectActive(this.m_csbtnShare, true);
					NKCUtil.SetGameobjectActive(this.m_objFacebookMark, false);
					return;
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnShare, false);
				}
			}
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x0020D10B File Offset: 0x0020B30B
		public void SetShipData(int curSkillCnt, int curMaxLv, int nextMaxLv)
		{
			this.m_iCurSkillCnt = curSkillCnt;
			this.m_iCurMaxShipLv = curMaxLv;
			this.m_iNextMaxShipLv = nextMaxLv;
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x0020D124 File Offset: 0x0020B324
		public void OnTouchAnywhere()
		{
			if (!NKCUIManager.IsTopmostUI(this))
			{
				return;
			}
			if (!this.m_bEnableNextChar && this.m_fTime + 1.1f < Time.time)
			{
				this.m_bEnableNextChar = true;
				this.ShowNext(true);
				return;
			}
			if (this.m_animGetUnit.GetCurrentAnimatorStateInfo(0).IsName("START") && this.m_NKMUnitTempletBase != null && !NKCUIGameResultGetUnit.m_setFirstGetUnit.Contains(this.m_NKMUnitTempletBase.m_UnitID))
			{
				this.m_bEnableNextChar = false;
				this.m_fTime = Time.time;
				this.m_animGetUnit.SetTrigger("SKIP");
			}
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x0020D1C0 File Offset: 0x0020B3C0
		public void SkipAll()
		{
			base.Close();
			this.InvokeCallback();
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x0020D1D0 File Offset: 0x0020B3D0
		public void OnShare()
		{
			NKMUnitData cNKMUnitData = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_CurrentUnitResultData.m_UnitUID);
			if (NKCPublisherModule.Marketing.IsUseSnsSharePopup())
			{
				NKCPopupSnsShareMenu.Instance.Open(delegate(NKCPublisherModule.SNS_SHARE_TYPE e)
				{
					NKCPopupSnsShare.Instance.Open(NKCScenManager.CurrentUserData(), cNKMUnitData, e);
				});
				return;
			}
			NKCPopupSnsShare.Instance.Open(NKCScenManager.CurrentUserData(), cNKMUnitData, NKCPublisherModule.SNS_SHARE_TYPE.SST_FACEBOOK);
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x0020D23C File Offset: 0x0020B43C
		private void Update()
		{
			if (this.m_bEnableNextChar && (this.m_animGetUnit.GetCurrentAnimatorStateInfo(0).IsName("LOOP") || this.m_animGetUnit.GetCurrentAnimatorStateInfo(0).IsName("ON")))
			{
				this.m_bEnableNextChar = false;
				this.m_fTime = Time.time;
			}
			if (this.m_bCheckTimeAutoSkip && this.m_fTimeAutoSkip + 4f < Time.time)
			{
				this.m_bCheckTimeAutoSkip = false;
				this.OnTouchAnywhere();
			}
			if (!this.m_bPlayVoice && (this.m_animGetUnit.GetCurrentAnimatorStateInfo(0).IsName("TERM") || this.m_animGetUnit.GetCurrentAnimatorStateInfo(0).IsName("LOOP")))
			{
				NKCUIVoiceManager.PlayVoice(this.TypeToVoiceType(this.m_Type), this.m_NKMUnitTempletBase.m_UnitStrID, this.m_skinID, false, false);
				this.m_bPlayVoice = true;
			}
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x0020D32C File Offset: 0x0020B52C
		private void SetIllust(NKMUnitTempletBase targetUnitTempletBase, NKCUIGameResultGetUnit.GetUnitResultData unitData)
		{
			if (this.m_NKCASUISpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust);
			}
			this.m_NKCASUISpineIllust = null;
			if (targetUnitTempletBase != null)
			{
				this.m_NKCASUISpineIllust = NKCResourceUtility.OpenSpineIllust(targetUnitTempletBase, unitData.m_SkinID, false);
				if (this.m_NKCASUISpineIllust != null)
				{
					if (targetUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
					{
						this.m_NKCASUISpineIllust.SetParent(this.m_trRootSpineShip, false);
					}
					else
					{
						this.m_NKCASUISpineIllust.SetParent(this.m_trRootSpineIllust, false);
					}
					this.m_NKCASUISpineIllust.SetDefaultAnimation(targetUnitTempletBase, true, false);
					this.m_NKCASUISpineIllust.SetAnchoredPosition(Vector2.zero);
					this.m_NKCASUISpineIllust.SetIllustBackgroundEnable(false);
					this.m_NKCASUISpineIllust.SetSkinOption(0);
					NKCDescTemplet descTemplet = NKCDescMgr.GetDescTemplet(unitData.m_UnitID, unitData.m_SkinID);
					if (descTemplet != null)
					{
						NKCDescTemplet.NKCDescData nkcdescData = descTemplet.m_arrDescData[this.TypeToDescType(this.m_Type)];
						this.m_NKCASUISpineIllust.SetAnimation(nkcdescData.m_Ani, false, 0, true, 0f, true);
						return;
					}
					NKM_UNIT_TYPE nkm_UNIT_TYPE = targetUnitTempletBase.m_NKM_UNIT_TYPE;
					this.m_NKCASUISpineIllust.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_TOUCH, false, 0, true, 0f, true);
				}
			}
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x0020D448 File Offset: 0x0020B648
		private void SetBlackIllust(NKMUnitTempletBase targetUnitTempletBase, NKCUIGameResultGetUnit.GetUnitResultData unitData)
		{
			if (this.m_NKCASUIBlackSpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUIBlackSpineIllust);
			}
			this.m_NKCASUIBlackSpineIllust = null;
			if (targetUnitTempletBase != null)
			{
				this.m_NKCASUIBlackSpineIllust = NKCResourceUtility.OpenSpineIllust(targetUnitTempletBase, unitData.m_SkinID, false);
				if (this.m_NKCASUIBlackSpineIllust != null)
				{
					this.m_NKCASUIBlackSpineIllust.SetColor(Color.black);
					this.m_NKCASUIBlackSpineIllust.SetIllustBackgroundEnable(unitData.m_SkinID == 0);
					this.m_NKCASUIBlackSpineIllust.SetSkinOption(0);
					if (targetUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
					{
						this.m_NKCASUIBlackSpineIllust.SetParent(this.m_rtSPINEAREA_SHIP, false);
					}
					else
					{
						this.m_NKCASUIBlackSpineIllust.SetParent(this.m_rtSPINEAREA, false);
					}
					this.m_NKCASUIBlackSpineIllust.SetDefaultAnimation(targetUnitTempletBase, true, false);
					this.m_NKCASUIBlackSpineIllust.SetAnchoredPosition(Vector2.zero);
					this.m_NKCASUIBlackSpineIllust.SetVFX(false);
				}
			}
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x0020D528 File Offset: 0x0020B728
		public static void AddFirstGetUnit(NKMRewardData rewardData)
		{
			if (rewardData == null)
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData == null)
			{
				return;
			}
			if (rewardData.UnitDataList != null)
			{
				foreach (NKMUnitData nkmunitData in rewardData.UnitDataList)
				{
					if (armyData.IsFirstGetUnit(nkmunitData.m_UnitID))
					{
						NKCUIGameResultGetUnit.AddFirstGetUnit(nkmunitData.m_UnitID);
					}
				}
			}
			if (rewardData.OperatorList != null)
			{
				foreach (NKMOperator nkmoperator in rewardData.OperatorList)
				{
					if (armyData.IsFirstGetUnit(nkmoperator.id))
					{
						NKCUIGameResultGetUnit.AddFirstGetUnit(nkmoperator.id);
					}
				}
			}
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x0020D608 File Offset: 0x0020B808
		public static void AddFirstGetUnit(int unitID)
		{
			NKCUIGameResultGetUnit.m_setFirstGetUnit.Add(unitID);
		}

		// Token: 0x0600669C RID: 26268 RVA: 0x0020D616 File Offset: 0x0020B816
		public static bool HaveFirstGetUnit(int unitID)
		{
			return NKCUIGameResultGetUnit.m_setFirstGetUnit.Contains(unitID);
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x0020D624 File Offset: 0x0020B824
		private void ClearIllustMem()
		{
			if (this.m_NKCASUISpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust);
				this.m_NKCASUISpineIllust = null;
			}
			if (this.m_NKCASUIBlackSpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUIBlackSpineIllust);
				this.m_NKCASUIBlackSpineIllust = null;
			}
			NKCScenManager.GetScenManager().m_NKCMemoryCleaner.UnloadObjectPool();
		}

		// Token: 0x0600669E RID: 26270 RVA: 0x0020D688 File Offset: 0x0020B888
		public override void CloseInternal()
		{
			this.ClearIllustMem();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			NKCUIGameResultGetUnit.m_setFirstGetUnit.Clear();
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
			this.m_bForceHideGetUnitMark = false;
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x0020D6DA File Offset: 0x0020B8DA
		private void InvokeCallback()
		{
			NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack nkcuigrgetUnitCallBack = this.m_NKCUIGRGetUnitCallBack;
			if (nkcuigrgetUnitCallBack != null)
			{
				nkcuigrgetUnitCallBack();
			}
			this.m_NKCUIGRGetUnitCallBack = null;
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x0020D6F4 File Offset: 0x0020B8F4
		private int TypeToDescType(NKCUIGameResultGetUnit.Type type)
		{
			if (type != NKCUIGameResultGetUnit.Type.Get && type == NKCUIGameResultGetUnit.Type.LimitBreak)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x0020D700 File Offset: 0x0020B900
		private VOICE_TYPE TypeToVoiceType(NKCUIGameResultGetUnit.Type type)
		{
			if (type != NKCUIGameResultGetUnit.Type.Get && type == NKCUIGameResultGetUnit.Type.LimitBreak)
			{
				return VOICE_TYPE.VT_GROWTH_ASCEND;
			}
			return VOICE_TYPE.VT_GET;
		}

		// Token: 0x060066A2 RID: 26274 RVA: 0x0020D710 File Offset: 0x0020B910
		public void SetBG()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			bool flag = gameOptionData != null && gameOptionData.UseVideoTexture;
			NKCUtil.SetGameobjectActive(this.m_objFallbackBG, !flag);
			if (flag)
			{
				NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
				if (subUICameraVideoPlayer != null)
				{
					subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
					subUICameraVideoPlayer.m_fMoviePlaySpeed = 1f;
					subUICameraVideoPlayer.Play("Contract_BG.mp4", true, false, new NKCUIComVideoPlayer.VideoPlayMessageCallback(this.VideoPlayMessageCallback), false);
				}
			}
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x0020D782 File Offset: 0x0020B982
		private void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message)
		{
			if (message == NKCUIComVideoPlayer.eVideoMessage.PlayFailed)
			{
				NKCUtil.SetGameobjectActive(this.m_objFallbackBG, true);
			}
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x0020D794 File Offset: 0x0020B994
		private void SetTag(NKCUIGameResultGetUnit.eUnitTagType type)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_UNIT_GET, type == NKCUIGameResultGetUnit.eUnitTagType.GetUnit);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_UNIT_NEW, type == NKCUIGameResultGetUnit.eUnitTagType.NewUnit);
			NKCUtil.SetGameobjectActive(this.m_objUnitLimitBreak, type == NKCUIGameResultGetUnit.eUnitTagType.LimitBreak);
			NKCUtil.SetGameobjectActive(this.m_objUnitTranscendence, type == NKCUIGameResultGetUnit.eUnitTagType.Transcendence);
			NKCUtil.SetGameobjectActive(this.m_objGetSkin, type == NKCUIGameResultGetUnit.eUnitTagType.GetSkin);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_RESULT_GET_SHIP_UPGRADE, type == NKCUIGameResultGetUnit.eUnitTagType.ShipUpgrade);
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x0020D7FC File Offset: 0x0020B9FC
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey != HotkeyEventType.Confirm)
			{
				if (hotkey == HotkeyEventType.ShowHotkey)
				{
					if (this.m_btnSkip != null)
					{
						NKCUIComHotkeyDisplay.OpenInstance(this.m_btnSkip.transform, new HotkeyEventType[]
						{
							HotkeyEventType.Confirm,
							HotkeyEventType.Skip
						});
					}
				}
				return false;
			}
			this.OnTouchAnywhere();
			return true;
		}

		// Token: 0x060066A6 RID: 26278 RVA: 0x0020D84A File Offset: 0x0020BA4A
		public override void OnHotkeyHold(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Skip)
			{
				this.OnTouchAnywhere();
			}
		}

		// Token: 0x04005227 RID: 21031
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_result";

		// Token: 0x04005228 RID: 21032
		private const string UI_ASSET_NAME = "NKM_UI_RESULT_GET_UNIT";

		// Token: 0x04005229 RID: 21033
		private static NKCUIGameResultGetUnit m_Instance;

		// Token: 0x0400522A RID: 21034
		[Header("Sound")]
		public GameObject m_SFX_STAR;

		// Token: 0x0400522B RID: 21035
		public GameObject m_SFX_STAR_SSR;

		// Token: 0x0400522C RID: 21036
		public GameObject m_SFX_STAR_OTHER;

		// Token: 0x0400522D RID: 21037
		public Animator m_animGetUnit;

		// Token: 0x0400522E RID: 21038
		public EventTrigger m_evtScreen;

		// Token: 0x0400522F RID: 21039
		[Header("유닛 기본정보")]
		public List<GameObject> m_lstUnitGradeStarOFF;

		// Token: 0x04005230 RID: 21040
		public List<GameObject> m_lstUnitGradeStar;

		// Token: 0x04005231 RID: 21041
		public List<NKCUIComResultStar> m_lstResultStar;

		// Token: 0x04005232 RID: 21042
		public Text m_lbTitle;

		// Token: 0x04005233 RID: 21043
		public Text m_lbName;

		// Token: 0x04005234 RID: 21044
		public Text m_lbNameShadow;

		// Token: 0x04005235 RID: 21045
		public List<GameObject> m_lstStar;

		// Token: 0x04005236 RID: 21046
		public GameObject m_objSilhouetCounter;

		// Token: 0x04005237 RID: 21047
		public GameObject m_objSilhouetMarkSoldier;

		// Token: 0x04005238 RID: 21048
		public GameObject m_objSilhouetMarkMech;

		// Token: 0x04005239 RID: 21049
		public GameObject m_objSilhouetMarkShip;

		// Token: 0x0400523A RID: 21050
		public List<GameObject> m_lstUnitRank;

		// Token: 0x0400523B RID: 21051
		public List<GameObject> m_lstUnitGradeFX;

		// Token: 0x0400523C RID: 21052
		public List<GameObject> m_lstUnitLiiustFX;

		// Token: 0x0400523D RID: 21053
		public GameObject m_objRankAwakenSR;

		// Token: 0x0400523E RID: 21054
		public GameObject m_objRankAwakenSSR;

		// Token: 0x0400523F RID: 21055
		public GameObject m_objBGAwakenSR;

		// Token: 0x04005240 RID: 21056
		public GameObject m_objBGAwakenSSR;

		// Token: 0x04005241 RID: 21057
		public GameObject m_NKM_UI_RESULT_GET_UNIT_GET;

		// Token: 0x04005242 RID: 21058
		public GameObject m_NKM_UI_RESULT_GET_UNIT_NEW;

		// Token: 0x04005243 RID: 21059
		public GameObject m_objUnitLimitBreak;

		// Token: 0x04005244 RID: 21060
		public GameObject m_objUnitTranscendence;

		// Token: 0x04005245 RID: 21061
		public GameObject m_objGetSkin;

		// Token: 0x04005246 RID: 21062
		public GameObject m_NKM_UI_RESULT_GET_SHIP_UPGRADE;

		// Token: 0x04005247 RID: 21063
		[Header("초월 관련")]
		public GameObject m_NKM_UI_RESULT_GET_UNIT_TRANSCENDENCE_INFO;

		// Token: 0x04005248 RID: 21064
		public GameObject m_LimitBreakTextRoot;

		// Token: 0x04005249 RID: 21065
		public Text m_lbMAXLEVEL_COUNT;

		// Token: 0x0400524A RID: 21066
		public Text m_lbSHIP_MAXLEVEL_COUNT;

		// Token: 0x0400524B RID: 21067
		public Text m_lbTRANSCENDENCE_TEXT1;

		// Token: 0x0400524C RID: 21068
		public GameObject m_TRANSCENDENCE_TEXT2;

		// Token: 0x0400524D RID: 21069
		[Header("초월각성 관련")]
		public GameObject m_objTranscendenceTextRoot;

		// Token: 0x0400524E RID: 21070
		public Text m_lbTCCount;

		// Token: 0x0400524F RID: 21071
		public Text m_lbTCMaxLevel;

		// Token: 0x04005250 RID: 21072
		public Text m_lbTCGrowRate;

		// Token: 0x04005251 RID: 21073
		[Header("그외")]
		public Image m_NKM_UI_RESULT_GET_UNIT_MARK;

		// Token: 0x04005252 RID: 21074
		public Text m_lbNKM_UI_RESULT_GET_UNIT_SUMMARY_STYLE_NAME;

		// Token: 0x04005253 RID: 21075
		public GameObject m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS;

		// Token: 0x04005254 RID: 21076
		public Image m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS_ICON;

		// Token: 0x04005255 RID: 21077
		public Text m_NKM_UI_RESULT_GET_UNIT_SUMMARY_CLASS_TEXT;

		// Token: 0x04005256 RID: 21078
		public GameObject m_NKM_UI_RESULT_GET_SHIP_UPGRADE_SKILL;

		// Token: 0x04005257 RID: 21079
		public List<Image> m_lstSkill;

		// Token: 0x04005258 RID: 21080
		public List<Image> m_lstNewSkill;

		// Token: 0x04005259 RID: 21081
		public Text m_lbDailoogue;

		// Token: 0x0400525A RID: 21082
		public NKCUIComStateButton m_btnSkip;

		// Token: 0x0400525B RID: 21083
		public GameObject m_objGameOpenAwakenFX;

		// Token: 0x0400525C RID: 21084
		[Header("캐릭터 스파인 일러스트")]
		public Transform m_trRootSpineIllust;

		// Token: 0x0400525D RID: 21085
		public Transform m_trRootSpineShip;

		// Token: 0x0400525E RID: 21086
		public RectTransform m_rtSPINEAREA;

		// Token: 0x0400525F RID: 21087
		public RectTransform m_rtSPINEAREA_SHIP;

		// Token: 0x04005260 RID: 21088
		[Header("비디오 플레이 안 될때 대비용 배경")]
		public GameObject m_objFallbackBG;

		// Token: 0x04005261 RID: 21089
		[Header("공유 버튼")]
		public NKCUIComStateButton m_csbtnShare;

		// Token: 0x04005262 RID: 21090
		public GameObject m_objFacebookMark;

		// Token: 0x04005263 RID: 21091
		private NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack m_NKCUIGRGetUnitCallBack;

		// Token: 0x04005264 RID: 21092
		private List<NKCUIGameResultGetUnit.GetUnitResultData> m_listUnitData;

		// Token: 0x04005265 RID: 21093
		private int m_CurrIdx;

		// Token: 0x04005266 RID: 21094
		private bool m_bEnableNextChar = true;

		// Token: 0x04005267 RID: 21095
		private float m_fTime;

		// Token: 0x04005268 RID: 21096
		private float m_fTimeAutoSkip;

		// Token: 0x04005269 RID: 21097
		private bool m_bCheckTimeAutoSkip;

		// Token: 0x0400526A RID: 21098
		private bool m_bEnableTimeAutoSkip;

		// Token: 0x0400526B RID: 21099
		private const float SKIP_INTERVAL_TIME = 4f;

		// Token: 0x0400526C RID: 21100
		private bool m_bPlayVoice;

		// Token: 0x0400526D RID: 21101
		private NKCUIGameResultGetUnit.Type m_Type;

		// Token: 0x0400526E RID: 21102
		private static HashSet<int> m_setFirstGetUnit = new HashSet<int>();

		// Token: 0x0400526F RID: 21103
		private NKCUIGameResultGetUnit.GetUnitResultData m_CurrentUnitResultData;

		// Token: 0x04005270 RID: 21104
		private NKMUnitTempletBase m_NKMUnitTempletBase;

		// Token: 0x04005271 RID: 21105
		private int m_skinID;

		// Token: 0x04005272 RID: 21106
		private List<GameObject> m_lstStarOFF = new List<GameObject>();

		// Token: 0x04005273 RID: 21107
		private List<GameObject> m_lstStarONParent = new List<GameObject>();

		// Token: 0x04005274 RID: 21108
		private bool m_bForceHideGetUnitMark;

		// Token: 0x04005275 RID: 21109
		private NKCASUIUnitIllust m_NKCASUISpineIllust;

		// Token: 0x04005276 RID: 21110
		private NKCASUIUnitIllust m_NKCASUIBlackSpineIllust;

		// Token: 0x04005277 RID: 21111
		private int m_iCurSkillCnt;

		// Token: 0x04005278 RID: 21112
		private int m_iCurMaxShipLv;

		// Token: 0x04005279 RID: 21113
		private int m_iNextMaxShipLv;

		// Token: 0x02001676 RID: 5750
		public enum Type
		{
			// Token: 0x0400A462 RID: 42082
			Get,
			// Token: 0x0400A463 RID: 42083
			LimitBreak,
			// Token: 0x0400A464 RID: 42084
			Ship,
			// Token: 0x0400A465 RID: 42085
			Skin
		}

		// Token: 0x02001677 RID: 5751
		// (Invoke) Token: 0x0600B053 RID: 45139
		public delegate void NKCUIGRGetUnitCallBack();

		// Token: 0x02001678 RID: 5752
		public enum eUnitTagType
		{
			// Token: 0x0400A467 RID: 42087
			None,
			// Token: 0x0400A468 RID: 42088
			GetUnit,
			// Token: 0x0400A469 RID: 42089
			NewUnit,
			// Token: 0x0400A46A RID: 42090
			LimitBreak,
			// Token: 0x0400A46B RID: 42091
			Transcendence,
			// Token: 0x0400A46C RID: 42092
			GetSkin,
			// Token: 0x0400A46D RID: 42093
			ShipUpgrade
		}

		// Token: 0x02001679 RID: 5753
		public struct GetUnitResultData
		{
			// Token: 0x0600B056 RID: 45142 RVA: 0x0035ED4C File Offset: 0x0035CF4C
			public GetUnitResultData(NKMUnitData unitData)
			{
				this.m_UnitID = unitData.m_UnitID;
				this.m_SkinID = unitData.m_SkinID;
				this.m_LimitBreakLevel = (int)unitData.m_LimitBreakLevel;
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
				NKM_UNIT_TYPE nkm_UNIT_TYPE = unitTempletBase.m_NKM_UNIT_TYPE;
				if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL && nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					this.limitBreakStarCount = unitData.GetStarGrade();
					this.StarMaxCount = 6;
					this.bFullTranscendence = NKMShipManager.IsMaxLimitBreak(unitData);
				}
				else
				{
					this.limitBreakStarCount = unitData.GetStarGrade(unitTempletBase);
					this.StarMaxCount = unitTempletBase.m_StarGradeMax;
					this.bFullTranscendence = NKMUnitLimitBreakManager.IsMaxLimitBreak(unitData, true);
				}
				this.m_bSkin = false;
				this.m_UnitUID = unitData.m_UnitUID;
			}

			// Token: 0x0600B057 RID: 45143 RVA: 0x0035EDF0 File Offset: 0x0035CFF0
			public GetUnitResultData(NKMOperator operatorData)
			{
				this.m_UnitID = operatorData.id;
				this.m_SkinID = 0;
				this.m_LimitBreakLevel = 3;
				this.limitBreakStarCount = -1;
				this.StarMaxCount = -1;
				this.bFullTranscendence = false;
				this.m_bSkin = false;
				this.m_UnitUID = operatorData.uid;
			}

			// Token: 0x0600B058 RID: 45144 RVA: 0x0035EE40 File Offset: 0x0035D040
			public GetUnitResultData(NKMSkinTemplet skinTemplet)
			{
				this.m_UnitID = skinTemplet.m_SkinEquipUnitID;
				this.m_SkinID = skinTemplet.m_SkinID;
				this.m_LimitBreakLevel = 3;
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(skinTemplet.m_SkinEquipUnitID);
				this.StarMaxCount = unitTempletBase.m_StarGradeMax;
				this.limitBreakStarCount = unitTempletBase.m_StarGradeMax;
				this.bFullTranscendence = false;
				this.m_bSkin = true;
				this.m_UnitUID = 0L;
			}

			// Token: 0x0600B059 RID: 45145 RVA: 0x0035EEA6 File Offset: 0x0035D0A6
			public static List<NKCUIGameResultGetUnit.GetUnitResultData> ConvertList(NKMRewardData rewardData)
			{
				if (rewardData == null)
				{
					return new List<NKCUIGameResultGetUnit.GetUnitResultData>();
				}
				return NKCUIGameResultGetUnit.GetUnitResultData.ConvertList(rewardData.UnitDataList, rewardData.OperatorList, rewardData.SkinIdList);
			}

			// Token: 0x0600B05A RID: 45146 RVA: 0x0035EEC8 File Offset: 0x0035D0C8
			public static List<NKCUIGameResultGetUnit.GetUnitResultData> ConvertList(List<NKMRewardData> lstRewardData)
			{
				if (lstRewardData == null)
				{
					return new List<NKCUIGameResultGetUnit.GetUnitResultData>();
				}
				List<NKCUIGameResultGetUnit.GetUnitResultData> list = new List<NKCUIGameResultGetUnit.GetUnitResultData>();
				foreach (NKMRewardData rewardData in lstRewardData)
				{
					list.AddRange(NKCUIGameResultGetUnit.GetUnitResultData.ConvertList(rewardData));
				}
				return list;
			}

			// Token: 0x0600B05B RID: 45147 RVA: 0x0035EF2C File Offset: 0x0035D12C
			public static List<NKCUIGameResultGetUnit.GetUnitResultData> ConvertList(IEnumerable<NKMUnitData> lstUnitData, IEnumerable<NKMOperator> lstOperator, IEnumerable<int> lstSkinID)
			{
				List<NKCUIGameResultGetUnit.GetUnitResultData> list = new List<NKCUIGameResultGetUnit.GetUnitResultData>();
				if (lstUnitData != null)
				{
					foreach (NKMUnitData unitData in lstUnitData)
					{
						list.Add(new NKCUIGameResultGetUnit.GetUnitResultData(unitData));
					}
				}
				if (lstOperator != null)
				{
					foreach (NKMOperator operatorData in lstOperator)
					{
						list.Add(new NKCUIGameResultGetUnit.GetUnitResultData(operatorData));
					}
				}
				if (lstSkinID != null)
				{
					foreach (int skinID in lstSkinID)
					{
						NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
						if (skinTemplet != null)
						{
							list.Add(new NKCUIGameResultGetUnit.GetUnitResultData(skinTemplet));
						}
					}
				}
				return list;
			}

			// Token: 0x0400A46E RID: 42094
			public int m_UnitID;

			// Token: 0x0400A46F RID: 42095
			public int m_SkinID;

			// Token: 0x0400A470 RID: 42096
			public long m_UnitUID;

			// Token: 0x0400A471 RID: 42097
			public int m_LimitBreakLevel;

			// Token: 0x0400A472 RID: 42098
			public int limitBreakStarCount;

			// Token: 0x0400A473 RID: 42099
			public int StarMaxCount;

			// Token: 0x0400A474 RID: 42100
			public bool bFullTranscendence;

			// Token: 0x0400A475 RID: 42101
			public bool m_bSkin;

			// Token: 0x02001A82 RID: 6786
			public enum eMode
			{
				// Token: 0x0400AE92 RID: 44690
				GetUnit,
				// Token: 0x0400AE93 RID: 44691
				GetSkin
			}
		}
	}
}

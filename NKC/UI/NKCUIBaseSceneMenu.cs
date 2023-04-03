using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000974 RID: 2420
	public class NKCUIBaseSceneMenu : NKCUIBase
	{
		// Token: 0x060061C6 RID: 25030 RVA: 0x001E9641 File Offset: 0x001E7841
		public override void OnCloseInstance()
		{
			UnityEngine.Object.Destroy(this.m_NKM_UI_BASE_NPC.gameObject);
			UnityEngine.Object.Destroy(this.m_NUM_BASE_BG.gameObject);
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060061C7 RID: 25031 RVA: 0x001E9663 File Offset: 0x001E7863
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060061C8 RID: 25032 RVA: 0x001E9666 File Offset: 0x001E7866
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060061C9 RID: 25033 RVA: 0x001E9669 File Offset: 0x001E7869
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GetBaseMenuName(this.m_CurMenu);
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060061CA RID: 25034 RVA: 0x001E9676 File Offset: 0x001E7876
		private string MenuNameEng
		{
			get
			{
				return NKCUtilString.GetBaseMenuNameEng(this.m_CurMenu);
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060061CB RID: 25035 RVA: 0x001E9683 File Offset: 0x001E7883
		private string SubMenuDetail
		{
			get
			{
				return NKCUtilString.GetBaseSubMenuDetail(this.m_CurMenu);
			}
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x001E9690 File Offset: 0x001E7890
		private void Awake()
		{
			this.InitEvent();
			GameObject gameObject = GameObject.Find("NKM_UI_BASE_LEFT_detail");
			if (gameObject == null)
			{
				Debug.LogError("Can not found GameObject - NKM_UI_BASE_LEFT_detail");
				return;
			}
			this.m_txtNKM_UI_BASE_LEFT_detail = gameObject.GetComponent<Text>();
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x001E96D0 File Offset: 0x001E78D0
		private void InitEvent()
		{
			EventTrigger component = GameObject.Find("NUM_BASE_BG").GetComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener(new UnityAction<BaseEventData>(NKCSystemEvent.UI_SCEN_BG_DRAG));
			component.triggers.Add(entry);
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x001E971C File Offset: 0x001E791C
		public void Open(bool bShortCutOpen = false)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.m_IsShortCutOpen = bShortCutOpen;
			this.SwitchBackObject(true, false);
			this.SetUnlockContents();
			this.SetEventObject();
			this.OpenAnimation(this.m_CurMenu);
			this.CloseAllSubMenu();
			if (this.m_IsShortCutOpen)
			{
				this.ChangeMainMenu(this.m_CurMenu);
			}
			base.UIOpened(true);
			this.NotifyCheck();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_BASE_LEFT, false);
			this.TutorialCheck(true);
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x001E97A4 File Offset: 0x001E79A4
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			Debug.LogWarning("CloseInternal");
			this.SwitchBackObject(false, false);
			this.m_OldMenu = this.m_CurMenu;
			this.m_CurMenu = NKCUIBaseSceneMenu.BaseSceneMenuType.Base;
			this.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Base, false);
			this.CloseAllNPC();
			this.CloseMenuDiscription(this.m_OldMenu, -1, true);
			this.bTransition = false;
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x001E9812 File Offset: 0x001E7A12
		public override void Hide()
		{
			base.Hide();
			this.bTransition = false;
			this.SwitchBackObject(false, true);
			this.DisableMenuBtn(this.m_OldMenu);
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x001E9835 File Offset: 0x001E7A35
		public override void UnHide()
		{
			base.UnHide();
			this.SwitchBackObject(true, false);
			this.m_IsShortCutOpen = true;
			this.OpenAnimation(this.m_CurMenu);
			this.m_IsShortCutOpen = false;
			this.NotifyCheck();
			this.TutorialCheck(true);
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x001E986D File Offset: 0x001E7A6D
		public override void OnBackButton()
		{
			if (this.bTransition)
			{
				return;
			}
			if (this.m_CurMenu != NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				this.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Base, false);
				return;
			}
			this.bTransition = true;
			this.CallFuncAfterPlayCloseAnimation(this.m_CurMenu, new UnityAction(this.ExitMenu), false);
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x001E98AC File Offset: 0x001E7AAC
		private void SwitchBackObject(bool bActive, bool bBGActive = false)
		{
			if (null != this.m_NKM_UI_BASE_NPC)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_BASE_NPC, bActive);
			}
			if (null != this.m_NUM_BASE_BG)
			{
				if (bBGActive)
				{
					NKCUtil.SetGameobjectActive(this.m_NUM_BASE_BG, bBGActive);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_NUM_BASE_BG, bActive);
			}
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x001E98FD File Offset: 0x001E7AFD
		private void ExitMenu()
		{
			if (this.m_CurMenu == NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				this.CloseNPC(this.m_CurMenu);
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x001E9920 File Offset: 0x001E7B20
		public void Init(UnityAction<NKC_SCEN_BASE.eUIOpenReserve> BeginUILoading, UnityAction OpenSubUI)
		{
			this.m_ReceiveUILoad = BeginUILoading;
			this.m_CallUI = OpenSubUI;
			this.InitToggleBtn();
			this.InitBackground();
			this.InitNPC();
			if (this.m_BaseScenMenuBtn != null)
			{
				for (int i = 0; i < this.m_BaseScenMenuBtn.Length; i++)
				{
					if (this.m_BaseScenMenuBtn[i].subBtn != null)
					{
						BaseSceneMenuBtn.BaseSceneMenuSubBtn[] subBtn = this.m_BaseScenMenuBtn[i].subBtn;
						for (int j = 0; j < subBtn.Length; j++)
						{
							BaseSceneMenuBtn.BaseSceneMenuSubBtn BaseScenMenuSubBtn = subBtn[j];
							if (null != BaseScenMenuSubBtn.Btn)
							{
								BaseScenMenuSubBtn.Btn.PointerClick.AddListener(delegate()
								{
									this.EnterSubUI(BaseScenMenuSubBtn.Type);
								});
							}
							if (null != BaseScenMenuSubBtn.LockedBtn)
							{
								BaseScenMenuSubBtn.LockedBtn.PointerClick.AddListener(delegate()
								{
									this.EnterSubUI(BaseScenMenuSubBtn.Type);
								});
								BaseScenMenuSubBtn.LockedBtn.m_bGetCallbackWhileLocked = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x001E9A34 File Offset: 0x001E7C34
		private void InitToggleBtn()
		{
			if (null != this.m_LabToggleBtn)
			{
				this.m_LabToggleBtn.m_bGetCallbackWhileLocked = true;
				this.m_LabToggleBtn.OnValueChanged.RemoveAllListeners();
				this.m_LabToggleBtn.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Lab, false);
				});
			}
			else
			{
				Debug.LogError("NKCUIBaseSceneMenu - LabToggleBtn is null!");
			}
			if (null != this.m_FactoryToggleBtn)
			{
				this.m_FactoryToggleBtn.m_bGetCallbackWhileLocked = true;
				this.m_FactoryToggleBtn.OnValueChanged.RemoveAllListeners();
				this.m_FactoryToggleBtn.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Factory, false);
				});
			}
			else
			{
				Debug.LogError("NKCUIBaseSceneMenu - FactoryToggleBtn is null!");
			}
			if (null != this.m_HangarToggleBtn)
			{
				this.m_HangarToggleBtn.m_bGetCallbackWhileLocked = true;
				this.m_HangarToggleBtn.OnValueChanged.RemoveAllListeners();
				this.m_HangarToggleBtn.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar, false);
				});
			}
			else
			{
				Debug.LogError("NKCUIBaseSceneMenu - HangarToggleBtn is null!");
			}
			if (null != this.m_PersonnelToggleBtn)
			{
				this.m_PersonnelToggleBtn.m_bGetCallbackWhileLocked = true;
				this.m_PersonnelToggleBtn.OnValueChanged.RemoveAllListeners();
				this.m_PersonnelToggleBtn.OnValueChanged.AddListener(delegate(bool <p0>)
				{
					this.ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel, false);
				});
			}
			else
			{
				Debug.LogError("NKCUIBaseSceneMenu - PersonnelToggleBtn is null!");
			}
			this.m_MenuBtnToggleGroup.SetAllToggleUnselected();
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x001E9B94 File Offset: 0x001E7D94
		private void InitBackground()
		{
			if (null != this.m_NUM_BASE_BG)
			{
				this.m_NUM_BASE_BG.SetParent(NKCUIManager.rectMidCanvas);
				this.m_NUM_BASE_BG.anchoredPosition3D = new Vector3(0f, 0f, 150f);
				this.m_NUM_BASE_BG.anchoredPosition = Vector2.zero;
				NKCCamera.RescaleRectToCameraFrustrum(this.m_NUM_BASE_BG, NKCCamera.GetCamera(), new Vector2(200f, 200f), -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
				this.m_srScenBG = this.m_NUM_BASE_BG.gameObject.GetComponentInChildren<SpriteRenderer>();
				NKCUtil.SetGameobjectActive(this.m_NUM_BASE_BG, false);
			}
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x001E9C3C File Offset: 0x001E7E3C
		private void InitNPC()
		{
			if (null != this.m_NKM_UI_BASE_NPC)
			{
				this.m_NKM_UI_BASE_NPC.SetParent(NKCUIManager.rectMidCanvas);
				this.m_NKM_UI_BASE_NPC.anchoredPosition3D = Vector3.zero;
				this.m_NKM_UI_BASE_NPC.anchoredPosition = Vector2.zero;
				this.m_NKM_UI_BASE_NPC_BG_GLOW_Img = GameObject.Find("NKM_UI_BASE_NPC_BG_GLOW").gameObject.GetComponent<Image>();
				this.m_NKM_UI_BASE_NPC_BG_GLOW_Img_OriginColor = this.m_NKM_UI_BASE_NPC_BG_GLOW_Img.color;
			}
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x001E9CB4 File Offset: 0x001E7EB4
		public void ChangeMenu(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu, bool bReturnIfSameMenu = false)
		{
			if (bReturnIfSameMenu && this.m_CurMenu == newMenu)
			{
				return;
			}
			if (!this.IsContentsUnlocked(newMenu))
			{
				this.ShowLockedMessage(newMenu);
				return;
			}
			this.m_OldMenu = this.m_CurMenu;
			if (this.m_CurMenu == newMenu)
			{
				if (this.m_CurMenu != NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
				{
					this.m_CurMenu = NKCUIBaseSceneMenu.BaseSceneMenuType.Base;
					this.OpenNPC(this.m_CurMenu);
					this.ExitSubUI();
					this.EnableBaseMenu();
					this.DisableMenuBtn(this.m_OldMenu);
				}
				return;
			}
			this.m_CurMenu = newMenu;
			if (this.m_OldMenu == NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_BASE_LEFT, true);
				this.OpenSubMenuAnimate(this.m_CurMenu);
				this.OpenMenuDescription(this.m_CurMenu);
				this.CloseNPC(this.m_OldMenu);
				this.OpenNPC(this.m_CurMenu);
				NKCUIManager.UpdateUpsideMenu();
				return;
			}
			if (this.m_CurMenu == NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				this.OpenNPC(this.m_CurMenu);
				this.ExitSubUI();
				return;
			}
			this.ChangeSubMenu(this.m_OldMenu, true);
			this.OpenMenuBackGround(newMenu);
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x001E9DA8 File Offset: 0x001E7FA8
		private void EnableBaseMenu()
		{
			this.m_srScenBG.sprite = this.m_BaseScenMenuBtn[0].spBackground;
			this.m_srScenBG.DOColor(Color.white, 0.15f);
			NKCUIManager.UpdateUpsideMenu();
			this.m_MenuBtnToggleGroup.SetAllToggleUnselected();
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x001E9DE8 File Offset: 0x001E7FE8
		private void EnterSubUI(NKC_SCEN_BASE.eUIOpenReserve Type)
		{
			switch (Type)
			{
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Train:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LAB_TRAINING, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LAB_TRAINING, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.LAB_Transcendence:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.LAB_LIMITBREAK, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.LAB_LIMITBREAK, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Craft:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_CRAFT, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_CRAFT, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_ENCHANT, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Factory_Tunning:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_TUNING, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_TUNING, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Hangar_Build:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPBUILD, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.HANGER_SHIPBUILD, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Hangar_Shipyard:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPYARD, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.HANGER_SHIPYARD, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_LIFETIME, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_LIFETIME, 0);
					return;
				}
				break;
			case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_SCOUT, 0, 0))
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_SCOUT, 0);
					return;
				}
				break;
			}
			this.bTransition = true;
			this.m_ReceiveUILoad(Type);
			this.CallFuncAfterPlayCloseAnimation(this.m_CurMenu, this.m_CallUI, true);
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x001E9F32 File Offset: 0x001E8132
		private void ExitSubUI()
		{
			this.CloseSubMenuAnimate(this.m_OldMenu);
			this.CloseNPC(this.m_OldMenu);
			this.CloseMenuDiscription(this.m_OldMenu, -1, true);
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x001E9F5B File Offset: 0x001E815B
		private void OpenAnimation(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			if (this.m_BaseScenMenuBtn == null)
			{
				return;
			}
			if (newMenu == NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				this.EnableBaseMenu();
				this.OpenMenuDescription(newMenu);
				this.OpenNPC(newMenu);
				return;
			}
			this.OpenMenuBackGround(newMenu);
			this.OpenMenuDescription(newMenu);
			this.OpenNPC(newMenu);
			this.OpenSubMenuAnimate(newMenu);
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x001E9F9A File Offset: 0x001E819A
		private void CloseBaseMenu(bool IsHalfTime = false, bool needDelay = false, UnityAction callBackFunc = null)
		{
			this.m_BaseScenMenuBtn[0].animator.SetTrigger("Outro");
			if (callBackFunc != null)
			{
				base.StartCoroutine(this.waitCallBack(callBackFunc, 0.3f));
			}
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x001E9FC9 File Offset: 0x001E81C9
		private IEnumerator waitCallBack(UnityAction callBackFunc, float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			callBackFunc();
			yield break;
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x001E9FE0 File Offset: 0x001E81E0
		private void OpenSubMenuAnimate(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			for (int i = 1; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				BaseSceneMenuBtn baseSceneMenuBtn = this.m_BaseScenMenuBtn[i];
				if (baseSceneMenuBtn.Type == newMenu)
				{
					NKCUtil.SetGameobjectActive(baseSceneMenuBtn.obj, true);
					if (baseSceneMenuBtn.animator != null && base.gameObject.activeSelf)
					{
						baseSceneMenuBtn.animator.SetTrigger("Intro");
						Debug.Log("OpenSubMenuAnimate " + newMenu.ToString() + ", Play 'Intro'");
						base.StartCoroutine(this.PlayAniDelay(0.3f));
					}
					if (newMenu != NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
					{
						NKCUtil.SetGameobjectActive(baseSceneMenuBtn.obj, true);
					}
					this.m_srScenBG.sprite = baseSceneMenuBtn.spBackground;
					return;
				}
			}
		}

		// Token: 0x060061E1 RID: 25057 RVA: 0x001EA0A6 File Offset: 0x001E82A6
		private IEnumerator PlayAniDelay(float time)
		{
			this.bTransition = true;
			yield return new WaitForSeconds(time);
			this.bTransition = false;
			yield break;
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x001EA0BC File Offset: 0x001E82BC
		private void OpenMenuBackGround(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			for (int i = 1; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				BaseSceneMenuBtn baseSceneMenuBtn = this.m_BaseScenMenuBtn[i];
				for (int j = 0; j < baseSceneMenuBtn.subBtn.Length; j++)
				{
					NKCUtil.SetGameobjectActive(baseSceneMenuBtn.subBtn[j].m_objEvent, this.IsEventEnabled(baseSceneMenuBtn.subBtn[j].Type));
					if (newMenu == baseSceneMenuBtn.Type)
					{
						if (newMenu != NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
						{
							NKCUtil.SetGameobjectActive(baseSceneMenuBtn.obj, true);
						}
						this.m_srScenBG.sprite = baseSceneMenuBtn.spBackground;
						this.m_srScenBG.DOColor(Color.white, 0.15f);
					}
				}
			}
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x001EA164 File Offset: 0x001E8364
		private bool IsEventEnabled(NKC_SCEN_BASE.eUIOpenReserve type)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				if (type == NKC_SCEN_BASE.eUIOpenReserve.Factory_Craft)
				{
					return NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT);
				}
				if (type - NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant <= 1)
				{
					return NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT);
				}
				if (type == NKC_SCEN_BASE.eUIOpenReserve.Personnel_Negotiate)
				{
					return NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT);
				}
			}
			return false;
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x001EA1B6 File Offset: 0x001E83B6
		private void OpenMenuDescription(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			this.ChangeMenuDiscription(newMenu);
			this.OpenMenuDiscriptionAnimation(newMenu);
		}

		// Token: 0x060061E5 RID: 25061 RVA: 0x001EA1C6 File Offset: 0x001E83C6
		private void ChangeMenuDiscription(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			if (newMenu == NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				return;
			}
			NKCUtil.SetLabelText(this.BaseTitle, this.MenuName);
			NKCUtil.SetLabelText(this.BaseTitleEng, this.MenuNameEng);
			NKCUtil.SetLabelText(this.m_txtNKM_UI_BASE_LEFT_detail, this.SubMenuDetail);
		}

		// Token: 0x060061E6 RID: 25062 RVA: 0x001EA1FF File Offset: 0x001E83FF
		private void OpenMenuDiscriptionAnimation(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			if (newMenu == NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				return;
			}
			this.SwitchLeftMenuAnimation(true);
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x001EA20C File Offset: 0x001E840C
		private void SwitchLeftMenuAnimation(bool bIntro)
		{
			if (bIntro)
			{
				this.m_Ani_LeftMenu.SetTrigger("Intro");
				return;
			}
			this.m_Ani_LeftMenu.SetTrigger("Outro");
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x001EA234 File Offset: 0x001E8434
		private void OpenNPC(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			switch (newMenu)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Base:
				NKCUtil.SetGameobjectActive(this.m_objNPC_Base, true);
				this.AddNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Base);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				NKCUtil.SetGameobjectActive(this.m_objNPCLab_Assistant, true);
				NKCUtil.SetGameobjectActive(this.m_objNPCLab_Professor, true);
				this.AddNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Lab);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				NKCUtil.SetGameobjectActive(this.m_objNPCFactory, true);
				this.AddNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Factory);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				NKCUtil.SetGameobjectActive(this.m_objNPCHangar, true);
				this.AddNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				NKCUtil.SetGameobjectActive(this.m_objNPCPersonnel, true);
				this.AddNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel);
				break;
			}
			this.m_NKM_UI_BASE_NPC_BG_GLOW_Img.DOColor(this.m_NKM_UI_BASE_NPC_BG_GLOW_Img_OriginColor, 0.15f);
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x001EA2E8 File Offset: 0x001E84E8
		private void CloseNPC(NKCUIBaseSceneMenu.BaseSceneMenuType oldMenu)
		{
			if (oldMenu <= NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel)
			{
				this.ReleaseNPC(oldMenu);
			}
			Color nkm_UI_BASE_NPC_BG_GLOW_Img_OriginColor = this.m_NKM_UI_BASE_NPC_BG_GLOW_Img_OriginColor;
			nkm_UI_BASE_NPC_BG_GLOW_Img_OriginColor.a = 0f;
			this.m_NKM_UI_BASE_NPC_BG_GLOW_Img.DOColor(nkm_UI_BASE_NPC_BG_GLOW_Img_OriginColor, 0.15f);
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x001EA328 File Offset: 0x001E8528
		public void ReleaseNPC(NKCUIBaseSceneMenu.BaseSceneMenuType type)
		{
			switch (type)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Base:
				NKCUtil.SetGameobjectActive(this.m_objNPC_Base, false);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				NKCUtil.SetGameobjectActive(this.m_objNPCLab_Assistant, false);
				NKCUtil.SetGameobjectActive(this.m_objNPCLab_Professor, false);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				NKCUtil.SetGameobjectActive(this.m_objNPCFactory, false);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				NKCUtil.SetGameobjectActive(this.m_objNPCHangar, false);
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				NKCUtil.SetGameobjectActive(this.m_objNPCPersonnel, false);
				break;
			}
			this.RemoveNPC(type);
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x001EA3A8 File Offset: 0x001E85A8
		private void CloseAllNPC()
		{
			NKCUtil.SetGameobjectActive(this.m_objNPC_Base, false);
			NKCUtil.SetGameobjectActive(this.m_objNPCLab_Assistant, false);
			NKCUtil.SetGameobjectActive(this.m_objNPCLab_Professor, false);
			NKCUtil.SetGameobjectActive(this.m_objNPCHangar, false);
			NKCUtil.SetGameobjectActive(this.m_objNPCPersonnel, false);
			NKCUtil.SetGameobjectActive(this.m_objNPCFactory, false);
			this.RemoveNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Base);
			this.RemoveNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Lab);
			this.RemoveNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar);
			this.RemoveNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel);
			this.RemoveNPC(NKCUIBaseSceneMenu.BaseSceneMenuType.Factory);
			NKCSoundManager.StopAllSound(SOUND_TRACK.VOICE);
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x001EA426 File Offset: 0x001E8626
		private void CallFuncAfterPlayCloseAnimation(NKCUIBaseSceneMenu.BaseSceneMenuType oldMenu, UnityAction callBackFunc = null, bool bCloseNPC = true)
		{
			if (this.m_BaseScenMenuBtn == null)
			{
				return;
			}
			if (oldMenu == NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				this.CloseBaseMenu(false, true, callBackFunc);
			}
			this.CloseMenuDiscription(oldMenu, -1, false);
			if (bCloseNPC)
			{
				this.CloseNPC(oldMenu);
			}
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x001EA450 File Offset: 0x001E8650
		private void ChangeMainMenu(NKCUIBaseSceneMenu.BaseSceneMenuType newMenu)
		{
			switch (newMenu)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				this.m_LabToggleBtn.Select(true, true, true);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				this.m_FactoryToggleBtn.Select(true, true, true);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				this.m_HangarToggleBtn.Select(true, true, true);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				this.m_PersonnelToggleBtn.Select(true, true, true);
				return;
			default:
				Debug.LogWarning("ChangeMainMenu - Undefined BaseSceneMenu Type : " + newMenu.ToString());
				return;
			}
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x001EA4D4 File Offset: 0x001E86D4
		private void ChangeSubMenu(NKCUIBaseSceneMenu.BaseSceneMenuType oldMenu, bool IsSubChange = false)
		{
			if (this.m_BaseScenMenuBtn == null)
			{
				return;
			}
			int changedIndex = this.CloseSubMenuAnimate(oldMenu);
			this.CloseNPC(oldMenu);
			this.CloseMenuDiscription(oldMenu, changedIndex, false);
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x001EA504 File Offset: 0x001E8704
		private void OpenSubMenu(int targetNum)
		{
			BaseSceneMenuBtn baseSceneMenuBtn = this.m_BaseScenMenuBtn[targetNum];
			if (targetNum == -1 || baseSceneMenuBtn == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(baseSceneMenuBtn.obj, false);
			NKCUIManager.UpdateUpsideMenu();
			this.OpenSubMenuAnimate(this.m_CurMenu);
			this.OpenMenuDescription(this.m_CurMenu);
			this.OpenNPC(this.m_CurMenu);
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x001EA558 File Offset: 0x001E8758
		private int CloseSubMenuAnimate(NKCUIBaseSceneMenu.BaseSceneMenuType oldMenu)
		{
			int result = -1;
			for (int i = 1; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				BaseSceneMenuBtn baseSceneMenuBtn = this.m_BaseScenMenuBtn[i];
				if (oldMenu == baseSceneMenuBtn.Type)
				{
					result = i;
					baseSceneMenuBtn.animator.SetTrigger("Outro");
				}
			}
			return result;
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x001EA59F File Offset: 0x001E879F
		private void CloseMenuDiscription(NKCUIBaseSceneMenu.BaseSceneMenuType oldMenu, int ChangedIndex = -1, bool returnBase = false)
		{
			if (oldMenu != NKCUIBaseSceneMenu.BaseSceneMenuType.Base)
			{
				if (ChangedIndex != -1)
				{
					this.PlaySubAni(oldMenu, ChangedIndex);
					return;
				}
				if (returnBase)
				{
					this.SwitchLeftMenuAnimation(false);
					this.EnableBaseMenu();
					return;
				}
				this.SwitchLeftMenuAnimation(false);
			}
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x001EA5CC File Offset: 0x001E87CC
		private void PlaySubAni(NKCUIBaseSceneMenu.BaseSceneMenuType oldMenu, int idx)
		{
			if (base.gameObject.activeSelf)
			{
				base.StartCoroutine(this.PlaySubAni(oldMenu, idx, 0.15f));
				return;
			}
			for (int i = 0; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				if (this.m_BaseScenMenuBtn[i].Type == oldMenu)
				{
					NKCUtil.SetGameobjectActive(this.m_BaseScenMenuBtn[i].obj, false);
					break;
				}
			}
			this.OpenSubMenu(idx);
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x001EA63A File Offset: 0x001E883A
		private IEnumerator PlaySubAni(NKCUIBaseSceneMenu.BaseSceneMenuType oldMenu, int idx, float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			for (int i = 0; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				if (this.m_BaseScenMenuBtn[i].Type == oldMenu)
				{
					NKCUtil.SetGameobjectActive(this.m_BaseScenMenuBtn[i].obj, false);
					break;
				}
			}
			this.OpenSubMenu(idx);
			yield break;
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x001EA660 File Offset: 0x001E8860
		private void SwitchDecoLine(bool bIn)
		{
			int num;
			int num2;
			if (bIn)
			{
				num = 0;
				num2 = 1;
			}
			else
			{
				num = 1;
				num2 = 0;
			}
			if (null != this.m_ImgMenuTitleDecoTop)
			{
				this.m_ImgMenuTitleDecoTop.fillAmount = (float)num;
				this.m_ImgMenuTitleDecoTop.DOKill(false);
				this.m_ImgMenuTitleDecoTop.DOFillAmount((float)num2, 0.3f).SetEase(Ease.OutCubic);
			}
			if (null != this.m_ImgMenuTitleDecoDown)
			{
				this.m_ImgMenuTitleDecoDown.fillAmount = (float)num;
				this.m_ImgMenuTitleDecoDown.DOKill(false);
				this.m_ImgMenuTitleDecoDown.DOFillAmount((float)num2, 0.3f).SetEase(Ease.OutCubic);
			}
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x001EA700 File Offset: 0x001E8900
		private void CloseAllSubMenu()
		{
			for (int i = 1; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_BaseScenMenuBtn[i].obj, false);
			}
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x001EA734 File Offset: 0x001E8934
		private void DisableMenuBtn(NKCUIBaseSceneMenu.BaseSceneMenuType closeType)
		{
			for (int i = 1; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				BaseSceneMenuBtn baseSceneMenuBtn = this.m_BaseScenMenuBtn[i];
				if (baseSceneMenuBtn.Type == closeType)
				{
					NKCUtil.SetGameobjectActive(baseSceneMenuBtn.obj, false);
					return;
				}
			}
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x001EA774 File Offset: 0x001E8974
		private void AddNPC(NKCUIBaseSceneMenu.BaseSceneMenuType newType)
		{
			bool flag = NKCGameEventManager.IsEventPlaying() || this.TutorialCheck(false);
			switch (newType)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Base:
				if (this.m_UINPCBase_KimHaNa == null)
				{
					this.m_UINPCBase_KimHaNa = this.m_NPC_BASE_TouchArea.GetComponent<NKCUINPCManagerKimHaNa>();
					this.m_UINPCBase_KimHaNa.Init(true);
				}
				this.m_UINPCBase_KimHaNa.PlayAni(NPC_ACTION_TYPE.ENTER_BASE, this.m_IsShortCutOpen || flag);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				if (this.m_UINPC_Lab_Assistant == null)
				{
					this.m_UINPC_Lab_Assistant = this.m_objNPCLab_Assistant_TouchArea.GetComponent<NKCUINPCAssistantLeeYoonJung>();
					this.m_UINPC_Lab_Assistant.Init(true);
				}
				this.m_UINPC_Lab_Assistant.PlayAni(NPC_ACTION_TYPE.START, this.m_IsShortCutOpen || flag);
				if (this.m_UINPC_Lab_Professor == null)
				{
					this.m_UINPC_Lab_Professor = this.m_objNPCLab_Professor_TouchArea.GetComponent<NKCUINPCProfessorOlivia>();
					this.m_UINPC_Lab_Professor.Init(true);
				}
				this.m_UINPC_Lab_Professor.PlayAni(NPC_ACTION_TYPE.START, this.m_IsShortCutOpen || flag);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				if (this.m_UINPC_Factory == null)
				{
					this.m_UINPC_Factory = this.m_objNPCFactory_TouchArea.GetComponent<NKCUINPCFactoryAnastasia>();
					this.m_UINPC_Factory.Init(true);
				}
				this.m_UINPC_Factory.PlayAni(NPC_ACTION_TYPE.START, this.m_IsShortCutOpen || flag);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				if (this.m_NPCHangar == null)
				{
					this.m_NPCHangar = this.m_NPCHanger_TouchArea.GetComponent<NKCUINPCHangarNaHeeRin>();
					this.m_NPCHangar.Init(true);
				}
				this.m_NPCHangar.PlayAni(NPC_ACTION_TYPE.START, this.m_IsShortCutOpen || flag);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				if (this.m_npcPersonnel == null)
				{
					this.m_npcPersonnel = this.m_NPCPersonnel_TouchArea.GetComponent<NKCUINPCMachineGap>();
					this.m_npcPersonnel.Init(true);
				}
				this.m_npcPersonnel.PlayAni(NPC_ACTION_TYPE.START, this.m_IsShortCutOpen || flag);
				return;
			default:
				return;
			}
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x001EA930 File Offset: 0x001E8B30
		private void RemoveNPC(NKCUIBaseSceneMenu.BaseSceneMenuType newType)
		{
			switch (newType)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Base:
				if (this.m_UINPCBase_KimHaNa != null)
				{
					this.m_UINPCBase_KimHaNa = null;
					return;
				}
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				if (this.m_UINPC_Lab_Assistant != null)
				{
					this.m_UINPC_Lab_Assistant = null;
				}
				if (this.m_UINPC_Lab_Professor != null)
				{
					this.m_UINPC_Lab_Professor = null;
					return;
				}
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				if (this.m_UINPC_Factory != null)
				{
					this.m_UINPC_Factory = null;
					return;
				}
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				if (this.m_NPCHangar != null)
				{
					this.m_NPCHangar = null;
				}
				break;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				if (this.m_npcPersonnel != null)
				{
					this.m_npcPersonnel = null;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x001EA9DC File Offset: 0x001E8BDC
		private void SetEventObject()
		{
			NKCUtil.SetGameobjectActive(this.m_objFactoryEvent, NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT) || NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT));
			NKCUtil.SetGameobjectActive(this.m_objPersonnalEvent, NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT));
			for (int i = 1; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				BaseSceneMenuBtn baseSceneMenuBtn = this.m_BaseScenMenuBtn[i];
				for (int j = 0; j < baseSceneMenuBtn.subBtn.Length; j++)
				{
					NKCUtil.SetGameobjectActive(baseSceneMenuBtn.subBtn[j].m_objEvent, this.IsEventEnabled(baseSceneMenuBtn.subBtn[j].Type));
				}
			}
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x001EAA8C File Offset: 0x001E8C8C
		private void SetUnlockContents()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_LAB, 0, 0))
			{
				this.m_LabToggleBtn.Lock(false);
			}
			else
			{
				this.m_LabToggleBtn.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				this.m_FactoryToggleBtn.Lock(false);
			}
			else
			{
				this.m_FactoryToggleBtn.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_HANGAR, 0, 0))
			{
				this.m_HangarToggleBtn.Lock(false);
			}
			else
			{
				this.m_HangarToggleBtn.UnLock(false);
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
			{
				this.m_PersonnelToggleBtn.Lock(false);
			}
			else
			{
				this.m_PersonnelToggleBtn.UnLock(false);
			}
			for (int i = 1; i < this.m_BaseScenMenuBtn.Length; i++)
			{
				if (this.m_BaseScenMenuBtn[i] != null)
				{
					for (int j = 0; j < this.m_BaseScenMenuBtn[i].subBtn.Length; j++)
					{
						if (this.m_BaseScenMenuBtn[i].subBtn[j] != null && !(this.m_BaseScenMenuBtn[i].subBtn[j].Btn == null))
						{
							ContentsType contentsType = ContentsType.None;
							switch (this.m_BaseScenMenuBtn[i].subBtn[j].Type)
							{
							case NKC_SCEN_BASE.eUIOpenReserve.LAB_Train:
								contentsType = ContentsType.LAB_TRAINING;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.LAB_Enchant:
								contentsType = ContentsType.BASE_LAB;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.LAB_Transcendence:
								contentsType = ContentsType.LAB_LIMITBREAK;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.Factory_Craft:
								contentsType = ContentsType.FACTORY_CRAFT;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.Factory_Enchant:
								contentsType = ContentsType.FACTORY_ENCHANT;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.Factory_Tunning:
								contentsType = ContentsType.FACTORY_TUNING;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.Hangar_Build:
								contentsType = ContentsType.HANGER_SHIPBUILD;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.Hangar_Shipyard:
								contentsType = ContentsType.HANGER_SHIPYARD;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime:
								contentsType = ContentsType.PERSONNAL_LIFETIME;
								break;
							case NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout:
								contentsType = ContentsType.PERSONNAL_SCOUT;
								break;
							}
							if (!NKCContentManager.IsContentsUnlocked(contentsType, 0, 0))
							{
								this.m_BaseScenMenuBtn[i].subBtn[j].Btn.Lock(false);
								NKCUtil.SetGameobjectActive(this.m_BaseScenMenuBtn[i].subBtn[j].Btn, false);
								NKCUtil.SetGameobjectActive(this.m_BaseScenMenuBtn[i].subBtn[j].LockedBtn, true);
							}
							else
							{
								this.m_BaseScenMenuBtn[i].subBtn[j].Btn.UnLock(false);
								NKCUtil.SetGameobjectActive(this.m_BaseScenMenuBtn[i].subBtn[j].Btn, true);
								NKCUtil.SetGameobjectActive(this.m_BaseScenMenuBtn[i].subBtn[j].LockedBtn, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x001EACEC File Offset: 0x001E8EEC
		private bool IsContentsUnlocked(NKCUIBaseSceneMenu.BaseSceneMenuType menuType)
		{
			switch (menuType)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				return NKCContentManager.IsContentsUnlocked(ContentsType.BASE_LAB, 0, 0);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				return NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				return NKCContentManager.IsContentsUnlocked(ContentsType.BASE_HANGAR, 0, 0);
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				return NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0);
			default:
				Debug.LogWarning("IsContentsUnlocked - Undefined BaseSceneMenu Type : " + menuType.ToString());
				return true;
			}
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x001EAD58 File Offset: 0x001E8F58
		private void ShowLockedMessage(NKCUIBaseSceneMenu.BaseSceneMenuType menuType)
		{
			switch (menuType)
			{
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Lab:
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_LAB, 0);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Factory:
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Hangar:
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_HANGAR, 0);
				return;
			case NKCUIBaseSceneMenu.BaseSceneMenuType.Personnel:
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_PERSONNAL, 0);
				return;
			default:
				Debug.LogWarning("ShowLockedMessage - Undefined BaseSceneMenu Type : " + menuType.ToString());
				return;
			}
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x001EADC4 File Offset: 0x001E8FC4
		private void NotifyCheck()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKCUIBaseSceneMenu.HeadquartersWorkState headquartersWorkState = this.CheckNewUnlockShipBuild();
			NKCUtil.SetGameobjectActive(this.m_objHangarRedDot, headquartersWorkState == NKCUIBaseSceneMenu.HeadquartersWorkState.Complete);
			NKCUtil.SetGameobjectActive(this.m_objHangarBuildRedDot, headquartersWorkState == NKCUIBaseSceneMenu.HeadquartersWorkState.Complete);
			NKCUIBaseSceneMenu.HeadquartersWorkState headquartersWorkState2 = this.CheckEquipCreationState(nkmuserData.m_CraftData);
			NKCUtil.SetGameobjectActive(this.m_objFactoryRedDot, headquartersWorkState2 == NKCUIBaseSceneMenu.HeadquartersWorkState.Complete);
			NKCUtil.SetGameobjectActive(this.m_objFactoryCraftRedDot, headquartersWorkState2 == NKCUIBaseSceneMenu.HeadquartersWorkState.Complete);
			bool bValue = NKCAlarmManager.CheckScoutNotify(nkmuserData);
			NKCUtil.SetGameobjectActive(this.m_objPersonnelRedDot, bValue);
			NKCUtil.SetGameobjectActive(this.m_objScoutRedDot, bValue);
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x001EAE4C File Offset: 0x001E904C
		private NKCUIBaseSceneMenu.HeadquartersWorkState CheckNewUnlockShipBuild()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPBUILD, 0, 0))
			{
				return NKCUIBaseSceneMenu.HeadquartersWorkState.Idle;
			}
			NKCUIBaseSceneMenu.HeadquartersWorkState result = NKCUIBaseSceneMenu.HeadquartersWorkState.Idle;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				foreach (NKMShipBuildTemplet nkmshipBuildTemplet in NKMTempletContainer<NKMShipBuildTemplet>.Values)
				{
					if (nkmshipBuildTemplet.ShipBuildUnlockType != NKMShipBuildTemplet.BuildUnlockType.BUT_UNABLE && NKMShipManager.CanUnlockShip(nkmuserData, nkmshipBuildTemplet) && !PlayerPrefs.HasKey(string.Format("{0}_{1}_{2}", "SHIP_BUILD_SLOT_CHECK", nkmuserData.m_UserUID, nkmshipBuildTemplet.ShipID)))
					{
						result = NKCUIBaseSceneMenu.HeadquartersWorkState.Complete;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x001EAEEC File Offset: 0x001E90EC
		private NKCUIBaseSceneMenu.HeadquartersWorkState CheckEquipCreationState(NKMCraftData creationData)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				return NKCUIBaseSceneMenu.HeadquartersWorkState.Idle;
			}
			NKCUIBaseSceneMenu.HeadquartersWorkState result = NKCUIBaseSceneMenu.HeadquartersWorkState.Idle;
			foreach (KeyValuePair<byte, NKMCraftSlotData> keyValuePair in creationData.SlotList)
			{
				if (keyValuePair.Value.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED)
				{
					result = NKCUIBaseSceneMenu.HeadquartersWorkState.Complete;
					return result;
				}
				if (keyValuePair.Value.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW)
				{
					result = NKCUIBaseSceneMenu.HeadquartersWorkState.Working;
				}
			}
			return result;
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x001EAF8C File Offset: 0x001E918C
		private bool TutorialCheck(bool play = true)
		{
			return NKCTutorialManager.TutorialRequired(TutorialPoint.Base, play) > TutorialStep.None;
		}

		// Token: 0x04004DC3 RID: 19907
		public const string ASSET_BUNDLE_NAME = "ab_ui_nuf_base";

		// Token: 0x04004DC4 RID: 19908
		public const string UI_ASSET_NAME = "NKM_UI_BASE";

		// Token: 0x04004DC5 RID: 19909
		public RectTransform m_NUM_BASE_BG;

		// Token: 0x04004DC6 RID: 19910
		[Header("본부 메뉴 토글 버튼 설정")]
		public NKCUIComToggle m_LabToggleBtn;

		// Token: 0x04004DC7 RID: 19911
		public NKCUIComToggle m_FactoryToggleBtn;

		// Token: 0x04004DC8 RID: 19912
		public NKCUIComToggle m_HangarToggleBtn;

		// Token: 0x04004DC9 RID: 19913
		public NKCUIComToggle m_PersonnelToggleBtn;

		// Token: 0x04004DCA RID: 19914
		public NKCUIComToggleGroup m_MenuBtnToggleGroup;

		// Token: 0x04004DCB RID: 19915
		[Header("버튼 서브 메뉴 설정(주의Base는 0 인덱스에 설정)")]
		public BaseSceneMenuBtn[] m_BaseScenMenuBtn;

		// Token: 0x04004DCC RID: 19916
		public GameObject m_objFactoryEvent;

		// Token: 0x04004DCD RID: 19917
		public GameObject m_objPersonnalEvent;

		// Token: 0x04004DCE RID: 19918
		[Header("기본 메뉴(우측) 배경")]
		[Header("MidCanvas - SpriteRenderer")]
		public SpriteRenderer m_srScenBG;

		// Token: 0x04004DCF RID: 19919
		[Header("좌측 하단 오브젝트")]
		public GameObject m_NKM_UI_BASE_LEFT;

		// Token: 0x04004DD0 RID: 19920
		public Animator m_Ani_LeftMenu;

		// Token: 0x04004DD1 RID: 19921
		[Header("좌측 이미지")]
		public Image m_ImgMenuTitleDecoTop;

		// Token: 0x04004DD2 RID: 19922
		public Image m_ImgMenuTitleDecoDown;

		// Token: 0x04004DD3 RID: 19923
		[Header("좌측 하단 텍스트")]
		public Text BaseTitle;

		// Token: 0x04004DD4 RID: 19924
		public Text BaseTitleEng;

		// Token: 0x04004DD5 RID: 19925
		public Text SubTitle;

		// Token: 0x04004DD6 RID: 19926
		private Text m_txtNKM_UI_BASE_LEFT_detail;

		// Token: 0x04004DD7 RID: 19927
		private RectTransform m_rtLeftDownMenu;

		// Token: 0x04004DD8 RID: 19928
		[Header("Spine NPC")]
		[Header("Base")]
		public GameObject m_objNPC_Base;

		// Token: 0x04004DD9 RID: 19929
		public GameObject m_NPC_BASE_TouchArea;

		// Token: 0x04004DDA RID: 19930
		private NKCUINPCManagerKimHaNa m_UINPCBase_KimHaNa;

		// Token: 0x04004DDB RID: 19931
		[Header("LAB")]
		private NKCUINPCAssistantLeeYoonJung m_UINPC_Lab_Assistant;

		// Token: 0x04004DDC RID: 19932
		public GameObject m_objNPCLab_Assistant;

		// Token: 0x04004DDD RID: 19933
		public GameObject m_objNPCLab_Assistant_TouchArea;

		// Token: 0x04004DDE RID: 19934
		private NKCUINPCProfessorOlivia m_UINPC_Lab_Professor;

		// Token: 0x04004DDF RID: 19935
		public GameObject m_objNPCLab_Professor;

		// Token: 0x04004DE0 RID: 19936
		public GameObject m_objNPCLab_Professor_TouchArea;

		// Token: 0x04004DE1 RID: 19937
		[Header("Factory")]
		private NKCUINPCFactoryAnastasia m_UINPC_Factory;

		// Token: 0x04004DE2 RID: 19938
		public GameObject m_objNPCFactory;

		// Token: 0x04004DE3 RID: 19939
		public GameObject m_objNPCFactory_TouchArea;

		// Token: 0x04004DE4 RID: 19940
		[Header("Hanger")]
		private NKCUINPCHangarNaHeeRin m_NPCHangar;

		// Token: 0x04004DE5 RID: 19941
		public GameObject m_objNPCHangar;

		// Token: 0x04004DE6 RID: 19942
		public GameObject m_NPCHanger_TouchArea;

		// Token: 0x04004DE7 RID: 19943
		[Header("HR")]
		private NKCUINPCMachineGap m_npcPersonnel;

		// Token: 0x04004DE8 RID: 19944
		public GameObject m_objNPCPersonnel;

		// Token: 0x04004DE9 RID: 19945
		public GameObject m_NPCPersonnel_TouchArea;

		// Token: 0x04004DEA RID: 19946
		public RectTransform m_NKM_UI_BASE_NPC;

		// Token: 0x04004DEB RID: 19947
		private const int SUB_MENU_START = 1;

		// Token: 0x04004DEC RID: 19948
		private const int SUB_MENU_LAB = 1;

		// Token: 0x04004DED RID: 19949
		private const int SUB_MENU_FACTORY = 2;

		// Token: 0x04004DEE RID: 19950
		private const int SUB_MENU_HANGAR = 3;

		// Token: 0x04004DEF RID: 19951
		private const int SUB_MENU_PERSONNEL = 4;

		// Token: 0x04004DF0 RID: 19952
		private Image m_NKM_UI_BASE_NPC_BG_GLOW_Img;

		// Token: 0x04004DF1 RID: 19953
		private Color m_NKM_UI_BASE_NPC_BG_GLOW_Img_OriginColor;

		// Token: 0x04004DF2 RID: 19954
		private NKCUIBaseSceneMenu.BaseSceneMenuType m_CurMenu;

		// Token: 0x04004DF3 RID: 19955
		private NKCUIBaseSceneMenu.BaseSceneMenuType m_OldMenu;

		// Token: 0x04004DF4 RID: 19956
		private bool bTransition;

		// Token: 0x04004DF5 RID: 19957
		private bool m_IsShortCutOpen;

		// Token: 0x04004DF6 RID: 19958
		private UnityAction<NKC_SCEN_BASE.eUIOpenReserve> m_ReceiveUILoad;

		// Token: 0x04004DF7 RID: 19959
		private UnityAction m_CallUI;

		// Token: 0x04004DF8 RID: 19960
		private NKCUIBaseSceneMenu.MenuAniSetting BaseMenuAnimSetting;

		// Token: 0x04004DF9 RID: 19961
		private const string ANI_INTRO = "Intro";

		// Token: 0x04004DFA RID: 19962
		private const string ANI_OUTRO = "Outro";

		// Token: 0x04004DFB RID: 19963
		private const float MENU_CHANGE_ANIMATE_TIME = 0.3f;

		// Token: 0x04004DFC RID: 19964
		[Header("알람 표시")]
		public GameObject m_objFactoryRedDot;

		// Token: 0x04004DFD RID: 19965
		public GameObject m_objFactoryCraftRedDot;

		// Token: 0x04004DFE RID: 19966
		public GameObject m_objHangarRedDot;

		// Token: 0x04004DFF RID: 19967
		public GameObject m_objHangarBuildRedDot;

		// Token: 0x04004E00 RID: 19968
		public GameObject m_objPersonnelRedDot;

		// Token: 0x04004E01 RID: 19969
		public GameObject m_objScoutRedDot;

		// Token: 0x02001614 RID: 5652
		public enum BaseSceneMenuType
		{
			// Token: 0x0400A31D RID: 41757
			None = -1,
			// Token: 0x0400A31E RID: 41758
			Base,
			// Token: 0x0400A31F RID: 41759
			Lab,
			// Token: 0x0400A320 RID: 41760
			Factory,
			// Token: 0x0400A321 RID: 41761
			Hangar,
			// Token: 0x0400A322 RID: 41762
			Personnel
		}

		// Token: 0x02001615 RID: 5653
		private struct MenuAniSetting
		{
			// Token: 0x0400A323 RID: 41763
			public const float BASE_BTN_POS_X = 0f;

			// Token: 0x0400A324 RID: 41764
			public const float BASE_BTN_HIDE_POS_X = 320f;

			// Token: 0x0400A325 RID: 41765
			public const float ANIMATION_DELAY_TIME = 0.3f;

			// Token: 0x0400A326 RID: 41766
			public const float ANIMATION_DELAY_HALF_TIME = 0.15f;

			// Token: 0x0400A327 RID: 41767
			public const float BUTTON_DELAY_GAP = 0.05f;

			// Token: 0x0400A328 RID: 41768
			public const float SUB_BUTTON_HIDE_TIME = 0.075f;
		}

		// Token: 0x02001616 RID: 5654
		private enum HeadquartersWorkState
		{
			// Token: 0x0400A32A RID: 41770
			Idle = -1,
			// Token: 0x0400A32B RID: 41771
			Working,
			// Token: 0x0400A32C RID: 41772
			Complete
		}
	}
}

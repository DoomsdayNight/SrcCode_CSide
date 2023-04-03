using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200097F RID: 2431
	public class NKCUICutsceneSimViewer : NKCUIBase
	{
		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060062F4 RID: 25332 RVA: 0x001F2013 File Offset: 0x001F0213
		public override string MenuName
		{
			get
			{
				return "컷신 시뮬레이터";
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x001F201A File Offset: 0x001F021A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060062F6 RID: 25334 RVA: 0x001F201D File Offset: 0x001F021D
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x001F2020 File Offset: 0x001F0220
		public override void CloseInternal()
		{
			if (this.m_NKCUICutScenPlayer)
			{
				this.m_NKCUICutScenPlayer.UnLoad();
				this.m_NKCUICutScenPlayer.StopWithCallBack();
			}
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x001F2045 File Offset: 0x001F0245
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x001F2053 File Offset: 0x001F0253
		public void Open()
		{
			this.m_NKCUICutScenPlayer = NKCUICutScenPlayer.Instance;
			base.UIOpened(true);
			base.gameObject.SetActive(true);
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x001F2073 File Offset: 0x001F0273
		private void SetCutScenList()
		{
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x001F2075 File Offset: 0x001F0275
		private void SetStrIDFilterCallBack()
		{
			if (this.m_strIdFilter != null)
			{
				this.m_strIdFilter.onEndEdit.RemoveAllListeners();
				this.m_strIdFilter.onEndEdit.AddListener(delegate(string _)
				{
					this.SetCutScenList();
				});
			}
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x001F20B4 File Offset: 0x001F02B4
		public static NKCUICutsceneSimViewer InitUI()
		{
			NKCUICutsceneSimViewer nkcuicutsceneSimViewer = NKCUIManager.OpenUI<NKCUICutsceneSimViewer>("NKM_CUTSCEN_SIM_Panel");
			if (nkcuicutsceneSimViewer != null)
			{
				nkcuicutsceneSimViewer.gameObject.SetActive(false);
				nkcuicutsceneSimViewer.m_tglFilterMain.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterMain.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_tglFilterSide.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterSide.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_tglFilterCounterCase.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterCounterCase.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_tglFilterEndLifeTimeContract.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterEndLifeTimeContract.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_tglFilterFreeContract.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterFreeContract.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_tglFilterFirstCome.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterFirstCome.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_tglFilterEvent.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterEvent.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_tglFilterEtc.onValueChanged.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_tglFilterEtc.onValueChanged.AddListener(new UnityAction<bool>(nkcuicutsceneSimViewer.OnValueChangedFilter));
				nkcuicutsceneSimViewer.m_comBtnVaildateListString.PointerClick.RemoveAllListeners();
				nkcuicutsceneSimViewer.m_comBtnVaildateListString.PointerClick.AddListener(new UnityAction(nkcuicutsceneSimViewer.OnClickedValidateBtn));
				foreach (object obj in Enum.GetValues(typeof(NKM_NATIONAL_CODE)))
				{
					NKM_NATIONAL_CODE nkm_NATIONAL_CODE = (NKM_NATIONAL_CODE)obj;
					if (!string.IsNullOrEmpty(NKCStringTable.GetNationalPostfix(nkm_NATIONAL_CODE)))
					{
						string nationalPostfix = NKCStringTable.GetNationalPostfix(nkm_NATIONAL_CODE);
						if (nationalPostfix != null)
						{
							uint num = <PrivateImplementationDetails>.ComputeStringHash(nationalPostfix);
							if (num <= 1313743886U)
							{
								if (num != 244747238U)
								{
									if (num != 496509257U)
									{
										if (num != 1313743886U)
										{
											continue;
										}
										if (!(nationalPostfix == "_VTN"))
										{
											continue;
										}
									}
									else if (!(nationalPostfix == "_THA"))
									{
										continue;
									}
								}
								else if (!(nationalPostfix == "_SCN"))
								{
									continue;
								}
							}
							else if (num <= 3426629774U)
							{
								if (num != 2944881438U)
								{
									if (num != 3426629774U)
									{
										continue;
									}
									if (!(nationalPostfix == "_JPN"))
									{
										continue;
									}
								}
								else if (!(nationalPostfix == "_ENG"))
								{
									continue;
								}
							}
							else if (num != 3905367055U)
							{
								if (num != 4078411848U)
								{
									continue;
								}
								if (!(nationalPostfix == "_KOREA"))
								{
									continue;
								}
							}
							else if (!(nationalPostfix == "_TWN"))
							{
								continue;
							}
							nkcuicutsceneSimViewer.m_dicNationalCode[nationalPostfix] = nkm_NATIONAL_CODE;
						}
					}
				}
				List<string> list = nkcuicutsceneSimViewer.m_dicNationalCode.Keys.ToList<string>();
				list.Insert(0, "_KOREA");
				nkcuicutsceneSimViewer.m_ddLanguageList.AddOptions(list);
				nkcuicutsceneSimViewer.SetStrIDFilterCallBack();
			}
			else
			{
				UnityEngine.Debug.LogError("NKM_CUTSCEN_SIM_Panel is null");
			}
			return nkcuicutsceneSimViewer;
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x001F2418 File Offset: 0x001F0618
		public void OnClickedValidateBtn()
		{
			this.ValidateTempletList();
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x001F2420 File Offset: 0x001F0620
		public void OnValueChangedFilter(bool bSet)
		{
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x001F2424 File Offset: 0x001F0624
		public void OnValueChangedLanguage(int index)
		{
			string text = this.m_ddLanguageList.options[index].text;
			this.m_CurrLanguage = this.m_dicNationalCode[text];
			this.OnClickReload();
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x001F2460 File Offset: 0x001F0660
		public void OnValueChanged(int index)
		{
			if (index == 0)
			{
				if (this.m_NKCUICutScenPlayer)
				{
					this.m_NKCUICutScenPlayer.StopWithCallBack();
				}
				return;
			}
			string text = this.m_ddCutScenList.options[index].text;
			this.PlayCutScen(text);
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x001F24A7 File Offset: 0x001F06A7
		public void OnClickPlay()
		{
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x001F24AC File Offset: 0x001F06AC
		public void OnClickReload()
		{
			if (this.m_NKCUICutScenPlayer.IsPlaying())
			{
				NKCPopupOKCancel.OpenOKBox("에러", "플레이 중에는 리로딩 불가능해요.", null, "");
				return;
			}
			if (NKCStringTable.GetNationalCode() != this.m_CurrLanguage)
			{
				NKCStringTable.SetNationalCode(this.m_CurrLanguage);
			}
			NKCCutScenManager.Init();
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x001F24FC File Offset: 0x001F06FC
		private void PlayCutScen(string strID)
		{
			if (this.m_NKCUICutScenPlayer)
			{
				this.m_NKCUICutScenPlayer.UnLoad();
				this.m_NKCUICutScenPlayer.Load(strID, false);
				this.m_NKCUICutScenPlayer.StopWithCallBack();
				this.m_NKCUICutScenPlayer.Play(strID, 0, delegate()
				{
					this.m_CanvasGroup.alpha = 1f;
					this.m_CanvasGroup.interactable = true;
				});
				this.m_CanvasGroup.alpha = 0f;
				this.m_CanvasGroup.interactable = false;
			}
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x001F2570 File Offset: 0x001F0770
		private void ValidateTempletList()
		{
			this.m_NKCUICutScenPlayer.UnLoad();
			UnityEngine.Debug.Log("Validation Start");
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				for (int i = 1; i < this.m_CurrentTempletList.Count; i++)
				{
					this.ValidateTemplet(this.m_CurrentTempletList[i]);
				}
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.Log(string.Format("Exception occurred. [{0}]", arg));
			}
			finally
			{
				this.m_CanvasGroup.alpha = 1f;
				this.m_CanvasGroup.interactable = true;
				stopwatch.Stop();
				UnityEngine.Debug.Log("Validation End");
				UnityEngine.Debug.Log(string.Format("Validated Templet Count : {0}", this.m_CurrentTempletList.Count));
				UnityEngine.Debug.Log(string.Format("Validation Time : {0} ms", stopwatch.ElapsedMilliseconds));
			}
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x001F2660 File Offset: 0x001F0860
		private void ValidateTemplet(string strID)
		{
			this.m_NKCUICutScenPlayer.Load(strID, false);
			this.m_NKCUICutScenPlayer.Play(strID, 0, null);
			this.m_NKCUICutScenPlayer.ValidateBySimulate();
		}

		// Token: 0x04004EB0 RID: 20144
		public Dropdown m_ddCutScenList;

		// Token: 0x04004EB1 RID: 20145
		public Dropdown m_ddLanguageList;

		// Token: 0x04004EB2 RID: 20146
		public Dictionary<string, NKM_NATIONAL_CODE> m_dicNationalCode = new Dictionary<string, NKM_NATIONAL_CODE>();

		// Token: 0x04004EB3 RID: 20147
		public List<string> m_CurrentTempletList = new List<string>();

		// Token: 0x04004EB4 RID: 20148
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04004EB5 RID: 20149
		public Toggle m_tglFilterMain;

		// Token: 0x04004EB6 RID: 20150
		public Toggle m_tglFilterSide;

		// Token: 0x04004EB7 RID: 20151
		public Toggle m_tglFilterCounterCase;

		// Token: 0x04004EB8 RID: 20152
		public Toggle m_tglFilterEndLifeTimeContract;

		// Token: 0x04004EB9 RID: 20153
		public Toggle m_tglFilterFreeContract;

		// Token: 0x04004EBA RID: 20154
		public Toggle m_tglFilterFirstCome;

		// Token: 0x04004EBB RID: 20155
		public Toggle m_tglFilterEvent;

		// Token: 0x04004EBC RID: 20156
		public Toggle m_tglFilterEtc;

		// Token: 0x04004EBD RID: 20157
		public InputField m_strIdFilter;

		// Token: 0x04004EBE RID: 20158
		public NKCUIComButton m_comBtnVaildateListString;

		// Token: 0x04004EBF RID: 20159
		private NKCUICutScenPlayer m_NKCUICutScenPlayer;

		// Token: 0x04004EC0 RID: 20160
		private NKM_NATIONAL_CODE m_CurrLanguage;
	}
}

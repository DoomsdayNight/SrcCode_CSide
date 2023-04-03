using System;
using Cs.Logging;
using NKC.Localization;
using NKC.Patcher;
using NKC.Publisher;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B4 RID: 2484
	public class NKCUILoginDevMenu : MonoBehaviour
	{
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06006898 RID: 26776 RVA: 0x0021CCCA File Offset: 0x0021AECA
		public NKM_USER_AUTH_LEVEL AuthLevel
		{
			get
			{
				return this.m_NKM_USER_AUTH_LEVEL;
			}
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x0021CCD2 File Offset: 0x0021AED2
		private void OnDestroy()
		{
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x0021CCD4 File Offset: 0x0021AED4
		public void InitUI()
		{
			if (this.m_comBtnServer1STSave != null)
			{
				this.m_comBtnServer1STSave.PointerClick.RemoveAllListeners();
				this.m_comBtnServer1STSave.PointerClick.AddListener(new UnityAction(this.OnSaveServer1ST));
			}
			if (this.m_comBtnServer1STLoad != null)
			{
				this.m_comBtnServer1STLoad.PointerClick.RemoveAllListeners();
				this.m_comBtnServer1STLoad.PointerClick.AddListener(new UnityAction(this.OnLoadServer1ST));
			}
			if (this.m_comBtnServer2NDSave != null)
			{
				this.m_comBtnServer2NDSave.PointerClick.RemoveAllListeners();
				this.m_comBtnServer2NDSave.PointerClick.AddListener(new UnityAction(this.OnSaveServer2ND));
			}
			if (this.m_comBtnServer2NDLoad != null)
			{
				this.m_comBtnServer2NDLoad.PointerClick.RemoveAllListeners();
				this.m_comBtnServer2NDLoad.PointerClick.AddListener(new UnityAction(this.OnLoadServer2ND));
			}
			if (this.m_comBtnServer3RDSave != null)
			{
				this.m_comBtnServer3RDSave.PointerClick.RemoveAllListeners();
				this.m_comBtnServer3RDSave.PointerClick.AddListener(new UnityAction(this.OnSaveServer3RD));
			}
			if (this.m_comBtnServer3RDLoad != null)
			{
				this.m_comBtnServer3RDLoad.PointerClick.RemoveAllListeners();
				this.m_comBtnServer3RDLoad.PointerClick.AddListener(new UnityAction(this.OnLoadServer3RD));
			}
			if (this.m_comBtnLogin != null)
			{
				this.m_comBtnLogin.PointerClick.RemoveAllListeners();
				this.m_comBtnLogin.PointerClick.AddListener(delegate()
				{
					this.OnDevLogin();
					this.m_loginBaseMenu.OnLogin();
				});
			}
			NKCUtil.SetButtonClickDelegate(this.m_comBtnPlaySingle, new UnityAction(this.OnPlaySingle));
			if (this.m_comBtnCutscen != null)
			{
				this.m_comBtnCutscen.PointerClick.RemoveAllListeners();
				this.m_comBtnCutscen.PointerClick.AddListener(new UnityAction(this.OnCutScenSim));
			}
			if (this.m_comBtnVoiceList != null)
			{
				this.m_comBtnVoiceList.PointerClick.RemoveAllListeners();
				this.m_comBtnVoiceList.PointerClick.AddListener(new UnityAction(this.OnVoiceList));
			}
			if (this.m_comBtnLanguageSelect != null)
			{
				this.m_comBtnLanguageSelect.PointerClick.RemoveAllListeners();
				this.m_comBtnLanguageSelect.PointerClick.AddListener(new UnityAction(this.OnLanguageSelect));
			}
			NKCUtil.SetButtonClickDelegate(this.m_comBtnMoveToPatch, new UnityAction(this.OnMoveToPatch));
			NKCUtil.SetButtonClickDelegate(this.m_comBtnPatchSkipTest, new UnityAction(this.OnPatchSkipTest));
			NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, NKCDefineManager.DEFINE_PATCH_SKIP());
			if (this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL != null)
			{
				this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL.PointerClick.RemoveAllListeners();
				this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL.PointerClick.AddListener(new UnityAction(this.OnClickAuthLevelChange));
			}
			string str = string.Format("{0} {1}", NKCUtilString.GetAppVersionText(), NKCConnectionInfo.s_ServerType);
			if (!string.IsNullOrEmpty(NKCUtil.PatchVersion))
			{
				string[] array = NKCUtil.PatchVersion.Split(new char[]
				{
					'_'
				});
				if (array.Length != 0)
				{
					str = str + " A." + array[array.Length - 1];
				}
			}
			if (!string.IsNullOrEmpty(NKCUtil.PatchVersionEA))
			{
				string[] array2 = NKCUtil.PatchVersionEA.Split(new char[]
				{
					'_'
				});
				if (array2.Length != 0)
				{
					str = str + " E." + array2[array2.Length - 1];
				}
			}
			if (this.m_NUF_LOGIN_DEV_SERVER_LIST != null)
			{
				this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN = this.m_NUF_LOGIN_DEV_SERVER_LIST.transform.GetComponentInChildren<Dropdown>();
				if (this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN != null)
				{
					this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN.onValueChanged.AddListener(delegate(int <p0>)
					{
						this.OnServerListDropDown(this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN);
					});
				}
			}
			this.m_NUF_LOGIN_DEV_SERVER_LIST.SetActive(false);
			this.m_NUF_LOGIN_DEV_CUTSCEN.SetActive(false);
			this.m_NUF_LOGIN_DEV_VOICE_LIST.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_comBtnLanguageSelect, false);
			NKCUtil.SetGameobjectActive(this.m_comBtnMoveToPatch, false);
			NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, false);
			if (!NKCDefineManager.DEFINE_SERVICE() || NKCDefineManager.DEFINE_USE_CHEAT())
			{
				this.m_NUF_LOGIN_DEV_SERVER_LIST.SetActive(!NKCDefineManager.DEFINE_SELECT_SERVER());
			}
			if (this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL.gameObject, false);
			}
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x0021D111 File Offset: 0x0021B311
		private void OnClickPcQR()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.QR_Login(new NKCPublisherModule.OnComplete(this.OnCompleteQR_Login));
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x0021D138 File Offset: 0x0021B338
		private void OnCompleteQR_Login(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false);
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x0021D148 File Offset: 0x0021B348
		private void OnClickAuthLevelChange()
		{
			int num = Enum.GetNames(typeof(NKM_USER_AUTH_LEVEL)).Length;
			if (this.m_NKM_USER_AUTH_LEVEL >= (NKM_USER_AUTH_LEVEL)num)
			{
				this.m_NKM_USER_AUTH_LEVEL = NKM_USER_AUTH_LEVEL.NORMAL_USER;
			}
			else
			{
				this.m_NKM_USER_AUTH_LEVEL += 1;
			}
			this.ApplyNowAuthLevelToText();
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x0021D18F File Offset: 0x0021B38F
		private void OnClickNoticeButton()
		{
			NKCPublisherModule.Notice.OpenNotice(null);
		}

		// Token: 0x0600689F RID: 26783 RVA: 0x0021D19C File Offset: 0x0021B39C
		public void Open()
		{
			base.gameObject.SetActive(true);
			this.m_fUpdateTime = 0f;
			this.LoadLastEditServerAddress();
			Log.Debug("[Login] Open PublisherModule[" + NKCPublisherModule.PublisherType.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginDevMenu.cs", 280);
			if (NKCPublisherModule.IsPublisherNoneType())
			{
				if (PlayerPrefs.HasKey("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"))
				{
					this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(false);
					this.m_bShowUpLoginPanel = false;
					Log.Debug(string.Concat(new string[]
					{
						"[Login] HasKey[",
						"NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING".ToString(),
						"] Value[",
						PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"),
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginDevMenu.cs", 288);
				}
				else
				{
					this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(true);
					this.m_bShowUpLoginPanel = true;
					Log.Debug("[Login] NUF_LOGIN_DEV_LOGIN active", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginDevMenu.cs", 295);
					Log.Debug(string.Concat(new string[]
					{
						"[Login] HasKey[",
						"NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING".ToString(),
						"] Value[",
						PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"),
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginDevMenu.cs", 296);
				}
			}
			else
			{
				this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(false);
				this.m_bShowUpLoginPanel = false;
			}
			if (NKCPublisherModule.IsPublisherNoneType())
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_CUTSCEN, true);
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_VOICE_LIST, true);
				NKCUtil.SetGameobjectActive(this.m_comBtnLanguageSelect, true);
				NKCUtil.SetGameobjectActive(this.m_comBtnMoveToPatch, true);
				NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_CUTSCEN, false);
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_VOICE_LIST, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnLanguageSelect, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnMoveToPatch, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, false);
			}
			if (NKCDefineManager.DEFINE_USE_CHEAT())
			{
				NKCUtil.SetGameobjectActive(this.m_comBtnPlaySingle, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_comBtnPlaySingle, false);
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x0021D39A File Offset: 0x0021B59A
		private void ApplyNowAuthLevelToText()
		{
			if (this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL_TEXT != null)
			{
				this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL_TEXT.text = this.m_NKM_USER_AUTH_LEVEL.ToString();
			}
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x0021D3C8 File Offset: 0x0021B5C8
		private void LoadLastEditServerAddress()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED);
				this.m_IFServerAddress.text = @string;
				return;
			}
			this.m_IFServerAddress.text = "192.168.0.201";
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x0021D421 File Offset: 0x0021B621
		public void OnEndEditServerAddress()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068A3 RID: 26787 RVA: 0x0021D43C File Offset: 0x0021B63C
		public void SaveIDPass()
		{
			Log.Debug(string.Format("[Login] SaveIDPass ShowUpLoginPanel[{0}]  ID[{1}]", this.m_bShowUpLoginPanel, this.m_IFID.text), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginDevMenu.cs", 374);
			if (this.m_bShowUpLoginPanel)
			{
				PlayerPrefs.SetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING", this.m_IFID.text);
				PlayerPrefs.SetString("NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING", this.m_IFPassword.text);
			}
		}

		// Token: 0x060068A4 RID: 26788 RVA: 0x0021D4AC File Offset: 0x0021B6AC
		private void OnServerListDropDown(Dropdown change)
		{
			string text = this.m_IFServerAddress.text;
			string text2 = change.captionText.text;
			if (text2 != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 2031899703U)
				{
					if (num <= 789859694U)
					{
						if (num <= 433112685U)
						{
							if (num != 215274041U)
							{
								if (num == 433112685U)
								{
									if (text2 == "JapanQa")
									{
										text = "Build02:5310";
									}
								}
							}
							else if (text2 == "JapanLive")
							{
								text = "Build02:7310";
							}
						}
						else if (num != 773196749U)
						{
							if (num == 789859694U)
							{
								if (text2 == "ChinaStage")
								{
									text = "DataManager:9100";
								}
							}
						}
						else if (text2 == "Stage")
						{
							text = "52.231.15.96";
						}
					}
					else if (num <= 1793414657U)
					{
						if (num != 979277916U)
						{
							if (num == 1793414657U)
							{
								if (text2 == "Review")
								{
									text = "52.141.21.24";
								}
							}
						}
						else if (text2 == "Dev")
						{
							text = "studiobsidedev.com";
						}
					}
					else if (num != 1833094123U)
					{
						if (num == 2031899703U)
						{
							if (text2 == "JapanStage")
							{
								text = "Build02:6310";
							}
						}
					}
					else if (text2 == "Vietnam")
					{
						text = "192.168.0.145";
					}
				}
				else if (num <= 3288986427U)
				{
					if (num <= 3067769471U)
					{
						if (num != 2043358034U)
						{
							if (num == 3067769471U)
							{
								if (text2 == "devChina")
								{
									text = "DataManager:8100";
								}
							}
						}
						else if (text2 == "SeaQa")
						{
							text = "DataManager:42000";
						}
					}
					else if (num != 3154005398U)
					{
						if (num == 3288986427U)
						{
							if (text2 == "TaiwanQa")
							{
								text = "build02";
							}
						}
					}
					else if (text2 == "ChinaQa")
					{
						text = "DataManager:7100";
					}
				}
				else if (num <= 3806450730U)
				{
					if (num != 3705854472U)
					{
						if (num == 3806450730U)
						{
							if (text2 == "SeaStage")
							{
								text = "DataManager:52000";
							}
						}
					}
					else if (text2 == "Next")
					{
						text = "Build02:32000";
					}
				}
				else if (num != 3918095846U)
				{
					if (num == 3946807265U)
					{
						if (text2 == "TaiwanStage")
						{
							text = "DataManager";
						}
					}
				}
				else if (text2 == "SeaLive")
				{
					text = "DataManager:32000";
				}
			}
			this.m_IFServerAddress.text = text;
		}

		// Token: 0x060068A5 RID: 26789 RVA: 0x0021D7BC File Offset: 0x0021B9BC
		private void LoadConnectionAddressFromUI()
		{
			Log.Debug("[Login] LoadConnectionAddressFromUI m_IFServerAddress[" + this.m_IFServerAddress.text + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginDevMenu.cs", 476);
			string text = this.m_IFServerAddress.text;
			int num = 22000;
			string[] array = this.m_IFServerAddress.text.Split(new char[]
			{
				':'
			});
			text = array[0];
			if (array.Length > 1)
			{
				num = Convert.ToInt32(array[1]);
			}
			NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, text, num, null);
			Log.Debug(string.Format("[Login] NKCConnectionInfo s_ServiceIP[{0}] s_ServicePort[{1}]", text, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginDevMenu.cs", 489);
		}

		// Token: 0x060068A6 RID: 26790 RVA: 0x0021D85C File Offset: 0x0021BA5C
		public string GetCurrentServerAddress()
		{
			return NKCConnectionInfo.ServiceIP;
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x0021D863 File Offset: 0x0021BA63
		public int GetCurrentServerPort()
		{
			return NKCConnectionInfo.ServicePort;
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x0021D86A File Offset: 0x0021BA6A
		private void InitConnect()
		{
			NKCConnectLogin connectLogin = NKCScenManager.GetScenManager().GetConnectLogin();
			connectLogin.SetRemoteAddress(this.GetCurrentServerAddress(), this.GetCurrentServerPort());
			connectLogin.ResetConnection();
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x0021D88D File Offset: 0x0021BA8D
		public bool ProceedLogin()
		{
			return !this.m_bShowUpLoginPanel && (!(this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN != null) || !(this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN.transform.Find("Dropdown List") != null));
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x0021D8C7 File Offset: 0x0021BAC7
		public void OnDevLogin()
		{
			if (this.m_bShowUpLoginPanel)
			{
				this.SaveIDPass();
			}
			if (this.m_NUF_LOGIN_DEV_SERVER_LIST != null && this.m_NUF_LOGIN_DEV_SERVER_LIST.activeInHierarchy)
			{
				this.LoadConnectionAddressFromUI();
			}
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x0021D8F8 File Offset: 0x0021BAF8
		private void OnPlaySingle()
		{
			if (NKCDefineManager.DEFINE_USE_CHEAT())
			{
				NKCPacketSender.Send_NKMPacket_DEV_GAME_LOAD_REQ("NKM_DUNGEON_TEST");
			}
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x0021D90B File Offset: 0x0021BB0B
		public void OnCutScenSim()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_SIM, true);
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x0021D919 File Offset: 0x0021BB19
		public void OnVoiceList()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_VOICE_LIST, true);
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x0021D928 File Offset: 0x0021BB28
		public void OnLanguageSelect()
		{
			NKCUIPopupLanguageSelect.Instance.Open(NKCLocalization.GetSelectLanguageSet(), new NKCUIPopupLanguageSelect.OnClose(this.OnChangeLanguage));
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x0021D945 File Offset: 0x0021BB45
		private void OnMoveToPatch()
		{
			NKCScenManager.GetScenManager().ShowBundleUpdate(false);
		}

		// Token: 0x060068B0 RID: 26800 RVA: 0x0021D952 File Offset: 0x0021BB52
		private void OnPatchSkipTest()
		{
			NKCPatchUtility.DeleteTutorialClearedStatus();
			NKCPatchUtility.ReservePatchSkipTest();
			Application.Quit();
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x0021D964 File Offset: 0x0021BB64
		public void OnChangeAccountDev()
		{
			this.m_bShowUpLoginPanel = true;
			if (!this.m_objNUF_LOGIN_DEV_LOGIN.activeSelf)
			{
				this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(true);
			}
			if (PlayerPrefs.HasKey("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"))
			{
				string @string = PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING");
				string string2 = PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING");
				this.m_IFID.text = @string;
				this.m_IFPassword.text = string2;
			}
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x0021D9CC File Offset: 0x0021BBCC
		private void OnChangeLanguage(NKM_NATIONAL_CODE eNKM_NATIONAL_CODE)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (eNKM_NATIONAL_CODE != NKM_NATIONAL_CODE.NNC_END)
			{
				gameOptionData.NKM_NATIONAL_CODE = eNKM_NATIONAL_CODE;
				NKCGameOptionData.SaveOnlyLang(eNKM_NATIONAL_CODE);
				Application.Quit();
			}
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x0021D9FF File Offset: 0x0021BBFF
		private void OnSaveServer1ST()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST, this.m_IFServerAddress.text);
		}

		// Token: 0x060068B4 RID: 26804 RVA: 0x0021DA18 File Offset: 0x0021BC18
		private void OnLoadServer1ST()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST);
				this.m_IFServerAddress.text = @string;
			}
			else
			{
				this.m_IFServerAddress.text = "192.168.0.201";
			}
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x0021DA72 File Offset: 0x0021BC72
		private void OnSaveServer2ND()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND, this.m_IFServerAddress.text);
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x0021DA8C File Offset: 0x0021BC8C
		private void OnLoadServer2ND()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND);
				this.m_IFServerAddress.text = @string;
			}
			else
			{
				this.m_IFServerAddress.text = "";
			}
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x0021DAE6 File Offset: 0x0021BCE6
		private void OnSaveServer3RD()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD, this.m_IFServerAddress.text);
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x0021DB00 File Offset: 0x0021BD00
		private void OnLoadServer3RD()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD);
				this.m_IFServerAddress.text = @string;
			}
			else
			{
				this.m_IFServerAddress.text = "";
			}
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x0021DB5C File Offset: 0x0021BD5C
		public void Update()
		{
			this.m_fUpdateTime += Time.deltaTime;
			if (!this.m_objNUF_LOGIN_DEV_LOGIN.activeSelf && this.m_bShowUpLoginPanel && this.m_fUpdateTime > 2.5f)
			{
				this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(true);
			}
		}

		// Token: 0x0400547A RID: 21626
		public NKCUILoginBaseMenu m_loginBaseMenu;

		// Token: 0x0400547B RID: 21627
		public GameObject m_NUF_LOGIN_DEV_SERVER_LIST;

		// Token: 0x0400547C RID: 21628
		public GameObject m_NUF_LOGIN_DEV_LOGIN_SIGN_BUTTON;

		// Token: 0x0400547D RID: 21629
		private Dropdown m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN;

		// Token: 0x0400547E RID: 21630
		public NKCUIComButton m_comBtnServer1STSave;

		// Token: 0x0400547F RID: 21631
		public NKCUIComButton m_comBtnServer1STLoad;

		// Token: 0x04005480 RID: 21632
		public NKCUIComButton m_comBtnServer2NDSave;

		// Token: 0x04005481 RID: 21633
		public NKCUIComButton m_comBtnServer2NDLoad;

		// Token: 0x04005482 RID: 21634
		public NKCUIComButton m_comBtnServer3RDSave;

		// Token: 0x04005483 RID: 21635
		public NKCUIComButton m_comBtnServer3RDLoad;

		// Token: 0x04005484 RID: 21636
		public InputField m_IFServerAddress;

		// Token: 0x04005485 RID: 21637
		public InputField m_IFID;

		// Token: 0x04005486 RID: 21638
		public InputField m_IFPassword;

		// Token: 0x04005487 RID: 21639
		public NKCUIComButton m_comBtnLogin;

		// Token: 0x04005488 RID: 21640
		public NKCUIComButton m_comBtnPlaySingle;

		// Token: 0x04005489 RID: 21641
		public NKCUIComButton m_comBtnCutscen;

		// Token: 0x0400548A RID: 21642
		public NKCUIComButton m_comBtnVoiceList;

		// Token: 0x0400548B RID: 21643
		public NKCUIComButton m_comBtnLanguageSelect;

		// Token: 0x0400548C RID: 21644
		public NKCUIComButton m_comBtnMoveToPatch;

		// Token: 0x0400548D RID: 21645
		public NKCUIComButton m_comBtnPatchSkipTest;

		// Token: 0x0400548E RID: 21646
		public const string NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING = "NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING";

		// Token: 0x0400548F RID: 21647
		public const string NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING = "NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING";

		// Token: 0x04005490 RID: 21648
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED";

		// Token: 0x04005491 RID: 21649
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST";

		// Token: 0x04005492 RID: 21650
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND";

		// Token: 0x04005493 RID: 21651
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD";

		// Token: 0x04005494 RID: 21652
		public GameObject m_objNUF_LOGIN_DEV_LOGIN;

		// Token: 0x04005495 RID: 21653
		[Obsolete]
		public GameObject m_NUF_LOGIN_DEV_SINGLE;

		// Token: 0x04005496 RID: 21654
		public GameObject m_NUF_LOGIN_DEV_CUTSCEN;

		// Token: 0x04005497 RID: 21655
		public GameObject m_NUF_LOGIN_DEV_VOICE_LIST;

		// Token: 0x04005498 RID: 21656
		public NKCUIComStateButton m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL;

		// Token: 0x04005499 RID: 21657
		public Text m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL_TEXT;

		// Token: 0x0400549A RID: 21658
		private NKM_USER_AUTH_LEVEL m_NKM_USER_AUTH_LEVEL = NKM_USER_AUTH_LEVEL.NORMAL_USER;

		// Token: 0x0400549B RID: 21659
		private float m_fUpdateTime;

		// Token: 0x0400549C RID: 21660
		private const float LOGIN_PANEL_SHOW_UP_TIME = 2.5f;

		// Token: 0x0400549D RID: 21661
		private bool m_bShowUpLoginPanel;
	}
}

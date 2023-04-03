using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Account;
using ClientPacket.WorldMap;
using Cs.Logging;
using Cs.Protocol;
using NKC.UI;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.Publisher
{
	// Token: 0x02000865 RID: 2149
	public abstract class NKCPublisherModule : MonoBehaviour
	{
		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06005549 RID: 21833 RVA: 0x0019E94F File Offset: 0x0019CB4F
		protected static NKCPublisherModule Instance
		{
			get
			{
				if (NKCPublisherModule.s_Instance == null)
				{
					NKCPublisherModule.MakeInstance();
				}
				return NKCPublisherModule.s_Instance;
			}
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x0019E96C File Offset: 0x0019CB6C
		public static NKCPublisherModule MakeInstance()
		{
			if (NKCPublisherModule.s_objInstance == null)
			{
				NKCPublisherModule.s_objInstance = new GameObject("NKCPublisherModule");
				UnityEngine.Object.DontDestroyOnLoad(NKCPublisherModule.s_objInstance);
			}
			if (NKCDefineManager.DEFINE_STEAM())
			{
				NKCPublisherModule.s_Instance = NKCPublisherModule.s_objInstance.AddComponent<NKCPMSteamPC>();
			}
			else if (NKCDefineManager.DEFINE_JPPC())
			{
				NKCPublisherModule.s_Instance = NKCPublisherModule.s_objInstance.AddComponent<NKCPMJPPC>();
			}
			else
			{
				NKCPublisherModule.s_Instance = NKCPublisherModule.s_objInstance.AddComponent<NKCPMNone>();
			}
			return NKCPublisherModule.s_Instance;
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x0600554B RID: 21835
		protected abstract NKCPublisherModule.ePublisherType _PublisherType { get; }

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x0600554C RID: 21836 RVA: 0x0019E9E3 File Offset: 0x0019CBE3
		public static NKCPublisherModule.ePublisherType PublisherType
		{
			get
			{
				return NKCPublisherModule.Instance._PublisherType;
			}
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0019E9EF File Offset: 0x0019CBEF
		public static bool IsPublisherNoneType()
		{
			return NKCPublisherModule.Instance._PublisherType == NKCPublisherModule.ePublisherType.None;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0019EA00 File Offset: 0x0019CC00
		public static bool ApplyCultureInfo()
		{
			NKCPublisherModule.ePublisherType publisherType = NKCPublisherModule.PublisherType;
			return publisherType == NKCPublisherModule.ePublisherType.SB_Gamebase || publisherType == NKCPublisherModule.ePublisherType.STEAM;
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0019EA1E File Offset: 0x0019CC1E
		public static bool IsPCBuild()
		{
			return NKCPublisherModule.IsNexonPCBuild() || NKCPublisherModule.IsSteamPC();
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0019EA34 File Offset: 0x0019CC34
		public static bool IsNexonPCBuild()
		{
			NKCPublisherModule.ePublisherType publisherType = NKCPublisherModule.PublisherType;
			return publisherType == NKCPublisherModule.ePublisherType.NexonPC || publisherType == NKCPublisherModule.ePublisherType.JPPC;
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x0019EA52 File Offset: 0x0019CC52
		public static bool IsSteamPC()
		{
			return NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.STEAM;
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x0019EA5F File Offset: 0x0019CC5F
		public static bool ShowGameOptionGradeCheck()
		{
			return NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.NexonPC;
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x0019EA6C File Offset: 0x0019CC6C
		public static bool IsNexonPublished()
		{
			switch (NKCPublisherModule.PublisherType)
			{
			case NKCPublisherModule.ePublisherType.NexonToy:
			case NKCPublisherModule.ePublisherType.NexonPC:
			case NKCPublisherModule.ePublisherType.NexonToyJP:
			case NKCPublisherModule.ePublisherType.JPPC:
				return true;
			}
			return false;
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0019EAA4 File Offset: 0x0019CCA4
		public static bool IsZlongPublished()
		{
			return NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong;
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x0019EAB1 File Offset: 0x0019CCB1
		public static bool IsGamebasePublished()
		{
			return NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.SB_Gamebase;
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06005556 RID: 21846 RVA: 0x0019EABE File Offset: 0x0019CCBE
		// (set) Token: 0x06005557 RID: 21847 RVA: 0x0019EAC5 File Offset: 0x0019CCC5
		public static NKCPublisherModule.ePublisherInitState InitState { get; protected set; }

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06005558 RID: 21848
		protected abstract bool _Busy { get; }

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06005559 RID: 21849 RVA: 0x0019EACD File Offset: 0x0019CCCD
		public static bool Busy
		{
			get
			{
				return NKCPublisherModule.Instance._Busy;
			}
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0019EAD9 File Offset: 0x0019CCD9
		public static bool IsBusy()
		{
			return NKCPublisherModule.Instance._Busy;
		}

		// Token: 0x0600555B RID: 21851
		protected abstract void OnTimeOut();

		// Token: 0x0600555C RID: 21852 RVA: 0x0019EAE8 File Offset: 0x0019CCE8
		public static void InitInstance(NKCPublisherModule.OnComplete onComplete)
		{
			Debug.Log("[NKCPM] InitInstance");
			NKCPublisherModule.Auth = NKCPublisherModule.Instance.MakeAuthInstance();
			NKCPublisherModule.InAppPurchase = NKCPublisherModule.Instance.MakeInappInstance();
			NKCPublisherModule.Statistics = NKCPublisherModule.Instance.MakeStatisticsInstance();
			NKCPublisherModule.Notice = NKCPublisherModule.Instance.MakeNoticeInstance();
			NKCPublisherModule.Push = NKCPublisherModule.Instance.MakePushInstance();
			if (NKCPublisherModule.Push != null)
			{
				NKCPublisherModule.Push.Init();
			}
			NKCPublisherModule.Permission = NKCPublisherModule.Instance.MakePermissionInstance();
			NKCPublisherModule.ServerInfo = NKCPublisherModule.Instance.MakeServerInfoInstance();
			NKCPublisherModule.Localization = NKCPublisherModule.Instance.MakeLocalizationInstance();
			NKCPublisherModule.Marketing = NKCPublisherModule.Instance.MakeMarketingInstance();
			NKCPublisherModule.Instance._Init(onComplete);
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x0019EBA2 File Offset: 0x0019CDA2
		public static void DoAfterLogout()
		{
			if (NKCPublisherModule.InitState != NKCPublisherModule.ePublisherInitState.Initialized)
			{
				return;
			}
			if (!NKCPublisherModule.Auth.Init())
			{
				return;
			}
			NKCPublisherModule.InAppPurchase.Init();
			NKCPublisherModule.Permission.Init();
			NKCPublisherModule.Marketing.Init();
			NKCPublisherModule.Localization.Init();
		}

		// Token: 0x0600555E RID: 21854
		protected abstract void _Init(NKCPublisherModule.OnComplete onComplete);

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x0600555F RID: 21855 RVA: 0x0019EBE2 File Offset: 0x0019CDE2
		// (set) Token: 0x06005560 RID: 21856 RVA: 0x0019EBE9 File Offset: 0x0019CDE9
		public static string LastError { get; protected set; }

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06005561 RID: 21857 RVA: 0x0019EBF1 File Offset: 0x0019CDF1
		// (set) Token: 0x06005562 RID: 21858 RVA: 0x0019EBF8 File Offset: 0x0019CDF8
		public static NKCPublisherModule.NKCPMAuthentication Auth { get; private set; }

		// Token: 0x06005563 RID: 21859
		protected abstract NKCPublisherModule.NKCPMAuthentication MakeAuthInstance();

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06005564 RID: 21860 RVA: 0x0019EC00 File Offset: 0x0019CE00
		// (set) Token: 0x06005565 RID: 21861 RVA: 0x0019EC07 File Offset: 0x0019CE07
		public static NKCPublisherModule.NKCPMInAppPurchase InAppPurchase { get; private set; }

		// Token: 0x06005566 RID: 21862
		protected abstract NKCPublisherModule.NKCPMInAppPurchase MakeInappInstance();

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06005567 RID: 21863 RVA: 0x0019EC0F File Offset: 0x0019CE0F
		// (set) Token: 0x06005568 RID: 21864 RVA: 0x0019EC16 File Offset: 0x0019CE16
		public static NKCPublisherModule.NKCPMStatistics Statistics { get; private set; }

		// Token: 0x06005569 RID: 21865
		protected abstract NKCPublisherModule.NKCPMStatistics MakeStatisticsInstance();

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x0019EC1E File Offset: 0x0019CE1E
		// (set) Token: 0x0600556B RID: 21867 RVA: 0x0019EC25 File Offset: 0x0019CE25
		public static NKCPublisherModule.NKCPMNotice Notice { get; private set; }

		// Token: 0x0600556C RID: 21868
		protected abstract NKCPublisherModule.NKCPMNotice MakeNoticeInstance();

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x0600556D RID: 21869 RVA: 0x0019EC2D File Offset: 0x0019CE2D
		// (set) Token: 0x0600556E RID: 21870 RVA: 0x0019EC34 File Offset: 0x0019CE34
		public static NKCPublisherModule.NKCPMPush Push { get; private set; }

		// Token: 0x0600556F RID: 21871 RVA: 0x0019EC3C File Offset: 0x0019CE3C
		protected virtual NKCPublisherModule.NKCPMPush MakePushInstance()
		{
			return new NKCPMNone.PushNone();
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0019EC43 File Offset: 0x0019CE43
		// (set) Token: 0x06005571 RID: 21873 RVA: 0x0019EC4A File Offset: 0x0019CE4A
		public static NKCPublisherModule.NKCPMPermission Permission { get; private set; }

		// Token: 0x06005572 RID: 21874 RVA: 0x0019EC52 File Offset: 0x0019CE52
		protected virtual NKCPublisherModule.NKCPMPermission MakePermissionInstance()
		{
			return new NKCPMNone.PermissionNone();
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06005573 RID: 21875 RVA: 0x0019EC59 File Offset: 0x0019CE59
		// (set) Token: 0x06005574 RID: 21876 RVA: 0x0019EC60 File Offset: 0x0019CE60
		public static NKCPublisherModule.NKCPMServerInfo ServerInfo { get; private set; }

		// Token: 0x06005575 RID: 21877 RVA: 0x0019EC68 File Offset: 0x0019CE68
		protected virtual NKCPublisherModule.NKCPMServerInfo MakeServerInfoInstance()
		{
			return new NKCPMNone.ServerInfoDefault();
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x0019EC6F File Offset: 0x0019CE6F
		// (set) Token: 0x06005577 RID: 21879 RVA: 0x0019EC76 File Offset: 0x0019CE76
		public static NKCPublisherModule.NKCPMLocalization Localization { get; private set; }

		// Token: 0x06005578 RID: 21880 RVA: 0x0019EC7E File Offset: 0x0019CE7E
		protected virtual NKCPublisherModule.NKCPMLocalization MakeLocalizationInstance()
		{
			return new NKCPMNone.LocalizationNone();
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06005579 RID: 21881 RVA: 0x0019EC85 File Offset: 0x0019CE85
		// (set) Token: 0x0600557A RID: 21882 RVA: 0x0019EC8C File Offset: 0x0019CE8C
		public static NKCPublisherModule.NKCPMMarketing Marketing { get; private set; }

		// Token: 0x0600557B RID: 21883 RVA: 0x0019EC94 File Offset: 0x0019CE94
		protected virtual NKCPublisherModule.NKCPMMarketing MakeMarketingInstance()
		{
			return new NKCPMNone.MarketingNone();
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x0019EC9B File Offset: 0x0019CE9B
		public virtual bool IsReviewServer()
		{
			return false;
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x0019ECA0 File Offset: 0x0019CEA0
		public static bool CheckReviewServerSkipVariant(string assetVariant)
		{
			if (!NKCPublisherModule.Instance.IsReviewServer())
			{
				return false;
			}
			if (assetVariant != null)
			{
				if (assetVariant == "vkor")
				{
					return true;
				}
				if (assetVariant == "vjpn")
				{
					return true;
				}
				if (assetVariant == "vchn")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x0019ECF0 File Offset: 0x0019CEF0
		public static bool CheckError(NKC_PUBLISHER_RESULT_CODE errorCode, string additionalError, bool bCloseWaitBox = true, NKCPopupOKCancel.OnButton onOkButton = null, bool popupMessage = false)
		{
			if (bCloseWaitBox)
			{
				NKMPopUpBox.CloseWaitBox();
			}
			if (errorCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				return true;
			}
			Debug.LogWarning("Publisher Error Code : " + errorCode.ToString());
			if (NKCPublisherModule.PassErrorPopup(errorCode))
			{
				if (onOkButton != null)
				{
					onOkButton();
				}
				return false;
			}
			string errorMessage = NKCPublisherModule.GetErrorMessage(errorCode, additionalError);
			if (popupMessage)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(errorMessage, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			}
			else
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, errorMessage, onOkButton, "");
			}
			return false;
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x0019ED72 File Offset: 0x0019CF72
		public static bool CheckBusy(NKCPublisherModule.OnComplete onComplete)
		{
			if (NKCPublisherModule.Busy)
			{
				Debug.Log("Busy!!");
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_BUSY, null);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x0019ED94 File Offset: 0x0019CF94
		public static string GetErrorMessage(NKC_PUBLISHER_RESULT_CODE errorCode, string additionalError = null)
		{
			string text = null;
			if (NKCStringTable.CheckExistString(errorCode.ToString()))
			{
				string @string = NKCStringTable.GetString(errorCode.ToString(), false);
				if (!string.IsNullOrEmpty(@string))
				{
					text = @string;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = NKCStringTable.GetString("SI_ERROR_DEFAULT_MESSAGE", false);
			}
			if (NKCScenManager.CurrentUserData() != null && NKCScenManager.CurrentUserData().m_eAuthLevel > NKM_USER_AUTH_LEVEL.NORMAL_USER)
			{
				text = string.Format("{0}\n(P{1} {2})", text, (int)errorCode, errorCode);
			}
			else
			{
				text = string.Format("{0}\n(P{1})", text, (int)errorCode);
			}
			if (!string.IsNullOrEmpty(additionalError))
			{
				text += string.Format("({0})", additionalError);
			}
			return text;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x0019EE44 File Offset: 0x0019D044
		private static bool PassErrorPopup(NKC_PUBLISHER_RESULT_CODE resultCode)
		{
			return resultCode - NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_ACCOUNT_CHANGED <= 1 || resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_QUIT || resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_NOTICE_NO_SHOW;
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x0019EE63 File Offset: 0x0019D063
		public void WaitForCall(NKCPublisherModule.Func targetFunc, NKCPublisherModule.OnComplete onComplete, float timeout = 10f)
		{
			this.crWaitForCall = base.StartCoroutine(NKCPublisherModule.Instance._WaitForCall(targetFunc, onComplete, timeout));
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x0019EE7E File Offset: 0x0019D07E
		private IEnumerator _WaitForCall(NKCPublisherModule.Func targetFunc, NKCPublisherModule.OnComplete onComplete, float timeout)
		{
			this.m_bWaitCall = true;
			targetFunc(delegate(NKC_PUBLISHER_RESULT_CODE resultCode, string addError)
			{
				this.m_bWaitCall = false;
				NKCPublisherModule.OnComplete onComplete3 = onComplete;
				if (onComplete3 != null)
				{
					onComplete3(resultCode, addError);
				}
				if (this.crWaitForCall != null)
				{
					this.StopCoroutine(this.crWaitForCall);
					this.crWaitForCall = null;
				}
			});
			float waitTime = 0f;
			while (this.m_bWaitCall)
			{
				waitTime += Time.unscaledDeltaTime;
				if (waitTime >= timeout && this.m_bWaitCall)
				{
					Debug.Log("Timeout!!");
					this.OnTimeOut();
					NKCPublisherModule.OnComplete onComplete2 = onComplete;
					if (onComplete2 != null)
					{
						onComplete2(NKC_PUBLISHER_RESULT_CODE.NPRC_TIMEOUT, null);
					}
					this.crWaitForCall = null;
					yield break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x04004414 RID: 17428
		private static GameObject s_objInstance;

		// Token: 0x04004415 RID: 17429
		protected static NKCPublisherModule s_Instance;

		// Token: 0x04004421 RID: 17441
		public const float API_TIMEOUT = 10f;

		// Token: 0x04004422 RID: 17442
		private Coroutine crWaitForCall;

		// Token: 0x04004423 RID: 17443
		private const float DEFAULT_TIME_OUT = 10f;

		// Token: 0x04004424 RID: 17444
		private bool m_bWaitCall;

		// Token: 0x02001506 RID: 5382
		public abstract class NKCPMPush
		{
			// Token: 0x0600AABD RID: 43709
			public abstract void Init();

			// Token: 0x0600AABE RID: 43710 RVA: 0x0034F919 File Offset: 0x0034DB19
			public virtual void ReRegisterPush()
			{
			}

			// Token: 0x0600AABF RID: 43711 RVA: 0x0034F91B File Offset: 0x0034DB1B
			protected void UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP type, long timeTick)
			{
				if (this.ReserveLocalPush(timeTick, type))
				{
					this.RegisterNotifiyTime(type, timeTick);
				}
			}

			// Token: 0x0600AAC0 RID: 43712
			protected abstract bool ReserveLocalPush(DateTime newUtcTime, NKC_GAME_OPTION_ALARM_GROUP evtType);

			// Token: 0x0600AAC1 RID: 43713
			protected abstract void CancelLocalPush(NKC_GAME_OPTION_ALARM_GROUP evtType);

			// Token: 0x0600AAC2 RID: 43714 RVA: 0x0034F930 File Offset: 0x0034DB30
			protected virtual void ClearAllLocalPush()
			{
				foreach (object obj in Enum.GetValues(typeof(NKC_GAME_OPTION_ALARM_GROUP)))
				{
					NKC_GAME_OPTION_ALARM_GROUP nkc_GAME_OPTION_ALARM_GROUP = (NKC_GAME_OPTION_ALARM_GROUP)obj;
					if (this.IsValidType(nkc_GAME_OPTION_ALARM_GROUP))
					{
						this.CancelLocalPush(nkc_GAME_OPTION_ALARM_GROUP);
						this.ClearRegistedNotifyTime(nkc_GAME_OPTION_ALARM_GROUP);
					}
				}
			}

			// Token: 0x0600AAC3 RID: 43715 RVA: 0x0034F9A4 File Offset: 0x0034DBA4
			protected bool ReserveLocalPush(long newTimeTick, NKC_GAME_OPTION_ALARM_GROUP evtType)
			{
				return this.ReserveLocalPush(new DateTime(newTimeTick), evtType);
			}

			// Token: 0x0600AAC4 RID: 43716 RVA: 0x0034F9B3 File Offset: 0x0034DBB3
			public void SetAlarm(NKC_GAME_OPTION_ALARM_GROUP alarmGroup, bool allow)
			{
				if (this.IsValidType(alarmGroup))
				{
					this.SetLocalPush(alarmGroup, allow);
					if (alarmGroup != NKC_GAME_OPTION_ALARM_GROUP.ALLOW_ALL_ALARM)
					{
						this.UpdateAllAlarmButton();
					}
				}
				if (alarmGroup == NKC_GAME_OPTION_ALARM_GROUP.ALLOW_ALL_ALARM)
				{
					if (allow)
					{
						this.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.LONG_TERM_NOT_CONNECTED_NOTIFY, true);
						return;
					}
					this.ClearLocalPush(NKC_GAME_OPTION_ALARM_GROUP.LONG_TERM_NOT_CONNECTED_NOTIFY);
				}
			}

			// Token: 0x0600AAC5 RID: 43717 RVA: 0x0034F9E8 File Offset: 0x0034DBE8
			private void UpdateAllAlarmButton()
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData != null)
				{
					bool flag = gameOptionData.GetAllowAlarm(NKC_GAME_OPTION_ALARM_GROUP.ALLOW_ALL_ALARM);
					bool flag2 = true;
					foreach (object obj in Enum.GetValues(typeof(NKC_GAME_OPTION_ALARM_GROUP)))
					{
						NKC_GAME_OPTION_ALARM_GROUP type = (NKC_GAME_OPTION_ALARM_GROUP)obj;
						if (this.IsValidType(type))
						{
							if (flag)
							{
								flag2 = false;
								if (!gameOptionData.GetAllowAlarm(type))
								{
									flag = false;
									break;
								}
							}
							else
							{
								flag2 &= gameOptionData.GetAllowAlarm(type);
							}
						}
					}
					if (flag2)
					{
						flag = true;
					}
					gameOptionData.SetAllowAlarm(NKC_GAME_OPTION_ALARM_GROUP.ALLOW_ALL_ALARM, flag);
				}
			}

			// Token: 0x0600AAC6 RID: 43718 RVA: 0x0034FA98 File Offset: 0x0034DC98
			private void SetLocalPush(NKC_GAME_OPTION_ALARM_GROUP type, bool bActive)
			{
				if (!this.IsValidType(type))
				{
					return;
				}
				if (bActive)
				{
					this.UpdateLocalPush(type, false);
					return;
				}
				this.ClearRegistedNotifyTime(type);
				this.CancelLocalPush(type);
			}

			// Token: 0x0600AAC7 RID: 43719 RVA: 0x0034FAC0 File Offset: 0x0034DCC0
			private long GetCompleteTime(NKC_GAME_OPTION_ALARM_GROUP type)
			{
				switch (type)
				{
				case NKC_GAME_OPTION_ALARM_GROUP.WORLD_MAP_MISSION_COMPLETE:
					return this.GetCompleteTimeWorldMission();
				case NKC_GAME_OPTION_ALARM_GROUP.RESOURCE_SUPPLY_COMPLETE:
					return this.GetCompleteTimeAutoSupply(false);
				case NKC_GAME_OPTION_ALARM_GROUP.PVP_POINT_COMPLETE:
					return this.GetCompleteTimePVPPoint();
				case NKC_GAME_OPTION_ALARM_GROUP.CRAFT_COMPLETE:
					return this.GetCompleteTimeCraft();
				case NKC_GAME_OPTION_ALARM_GROUP.LONG_TERM_NOT_CONNECTED_NOTIFY:
					return this.GetNotifyTimeLongTermNotConnected();
				case NKC_GAME_OPTION_ALARM_GROUP.GUILD_DUNGEON_NOTIFY:
					if (NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON, 0, 0))
					{
						return this.GetNextGuildDungeonStateChangeTime();
					}
					break;
				}
				return 0L;
			}

			// Token: 0x0600AAC8 RID: 43720 RVA: 0x0034FB28 File Offset: 0x0034DD28
			private long GetCompleteTimeAutoSupply(bool bForce = false)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				NKMUserExpTemplet userExpTemplet = NKCExpManager.GetUserExpTemplet(nkmuserData);
				if (userExpTemplet == null)
				{
					return 0L;
				}
				long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(2);
				if (userExpTemplet.m_EterniumCap <= countMiscItem || NKCScenManager.CurrentUserData().lastEterniumUpdateDate == DateTime.MinValue)
				{
					return 0L;
				}
				int num = (int)((float)(userExpTemplet.m_EterniumCap - countMiscItem) / (float)userExpTemplet.m_RechargeEternium + 0.5f);
				return NKCSynchronizedTime.ToUtcTime(NKCScenManager.CurrentUserData().lastEterniumUpdateDate.AddTicks(NKMCommonConst.RECHARGE_TIME.Ticks * (long)num)).Ticks;
			}

			// Token: 0x0600AAC9 RID: 43721 RVA: 0x0034FBC0 File Offset: 0x0034DDC0
			private long GetCompleteTimeWorldMission()
			{
				long num = 0L;
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in nkmuserData.m_WorldmapData.worldMapCityDataMap)
					{
						NKMWorldMapCityData value = keyValuePair.Value;
						if (value != null && value.HasMission() && !value.IsMissionFinished(NKCSynchronizedTime.GetServerUTCTime(0.0)) && num < value.worldMapMission.completeTime)
						{
							num = value.worldMapMission.completeTime;
						}
					}
				}
				return num;
			}

			// Token: 0x0600AACA RID: 43722 RVA: 0x0034FC68 File Offset: 0x0034DE68
			private long GetCompleteTimeCraft()
			{
				long num = 0L;
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					foreach (KeyValuePair<byte, NKMCraftSlotData> keyValuePair in nkmuserData.m_CraftData.SlotList)
					{
						if (keyValuePair.Value.CompleteDate > num)
						{
							num = keyValuePair.Value.CompleteDate;
						}
					}
				}
				return num;
			}

			// Token: 0x0600AACB RID: 43723 RVA: 0x0034FCE4 File Offset: 0x0034DEE4
			private long GetCompleteTimePVPPoint()
			{
				long result = 0L;
				long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(6);
				int charge_POINT_MAX_COUNT = NKMPvpCommonConst.Instance.CHARGE_POINT_MAX_COUNT;
				if (countMiscItem < (long)charge_POINT_MAX_COUNT)
				{
					int num = charge_POINT_MAX_COUNT - (int)countMiscItem;
					int num2 = num / NKMPvpCommonConst.Instance.CHARGE_POINT_ONE_STEP;
					if (num - num2 * NKMPvpCommonConst.Instance.CHARGE_POINT_ONE_STEP > 0)
					{
						num2++;
					}
					DateTime dateTime = new DateTime(NKCPVPManager.GetLastUpdateChargePointTicks());
					result = new DateTime(dateTime.Ticks + NKMPvpCommonConst.Instance.CHARGE_POINT_REFRESH_INTERVAL_TICKS * (long)num2).Ticks;
				}
				return result;
			}

			// Token: 0x0600AACC RID: 43724 RVA: 0x0034FD6C File Offset: 0x0034DF6C
			private long GetNotifyTimeLongTermNotConnected()
			{
				return NKCSynchronizedTime.GetServerUTCTime(0.0).AddDays(10.0).Ticks;
			}

			// Token: 0x0600AACD RID: 43725 RVA: 0x0034FDA0 File Offset: 0x0034DFA0
			private long GetNextGuildDungeonStateChangeTime()
			{
				return NKCGuildCoopManager.m_NextSessionStartDateUTC.Ticks;
			}

			// Token: 0x0600AACE RID: 43726 RVA: 0x0034FDBC File Offset: 0x0034DFBC
			protected bool GetRegistedNotifyTime(NKC_GAME_OPTION_ALARM_GROUP type, out NKCPublisherModule.NKCPMPush.LocalPushData pushData)
			{
				pushData = default(NKCPublisherModule.NKCPMPush.LocalPushData);
				string key = string.Format("{0}_{1}_{2}", "LOCAL_PUSH", type, NKCScenManager.CurrentUserData().m_UserUID);
				if (!PlayerPrefs.HasKey(key))
				{
					pushData.reserveTime = new DateTime(0L);
					return false;
				}
				string @string = PlayerPrefs.GetString(key);
				long ticks = 0L;
				if (!long.TryParse(@string, out ticks))
				{
					pushData.reserveTime = new DateTime(0L);
					return false;
				}
				pushData.reserveTime = new DateTime(ticks);
				pushData.eventType = type;
				return true;
			}

			// Token: 0x0600AACF RID: 43727 RVA: 0x0034FE41 File Offset: 0x0034E041
			protected void RegisterNotifiyTime(NKC_GAME_OPTION_ALARM_GROUP type, long timeTick)
			{
				PlayerPrefs.SetString(string.Format("{0}_{1}_{2}", "LOCAL_PUSH", type, NKCScenManager.CurrentUserData().m_UserUID), timeTick.ToString());
			}

			// Token: 0x0600AAD0 RID: 43728 RVA: 0x0034FE73 File Offset: 0x0034E073
			protected void ClearRegistedNotifyTime(NKC_GAME_OPTION_ALARM_GROUP type)
			{
				PlayerPrefs.DeleteKey(string.Format("{0}_{1}_{2}", "LOCAL_PUSH", type, NKCScenManager.CurrentUserData().m_UserUID));
			}

			// Token: 0x0600AAD1 RID: 43729 RVA: 0x0034FEA0 File Offset: 0x0034E0A0
			protected List<NKCPublisherModule.NKCPMPush.LocalPushData> GetAllRegisteredPush()
			{
				List<NKCPublisherModule.NKCPMPush.LocalPushData> list = new List<NKCPublisherModule.NKCPMPush.LocalPushData>();
				foreach (object obj in Enum.GetValues(typeof(NKC_GAME_OPTION_ALARM_GROUP)))
				{
					NKC_GAME_OPTION_ALARM_GROUP type = (NKC_GAME_OPTION_ALARM_GROUP)obj;
					NKCPublisherModule.NKCPMPush.LocalPushData localPushData;
					if (this.GetRegistedNotifyTime(type, out localPushData) && !NKCSynchronizedTime.IsFinished(localPushData.reserveTime))
					{
						list.Add(localPushData);
					}
				}
				return list;
			}

			// Token: 0x0600AAD2 RID: 43730 RVA: 0x0034FF24 File Offset: 0x0034E124
			public void UpdateAllLocalPush()
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData == null)
				{
					return;
				}
				foreach (object obj in Enum.GetValues(typeof(NKC_GAME_OPTION_ALARM_GROUP)))
				{
					NKC_GAME_OPTION_ALARM_GROUP type = (NKC_GAME_OPTION_ALARM_GROUP)obj;
					if (this.IsValidType(type))
					{
						this.SetLocalPush(type, gameOptionData.GetAllowAlarm(type));
					}
				}
			}

			// Token: 0x0600AAD3 RID: 43731 RVA: 0x0034FFA8 File Offset: 0x0034E1A8
			public void UpdateAllLocalPush(bool bVal)
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData == null)
				{
					return;
				}
				foreach (object obj in Enum.GetValues(typeof(NKC_GAME_OPTION_ALARM_GROUP)))
				{
					NKC_GAME_OPTION_ALARM_GROUP type = (NKC_GAME_OPTION_ALARM_GROUP)obj;
					if (this.IsValidType(type))
					{
						gameOptionData.SetAllowAlarm(type, bVal);
					}
				}
			}

			// Token: 0x0600AAD4 RID: 43732 RVA: 0x00350024 File Offset: 0x0034E224
			private bool IsPossiblePush(NKC_GAME_OPTION_ALARM_GROUP type)
			{
				if (type == NKC_GAME_OPTION_ALARM_GROUP.LONG_TERM_NOT_CONNECTED_NOTIFY)
				{
					return true;
				}
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				return gameOptionData != null && gameOptionData.GetAllowAlarm(type);
			}

			// Token: 0x0600AAD5 RID: 43733 RVA: 0x0035004E File Offset: 0x0034E24E
			private bool IsValidType(NKC_GAME_OPTION_ALARM_GROUP type)
			{
				return type == NKC_GAME_OPTION_ALARM_GROUP.CRAFT_COMPLETE || type == NKC_GAME_OPTION_ALARM_GROUP.PVP_POINT_COMPLETE || type == NKC_GAME_OPTION_ALARM_GROUP.WORLD_MAP_MISSION_COMPLETE || type == NKC_GAME_OPTION_ALARM_GROUP.LONG_TERM_NOT_CONNECTED_NOTIFY || type == NKC_GAME_OPTION_ALARM_GROUP.RESOURCE_SUPPLY_COMPLETE;
			}

			// Token: 0x0600AAD6 RID: 43734 RVA: 0x00350068 File Offset: 0x0034E268
			public void UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP type, bool bForce = false)
			{
				if (!this.IsValidType(type))
				{
					return;
				}
				if (!this.IsPossiblePush(type))
				{
					return;
				}
				long num = 0L;
				NKCPublisherModule.NKCPMPush.LocalPushData localPushData;
				if (this.GetRegistedNotifyTime(type, out localPushData))
				{
					num = localPushData.reserveTime.Ticks;
				}
				long completeTime = this.GetCompleteTime(type);
				if (completeTime == 0L)
				{
					this.ClearRegistedNotifyTime(type);
					this.CancelLocalPush(type);
					return;
				}
				if (bForce || num < completeTime)
				{
					Debug.Log(string.Format("[로컬푸시 등록] Type : {0} Time : {1}, CurcompleteTime : {2}", type, new DateTime(num), new DateTime(completeTime)));
					this.UpdateLocalPush(type, completeTime);
				}
			}

			// Token: 0x0600AAD7 RID: 43735 RVA: 0x003500F8 File Offset: 0x0034E2F8
			protected string GetLocalPushText(NKC_GAME_OPTION_ALARM_GROUP evtType)
			{
				switch (evtType)
				{
				case NKC_GAME_OPTION_ALARM_GROUP.WORLD_MAP_MISSION_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_WORLD_MAP_DESCRIPTION;
				case NKC_GAME_OPTION_ALARM_GROUP.RESOURCE_SUPPLY_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_AUTO_SUPPLY_DESCRIPTION;
				case NKC_GAME_OPTION_ALARM_GROUP.PVP_POINT_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_PVP_POINT_DESCRIPTION;
				case NKC_GAME_OPTION_ALARM_GROUP.CRAFT_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_GEAR_CRAFT_DESCRIPTION;
				case NKC_GAME_OPTION_ALARM_GROUP.LONG_TERM_NOT_CONNECTED_NOTIFY:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_NOT_CONNECTED_DESCRIPTION;
				default:
					return "";
				}
			}

			// Token: 0x0600AAD8 RID: 43736 RVA: 0x00350148 File Offset: 0x0034E348
			protected string GetLocalPushTitle(NKC_GAME_OPTION_ALARM_GROUP evtType)
			{
				switch (evtType)
				{
				case NKC_GAME_OPTION_ALARM_GROUP.WORLD_MAP_MISSION_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_WORLD_MAP_TITLE;
				case NKC_GAME_OPTION_ALARM_GROUP.RESOURCE_SUPPLY_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_AUTO_SUPPLY_TITLE;
				case NKC_GAME_OPTION_ALARM_GROUP.PVP_POINT_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_PVP_POINT_TITLE;
				case NKC_GAME_OPTION_ALARM_GROUP.CRAFT_COMPLETE:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_GEAR_CRAFT_TITLE;
				case NKC_GAME_OPTION_ALARM_GROUP.LONG_TERM_NOT_CONNECTED_NOTIFY:
					return NKCUtilString.GET_STRING_TOY_LOCAL_PUSH_NOT_CONNECTED_TITLE;
				default:
					return "";
				}
			}

			// Token: 0x0600AAD9 RID: 43737 RVA: 0x00350196 File Offset: 0x0034E396
			private void ClearLocalPush(NKC_GAME_OPTION_ALARM_GROUP type)
			{
				if (!this.IsValidType(type))
				{
					return;
				}
				this.CancelLocalPush(type);
				this.ClearRegistedNotifyTime(type);
			}

			// Token: 0x04009F90 RID: 40848
			protected const string LOCAL_PUSH = "LOCAL_PUSH";

			// Token: 0x02001A61 RID: 6753
			protected struct LocalPushData
			{
				// Token: 0x0600BBC8 RID: 48072 RVA: 0x0036F924 File Offset: 0x0036DB24
				public LocalPushData(NKC_GAME_OPTION_ALARM_GROUP type, long timeTick)
				{
					this.eventType = type;
					this.reserveTime = new DateTime(timeTick);
				}

				// Token: 0x0600BBC9 RID: 48073 RVA: 0x0036F939 File Offset: 0x0036DB39
				public LocalPushData(NKC_GAME_OPTION_ALARM_GROUP type, DateTime time)
				{
					this.eventType = type;
					this.reserveTime = time;
				}

				// Token: 0x0400AE43 RID: 44611
				public NKC_GAME_OPTION_ALARM_GROUP eventType;

				// Token: 0x0400AE44 RID: 44612
				public DateTime reserveTime;
			}
		}

		// Token: 0x02001507 RID: 5383
		public enum SNS_SHARE_TYPE
		{
			// Token: 0x04009F92 RID: 40850
			SST_NONE = -1,
			// Token: 0x04009F93 RID: 40851
			SST_WECHAT = 1,
			// Token: 0x04009F94 RID: 40852
			SST_WECHAT_MOMENTS,
			// Token: 0x04009F95 RID: 40853
			SST_WEIBO,
			// Token: 0x04009F96 RID: 40854
			SST_FACEBOOK,
			// Token: 0x04009F97 RID: 40855
			SST_QQ
		}

		// Token: 0x02001508 RID: 5384
		// (Invoke) Token: 0x0600AADC RID: 43740
		public delegate void OnComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError = null);

		// Token: 0x02001509 RID: 5385
		public enum ePublisherType
		{
			// Token: 0x04009F99 RID: 40857
			None,
			// Token: 0x04009F9A RID: 40858
			NexonToy,
			// Token: 0x04009F9B RID: 40859
			Zlong,
			// Token: 0x04009F9C RID: 40860
			NexonPC,
			// Token: 0x04009F9D RID: 40861
			SB_Gamebase,
			// Token: 0x04009F9E RID: 40862
			NexonToyJP,
			// Token: 0x04009F9F RID: 40863
			JPPC,
			// Token: 0x04009FA0 RID: 40864
			STEAM
		}

		// Token: 0x0200150A RID: 5386
		public enum ePublisherInitState
		{
			// Token: 0x04009FA2 RID: 40866
			NotInitialized,
			// Token: 0x04009FA3 RID: 40867
			Maintanance,
			// Token: 0x04009FA4 RID: 40868
			Initialized
		}

		// Token: 0x0200150B RID: 5387
		public abstract class NKCPMAuthentication
		{
			// Token: 0x0600AADF RID: 43743 RVA: 0x003501B8 File Offset: 0x0034E3B8
			public virtual bool Init()
			{
				return true;
			}

			// Token: 0x0600AAE0 RID: 43744
			public abstract string GetPublisherAccountCode();

			// Token: 0x17001864 RID: 6244
			// (get) Token: 0x0600AAE1 RID: 43745 RVA: 0x003501BB File Offset: 0x0034E3BB
			// (set) Token: 0x0600AAE2 RID: 43746 RVA: 0x003501C3 File Offset: 0x0034E3C3
			public virtual bool LogoutReservedAfterGame { get; set; }

			// Token: 0x0600AAE3 RID: 43747 RVA: 0x003501CC File Offset: 0x0034E3CC
			public virtual void LogoutReserved()
			{
				this.LogoutReservedAfterGame = false;
				this.Logout(null);
			}

			// Token: 0x0600AAE4 RID: 43748 RVA: 0x003501DC File Offset: 0x0034E3DC
			public virtual NKCPublisherModule.NKCPMAuthentication.LOGIN_IDP_TYPE GetLoginIdpType()
			{
				return NKCPublisherModule.NKCPMAuthentication.LOGIN_IDP_TYPE.guest;
			}

			// Token: 0x0600AAE5 RID: 43749
			public abstract void LoginToPublisher(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x17001865 RID: 6245
			// (get) Token: 0x0600AAE6 RID: 43750
			public abstract bool LoginToPublisherCompleted { get; }

			// Token: 0x0600AAE7 RID: 43751
			public abstract void PrepareCSLogin(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x0600AAE8 RID: 43752
			public abstract ISerializable MakeLoginServerLoginReqPacket();

			// Token: 0x0600AAE9 RID: 43753
			public abstract ISerializable MakeGameServerLoginReqPacket(string accesstoken);

			// Token: 0x0600AAEA RID: 43754
			public abstract void ChangeAccount(NKCPublisherModule.OnComplete dOnComplete, bool syncAccount);

			// Token: 0x0600AAEB RID: 43755
			public abstract void Logout(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x0600AAEC RID: 43756 RVA: 0x003501DF File Offset: 0x0034E3DF
			public virtual bool IsTryAuthWhenSessionExpired()
			{
				return false;
			}

			// Token: 0x0600AAED RID: 43757 RVA: 0x003501E2 File Offset: 0x0034E3E2
			public virtual void TryRestoreQuitUser(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL_QUIT_USER_RESTORE, null);
				}
			}

			// Token: 0x0600AAEE RID: 43758 RVA: 0x003501F3 File Offset: 0x0034E3F3
			public virtual void TryResolveUser(NKCPublisherModule.OnComplete onComplete)
			{
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL_USER_RESOLVE, null);
				}
			}

			// Token: 0x0600AAEF RID: 43759 RVA: 0x00350204 File Offset: 0x0034E404
			public virtual void ResetConnection()
			{
			}

			// Token: 0x0600AAF0 RID: 43760 RVA: 0x00350206 File Offset: 0x0034E406
			public virtual void OnReconnectFail(NKCPublisherModule.OnComplete dOnComplete)
			{
			}

			// Token: 0x0600AAF1 RID: 43761 RVA: 0x00350208 File Offset: 0x0034E408
			public virtual bool CheckExitCallFirst()
			{
				return false;
			}

			// Token: 0x0600AAF2 RID: 43762 RVA: 0x0035020B File Offset: 0x0034E40B
			public virtual void Exit(NKCPublisherModule.OnComplete onComplete)
			{
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AAF3 RID: 43763 RVA: 0x00350218 File Offset: 0x0034E418
			public virtual bool CheckNeedToCheckEnableQR_AfterPubLogin()
			{
				return false;
			}

			// Token: 0x0600AAF4 RID: 43764 RVA: 0x0035021B File Offset: 0x0034E41B
			public virtual bool CheckEnableQR_Login()
			{
				return false;
			}

			// Token: 0x0600AAF5 RID: 43765 RVA: 0x0035021E File Offset: 0x0034E41E
			public virtual void QR_Login(NKCPublisherModule.OnComplete onComplete)
			{
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AAF6 RID: 43766 RVA: 0x0035022B File Offset: 0x0034E42B
			public virtual void LoginToPublisherBy(string providerName, NKCPublisherModule.OnComplete onComplete)
			{
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AAF7 RID: 43767 RVA: 0x00350238 File Offset: 0x0034E438
			public virtual void AddMapping(string providerName, NKCPublisherModule.OnComplete onComplete)
			{
				Debug.Log("AddMapping : " + providerName);
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AAF8 RID: 43768 RVA: 0x00350255 File Offset: 0x0034E455
			public virtual void RemoveMapping(string providerName)
			{
				Debug.Log("RemoveMapping : " + providerName);
			}

			// Token: 0x0600AAF9 RID: 43769 RVA: 0x00350267 File Offset: 0x0034E467
			public virtual void Withdraw(NKCPublisherModule.OnComplete onComplete)
			{
				Debug.Log("Withdraw");
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AAFA RID: 43770 RVA: 0x0035027E File Offset: 0x0034E47E
			public virtual void TemporaryWithdrawal(NKCPublisherModule.OnComplete onComplete)
			{
				Debug.Log("TemporaryWithdrawal");
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AAFB RID: 43771 RVA: 0x00350295 File Offset: 0x0034E495
			public virtual bool IsGuest()
			{
				return false;
			}

			// Token: 0x0600AAFC RID: 43772 RVA: 0x00350298 File Offset: 0x0034E498
			public virtual bool OnLoginSuccessToCS()
			{
				return true;
			}

			// Token: 0x0600AAFD RID: 43773 RVA: 0x0035029B File Offset: 0x0034E49B
			public virtual void OpenCertification()
			{
			}

			// Token: 0x0600AAFE RID: 43774 RVA: 0x0035029D File Offset: 0x0034E49D
			public virtual string GetAccountLinkText()
			{
				return "";
			}

			// Token: 0x0600AAFF RID: 43775 RVA: 0x003502A4 File Offset: 0x0034E4A4
			public virtual ISerializable MakeWithdrawReqPacket()
			{
				return null;
			}

			// Token: 0x02001A62 RID: 6754
			public enum LOGIN_IDP_TYPE
			{
				// Token: 0x0400AE46 RID: 44614
				none,
				// Token: 0x0400AE47 RID: 44615
				guest,
				// Token: 0x0400AE48 RID: 44616
				google,
				// Token: 0x0400AE49 RID: 44617
				facebook,
				// Token: 0x0400AE4A RID: 44618
				twitter,
				// Token: 0x0400AE4B RID: 44619
				appleid
			}
		}

		// Token: 0x0200150C RID: 5388
		public abstract class NKCPMInAppPurchase
		{
			// Token: 0x0600AB01 RID: 43777 RVA: 0x003502AF File Offset: 0x0034E4AF
			public virtual void Init()
			{
			}

			// Token: 0x0600AB02 RID: 43778
			public abstract void RequestBillingProductList(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x17001866 RID: 6246
			// (get) Token: 0x0600AB03 RID: 43779
			public abstract bool CheckReceivedBillingProductList { get; }

			// Token: 0x0600AB04 RID: 43780
			public abstract bool IsRegisteredProduct(string marketID, int productID);

			// Token: 0x0600AB05 RID: 43781
			public abstract string GetLocalPriceString(string marketID, int productID);

			// Token: 0x0600AB06 RID: 43782
			public abstract decimal GetLocalPrice(string marketID, int productID);

			// Token: 0x0600AB07 RID: 43783 RVA: 0x003502B1 File Offset: 0x0034E4B1
			public virtual string GetPriceCurrency(string marketID, int productID)
			{
				return "";
			}

			// Token: 0x0600AB08 RID: 43784
			public abstract void InappPurchase(ShopItemTemplet shopTemplet, NKCPublisherModule.OnComplete dOnComplete, string metadata = "", List<int> lstSelection = null);

			// Token: 0x0600AB09 RID: 43785
			public abstract void BillingRestore(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x0600AB0A RID: 43786
			public abstract List<int> GetInappProductIDs();

			// Token: 0x0600AB0B RID: 43787 RVA: 0x003502B8 File Offset: 0x0034E4B8
			public virtual void OpenPolicy(NKCPublisherModule.OnComplete dOnClose)
			{
				if (dOnClose != null)
				{
					dOnClose(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB0C RID: 43788 RVA: 0x003502C5 File Offset: 0x0034E4C5
			public virtual string GetCurrencyMark(int productID)
			{
				return "￦";
			}

			// Token: 0x0600AB0D RID: 43789 RVA: 0x003502CC File Offset: 0x0034E4CC
			public virtual bool ShowJPNPaymentPolicy()
			{
				NKCDefineManager.DEFINE_NX_PC();
				return false;
			}

			// Token: 0x0600AB0E RID: 43790 RVA: 0x003502D5 File Offset: 0x0034E4D5
			public virtual bool IsJPNPaymentPolicy()
			{
				return false;
			}

			// Token: 0x0600AB0F RID: 43791 RVA: 0x003502D8 File Offset: 0x0034E4D8
			public virtual bool ShowCashResourceState()
			{
				return false;
			}

			// Token: 0x0600AB10 RID: 43792 RVA: 0x003502DB File Offset: 0x0034E4DB
			public virtual void OpenPaymentLaw(NKCPublisherModule.OnComplete dOnClose)
			{
				if (dOnClose != null)
				{
					dOnClose(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB11 RID: 43793 RVA: 0x003502E8 File Offset: 0x0034E4E8
			public virtual void OpenCommercialLaw(NKCPublisherModule.OnComplete dOnClose)
			{
				if (dOnClose != null)
				{
					dOnClose(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB12 RID: 43794 RVA: 0x003502F5 File Offset: 0x0034E4F5
			public virtual bool ShowPurchasePolicy()
			{
				return false;
			}

			// Token: 0x0600AB13 RID: 43795 RVA: 0x003502F8 File Offset: 0x0034E4F8
			public virtual bool ShowPurchasePolicyBtn()
			{
				return false;
			}

			// Token: 0x0600AB14 RID: 43796 RVA: 0x003502FC File Offset: 0x0034E4FC
			protected bool AddBillingRestoreInfo(string stampId, string marketID, string token, string state)
			{
				if (this.m_billingRestoredDic.ContainsKey(stampId))
				{
					Log.Warn("[BillingRestoreInfo] Already Exist", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPublisherModule.cs", 746);
					return false;
				}
				NKCPublisherModule.NKCPMInAppPurchase.BillingRestoreInfo billingRestoreInfo = new NKCPublisherModule.NKCPMInAppPurchase.BillingRestoreInfo(marketID, token, state);
				ShopItemTemplet shopTempletByMarketID = NKCShopManager.GetShopTempletByMarketID(marketID);
				if (shopTempletByMarketID == null)
				{
					billingRestoreInfo.SetError(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_INVALID_SHOPTEMPLET, marketID);
					Log.Warn("[BillingRestoreInfo] 템플릿에 해당 상품 찾지 못함 : " + marketID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPublisherModule.cs", 756);
					this.m_billingRestoredDic.Add(stampId, billingRestoreInfo);
					return false;
				}
				billingRestoreInfo.ProductId = shopTempletByMarketID.m_ProductID;
				if (!this.IsRegisteredProduct(marketID, billingRestoreInfo.ProductId))
				{
					billingRestoreInfo.SetError(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_INVALID_TOYPRODUCT, marketID);
					Log.Warn(string.Format("[BillingRestoreInfo] 토이 상품리스트에서 상품 찾지 못함 : {0}, {1}", marketID, shopTempletByMarketID.m_ProductID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPublisherModule.cs", 766);
					this.m_billingRestoredDic.Add(stampId, billingRestoreInfo);
					return false;
				}
				this.m_billingRestoredDic.Add(stampId, billingRestoreInfo);
				return true;
			}

			// Token: 0x04009FA6 RID: 40870
			private readonly Dictionary<string, NKCPublisherModule.NKCPMInAppPurchase.BillingRestoreInfo> m_billingRestoredDic = new Dictionary<string, NKCPublisherModule.NKCPMInAppPurchase.BillingRestoreInfo>();

			// Token: 0x02001A63 RID: 6755
			public class BillingRestoreInfo
			{
				// Token: 0x0600BBCA RID: 48074 RVA: 0x0036F949 File Offset: 0x0036DB49
				public BillingRestoreInfo(string marketId, string token, string state)
				{
					this.MarketId = marketId;
					this.Token = token;
					this.State = state;
				}

				// Token: 0x0600BBCB RID: 48075 RVA: 0x0036F966 File Offset: 0x0036DB66
				public void SetServerError(NKM_ERROR_CODE errorCode)
				{
					this._errorCode = errorCode;
				}

				// Token: 0x0600BBCC RID: 48076 RVA: 0x0036F96F File Offset: 0x0036DB6F
				public void SetError(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
				{
					if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
					{
						return;
					}
					NKCPublisherModule.CheckError(resultCode, additionalError, false, null, true);
				}

				// Token: 0x0400AE4C RID: 44620
				public int ProductId;

				// Token: 0x0400AE4D RID: 44621
				public readonly string MarketId;

				// Token: 0x0400AE4E RID: 44622
				public readonly string Token;

				// Token: 0x0400AE4F RID: 44623
				public readonly string State;

				// Token: 0x0400AE50 RID: 44624
				private NKM_ERROR_CODE _errorCode;
			}
		}

		// Token: 0x0200150D RID: 5389
		public struct FunnelLogData
		{
			// Token: 0x04009FA7 RID: 40871
			public string funnelName;
		}

		// Token: 0x0200150E RID: 5390
		public abstract class NKCPMStatistics
		{
			// Token: 0x0600AB16 RID: 43798 RVA: 0x003503F2 File Offset: 0x0034E5F2
			public string GetDeviceID()
			{
				return SystemInfo.deviceUniqueIdentifier;
			}

			// Token: 0x0600AB17 RID: 43799 RVA: 0x003503FC File Offset: 0x0034E5FC
			public virtual void LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction funnelPosition, int key = 0, string data = null)
			{
				switch (funnelPosition)
				{
				case NKCPublisherModule.NKCPMStatistics.eClientAction.DungeonGameClear:
					NKCAdjustManager.OnClearDungeon(key);
					break;
				case NKCPublisherModule.NKCPMStatistics.eClientAction.WarfareGameClear:
					NKCAdjustManager.OnWarfareResult(key);
					break;
				case NKCPublisherModule.NKCPMStatistics.eClientAction.AfterSyncAccountComplete:
					NKCAdjustManager.OnCustomEvent("05_loading_complete");
					break;
				case NKCPublisherModule.NKCPMStatistics.eClientAction.MissionClearAdjust:
					NKCAdjustManager.OnCustomEvent(data);
					break;
				}
				this.LogClientActionForPublisher(funnelPosition, key, data);
			}

			// Token: 0x0600AB18 RID: 43800
			public abstract void LogClientActionForPublisher(NKCPublisherModule.NKCPMStatistics.eClientAction funnelPosition, int key, string data);

			// Token: 0x0600AB19 RID: 43801 RVA: 0x00350488 File Offset: 0x0034E688
			public virtual void TrackPurchase(int itemID)
			{
			}

			// Token: 0x0600AB1A RID: 43802 RVA: 0x0035048A File Offset: 0x0034E68A
			public virtual void OnLoginSuccessToCS(NKMPacket_JOIN_LOBBY_ACK res)
			{
			}

			// Token: 0x0600AB1B RID: 43803 RVA: 0x0035048C File Offset: 0x0034E68C
			public virtual void OnFirstEpisodeClear()
			{
				Log.Debug("OnFirstEpisodeClear!!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPublisherModule.cs", 887);
			}

			// Token: 0x0600AB1C RID: 43804 RVA: 0x003504A2 File Offset: 0x0034E6A2
			public virtual void OnUserLevelUp(int newUserLevel)
			{
			}

			// Token: 0x0600AB1D RID: 43805 RVA: 0x003504A4 File Offset: 0x0034E6A4
			public virtual void OnPatchStart()
			{
			}

			// Token: 0x0600AB1E RID: 43806 RVA: 0x003504A6 File Offset: 0x0034E6A6
			public virtual void OnPatchEnd()
			{
			}

			// Token: 0x0600AB1F RID: 43807 RVA: 0x003504A8 File Offset: 0x0034E6A8
			public virtual void OnResetAgreement()
			{
			}

			// Token: 0x02001A64 RID: 6756
			public enum eClientAction
			{
				// Token: 0x0400AE52 RID: 44626
				AppStart,
				// Token: 0x0400AE53 RID: 44627
				Patch_TagGetFailed,
				// Token: 0x0400AE54 RID: 44628
				Patch_TagProvided,
				// Token: 0x0400AE55 RID: 44629
				Patch_VersionCheckComplete,
				// Token: 0x0400AE56 RID: 44630
				Patch_DownloadAvailable,
				// Token: 0x0400AE57 RID: 44631
				Patch_DownloadStart,
				// Token: 0x0400AE58 RID: 44632
				Patch_DownloadComplete,
				// Token: 0x0400AE59 RID: 44633
				Patch_MoveToMainScene,
				// Token: 0x0400AE5A RID: 44634
				TryLoginToGameServer,
				// Token: 0x0400AE5B RID: 44635
				Login_ShowNotice,
				// Token: 0x0400AE5C RID: 44636
				DungeonGameClear,
				// Token: 0x0400AE5D RID: 44637
				WarfareGameClear,
				// Token: 0x0400AE5E RID: 44638
				PvPGameFinished,
				// Token: 0x0400AE5F RID: 44639
				Lobby_ShowNotice,
				// Token: 0x0400AE60 RID: 44640
				PlayerNameChanged,
				// Token: 0x0400AE61 RID: 44641
				AfterPublisherLogin,
				// Token: 0x0400AE62 RID: 44642
				AfterSyncAccountComplete,
				// Token: 0x0400AE63 RID: 44643
				MissionClear,
				// Token: 0x0400AE64 RID: 44644
				MissionClearAdjust
			}
		}

		// Token: 0x0200150F RID: 5391
		public abstract class NKCPMNotice
		{
			// Token: 0x0600AB21 RID: 43809 RVA: 0x003504B2 File Offset: 0x0034E6B2
			public virtual string NoticeUrl(bool bPatcher = false)
			{
				return "";
			}

			// Token: 0x0600AB22 RID: 43810 RVA: 0x003504B9 File Offset: 0x0034E6B9
			public virtual bool CheckOpenNoticeWhenFirstLoginSuccess()
			{
				return false;
			}

			// Token: 0x0600AB23 RID: 43811 RVA: 0x003504BC File Offset: 0x0034E6BC
			public virtual bool CheckOpenNoticeWhenFirstLobbyVisit()
			{
				return true;
			}

			// Token: 0x0600AB24 RID: 43812
			public abstract void OpenNotice(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x0600AB25 RID: 43813
			public abstract void NotifyMainenance(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x0600AB26 RID: 43814 RVA: 0x003504BF File Offset: 0x0034E6BF
			public virtual void OpenCustomerCenter(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB27 RID: 43815 RVA: 0x003504CC File Offset: 0x0034E6CC
			public virtual bool IsActiveCustomerCenter()
			{
				return true;
			}

			// Token: 0x0600AB28 RID: 43816 RVA: 0x003504CF File Offset: 0x0034E6CF
			public virtual void OpenQnA(NKCPublisherModule.OnComplete dOnComplete)
			{
			}

			// Token: 0x0600AB29 RID: 43817 RVA: 0x003504D4 File Offset: 0x0034E6D4
			public virtual void OpenCommunity(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (NKMContentsVersionManager.HasCountryTag(CountryTagType.TWN))
				{
					Application.OpenURL("https://www.facebook.com/gamebeansFW");
					return;
				}
				if (NKMContentsVersionManager.HasCountryTag(CountryTagType.SEA))
				{
					Application.OpenURL("https://www.facebook.com/CounterSide-113060560534111");
					return;
				}
				if (!NKCPublisherModule.IsNexonPCBuild())
				{
					NKCPublisherModule.Notice.OpenNotice(null);
					return;
				}
				if (NKMContentsVersionManager.HasCountryTag(CountryTagType.KOR))
				{
					Application.OpenURL("https://forum.nexon.com/counterside/main");
					return;
				}
				NKCPublisherModule.Notice.OpenNotice(null);
			}

			// Token: 0x0600AB2A RID: 43818 RVA: 0x00350538 File Offset: 0x0034E738
			public virtual void OpenSurvey(long surveyID, NKCPublisherModule.OnComplete onComplete)
			{
			}

			// Token: 0x0600AB2B RID: 43819 RVA: 0x0035053A File Offset: 0x0034E73A
			public virtual void OpenURL(string url, NKCPublisherModule.OnComplete dOnComplete)
			{
				Debug.Log("OpenURL : " + url);
				Application.OpenURL(url);
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB2C RID: 43820 RVA: 0x0035055D File Offset: 0x0034E75D
			public virtual void OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces placeType, NKCPublisherModule.OnComplete dOnComplete)
			{
				Debug.Log("OpenPromotionalBanner " + placeType.ToString());
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB2D RID: 43821 RVA: 0x00350586 File Offset: 0x0034E786
			public virtual void OpenInfoWindow(NKCPublisherModule.OnComplete dOnComplete)
			{
				Debug.Log("OpenInfoWindow");
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB2E RID: 43822 RVA: 0x0035059D File Offset: 0x0034E79D
			public virtual void OpenAgreement(NKCPublisherModule.OnComplete dOnComplete)
			{
				Debug.Log("OpenAgreement");
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB2F RID: 43823 RVA: 0x003505B4 File Offset: 0x0034E7B4
			public virtual void OpenPrivacyPolicy(NKCPublisherModule.OnComplete dOnComplete)
			{
				Debug.Log("OpenPrivacyPolicy");
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x02001A65 RID: 6757
			public enum eOptionalBannerPlaces
			{
				// Token: 0x0400AE66 RID: 44646
				AppLaunch,
				// Token: 0x0400AE67 RID: 44647
				EnterLobby,
				// Token: 0x0400AE68 RID: 44648
				EP1Start,
				// Token: 0x0400AE69 RID: 44649
				EP2Act2Clear,
				// Token: 0x0400AE6A RID: 44650
				EP2Clear,
				// Token: 0x0400AE6B RID: 44651
				maintenance,
				// Token: 0x0400AE6C RID: 44652
				ep1act4clear
			}
		}

		// Token: 0x02001510 RID: 5392
		public abstract class NKCPMMarketing
		{
			// Token: 0x0600AB31 RID: 43825 RVA: 0x003505D3 File Offset: 0x0034E7D3
			public virtual void Init()
			{
			}

			// Token: 0x0600AB32 RID: 43826 RVA: 0x003505D5 File Offset: 0x0034E7D5
			public virtual bool IsEnableWechatFollowEvent()
			{
				return false;
			}

			// Token: 0x0600AB33 RID: 43827 RVA: 0x003505D8 File Offset: 0x0034E7D8
			public virtual string MakeWechatFollowCode(int activityInstanceId)
			{
				return "";
			}

			// Token: 0x0600AB34 RID: 43828 RVA: 0x003505DF File Offset: 0x0034E7DF
			public virtual bool IsCouponEnabled()
			{
				return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.INTERNAL_COUPON_SYSTEM);
			}

			// Token: 0x0600AB35 RID: 43829 RVA: 0x003505ED File Offset: 0x0034E7ED
			public virtual bool IsUseSelfCouponPopup()
			{
				return !NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.INTERNAL_COUPON_SYSTEM);
			}

			// Token: 0x0600AB36 RID: 43830 RVA: 0x003505FB File Offset: 0x0034E7FB
			public virtual void OpenCoupon()
			{
				Debug.Log("OpenCoupon");
			}

			// Token: 0x0600AB37 RID: 43831 RVA: 0x00350607 File Offset: 0x0034E807
			public virtual void SendUseCouponReqToCSServer(string code)
			{
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.INTERNAL_COUPON_SYSTEM))
				{
					NKCPacketSender.Send_NKMPacket_BSIDE_COUPON_USE_REQ(code);
				}
			}

			// Token: 0x17001867 RID: 6247
			// (get) Token: 0x0600AB38 RID: 43832 RVA: 0x00350618 File Offset: 0x0034E818
			public virtual bool IsOfferwallAvailable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600AB39 RID: 43833 RVA: 0x0035061B File Offset: 0x0034E81B
			public virtual void ShowOfferWall()
			{
			}

			// Token: 0x17001868 RID: 6248
			// (get) Token: 0x0600AB3A RID: 43834 RVA: 0x0035061D File Offset: 0x0034E81D
			public virtual bool MarketReviewEnabled
			{
				get
				{
					return !this.marketReviewCompleted && !NKCDefineManager.DEFINE_UNITY_STANDALONE();
				}
			}

			// Token: 0x0600AB3B RID: 43835 RVA: 0x00350633 File Offset: 0x0034E833
			public void SetMarketReviewCompleted()
			{
				this.marketReviewCompleted = true;
			}

			// Token: 0x0600AB3C RID: 43836 RVA: 0x0035063C File Offset: 0x0034E83C
			public virtual void OpenMarketReviewPopup(string reviewLog, UnityAction onClose)
			{
				Debug.Log("MarketReviewPopup");
				this.m_reviewReason = reviewLog;
				this.dOnCloseMarketReview = onClose;
				if (NKCDefineManager.DEFINE_IOS())
				{
					this.MoveToReview();
					UnityAction unityAction = this.dOnCloseMarketReview;
					if (unityAction == null)
					{
						return;
					}
					unityAction();
					return;
				}
				else
				{
					if (NKCDefineManager.DEFINE_ANDROID())
					{
						NKCPopupReviewInduce.Instance.OpenOKCancel(delegate
						{
							this.MoveToReview();
							UnityAction unityAction2 = this.dOnCloseMarketReview;
							if (unityAction2 == null)
							{
								return;
							}
							unityAction2();
						}, delegate
						{
							UnityAction unityAction2 = this.dOnCloseMarketReview;
							if (unityAction2 == null)
							{
								return;
							}
							unityAction2();
						});
						return;
					}
					if (onClose != null)
					{
						onClose();
					}
					return;
				}
			}

			// Token: 0x0600AB3D RID: 43837 RVA: 0x003506B2 File Offset: 0x0034E8B2
			private void MoveToReview()
			{
				Debug.Log("MoveToMarket");
				if (NKCDefineManager.DEFINE_ANDROID())
				{
					Application.OpenURL("market://details?id=" + Application.identifier);
				}
				else
				{
					NKCDefineManager.DEFINE_IOS();
				}
				NKCPacketSender.Send_NKMPacket_UPDATE_MARKET_REVIEW_REQ(this.m_reviewReason);
			}

			// Token: 0x0600AB3E RID: 43838 RVA: 0x003506EC File Offset: 0x0034E8EC
			public virtual bool SnsShareEnabled(NKMUnitData unitData)
			{
				return false;
			}

			// Token: 0x0600AB3F RID: 43839 RVA: 0x003506EF File Offset: 0x0034E8EF
			public virtual bool IsUseSnsSharePopup()
			{
				return false;
			}

			// Token: 0x0600AB40 RID: 43840 RVA: 0x003506F2 File Offset: 0x0034E8F2
			public virtual bool IsOnlyUnitShare()
			{
				return true;
			}

			// Token: 0x0600AB41 RID: 43841 RVA: 0x003506F5 File Offset: 0x0034E8F5
			public virtual bool IsUseSnsShareOn10SeqContract()
			{
				return false;
			}

			// Token: 0x0600AB42 RID: 43842 RVA: 0x003506F8 File Offset: 0x0034E8F8
			public virtual bool SnsQRImageEnabled()
			{
				return true;
			}

			// Token: 0x0600AB43 RID: 43843 RVA: 0x003506FB File Offset: 0x0034E8FB
			public virtual void TrySnsShare(NKCPublisherModule.SNS_SHARE_TYPE sst, string capturePath, string thumbnailPath, NKCPublisherModule.OnComplete onComplete)
			{
				Debug.Log("TrySnsShare : " + capturePath);
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x04009FA8 RID: 40872
			protected bool marketReviewCompleted;

			// Token: 0x04009FA9 RID: 40873
			protected string m_reviewReason;

			// Token: 0x04009FAA RID: 40874
			private UnityAction dOnCloseMarketReview;
		}

		// Token: 0x02001511 RID: 5393
		public abstract class NKCPMPermission
		{
			// Token: 0x0600AB47 RID: 43847 RVA: 0x0035074C File Offset: 0x0034E94C
			public virtual void Init()
			{
			}

			// Token: 0x0600AB48 RID: 43848
			public abstract void RequestCameraPermission(NKCPublisherModule.OnComplete dOnComplete);

			// Token: 0x0600AB49 RID: 43849 RVA: 0x0035074E File Offset: 0x0034E94E
			public virtual void CheckCameraPermission()
			{
			}

			// Token: 0x0600AB4A RID: 43850 RVA: 0x00350750 File Offset: 0x0034E950
			public virtual void RequestAppTrackingPermission()
			{
			}

			// Token: 0x0600AB4B RID: 43851 RVA: 0x00350752 File Offset: 0x0034E952
			public virtual void RequestTerm(NKCPublisherModule.OnComplete dOnComplete)
			{
			}
		}

		// Token: 0x02001512 RID: 5394
		public abstract class NKCPMServerInfo
		{
			// Token: 0x0600AB4D RID: 43853 RVA: 0x0035075C File Offset: 0x0034E95C
			public virtual bool IsUsePatchConnectionInfo()
			{
				return true;
			}

			// Token: 0x0600AB4E RID: 43854 RVA: 0x0035075F File Offset: 0x0034E95F
			public virtual string GetServerConfigPath()
			{
				return "";
			}

			// Token: 0x0600AB4F RID: 43855 RVA: 0x00350766 File Offset: 0x0034E966
			public virtual bool GetUseLocalSaveLastServerInfoToGetTags()
			{
				return true;
			}
		}

		// Token: 0x02001513 RID: 5395
		public abstract class NKCPMLocalization
		{
			// Token: 0x0600AB51 RID: 43857 RVA: 0x00350771 File Offset: 0x0034E971
			public virtual void Init()
			{
			}

			// Token: 0x17001869 RID: 6249
			// (get) Token: 0x0600AB52 RID: 43858 RVA: 0x00350773 File Offset: 0x0034E973
			public virtual bool UseDefaultLanguageOnFirstRun
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600AB53 RID: 43859
			public abstract NKM_NATIONAL_CODE GetDefaultLanguage();

			// Token: 0x0600AB54 RID: 43860 RVA: 0x00350776 File Offset: 0x0034E976
			public virtual NKC_VOICE_CODE GetDefaultVoice()
			{
				return NKC_VOICE_CODE.NVC_KOR;
			}

			// Token: 0x0600AB55 RID: 43861 RVA: 0x00350779 File Offset: 0x0034E979
			public virtual void SetPublisherModuleLanguage(NKM_NATIONAL_CODE code)
			{
			}

			// Token: 0x0600AB56 RID: 43862 RVA: 0x0035077B File Offset: 0x0034E97B
			public virtual bool IsPossibleJson(string inputStr)
			{
				return false;
			}

			// Token: 0x0600AB57 RID: 43863 RVA: 0x0035077E File Offset: 0x0034E97E
			public virtual string GetTranslationIfJson(string origin)
			{
				return origin;
			}

			// Token: 0x1700186A RID: 6250
			// (get) Token: 0x0600AB58 RID: 43864 RVA: 0x00350781 File Offset: 0x0034E981
			public virtual bool UseTranslation
			{
				get
				{
					return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.USE_CHAT_TRANSLATION);
				}
			}

			// Token: 0x0600AB59 RID: 43865 RVA: 0x0035078C File Offset: 0x0034E98C
			public virtual void Translate(long chatUID, string str, NKM_NATIONAL_CODE userLanguage, NKCPublisherModule.NKCPMLocalization.TranslateCallback callback)
			{
				if (callback == null)
				{
					return;
				}
				if (this.m_requestedTranslationList == null)
				{
					return;
				}
				if (!this.m_requestedTranslationList.ContainsKey(chatUID))
				{
					this.m_requestedTranslationList.Add(chatUID, callback);
				}
				NKCPacketSender.Send_NKMPacket_GUILD_CHAT_TRANSLATE_REQ(NKCGuildManager.MyGuildData.guildUid, chatUID, NKCStringTable.GetLanguageCode(userLanguage, true));
			}

			// Token: 0x0600AB5A RID: 43866 RVA: 0x003507DC File Offset: 0x0034E9DC
			public virtual void OnTranslateCompleteFromCS_Server(long chatUID, string textTranslated)
			{
				if (this.m_requestedTranslationList == null)
				{
					return;
				}
				NKCPublisherModule.NKCPMLocalization.TranslateCallback translateCallback;
				if (this.m_requestedTranslationList.TryGetValue(chatUID, out translateCallback) && translateCallback != null)
				{
					translateCallback(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, textTranslated, chatUID, null);
				}
				this.m_requestedTranslationList.Remove(chatUID);
			}

			// Token: 0x0600AB5B RID: 43867 RVA: 0x0035081C File Offset: 0x0034EA1C
			public virtual bool IsForceSelectPrevLangWhenNoServerTags()
			{
				return false;
			}

			// Token: 0x0600AB5C RID: 43868 RVA: 0x0035081F File Offset: 0x0034EA1F
			public virtual void SetDefaultLanage(NKM_NATIONAL_CODE eNKM_NATIONAL_CODE)
			{
			}

			// Token: 0x04009FAB RID: 40875
			private Dictionary<long, NKCPublisherModule.NKCPMLocalization.TranslateCallback> m_requestedTranslationList = new Dictionary<long, NKCPublisherModule.NKCPMLocalization.TranslateCallback>();

			// Token: 0x02001A66 RID: 6758
			// (Invoke) Token: 0x0600BBCE RID: 48078
			public delegate void TranslateCallback(NKC_PUBLISHER_RESULT_CODE resultCode, string translatedString, long chatUID, string additionalError);
		}

		// Token: 0x02001514 RID: 5396
		// (Invoke) Token: 0x0600AB5F RID: 43871
		public delegate void Func(NKCPublisherModule.OnComplete onComplete);
	}
}

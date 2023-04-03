using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000697 RID: 1687
	public static class NKCLoginCutSceneManager
	{
		// Token: 0x06003779 RID: 14201 RVA: 0x0011DAF7 File Offset: 0x0011BCF7
		public static bool LoadFromLua()
		{
			NKCLoginCutSceneManager.m_dicTemplet = NKMTempletLoader.LoadDictionary<NKCLoginCutSceneTemplet>("ab_script", "LUA_LOGIN_CUTSCENE_TEMPLET", "m_LoginCutSceneTemplet", new Func<NKMLua, NKCLoginCutSceneTemplet>(NKCLoginCutSceneTemplet.LoadFromLUA));
			return true;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x0011DB20 File Offset: 0x0011BD20
		public static void Join()
		{
			foreach (NKCLoginCutSceneTemplet nkcloginCutSceneTemplet in NKCLoginCutSceneManager.m_dicTemplet.Values)
			{
				nkcloginCutSceneTemplet.Join();
			}
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x0011DB74 File Offset: 0x0011BD74
		public static void PostJoin()
		{
			foreach (NKCLoginCutSceneTemplet nkcloginCutSceneTemplet in NKCLoginCutSceneManager.m_dicTemplet.Values)
			{
				nkcloginCutSceneTemplet.PostJoin();
			}
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x0011DBC8 File Offset: 0x0011BDC8
		public static bool CheckLoginCutScene(Action endCallback)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!NKMTutorialManager.IsTutorialCompleted(TutorialStep.NicknameChange, myUserData))
			{
				return false;
			}
			NKCLoginCutSceneManager.m_EndCallback = endCallback;
			foreach (NKCLoginCutSceneTemplet nkcloginCutSceneTemplet in NKCLoginCutSceneManager.m_dicTemplet.Values)
			{
				if (NKCLoginCutSceneManager.CheckCond(myUserData, nkcloginCutSceneTemplet))
				{
					Debug.Log("LoginCutScene enabled : " + nkcloginCutSceneTemplet.m_CutSceneStrID);
					NKCUICutScenPlayer.Instance.UnLoad();
					NKCUICutScenPlayer.Instance.Load(nkcloginCutSceneTemplet.m_CutSceneStrID, true);
					NKMPopUpBox.OpenSmallWaitBox(0f, "");
					NKCLoginCutSceneManager.m_bCutsceneLoading = true;
					NKCLoginCutSceneManager.m_currentTempletKey = nkcloginCutSceneTemplet.Key;
					NKCLoginCutSceneManager.m_bPlaying = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x0011DCA0 File Offset: 0x0011BEA0
		public static bool IsPlaying()
		{
			return NKCLoginCutSceneManager.m_bCutsceneLoading || NKCLoginCutSceneManager.m_bPlaying;
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x0011DCB0 File Offset: 0x0011BEB0
		public static void Update()
		{
			if (!NKCLoginCutSceneManager.m_bCutsceneLoading)
			{
				return;
			}
			if (!NKCAssetResourceManager.IsLoadEnd())
			{
				return;
			}
			NKCResourceUtility.SwapResource();
			NKMPopUpBox.CloseWaitBox();
			NKCLoginCutSceneTemplet nkcloginCutSceneTemplet;
			if (NKCLoginCutSceneManager.m_dicTemplet.TryGetValue(NKCLoginCutSceneManager.m_currentTempletKey, out nkcloginCutSceneTemplet))
			{
				Debug.Log(string.Format("Playing cutscene {0}..", NKCLoginCutSceneManager.m_currentTempletKey));
				NKCLoginCutSceneManager.SetComplete(NKCScenManager.GetScenManager().GetMyUserData(), NKCLoginCutSceneManager.m_currentTempletKey);
				NKCUICutScenPlayer.Instance.Play(nkcloginCutSceneTemplet.m_CutSceneStrID, 0, new NKCUICutScenPlayer.CutScenCallBack(NKCLoginCutSceneManager.EndCutScene));
				NKCLoginCutSceneManager.m_bPlaying = true;
			}
			NKCLoginCutSceneManager.m_bCutsceneLoading = false;
			NKCLoginCutSceneManager.m_currentTempletKey = 0;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x0011DD48 File Offset: 0x0011BF48
		private static void EndCutScene()
		{
			NKCSoundManager.StopAllSound();
			NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
			Debug.Log(string.Format("Cutscene {0} complete..", NKCLoginCutSceneManager.m_currentTempletKey));
			NKCLoginCutSceneManager.m_bCutsceneLoading = false;
			NKCLoginCutSceneManager.m_currentTempletKey = 0;
			if (!NKCLoginCutSceneManager.CheckLoginCutScene(NKCLoginCutSceneManager.m_EndCallback))
			{
				NKCLoginCutSceneManager.m_bPlaying = false;
				if (NKCLoginCutSceneManager.m_EndCallback != null)
				{
					NKCLoginCutSceneManager.m_EndCallback();
					NKCLoginCutSceneManager.m_EndCallback = null;
				}
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x0011DDB8 File Offset: 0x0011BFB8
		private static bool IsComplete(NKMUserData userData, int templetKey)
		{
			return PlayerPrefs.GetInt(string.Format("LOGIN_CUT_SCENE_KEY_{0}_{1}", userData.m_UserUID, templetKey), 0) == 1;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0011DDDE File Offset: 0x0011BFDE
		private static void SetComplete(NKMUserData userData, int templetKey)
		{
			PlayerPrefs.SetInt(string.Format("LOGIN_CUT_SCENE_KEY_{0}_{1}", userData.m_UserUID, templetKey), 1);
			PlayerPrefs.Save();
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x0011DE08 File Offset: 0x0011C008
		private static bool CheckCond(NKMUserData userData, NKCLoginCutSceneTemplet templet)
		{
			if (userData == null || templet == null)
			{
				return false;
			}
			if (NKCLoginCutSceneManager.IsComplete(userData, templet.Key))
			{
				return false;
			}
			if (templet.HasDateLimit && !NKCSynchronizedTime.IsEventTime(templet.StartDateUTC, templet.EndDateUTC))
			{
				return false;
			}
			if (templet.m_CondType != EventUnlockCond.None)
			{
				switch (templet.m_CondType)
				{
				case EventUnlockCond.EventClear:
				{
					string condValue = templet.m_CondValue;
					TutorialStep step;
					bool flag = Enum.TryParse<TutorialStep>(condValue, out step);
					int step2;
					bool flag2 = int.TryParse(condValue, out step2);
					if (flag)
					{
						if (!NKCTutorialManager.TutorialCompleted(step))
						{
							return false;
						}
					}
					else
					{
						if (!flag2)
						{
							return false;
						}
						if (!NKCTutorialManager.TutorialCompleted((TutorialStep)step2))
						{
							return false;
						}
					}
					break;
				}
				case EventUnlockCond.DungeonClear:
					if (!userData.CheckDungeonClear(templet.m_CondValue))
					{
						return false;
					}
					break;
				case EventUnlockCond.WarfareClear:
					if (!userData.CheckWarfareClear(templet.m_CondValue))
					{
						return false;
					}
					break;
				case EventUnlockCond.PhaseClear:
					if (!NKCPhaseManager.CheckPhaseClear(NKMPhaseTemplet.Find(templet.m_CondValue)))
					{
						return false;
					}
					break;
				case EventUnlockCond.StageClear:
				{
					NKMStageTempletV2 stageTemplet = NKMStageTempletV2.Find(templet.m_CondValue);
					if (!userData.CheckStageCleared(stageTemplet))
					{
						return false;
					}
					break;
				}
				}
			}
			return true;
		}

		// Token: 0x04003430 RID: 13360
		private static Dictionary<int, NKCLoginCutSceneTemplet> m_dicTemplet = new Dictionary<int, NKCLoginCutSceneTemplet>();

		// Token: 0x04003431 RID: 13361
		private const string LOGIN_CUT_SCENE_KEY = "LOGIN_CUT_SCENE_KEY_{0}_{1}";

		// Token: 0x04003432 RID: 13362
		private static bool m_bCutsceneLoading = false;

		// Token: 0x04003433 RID: 13363
		private static int m_currentTempletKey = 0;

		// Token: 0x04003434 RID: 13364
		private static bool m_bPlaying = false;

		// Token: 0x04003435 RID: 13365
		private static Action m_EndCallback = null;
	}
}

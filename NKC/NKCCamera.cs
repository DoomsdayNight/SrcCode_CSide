using System;
using NKC.ImageEffects;
using NKM;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace NKC
{
	// Token: 0x02000647 RID: 1607
	public class NKCCamera
	{
		// Token: 0x0600320E RID: 12814 RVA: 0x000F87F4 File Offset: 0x000F69F4
		public static void SetEnableSepiaToneSubUILowCam(bool bSet)
		{
			NKCCamera.m_SepiaTone_SUB_UI_LOW_CAMERA.enabled = bSet;
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000F8801 File Offset: 0x000F6A01
		public static void SetBlackCameraEnable(bool value)
		{
			if (NKCCamera.m_SCEN_BLACK_Camera != null)
			{
				NKCUtil.SetGameobjectActive(NKCCamera.m_SCEN_BLACK_Camera.gameObject, value);
			}
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000F8820 File Offset: 0x000F6A20
		public static Camera GetCamera()
		{
			return NKCCamera.m_SCEN_MAIN_Camera_Camera;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000F8827 File Offset: 0x000F6A27
		public static Camera GetSubUILowCamera()
		{
			return NKCCamera.m_SCEN_SUB_UI_LOW_Camera_Camera;
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000F882E File Offset: 0x000F6A2E
		public static Camera GetSubUICamera()
		{
			return NKCCamera.m_SCEN_SUB_UI_Camera_Camera;
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000F8835 File Offset: 0x000F6A35
		public static NKCUIComVideoCamera GetSubUICameraVideoPlayer()
		{
			return NKCCamera.m_SCEN_SUB_UI_Camera_Video_Player;
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000F883C File Offset: 0x000F6A3C
		public static bool GetEnableBloom()
		{
			return NKCCamera.m_bBloomEnable;
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000F8843 File Offset: 0x000F6A43
		public static void EnableBloom(bool bEnable)
		{
			NKCCamera.m_bBloomEnable = bEnable;
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000F884B File Offset: 0x000F6A4B
		public static void SetBloomEnableUI(bool bEnable)
		{
			NKCCamera.m_bBloomEnableUI = bEnable;
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000F8853 File Offset: 0x000F6A53
		public static float GetCameraAspect()
		{
			return NKCCamera.m_fCameraAspect;
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000F885A File Offset: 0x000F6A5A
		public static float GetCameraSizeOrg()
		{
			return NKCCamera.m_CameraSizeOrg;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000F8861 File Offset: 0x000F6A61
		public static float GetBloomIntensityOrg()
		{
			return NKCCamera.m_fBloomIntensityOrg;
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000F8868 File Offset: 0x000F6A68
		public static float GetBloomThreshHoldOrg()
		{
			return NKCCamera.m_fBloomThreshHoldOrg;
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000F886F File Offset: 0x000F6A6F
		public static void SetBoundRectTransform(RectTransform rect)
		{
			NKCCamera.m_rectCamMoveBound = rect;
			if (rect != null)
			{
				NKCCamera.m_rectCamBound = NKCCamera.GetCameraBoundRect(NKCCamera.m_rectCamMoveBound, NKCCamera.m_CamPosTemp.z);
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000F88A4 File Offset: 0x000F6AA4
		public static void Init()
		{
			NKCCamera.m_SCEN_MAIN_Camera = GameObject.Find("SCEN_MAIN_Camera");
			NKCCamera.m_SCEN_MAIN_Camera_Camera = NKCCamera.m_SCEN_MAIN_Camera.GetComponent<Camera>();
			NKCCamera.m_SCEN_SUB_UI_LOW_Camera = GameObject.Find("SCEN_SUB_UI_LOW_Camera");
			NKCCamera.m_SCEN_SUB_UI_LOW_Camera_Camera = NKCCamera.m_SCEN_SUB_UI_LOW_Camera.GetComponent<Camera>();
			NKCCamera.m_SepiaTone_SUB_UI_LOW_CAMERA = NKCCamera.m_SCEN_SUB_UI_LOW_Camera.GetComponent<SepiaTone>();
			if (NKCCamera.m_SepiaTone_SUB_UI_LOW_CAMERA != null)
			{
				NKCCamera.m_SepiaTone_SUB_UI_LOW_CAMERA.enabled = false;
			}
			NKCCamera.m_SCEN_SUB_UI_Camera = GameObject.Find("SCEN_SUB_UI_Camera");
			NKCCamera.m_SCEN_SUB_UI_Camera_Camera = NKCCamera.m_SCEN_SUB_UI_Camera.GetComponent<Camera>();
			NKCCamera.m_SCEN_SUB_UI_Camera_Video_Player = NKCCamera.m_SCEN_SUB_UI_Camera.GetComponent<NKCUIComVideoCamera>();
			GameObject gameObject = new GameObject("SCEN_BLACK_Camera");
			gameObject.transform.parent = NKCCamera.m_SCEN_MAIN_Camera.transform.parent;
			NKCCamera.m_SCEN_BLACK_Camera = gameObject.AddComponent<Camera>();
			NKCCamera.m_SCEN_BLACK_Camera.clearFlags = CameraClearFlags.Color;
			NKCCamera.m_SCEN_BLACK_Camera.backgroundColor = Color.black;
			NKCCamera.m_SCEN_BLACK_Camera.cullingMask = 0;
			gameObject.SetActive(false);
			NKCCamera.m_SCEN_MAIN_Camera_Transform = NKCCamera.m_SCEN_MAIN_Camera.GetComponent<Transform>();
			NKCCamera.m_SCEN_MAIN_Camera_Bloom = NKCCamera.m_SCEN_MAIN_Camera.GetComponent<NKCBloom>();
			if (NKCCamera.m_SCEN_MAIN_Camera_Bloom != null)
			{
				NKCCamera.m_bBloomEnableNow = NKCCamera.m_SCEN_MAIN_Camera_Bloom.enabled;
				NKCCamera.m_bBloomEnable = NKCCamera.m_bBloomEnableNow;
				NKCCamera.m_fBloomIntensityOrg = NKCCamera.m_SCEN_MAIN_Camera_Bloom.bloomIntensity;
				NKCCamera.m_fBloomThreshHoldOrg = NKCCamera.m_SCEN_MAIN_Camera_Bloom.bloomThreshold;
			}
			NKCCamera.m_CoolMotionBlur = NKCCamera.m_SCEN_MAIN_Camera.GetComponent<CoolMotionBlur>();
			NKCCamera.m_FocusMat = NKCCamera.m_CoolMotionBlur.ScreenMat;
			NKCCamera.DisableFocusBlur();
			NKCCamera.m_BlurOptimized = NKCCamera.m_SCEN_MAIN_Camera.GetComponent<BlurOptimized>();
			if (NKCCamera.m_BlurOptimized != null)
			{
				NKCCamera.m_BlurOptimized.enabled = false;
			}
			NKCCamera.m_ScreenWater = NKCCamera.m_SCEN_MAIN_Camera.GetComponent<ScreenWater>();
			if (NKCCamera.m_ScreenWater != null)
			{
				NKCCamera.m_ScreenWater.enabled = false;
			}
			NKCCamera.m_tracScreenWaterSpeed.SetNowValue(0f);
			NKCCamera.m_tracScreenWaterIntens.SetNowValue(0f);
			NKCCamera.m_fCameraAspect = NKCCamera.m_SCEN_MAIN_Camera_Camera.aspect;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000F8A9B File Offset: 0x000F6C9B
		public static void SetCameraRect(Rect rect)
		{
			NKCCamera.m_SCEN_MAIN_Camera_Camera.rect = rect;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000F8AA8 File Offset: 0x000F6CA8
		public static void InitBattle(float fCamMinX, float fCamMaxX, float fCamMinY, float fCamMaxY, float fCamSize, float fCamSizeMax)
		{
			NKCCamera.m_BattleCam = true;
			NKCCamera.m_CameraMinX = fCamMinX;
			NKCCamera.m_CameraMaxX = fCamMaxX;
			NKCCamera.m_fCamMinYGap = fCamMinY;
			NKCCamera.m_fCamMaxYGap = fCamMaxY;
			NKCCamera.m_CameraSizeOrg = fCamSize;
			NKCCamera.m_CameraSizeMax = fCamSizeMax;
			NKCCamera.m_NKMTrackingZoom.SetNowValue(NKCCamera.m_CameraSizeOrg);
			NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_SCEN_MAIN_Camera_Transform.position.x, NKCCamera.m_SCEN_MAIN_Camera_Transform.position.y, NKCCamera.m_SCEN_MAIN_Camera_Transform.position.z);
			NKCCamera.m_fCameraAspect = NKCCamera.m_SCEN_MAIN_Camera_Camera.aspect;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000F8B36 File Offset: 0x000F6D36
		public static void BattleEnd()
		{
			NKCCamera.m_BattleCam = false;
			NKCCamera.m_SCEN_MAIN_Camera_Camera.targetTexture = null;
			NKCCamera.m_SCEN_SUB_UI_Camera_Camera.targetTexture = null;
			NKCCamera.InitCrashCamera();
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000F8B5C File Offset: 0x000F6D5C
		public static void BloomProcess()
		{
			if (NKCScenManager.GetScenManager().GetSystemMemorySize() < 3000)
			{
				NKCCamera.m_bBloomEnableNow = false;
				return;
			}
			if (NKCCamera.m_bBloomEnable)
			{
				if (NKCCamera.m_BattleCam)
				{
					if (NKCCamera.m_bBloomEnableNow)
					{
						NKCCamera.m_bBloomEnableNow = false;
						if (NKCCamera.m_SCEN_MAIN_Camera_Bloom != null)
						{
							NKCCamera.m_SCEN_MAIN_Camera_Bloom.enabled = NKCCamera.m_bBloomEnableNow;
							return;
						}
					}
				}
				else if (NKCCamera.m_bBloomEnableNow != NKCCamera.m_bBloomEnableUI)
				{
					NKCCamera.m_bBloomEnableNow = NKCCamera.m_bBloomEnableUI;
					if (NKCCamera.m_SCEN_MAIN_Camera_Bloom != null)
					{
						NKCCamera.m_SCEN_MAIN_Camera_Bloom.enabled = NKCCamera.m_bBloomEnableNow;
						return;
					}
				}
			}
			else if (NKCCamera.m_bBloomEnableNow)
			{
				NKCCamera.m_bBloomEnableNow = false;
				if (NKCCamera.m_SCEN_MAIN_Camera_Bloom != null)
				{
					NKCCamera.m_SCEN_MAIN_Camera_Bloom.enabled = NKCCamera.m_bBloomEnableNow;
				}
			}
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000F8C18 File Offset: 0x000F6E18
		private static void UpdateCrashBasic()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				float num = 0f;
				if (gameOptionData.CameraShakeLevel != NKCGameOptionDataSt.GameOptionCameraShake.None)
				{
					num += NKCCamera.m_CamCrashUp.m_fCrashGap;
					num += NKCCamera.m_CamCrashDown.m_fCrashGap;
					if (NKCCamera.m_CamCrashUpDown.m_bCrashPositive)
					{
						num += NKCCamera.m_CamCrashUpDown.m_fCrashGap;
					}
					else
					{
						num -= NKCCamera.m_CamCrashUpDown.m_fCrashGap;
					}
					if (gameOptionData.CameraShakeLevel == NKCGameOptionDataSt.GameOptionCameraShake.Low)
					{
						num *= 0.5f;
					}
					NKCCamera.m_CamPosTemp.y = NKCCamera.m_CamPosTemp.y + num;
				}
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000F8CA4 File Offset: 0x000F6EA4
		public static void Update(float deltaTime)
		{
			NKCCamera.m_fDeltaTime = deltaTime;
			if (NKCCamera.m_fFocusTime > 0f)
			{
				NKCCamera.m_fFocusTime -= NKCCamera.m_fDeltaTime;
				if (NKCCamera.m_fFocusTime <= 0f)
				{
					NKCCamera.m_fFocusTime = 0f;
					NKCCamera.m_FocusFactor.SetTracking(0f, 0.3f, TRACKING_DATA_TYPE.TDT_NORMAL);
				}
			}
			else if (NKCCamera.m_FocusFactor.GetNowValue() <= 0f && NKCCamera.m_CoolMotionBlur.isActiveAndEnabled)
			{
				NKCCamera.DisableFocusBlur();
			}
			NKCCamera.m_FocusFactor.Update(NKCCamera.m_fDeltaTime);
			NKCCamera.m_FocusMat.SetFloat("_SampleDist", NKCCamera.m_FocusFactor.GetNowValue());
			NKCCamera.m_NKMTrackingPos.Update(NKCCamera.m_fDeltaTime);
			NKCCamera.m_NKMTrackingZoom.Update(NKCCamera.m_fDeltaTime);
			NKCCamera.m_NKMTrackingRotation.Update(NKCCamera.m_fDeltaTime);
			NKCCamera.m_tracScreenWaterSpeed.Update(NKCCamera.m_fDeltaTime);
			NKCCamera.m_tracScreenWaterIntens.Update(NKCCamera.m_fDeltaTime);
			if (NKCCamera.m_ScreenWater != null && NKCCamera.m_tracScreenWaterSpeed.IsTracking())
			{
				NKCCamera.m_ScreenWater.Speed = NKCCamera.m_tracScreenWaterSpeed.GetNowValue();
			}
			if (NKCCamera.m_ScreenWater != null && NKCCamera.m_tracScreenWaterIntens.IsTracking())
			{
				NKCCamera.m_ScreenWater.Intens = NKCCamera.m_tracScreenWaterIntens.GetNowValue();
			}
			NKCCamera.CamCrashProcessUp();
			NKCCamera.CamCrashProcessDown();
			NKCCamera.CamCrashProcessUpDown();
			NKCCamera.m_CamPosTemp.x = NKCCamera.m_NKMTrackingPos.GetNowValueX();
			NKCCamera.m_CamPosTemp.y = NKCCamera.m_NKMTrackingPos.GetNowValueY();
			NKCCamera.m_CamPosTemp.z = NKCCamera.m_NKMTrackingPos.GetNowValueZ();
			if (NKCCamera.m_BattleCam)
			{
				float cameraSizeNow = NKCCamera.GetCameraSizeNow();
				NKCCamera.m_SCEN_MAIN_Camera_Camera.orthographicSize = cameraSizeNow;
				float num = cameraSizeNow * NKCCamera.m_fCameraAspect * 0.5f;
				if (num * 2f > NKCCamera.m_CameraMaxX - NKCCamera.m_CameraMinX)
				{
					NKCCamera.m_CamPosTemp.x = (NKCCamera.m_CameraMinX + NKCCamera.m_CameraMaxX) * 0.5f;
					NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_CamPosTemp.x, NKCCamera.m_NKMTrackingPos.GetNowValueY(), NKCCamera.m_NKMTrackingPos.GetNowValueZ());
				}
				else if (NKCCamera.m_CamPosTemp.x < NKCCamera.m_CameraMinX + num)
				{
					NKCCamera.m_CamPosTemp.x = NKCCamera.m_CameraMinX + num;
					NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_CamPosTemp.x, NKCCamera.m_NKMTrackingPos.GetNowValueY(), NKCCamera.m_NKMTrackingPos.GetNowValueZ());
				}
				else if (NKCCamera.m_CamPosTemp.x > NKCCamera.m_CameraMaxX - num)
				{
					NKCCamera.m_CamPosTemp.x = NKCCamera.m_CameraMaxX - num;
					NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_CamPosTemp.x, NKCCamera.m_NKMTrackingPos.GetNowValueY(), NKCCamera.m_NKMTrackingPos.GetNowValueZ());
				}
				num = NKCCamera.m_CameraSizeOrg - cameraSizeNow;
				if (NKCCamera.m_CamPosTemp.y > num + NKCCamera.m_fCamMaxYGap)
				{
					NKCCamera.m_CamPosTemp.y = num + NKCCamera.m_fCamMaxYGap;
				}
				else if (NKCCamera.m_CamPosTemp.y < -num + NKCCamera.m_fCamMinYGap)
				{
					NKCCamera.m_CamPosTemp.y = -num + NKCCamera.m_fCamMinYGap;
				}
				NKCCamera.UpdateCrashBasic();
				num = NKCCamera.m_CameraSizeMax - cameraSizeNow;
				if (NKCCamera.m_CamPosTemp.y > num + NKCCamera.m_fCamMaxYGap)
				{
					NKCCamera.m_CamPosTemp.y = num + NKCCamera.m_fCamMaxYGap;
				}
				else if (NKCCamera.m_CamPosTemp.y < -num + NKCCamera.m_fCamMinYGap)
				{
					NKCCamera.m_CamPosTemp.y = -num + NKCCamera.m_fCamMinYGap;
				}
			}
			else
			{
				NKCCamera.UpdateCrashBasic();
			}
			NKCCamera.BloomProcess();
			if (NKCCamera.m_rectCamMoveBound != null)
			{
				if (NKCCamera.m_CamPosTemp.z != NKCCamera.m_SCEN_MAIN_Camera_Transform.position.z)
				{
					NKCCamera.m_rectCamBound = NKCCamera.GetCameraBoundRect(NKCCamera.m_rectCamMoveBound, NKCCamera.m_CamPosTemp.z);
				}
				NKCCamera.m_CamPosTemp.x = Mathf.Clamp(NKCCamera.m_CamPosTemp.x, NKCCamera.m_rectCamBound.xMin, NKCCamera.m_rectCamBound.xMax);
				NKCCamera.m_CamPosTemp.y = Mathf.Clamp(NKCCamera.m_CamPosTemp.y, NKCCamera.m_rectCamBound.yMin, NKCCamera.m_rectCamBound.yMax);
			}
			NKCCamera.m_SCEN_MAIN_Camera_Transform.position = NKCCamera.m_CamPosTemp;
			NKCCamera.m_SCEN_MAIN_Camera_Transform.rotation = Quaternion.Euler(NKCCamera.m_NKMTrackingRotation.GetNowValueX(), NKCCamera.m_NKMTrackingRotation.GetNowValueY(), NKCCamera.m_NKMTrackingRotation.GetNowValueZ());
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000F90EC File Offset: 0x000F72EC
		public static void EnableBlur(bool bEnable, float blurSize = 2f, int blurIteration = 2)
		{
			NKCCamera.m_bBlur = bEnable;
			if (NKCCamera.m_BlurOptimized != null)
			{
				NKCCamera.m_BlurOptimized.blurSize = blurSize;
				NKCCamera.m_BlurOptimized.blurIterations = blurIteration;
				NKCCamera.m_BlurOptimized.enabled = NKCCamera.m_bBlur;
			}
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000F9128 File Offset: 0x000F7328
		public static void EnableScreenWater(bool bEnable, float fSpeed, float fIntens, float fTime)
		{
			if (NKCCamera.m_ScreenWater != null && bEnable != NKCCamera.m_ScreenWater.enabled)
			{
				NKCCamera.m_ScreenWater.enabled = bEnable;
			}
			NKCCamera.m_tracScreenWaterSpeed.SetTracking(fSpeed, fTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			NKCCamera.m_tracScreenWaterIntens.SetTracking(fIntens, fTime, TRACKING_DATA_TYPE.TDT_SLOWER);
			if (!bEnable)
			{
				NKCCamera.m_tracScreenWaterSpeed.SetNowValue(0f);
				NKCCamera.m_tracScreenWaterIntens.SetNowValue(0f);
				if (NKCCamera.m_ScreenWater != null)
				{
					NKCCamera.m_ScreenWater.Speed = NKCCamera.m_tracScreenWaterSpeed.GetNowValue();
					NKCCamera.m_ScreenWater.Intens = NKCCamera.m_tracScreenWaterIntens.GetNowValue();
				}
			}
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000F91CA File Offset: 0x000F73CA
		public static void DisableFocusBlur()
		{
			NKCCamera.m_CoolMotionBlur.enabled = false;
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000F91D8 File Offset: 0x000F73D8
		public static void SetFocusBlur(float fFocusTime, float fCenterPosX = 0.5f, float fCenterPosY = 0.5f, float fCenterPosZ = 0f)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.UseGameEffect)
			{
				return;
			}
			NKCCamera.m_CoolMotionBlur.enabled = true;
			if (NKCCamera.m_fFocusTime <= 0f)
			{
				NKCCamera.m_FocusFactor.SetTracking(0.1f, 0.3f, TRACKING_DATA_TYPE.TDT_NORMAL);
			}
			NKCCamera.m_fFocusTime = fFocusTime;
			NKCCamera.m_Vec3Temp.Set(fCenterPosX, fCenterPosY, fCenterPosZ);
			NKCCamera.m_Vec3Temp = NKCCamera.m_SCEN_MAIN_Camera_Camera.WorldToScreenPoint(NKCCamera.m_Vec3Temp);
			NKCCamera.m_Vec3Temp.x = NKCCamera.m_Vec3Temp.x / (float)Screen.width;
			NKCCamera.m_Vec3Temp.y = NKCCamera.m_Vec3Temp.y / (float)Screen.height;
			if (NKCCamera.m_Vec3Temp.x < 0f || NKCCamera.m_Vec3Temp.x > 1f)
			{
				NKCCamera.m_Vec3Temp.x = 0.5f;
			}
			if (NKCCamera.m_Vec3Temp.y < 0f || NKCCamera.m_Vec3Temp.y > 1f)
			{
				NKCCamera.m_Vec3Temp.y = 0.5f;
			}
			NKCCamera.m_Vec3Temp.z = 0f;
			NKCCamera.m_FocusMat.SetVector("_Center", NKCCamera.m_Vec3Temp);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000F930F File Offset: 0x000F750F
		public static void SetBloomIntensity(float fIntensity)
		{
			if (NKCCamera.m_SCEN_MAIN_Camera_Bloom != null)
			{
				NKCCamera.m_SCEN_MAIN_Camera_Bloom.bloomIntensity = fIntensity;
			}
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000F9329 File Offset: 0x000F7529
		public static void SetBloomThreshHold(float fThreshHold)
		{
			if (NKCCamera.m_SCEN_MAIN_Camera_Bloom != null)
			{
				NKCCamera.m_SCEN_MAIN_Camera_Bloom.bloomThreshold = fThreshHold;
			}
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000F9343 File Offset: 0x000F7543
		public static float GetPosNowX(bool bUseEdge = false)
		{
			if (!bUseEdge)
			{
				return NKCCamera.m_NKMTrackingPos.GetNowValue().x;
			}
			return NKCCamera.m_SCEN_MAIN_Camera_Transform.position.x;
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000F9367 File Offset: 0x000F7567
		public static float GetPosNowY(bool bUseEdge = false)
		{
			if (!bUseEdge)
			{
				return NKCCamera.m_NKMTrackingPos.GetNowValue().y;
			}
			return NKCCamera.m_SCEN_MAIN_Camera_Transform.position.y;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000F938B File Offset: 0x000F758B
		public static float GetPosNowZ(bool bUseEdge = false)
		{
			if (!bUseEdge)
			{
				return NKCCamera.m_NKMTrackingPos.GetNowValue().z;
			}
			return NKCCamera.m_SCEN_MAIN_Camera_Transform.position.z;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000F93AF File Offset: 0x000F75AF
		public static NKMTrackingVector3 GetTrackingPos()
		{
			return NKCCamera.m_NKMTrackingPos;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000F93B6 File Offset: 0x000F75B6
		public static NKMTrackingVector3 GetTrackingRotation()
		{
			return NKCCamera.m_NKMTrackingRotation;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000F93BD File Offset: 0x000F75BD
		public static bool IsTrackingCamera()
		{
			return NKCCamera.m_NKMTrackingPos.IsTracking() || NKCCamera.m_NKMTrackingZoom.IsTracking();
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000F93DA File Offset: 0x000F75DA
		public static bool IsTrackingCameraPos()
		{
			return NKCCamera.m_NKMTrackingPos.IsTracking();
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000F93EB File Offset: 0x000F75EB
		public static bool IsTrackingWater()
		{
			return NKCCamera.m_tracScreenWaterSpeed.IsTracking() || NKCCamera.m_tracScreenWaterIntens.IsTracking();
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000F9408 File Offset: 0x000F7608
		public static void StopTrackingCamera()
		{
			NKCCamera.m_NKMTrackingPos.StopTracking();
			NKCCamera.m_NKMTrackingZoom.SetTracking(NKCCamera.m_CameraSizeOrg, 0.1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			NKCCamera.m_NKMTrackingRotation.StopTracking();
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000F9433 File Offset: 0x000F7633
		public static void SetZoom(float fZoomSize, bool bTrackingStop)
		{
			if (bTrackingStop)
			{
				NKCCamera.m_NKMTrackingZoom.StopTracking();
			}
			NKCCamera.m_NKMTrackingZoom.SetNowValue(fZoomSize);
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000F944D File Offset: 0x000F764D
		public static float GetZoomRate()
		{
			return NKCCamera.m_NKMTrackingZoom.GetNowValue() / NKCCamera.m_CameraSizeOrg;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000F9460 File Offset: 0x000F7660
		public static float GetCameraSizeNow()
		{
			float num = NKCCamera.m_NKMTrackingZoom.GetNowValue();
			if (num > NKCCamera.m_CameraSizeMax)
			{
				num = NKCCamera.m_CameraSizeMax;
			}
			return num;
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000F9488 File Offset: 0x000F7688
		public static float GetScreenRatio(bool bSafeRect)
		{
			if (bSafeRect)
			{
				return Screen.safeArea.width / Screen.safeArea.height;
			}
			return (float)Screen.width / (float)Screen.height;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000F94C4 File Offset: 0x000F76C4
		public static void SetPos(Vector3 pos, bool bTrackingStop, bool bForce = false)
		{
			if (bTrackingStop)
			{
				NKCCamera.m_NKMTrackingPos.StopTracking();
			}
			if (!NKCCamera.m_NKMTrackingPos.IsTracking())
			{
				NKCCamera.m_NKMTrackingPos.SetNowValue(pos.x, pos.y, pos.z);
			}
			if (bForce)
			{
				NKCCamera.m_CamPosTemp.x = NKCCamera.m_NKMTrackingPos.GetNowValueX();
				NKCCamera.m_CamPosTemp.y = NKCCamera.m_NKMTrackingPos.GetNowValueY();
				NKCCamera.m_CamPosTemp.z = NKCCamera.m_NKMTrackingPos.GetNowValueZ();
				NKCCamera.m_SCEN_MAIN_Camera_Transform.position = NKCCamera.m_CamPosTemp;
			}
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000F9554 File Offset: 0x000F7754
		public static void SetPos(float x = -1f, float y = -1f, float z = -1f, bool bTrackingStop = true, bool bForce = false)
		{
			if (bTrackingStop)
			{
				NKCCamera.m_NKMTrackingPos.StopTracking();
			}
			if (!NKCCamera.m_NKMTrackingPos.IsTracking())
			{
				if (x != -1f)
				{
					NKCCamera.m_CamPosTemp.x = x;
				}
				else
				{
					NKCCamera.m_CamPosTemp.x = NKCCamera.m_NKMTrackingPos.GetNowValue().x;
				}
				if (y != -1f)
				{
					NKCCamera.m_CamPosTemp.y = y;
				}
				else
				{
					NKCCamera.m_CamPosTemp.y = NKCCamera.m_NKMTrackingPos.GetNowValue().y;
				}
				if (z != -1f)
				{
					NKCCamera.m_CamPosTemp.z = z;
				}
				else
				{
					NKCCamera.m_CamPosTemp.z = NKCCamera.m_NKMTrackingPos.GetNowValue().z;
				}
				NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_CamPosTemp.x, NKCCamera.m_CamPosTemp.y, NKCCamera.m_CamPosTemp.z);
			}
			if (bForce)
			{
				NKCCamera.m_CamPosTemp.x = NKCCamera.m_NKMTrackingPos.GetNowValueX();
				NKCCamera.m_CamPosTemp.y = NKCCamera.m_NKMTrackingPos.GetNowValueY();
				NKCCamera.m_CamPosTemp.z = NKCCamera.m_NKMTrackingPos.GetNowValueZ();
				NKCCamera.m_SCEN_MAIN_Camera_Transform.position = NKCCamera.m_CamPosTemp;
			}
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000F9680 File Offset: 0x000F7880
		public static void SetPosRel(Vector3 pos, bool bTrackingStop)
		{
			if (bTrackingStop)
			{
				NKCCamera.m_NKMTrackingPos.StopTracking();
			}
			if (!NKCCamera.m_NKMTrackingPos.IsTracking())
			{
				NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_NKMTrackingPos.GetNowValueX() + pos.x, NKCCamera.m_NKMTrackingPos.GetNowValueY() + pos.y, NKCCamera.m_NKMTrackingPos.GetNowValueZ() + pos.z);
			}
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000F96E4 File Offset: 0x000F78E4
		public static void SetPosRel(Vector2 pos, bool bTrackingStop = true)
		{
			if (bTrackingStop)
			{
				NKCCamera.m_NKMTrackingPos.StopTracking();
			}
			if (!NKCCamera.m_NKMTrackingPos.IsTracking())
			{
				NKCCamera.m_CamPosTemp.Set(pos.x, pos.y, NKCCamera.m_SCEN_MAIN_Camera_Transform.position.z);
				NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_SCEN_MAIN_Camera_Transform.position.x + NKCCamera.m_CamPosTemp.x, NKCCamera.m_SCEN_MAIN_Camera_Transform.position.y + NKCCamera.m_CamPosTemp.y, NKCCamera.m_SCEN_MAIN_Camera_Transform.position.z + NKCCamera.m_CamPosTemp.z);
			}
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000F9788 File Offset: 0x000F7988
		public static void SetPosRel(float x, float y, float z, bool bTrackingStop = true)
		{
			if (bTrackingStop)
			{
				NKCCamera.m_NKMTrackingPos.StopTracking();
			}
			if (!NKCCamera.m_NKMTrackingPos.IsTracking())
			{
				NKCCamera.m_CamPosTemp.x = x;
				NKCCamera.m_CamPosTemp.y = y;
				NKCCamera.m_CamPosTemp.z = z;
				NKCCamera.m_NKMTrackingPos.SetNowValue(NKCCamera.m_NKMTrackingPos.GetNowValueX() + NKCCamera.m_CamPosTemp.x, NKCCamera.m_NKMTrackingPos.GetNowValueY() + NKCCamera.m_CamPosTemp.y, NKCCamera.m_NKMTrackingPos.GetNowValueZ() + NKCCamera.m_CamPosTemp.z);
			}
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000F9818 File Offset: 0x000F7A18
		public static float GetDist(NKMUnit cNKMUnit)
		{
			return Mathf.Abs(NKCCamera.m_NKMTrackingPos.GetNowValue().x - cNKMUnit.GetUnitSyncData().m_PosX);
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000F983A File Offset: 0x000F7A3A
		public static float GetDist(NKMDamageEffect cNKMDamageEffect)
		{
			return Mathf.Abs(NKCCamera.m_NKMTrackingPos.GetNowValue().x - cNKMDamageEffect.GetDEData().m_PosX);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000F985C File Offset: 0x000F7A5C
		public static void CamCrashProcessUp()
		{
			if (NKCCamera.m_CamCrashUp.m_fCrashGap >= 0f && NKCCamera.m_CamCrashUp.m_fCrashAccel != 0f)
			{
				NKCCamera.m_CamCrashUp.m_fCrashGap += NKCCamera.m_CamCrashUp.m_fCrashSpeed * NKCCamera.m_fDeltaTime;
				NKCCamera.m_CamCrashUp.m_fCrashSpeed += NKCCamera.m_CamCrashUp.m_fCrashAccel * NKCCamera.m_fDeltaTime;
				return;
			}
			NKCCamera.m_CamCrashUp.m_fCrashGap = 0f;
			NKCCamera.m_CamCrashUp.m_fCrashSpeed = 0f;
			NKCCamera.m_CamCrashUp.m_fCrashAccel = 0f;
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000F98FC File Offset: 0x000F7AFC
		public static void CamCrashProcessDown()
		{
			if (NKCCamera.m_CamCrashDown.m_fCrashGap <= 0f && NKCCamera.m_CamCrashDown.m_fCrashAccel != 0f)
			{
				NKCCamera.m_CamCrashDown.m_fCrashGap += NKCCamera.m_CamCrashDown.m_fCrashSpeed * NKCCamera.m_fDeltaTime;
				NKCCamera.m_CamCrashDown.m_fCrashSpeed += NKCCamera.m_CamCrashDown.m_fCrashAccel * NKCCamera.m_fDeltaTime;
				return;
			}
			NKCCamera.m_CamCrashDown.m_fCrashGap = 0f;
			NKCCamera.m_CamCrashDown.m_fCrashSpeed = 0f;
			NKCCamera.m_CamCrashDown.m_fCrashAccel = 0f;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000F999C File Offset: 0x000F7B9C
		public static void CamCrashProcessUpDown()
		{
			if (NKCCamera.m_CamCrashUpDown.m_fCrashTime > 0f)
			{
				NKCCamera.m_CamCrashUpDown.m_fCrashTime -= NKCCamera.m_fDeltaTime;
				NKCCamera.m_CamCrashUpDown.m_fCrashTimeGap -= NKCCamera.m_fDeltaTime;
				if (NKCCamera.m_CamCrashUpDown.m_fCrashTimeGap <= 0f)
				{
					NKCCamera.m_CamCrashUpDown.m_fCrashTimeGap = NKCCamera.m_CamCrashUpDown.m_fCrashTimeGapOrg;
					NKCCamera.m_CamCrashUpDown.m_bCrashPositive = !NKCCamera.m_CamCrashUpDown.m_bCrashPositive;
					return;
				}
			}
			else
			{
				NKCCamera.m_CamCrashUpDown.m_fCrashTime = 0f;
				NKCCamera.m_CamCrashUpDown.m_fCrashGap = 0f;
			}
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000F9A41 File Offset: 0x000F7C41
		public static void UpCrashCamera(float fSpeed = 100f, float fAccel = -1500f)
		{
			NKCCamera.m_CamCrashUp.m_fCrashGap = 0f;
			NKCCamera.m_CamCrashUp.m_fCrashSpeed = fSpeed;
			NKCCamera.m_CamCrashUp.m_fCrashAccel = fAccel;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000F9A68 File Offset: 0x000F7C68
		public static void DownCrashCamera(float fSpeed = -100f, float fAccel = 1500f)
		{
			NKCCamera.m_CamCrashDown.m_fCrashGap = 0f;
			NKCCamera.m_CamCrashDown.m_fCrashSpeed = fSpeed;
			NKCCamera.m_CamCrashDown.m_fCrashAccel = fAccel;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000F9A90 File Offset: 0x000F7C90
		public static void UpDownCrashCamera(float fGap = 10f, float fTime = 0.2f, float fCrashTimeGapOrg = 0.05f)
		{
			if (NKCCamera.m_CamCrashUpDown.m_fCrashTime > 0f && fGap < NKCCamera.m_CamCrashUpDown.m_fCrashGap)
			{
				return;
			}
			NKCCamera.m_CamCrashUpDown.m_fCrashGap = fGap;
			NKCCamera.m_CamCrashUpDown.m_fCrashTime = fTime;
			NKCCamera.m_CamCrashUpDown.m_fCrashTimeGap = fCrashTimeGapOrg;
			NKCCamera.m_CamCrashUpDown.m_fCrashTimeGapOrg = fCrashTimeGapOrg;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000F9AE8 File Offset: 0x000F7CE8
		public static void TurnOffCrashUpDown()
		{
			NKCCamera.m_CamCrashUpDown.m_fCrashGap = 0f;
			NKCCamera.m_CamCrashUpDown.m_fCrashTime = 0f;
			NKCCamera.m_CamCrashUpDown.m_fCrashTimeGap = 0f;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000F9B17 File Offset: 0x000F7D17
		public static void UpDownCrashCameraNoReset(float fGap = 10f, float fTime = 0.2f)
		{
			NKCCamera.m_CamCrashUpDown.m_fCrashGap = fGap;
			NKCCamera.m_CamCrashUpDown.m_fCrashTime = fTime;
			NKCCamera.m_CamCrashUpDown.m_fCrashTimeGapOrg = 0.05f;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000F9B40 File Offset: 0x000F7D40
		private static void InitCrashCamera()
		{
			NKCCamera.m_CamCrashDown.m_fCrashGap = 0f;
			NKCCamera.m_CamCrashDown.m_fCrashSpeed = 0f;
			NKCCamera.m_CamCrashDown.m_fCrashAccel = 0f;
			NKCCamera.m_CamCrashUp.m_fCrashGap = 0f;
			NKCCamera.m_CamCrashUp.m_fCrashSpeed = 0f;
			NKCCamera.m_CamCrashUp.m_fCrashAccel = 0f;
			NKCCamera.m_CamCrashUpDown.m_fCrashTime = 0f;
			NKCCamera.m_CamCrashUpDown.m_fCrashGap = 0f;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000F9BC8 File Offset: 0x000F7DC8
		public static void TrackingPos(float fTrackingTime, float fX = -1f, float fY = -1f, float fZ = -1f)
		{
			if (fX == -1f)
			{
				fX = NKCCamera.m_NKMTrackingPos.GetNowValue().x;
			}
			if (fY == -1f)
			{
				fY = NKCCamera.m_NKMTrackingPos.GetNowValue().y;
			}
			if (fZ == -1f)
			{
				fZ = NKCCamera.m_NKMTrackingPos.GetNowValue().z;
			}
			NKCCamera.m_NKMTrackingPos.SetTracking(fX, fY, fZ, fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000F9C30 File Offset: 0x000F7E30
		public static void TrackingPosRel(float fTrackingTime, float fX, float fY, float fZ)
		{
			fX = NKCCamera.m_NKMTrackingPos.GetNowValue().x + fX;
			fY = NKCCamera.m_NKMTrackingPos.GetNowValue().y + fY;
			fZ = NKCCamera.m_NKMTrackingPos.GetNowValue().z + fZ;
			NKCCamera.m_NKMTrackingPos.SetTracking(fX, fY, fZ, fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000F9C85 File Offset: 0x000F7E85
		public static void TrackingZoom(float fTrackingTime, float fZoomSize)
		{
			NKCCamera.m_NKMTrackingZoom.SetTracking(fZoomSize, fTrackingTime, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000F9C94 File Offset: 0x000F7E94
		public static void GetScreenPosToWorldPos(out Vector3 worldPos, float fScreenX, float fScreenY)
		{
			NKCCamera.m_CamPosTemp.Set(fScreenX, fScreenY, NKCCamera.m_SCEN_MAIN_Camera_Camera.nearClipPlane);
			worldPos = NKCCamera.m_SCEN_MAIN_Camera_Camera.ScreenToWorldPoint(NKCCamera.m_CamPosTemp);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000F9CC1 File Offset: 0x000F7EC1
		public static void GetWorldPosToScreenPos(out Vector3 screenPos, float fWorldX, float fWorldY, float fWorldZ)
		{
			NKCCamera.m_CamPosTemp.Set(fWorldX, fWorldY, fWorldZ);
			screenPos = NKCCamera.m_SCEN_SUB_UI_Camera_Camera.ScreenToWorldPoint(NKCCamera.m_SCEN_MAIN_Camera_Camera.WorldToScreenPoint(NKCCamera.m_CamPosTemp));
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000F9CF0 File Offset: 0x000F7EF0
		public static Rect GetCameraBoundRect(RectTransform rect, float CameraZPos)
		{
			Vector3 vector;
			Rect worldRect = NKCCamera.GetWorldRect(rect, out vector);
			Vector3 v = new Vector3(vector.x, vector.y, CameraZPos);
			Rect result = default(Rect);
			if (NKCCamera.m_SCEN_MAIN_Camera_Camera.orthographic)
			{
				result.height = worldRect.height - NKCCamera.m_SCEN_MAIN_Camera_Camera.orthographicSize * 2f;
				float num = NKCCamera.GetScreenRatio(false) * NKCCamera.m_SCEN_MAIN_Camera_Camera.orthographicSize * 2f;
				result.width = worldRect.width - num;
			}
			else
			{
				float num2 = Mathf.Abs(vector.z - CameraZPos);
				float num3 = Mathf.Tan(0.017453292f * NKCCamera.m_SCEN_MAIN_Camera_Camera.fieldOfView * 0.5f) * num2 * 2f;
				float num4 = NKCCamera.GetScreenRatio(false) * num3;
				result.height = worldRect.height - num3;
				result.width = worldRect.width - num4;
			}
			if (result.height < 0f)
			{
				result.height = 0f;
			}
			if (result.width < 0f)
			{
				result.width = 0f;
			}
			result.center = v;
			return result;
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000F9E20 File Offset: 0x000F8020
		public static void FitCameraToRect(Camera camera, RectTransform rect)
		{
			Vector3[] array = new Vector3[4];
			rect.GetWorldCorners(array);
			NKCCamera.FitCameraToWorldRect(camera, array);
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000F9E44 File Offset: 0x000F8044
		public static void FitCameraToWorldRect(Camera camera, Vector3[] WorldCornerPosArray)
		{
			Vector3 vector = WorldCornerPosArray[1];
			Vector3 vector2 = WorldCornerPosArray[2];
			Vector3 vector3 = WorldCornerPosArray[3];
			Vector3 vector4 = (vector + vector3) * 0.5f;
			if (camera.orthographic)
			{
				camera.transform.position = new Vector3(vector4.x, vector4.y, Mathf.Min(vector.z, vector3.z) - 10f);
				float num = vector2.x - vector.x;
				float num2 = vector2.y - vector3.y;
				camera.orthographicSize = (vector2.y - vector3.y) / 2f;
				camera.aspect = num / num2;
				return;
			}
			Vector3 normalized = Vector3.Cross(vector - vector4, vector2 - vector4).normalized;
			float d = (vector.y - vector4.y) / Mathf.Tan(0.017453292f * camera.fieldOfView * 0.5f);
			Vector3 position = vector4 + normalized * d;
			float num3 = vector2.x - vector.x;
			float num4 = vector2.y - vector3.y;
			camera.aspect = num3 / num4;
			camera.transform.position = position;
			camera.transform.LookAt(vector4);
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000F9F94 File Offset: 0x000F8194
		public static void FitRectToCamera(RectTransform rect)
		{
			float screenRatio = NKCCamera.GetScreenRatio(false);
			if (screenRatio > 1f)
			{
				rect.SetWidth(screenRatio * rect.GetHeight());
			}
			Vector3[] array = new Vector3[4];
			rect.GetWorldCorners(array);
			Vector3 vector = array[0];
			float d = (array[1].y - vector.y) * 0.5f / Mathf.Tan(0.017453292f * NKCCamera.m_SCEN_MAIN_Camera_Camera.fieldOfView * 0.5f);
			rect.position = NKCCamera.m_SCEN_MAIN_Camera_Camera.transform.position + NKCCamera.m_SCEN_MAIN_Camera_Camera.transform.forward * d;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000FA03C File Offset: 0x000F823C
		public static Rect GetCameraRectOnRectTransform(RectTransform rect, Camera Camera, float CameraWorldZPos)
		{
			Rect result = default(Rect);
			if (rect == null)
			{
				return result;
			}
			Vector3 vector;
			NKCCamera.GetWorldRect(rect, out vector);
			if (Camera.orthographic)
			{
				result.height = Camera.orthographicSize * 2f;
				result.width = NKCCamera.GetScreenRatio(false) * result.height;
				result.center = vector;
			}
			else
			{
				float num = Mathf.Abs(vector.z - CameraWorldZPos);
				float num2 = Mathf.Tan(0.017453292f * Camera.fieldOfView * 0.5f) * num * 2f;
				float width = NKCCamera.GetScreenRatio(false) * num2;
				result.width = width;
				result.height = num2;
				result.center = vector;
			}
			return result;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000FA0FC File Offset: 0x000F82FC
		private static Rect GetWorldRect(RectTransform rect, out Vector3 CenterWorldPos)
		{
			if (rect == null)
			{
				CenterWorldPos = Vector3.zero;
				return default(Rect);
			}
			Vector3[] array = new Vector3[4];
			rect.GetWorldCorners(array);
			Vector3 vector = array[1];
			Vector3 vector2 = array[2];
			Vector3 vector3 = array[3];
			CenterWorldPos = (vector + vector3) * 0.5f;
			float width = Mathf.Abs(vector2.x - vector.x);
			float height = Mathf.Abs(vector2.y - vector3.y);
			return new Rect
			{
				width = width,
				height = height,
				center = CenterWorldPos
			};
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000FA1B8 File Offset: 0x000F83B8
		public static void RescaleRectToCameraFrustrum(RectTransform rect, Camera targetCamera, Vector2 CameraMoveRectSize, float farthestZPosition, NKCCamera.FitMode fitMode = NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode scaleMode = NKCCamera.ScaleMode.Scale)
		{
			if (rect == null)
			{
				return;
			}
			Vector3 vector;
			Rect worldRect = NKCCamera.GetWorldRect(rect, out vector);
			Rect cameraRectOnRectTransform = NKCCamera.GetCameraRectOnRectTransform(rect, targetCamera, farthestZPosition);
			Vector2 vector2 = default(Vector2);
			vector2.x = cameraRectOnRectTransform.width + CameraMoveRectSize.x;
			vector2.y = cameraRectOnRectTransform.height + CameraMoveRectSize.y;
			Vector2 vector3 = default(Vector2);
			switch (fitMode)
			{
			case NKCCamera.FitMode.FitToScreen:
				vector3.x = vector2.x;
				vector3.y = vector2.y;
				goto IL_1DC;
			case NKCCamera.FitMode.FitToWidth:
			{
				float num = worldRect.width / worldRect.height;
				vector3.x = vector2.x;
				vector3.y = vector3.x / num;
				goto IL_1DC;
			}
			case NKCCamera.FitMode.FitToHeight:
			{
				float num2 = worldRect.width / worldRect.height;
				vector3.y = vector2.y;
				vector3.x = vector3.y * num2;
				goto IL_1DC;
			}
			case NKCCamera.FitMode.FitIn:
			{
				float num3 = worldRect.width / worldRect.height;
				if (worldRect.width / vector2.x > worldRect.height / vector2.y)
				{
					vector3.x = vector2.x;
					vector3.y = vector3.x / num3;
					goto IL_1DC;
				}
				vector3.y = vector2.y;
				vector3.x = vector3.y * num3;
				goto IL_1DC;
			}
			}
			float num4 = worldRect.width / worldRect.height;
			if (worldRect.width / vector2.x > worldRect.height / vector2.y)
			{
				vector3.y = vector2.y;
				vector3.x = vector3.y * num4;
			}
			else
			{
				vector3.x = vector2.x;
				vector3.y = vector3.x / num4;
			}
			IL_1DC:
			if (scaleMode == NKCCamera.ScaleMode.Scale)
			{
				rect.localScale = new Vector3
				{
					x = vector3.x / rect.GetWidth(),
					y = vector3.y / rect.GetHeight(),
					z = rect.localScale.z
				};
				return;
			}
			if (scaleMode != NKCCamera.ScaleMode.RectSize)
			{
				return;
			}
			rect.SetSize(vector3);
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000FA400 File Offset: 0x000F8600
		public static Vector3 CameraPositionOfFitRectHeight(RectTransform rect, Camera targetCamera)
		{
			float num = rect.GetHeight() * rect.lossyScale.y;
			float num2 = Mathf.Tan(targetCamera.fieldOfView * 0.5f * 0.017453292f);
			float f = rect.eulerAngles.x * 0.017453292f;
			float num3 = -0.5f * num * Mathf.Cos(f) / num2;
			float num4 = -0.5f * num * Mathf.Sin(f) * num2;
			return new Vector3(rect.position.x, rect.position.y + num4, rect.position.z + num3);
		}

		// Token: 0x04003116 RID: 12566
		private static GameObject m_SCEN_MAIN_Camera = null;

		// Token: 0x04003117 RID: 12567
		private static Camera m_SCEN_MAIN_Camera_Camera = null;

		// Token: 0x04003118 RID: 12568
		private static GameObject m_SCEN_SUB_UI_LOW_Camera = null;

		// Token: 0x04003119 RID: 12569
		private static Camera m_SCEN_SUB_UI_LOW_Camera_Camera = null;

		// Token: 0x0400311A RID: 12570
		private static SepiaTone m_SepiaTone_SUB_UI_LOW_CAMERA = null;

		// Token: 0x0400311B RID: 12571
		private static GameObject m_SCEN_SUB_UI_Camera = null;

		// Token: 0x0400311C RID: 12572
		private static Camera m_SCEN_SUB_UI_Camera_Camera = null;

		// Token: 0x0400311D RID: 12573
		private static NKCUIComVideoCamera m_SCEN_SUB_UI_Camera_Video_Player = null;

		// Token: 0x0400311E RID: 12574
		private static Camera m_SCEN_BLACK_Camera = null;

		// Token: 0x0400311F RID: 12575
		private static Transform m_SCEN_MAIN_Camera_Transform = null;

		// Token: 0x04003120 RID: 12576
		private static bool m_bBloomEnable;

		// Token: 0x04003121 RID: 12577
		private static bool m_bBloomEnableNow;

		// Token: 0x04003122 RID: 12578
		private static bool m_bBloomEnableUI;

		// Token: 0x04003123 RID: 12579
		private static NKCBloom m_SCEN_MAIN_Camera_Bloom = null;

		// Token: 0x04003124 RID: 12580
		private static NKMTrackingVector3 m_NKMTrackingPos = new NKMTrackingVector3();

		// Token: 0x04003125 RID: 12581
		private static NKMTrackingVector3 m_NKMTrackingRotation = new NKMTrackingVector3();

		// Token: 0x04003126 RID: 12582
		private static NKMTrackingFloat m_NKMTrackingZoom = new NKMTrackingFloat();

		// Token: 0x04003127 RID: 12583
		private static float m_fCameraAspect;

		// Token: 0x04003128 RID: 12584
		private static float m_CameraMinX = 0f;

		// Token: 0x04003129 RID: 12585
		private static float m_CameraMaxX = 0f;

		// Token: 0x0400312A RID: 12586
		private static float m_fCamMinYGap = 0f;

		// Token: 0x0400312B RID: 12587
		private static float m_fCamMaxYGap = 0f;

		// Token: 0x0400312C RID: 12588
		private static float m_CameraSizeOrg = 0f;

		// Token: 0x0400312D RID: 12589
		private static float m_CameraSizeMax = 512f;

		// Token: 0x0400312E RID: 12590
		private static CamCrashControl m_CamCrashUp = new CamCrashControl();

		// Token: 0x0400312F RID: 12591
		private static CamCrashControl m_CamCrashDown = new CamCrashControl();

		// Token: 0x04003130 RID: 12592
		private static CamCrashControl m_CamCrashUpDown = new CamCrashControl();

		// Token: 0x04003131 RID: 12593
		private static float m_fDeltaTime = 0f;

		// Token: 0x04003132 RID: 12594
		private static Vector3 m_CamPosTemp = default(Vector3);

		// Token: 0x04003133 RID: 12595
		private static bool m_BattleCam = false;

		// Token: 0x04003134 RID: 12596
		private static float m_fBloomIntensityOrg = 0.7f;

		// Token: 0x04003135 RID: 12597
		private static float m_fBloomThreshHoldOrg = 0.5f;

		// Token: 0x04003136 RID: 12598
		private static CoolMotionBlur m_CoolMotionBlur = null;

		// Token: 0x04003137 RID: 12599
		private static Material m_FocusMat = null;

		// Token: 0x04003138 RID: 12600
		private static NKMTrackingFloat m_FocusFactor = new NKMTrackingFloat();

		// Token: 0x04003139 RID: 12601
		private static float m_fFocusTime = 0f;

		// Token: 0x0400313A RID: 12602
		private static BlurOptimized m_BlurOptimized = null;

		// Token: 0x0400313B RID: 12603
		public static bool m_bBlur = false;

		// Token: 0x0400313C RID: 12604
		private static ScreenWater m_ScreenWater = null;

		// Token: 0x0400313D RID: 12605
		private static NKMTrackingFloat m_tracScreenWaterSpeed = new NKMTrackingFloat();

		// Token: 0x0400313E RID: 12606
		private static NKMTrackingFloat m_tracScreenWaterIntens = new NKMTrackingFloat();

		// Token: 0x0400313F RID: 12607
		private static Vector3 m_Vec3Temp = default(Vector3);

		// Token: 0x04003140 RID: 12608
		private static RectTransform m_rectCamMoveBound;

		// Token: 0x04003141 RID: 12609
		private static Rect m_rectCamBound;

		// Token: 0x020012EE RID: 4846
		public enum FitMode
		{
			// Token: 0x04009791 RID: 38801
			FitToScreen,
			// Token: 0x04009792 RID: 38802
			FitAuto,
			// Token: 0x04009793 RID: 38803
			FitToWidth,
			// Token: 0x04009794 RID: 38804
			FitToHeight,
			// Token: 0x04009795 RID: 38805
			FitIn
		}

		// Token: 0x020012EF RID: 4847
		public enum ScaleMode
		{
			// Token: 0x04009797 RID: 38807
			Scale,
			// Token: 0x04009798 RID: 38808
			RectSize
		}
	}
}

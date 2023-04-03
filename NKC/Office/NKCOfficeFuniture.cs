using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.Office
{
	// Token: 0x02000837 RID: 2103
	public class NKCOfficeFuniture : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, INKCAnimationActor
	{
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06005394 RID: 21396 RVA: 0x00197685 File Offset: 0x00195885
		public long UID
		{
			get
			{
				return this.m_uid;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06005395 RID: 21397 RVA: 0x0019768D File Offset: 0x0019588D
		public NKMOfficeInteriorTemplet Templet
		{
			get
			{
				return this.m_Templet;
			}
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00197698 File Offset: 0x00195898
		public static NKCOfficeFuniture GetInstance(long uid, NKMOfficeInteriorTemplet templet, float tileSize, bool bInvert, Transform parent = null, bool bShowTile = false)
		{
			NKCOfficeFuniture instance = NKCOfficeFuniture.GetInstance(uid, templet, tileSize, parent);
			if (instance == null)
			{
				return null;
			}
			instance.SetShowTile(bShowTile);
			instance.SetInvert(bInvert, false);
			instance.InvalidateWorldRect();
			return instance;
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x001976D4 File Offset: 0x001958D4
		public static NKCOfficeFuniture GetInstance(long uid, NKMOfficeInteriorTemplet templet, float tileSize, Transform parent = null)
		{
			if (templet == null)
			{
				return null;
			}
			NKMAssetName nkmassetName;
			if (templet.IsTexture)
			{
				nkmassetName = new NKMAssetName("ab_ui_office", "FNC_FLAT_BASE");
			}
			else
			{
				nkmassetName = NKMAssetName.ParseBundleName(templet.PrefabName, templet.PrefabName);
			}
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(nkmassetName, false, parent);
			if (((nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant : null) == null)
			{
				Debug.LogError(string.Format("NKCUIOfficeFuniture : {0} not found!", nkmassetName));
				return null;
			}
			NKCOfficeFuniture component = nkcassetInstanceData.m_Instant.GetComponent<NKCOfficeFuniture>();
			component.m_instanceData = nkcassetInstanceData;
			component.Init();
			component.m_uid = uid;
			component.m_id = templet.m_ItemMiscID;
			component.SetData(templet, tileSize);
			return component;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x00197774 File Offset: 0x00195974
		public virtual void Init()
		{
			if (this.m_rtFuniture != null)
			{
				this.m_imgFuniture = this.m_rtFuniture.GetComponent<Image>();
				if (this.m_imgFuniture != null && this.m_imgFuniture.mainTexture != null && this.m_imgFuniture.mainTexture.isReadable)
				{
					this.m_imgFuniture.alphaHitTestMinimumThreshold = 0.5f;
				}
			}
			if (this.m_rtInverse != null)
			{
				this.m_imgInverse = this.m_rtInverse.GetComponent<Image>();
				if (this.m_imgInverse != null && this.m_imgInverse.mainTexture != null && this.m_imgInverse.mainTexture.isReadable)
				{
					this.m_imgInverse.alphaHitTestMinimumThreshold = 0.5f;
				}
			}
			if (this.m_aImgFurnitureSub != null)
			{
				foreach (Image image in this.m_aImgFurnitureSub)
				{
					if (image.mainTexture != null && image.mainTexture.isReadable)
					{
						image.alphaHitTestMinimumThreshold = 0.5f;
					}
				}
			}
			if (this.m_rtFloor != null)
			{
				this.m_imgFloor = this.m_rtFloor.GetComponent<Image>();
				if (this.m_imgFloor != null && this.m_imgFloor.mainTexture != null && this.m_imgFloor.mainTexture.isReadable)
				{
					this.m_imgFloor.alphaHitTestMinimumThreshold = 0.5f;
				}
			}
			this.m_originalScale = this.m_rtFuniture.localScale;
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x00197900 File Offset: 0x00195B00
		public void CleanUp()
		{
			if (this.m_mask != null)
			{
				UnityEngine.Object.Destroy(this.m_mask);
				this.m_rtFuniture.localPosition = Vector3.zero;
			}
			if (this.InteractingCharacter != null)
			{
				this.InteractingCharacter.UnregisterInteraction();
			}
			this.SetColor(Color.white);
			this.dOnDragFuniture = null;
			NKCAssetResourceManager.CloseInstance(this.m_instanceData);
			this.m_instanceData = null;
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00197974 File Offset: 0x00195B74
		public void SetShowTile(bool value)
		{
			if (this.m_rtFloor == null)
			{
				return;
			}
			Image component = this.m_rtFloor.GetComponent<Image>();
			if (component != null)
			{
				if (this.m_mask != null)
				{
					component.color = Color.white;
					return;
				}
				component.color = (value ? Color.white : new Color(1f, 1f, 1f, 0f));
			}
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x001979E8 File Offset: 0x00195BE8
		public void SetFunitureBoxRaycast(bool value)
		{
			if (this.m_imgFuniture != null)
			{
				this.m_imgFuniture.raycastTarget = value;
			}
			if (this.m_imgInverse != null)
			{
				this.m_imgInverse.raycastTarget = value;
			}
			if (this.m_aImgFurnitureSub != null)
			{
				Image[] aImgFurnitureSub = this.m_aImgFurnitureSub;
				for (int i = 0; i < aImgFurnitureSub.Length; i++)
				{
					aImgFurnitureSub[i].raycastTarget = value;
				}
			}
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x00197A50 File Offset: 0x00195C50
		public virtual void SetData(NKMOfficeInteriorTemplet templet, float tileSize)
		{
			this.m_Templet = templet;
			if (!templet.IsTexture && templet.InteriorCategory == InteriorCategory.FURNITURE && (this.m_sizeX != templet.CellX || this.m_sizeY != templet.CellY))
			{
				Debug.LogError(templet.DebugName + " / " + base.gameObject.name + " : Templet 상의 가구 사이즈와 실제 프리팹 사이즈가 다름!");
			}
			this.SetTileSize(templet.CellX, templet.CellY, tileSize);
			this.SetFunitureBoxRaycast(templet.Target == InteriorTarget.Floor);
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x00197AD8 File Offset: 0x00195CD8
		public void SetTileSize(int x, int y, float tileSize)
		{
			this.m_fTileSize = tileSize;
			this.m_rtFloor.SetSize(new Vector2((float)x * tileSize, (float)y * tileSize));
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x00197AF9 File Offset: 0x00195CF9
		public bool GetInvert()
		{
			return this.m_bInvert;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x00197B04 File Offset: 0x00195D04
		public virtual void SetInvert(bool bInvert, bool bEditMode = false)
		{
			this.m_bInvert = bInvert;
			if (this.m_rtInverse != null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtInverse, bInvert);
				NKCUtil.SetGameobjectActive(this.m_rtFuniture, !bInvert);
				InteriorTarget eFunitureType = this.m_eFunitureType;
				if (eFunitureType > InteriorTarget.Tile)
				{
					if (eFunitureType != InteriorTarget.Wall)
					{
						return;
					}
					if (bInvert)
					{
						if (bEditMode)
						{
							this.m_rtFloor.rotation = Quaternion.Euler(-16.377f, 47.477f, -17.091f);
						}
						else
						{
							this.m_rtFloor.localRotation = Quaternion.identity;
						}
						this.m_rtInverse.rotation = Quaternion.identity;
						Vector3 eulerAngles = this.m_rtInverse.rotation.eulerAngles;
						this.m_rtInverse.rotation = Quaternion.Euler(eulerAngles.x, -eulerAngles.y, eulerAngles.z);
						return;
					}
					if (bEditMode)
					{
						this.m_rtFloor.rotation = Quaternion.Euler(-16.377f, -47.477f, 17.091f);
						this.m_rtFuniture.localScale = new Vector3(this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.z);
					}
					else
					{
						this.m_rtFloor.localRotation = Quaternion.identity;
						this.m_rtFuniture.localScale = this.m_originalScale;
					}
					this.m_rtFuniture.rotation = Quaternion.identity;
					return;
				}
				else
				{
					if (bInvert)
					{
						if (bEditMode)
						{
							this.m_rtFloor.rotation = Quaternion.Euler(66.5f, 0f, -45f);
						}
						else
						{
							this.m_rtFloor.localRotation = Quaternion.Euler(0f, 0f, -90f);
						}
						this.m_rtInverse.rotation = Quaternion.identity;
						Vector3 eulerAngles2 = this.m_rtInverse.rotation.eulerAngles;
						this.m_rtInverse.rotation = Quaternion.Euler(eulerAngles2.x, -eulerAngles2.y, -eulerAngles2.z);
						return;
					}
					if (bEditMode)
					{
						this.m_rtFloor.rotation = Quaternion.Euler(66.5f, 0f, 45f);
						this.m_rtFuniture.localScale = new Vector3(this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.z);
					}
					else
					{
						this.m_rtFloor.localRotation = Quaternion.identity;
						this.m_rtFuniture.localScale = this.m_originalScale;
					}
					this.m_rtFuniture.rotation = Quaternion.identity;
					return;
				}
			}
			else
			{
				InteriorTarget eFunitureType = this.m_eFunitureType;
				if (eFunitureType > InteriorTarget.Tile)
				{
					if (eFunitureType != InteriorTarget.Wall)
					{
						return;
					}
					if (bInvert)
					{
						if (bEditMode)
						{
							this.m_rtFloor.rotation = Quaternion.Euler(-16.377f, 47.477f, -17.091f);
							this.m_rtFuniture.localScale = new Vector3(-this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.z);
						}
						else
						{
							this.m_rtFloor.localRotation = Quaternion.identity;
							this.m_rtFuniture.localScale = new Vector3(-this.m_originalScale.x, this.m_originalScale.y, this.m_originalScale.z);
						}
						this.m_rtFuniture.rotation = Quaternion.identity;
						Vector3 eulerAngles3 = this.m_rtFuniture.rotation.eulerAngles;
						this.m_rtFuniture.rotation = Quaternion.Euler(eulerAngles3.x, -eulerAngles3.y, eulerAngles3.z);
						return;
					}
					if (bEditMode)
					{
						this.m_rtFloor.rotation = Quaternion.Euler(-16.377f, -47.477f, 17.091f);
						this.m_rtFuniture.localScale = new Vector3(this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.z);
					}
					else
					{
						this.m_rtFloor.localRotation = Quaternion.identity;
						this.m_rtFuniture.localScale = this.m_originalScale;
					}
					this.m_rtFuniture.rotation = Quaternion.identity;
					return;
				}
				else
				{
					if (bInvert)
					{
						if (bEditMode)
						{
							this.m_rtFloor.rotation = Quaternion.Euler(66.5f, 0f, -45f);
							this.m_rtFuniture.localScale = new Vector3(-this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.z);
						}
						else
						{
							this.m_rtFloor.localRotation = Quaternion.Euler(0f, 0f, -90f);
							this.m_rtFuniture.localScale = new Vector3(-this.m_originalScale.x, this.m_originalScale.y, this.m_originalScale.z);
						}
						this.m_rtFuniture.rotation = Quaternion.identity;
						Vector3 eulerAngles4 = this.m_rtFuniture.rotation.eulerAngles;
						this.m_rtFuniture.rotation = Quaternion.Euler(eulerAngles4.x, -eulerAngles4.y, -eulerAngles4.z);
						return;
					}
					if (bEditMode)
					{
						this.m_rtFloor.rotation = Quaternion.Euler(66.5f, 0f, 45f);
						this.m_rtFuniture.localScale = new Vector3(this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.y, this.m_rtFuniture.localScale.z);
					}
					else
					{
						this.m_rtFloor.localRotation = Quaternion.identity;
						this.m_rtFuniture.localScale = this.m_originalScale;
					}
					this.m_rtFuniture.rotation = Quaternion.identity;
					return;
				}
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x001980D4 File Offset: 0x001962D4
		public virtual void Resize(NKMOfficeInteriorTemplet templet, int overflowX, int overflowY)
		{
			if (overflowX == 0 && overflowY == 0)
			{
				if (this.m_mask != null)
				{
					UnityEngine.Object.Destroy(this.m_mask);
					this.m_mask = null;
				}
			}
			else
			{
				if (this.m_mask == null)
				{
					this.m_mask = this.m_rtFloor.gameObject.AddComponent<Mask>();
					this.m_mask.showMaskGraphic = false;
				}
				if (this.m_imgFloor != null)
				{
					this.m_imgFloor.color = Color.white;
				}
			}
			this.m_rtFuniture.localPosition = new Vector3((float)overflowX * this.m_fTileSize, (float)overflowY * this.m_fTileSize, 0f);
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x00198180 File Offset: 0x00196380
		public virtual void SetAlpha(float value)
		{
			if (this.m_imgFuniture != null)
			{
				this.m_imgFuniture.DOKill(false);
				this.m_imgFuniture.color = new Color(1f, 1f, 1f, value);
			}
			if (this.m_imgInverse != null)
			{
				this.m_imgInverse.DOKill(false);
				this.m_imgInverse.color = new Color(1f, 1f, 1f, value);
			}
			if (this.m_aImgFurnitureSub != null)
			{
				foreach (Image image in this.m_aImgFurnitureSub)
				{
					image.DOKill(false);
					image.color = new Color(1f, 1f, 1f, value);
				}
			}
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00198248 File Offset: 0x00196448
		public virtual void SetColor(Color color)
		{
			if (this.m_imgFuniture != null)
			{
				this.m_imgFuniture.DOKill(false);
				this.m_imgFuniture.color = color;
			}
			if (this.m_imgInverse != null)
			{
				this.m_imgInverse.DOKill(false);
				this.m_imgInverse.color = color;
			}
			if (this.m_aImgFurnitureSub != null)
			{
				foreach (Image image in this.m_aImgFurnitureSub)
				{
					image.DOKill(false);
					image.color = color;
				}
			}
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x001982D4 File Offset: 0x001964D4
		public virtual void SetGlow(Color color, float time)
		{
			if (this.m_imgFuniture != null)
			{
				this.m_imgFuniture.DOKill(false);
				this.m_imgFuniture.color = Color.white;
				this.m_imgFuniture.DOColor(color, time).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
			}
			if (this.m_imgInverse != null)
			{
				this.m_imgInverse.DOKill(false);
				this.m_imgInverse.color = Color.white;
				this.m_imgInverse.DOColor(color, time).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
			}
			if (this.m_aImgFurnitureSub != null)
			{
				foreach (Image image in this.m_aImgFurnitureSub)
				{
					image.DOKill(false);
					image.color = Color.white;
					image.DOColor(color, time).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
				}
			}
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x001983B5 File Offset: 0x001965B5
		public virtual void SetHighlight(bool value)
		{
			if (value)
			{
				this.SetGlow(Color.green, 1f);
				return;
			}
			this.SetColor(Color.white);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x001983D6 File Offset: 0x001965D6
		public virtual void InvalidateWorldRect()
		{
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x001983D8 File Offset: 0x001965D8
		public virtual Rect GetWorldRect(bool bFurnitureOnly)
		{
			Rect rect = this.GetFurnitureRect();
			if (!bFurnitureOnly)
			{
				Rect worldRect = this.m_rtFloor.GetWorldRect();
				rect = rect.Union(worldRect);
			}
			if (this.InteractingCharacter != null && this.InteractingCharacter.PlayingInteractionAnimation)
			{
				Rect worldRect2 = this.InteractingCharacter.GetWorldRect();
				rect = rect.Union(worldRect2);
			}
			return rect;
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x00198433 File Offset: 0x00196633
		protected virtual Rect GetFurnitureRect()
		{
			if (this.m_bInvert && this.m_rtInverse != null)
			{
				return this.m_rtInverse.GetWorldRect();
			}
			return this.m_rtFuniture.GetWorldRect();
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00198462 File Offset: 0x00196662
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.m_bDragged = true;
			NKCOfficeFuniture.OnFunitureDragEvent onFunitureDragEvent = this.dOnBeginDragFuniture;
			if (onFunitureDragEvent == null)
			{
				return;
			}
			onFunitureDragEvent(eventData);
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x0019847C File Offset: 0x0019667C
		public void OnDrag(PointerEventData eventData)
		{
			NKCOfficeFuniture.OnFunitureDragEvent onFunitureDragEvent = this.dOnDragFuniture;
			if (onFunitureDragEvent == null)
			{
				return;
			}
			onFunitureDragEvent(eventData);
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x0019848F File Offset: 0x0019668F
		public void OnEndDrag(PointerEventData eventData)
		{
			this.m_bDragged = false;
			NKCOfficeFuniture.OnFunitureDragEvent onFunitureDragEvent = this.dOnEndDragFuniture;
			if (onFunitureDragEvent == null)
			{
				return;
			}
			onFunitureDragEvent(eventData);
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x001984A9 File Offset: 0x001966A9
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (!this.m_bDragged)
			{
				NKCOfficeFuniture.OnClickFuniture onClickFuniture = this.dOnClickFuniture;
				if (onClickFuniture == null)
				{
					return;
				}
				onClickFuniture(this.m_id, this.m_uid);
			}
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x001984D0 File Offset: 0x001966D0
		public virtual void OnTouchReact()
		{
			Debug.Log(string.Format("Futniture Touch : {0}(id {1} / uid {2})", base.gameObject.name, this.m_id, this.m_uid));
			if (!string.IsNullOrEmpty(this.m_Templet.TouchSound))
			{
				this.PlaySound();
			}
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x00198525 File Offset: 0x00196725
		protected void PlaySound()
		{
			if (this.m_soundUID != 0)
			{
				NKCSoundManager.StopSound(this.m_soundUID);
			}
			if (this.soundCoroutine != null)
			{
				base.StopCoroutine(this.soundCoroutine);
			}
			this.soundCoroutine = base.StartCoroutine(this.SoundProcess());
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x00198560 File Offset: 0x00196760
		private IEnumerator SoundProcess()
		{
			if (this.m_Templet == null)
			{
				yield break;
			}
			if (this.m_Templet.SoundDelay > 0f)
			{
				yield return new WaitForSeconds(this.m_Templet.SoundDelay);
			}
			this.m_soundUID = NKCSoundManager.PlaySound(this.m_Templet.TouchSound, 1f, 0f, 0f, false, 0f, false, 0f);
			this.soundCoroutine = null;
			yield break;
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x060053AF RID: 21423 RVA: 0x0019856F File Offset: 0x0019676F
		// (set) Token: 0x060053B0 RID: 21424 RVA: 0x00198577 File Offset: 0x00196777
		public NKCOfficeCharacter InteractingCharacter { get; private set; }

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x00198580 File Offset: 0x00196780
		public bool IsInteractionOngoing
		{
			get
			{
				return this.InteractingCharacter != null;
			}
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x0019858E File Offset: 0x0019678E
		public bool HasInteractionTarget()
		{
			return this.InteractingCharacter != null;
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x0019859C File Offset: 0x0019679C
		public void RegisterInteractionCharacter(NKCOfficeCharacter character)
		{
			this.InteractingCharacter = character;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x001985A5 File Offset: 0x001967A5
		public void CleanupInteraction()
		{
			this.InteractingCharacter = null;
			this.CleanupAnimEvent();
			this.InvalidateWorldRect();
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x001985BC File Offset: 0x001967BC
		public void PlayAnimationEvent(string animEventName)
		{
			if (string.IsNullOrEmpty(animEventName))
			{
				return;
			}
			List<NKCAnimationEventTemplet> list = NKCAnimationEventManager.Find(animEventName);
			if (list == null || list.Count == 0)
			{
				return;
			}
			this.m_animEventInstance = new NKCAnimationInstance(this, base.transform, list, base.transform.localPosition, base.transform.localPosition);
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0019860E File Offset: 0x0019680E
		public GameObject GetInteractionPoint()
		{
			if (this.m_objInteractionInvertPos != null && this.m_bInvert)
			{
				return this.m_objInteractionInvertPos;
			}
			return this.m_objInteractionPos;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x00198633 File Offset: 0x00196833
		public virtual void CleanupAnimEvent()
		{
			if (this.m_animEventInstance != null)
			{
				this.m_animEventInstance.RemoveEffect();
				this.m_animEventInstance = null;
			}
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x0019864F File Offset: 0x0019684F
		public void InvokeTouchEvent()
		{
			NKCOfficeFuniture.OnClickFuniture onClickFuniture = this.dOnClickFuniture;
			if (onClickFuniture == null)
			{
				return;
			}
			onClickFuniture(this.m_id, this.m_uid);
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x0019866D File Offset: 0x0019686D
		public virtual RectTransform MakeHighlightRect()
		{
			if (this.m_rtFuniture.gameObject.activeInHierarchy)
			{
				return this.m_rtFuniture;
			}
			return this.m_rtInverse;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x00198690 File Offset: 0x00196890
		public ValueTuple<Vector3, Vector3> GetHorizonalLine(float extend = 0f)
		{
			Vector3[] array = new Vector3[4];
			this.m_rtFloor.GetWorldCorners(array);
			float num = Mathf.Abs(array[0].x - array[2].x);
			float num2 = Mathf.Abs(array[1].x - array[3].x);
			ValueTuple<Vector3, Vector3> valueTuple;
			if (num > num2)
			{
				if (array[0].x < array[2].x)
				{
					valueTuple = new ValueTuple<Vector3, Vector3>(array[0], array[2]);
				}
				else
				{
					valueTuple = new ValueTuple<Vector3, Vector3>(array[2], array[0]);
				}
			}
			else if (array[1].x < array[3].x)
			{
				valueTuple = new ValueTuple<Vector3, Vector3>(array[1], array[3]);
			}
			else
			{
				valueTuple = new ValueTuple<Vector3, Vector3>(array[3], array[1]);
			}
			Vector3 b = (valueTuple.Item2 - valueTuple.Item1).normalized * extend;
			return new ValueTuple<Vector3, Vector3>(valueTuple.Item1 - b, valueTuple.Item2 + b);
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x001987BC File Offset: 0x001969BC
		public ValueTuple<float, float> GetZMinMax()
		{
			Vector3[] array = new Vector3[4];
			this.m_rtFloor.GetWorldCorners(array);
			float item = Mathf.Min(new float[]
			{
				array[0].z,
				array[1].z,
				array[2].z,
				array[3].z
			});
			float item2 = Mathf.Max(new float[]
			{
				array[0].z,
				array[1].z,
				array[2].z,
				array[3].z
			});
			return new ValueTuple<float, float>(item, item2);
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00198874 File Offset: 0x00196A74
		public void GetWorldInfo(out float zMin, out float zMax, out Vector3 xMinPos, out Vector3 xMaxPos)
		{
			Vector3[] array = new Vector3[4];
			this.m_rtFloor.GetWorldCorners(array);
			zMin = Mathf.Min(new float[]
			{
				array[0].z,
				array[1].z,
				array[2].z,
				array[3].z
			});
			zMax = Mathf.Max(new float[]
			{
				array[0].z,
				array[1].z,
				array[2].z,
				array[3].z
			});
			float num = Mathf.Abs(array[0].x - array[2].x);
			float num2 = Mathf.Abs(array[1].x - array[3].x);
			if (num > num2)
			{
				if (array[0].x < array[2].x)
				{
					xMinPos = array[0];
					xMaxPos = array[2];
					return;
				}
				xMinPos = array[2];
				xMaxPos = array[0];
				return;
			}
			else
			{
				if (array[1].x < array[3].x)
				{
					xMinPos = array[1];
					xMaxPos = array[3];
					return;
				}
				xMinPos = array[3];
				xMaxPos = array[1];
				return;
			}
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x00198A09 File Offset: 0x00196C09
		protected virtual void Update()
		{
			if (this.m_animEventInstance != null)
			{
				if (this.m_animEventInstance.IsFinished())
				{
					this.CleanupAnimEvent();
					return;
				}
				this.m_animEventInstance.Update(Time.deltaTime);
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x00198A37 File Offset: 0x00196C37
		Animator INKCAnimationActor.Animator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x00198A3A File Offset: 0x00196C3A
		Transform INKCAnimationActor.SDParent
		{
			get
			{
				return this.m_rtFloor;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060053C0 RID: 21440 RVA: 0x00198A42 File Offset: 0x00196C42
		Transform INKCAnimationActor.Transform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00198A4A File Offset: 0x00196C4A
		public virtual Vector3 GetBonePosition(string name)
		{
			return base.transform.position;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00198A57 File Offset: 0x00196C57
		public void PlayEmotion(string animName, float speed)
		{
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00198A59 File Offset: 0x00196C59
		public virtual void PlaySpineAnimation(string name, bool loop, float timeScale)
		{
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00198A5B File Offset: 0x00196C5B
		public virtual void PlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, float timeScale, bool bDefaultAnim)
		{
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x00198A5D File Offset: 0x00196C5D
		public virtual bool IsSpineAnimationFinished(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return true;
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00198A60 File Offset: 0x00196C60
		public virtual bool IsSpineAnimationFinished(string name)
		{
			return true;
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00198A63 File Offset: 0x00196C63
		public virtual bool CanPlaySpineAnimation(string name)
		{
			return false;
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x00198A66 File Offset: 0x00196C66
		public virtual bool CanPlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return false;
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00198A69 File Offset: 0x00196C69
		public void UpdateInteractionPos(RectTransform floorRect)
		{
			this.ProjectPointToPlane(this.m_objInteractionPos, floorRect);
			this.ProjectPointToPlane(this.m_objInteractionInvertPos, floorRect);
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x00198A85 File Offset: 0x00196C85
		private void ProjectPointToPlane(GameObject point, RectTransform plane)
		{
			if (point == null)
			{
				return;
			}
			point.transform.position = plane.ProjectPointToPlaneWorldPos(point.transform.position);
		}

		// Token: 0x040042EA RID: 17130
		public RectTransform m_rtFloor;

		// Token: 0x040042EB RID: 17131
		public RectTransform m_rtFuniture;

		// Token: 0x040042EC RID: 17132
		public RectTransform m_rtInverse;

		// Token: 0x040042ED RID: 17133
		[Header("가구 기본")]
		protected Image m_imgFloor;

		// Token: 0x040042EE RID: 17134
		protected Image m_imgFuniture;

		// Token: 0x040042EF RID: 17135
		protected Image m_imgInverse;

		// Token: 0x040042F0 RID: 17136
		[Header("가구 추가 이미지들")]
		public Image[] m_aImgFurnitureSub;

		// Token: 0x040042F1 RID: 17137
		public InteriorTarget m_eFunitureType;

		// Token: 0x040042F2 RID: 17138
		[Header("타일 사이즈")]
		public int m_sizeX;

		// Token: 0x040042F3 RID: 17139
		public int m_sizeY;

		// Token: 0x040042F4 RID: 17140
		[Header("터치 사운드")]
		public string m_strTouchSound;

		// Token: 0x040042F5 RID: 17141
		public float m_fSoundDelay;

		// Token: 0x040042F6 RID: 17142
		[Header("상호작용 포지션")]
		public GameObject m_objInteractionPos;

		// Token: 0x040042F7 RID: 17143
		public GameObject m_objInteractionInvertPos;

		// Token: 0x040042F8 RID: 17144
		private int m_id;

		// Token: 0x040042F9 RID: 17145
		private long m_uid;

		// Token: 0x040042FA RID: 17146
		private float m_fTileSize;

		// Token: 0x040042FB RID: 17147
		private Mask m_mask;

		// Token: 0x040042FC RID: 17148
		protected bool m_bInvert;

		// Token: 0x040042FD RID: 17149
		private Vector3 m_originalScale = Vector3.one;

		// Token: 0x040042FE RID: 17150
		private NKCAssetInstanceData m_instanceData;

		// Token: 0x040042FF RID: 17151
		private NKMOfficeInteriorTemplet m_Templet;

		// Token: 0x04004300 RID: 17152
		public NKCOfficeFuniture.OnFunitureDragEvent dOnBeginDragFuniture;

		// Token: 0x04004301 RID: 17153
		public NKCOfficeFuniture.OnFunitureDragEvent dOnDragFuniture;

		// Token: 0x04004302 RID: 17154
		public NKCOfficeFuniture.OnFunitureDragEvent dOnEndDragFuniture;

		// Token: 0x04004303 RID: 17155
		private bool m_bDragged;

		// Token: 0x04004304 RID: 17156
		public NKCOfficeFuniture.OnClickFuniture dOnClickFuniture;

		// Token: 0x04004305 RID: 17157
		private int m_soundUID;

		// Token: 0x04004306 RID: 17158
		private Coroutine soundCoroutine;

		// Token: 0x04004308 RID: 17160
		private NKCAnimationInstance m_animEventInstance;

		// Token: 0x020014E1 RID: 5345
		// (Invoke) Token: 0x0600AA22 RID: 43554
		public delegate void OnFunitureDragEvent(PointerEventData eventData);

		// Token: 0x020014E2 RID: 5346
		// (Invoke) Token: 0x0600AA26 RID: 43558
		public delegate void OnClickFuniture(int id, long uid);
	}
}

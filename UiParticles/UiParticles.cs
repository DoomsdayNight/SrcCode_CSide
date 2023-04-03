using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UiParticles
{
	// Token: 0x02000032 RID: 50
	[RequireComponent(typeof(ParticleSystem))]
	public class UiParticles : MaskableGraphic
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000075C1 File Offset: 0x000057C1
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000075C9 File Offset: 0x000057C9
		public ParticleSystem ParticleSystem
		{
			get
			{
				return this.m_ParticleSystem;
			}
			set
			{
				if (SetPropertyUtility.SetClass<ParticleSystem>(ref this.m_ParticleSystem, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000075DF File Offset: 0x000057DF
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000075E7 File Offset: 0x000057E7
		public ParticleSystemRenderer ParticleSystemRenderer
		{
			get
			{
				return this.m_ParticleSystemRenderer;
			}
			set
			{
				if (SetPropertyUtility.SetClass<ParticleSystemRenderer>(ref this.m_ParticleSystemRenderer, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000075FD File Offset: 0x000057FD
		public override Texture mainTexture
		{
			get
			{
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007631 File Offset: 0x00005831
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00007639 File Offset: 0x00005839
		public UiParticleRenderMode RenderMode
		{
			get
			{
				return this.m_RenderMode;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<UiParticleRenderMode>(ref this.m_RenderMode, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007650 File Offset: 0x00005850
		protected override void Awake()
		{
			ParticleSystem component = base.GetComponent<ParticleSystem>();
			ParticleSystemRenderer component2 = base.GetComponent<ParticleSystemRenderer>();
			if (this.m_Material == null)
			{
				this.m_Material = component2.sharedMaterial;
			}
			if (component2.renderMode == ParticleSystemRenderMode.Stretch)
			{
				this.RenderMode = UiParticleRenderMode.StreachedBillboard;
			}
			this.ParticleSystem = component;
			this.ParticleSystemRenderer = component2;
			this.Initialize();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000076AC File Offset: 0x000058AC
		public void Initialize()
		{
			if (this.ParticleSystemRenderer != null && this.ParticleSystemRenderer.enabled)
			{
				this.ParticleSystemRenderer.enabled = false;
			}
			this.raycastTarget = false;
			this.textureAnimator = this.ParticleSystem.textureSheetAnimation;
			this.frameOverTime = this.textureAnimator.frameOverTime;
			this.startFrame = this.textureAnimator.startFrame;
			this.SetAllDirty();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007720 File Offset: 0x00005920
		public override void SetMaterialDirty()
		{
			base.SetMaterialDirty();
			if (this.ParticleSystemRenderer != null)
			{
				this.ParticleSystemRenderer.sharedMaterial = this.m_Material;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007747 File Offset: 0x00005947
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (base.isActiveAndEnabled)
			{
				if (this.ParticleSystem == null)
				{
					base.OnPopulateMesh(toFill);
					return;
				}
				this.GenerateParticlesBillboards(toFill);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007770 File Offset: 0x00005970
		private void InitParticlesBuffer()
		{
			if (this.m_Particles == null || this.m_Particles.Length < this.ParticleSystem.main.maxParticles)
			{
				this.m_Particles = new ParticleSystem.Particle[this.ParticleSystem.main.maxParticles];
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000077C0 File Offset: 0x000059C0
		private void GenerateParticlesBillboards(VertexHelper vh)
		{
			this.InitParticlesBuffer();
			this.numParticlesAlive = this.ParticleSystem.GetParticles(this.m_Particles);
			if (this.ParticleSystem.textureSheetAnimation.enabled)
			{
				this.textureAnimator = this.ParticleSystem.textureSheetAnimation;
				this.numTilesX = this.textureAnimator.numTilesX;
				this.numTilesY = this.textureAnimator.numTilesY;
				this.startFrame = this.textureAnimator.startFrame;
			}
			vh.Clear();
			this.ParticleSystem.GetCustomParticleData(this.customDataList, ParticleSystemCustomData.Custom1);
			for (int i = 0; i < this.numParticlesAlive; i++)
			{
				this.DrawParticleBillboard(this.m_Particles[i], vh, this.customDataList[i]);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000788C File Offset: 0x00005A8C
		private void DrawParticleBillboard(ParticleSystem.Particle particle, VertexHelper vh, Vector4 customdataStream)
		{
			Vector3 vector = particle.position;
			Quaternion rotation = Quaternion.Euler(particle.rotation3D);
			if (this.ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World)
			{
				vector = base.rectTransform.InverseTransformPoint(vector);
			}
			float num = particle.startLifetime - particle.remainingLifetime;
			float num2 = num / particle.startLifetime + this.zeroNaN;
			Vector3 vector2 = Vector3.zero;
			vector2 = particle.GetCurrentSize3D(this.ParticleSystem);
			if (this.m_RenderMode == UiParticleRenderMode.StreachedBillboard)
			{
				this.GetStrechedBillboardsSizeAndRotation(particle, num2, ref vector2, ref rotation);
			}
			this.bufferVector3.x = -vector2.x * 0.5f;
			this.bufferVector3.y = vector2.y * 0.5f;
			Vector3 vector3 = this.bufferVector3;
			this.bufferVector3.x = vector2.x * 0.5f;
			this.bufferVector3.y = vector2.y * 0.5f;
			Vector3 vector4 = this.bufferVector3;
			this.bufferVector3.x = vector2.x * 0.5f;
			this.bufferVector3.y = -vector2.y * 0.5f;
			Vector3 vector5 = this.bufferVector3;
			this.bufferVector3.x = -vector2.x * 0.5f;
			this.bufferVector3.y = -vector2.y * 0.5f;
			Vector3 vector6 = this.bufferVector3;
			vector3 = rotation * vector3 + vector;
			vector4 = rotation * vector4 + vector;
			vector5 = rotation * vector5 + vector;
			vector6 = rotation * vector6 + vector;
			Color32 currentColor = particle.GetCurrentColor(this.ParticleSystem);
			Color color = currentColor;
			if (Mathf.FloorToInt(num2) == 1)
			{
				color.a = 0f;
				return;
			}
			int currentVertCount = vh.currentVertCount;
			if (!this.ParticleSystem.textureSheetAnimation.enabled)
			{
				this.bufferVector2.x = 0f;
				this.bufferVector2.y = 0f;
				vh.AddVert(vector6, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
				this.bufferVector2.x = 0f;
				this.bufferVector2.y = 1f;
				vh.AddVert(vector3, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
				this.bufferVector2.x = 1f;
				this.bufferVector2.y = 1f;
				vh.AddVert(vector4, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
				this.bufferVector2.x = 1f;
				this.bufferVector2.y = 0f;
				vh.AddVert(vector5, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
			}
			else
			{
				float num3 = particle.startLifetime / (float)this.textureAnimator.cycleCount + this.zeroNaN;
				float time = num % num3 / num3;
				int num4 = this.numTilesY * this.numTilesX;
				float num5 = this.startFrame.constant;
				UnityEngine.Random.InitState((int)particle.randomSeed);
				ParticleSystemCurveMode mode = this.startFrame.mode;
				if (mode != ParticleSystemCurveMode.Constant)
				{
					if (mode == ParticleSystemCurveMode.TwoConstants)
					{
						num5 = UnityEngine.Random.Range(this.startFrame.constantMin, this.startFrame.constantMax);
					}
				}
				else
				{
					num5 = this.startFrame.constant;
				}
				switch (this.frameOverTime.mode)
				{
				case ParticleSystemCurveMode.Constant:
					num5 += this.frameOverTime.constant;
					break;
				case ParticleSystemCurveMode.Curve:
					num5 += this.frameOverTime.Evaluate(time);
					break;
				case ParticleSystemCurveMode.TwoCurves:
					num5 += this.frameOverTime.Evaluate(time);
					break;
				case ParticleSystemCurveMode.TwoConstants:
					num5 = UnityEngine.Random.Range(this.frameOverTime.constantMin, this.frameOverTime.constantMax);
					break;
				}
				num5 = Mathf.Repeat(num5, 1f);
				float num6 = 0f;
				ParticleSystemAnimationType animation = this.textureAnimator.animation;
				if (animation != ParticleSystemAnimationType.WholeSheet)
				{
					if (animation == ParticleSystemAnimationType.SingleRow)
					{
						num6 = Mathf.Clamp(Mathf.Floor(num5 * (float)this.numTilesX), 0f, (float)this.numTilesX);
						int num7 = this.textureAnimator.rowIndex;
						if (this.textureAnimator.rowMode == ParticleSystemAnimationRowMode.Random)
						{
							num7 = UnityEngine.Random.Range(0, this.numTilesY);
						}
						num6 += (float)(num7 * this.numTilesX);
					}
				}
				else
				{
					num6 = Mathf.Clamp(Mathf.Floor(num5 * (float)num4), 0f, (float)num4);
				}
				int num8 = 1;
				int num9 = 1;
				if (!this.numTilesX.Equals(0))
				{
					num8 = (int)num6 % this.numTilesX;
					num9 = (int)num6 / this.numTilesX;
				}
				float num10 = 1f / ((float)this.numTilesX + this.zeroNaN);
				float num11 = 1f / ((float)this.numTilesY + this.zeroNaN);
				num9 = this.numTilesY - 1 - num9;
				float num12 = (float)num8 * num10;
				float num13 = (float)num9 * num11;
				float x = num12 + num10;
				float y = num13 + num11;
				this.bufferVector2.x = num12;
				this.bufferVector2.y = num13;
				vh.AddVert(vector6, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
				this.bufferVector2.x = num12;
				this.bufferVector2.y = y;
				vh.AddVert(vector3, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
				this.bufferVector2.x = x;
				this.bufferVector2.y = y;
				vh.AddVert(vector4, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
				this.bufferVector2.x = x;
				this.bufferVector2.y = num13;
				vh.AddVert(vector5, currentColor, this.bufferVector2, customdataStream, Vector3.zero, Vector4.zero);
			}
			vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007ED4 File Offset: 0x000060D4
		private void GetStrechedBillboardsSizeAndRotation(ParticleSystem.Particle particle, float timeAlive01, ref Vector3 size3D, ref Quaternion rotation)
		{
			if (this.ParticleSystem.velocityOverLifetime.enabled)
			{
				this.bufferVelocity.x = this.ParticleSystem.velocityOverLifetime.x.Evaluate(timeAlive01);
				this.bufferVelocity.y = this.ParticleSystem.velocityOverLifetime.y.Evaluate(timeAlive01);
				this.bufferVelocity.z = this.ParticleSystem.velocityOverLifetime.z.Evaluate(timeAlive01);
			}
			Vector3 vector = particle.velocity + this.bufferVelocity;
			float num = Vector3.Angle(vector, Vector3.up);
			int num2 = (vector.x < 0f) ? 1 : -1;
			this.bufferRotation.Set(0f, 0f, num * (float)num2);
			rotation = Quaternion.Euler(this.bufferRotation);
			size3D.y *= this.m_StretchedLenghScale;
			this.bufferSize.Set(0f, this.m_StretchedSpeedScale * vector.magnitude, 0f);
			size3D += this.bufferSize;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008014 File Offset: 0x00006214
		private void LateUpdate()
		{
			if (base.isActiveAndEnabled && this.ParticleSystem != null && this.ParticleSystem.isPlaying)
			{
				this.SetVerticesDirty();
			}
		}

		// Token: 0x0400011A RID: 282
		[SerializeField]
		[FormerlySerializedAs("m_ParticleSystem")]
		private ParticleSystem m_ParticleSystem;

		// Token: 0x0400011B RID: 283
		[FormerlySerializedAs("m_ParticleSystemRenderer")]
		private ParticleSystemRenderer m_ParticleSystemRenderer;

		// Token: 0x0400011C RID: 284
		[FormerlySerializedAs("m_RenderMode")]
		[SerializeField]
		[Tooltip("Render mode of particles")]
		private UiParticleRenderMode m_RenderMode;

		// Token: 0x0400011D RID: 285
		[FormerlySerializedAs("m_StretchedSpeedScale")]
		[SerializeField]
		[Tooltip("Speed Scale for streched billboards")]
		private float m_StretchedSpeedScale = 1f;

		// Token: 0x0400011E RID: 286
		[FormerlySerializedAs("m_StretchedLenghScale")]
		[SerializeField]
		[Tooltip("Speed Scale for streched billboards")]
		private float m_StretchedLenghScale = 1f;

		// Token: 0x0400011F RID: 287
		private int numParticlesAlive;

		// Token: 0x04000120 RID: 288
		private ParticleSystem.Particle[] m_Particles;

		// Token: 0x04000121 RID: 289
		private List<Vector4> customDataList = new List<Vector4>();

		// Token: 0x04000122 RID: 290
		private Vector2 bufferVector2 = new Vector2(0f, 0f);

		// Token: 0x04000123 RID: 291
		private Vector3 bufferVector3 = new Vector3(0f, 0f, 0f);

		// Token: 0x04000124 RID: 292
		private Vector3 bufferVelocity = new Vector3(0f, 0f, 0f);

		// Token: 0x04000125 RID: 293
		private Vector3 bufferRotation = new Vector3(0f, 0f, 0f);

		// Token: 0x04000126 RID: 294
		private Vector3 bufferSize = new Vector3(0f, 0f, 0f);

		// Token: 0x04000127 RID: 295
		private int numTilesX;

		// Token: 0x04000128 RID: 296
		private int numTilesY;

		// Token: 0x04000129 RID: 297
		private readonly float zeroNaN = 1E-12f;

		// Token: 0x0400012A RID: 298
		private ParticleSystem.TextureSheetAnimationModule textureAnimator;

		// Token: 0x0400012B RID: 299
		private ParticleSystem.MinMaxCurve frameOverTime;

		// Token: 0x0400012C RID: 300
		private ParticleSystem.MinMaxCurve startFrame;
	}
}

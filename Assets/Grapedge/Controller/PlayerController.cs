using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : Grapedge {

	// 位置相关
	public Vector3 playPosition;
	public Vector3 titlePosition;

	// 组件相关
	private Transform m_Transform;
	private Rigidbody2D m_Rigidbody;
	private SpriteRenderer m_Renderer;

	// 玩家相关
	private int m_BirdColor = 0;    // 小鸟的颜色

	// 动画相关
	private float m_AnimTimer = 0f;    // 动画计时器
	private int m_CurFrame = 0;    // 当前播放帧
	public float perFrameTime = 0.2f;    // 每帧耗时
	public Sprite[] birdSprite;    // 小鸟使用的精灵素材

	// 游戏逻辑相关
	public float velocity = 9.165f;
	private float m_RotationTimer = 0f, m_RotationWanted = 0f;    // 旋转使用

	// 音效
	private AudioSource m_Audio;
	public AudioClip wings, die;

	// 界面浮动效果相关
	private float m_ShmTimer = 0f;
	
	private void Start() {
		m_Transform = transform;
		m_Rigidbody = GetComponent<Rigidbody2D>();
		m_Renderer = GetComponent<SpriteRenderer>();
		Random.seed = System.Environment.TickCount;
		m_Audio = GetComponent<AudioSource>();
		Initialize();
	}

	/*************************************
	 * - 游戏运行流程
	 * - 位于标题界面初始化小鸟
	 * - 进入GetReady界面后点击鼠标时开启更新
	 * - 逻辑执行
	 * - 小鸟死亡
	 * - 重新初始化
	 *************************************/

	/// <summary>
	/// 初始化小鸟为准备状态
	/// </summary>
	public override void Initialize() {
		// 更新Transform组件
		m_Transform.position = stateInfo == GameState.title ? titlePosition : playPosition;
		m_Transform.rotation = Quaternion.identity;
		m_Rigidbody.isKinematic = true;
		// 更新颜色
		m_BirdColor = Random.Range(0, 3);
		// 动画计时器清零/近无影响
		m_AnimTimer = 0f;
		// 浮动重新计时
		m_ShmTimer = 0f;
	}

	/// <summary>
	/// 逻辑更新处理
	/// </summary>
	private void Update() {
		UpdateAnimation();
		UpdateSimpleHarmonicMotion();
		RotationUpdate();
		UpdateGame();
	}

	public void StartPlayer() {
		m_Rigidbody.isKinematic = false;
		stateInfo = GameState.playing;
	}
	private void UpdateGame() {
		if (stateInfo != GameState.playing) return;
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space)) { 
			m_Audio.PlayOneShot(wings);
			UpdateFlappyBird ();
		}

	}

	/// <summary>
	/// 使小鸟进行飞行并设置正确的旋转角度
	/// </summary>
	/// <returns>The flappy bird.</returns>
	public void UpdateFlappyBird () {
		m_Rigidbody.velocity = Vector2.up * velocity;
		m_RotationWanted = 25.0f;   // 期待旋转30度
		m_RotationTimer = 0.0f;   // 旋转计时器初始化
	}

	private void RotationUpdate() {
		// 仅在游戏时更新, 特别地，当鸟死亡时仍要更新角度
		if (stateInfo != GameState.playing && stateInfo != GameState.gameover) return;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(
			Vector3.forward * m_RotationWanted), Time.deltaTime * 5f);   // 差值旋转小鸟
		m_RotationTimer += Time.deltaTime;
		
		// 0.64f是一个认为相对合适的数值, 因为此处通过时间计算旋转而非速度大小
		if (m_RotationTimer >= 0.64f) {
			m_RotationTimer = 0;
			m_RotationWanted = -90f;
		}

	}


	/// <summary>
	/// 对于非游戏结束状态, 更新其动画效果
	/// </summary>
	/// <returns>The animation.</returns>
	private void UpdateAnimation() {
		if (stateInfo == GameState.gameover) return;
		m_AnimTimer += Time.deltaTime;    // 计时器
		if (m_AnimTimer >= perFrameTime) {
			m_AnimTimer -= perFrameTime;    // 使动画更真实
			/*************************
			 * 动画精灵的设置采用：每3帧
			 * 为一个动画片段, 即一个颜色
			 *************************/
			m_Renderer.sprite = birdSprite[m_BirdColor * 3 + m_CurFrame];   // 更新动画
			m_CurFrame = m_CurFrame + 1 >= 3 ? 0 : m_CurFrame + 1;    // 更新帧, 防止越界

		}
	}

	/// <summary>
	/// 更新上下浮动效果
	/// </summary>
	/// <returns>The simple harmonic motion.</returns>
	private void UpdateSimpleHarmonicMotion() {
		bool title = stateInfo == GameState.title, 
		get_ready = stateInfo == GameState.get_ready;
		if (!title && !get_ready) return;
		m_ShmTimer += Time.deltaTime;
		float offset = 0.03f * Mathf.Cos(10 * m_ShmTimer);
		transform.position = (title ? titlePosition : playPosition) + Vector3.up * offset;

	}
	
	private void OnCollisionEnter2D (Collision2D col) {
		if (stateInfo == GameState.gameover) return;
		if (col.collider.tag != "Failed") return;
		if (col.collider.name != "Land_1" || col.collider.name != "Land_2") {
			UpdateFlappyBird();
			m_RotationWanted = -90f;
		}
		GameObject.Find("Fade").GetComponent<Animator>().Play ("Flash");
		m_Audio.PlayOneShot(die);
		stateInfo = GameState.gameover;
	}
}

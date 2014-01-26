using UnityEngine;
using System.Collections;

public class CrossfadeSprites : MonoBehaviour {

	public  Sprite[]		sprites;
	public  SpriteRenderer	spriteRenderer1, spriteRenderer2;
	public  Color			color;
	public 	float			duration			=	2f;
	public  int				spriteIndex1		=	0;
	public  int				spriteIndex2		=	0;

	private	bool			_isFadingIn			=	true;
	private	int				_maxSpriteIndex		=	0;
	private float			_t					=	0;

	private Color			_almostTransparent	=	new Color(1, 1, 1, 0.35f);

	void Awake() {
		ChangeSpriteRenderer1();
	}

	// Use this for initialization
	void Start () {
		_maxSpriteIndex = sprites.Length;
	}

	void Update() {
		spriteRenderer1.color = Color.Lerp(_almostTransparent, color, _isFadingIn ? _t : 1 - _t);
		spriteRenderer2.color = Color.Lerp(_almostTransparent, color, _isFadingIn ? 1 -_t : _t);
		_t += Time.deltaTime / duration;
		_t	= Mathf.Clamp(_t, 0, 1);
		if (_t == 1) {
			SwitchFade();
		}
	}

	void SwitchFade() {
		_isFadingIn = !_isFadingIn;
		_t = 0;

		if (spriteRenderer1.color.a < 0.05f) {
			ChangeSpriteRenderer1();
		} else if (spriteRenderer2.color.a < 0.05f) {
			ChangeSpriteRenderer2();
		}
	}

	void ChangeSpriteRenderer1() {
		++spriteIndex1;
		if (spriteIndex1 >= _maxSpriteIndex) spriteIndex1 = 0;
		spriteRenderer1.sprite = sprites[spriteIndex1];
	}

	void ChangeSpriteRenderer2() {
		++spriteIndex2;
		if (spriteIndex2 >= _maxSpriteIndex) spriteIndex2 = 0;
		spriteRenderer2.sprite = sprites[spriteIndex2];
	}
}

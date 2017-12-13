using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    public Animator animator;
    public Text damageText;

    void OnEnable() {
        //AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy(gameObject, 1);//clipInfo[0].clip.length);
    }

    public void SetText(string text) {
        damageText.text = text;
    }
}

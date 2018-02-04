using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Spell {
    public float speed = 5f;
    public float slowAmount = 0.8f; //decrease 80% speed
    public float slowRadius = 4f; //decrease 80% speed
    private Vector3 direction;
    public Light pointLight;
    public GameObject SlowRangePrefab;

    #region ParticleSystem
    public ParticleSystem ps;
    private float targetStartSize;
    private float curStartSize;
    private bool isInitSizing = true;
    private float curVelocitySmoothDamp = 0f;


    private float maxScaleMultiplier = 3f; //double the size of fireball;
    private Vector3 maxScale;
    private Vector3 scaleSmoothDamp;


    #endregion

    protected override void Start_Derived() {
        targetStartSize = ps.main.startSize.constant;
        curStartSize = 0f;
        maxScale = transform.lossyScale * maxScaleMultiplier;
        StartCoroutine(Show());
    }

    protected override void Update_Derived() {
        if(Input.GetKeyDown(KeyCode.Space))
            HidePreview();
        if(fired) {
            transform.Translate(direction * (speed * Time.deltaTime), Space.World);
            // TODO: edit gravity of fireball to get the moving feel
        }
    }

    protected override void FixedUpdate_Derived() { }

    public override void OnPlayerHoldSpell() {
        //move itself towards the hand position
        if(transform.parent)
            transform.position = Vector3.Lerp(transform.position, transform.parent.position + transform.parent.forward * 0.2f, 0.2f);
        if(!isInitSizing) //wait till the spell is on a ready size. Then increase the size;
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, maxScale, ref scaleSmoothDamp, 5f);
            //add damage
        }

    }

    public override void Fire(Vector3 handDirection) {
        transform.parent = null;
        direction = handDirection;
        fired = true;
        StartCoroutine(LifeTimeOut());
    }

    void OnTriggerEnter(Collider collider) {
        var slowRange = Instantiate(SlowRangePrefab);
        slowRange.transform.localScale *= slowRadius;
        slowRange.transform.position = transform.position;
        Destroy(gameObject);
    }


    public override void ShowPreview() {
        StartCoroutine(Show());
    }
    public override void HidePreview() {
        StartCoroutine(HideAndDestroy());
    }
    private IEnumerator HideAndDestroy() {
        Vector3 scaleAtThatMoment = transform.localScale;
        float intensityAtThatMoment = pointLight.intensity;
        float curDuration = fadeDuration;
        while(curDuration > 0f) {
            var a = Time.deltaTime / fadeDuration;
            transform.localScale -= scaleAtThatMoment * a;
            pointLight.intensity -= intensityAtThatMoment * a;
            curDuration -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    private IEnumerator Show() {
        Vector3 scaleAtThatMoment = transform.localScale;
        float intensityAtThatMoment = pointLight.intensity;
        transform.localScale = Vector3.zero;
        pointLight.intensity = 0f;
        float curDuration = fadeDuration;
        curStartSize = Mathf.SmoothDamp(curStartSize, targetStartSize, ref curVelocitySmoothDamp, 1f);
        while(curDuration > 0f) {
            var a = Time.deltaTime / fadeDuration;
            transform.localScale += scaleAtThatMoment * a;
            pointLight.intensity += intensityAtThatMoment * a;
            curDuration -= Time.deltaTime;
            yield return null;
        }
        isInitSizing = false;
    }


    
    protected IEnumerator LifeTimeOut() {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }

}

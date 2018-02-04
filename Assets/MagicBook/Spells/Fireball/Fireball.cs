using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
    public float speed = 5f;
    public float damage = Mathf.Ceil(200f / 3f); // 200 is the amount of health bears currently have
    public float maxAdditionalDamage = 100f;
    public float playerHoldScalingDuration = 3f;
    private Vector3 direction;
    public Light pointLight;

    #region ParticleSystem
    public ParticleSystem ps;
    private float targetStartSize;
    private float curStartSize;
    private float curVelocitySmoothDamp = 0f;


    private float maxScaleMultiplier = 10f; //double the size of fireball;
    private Vector3 maxAdditionalScale;


    #endregion

    protected override void Start_Derived()
    {
        targetStartSize = ps.main.startSize.constant;
        curStartSize = 0f;
        maxAdditionalScale = transform.lossyScale * maxScaleMultiplier;
        StartCoroutine(Show());
    }

    protected override void Update_Derived()
    {
        if (Input.GetKey(KeyCode.Space))
            OnPlayerHoldSpell();
        if (fired)
        {
            transform.Translate(direction * (speed * Time.deltaTime), Space.World);
            // TODO: edit gravity of fireball to get the moving feel
        }
    }

    protected override void FixedUpdate_Derived() { }

    public override void OnPlayerHoldSpell()
    {
        //move itself towards the hand position
        if (transform.parent)
            transform.position = Vector3.Lerp(transform.position, transform.parent.position + transform.parent.forward * 0.2f, 0.2f);
        if (!isInitSizing) //wait till the spell is on a ready size. Then increase the size;
        {
            if (transform.lossyScale.x >= 0.5f)
                return;
            var a = Time.deltaTime / playerHoldScalingDuration;
            transform.localScale += maxAdditionalScale * Time.deltaTime * 2f;
            damage += maxAdditionalDamage * a * 0.65f;
            print(damage);
        }

    }

    public override void Fire(Vector3 handDirection)
    {
        transform.parent = null;
        direction = handDirection;
        fired = true;
        StartCoroutine(LifeTimeOut());
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Spell>()) //don't collide with spells
            return;
        if (!fired)
            return;
        IDamagable damagable = collider.gameObject.GetComponentInParent<IDamagable>();
        if (damagable != null)
        {
            damagable.InflictDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            // To ignore collisions with MagicBook's own collider; layer 0 is the Default layer
            if (collider.gameObject.layer == 0)
                Destroy(gameObject);
        }
    }



    public override void ShowPreview()
    {
        StartCoroutine(Show());
    }

    public override void HidePreview()
    {
        StartCoroutine(HideAndDestroy());
    }


    private IEnumerator HideAndDestroy()
    { //gradually decrease the size of the spell and finally destroy
        Vector3 scaleAtThatMoment = transform.localScale;
        float intensityAtThatMoment = pointLight.intensity;
        float curDuration = fadeDuration;
        while (curDuration > 0f)
        {
            var a = Time.deltaTime / fadeDuration;
            transform.localScale -= scaleAtThatMoment * a;
            pointLight.intensity -= intensityAtThatMoment * a;
            curDuration -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }


    private IEnumerator Show()
    { //gradually increase the size of the spell
        Vector3 scaleAtThatMoment = transform.localScale;
        float intensityAtThatMoment = pointLight.intensity;
        transform.localScale = Vector3.zero;
        pointLight.intensity = 0f;
        float curDuration = fadeDuration;
        yield return new WaitForSeconds(delayBetweenSpawns);
        while (curDuration > 0f)
        {
            var a = Time.deltaTime / fadeDuration;
            transform.localScale += scaleAtThatMoment * a;
            pointLight.intensity += intensityAtThatMoment * a;
            curDuration -= Time.deltaTime;
            yield return null;
        }
        isInitSizing = false;
    }

    protected IEnumerator LifeTimeOut()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}

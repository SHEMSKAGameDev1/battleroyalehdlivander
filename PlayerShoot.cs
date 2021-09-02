using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Camera camera;

    public float damage;

    public bool canShoot = true;
    WaitForSeconds shootDelay = new WaitForSeconds(0.2f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerMovement.animator.SetBool("Shoot", true);
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            playerMovement.animator.SetBool("Shoot", false);
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(ShootDelayCoroutine());
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
            {
                print("benda yang tertembak : " + hit.collider.gameObject.name);
                if (hit.collider.GetComponent<Health>())
                {
                    Instantiate(VFxManager.instance.bloodImpac, hit.point, Quaternion.identity);
                    hit.collider.GetComponent<Health>().TakeDamage(damage);
                }
                else
                {
                    Instantiate(VFxManager.instance.woodImpac, hit.point, Quaternion.identity);
                }
            }
        }
    }

    IEnumerator ShootDelayCoroutine()
    {
        canShoot = false;
        yield return shootDelay;
        canShoot = true;
    }
}

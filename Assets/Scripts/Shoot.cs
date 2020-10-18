using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject shellSpawnPosition;
    public GameObject target;
    public GameObject parrent;
    float speed = 17f;
    float turnSpeed = 3f;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    void CanShootAgain()
    {
        canShoot = true;
    }

    void Fire()
    {
        if (canShoot)
        {
            GameObject shell = Instantiate(shellPrefab, shellSpawnPosition.transform.position, shellSpawnPosition.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
            canShoot = false;
            Invoke("CanShootAgain", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.transform.position - parrent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        parrent.transform.rotation = Quaternion.Slerp(parrent.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        float? angle = RotateTurret();

        if (angle != null && Vector3.Angle(direction, parrent.transform.forward) < 10)
            Fire();
    }

    float? RotateTurret()
    {
        float? angle = CalculateAngle(true);

        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360 - (float)angle, 0f, 0f);
        }

        return angle;
    }    

    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float speedSquare = speed * speed;
        float underTheSquareRoot = (speedSquare * speedSquare) - gravity * (gravity * x * x + 2 * y * speedSquare);

        if (underTheSquareRoot >= 0)
        {
            float root = Mathf.Sqrt(underTheSquareRoot);
            float highAngle = speedSquare + root;
            float lowAngle = speedSquare - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else
        {
            return null;
        }
    }
}

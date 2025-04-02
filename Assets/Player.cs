using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Debug data")]
    public float verticalInput;
    public float horizontalInput;
    public float towerRotationSpeed;

    [Space]
    public LayerMask whatIsAimMask;
    public Transform aimTransform;

    [Header("Movement data")]
    public float moveSpeed;
    public float rotationSpeed;

    [Header("Tower data")]
    public Transform tankTower;

    [Header("Gun data")]
    [SerializeField]
    private Transform gunPoint;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private GameObject bulletPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 2.5f;
        rotationSpeed = 1;
    }

    void Update()
    {
        UpdateAim();
        CheckInputs();
    }

    private void CheckInputs()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput < 0)
            horizontalInput = -horizontalInput;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyBodyRotation();
        ApplyTowerRotation();
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = gunPoint.forward * bulletSpeed;

        Destroy(bullet, 7);
    }

    private void ApplyTowerRotation()
    {
        Vector3 direction = aimTransform.position - tankTower.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        tankTower.rotation = Quaternion.RotateTowards(tankTower.rotation, targetRotation, towerRotationSpeed);
    }

    private void ApplyBodyRotation()
    {
        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }

    private void ApplyMovement()
    {
        Vector3 movement = transform.forward * moveSpeed * verticalInput;
        rb.velocity = movement;
    }

    private void UpdateAim()
    {   
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsAimMask)) 
        {
            float fixedY = aimTransform.position.y;
            aimTransform.position = new Vector3(hit.point.x, fixedY, hit.point.z);
            Debug.Log(hit.point);
        }
    }
}

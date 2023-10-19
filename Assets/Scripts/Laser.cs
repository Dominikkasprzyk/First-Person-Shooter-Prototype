using UnityEngine;

public class Laser : MonoBehaviour
{ 
    [SerializeField] private GameObject HitEffect;
    [SerializeField] private float HitOffset = 0;
    [SerializeField] private float MaxLength;
    [SerializeField] private float MainTextureLength = 1f;
    [SerializeField] private float NoiseTextureLength = 1f;

    private Vector4 Length = new Vector4(1, 1, 1, 1);
    private bool LaserSaver = false;
    private bool UpdateSaver = false;
    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;
    private LineRenderer LaserBeam;

    void Awake()
    {
        LaserBeam = GetComponent<LineRenderer>();
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        LaserBeam.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));
        LaserBeam.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));
        //To set LineRender position
        if (LaserBeam != null && UpdateSaver == false)
        {
            LaserBeam.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
            {
                //End laser position if collides with object
                LaserBeam.SetPosition(1, hit.point);

                HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                HitEffect.transform.LookAt(hit.point + hit.normal);

                foreach (var AllPs in Effects)
                {
                    if (!AllPs.isPlaying) AllPs.Play();
                }
                //Texture tiling
                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, hit.point));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, hit.point));
            }
            else
            {
                //End laser position if doesn't collide with object
                var EndPos = transform.position + transform.forward * MaxLength;
                LaserBeam.SetPosition(1, EndPos);
                HitEffect.transform.position = EndPos;
                foreach (var AllPs in Hit)
                {
                    if (AllPs.isPlaying) AllPs.Stop();
                }
                //Texture tiling
                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, EndPos));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, EndPos));
            }
            //Insurance against the appearance of a laser in the center of coordinates!
            if (LaserBeam.enabled == false && LaserSaver == false)
            {
                LaserSaver = true;
                LaserBeam.enabled = true;
            }
        }
    }

    public void DisablePrepare()
    {
        if (LaserBeam != null)
        {
            LaserBeam.enabled = false;
        }
        UpdateSaver = true;
        //Effects can = null in multiply shooting
        if (Effects != null)
        {
            foreach (var AllPs in Effects)
            {
                if (AllPs.isPlaying) AllPs.Stop();
            }
        }
    }
}

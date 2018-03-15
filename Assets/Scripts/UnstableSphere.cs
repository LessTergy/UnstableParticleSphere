using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class UnstableSphere : MonoBehaviour {

    [Header("Particle Preferences")]
    public int particleCount = 1000;
    public float particleSize = 0.1f;


    [Space(10)]
    [Header("Sphere Preferences")]
    public float sphereRadius = 5f;

    [Space(10)]
    [Header("Physics Preferences")]
    public float particleMass = 0.01f;

    [Range(1f, 85f)]
    public float deltaGravityAngleForChange = 20f;
    public Vector3 accelerateDirection = new Vector3(0f, 10f, 0f);

    [Range(1f, 200f)]
    public float forceRatio = 1f;

    public Color upColor;
    public Color downColor;

    public float timeColorDelay = 1f;
    private TimeSince timeColorSince = new TimeSince();

    private Vector3 circleCenter;

    private new ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private ParticleData[] dataList;
    

	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        InitParticles();

        circleCenter = transform.position;
        Cursor.visible = false;

        lastUpColor = upColor;
        lastDownColor = downColor;
        nextUpColor = upColor;
        nextDownColor = downColor;
	}

    private void OnValidate() {
        InitParticles();
    }

    private void InitParticles() {
        particles = new ParticleSystem.Particle[particleCount];
        dataList = new ParticleData[particleCount];

        for (int i = 0; i < particleCount; i++) {
            //Get
            ParticleSystem.Particle particle = particles[i];
            ParticleData data = dataList[i];

            //Change
            Vector3 randomPoint = Random.insideUnitSphere;
            Vector3 startPosition = randomPoint * sphereRadius + transform.position;
            particle.position = startPosition;
            data.startPosition = startPosition;

            particle.startColor = new Color(1, 1, 1);
            particle.startSize = particleSize;

            //Apply
            particles[i] = particle;
            dataList[i] = data;
        }
    }
	
	void Update () {
        //Resimulate
        if (Input.GetKeyDown(KeyCode.Space)) {
            InitParticles();
        }

        UpdateColors();
	}

    private void FixedUpdate() {
        UpdateParticles();
    }

    private void UpdateParticles() {
        for (int i = 0; i < particleCount; i++) {
            //Get
            ParticleSystem.Particle particle = particles[i];
            ParticleData data = dataList[i];

            #region Change
            //Gravity
            Vector3 direction = circleCenter - particle.position;
            Vector3 gravityForce = direction.normalized * forceRatio * particleMass;

            float deltaGravityAngle = Vector3.Angle(gravityForce, data.prevGravityForce);
            
            if (deltaGravityAngle > deltaGravityAngleForChange) {
                data.acceleration = accelerateDirection;
            } else {
                data.acceleration += gravityForce;
            }

            data.prevGravityForce = gravityForce;

            //Color
            Color color = Color.Lerp(downColor, upColor, Mathl.Map(particle.position.x, -20, 20, 0, 1f));
            particle.startColor = color;

            //Acceleration
            particle.position += data.acceleration * Time.deltaTime;
            #endregion

            //Apply
            particles[i] = particle;
            dataList[i] = data;
        }

        particleSystem.SetParticles(particles, particleCount);
    }


    Color lastUpColor;
    Color lastDownColor;

    Color nextUpColor;
    Color nextDownColor;
    private void UpdateColors() {
        float delta = timeColorSince.delta;

        float map = Mathl.Map(delta, 0f, timeColorDelay, 0f, 1f);

        upColor = Color.Lerp(lastUpColor, nextUpColor, map);
        downColor = Color.Lerp(lastDownColor, nextDownColor, map);

        if (delta > timeColorDelay) {
            lastUpColor = upColor;
            lastDownColor = downColor;

            nextUpColor = new Color(Random.value, Random.value, Random.value);
            nextDownColor = new Color(Random.value, Random.value, Random.value);
            
            timeColorSince.Fixate();
        }
    }

    public struct ParticleData {
        public Vector3 startPosition;
        public Vector3 acceleration;
        public Vector3 prevGravityForce;
    }
}

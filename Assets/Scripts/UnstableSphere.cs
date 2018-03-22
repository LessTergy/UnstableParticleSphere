using UnityEngine;
using Lesstergy.Colors;

[RequireComponent(typeof(ParticleSystem))]
public class UnstableSphere : MonoBehaviour {

    [Header("Particle Preferences")]
    public int particleCount = 1000;
    public float particleSize = 0.1f;
    
    [Space(10)]
    [Header("Sphere Preferences")]
    public float sphereRadius = 5f;
    public ChangingColor changingColor;

    [Space(10)]
    [Header("Physics Preferences")]
    public float particleMass = 0.01f;

    [Range(1f, 180f)]
    public float gravityAngle = 20f;
    [Range(1f, 200f)]
    public float gravityForceRatio = 1f;
    public Vector3 accelerateDirection = new Vector3(0f, 10f, 0f);


    //private data
    private Vector3 circleCenter;

    private new ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private ParticleData[] dataList;
    

	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        InitParticles();

        circleCenter = transform.position;
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

            particle.startColor = Color.white;
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
	}

    private void FixedUpdate() {
        UpdateParticles();
    }

    private void UpdateParticles() {
        changingColor.Update();

        for (int i = 0; i < particleCount; i++) {
            //Get
            ParticleSystem.Particle particle = particles[i];
            ParticleData data = dataList[i];

            #region Change
            //Gravity
            Vector3 gravityDirection = circleCenter - particle.position;
            Vector3 gravityForce = gravityDirection.normalized * gravityForceRatio * particleMass;

            float deltaGravityAngle = Vector3.Angle(gravityForce, data.prevGravityForce);
            
            bool isExceedAngle = deltaGravityAngle > gravityAngle;
            if (isExceedAngle) {
                data.acceleration = accelerateDirection;
            } else {
                data.acceleration += gravityForce;
            }

            data.prevGravityForce = gravityForce;

            //Color
            particle.startColor = changingColor.value;

            //Acceleration
            particle.position += data.acceleration * Time.deltaTime;
            #endregion

            //Apply
            particles[i] = particle;
            dataList[i] = data;
        }

        particleSystem.SetParticles(particles, particleCount);
    }

    public struct ParticleData {
        public Vector3 acceleration;
        public Vector3 prevGravityForce;
    }
}

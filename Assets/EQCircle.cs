using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQCircle : MonoBehaviour {

    public int maxParticles = 100;
    public float particleSize = 0.1f;

    public float circleRadius = 1f;
    public float gravitationRadius = 0.01f;

    public float gravityPointMass = 1f;
    public float particleMass = 0.01f;

    [Range(1f, 85f)]
    public float angle = 1f;
    public Vector3 acceleration = Vector3.one;

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

	// Use this for initialization
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
        particles = new ParticleSystem.Particle[maxParticles];
        dataList = new ParticleData[maxParticles];

        for (int i = 0; i < maxParticles; i++) {
            //Get
            ParticleSystem.Particle particle = particles[i];
            ParticleData data = dataList[i];

            //Change
            Vector3 randomPoint = Random.insideUnitSphere;
            Vector3 startPosition = randomPoint * circleRadius + transform.position;
            data.gravitationPoint = randomPoint * gravitationRadius + transform.position;
            particle.position = startPosition;
            data.startPosition = startPosition;

            particle.startColor = new Color(1, 1, 1);
            particle.startSize = particleSize;

            //Apply
            particles[i] = particle;
            dataList[i] = data;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            InitParticles();
        }
        UpdateColors();
        UpdateParticles();
	}

    private void UpdateParticles() {
        for (int i = 0; i < maxParticles; i++) {
            //Get
            ParticleSystem.Particle particle = particles[i];
            ParticleData data = dataList[i];

            //Change


            //Gravity
            Vector3 direction = circleCenter - particle.position;
            Vector3 gravityForce = direction.normalized * gravityPointMass * particleMass * forceRatio;

            float gravityAngle = Vector3.Angle(gravityForce, data.prevGravityForce);

            //inverse direction
            data.acceleration += gravityForce;

            if (gravityAngle > angle) {
                data.acceleration = acceleration;
            }

            data.prevGravityForce = gravityForce;

            //Color
            Color color = Color.Lerp(downColor, upColor, Mathl.Map(particle.position.x, -20, 20, 0, 1f));
            particle.startColor = color;

            //Size
            //particle.startSize = direction.sqrMagnitude * 0.01f;

            //Acceleration
            particle.position += data.acceleration * Time.deltaTime;

            //Apply
            particles[i] = particle;
            dataList[i] = data;
        }

        particleSystem.SetParticles(particles, maxParticles);
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
        public Vector3 gravitationPoint;
        public Vector3 startPosition;
        public Vector3 acceleration;
        public Vector3 prevGravityForce;
    }
}
